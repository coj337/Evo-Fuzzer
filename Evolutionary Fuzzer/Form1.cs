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
        private readonly Debugger debugger = new Debugger();
        
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
            new Task(() => debugger.createProcess(textBox1.Text, textBox2.Text)).Start();

        }

        private void textBox4_TextChanged(object sender, EventArgs e){
            debugger.dynamoDir = textBox4.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e){
            debugger.arch = "32";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            debugger.arch = "64";
        }

        private void button3_Click(object sender, EventArgs e) {

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
            debugger.logDir = textBox5.Text;
        }
    }
}