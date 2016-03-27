using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LiteDB.Shell
{
    public class InputCommand
    {
        public InputCommand()
        {
            Queue = new Queue<string>();
            History = new List<string>();
            Timer = new Stopwatch();
            Running = true;
            AutoExit = false; // run "exit" command when there is not more command in queue
        }

        public Queue<string> Queue { get; set; }
        public List<string> History { get; set; }
        public Stopwatch Timer { get; set; }
        public bool Running { get; set; }
        public bool AutoExit { get; set; }

        public Action<string> OnWrite { get; set; }

        public string ReadCommand()
        {
            if (Timer.IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Write(Timer.ElapsedMilliseconds.ToString("0000") + " ");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Write("> ");

            var cmd = ReadLine();

            // suport for multiline command
            if (cmd.StartsWith("/"))
            {
                cmd = cmd.Substring(1);

                while (!cmd.EndsWith("/"))
                {
                    if (Timer.IsRunning)
                    {
                        Write("     ");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Write("| ");

                    var line = ReadLine();
                    cmd += Environment.NewLine + line;
                }

                cmd = cmd.Substring(0, cmd.Length - 1);
            }

            cmd = cmd.Trim();

            History.Add(cmd);

            if (Timer.IsRunning)
            {
                Timer.Reset();
                Timer.Start();
            }

            return cmd.Trim();
        }

        /// <summary>
        ///     Read a line from queue or user
        /// </summary>
        private string ReadLine()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            if (Queue.Count > 0)
            {
                var cmd = Queue.Dequeue();
                Write(cmd + Environment.NewLine);
                return cmd;
            }
            else
            {
                if (AutoExit) return "exit";

                var cmd = Console.ReadLine();

                if (OnWrite != null)
                {
                    OnWrite(cmd + Environment.NewLine);
                }

                return cmd;
            }
        }

        private void Write(string text)
        {
            Console.Write(text);

            if (OnWrite != null)
            {
                OnWrite(text);
            }
        }
    }
}