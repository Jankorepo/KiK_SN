using System;
using System.Collections.Generic;
using System.Text;

namespace KiK_SN
{
    class Neuron
    {
        double output;
        List<double> matrix=new List<double>();
        public Neuron Copy(Neuron my_old_neuron)
        {
            Neuron my_new_neuron = new Neuron();
            foreach (var number in my_old_neuron.matrix)
            {
                my_new_neuron.matrix.Add(number);
            }
            my_new_neuron.output = my_old_neuron.output;
            return my_old_neuron;
        }
    }
}
