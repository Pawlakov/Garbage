using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNET
{
	class Program
	{
		static void Main(string[] args)
		{
			//SearchInGoogle("MSDN Process");
			//SearchInGoogle("Maciek");
			//FindInDesktop("/NetASCII.txt");
			
			SearchInGoogle("IntelliSense nie krzycz po mnie");
			Console.ReadKey();
		}

		private static void SearchInGoogle(string path)
		{
			Process.Start("http://google.com/search?q=" + path);
		}

		private static void FindInDesktop(string path)
		{
			Process.Start("C:/Users/pmatu_000/Desktop" + path);
		}
	}
}
