using System;
using System.IO;
using System.Numerics;

class Program
{
	static void Main()
	{
		//const byte p = 29;
		//const byte q = 11;
		const short n = 143;
		const short e = 103;
		const short d = 7;

		FileStream stream1 = new FileStream("Bitmap1.bmp", FileMode.OpenOrCreate);
		FileStream stream2 = new FileStream("Bitmap2.bmp", FileMode.OpenOrCreate);
		for(int i = 0; i < stream1.Length; i++)
		{
			long next = stream1.ReadByte();
			next = (long)(Power(next, d) % n);
			stream2.WriteByte((byte)next);
		}
		stream1.Close();
		stream2.Close();

		FileStream stream3 = new FileStream("Bitmap2.bmp", FileMode.OpenOrCreate);
		FileStream stream4 = new FileStream("Bitmap3.bmp", FileMode.OpenOrCreate);
		for (int i = 0; i < stream3.Length; i++)
		{
			long next = stream3.ReadByte();
			next = (long)(Power(next, e) % n);
			stream4.WriteByte((byte)next);
		}
		stream3.Close();
		stream4.Close();

		Console.WriteLine("Done with this.");
		Console.ReadKey();
	}

	//static byte Cypher(byte given)
	//{

	//}

	static BigInteger Power(long a, long b)
	{
		BigInteger result = 1;
		for (int i = 0; i < (long)b; i++)
		{
			result = result * a;
		}
		return result;
	}
}

