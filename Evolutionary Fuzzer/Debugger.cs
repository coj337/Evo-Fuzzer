using System;
using System.Collections;
using System.Diagnostics;

namespace Evolutionary_Fuzzer {
    public class Debugger{
        public string dynamoDir { private get; set; }
        public string arch { private get; set; }
        public string logDir { private get; set; }
        private Process p;

        public Debugger(){
            dynamoDir = "C:\\Program Files (x86)\\DynamoRIO";
            arch = "32";
            logDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        public void createProcess(string command, string parameters) {
            // Format the directories so they can optionally end in a slash
            dynamoDir = dynamoDir.TrimEnd('\\', '/');
            logDir = logDir.TrimEnd('\\', '/');

            // Pre/Append quotes to the parameter to allow spaces
            parameters.Insert(0, "\"");
            parameters.Insert(parameters.Length, "\"");

            // Declare the child process.
            p = new Process {
                // Redirect the output stream to stdout and stderr
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    FileName = dynamoDir + "\\bin" + arch + "\\drrun.exe",
                    Arguments = "-t drcov -dump_text -logdir \"" + logDir + "\" -- \"" + command + "\" " + parameters,
                    CreateNoWindow = true
                }
            };

            // Start the process
            p.Start();
            Console.WriteLine(p.StandardOutput.ReadToEnd()); // Write result of command to stdout
            if (p.StandardError.Peek() != -1) {
                Console.WriteLine(p.StandardError.ReadToEnd()); // Write any errors to stdout
            }
            else {
                Console.WriteLine("Program ran successfully!");
            }
        }
    }
}