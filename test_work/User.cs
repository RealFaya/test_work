using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_work
{
	class User
	{
		public string fio;
		public int age;
		public string post;
		public int workHours;

		private static List<User> _users;
		public static List<User> Users
		{
			get => _users;
		}

		public User(string[] userInfo)
		{
			int length = userInfo.Length;
			
			fio = userInfo[0];
			age = 1 < length ? intTryParse(userInfo[1], 18) : 18;
			post = 2 < length ? userInfo[2] : "не определено";
			workHours = 3 < length ? intTryParse(userInfo[3], 0) : 0;
		}

		public User(DataRow userInfo)
		{
			fio = (string)userInfo["fio"];
			age = (int)userInfo["age"];
			post = (string)userInfo["post"];
			workHours = (int)userInfo["work_hours"];
		}

		private int intTryParse(string str, int def)
		{
			if(int.TryParse(str, out int num))
			{
				return num;
			}

			return def;
		}

		public static void SetUsers(List<User> users)
		{
			_users = new List<User>(users);
		}

		public static bool TryParse(string line, out User user)
		{
			user = null;
			string[] userInfo = line.Split("|");

			if(userInfo.Length > 0 && userInfo.Length < 5)
			{
				userInfo = userInfo.Select(x => x.Trim()).ToArray();
				user = new User(userInfo);

				return true;
			}

			return false;
		}

		public override string ToString()
		{
			return string.Format("{0} должность {1}", fio, post);
		}
	}
}