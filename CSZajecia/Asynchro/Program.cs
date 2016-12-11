using System;
using System.Threading;
using System.Threading.Tasks;

class Chyba
{
	static void Main()
	{
		//Example1();

		//Example2();

		//Example3();

		//Task.Factory.StartNew(Example4);
		//Task.Factory.StartNew(Example5);
	}

	public static void Example1()
	{
		Thread.CurrentThread.Name = "Main";

		Task taskA = Task.Run(() => Console.WriteLine("TaskA"));
		Task taskB = Task.Run(() => Console.WriteLine("TaskB"));

		taskA.Start();
		taskB.Start();

		taskA.Wait();
		taskB.Wait();
	}

	public static void Example2()
	{
		Thread.CurrentThread.Name = "Main";

		Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA"));
		Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB"));
	}

	//public static void Example3()
	//{
	//	//What the f
	//}

	//public static void Example4()
	//{
	//	return MethodAsyncInternalMinus(count);
	//}

	//public static void Example5()
	//{
	//	return MethodAsyncInternal(count);
	//}

	//private static async Task<int> MethodAsyncInternal(int count)
	//{
	//	int counter;

	//	//

	//	return counter;
	//}
}
