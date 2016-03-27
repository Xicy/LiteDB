using System;
using System.Collections.Generic;
using System.IO;

namespace LiteDB.Shell
{
    public class Display
    {
        public Display()
        {
            TextWriters = new List<TextWriter>();
            Pretty = false;
        }

        public List<TextWriter> TextWriters { get; set; }
        public bool Pretty { get; set; }

        public void WriteWelcome()
        {
            WriteInfo("Welcome to LiteDB Shell");
            WriteInfo("");
            WriteInfo("Getting started with `help`");
            WriteInfo("");
        }

        public void WritePrompt(string text)
        {
            Write(ConsoleColor.White, text);
        }

        public void WriteInfo(string text)
        {
            WriteLine(ConsoleColor.Gray, text);
        }

        public void WriteError(string err)
        {
            WriteLine(ConsoleColor.Red, err);
        }

        public void WriteHelp(string line1 = null, string line2 = null)
        {
            if (string.IsNullOrEmpty(line1))
            {
                WriteLine("");
            }
            else
            {
                WriteLine(ConsoleColor.Cyan, line1);

                if (!string.IsNullOrEmpty(line2))
                {
                    WriteLine(ConsoleColor.DarkCyan, "    " + line2);
                    WriteLine("");
                }
            }
        }

        #region Print public methods

        public void Write(string text)
        {
            Write(Console.ForegroundColor, text);
        }

        public void WriteLine(string text)
        {
            WriteLine(Console.ForegroundColor, text);
        }

        public void WriteLine(ConsoleColor color, string text)
        {
            Write(color, text + Environment.NewLine);
        }

        public void Write(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;

            foreach (var writer in TextWriters)
            {
                writer.Write(text);
            }
        }

        #endregion Private methods
    }
}