using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Sieci1
{
    public partial class Form2 : Form
    {
        public byte[] _buffer;
        private Socket _clientSocket;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void wyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f = new Form4();
            f.ShowDialog();
        }

        private void wylogujSięToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndConnect(ar);
                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dodajZnajomegoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse("192.168.1.5"), 8001), new AsyncCallback(ConnectCallback), _clientSocket);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            byte[] buffer1 = Encoding.ASCII.GetBytes(toolStripTextBox2.Text + toolStripTextBox4.Text+ " " + "DodajNowegoZnajomegoProsze");
            Thread.Sleep(1500);
            _clientSocket.BeginSend(buffer1, 0, buffer1.Length, SocketFlags.None, new AsyncCallback(SendCallback), _clientSocket);
            _buffer = new byte[256];
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, 0, new AsyncCallback(ReceiveCallback), _clientSocket);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = _clientSocket.EndReceive(ar);

                if (bytesRead == 0)
                {
                    return;
                }
                else
                { _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _clientSocket);
                    string text = Encoding.ASCII.GetString(_buffer);
                    
                    int i = text.IndexOf('\0');
                    if (i >= 0) text = text.Substring(0, i);
                    var a = "Blad";
                     if (a == text)
                    {
                        MessageBox.Show("Nie istnieje konto o podanym Imieniu i Nazwisku. ", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                     else
                     richTextBox1.Invoke(new Action(() => richTextBox1.Text = richTextBox1.Text + Environment.NewLine + text));
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        

        private void usuńZnajomegoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuńToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //int index = 1;
            
            string tmp = richTextBox1.Text;
            //richTextBox1.Text = "";
            //richTextBox1.Text = tmp;
            //while (index < richTextBox1.Text.LastIndexOf(toolStripTextBox5.Text))
            //{
            //    richTextBox1.Find(toolStripTextBox5.Text, index, richTextBox1.TextLength, RichTextBoxFinds.None);
            //    richTextBox1.Text = richTextBox1.Text.Remove(index,index+1);
            //    index = richTextBox1.Text.IndexOf(toolStripTextBox5.Text, index) + 1;
            //}
            string result = "";
            string g = richTextBox1.Text;
            int index = g.IndexOf(tmp);           //    NIE DZIALA !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
            if (index != -1)
            { result = g.Remove(index);
                richTextBox1.Text = g;
             }
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
