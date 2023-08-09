using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "Usman's Prediction";
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Directory.GetCurrentDirectory()}\\predictionConfig.json";
        private string[] _predictions;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            this.bPredict.Enabled = false;
            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Value = i;
                        this.Text = $"{i}%";
                    }));
                    System.Threading.Thread.Sleep(10);
                }
            });

            var index = _random.Next(_predictions.Length);
            MessageBox.Show(_predictions[index]);
            this.Text = APP_NAME;
            this.bPredict.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;
            try
            {
                var data = File.ReadAllText(PREDICTIONS_CONFIG_PATH);
                _predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                if (_predictions == null)
                {
                    this.Close();
                }
                else if (_predictions.Length == 0)
                {
                    MessageBox.Show("Список предсказаний пуст! Попробуйте в другой раз");
                    this.Close();
                }
            }
        }
    }
}
