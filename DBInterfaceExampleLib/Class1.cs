using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Data.Sqlite;

namespace DBInterfaceExampleLib {


	/* Interfaces by convention start with "I" followed by the name of the class they go to. */
	public interface IDBInterfaceExample {

		Boolean CreateATable(String stringTableName, List<String> ColumnNames);
		Boolean AddRowsToTable(List<OrderedDictionary> loRows);
		List<Dictionary<String,String>> GetRowsFromTable(String stringTableName);
		String GetDBName();
		Boolean SetDBName(String stringNewDBName);
		Boolean ConnectToDB();
		Boolean DisconnectFromDB();

	}



	public abstract class DBInterfaceExample : IDBInterfaceExample {

		/* Variables belong to class */
		String dbname = "NODB";


		/* Methods required by the interface */
		public abstract Boolean CreateATable(String stringTableName, List<String> ColumnNames);
		public abstract Boolean AddRowsToTable(List<OrderedDictionary> loRows);
		public abstract List<Dictionary<String, String>> GetRowsFromTable(String stringTableName);
		public abstract Boolean ConnectToDB();
		public abstract Boolean DisconnectFromDB();
		public String GetDBName() {
			return this.dbname;

		}
		public Boolean SetDBName(String stringNewDBName) {
			Boolean boolReturn = true;
			this.dbname = stringNewDBName;
			return boolReturn;
		}

	}



	public class SqliteInterfaceExample : DBInterfaceExample, IDBInterfaceExample {

		String stringDBFilePath = String.Empty;
		SqliteConnection dbconnection;

		/* This is NOT recommended, Just here to speed things along in class. */
		public Boolean SetConnectionVariables(String stringTempDBFilePath) {
			stringDBFilePath = stringTempDBFilePath;
			return true;
		}

		public override Boolean ConnectToDB() {
			Boolean boolConnectionSucceeded = false;
			try {
				dbconnection = new SqliteConnection("Data Source=" + stringDBFilePath + ";");
				boolConnectionSucceeded = true;
			}
			catch {
				boolConnectionSucceeded = false;
			}
			return boolConnectionSucceeded;
		}

		public override bool DisconnectFromDB() {
			Boolean boolDisconnectSucceeded = true;
			try {
				dbconnection.Close();
			}
			catch {
				boolDisconnectSucceeded = false;
			}
			return boolDisconnectSucceeded;
		}

		public override Boolean CreateATable(String stringTableName, List<String> ColumnNames) {

			/* Create the column create SQL statement while checking that the table and column names are valid. */
			Boolean boolProceed = false;
			boolProceed = IsAlphaNumeric(stringTableName);
			if(boolProceed) {
				foreach(String stringColumnName in ColumnNames) {
					if(!(IsAlphaNumeric(stringColumnName))) {
						boolProceed = false;
					}
				}
			}
			if(boolProceed) {
				StringBuilder sbCreateTable = new StringBuilder();
				sbCreateTable.Append("CREATE TABLE tablename (");
				Boolean boolIsNotFirst = false;
				foreach(String stringColumnName in ColumnNames) {
					if(boolIsNotFirst) {
						sbCreateTable.Append(",");
					}
					else {
						boolIsNotFirst = true;
					}
					sbCreateTable.Append(" " + stringColumnName + " varchar");
				}
				sbCreateTable.Append(");");
				String stringColumnCreate = sbCreateTable.ToString();
				Console.WriteLine(stringColumnCreate); // DELETE ME

				/* Create the table. */
				try {
					this.ConnectToDB();
					SqliteCommand scmdCreateTable = new SqliteCommand(stringColumnCreate, dbconnection);
					/* This command will be sent as text, and it will not return any data. */
					scmdCreateTable.CommandType = CommandType.Text;
					int intCommandSuccessCode = scmdCreateTable.ExecuteNonQuery();
					scmdCreateTable.Dispose();
					this.DisconnectFromDB();
				}
				catch {
					boolProceed = false;
				}
				/* Standard SQL Create Table Command
				 * CREATE TABLE tablename (column1 datatype, column2 datatype, column3 datatype);
				 * etc. etc. */
			}
			else {
				boolProceed = false;
			}

			return boolProceed;
		}

		public override Boolean AddRowsToTable(List<OrderedDictionary> loRows) {
			return true;
		}

		public override List<Dictionary<String, String>> GetRowsFromTable(String stringTableName) {

			throw new NotImplementedException();
		}



		/* https://stackoverflow.com/questions/1046740/how-can-i-validate-a-string-to-only-allow-alphanumeric-characters-in-it */
		static Boolean IsAlphaNumeric(String stringPossibleAlphaNumeric) {
			Boolean boolValid = false;
			/* Probably unnecessary, but paranoia has set in. */
			if(String.IsNullOrWhiteSpace(stringPossibleAlphaNumeric)) {
				boolValid = false;
			}
			else {
				if(stringPossibleAlphaNumeric.All(char.IsLetterOrDigit)) {
					boolValid = true;
				}
			}
			return boolValid;
		}

	}


	public class DBInterfaceExampleFactory {

		public enum DBINTERFACETYPE { mssql = 1, postgres, mysql, sqlite };

		public DBInterfaceExampleFactory() {

		}

		public IDBInterfaceExample CreateDBInterfaceExample(int intTypeOfDB) {

			IDBInterfaceExample IDBIEReturn = null;

			switch(intTypeOfDB) {
				case (int)DBINTERFACETYPE.sqlite:
					IDBIEReturn = new SqliteInterfaceExample();
					break;
			}

			return IDBIEReturn;

		}

	}


}
