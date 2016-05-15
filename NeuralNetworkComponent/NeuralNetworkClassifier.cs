﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace NeuralNetworkComponent
{
    public class NeuralNetworkClassifier: Component
    {
        private Uri m_url;
        private const string hostname = "http://46.101.106.176";
        private string m_data;
        public NeuralNetworkClassifier(int[] pixels)
        {
            m_url = new Uri(hostname + "/api/v1/neural_network/");
            m_data = string.Join(",", pixels);
        }

        public double[] predict(){
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            formData["image"] = m_data;
            byte[] responseBytes = webClient.UploadValues(m_url, "POST", formData);
            string jsonString = Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(jsonString);
            JObject json = JObject.Parse(jsonString);
            double[] result = new double[10];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = json[i.ToString()].Value<double>();
            }
            return result;
        }
    }
}
