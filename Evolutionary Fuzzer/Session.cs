using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evolutionary_Fuzzer {
    internal class Session {
        private Int32 fitness; // Sum of unique basic blocks
        private Int32 bbCount; // Sum of total basic blocks
        private readonly List<Token> data = new List<Token>(); //Proposed file format for this session
        private readonly List<BbDetails> bbVector = new List<BbDetails>();

        private class BbDetails{
            public Int32 moduleID; // Basic blocks module ID
            public String start; // Memory address where the basic block begins
            public Int32 size; // Size of the basic block (bytes)
        }
        
        public void parseCovFile(String filePath) {
            StreamReader reader = File.OpenText(filePath);

            getBbCount(reader);
            reader.ReadLine(); // Consume headings for bbVector info
            getBbVector(reader);
            reader.Dispose(); // Close the stream
            calcFitness();
        }

        public void addToken(Token.Type type, Byte[] data){
            this.data.Add(new Token(type, data));
        }

        private void getBbCount(TextReader reader) {
            while (reader.Peek() != 'B') { // Discard all lines until the BB Count
                reader.ReadLine();
            }

            String line = reader.ReadLine();
            if (line != null)
                bbCount = Int32.Parse(line.Substring(10, line.Length - 14)); //10th char to (length - 14)th char will be the BB count
        }

        private void getBbVector(TextReader reader) {
            for (Int32 i = 0; i < bbCount; i++) { //Loop through all the basic blocks
                bbVector.Add(new BbDetails());
                String line = reader.ReadLine();

                bbVector[i].moduleID = Int32.Parse(line.Substring(7, 3));
                bbVector[i].start = line.Substring(15, 16);
                bbVector[i].size = Int32.Parse(line.Substring(32, 4));
            }
        }

        private void calcFitness() {
            fitness = bbVector.Select(x => x.start).Distinct().Count();
        }
    }
}