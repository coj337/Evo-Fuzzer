using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolutionary_Fuzzer {
    internal class Token {
        public enum Type{
            Ascii,
	        Utf16,
	        Utf32,
	        AsciiCmd,
	        AsciiCmdVar,
	        AsciiSpace,
	        AsciiEnd,
	        BinaryEnd,
	        Len,
	        Hash,
	        Mixed,
	        Binary,
	        SessionId,
	        Flags,
	        ProtoId,
	        Undefined
        }

        private Byte[] data;
        private Type type;

        public Token(Type type, Byte[] data){
            this.type = type;
            this.data = data;
        }
    }
}
