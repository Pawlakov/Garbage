using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASynCAWAit
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		//napisz cos
		private void button1_Click(object sender, EventArgs e)
		{
			WriteLog("Maciek");
		}
		private void WriteLog(string message)
		{
			listBox1.Items.Insert(0, String.Format("{0} {1} {2}", DateTime.Now.TimeOfDay.ToString(), Thread.CurrentThread.ManagedThreadId, message));
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private async void button3_Click(object sender, EventArgs e)
		{
			button3.Enabled = false;
			WriteLog("Startuje Maciek 1");
			for (int i = 0; i < 20; i++)
			{
				WriteLog(i.ToString());
				//jezeli chcemy, zeby z async nie zatrzymywalo aplikacji, to dla danego nowego watku trzeba podac await
				await Task.Run(() => Thread.Sleep(1000));//bez Task.Run(() => ) program zzostanie zablokowany na jakis czas i przestanie odpowiadac
			}
			WriteLog("Koniec Maćka 1");
			button3.Enabled = true;
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			button3.Enabled = false;
			WriteLog("Startuje Maciek 2");
			for (int i = 30; i > 0; i--)
			{
				WriteLog(i.ToString());
				//jezeli chcemy, zeby z async nie zatrzymywalo aplikacji, to dla danego nowego watku trzeba podac await
				await Task.Run(() => Thread.Sleep(1000));//bez Task.Run(() => ) program zzostanie zablokowany na jakis czas i przestanie odpowiadac
			}
			WriteLog("Koniec Maćka 2");
			button3.Enabled = true;
		}
	}
}
