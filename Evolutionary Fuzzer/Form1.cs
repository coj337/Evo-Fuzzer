using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evolutionary_Fuzzer {
    public partial class Form1 : Form {
        private readonly Debugger debugger = new Debugger();
        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Console.SetOut(new TextBoxConsole(txtConsole));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e){
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK){
                new Task(() => debugger.createProcess(openFileDialog1.FileName)).Start();
            }
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e){
            ProcessChooser dialog = new ProcessChooser();
            if (dialog.ShowDialog() == DialogResult.OK){
                new Task(() => debugger.attachProcess(dialog.pid)).Start();
            }
        }
    }
}