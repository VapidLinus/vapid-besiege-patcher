using System.Drawing;
using System.Windows.Forms;

namespace Vapid.Patcher
{
	public class Logger
	{
		private readonly RichTextBox textBox;

		public Logger(RichTextBox textBox)
		{
			this.textBox = textBox;
		}

		public void Clear()
		{
			textBox.Clear();
		}

		public void Log(string text, bool newLine = true)
		{
			Log(text, Color.Black, newLine);
		}

		public void LogSuccess(string text, bool newLine = true)
		{
			Log(text, Color.Green, newLine);
		}

		public void LogWarning(string text, bool newLine = true)
		{
			Log(text, Color.Orange, newLine);
		}

		public void LogError(string text, bool newLine = true)
		{
			Log(text, Color.Red, newLine);
		}

		public void Log(string text, Color color, bool newLine = true)
		{
			textBox.SelectionStart = textBox.TextLength;
			textBox.SelectionLength = 0;

			textBox.SelectionColor = color;
			textBox.AppendText(text + (newLine ? "\n" : ""));
			textBox.SelectionColor = textBox.ForeColor;
		}
	}
}