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

namespace Drazhin_32_1_AI
{
    public partial class FormMain : Form
    {
        private double[] inputPixels; //массив входных

        //конструктор
        public FormMain()
        {
            InitializeComponent();

            inputPixels = new double[15];
            
        }

        //обработчик события клика "пиксельной" кнопки
        private void Changing_State_Pixel_Button_Click(object sender, EventArgs e)
        {
            //если изначально кнопка белая
            if (((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = Color.Black; //меняем цвет кнопки
                inputPixels[((Button)sender).TabIndex] = 1d; //меняем значение в массиве
            }
            else //если изначально кнопка черная
            {
                ((Button)sender).BackColor = Color.White; //меняем цвет кнопки
                inputPixels[((Button)sender).TabIndex] = 0d; //меняем значение в массиве
            }
        }

        //сохранить в файл ОБУЧАЮЩИЙ пример
        private void SaveTrainSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string tmpStr = numericUpDown_NecessaryOutput.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllLines()
        }
 
    }
}
