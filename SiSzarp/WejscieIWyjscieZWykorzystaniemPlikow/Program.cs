using System;
using System.IO;

static class Program
{
	static void Main(string[] args)
	{
		//Exercise1and2();
		Exercise3();
		Console.ReadKey();
	}
	static void Exercise1and2()
	{
		int i;
		StreamReader reader = null;
		FileInfo file = new FileInfo("KrótkaOpowieść.txt");
		try
		{
			reader = file.OpenText();
			Console.WriteLine(file.FullName);
			Console.WriteLine(file.CreationTime);
			for (i = 0; reader.ReadLine() != null; i++) ;
			Console.WriteLine("Plik zawiera {0} wierszy.", i);
		}
		catch (Exception exceptionObject)
		{
			Console.WriteLine(exceptionObject);
		}
		finally
		{
			if (reader != null)
				reader.Close();
		}
	}
	static void Exercise3()
	{
		Random random = new Random();
		byte[] rainfall = new byte[12];
		FileStream stream = null;
		FileInfo file = new FileInfo("Opady.dat");
		stream = file.Open(FileMode.OpenOrCreate);
		for(; stream.Length < 12;)
		{
			stream.WriteByte((byte)random.Next(255));
		}
		stream.Position = 0;
		for (int i = 0; i < 12; i++)
		{
			Console.WriteLine(rainfall[i] = (byte)stream.ReadByte());
		}
	}
}
