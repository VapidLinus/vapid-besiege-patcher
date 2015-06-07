using System.IO;

namespace Vapid.Patcher
{
	public static class Paths
	{
		public const string MOD_DLL = "VapidModLoader.dll";
		public const string UNITYSCRIPT_DLL = "Assembly-UnityScript.dll";
		public const string UNITYSCRIPT_DLL_BACKUP = "VAPIDBACKUP_Assembly-UnityScript.dll";

		public static string ManagedFolder
		{
			get { return Path.Combine("Besiege_Data", "Managed"); }
		}

		public static string ModsFolder
		{
			get { return Path.Combine("Besiege_Data", "Managed"); }
		}
	}
}