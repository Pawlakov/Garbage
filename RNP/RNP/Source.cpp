#include <fstream>
#include <string>
#include <vector>
#include <iostream>

using namespace std;

class Converter
{
	vector <string> input;
	vector <string> output;

public:
	Converter()
	{
		//is it even necessary?
	}

	void load()
	{
		fstream file("dane.txt");
		string temp;

		while (!file.eof())
		{
			getline(file, temp);
			input.push_back(temp);
		}

		file.close();
	}

	void convert()
	{
		vector <char> stack;
		string lineIn;
		string lineOut;
		char character;

		for (unsigned i = 0; i < input.size(); i++)
		{
			lineIn = input[i];
			lineOut = "";
			for (unsigned j = 0; j < lineIn.length(); j++)
			{
				character = lineIn[j];

				switch (character)
				{
				case '(':
					stack.push_back(character);
					break;
				case ')':
					while (true)
					{
						if (stack[stack.size() - 1] == '(')
						{
							stack.pop_back();
							break;
						}
						else
						{
							lineOut.push_back(stack[stack.size() - 1]);
							stack.pop_back();
						}
					}
					break;
				case '+':
					if (stack.size() > 0)
					{
						while (stack[stack.size() - 1] == '/' || stack[stack.size() - 1] == '^' || stack[stack.size() - 1] == '*' || stack[stack.size() - 1] == '-' || stack[stack.size() - 1] == '+')
						{
							lineOut = lineOut + stack[stack.size() - 1];
							stack.pop_back();
							if (stack.size() == 0)
								break;
						}
					}
					stack.push_back(character);
					break;
				case '-':
					if (stack.size() > 0)
					{
						while (stack[stack.size() - 1] == '/' || stack[stack.size() - 1] == '^' || stack[stack.size() - 1] == '*' || stack[stack.size() - 1] == '-' || stack[stack.size() - 1] == '+')
						{
							lineOut = lineOut + stack[stack.size() - 1];
							stack.pop_back();
							if (stack.size() == 0)
								break;
						}
					}
					stack.push_back(character);
					break;
				case '*':
					if (stack.size() > 0)
					{
						while (stack[stack.size() - 1] == '/' || stack[stack.size() - 1] == '^' || stack[stack.size() - 1] == '*')
						{
							lineOut = lineOut + stack[stack.size() - 1];
							stack.pop_back();
							if (stack.size() == 0)
								break;
						}
					}
					stack.push_back(character);
					break;
				case '/':
					if (stack.size() > 0)
					{
						while (stack[stack.size() - 1] == '/' || stack[stack.size() - 1] == '^' || stack[stack.size() - 1] == '*')
						{
							lineOut = lineOut + stack[stack.size() - 1];
							stack.pop_back();
							if (stack.size() == 0)
								break;
						}
					}
					stack.push_back(character);
					break;
				case '^':
					if (stack.size() > 0)
					{
						while (stack[stack.size() - 1] == '^')
						{
							lineOut = lineOut + stack[stack.size() - 1];
							stack.pop_back();
							if (stack.size() == 0)
								break;
						}
					}
					stack.push_back(character);
					break;
				default:
					lineOut = lineOut + character;
				}
			}
			while (stack.size() > 0)
			{
				lineOut = lineOut + stack[stack.size() - 1];
				stack.pop_back();
			}
			output.push_back(lineOut);
		}
	}

	void show()
	{
		cout << "Wyjsciowe wyrazenia:" << endl;
		for (unsigned i = 0; i < output.size(); i++)
		{
			cout << i << ": " << output[i] << endl;
		}
	}
};

int main()
{
	Converter converter;
	converter.load();
	converter.convert();
	converter.show();

	system("Pause");
	return 0;
}