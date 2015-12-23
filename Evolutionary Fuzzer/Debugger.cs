using System;
using System.Collections;
using System.Diagnostics;

namespace Evolutionary_Fuzzer {
    public class Debugger{ //TODO: Create parent class for windows and unix instrumentation.
        public string dynamoDir { private get; set; }
        public string arch { private get; set; }
        public string logDir { private get; set; }
        private Process p;
        private int hits;
        private ArrayList blockVector;

        public Debugger(){
            dynamoDir = "C:\\Program Files (x86)\\DynamoRIO\\";
            arch = "32";
            logDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        public void createProcess(string command, string parameters) {
            // Declare the child process.
            p = new Process {
                // Redirect the output stream to stdout and stderr
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    FileName = dynamoDir + "bin" + arch + "\\drrun.exe",
                    Arguments = "-t drcov -dump_text -logdir \"" + logDir + "\" -- \"" + command + "\" \"" + parameters + "\"",
                    CreateNoWindow = true
                }
            };
            
            //Start the child process
            p.Start();
            Console.Write(p.StandardOutput.ReadToEnd()); //Write result of command to stdout
            Console.Write(p.StandardError.ReadToEnd()); //Write any errors to stdout
        }
    }
}