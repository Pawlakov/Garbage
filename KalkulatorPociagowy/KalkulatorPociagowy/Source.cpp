#include <iostream>
#include <fstream>
#include <vector>
#include <string>

using namespace std;

class Calculator
{
public:
	static double simplifiedACalc(double v, double t, double s)
	{
		return (v * v) / (v * t - s);
	}

	static void calculateSimplifiedAs()
	{
		vector <double> distances;
		vector <double> times;
		vector <double> results;

		loadVector(distances, "distances.txt");
		loadVector(times, "times.txt");
		
		for (unsigned i = 1; i < times.size(); i++)
		{
			results.push_back(simplifiedACalc(100.0, (times[i]), (distances[i] - distances[i - 1])));
		}

		system("Pause");
	}

	static void loadVector(vector <double> &container, string fileName)
	{
		fstream file(fileName);
		string temp;

		while (!file.eof())
		{
			getline(file, temp);
			container.push_back(stod(temp));
		}

		file.close();
	}
};

int main()
{
	Calculator::calculateSimplifiedAs();

	system("Pause");
	return 0;
}