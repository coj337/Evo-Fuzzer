﻿using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Evolutionary_Fuzzer.Custom_Controls {
    public class TextBoxConsole : TextWriter {
        readonly TextBox output;

        public TextBoxConsole(TextBox output) {
            this.output = output;
        }

        public override void Write(char value) {
            base.Write(value);
            if (output.InvokeRequired)
                output.Invoke(new MethodInvoker(delegate { this.output.AppendText(value.ToString()); }));
            else
                output.AppendText(value.ToString());
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
