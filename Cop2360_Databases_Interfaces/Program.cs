using System;
using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;
using DBInterfaceExampleLib;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Cop2360_Databases_Interfaces {
	class Program {
		static void Main(string[] args) {

			/* Arguments */
			String stringPathToInputFile = args[0];
			String stringDatabasePathOrDatabaseServer = args[1];


			/* Create a Factory that can in turn create an object conforming to our interface. */
			DBInterfaceExampleFactory factory = new DBInterfaceExampleFactory();
			IDBInterfaceExample dblib = factory.CreateDBInterfaceExample((int)DBInterfaceExampleFactory.DBINTERFACETYPE.sqlite);
			dblib.SetDBName(stringDatabasePathOrDatabaseServer);
			Console.WriteLine(dblib.GetDBName());


			/* Code Adapted from Previous Notes to Give Us Some Data To Work With */
			/* (We do not need all of this.) */
			Console.Write("\n\n\nPROGRAM STARTS HERE\n");
			Decimal decMaxContribution = 500.00m;
			Decimal decSmallContributionThreshold = 100m;
			/* Make sure the file exists before proceeding. */
			if(File.Exists(stringPathToInputFile)) {
				String stringEntireText = File.ReadAllText(stringPathToInputFile); /* Read the whole file into memory as a string (could go line by line. */
				String stringNewLine = "\r\n"; /* Force using Windows newline instead of Environment.NewLine */
				Char charDelimiter = '\t'; /* This file uses tabs, but we could change this variable if other files use other delimiters */
				String[] arrayStringLines = stringEntireText.Split(stringNewLine); /* Convert the big string into an array of rows */
				/* Create some more variables we will need. */
				int lineNumber = 0;
				List<String> listStringHeader = new List<String>();
				List<OrderedDictionary> listOrdicRows = new List<OrderedDictionary>();
				/* Go through every line/row and process appropriately. */
				foreach(String stringLine in arrayStringLines) {
					lineNumber++;
					/* If a line is blank, do not do anything with it. */
					if(String.IsNullOrWhiteSpace(stringLine)) {
						Console.WriteLine("Blank Line");
					}
					else {
						/* Create a list to holding header information if this is the first line (the header). */
						if(lineNumber == 1) {
							String[] arrayStringTempHeader = stringLine.Split(charDelimiter);
							foreach(String stringHeaderColumn in arrayStringTempHeader) {
								listStringHeader.Add(stringHeaderColumn.Replace(' ', '_').Replace('/', '_').Trim().ToLower());
								/* We can dice the header string a lot in just one line (above). */
							}
						}
						/* If this is not the first line, treat it like a data row. */
						else {
							String[] arrayStringTempROw = stringLine.Split(charDelimiter);
							OrderedDictionary ordicRow = new OrderedDictionary();
							for(int i = 0; i < listStringHeader.Count; i++) {
								ordicRow.Add(listStringHeader[i], arrayStringTempROw[i]);
								/* (Above) Add each column in the row to an ordered dictionary keyed to its header name. 
								 We can look up this column data in the future either by its order by its header name. */
							}
							listOrdicRows.Add(ordicRow);
						}
					}
				}
			}
			/* New code goes here again. */

		}
	}
}
