using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;
using Activator = Vapid.ModLoader.Activator;

namespace Vapid.Patcher
{
	public static class Injector
	{
		public static void Inject(MainForm main, AssemblyDefinition game, AssemblyDefinition mod, Logger logger)
		{
			logger.Log("Starting injection.");

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

			// Inject startup code
			var processor = gameAwakeMethod.Body.GetILProcessor();
			processor.Body.Instructions.Insert(0, processor.Create(OpCodes.Call, Util.ImportMethod<Activator>(game, "Activate")));

			new InjectionTest(game);

			game.Write(main.UnityScriptAssemblyPath);
			logger.LogSuccess("Injection completed successfully.");
		}
	}
}