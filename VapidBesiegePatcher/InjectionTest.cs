using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Vapid.ModLoader;

namespace Vapid.Patcher
{
	public class InjectionTest
	{
		private readonly AssemblyDefinition game;
		private readonly ModuleDefinition module;

		public InjectionTest(AssemblyDefinition game)
		{
			this.game = game;
			module = game.MainModule;

			Inject();
		}

		public void Inject()
		{
			TypeDefinition addPiece = module.GetType("AddPiece");
			MethodDefinition sendSimulateMessage = addPiece.Methods.First(m => m.Name == "SendSimulateMessage");

			ILProcessor processor = sendSimulateMessage.Body.GetILProcessor();
			// processor.Body.Instructions.Insert(0, processor.Create(OpCodes.Ldarg_0));
			processor.Body.Instructions.Insert(1, processor.Create(OpCodes.Ldarg_1));
			processor.Body.Instructions.Insert(2, processor.Create(OpCodes.Call, Util.ImportMethod<RecieverTest>(game, "RecieveSimulationToggle", typeof(bool))));
		}
	}
}