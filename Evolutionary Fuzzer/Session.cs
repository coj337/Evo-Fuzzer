using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evolutionary_Fuzzer {
    internal class Session {
        private int fitness;

        public void parseCovFile(String filePath) {
            StreamReader reader = File.OpenText(filePath);
            String line;

            while (reader.Peek() != 'B') { // Discard all lines until the BB Count
                reader.ReadLine();
            }
            line = reader.ReadLine();
            line.Substring(10, line.Length - 14); //10th char to (length - 14)th char will be the BB count
            Console.WriteLine(line);
        }

        private void calcFitness() {

        }
    }
}