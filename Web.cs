using System;
using System.Collections.Generic;
using System.Text;

namespace KiK_SN
{
    class Web
    {
        public List<List<Neuron>> layers;
        public List<int> web_structure;
        public Web Copy(Web my_old_web)
        {
            Web my_new_web = new Web();
            my_new_web.layers = new List<List<Neuron>>();
            foreach (var layer in my_old_web.layers)
            {
                my_new_web.layers.Add(new List<Neuron>());
                foreach (var single_neuron in layer)
                    my_new_web.layers[my_new_web.layers.Count - 1].Add(single_neuron.Copy(single_neuron));
            }
            my_new_web.web_structure = my_old_web.web_structure;
            return my_new_web;
        }
        public Web Fill(Web my_web)
        {
            for (int i = 0; i < my_web.web_structure.Count; i++)
            {
                my_web.layers.Add(new List<Neuron>());
                for (int j = 0; j < my_web.web_structure[i]; j++)
                {
                    if (i == 0)
                        my_web.layers[i].Add(new Neuron());
                    else
                        my_web.layers[i].Add(new Neuron(true)); // jeżeli neuron nie leży na pierwszej warstwie to użyj true
                }
            }
            return my_web;
        }
    }
}
