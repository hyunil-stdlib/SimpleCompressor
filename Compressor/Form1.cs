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
    public class HuffmanTree
    {
        public byte mValue;
        public int mFreq;
        public HuffmanTree mRight;
        public HuffmanTree mLeft;

        public HuffmanTree()
        {
            mValue = 0;
            mFreq = 0;
            mRight = null;
            mLeft = null;
        }
    }

    public partial class Form1 : Form
    {
        HuffmanTree[] huffList = new HuffmanTree[256];
        HuffmanTree headNode = new HuffmanTree();

        byte[] dataArray;  // 16MB

        int[] freq = new int[256];
        int nodeCount = 0;

        uint[] huffCode = new uint[256];
        int[] huffLength = new int[256];

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
                if (huffList[i].mFreq < huffList[minIndex].mFreq)
                    minIndex = i;
            }

            return minIndex;
        }

        public void MakeHuffmanTree()
        {
            for (int i = 0; i < 256; i++)
            {
                if (freq[i] > 0)
                {
                    HuffmanTree node = new HuffmanTree();
                    node.mValue = (byte)i;
                    node.mFreq = freq[i];
                    node.mRight = null;
                    node.mLeft = null;

                    nodeCount++;

                    huffList[nodeCount - 1] = node;
                }
            }


            int head = nodeCount;

            while (head > 1)
            {
                int min = FindMinFrequency(head);
                HuffmanTree node1 = huffList[min];

                head--;
                huffList[min] = huffList[head];

                min = FindMinFrequency(head);
                HuffmanTree node2 = huffList[min];

                HuffmanTree node = new HuffmanTree();

                node.mValue = 0;
                node.mFreq = node1.mFreq + node2.mFreq;
                node.mLeft = node1;
                node.mRight = node2;

                huffList[min] = node;
            }

            headNode = huffList[0];
        }

        public void MakeCode(HuffmanTree node, uint code, int length)
        {
            if (node.mLeft == null && node.mRight == null)
            {
                huffCode[node.mValue] = code;
                huffLength[node.mValue] = length;
            }
            else
            {
                code = code << 1;
                length++;
                MakeCode(node.mLeft, code, length);

                code = code | 1u;
                MakeCode(node.mRight, code, length);
                code = code >> 1;
                length--;
            }
        }

        public void WriteByteFromBit(uint code, bool flush)
        {
            uint nByte = 0;
            int nBit = 7;


             
        }
    }
}
