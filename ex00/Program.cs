using System;

class MyProgram
{
	static double plateg(double stavka, int term, double sum)
	{
		double anoy = (sum * stavka * Math.Pow(1 + stavka, term)) / (Math.Pow(1 + stavka, term) - 1);
		anoy = Math.Round(anoy, 2);
		return (anoy);
	}

	static bool chek(string[] args)
	{
		if (args.Length != 5)
			return (false);
		if (double.TryParse(args[0], out _) == false || double.TryParse(args[1], out _) == false || int.TryParse(args[2], out _) == false || int.TryParse(args[3], out _) == false || double.TryParse(args[4], out _) == false)
			return (false);
		return (true);
	}
	static void Main(string[] args)
	{
		if (chek(args) == false)
		{
			Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
			return ;
		}
		double sum = double.Parse(args[0]);
		double rate = double.Parse(args[1]);
		int term = int.Parse(args[2]);
		int selectedMonth = int.Parse(args[3]);
		double payment = double.Parse(args[4]);
		double anoy;
		double stavka = rate / 12 / 100;
		anoy = plateg(stavka, term, sum);
		double var1 = variant1(sum, stavka, term, anoy, payment, selectedMonth, rate);
		double var2 = variant2(sum, stavka, term, anoy, payment, selectedMonth, rate);
		var1 = var1 - sum;
		var2 = var2 - sum;
		var1 = Math.Round(var1, 2);
		var2 = Math.Round(var2, 2);
		if (sum < 0 || rate < 0 || term < 0 || selectedMonth < 0 || payment < 0 || var1 < 0 || var2 < 0)
		{
			Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
			return ;
		}
		Console.WriteLine($"\n\nПереплата при уменьшении платежа: {var1}");
		Console.WriteLine($"Переплата при уменьшении срока: {var2}");
		if (var1 < var2)
			Console.WriteLine($"Уменьшение платежа выгоднее уменьшения срока на {Math.Round(var2 - var1, 2)}");
		else if (var1 > var2)
			Console.WriteLine($"Уменьшение срока выгоднее уменьшения платежа на {Math.Round(var1 - var2, 2)}");
		else if (var1 == var2)
			Console.WriteLine($"Переплата одинакова в обоих вариантах.");
	}
	static double variant1(double sum, double stavka, int term, double anoy, double payment, int selectedMonth, double rate)
	{
		double ost = 0;
		double sum2 = 1000000;
		int i;
		int months = 5;
		Console.WriteLine("Дата\t\t\tПлатеж\t\t\tОД\t\t\tПроценты\t\tОстаток долга");
		Console.WriteLine($"\t\t\t{anoy, -5}\t\t{sum}\t\t\t{anoy * term}\t\t{sum}");
		for(i = 1; i < term + 1; i++)
		{
			if (months == 13)
				months = 1;
			double procent = (sum2 * rate * System.DateTime.DaysInMonth(2021, months)) / (100 * 365);
			ost += anoy;
			sum2 = sum2 - anoy;
			sum2 = sum2 + procent;
			if (i == selectedMonth)
			{
				sum2 = sum2 - payment;
				anoy = plateg(stavka, term - selectedMonth, sum2);
				ost = ost + payment;
			}
			DateTime date1 = new DateTime(2021, months, 1, 18, 30, 25);
			Console.WriteLine($"{date1.ToShortDateString()}\t\t{anoy}\t\t{Math.Round(anoy - procent, 2)}\t\t{Math.Round(procent, 2)}\t\t\t{Math.Round(sum2, 2)}");
			months = months + 1;
		}
		ost += sum2;
		ost = Math.Round(ost, 2);
		return (ost);
	}
	static double variant2(double sum, double stavka, int term, double anoy, double payment, int selectedMonth, double rate)
	{
		double ost = 0;
		double sum2 = 1000000;
		int i;
		int months = 5;
		double a;
		Console.WriteLine("\n\nДата\t\t\tПлатеж\t\t\tОД\t\t\tПроценты\t\tОстаток долга");
		Console.WriteLine($"\t\t\t{anoy, 5}\t\t{sum}\t\t\t{anoy * term}\t\t{sum}");
		for(i = 1; i < term + 1; i++)
		{
			if (months == 13)
				months = 1;
			double procent = (sum2 * rate * System.DateTime.DaysInMonth(2021, months)) / (100 * 365);
			ost += anoy;
			sum2 = sum2 - anoy;
			sum2 = sum2 + procent;
			if (i == selectedMonth)
			{
				sum2 = sum2 - payment;
				ost = ost + payment;
				a = Math.Log((anoy / (anoy - stavka * sum2)), 1+stavka);
				a = Math.Round(a, 0);
				term = (int)a + i;
			}
			DateTime date1 = new DateTime(2021, months, 1, 18, 30, 25);
			Console.WriteLine($"{date1.ToShortDateString()}\t\t{anoy}\t\t{Math.Round(anoy - procent, 2)}\t\t{Math.Round(procent, 2)}\t\t\t{Math.Round(sum2, 2)}");
			months = months + 1;
		}
		ost += sum2;
		ost = Math.Round(ost, 2);
		return (ost);
	}
}
