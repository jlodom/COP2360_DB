using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace DBInterfaceExampleLib {


	/* Interfaces by convention start with "I" followed by the name of the class they go to. */
	public interface IDBInterfaceExample {

		Boolean CreateATable(String stringTableName, List<String> ColumnNames);
		Boolean AddRowsToTable(List<OrderedDictionary> loRows);
		List<Dictionary<String,String>> GetRowsFromTable(String stringTableName);
		String GetDBName();
		Boolean SetDBName(String stringNewDBName);

	}



	public abstract class DBInterfaceExample : IDBInterfaceExample {

		/* Variables belong to class */
		String dbname = "NODB";


		/* Methods required by the interface */
		public abstract Boolean CreateATable(String stringTableName, List<String> ColumnNames);
		public abstract Boolean AddRowsToTable(List<OrderedDictionary> loRows);
		public abstract List<Dictionary<String, String>> GetRowsFromTable(String stringTableName);
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

		public override Boolean CreateATable(String stringTableName, List<String> ColumnNames) {

			return true;
		}

		public override Boolean AddRowsToTable(List<OrderedDictionary> loRows) {
			return true;
		}

		public override List<Dictionary<String, String>> GetRowsFromTable(String stringTableName) {

			throw new NotImplementedException();
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
