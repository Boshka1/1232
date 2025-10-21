namespace Drazhin_32_1_AI.NeuroNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int numofneurons, int numofprevneurons, NeuronType nt, string name_Layer) : base(numofneurons, numofprevneurons, nt, name_Layer)
        {
        }
        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[numofneurons];
            for (int i = 0; i < numofneurons; i++)
            {
                hidden_out[i] = neurons[i].Output;
            }
            nextLayer.Data = hidden_out;
        }
        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numofneurons];
            return gr_sum;
        }
    }
}