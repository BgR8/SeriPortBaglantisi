using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace seriPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var ports = SerialPort.GetPortNames();
            comboBox1.DataSource = ports;

            comboBox2.Items.AddRange(new string[] { "300", "600", "1200", "2400", "4800", "9600", "19200" });
            var databids = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", }; comboBox3.Items.AddRange(databids);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text);
                serialPort1.DataBits = Convert.ToInt16(comboBox3.Text);

                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    Sinyal.Visible = true;
                    Sinyal.Text = "Bağlantı Kuruldu";
                    Sinyal.ForeColor = Color.Green;
                }
            }
            catch (Exception)
            {

                Sinyal.Visible = true;
                Sinyal.Text = "Bağlantı Kurulumadı";
                Sinyal.ForeColor = Color.Red;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string bilgi = (serialPort1.ReadExisting());
                textBox1.Text = bilgi.ToString();
                listBox1.Items.Add(textBox1.Text);
                System.Threading.Thread.Sleep(1200);
            }
            catch (Exception)
            {

                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();
            Sinyal.Visible = true;
            Sinyal.Text = "Bağlantı Sonlandırıldı";
            Sinyal.ForeColor = Color.Red;
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.WriteLine(textBox2.Text);
                    listBox2.Items.Add(textBox2.Text);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("PORTU BAĞLA");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }
}
