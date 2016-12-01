using System;
using System.Collections.Generic;

enum Order
{
	DOD = 1,
	ODE,
	ŁAD,
	POB,
	SOB,
	SOM,
	STP
}

class Tag
{
	private int line;
	private string name;

	int Line
	{
		get
		{
			return line;
		}
	}

	string Name
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
	Order order;
	string data;

	public string Tag
	{
		get
		{
			return tag;
		}
	}

	public int Order
	{
		get
		{
			return (int)order;
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
		// TODO
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
	}

	private static string[] Compile(List<string> input)
	{
		Line[] lineArray = convertLines(input);
		List<Tag> tagList = firstRun(lineArray);
		string[] result = secondRun(lineArray,tagList);
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
			int order = lineArray[whichLine].Order;
			// Żeby rozbiło również dalejsze.
		}

		return result;
	}

	private static void WriteFile(string fileName, string[] result)
	{

	}

	private static List<string> ReadFile(string fileName)
	{
		List<string> result = new List<string>();

		// Wczytanie danych nastąpi mniej więcej tu.

		return result;
	}
}

