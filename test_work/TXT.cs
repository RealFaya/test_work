using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_work
{
	class TXT
	{
		private string txt;

		public TXT()
		{
			txt = string.Format("{0}\\users.txt", Directory.GetCurrentDirectory());
		}

		public int ReadUsers()
		{
			List<User> users = new List<User>();

			using(StreamReader streamReader = new StreamReader(txt))
			{
				int i = 0;
				string line;

				while((line = streamReader.ReadLine()) != null)
				{
					if(i++ == 0)
					{
						continue;
					}

					if(User.TryParse(line, out User user))
					{
						users.Add(user);
					}
				}
			}

			User.SetUsers(users);

			return users.Count;
		}
	}
}
