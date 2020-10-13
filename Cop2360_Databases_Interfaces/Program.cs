using System;
using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;
using DBInterfaceExampleLib;

namespace Cop2360_Databases_Interfaces {
	class Program {
		static void Main(string[] args) {


			/* Create a Factory that can in turn create an object conforming to our interface. */
			DBInterfaceExampleFactory factory = new DBInterfaceExampleFactory();
			IDBInterfaceExample dblib = factory.CreateDBInterfaceExample((int)DBInterfaceExampleFactory.DBINTERFACETYPE.sqlite);
			dblib.SetDBName("example.sqlite");
			Console.WriteLine(dblib.GetDBName());


			/* Create a SQLite connection. Specify a file and create if it does not exist.
			 * Use a connection string to connect to the database.
			 * For most SQL servers you will need server name, database name, username, password
			 * and maybe some additional parameters about the connection.
			 * For SQLite you just need a path to a SQLite file (which uses .sqlite or .db extensions )
			 */
			//SqliteConnection dbconnection = new SqliteConnection("Data Source=example.sqlite;");
			//dbconnection.Open(); /* Write the close connection when you write the open. */
			/* Using a StringBuilder is recommended when building SQL strings to make the process a bit safer. */
			//StringBuilder sbCreateTable = new StringBuilder();
			//String stringTableName = "CANDY";
			/* Usually building a SQL string will be more complex depending on variables. */
			//sbCreateTable.Append("CREATE TABLE " + stringTableName + "('name' varchar, 'color' varchar, 'nuts' varchar)");
			//Console.WriteLine(sbCreateTable); /* We are just showing what we are going to send. Debug. */
			/* Now create a SQL command that we will execute. Need both the command string and the connection. */
			//SqliteCommand scmdCreateTable = new SqliteCommand(sbCreateTable.ToString(), dbconnection);
			/* This command will be sent as text, and it will not return any data. */
			//.CommandType = CommandType.Text;
			//int intCommandSuccessCode = scmdCreateTable.ExecuteNonQuery();
			//scmdCreateTable.Dispose();

			//dbconnection.Close(); /* Write this when you write the open. */

			/* Create a database on disk (SQLIte does this, other DBs not so much. */



		}
	}
}
