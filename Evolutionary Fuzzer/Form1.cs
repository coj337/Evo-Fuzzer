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
using Evolutionary_Fuzzer.Custom_Controls;

namespace Evolutionary_Fuzzer {
    public partial class Form1 : Form {        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Console.SetOut(new TextBoxConsole(txtConsole));
            textBox5.Text = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (checkInputs()) { //Check all fields exists, otherwise print an error
                new Task(() => Core.Start(textBox1.Text, textBox2.Text.Replace("<input>", textBox3.Text))).Start();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e){
            Core.debugger.dynamoDir = textBox4.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e){
            Core.debugger.arch = "32";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            Core.debugger.arch = "64";
        }

        private void button3_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBox5.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e){
            Core.debugger.logDir = textBox5.Text;
        }

        private Boolean checkInputs() {
            if (textBox4.Text == "") {
                Console.WriteLine("Error: No DynamoRIO directory selected.");
                return false;
            }
            else if (textBox5.Text == "") {
                Console.WriteLine("Error: No log directory selected.");
                return false;
            }
            else if (textBox1.Text == "") {
                Console.WriteLine("Error: No executable selected.");
                return false;
            }
            else if (textBox2.Text == "") {
                Console.WriteLine("Error: No parameters entered.");
                return false;
            }
            else if (!textBox2.Text.Contains("<input>")) {
                Console.WriteLine("Error: No <input> placeholder in the parameters.");
                return false;
            }
            else if(textBox3.Text == "") {
                Console.WriteLine("Warning: No seed file selected. This may take a while or cause issues with programs expecting specific extensions.");
                textBox3.Text = "fuzzed_file";
                return true;
            }
            else {
                return true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            Core.seedPath = textBox3.Text;
        }
    }
}