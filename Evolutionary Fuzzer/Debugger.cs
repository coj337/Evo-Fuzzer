using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Evolutionary_Fuzzer{
    public class Debugger{ //TODO: Create parent class for windows and unix debuggers.
        private Process p;
        private int functionCallCount; //TODO: Find technical word and rename

        public void attachProcess(string pid){
            // Declare the child process.
            p = new Process {
                // Redirect the output stream of the child process.
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    FileName = "cdb.exe",
                    Arguments = "-y \"srv*http://msdl.microsoft.com/download/symbols\" -p \"" + pid + "\" -c \"kb; qd\"", //TODO: Edit to get needed data from application
                    CreateNoWindow = true
                }
            };
            //Start the child process
            p.Start();

            Console.WriteLine(p.StandardOutput.ReadToEnd()); //Write result of command to stdout
            p.WaitForExit();
        }

        public void createProcess(string command){
            // Declare the child process.
            p = new Process {
                // Redirect the output stream of the child process.
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    FileName = "cdb.exe",
                    Arguments = "\"" + command + "\" -y \"srv*http://msdl.microsoft.com/download/symbols\" -c \"kb; qd\"", //TODO: Edit to get needed data from application
                    CreateNoWindow = true
                }
            };
            //Start the child process
            p.Start();
            //p.StandardInput.WriteLine();
            Console.WriteLine(p.StandardOutput.ReadToEnd()); //Write result of command to stdout
            p.WaitForExit();
        }
    }
}