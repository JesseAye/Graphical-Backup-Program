using System;
using System.Diagnostics;
using System.IO;

namespace Graphical_Backup_Program
{
	static class GBP_Log
	{
		/// <summary>
		/// Provides reference/context to which directory this program is running from. If debugger is attached, set directory to base project directory. Else, set it to regular filepath of exe<para/>
		/// <see href="https://stackoverflow.com/questions/1343053/detecting-if-a-program-was-run-by-visual-studio-as-opposed-to-run-from-windows">Check if debugger is attached</see><para/>
		/// <see href="https://stackoverflow.com/a/11882118">Setting the directory depending on context (debugging or standalone exe)</see>
		/// </summary>
		private static string ProjectDirectory
		{
			get
			{
				return Debugger.IsAttached ? Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName : Environment.CurrentDirectory;
			}
		}

		/// <summary>
		/// Reference to the log file
		/// </summary>
		public static string FilePath
		{
			get
			{
				return ProjectDirectory + "/GBP.log";
			}
		}

		/// <summary>
		/// Divider line for use in the log file
		/// </summary>
		private const string dividerLine = "---------------------------------------------------------------------------------------------------\n";

		/// <summary>
		/// Append new text to the log
		/// </summary>
		/// <param name="text">The text to be appended</param>
		public static void Append(string text) => File.AppendAllText(FilePath, text);

		/// <summary>
		/// Append a divider line to the log
		/// </summary>
		public static void AppendDivider() => File.AppendAllText(FilePath, dividerLine);
	}
}
