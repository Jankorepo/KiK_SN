using System;
using System.Collections.Generic;
using System.Text;

namespace KiK_SN
{
    class Neuron
    {
        public double output;
        public List<double> matrix=new List<double>();
        public double correction;
        public double bias = 1;
        public Neuron() { }
        public Neuron(int number_of_inputs)
        {
            Random rnd = new Random();
            for (int i = 0; i < number_of_inputs+1; i++)
            {
                matrix.Add(rnd.NextDouble()*2-1);
            }
        }
        public Neuron Copy(Neuron my_old_neuron)
        {
            Neuron my_new_neuron = new Neuron();
            foreach (var number in my_old_neuron.matrix)
                my_new_neuron.matrix.Add(number);
            my_new_neuron.output = my_old_neuron.output;
            return my_old_neuron;
        }
        
    }
}
