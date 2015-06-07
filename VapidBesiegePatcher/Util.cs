using Mono.Cecil;
using System;
using System.Linq;
using System.Reflection;

namespace Vapid.Patcher
{
	public static class Util
	{
		public static MethodReference ImportMethod<T>(AssemblyDefinition assembly, string name)
		{
			return assembly.MainModule.Import(typeof(T).GetMethod(name, Type.EmptyTypes));
		}

		public static MethodReference ImportMethod<T>(AssemblyDefinition assembly, string name, params Type[] types)
		{
			return assembly.MainModule.Import(typeof(T).GetMethod(name, types));
		}

		public static MethodReference ImportMethod(AssemblyDefinition assembly, string type, string method, params Type[] types)
		{
			TypeReference reference = assembly.MainModule.Types.First(t => t.Name == type);
			return assembly.MainModule.Import(reference.Resolve().Methods.First(m => m.Name == method));
		}
	}
}