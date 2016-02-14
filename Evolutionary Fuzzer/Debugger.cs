using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Management;

namespace Evolutionary_Fuzzer {
    public class Debugger{
        public String dynamoDir { private get; set; }
        public String arch { private get; set; }
        public String logDir { get; set; }
        private Process p;
        private String[] childProcessDetails { get; set; } = new String[2];

        public Debugger(){
            dynamoDir = "C:\\Program Files (x86)\\DynamoRIO";
            arch = "32";
            logDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        public String[] createProcess(String command, String parameters) {
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
            while (!p.HasExited) { //The child process created by drcov may not exist right away, loop until it's found or drcov exits
                childProcessDetails = getChildPID();
                if (childProcessDetails[0] != "")
                    break;
            }
            p.WaitForExit();
            Console.WriteLine(p.StandardOutput.ReadToEnd()); // Write result of command to stdout
            if (p.StandardError.Peek() != -1) {
                Console.WriteLine(p.StandardError.ReadToEnd()); // Write any errors to stdout
            }
            else {
                Console.WriteLine("Program ran successfully!");
            }
            return childProcessDetails;
        }

        private String[] getChildPID() {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={p.Id}");

            foreach (ManagementObject proc in searcher.Get()) {
                if (proc["Name"].ToString() != "conhost.exe")
                    return new String[] { proc["ProcessID"].ToString(), proc["Name"].ToString() };
            }
            return new[] {""};
        }
    }
}