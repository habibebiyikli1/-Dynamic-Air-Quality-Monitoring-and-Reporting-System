using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace ArduinoDataLogger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }  
        }
        SerialPort port;
        StreamWriter writer;
        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(pathTxt.Text)){
                MessageBox.Show("Lütfen dosya yolunu seçiniz.");
                return;
            }
            if(String.IsNullOrEmpty(comboBox1.Text)){
                MessageBox.Show("Lütfen COM portunu seçiniz.");
                return;
            }

            writer = new StreamWriter(pathTxt.Text,true);
            writer.AutoFlush = true;
            port=new SerialPort(comboBox1.Text,9600);
            port.NewLine = "\n";
            port.DataReceived+=new SerialDataReceivedEventHandler(port_DataReceived);
            if(!port.IsOpen)
                port.Open();
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp=(SerialPort)sender;
            String line = sp.ReadLine();
            if (line.Length < 15) return;
            this.Invoke((MethodInvoker)(() => listBox1.Items.Add(line)));
            writer.WriteLine(line);
            Console.WriteLine(line);
        }

        private void pathTxt_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = dialog.FileName;
            }
        }
    }
}
