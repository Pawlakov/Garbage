using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSZajecia
{
	class Kolekcje
	{
		//static void Main(string[] args)
		//{
		//	//pierwsza lista w C#. Miesci w sumie tyle elementow ile pamieci w kompie jest dostepnej

		//	ArrayList mojaLista = new ArrayList();
		//	mojaLista.Add(21);
		//	mojaLista.Add("Doge");

		//	//trzeba narzucic typ konwersji recznie, bo dane, co widac powyzej, sa sciagane do jednego uniwersalnego typi\u na czas przechowania
		//	int value = (int)mojaLista[0];
		//	//on taki granat ponizej lyknie, a potem wybuchnie masa wyjatkow. Trzeba uwazac
		//	//int value2 = (int)mojaLista[1];

		//	List<int> myList = new List<int>();

		//	myList.Add(9000);

		//	List<int> qList = new List<int>()
		//	{
		//		1,2,3,4,5,6,7,7,8,8,9
		//	};

		//	qList.Count();
		//	qList.Add('h');
		//	qList[4] = 9000;

		//	qList.Where(x => x == 4);
		//	//ponizsze sie wywali jezeli bedzie wiecej niz 1 elementow
		//	qList.Single(x => x == 4);
		//	//uzyc w przypadku niepewnosci (jezeli obiektu moze nie byc lub gdy moga byc 2, w drugim wypadku sprwdzic)
		//	qList.SingleOrDefault(x => x == 4);
		//	qList.Select(x => x == 1);
		//	qList.Any();

		//	//under the sun, no shadows will fall! Piercing our eyes as we code! .Net battalion on course to List, closing the end of it's march!
		//	//This time we're here, to finish our job, started one weekend ago! Driving the decimals out of memory cells to bury them deep in the lists!
		//	foreach (var x in qList)
		//	{
		//		Console.WriteLine(x);
		//	}

		//	Dictionary<string, int> mDictionary = new Dictionary<string, int>();

		//	mDictionary.Add("Heavy", 1);
		//	mDictionary.Add("Scout", 2);
		//	mDictionary.Add("Soldeior", 3);

		//	IList<Person> iList = new List<Person>();
		//	iList.Add(new Person());

		//	Console.WriteLine(Kolekcje.HisMethod().Select(x => x));

		//	Console.ReadKey();

		//	//czemu internet jest wylaczony? IE chcial sie stac domyslna przegladarka....
		//}
		static public IEnumerable<int> HisMethod()
		{
			for (int i = 0; i < 50; ++i)
			{
				yield return i;
			}
		}
	}

	class Person
	{
		public Person()
		{
			Console.WriteLine("A wild .NET member appears!");
		}
	}
}
