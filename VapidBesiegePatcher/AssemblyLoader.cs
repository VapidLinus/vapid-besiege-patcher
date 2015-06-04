using Mono.Cecil;
using System;
using System.IO;

namespace Vapid.Patcher
{
	public class AssemblyLoader
	{
		public AssemblyDefinition GameAssembly { get; private set; }
		public AssemblyDefinition GameBackupAssembly { get; private set; }
		public AssemblyDefinition ModAssembly { get; private set; }

		private readonly MainForm main;

		public AssemblyLoader(MainForm main)
		{
			this.main = main;
		}

		/// <summary>
		/// Will try to load the game assembly and the mod assembly.
		/// A backup will be created.
		/// </summary>
		/// <returns>Whether the assemblies where loaded successfully and a backup was created or found.</returns>
		public bool Load()
		{
			if (!RestoreBackup())
			{
				return false;
			}

			string backupPath = main.UnityScriptAssemblyBackupPath;
			string gamePath = main.UnityScriptAssemblyPath;
			string modPath = main.ModAssemblyPath;
			main.Logger.Clear();

			// Does a backup exist?
			if (File.Exists(backupPath))
			{
				try
				{
					// Load backup
					main.Logger.Log("Loading backup...", false);
					GameBackupAssembly = AssemblyDefinition.ReadAssembly(backupPath);
					main.Logger.Log(" Done.");
				}
				catch (Exception e)
				{
					// Unable to load backup
					main.Logger.LogError("Backup file couldn't be loaded!\n" + e.Message + "\nIf you updated Besiege and you're trying to patch again, press 'Reset Backup'.");
					return false;
				}
			}
			else
			{
				// There is no backup
				try
				{
					// Create backup and then load backup
					main.Logger.Log("Creating backup...", false);
					File.Copy(main.UnityScriptAssemblyPath, backupPath);
					GameBackupAssembly = AssemblyDefinition.ReadAssembly(backupPath);
					main.Logger.Log(" Done.");
				}
				catch (Exception exception)
				{
					// Unable to create backup
					main.Logger.LogError("Couldn't create backup.\n" + exception.Message);
					return false;
				}
			}

			try
			{
				main.Logger.Log("Loading game assembly...", false);
				GameAssembly = AssemblyDefinition.ReadAssembly(gamePath);
				main.Logger.Log(" Done.");
			}
			catch (Exception exception)
			{
				// Unable to create backup
				main.Logger.LogError("Couldn't load game assembly.\n" + exception.Message);
				return false;
			}

			// Is mod installed?
			if (File.Exists(modPath))
			{
				try
				{
					// Load mod
					main.Logger.Log("Loading mod...", false);
					ModAssembly = AssemblyDefinition.ReadAssembly(modPath);
					main.Logger.Log(" Done.");
				}
				catch (Exception e)
				{
					// Unable to load backup
					main.Logger.LogError("Mod file couldn't be loaded!\n" + e.Message + "\nMake sure you've got the latest version of " + Paths.MOD_DLL + " and it's not corrupted.");
					return false;
				}
			}
			else
			{
				main.Logger.LogError("Couldn't find " + Paths.MOD_DLL + "!\nMake sure you've placed " + Paths.MOD_DLL + " in " + Paths.ModsFolder + ".");
				return false;
			}

			return true;
		}

		public bool RestoreBackup()
		{
			try
			{
				if (File.Exists(main.UnityScriptAssemblyBackupPath))
				{
					File.Copy(main.UnityScriptAssemblyBackupPath, main.UnityScriptAssemblyPath, true);
					main.Logger.Log("Backup restored.");
				}
				else
				{
					main.Logger.Log("No backup found.");
				}
				return true;
			}
			catch (Exception e)
			{
				main.Logger.LogError("Failed restoring backup!\n" + e.Message);
				return false;
			}
		}

		public void DeleteBackup()
		{
			main.Logger.Clear();
			try
			{
				File.Delete(main.UnityScriptAssemblyBackupPath);
				main.Logger.LogSuccess("Backup deleted!");
			}
			catch (DirectoryNotFoundException)
			{
				main.Logger.Log("Backup already deleted.");
			}
			catch (Exception exception)
			{
				main.Logger.LogError("Couldn't delete backup.\n" + exception.Message);
			}
		}
	}
}