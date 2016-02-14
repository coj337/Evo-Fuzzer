using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Evolutionary_Fuzzer {
    internal static class Core {
        public static readonly Debugger debugger = new Debugger();
        public static String seedPath { private get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        private static ArrayList sessions = new ArrayList();

        public static void start(String command, String param) {
            Session currentSession = new Session();

            Byte[] seed = File.ReadAllBytes(seedPath); //Read initial seed data from the seed file
            tokenizeSeed(seed, currentSession);

            //LOOP
            String[] processDetails = debugger.createProcess(command, param);

            currentSession.parseCovFile(debugger.logDir + "\\drcov." + processDetails[1] + "." + processDetails[0].PadLeft(5, '0') + ".0000.proc.log"); // Pass log location so it can be parsed
            
            //currentSession = new Session(seed);
            //sessions.Add(currentSession);

            // Analyze data
                // Generate fitness
                // Update file format
            // Mutate Input File
            //END LOOP
        }

        private static void tokenizeSeed(Byte[] seed, Session session){
            Token.Type tokenType;
            List<Byte> tokenData = new List<Byte>();

            foreach (Byte tok in seed){
                if (isAscii(Convert.ToChar(tok))) {
                    tokenType = Token.Type.Ascii;
                    tokenData.Add(tok);
                }
                else 
                    if (isBracket(Convert.ToChar(tok))){
                        tokenType = Token.Type.Bracket;
                    }
            }
        }

        private static Boolean isAscii(Char c){
            return (c >= 'A' && c <= 'z');
        }

        private static Boolean isBracket(Char c) {
            return new[] {'{', '}', '(', ')', '<', '>', '[', ']'}.Contains(c);
        }

        private static Boolean isQuote(Char c) {
            return new[] {'\'', '\"', '`'}.Contains(c);
        }
    }
}