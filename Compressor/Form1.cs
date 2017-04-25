using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compressor
{
    public partial class Form1 : Form
    {
        byte[] dataArray;  // 16MB
        int[] freq = new int[256]; 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                label1.Text = fd.FileName;

                FileStream fs = new FileStream(fd.FileName, FileMode.Open);
                fs.Seek(0, SeekOrigin.Begin);

                byte[] temp = new byte[16777216];

                int size = 0;
                for (; size < fs.Length; )
                {
                    temp[size++] = (byte)fs.ReadByte();
                }

                dataArray = new byte[size];
                for (int i=0; i < size; i++)
                {
                    dataArray[i] = temp[i];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Calcfrequency();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Calcfrequency()
        {
            foreach (byte b in dataArray)
            {
                freq[b]++;
            }

            //int size = 0;
            //for (int i = 0; i < freq.Length; i++)
            //{
            //    size += freq[i];
            //}
        }

        private int FindMinFrequency(int index)
        {
            int minIndex = 0;
            for (int i = 0; i < index; i++)
            {
                if()
            }

            return minIndex;
        }
    }
}
