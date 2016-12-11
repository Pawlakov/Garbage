using System;
using System.Collections.Generic;
using System.IO;

class Tag
{
	private int line;
	private string name;

	public int Line
	{
		get
		{
			return line;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	public Tag(int line, string name)
	{
		this.line = line;
		this.name = name;
	}
}

class Line
{
	string tag;
	string order;
	string data;

	public string Tag
	{
		get
		{
			return tag;
		}
	}

	public string Order
	{
		get
		{
			return order;
		}
	}

	public string Data
	{
		get
		{
			return data;
		}
	}

	public Line(string line)
	{
		if(line.LastIndexOf(" ") == line.IndexOf(" "))// Nie ma taga
		{
			int justAfterSpace = 0;
			int nextSpace;

			nextSpace = line.IndexOf(' ', justAfterSpace);
			this. order = line.Substring(0, nextSpace - justAfterSpace);
			justAfterSpace = nextSpace + 1;

			nextSpace = line.Length;
			this.data = line.Substring(justAfterSpace, nextSpace - justAfterSpace);
		}
		else// Jest tag
		{
			int justAfterSpace = 0;
			int nextSpace;

			nextSpace = line.IndexOf(' ', justAfterSpace);
			this.tag = line.Substring(0, line.IndexOf(' ') - 1);
			justAfterSpace = nextSpace + 1;

			nextSpace = line.IndexOf(' ', justAfterSpace);
			this.order = line.Substring(justAfterSpace, nextSpace - justAfterSpace);
			justAfterSpace = nextSpace + 1;

			nextSpace = line.Length;
			this.data = line.Substring(justAfterSpace, nextSpace - justAfterSpace);
		}
	}
}

class AssemblerW
{
	static void Main()
	{
		string inputName;
		string outputName;

		Console.Write("Proszę podać nazwę pliku-źródła (bez rozszerzenia): ");
		inputName = Console.ReadLine();
		Console.Write("Proszę podać nazwę pliku-celu (bez rozszerzenia, jeśli nie istnieje to zostanie utworzony): ");
		outputName = Console.ReadLine();

		WriteFile(outputName, Compile(ReadFile(inputName)));
		Console.ReadKey();
	}

	private static string[] Compile(List<string> input)
	{
		Line[] lineArray = convertLines(input);
		string[] result = secondRun(lineArray, firstRun(lineArray));
		return result;
	}

	private static Line[] convertLines(List<string> input)
	{
		Line[] result = new Line[input.Count];
		for(int whichLine = 0; whichLine < result.Length; whichLine++)
		{
			result[whichLine] = new Line(input[whichLine]);
		}
		return result;
	}

	private static List<Tag> firstRun(Line[] lineArray)
	{
		List<Tag> result = new List<Tag>();

		for (int whichLine = 0; whichLine < lineArray.Length; whichLine++)
		{
			if(lineArray[whichLine].Tag != null)
			{
				result.Add(new Tag(whichLine, lineArray[whichLine].Tag));
			}
		}

		return result;
	}

	private static string[] secondRun(Line[] lineArray, List<Tag> tagList)
	{
		string[] result = new string[lineArray.Length];

		for (int whichLine = 0; whichLine < lineArray.Length; whichLine++)
		{
			int data = 0;
			string order = lineArray[whichLine].Order;
			bool isDataATag = false;

			for (int whichTag = 0; whichTag < tagList.Count; whichTag++)
			{
				if (lineArray[whichLine].Data == tagList[whichTag].Name)
				{
					data = tagList[whichTag].Line;
					isDataATag = true;
					break;
				}
			}

			if (!isDataATag)
			{
				data = Convert.ToInt32(lineArray[whichLine].Data);
			}

			result[whichLine] = generateOrder(order, data);
		}

		return result;
	}

	private static string generateOrder(string order, int data)
	{
		int result = 0;
		if (data >= 0)
			result += data;
		else
			result += (2047 + data);
		if(order == "DOD")
		{
			result += 2048;
		}
		else if (order == "ODE")
		{
			result += 4096;
		}
		else if (order == "�AD")
		{
			result += 6144;
		}
		else if (order == "POB")
		{
			result += 8192;
		}
		else if (order == "SOB")
		{
			result += 10240;
		}
		else if (order == "SOM")
		{
			result += 12288;
		}
		else if (order == "STP")
		{
			result += 14336;
		}
		else if (order == "KON")
		{
			result += 0;
		}
		else if (order == "RPA")
		{
			result += 0;
		}
		else if (order == "RST")
		{
			result += 0;
		}
		else
		{
			Console.WriteLine("Napotkano niewłaściwy rozkaz: " + order);
		}
		return result.ToString();
	}

	private static void WriteFile(string fileName, string[] result)
	{
		StreamWriter writer = new StreamWriter(fileName + ".txt");
		for(int whichLine = 0; whichLine < result.Length; whichLine++)
		{
			writer.WriteLine(result[whichLine]);
		}
		writer.Close();
	}

	private static List<string> ReadFile(string fileName)
	{
		List<string> result = new List<string>();

		StreamReader reader = new StreamReader(fileName + ".txt");
		string line = reader.ReadLine();

		while (line != null)
		{
			result.Add(line);
			line = reader.ReadLine();
		}
		reader.Close();

		for (int whichLine = 0; whichLine < result.Count; whichLine++)
		{
			//1. Usuwa tabulacje.
			result[whichLine] = result[whichLine].Replace("	", " ");
			//2. Usuwa podwójne spacje.
			result[whichLine] = result[whichLine].Replace("  ", " ");
			result[whichLine] = result[whichLine].Replace("KON", "KON 0");
			result[whichLine] = result[whichLine].Replace("STP", "STP 0");
			result[whichLine] = result[whichLine].Replace("RPA", "RPA 0");
		}

		return result;
	}
}