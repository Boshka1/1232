using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Drazhin_32_1_AI.NeuroNet;


namespace Drazhin_32_1_AI
{
    public partial class FormMain : Form
    {
        // Добавляем поля слоёв
        private Layer hiddenLayer;
        private Layer outputLayer;
        private Network network; // объявление нейросети



        private double[] inputPixels;


        // Конструктор
        public FormMain()
        {
            InitializeComponent();
            inputPixels = new double[15];

            network = new Network();
        }
        // Обработчик событий
        private void Changing_State_Pixel_Button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.Black)
            {
                ((Button)sender).BackColor = Color.White;
                inputPixels[((Button)sender).TabIndex] = 1d;
            }
            else
            {
                ((Button)sender).BackColor = Color.Black;
                inputPixels[((Button)sender).TabIndex] = 0d;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        private void SaveTrainSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string tmpStr = numericUpDown_NecessaryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }


        private void Button_Recognize_Click(object sender, EventArgs e)
        {

            network.ForwardPass(network, inputPixels);
            label_out.Text = network.Fact.ToList().IndexOf(network.Fact.Max()).ToString();
            //label_probability.Text = (100 * network.Fact.Max()).ToString("0.00") + " %";
        }

        
    }
}