using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evolutionary_Fuzzer {
    internal class Session {
        private int fitness; // Sum of unique basic blocks
        private int bbCount; // Sum of total basic blocks
        private List<Token> data = new List<Token>(); //Proposed file format for this session
        private List<bbDetails> bbVector = new List<bbDetails>();

        private class bbDetails{
            public int moduleID; // Basic blocks module ID
            public String start; // Memory address where the basic block begins
            public int size; // Size of the basic block (bytes)
        }
        

        public void parseCovFile(String filePath) {
            StreamReader reader = File.OpenText(filePath);

            getBbCount(reader);
            reader.ReadLine(); // Consume headings for bbVector info
            getBbVector(reader);
            reader.Dispose(); // Close the stream
            calcFitness();

            Console.WriteLine("File parsed successfully!");
        }

        public void addToken(Token.Type type, Byte[] data){
            this.data.Add(new Token(type, data));
        }

        private void getBbCount(StreamReader reader) {
            while (reader.Peek() != 'B') { // Discard all lines until the BB Count
                reader.ReadLine();
            }
            String line = reader.ReadLine();
            bbCount = int.Parse(line.Substring(10, line.Length - 14)); //10th char to (length - 14)th char will be the BB count
        }

        private void getBbVector(StreamReader reader) {
            for (int i = 0; i < bbCount; i++) { //Loop through all the basic blocks
                bbVector.Add(new bbDetails());
                String line = reader.ReadLine();

                bbVector[i].moduleID = int.Parse(line.Substring(7, 3));
                bbVector[i].start = line.Substring(15, 16);
                bbVector[i].size = int.Parse(line.Substring(32, 4));
            }
        }

        private void calcFitness() {
            fitness = bbVector.Select(x => x.start).Distinct().Count();
        }
    }
}