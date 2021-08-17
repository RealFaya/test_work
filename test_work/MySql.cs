using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_work
{
	class MySql
	{
		private string connect;

		public MySql(string connect)
		{
			this.connect = connect;
		}

		public int ReadUsers()
		{
			List<User> users = new List<User>();

			using(MySqlConnection connection = new MySqlConnection(connect))
			{
				connection.Open();
				MySqlDataAdapter dataReader = new MySqlDataAdapter("SELECT * FROM users", connection);
				DataTable table = new DataTable();
				dataReader.Fill(table);

				foreach(DataRow row in table.Rows)
				{
					User user = new User(row);
					users.Add(user);
				}
			}

			User.SetUsers(users);

			return users.Count();
		}
	}
}