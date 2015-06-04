using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Linq;
using UnityEngine;

namespace Vapid.Patcher
{
	public static class Injector
	{
		public static void Inject(MainForm main, AssemblyDefinition game, AssemblyDefinition mod, Logger logger)
		{
			logger.Log("Starting injection.");

			try
			{
				// Get a reference to PlanetRotateMouse type
				TypeDefinition gameType = game.MainModule.GetType("", "PlanetRotateMouse");

				// If this type wasn't found it's likely the game is outdated
				if (gameType == null)
				{
					logger.LogError("Couldn't find PlanetRotateMouse type. Is the game or mod loader outdated?");
					return;
				}

				// Get a reference to PlanetRotateMouse.Start()
				MethodDefinition gameAwakeMethod = gameType.Methods.FirstOrDefault(method => method.Name == "Start");

				// If this method wasn't found it's likely the game is outdated
				if (gameAwakeMethod == null)
				{
					logger.LogError("Couldn't find PlanetRotateMouse.Start(). Is the game or mod loader outdated?");
					return;
				}

				/* Write IL code for this
				*  Assembly.LoadFrom(Path.Combine(Application.dataPath, "Mods/VapidModLoader.dll")).GetType("Vapid.ModLoader.ModLoaderActivator").GetMethod("Activate").Invoke(null, null);
				*/
				var processor = gameAwakeMethod.Body.GetILProcessor();
				processor.Body.Instructions.Insert(0, processor.Create(OpCodes.Call, ImportMethod<Application>(game, "get_dataPath")));
				processor.Body.Instructions.Insert(1, processor.Create(OpCodes.Ldstr, "/Mods/VapidModLoader.dll"));
				processor.Body.Instructions.Insert(2, processor.Create(OpCodes.Call, ImportMethod<String>(game, "Concat", typeof(string), typeof(string))));
				processor.Body.Instructions.Insert(3, processor.Create(OpCodes.Call, ImportMethod<Assembly>(game, "LoadFrom", typeof(string))));
				processor.Body.Instructions.Insert(4, processor.Create(OpCodes.Ldstr, "Vapid.ModLoader.Activator"));
				processor.Body.Instructions.Insert(5, processor.Create(OpCodes.Callvirt, ImportMethod<Assembly>(game, "GetType", typeof(string))));
				processor.Body.Instructions.Insert(6, processor.Create(OpCodes.Ldstr, "Activate"));
				processor.Body.Instructions.Insert(7, processor.Create(OpCodes.Callvirt, ImportMethod<Type>(game, "GetMethod", typeof(string))));
				processor.Body.Instructions.Insert(8, processor.Create(OpCodes.Ldnull));
				processor.Body.Instructions.Insert(9, processor.Create(OpCodes.Ldnull));
				processor.Body.Instructions.Insert(10,processor.Create(OpCodes.Callvirt, ImportMethod<MethodBase>(game, "Invoke", typeof(object), typeof(object[]))));
				processor.Body.Instructions.Insert(11,processor.Create(OpCodes.Pop));

				game.Write(main.UnityScriptAssemblyPath);
			}
			catch (Exception e)
			{
				logger.LogError("Failed injecting modloader.\n" + e.Message);
				return;
			}
			
			logger.LogSuccess("Injection completed successfully.");
		}

		private static MethodReference ImportMethod<T>(AssemblyDefinition assembly, string name)
		{
			return assembly.MainModule.Import(typeof(T).GetMethod(name, Type.EmptyTypes));
		}

		private static MethodReference ImportMethod<T>(AssemblyDefinition assembly, string name, params Type[] types)
		{
			return assembly.MainModule.Import(typeof(T).GetMethod(name, types));
		}
	}
}