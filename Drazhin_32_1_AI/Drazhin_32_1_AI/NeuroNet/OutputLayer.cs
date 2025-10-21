namespace Drazhin_32_1_AI.NeuroNet
{
    internal class OutputLayer : Layer
    {
        public OutputLayer(int numofneurons, int numofprevneurons, NeuronType nt, string name_Layer) : base(numofneurons, numofprevneurons, nt, name_Layer)
        {
        }

        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numofprevneurons + 1];
            // TODO: дописать код метода  
            return gr_sum;
        }

        public override void Recognize(Network net, Layer nextLayer)
        {
            double e_sum = 0;
            for (int i = 0; i < neurons.Length; i++)
            {
                e_sum += neurons[i].Output;
            }
            for (int i = 0; i < neurons.Length; i++)
            {
                net.Fact[i] = neurons[i].Output / e_sum;
            }
        }
    }
}

