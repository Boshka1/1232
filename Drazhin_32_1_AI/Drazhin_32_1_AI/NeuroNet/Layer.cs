using System;
using System.IO;
using System.Windows.Forms;

namespace Drazhin_32_1_AI.NeuroNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        string pathFileWeights;
        protected int numofneurons;
        protected int numofprevneurons;
        protected const double leraningrate = 0.060;
        protected const double momentum = 0.050d;
        protected double[,] lastdeltaweights;
        protected Neuron[] neurons;

        public Neuron[] Neurons { get => neurons; set => neurons = value; }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < numofneurons; i++)
                {
                    Neurons[i].Activator(value);
                }
            }
        }
        protected Layer(int numofneurons, int numofprevneurons, NeuronType nt, string name_Layer)
        {
            this.name_Layer = name_Layer;
            this.numofneurons = numofneurons;
            this.numofprevneurons = numofprevneurons;
            Neurons = new Neuron[numofneurons];
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";
            lastdeltaweights = new double[numofneurons, numofprevneurons + 1];
            double[,] Weights;
            if (File.Exists(pathFileWeights))
            {
                Weights = WeightsInitialize(MemoryMode.GET, pathFileWeights);
            }
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightsInitialize(MemoryMode.INIT, pathFileWeights);
            }
            for (int i = 0; i < numofneurons; i++)
            {
                double[] tmp_weights = new double[numofprevneurons + 1];
                for (int j = 0; j < numofprevneurons + 1; j++)
                {
                    tmp_weights[j] = Weights[i, j];
                }
                Neurons[i] = new Neuron(tmp_weights, nt);
            }
        }
        public double[,] WeightsInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurons + 1];
            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path);
                    string[] memory_element;
                    for (int i = 0; i < numofneurons; i++)
                    {
                        memory_element = tmpStrWeights[i].Split(delim);
                        for (int j = 0; j < numofprevneurons; j++)
                        {
                            weights[i, j] = double.Parse(memory_element[j].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;
                case MemoryMode.SET:
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Файл не найден");
                    }
                    tmpStrWeights = new string[numofneurons];
                    for (int i = 0; i < numofneurons; i++)
                    {
                        tmpStr = Neurons[i].Weights[0].ToString();
                        for (int j = 0; j < numofprevneurons; j++)
                        {
                            tmpStr += delim[0] + Neurons[i].Weights[j].ToString();
                        }
                        tmpStrWeights[i] = tmpStr;
                    }
                    File.WriteAllLines(path, tmpStrWeights);
                    break;
                case MemoryMode.INIT:
                    Random rnd = new Random();
                    int totalWeights = numofneurons * numofprevneurons;
                    double[] allWeights = new double[totalWeights];
                    double sum = 0;
                    double sumSq = 0;

                    // 1. Генерация случайных весов в диапазоне [-1, 1]
                    for (int k = 0; k < totalWeights; k++)
                    {
                        allWeights[k] = rnd.NextDouble() * 2 - 1;
                        sum += allWeights[k];
                    }

                    // 2. Центрирование (среднее = 0)
                    double mean = sum / totalWeights;
                    for (int k = 0; k < totalWeights; k++)
                    {
                        allWeights[k] -= mean;
                        sumSq += allWeights[k] * allWeights[k];
                    }

                    // 3. Нормализация (СКО = 1)
                    double std = Math.Sqrt(sumSq / totalWeights);
                    if (std == 0) std = 1e-8;
                    for (int k = 0; k < totalWeights; k++)
                    {
                        allWeights[k] /= std;
                    }

                    // 4. Заполнение матрицы весов
                    int index = 0;
                    for (int i = 0; i < numofneurons; i++)
                    {
                        for (int j = 0; j < numofprevneurons; j++)
                        {
                            weights[i, j] = allWeights[index++];
                        }
                    }

                    // 5. Сохранение в файл
                    string[] lines = new string[numofneurons];
                    for (int i = 0; i < numofneurons; i++)
                    {
                        string line = "";
                        for (int j = 0; j < numofprevneurons; j++)
                        {
                            line += weights[i, j].ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
                            if (j < numofprevneurons)
                                line += ";";
                        }
                        lines[i] = line;
                    }
                    File.WriteAllLines(path, lines);
                    break;
            }
            //            1.Все веса должны быть случайными
            //2.среднее значение должно быть равно 0(мат ожидание = 0)
            //3.среднее квадратичное отклонение равно 1(вычислить ско всех случайных величин, где выполняется пункт 2, потом разделил на средне квадратичное отклонение)
            return weights;
        }
        abstract public void Recognize(Network nt, Layer nextLayer);
        abstract public double[] BackwardPass(double[] stuff);
    }
}