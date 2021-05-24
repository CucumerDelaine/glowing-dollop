using System;
using System.IO;

	string[] lines = File.ReadAllLines("test.txt");
	Console.Write("Write your name: ");
    string name = Console.ReadLine();
	for(int i = 0; i < 5162; i++)
	{
		if (LevenshteinDistance(name, lines[i]) == 0)
		{
			Console.Write($"Hello, {name}!");
			Environment.Exit(0);
		}
	}
	for(int i = 0; i < 5162; i++)
	{
		// if (LevenshteinDistance(name, lines[i]) == 0)
		// {
		// 	Console.Write($"Hello, {name}");
		// 	break;
		// }
		if (LevenshteinDistance(name, lines[i]) < 3 && LevenshteinDistance(name, lines[i]) != 0)
		{
			string Y = "Y";
			string y = "y";
			Console.Write($"Did you mean, {lines[i]} ? Y/N  ");
			string otvet = Console.ReadLine();
			if (otvet[0] == Y[0] || otvet[0] == y[0])
			{
				Console.Write($"Hello, {lines[i]}");
				break ;
			}
		}
		if (i == 5161)
		Console.Write("\nYour name was not found.");
	}

static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
static int LevenshteinDistance(string firstWord, string secondWord)
{
    var n = firstWord.Length + 1;
    var m = secondWord.Length + 1;
    var matrixD = new int[n, m];

    const int deletionCost = 1;
    const int insertionCost = 1;

    for (var i = 0; i < n; i++)
    {
        matrixD[i, 0] = i;
    }

    for (var j = 0; j < m; j++)
    {
        matrixD[0, j] = j;
    }

    for (var i = 1; i < n; i++)
    {
        for (var j = 1; j < m; j++)
        {
            var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

            matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                    matrixD[i, j - 1] + insertionCost,         // вставка
                                    matrixD[i - 1, j - 1] + substitutionCost); // замена
        }
    }

    return matrixD[n - 1, m - 1];
}