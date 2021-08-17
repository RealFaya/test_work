using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace test_work
{
	class Program
	{
		private static Dictionary<int, Func<bool>> func = new Dictionary<int, Func<bool>>
		{
			[1] = new Func<bool>(() =>
			{
				TXT txt = new TXT();
				int count = txt.ReadUsers();

				return count > 0;
			}),
			[2] = new Func<bool>(() =>
			{
				List<string> connections = new List<string>();

				foreach(ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
				{
					connections.Add(connectionString.Name);
				}

				Console.WriteLine(string.Format("Выберете куда поключаться: \n{0}", string.Join("\n", connections)));

				while(true)
				{
					string name = Console.ReadLine();

					if(ConfigurationManager.ConnectionStrings[name] != null)
					{
						MySql mySql = new MySql(ConfigurationManager.ConnectionStrings[name].ConnectionString);
						int count = mySql.ReadUsers();

						return count > 0;
					}
					else
					{
						Console.WriteLine("Не корректный ввод! Попробуйте еще раз");
					}
				}

				return false;
			})
		};

		static void Main(string[] args)
		{
			while(true)
			{
				Console.WriteLine("\nОткуда брать информацию? [1 - Файл | 2 - База]");

				if(int.TryParse(Console.ReadLine(), out int num))
				{
					if(func.ContainsKey(num))
					{
						if(func[num]())
						{
							Console.WriteLine(string.Format("Всего сотрудников: {0}", User.Users.Count));
							Console.WriteLine(string.Format("Всего сотрудников с фамилией \"Иванов\": {0}", User.Users.Where(x => x.fio.Contains("Иванов")).Count()));
							Console.WriteLine(string.Format("Сумма отработанных часов: {0}", User.Users.Sum(x => x.workHours)));
							Console.WriteLine(string.Format("Самый молодой сотрудник: {0}", User.Users.FirstOrDefault(x => x.age == User.Users.Min(x => x.age)).ToString()));

							Console.WriteLine("Повторить? [Y - да | N - нет]");
							if(Console.ReadKey().Key == ConsoleKey.N)
							{
								Console.WriteLine("Нажмите любую клавишу для закрытия консоли ...");
								return;
							}
						}
						else
						{
							Console.WriteLine(string.Format("В {0} нет сотрудников", num == 1 ? "файле" : "базе"));
						}
					}
					else
					{
						Console.WriteLine("Неверная цифра! Попробуйте еще раз");
					}
				}
				else
				{
					Console.WriteLine("Не корректный ввод! Попробуйте еще раз");
				}
			}
		}
	}
}