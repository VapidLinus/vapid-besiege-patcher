using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Vapid.Patcher
{
	public partial class MainForm : Form
	{
		public string ModAssemblyPath
		{
			get { return Path.Combine(GameFolderPath, Paths.ModsFolder, Paths.MOD_DLL); }
		}
		public string UnityScriptAssemblyPath
		{
			get { return Path.Combine(GameFolderPath, Paths.ManagedFolder, Paths.UNITYSCRIPT_DLL); }
		}
		public string UnityScriptAssemblyBackupPath
		{
			get { return Path.Combine(GameFolderPath, Paths.ManagedFolder, Paths.UNITYSCRIPT_DLL_BACKUP); }
		}

		public string GamePath
		{
			get { return textGamePath.Text; }
			set { textGamePath.Text = value; }
		}
		public string GameFolderPath
		{
			get { return Path.GetDirectoryName(GamePath); }
		}

		public Logger Logger { get; private set; }
		private readonly AssemblyLoader loader;

		public MainForm()
		{
			InitializeComponent();

			Logger = new Logger(textLog);
			loader = new AssemblyLoader(this);

			string defaultPath = null;
			try
			{
				defaultPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null).ToString();
				Debug.WriteLine("Steam path: " + defaultPath);
			}
			catch (Exception ex) { Debug.WriteLine("Can't find Steam path: " + ex.Message); }

			if (!String.IsNullOrWhiteSpace(defaultPath))
			{
				defaultPath = Path.Combine(defaultPath, "steamapps", "common", "besiege", "besiege.exe");
				Debug.WriteLine("Game path: " + defaultPath);
				if (File.Exists(defaultPath))
				{
					GamePath = Path.GetFullPath(defaultPath);
				}
				else { Debug.WriteLine("Game path doesn't exist."); }
			}
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog()
			{
				DefaultExt = ".exe",
				Title = @"Select Besiege.exe"
			};

			if (!String.IsNullOrWhiteSpace(GamePath))
			{
				dialog.RestoreDirectory = false;
				dialog.InitialDirectory = Path.GetDirectoryName(GamePath);
			}

			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				GamePath = dialog.FileName;
			}
		}

		private void buttonInject_Click(object sender, EventArgs e)
		{
			SetButtonsEnabled(false);
			if (loader.Load())
			{
				Injector.Inject(this, loader.GameAssembly, loader.ModAssembly, Logger);
			}
			SetButtonsEnabled(true);
		}

		private void SetButtonsEnabled(bool enabled) 
		{
			foreach (var control in this.Controls)
			{
				Button button = control as Button;
				if (button != null)
				{
					button.Enabled = enabled;
				}
			}
		}

		private void buttonResetBackup_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("You are about to delete the backup.\nIf you do this, you will not be able to restore the unmodded file. You should only do this if you recently updated Besiege and want to inject the modloader again.", "Warning", MessageBoxButtons.OKCancel);
			if (result == DialogResult.OK)
			{
				loader.DeleteBackup();
			}
		}

		private void buttonRestore_Click(object sender, EventArgs e)
		{
			Logger.Clear();
			if (loader.RestoreBackup())
			{
				Logger.LogSuccess("Vapid's ModLoader has been uninstalled.");
			}
		}
	}
}