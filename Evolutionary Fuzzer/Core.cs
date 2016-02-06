using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Evolutionary_Fuzzer {
    class Core {
        public static Debugger debugger = new Debugger();
        public static String seedPath { private get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        private static ArrayList sessions = new ArrayList();

        public static void Start(string command, string param) {
            String[] processDetails;
            Session currentSession;

            //LOOP
            currentSession = new Session();
            processDetails = debugger.createProcess(command, param); // Run program under code coverage tool, returns drcovs childs process id and name

            currentSession.parseCovFile(debugger.logDir + "\\drcov." + processDetails[1] + "." + processDetails[0].PadLeft(5, '0') + ".0000.proc.log"); // Pass log location so it can be parsed

            sessions.Add(currentSession);
            // Analyze data
                // Generate fitness
                // Update file format
            // Mutate Input File
            //END LOOP
        }
    }
}