using System;
using static System.Math;

namespace Drazhin_32_1_AI.NeuroNet
{
    class Neuron
    {
        //поля
        private NeuronType type;        //тип нейрона
        private double[] weights;       //его веса
        private double[] inputs;        //его входы
        private double output;          //его выход
        private double derivative;      //производная

        //константы для функции активации 
        private double a = 0.01d;

        //свойства 
        public double[] Weights { get => weights; set => weights = value; }
        public double[] Inputs { get => inputs; set => inputs = value; }
        public double Output { get => output; }
        public double Derivative { get => derivative; }

        //конструктор 
        public Neuron(double[] memoryWeights, NeuronType typeNeuron)
        {
            this.type = typeNeuron;
            this.weights = memoryWeights;
        }

        //метод активации нейрона
        public void Activator(double[] i)
        {
            inputs = i;
            double sum = weights[0];
            for (int j = 0; j < inputs.Length; j++)
            {
                sum += inputs[j] * weights[j + 1];
            }

            switch (type)
            {
                case NeuronType.Hidden:
                    output = Tanh(sum);
                    derivative = TanhDerivative(output); // производная через output
                    break;
                case NeuronType.Output:
                    output = Exp(sum);
                    break;
            }
        }

        // Функция активации гиперболический тангенс
        private double Tanh(double x)
        {
            return Math.Tanh(x);
        }

        // Производная гиперболического тангенса
        private double TanhDerivative(double tanhOutput)
        {
            return 1 - tanhOutput * tanhOutput;
        }



    }
}
