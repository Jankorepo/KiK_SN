using System;
using System.Collections.Generic;
using System.Text;

namespace KiK_SN
{
    class Web
    {
        public List<List<Neuron>> layers;
        public List<double> web_structure;
        public Web Copy(Web my_old_web)
        {
            Web my_new_web = new Web();
            my_new_web.layers = new List<List<Neuron>>();
            foreach (var layer in my_old_web.layers)
            {
                my_new_web.layers.Add(new List<Neuron>());
                foreach (var single_neuron in layer)
                {
                    my_new_web.layers[my_new_web.layers.Count - 1].Add(single_neuron.Copy(single_neuron));
                }
            }
            my_new_web.web_structure = my_old_web.web_structure;
            return my_new_web;
        }
    }
}
