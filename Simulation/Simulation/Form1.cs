using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        char[] cevaplar = { 'A', 'B', 'C', 'D', 'E' };
        private async void button1_Click(object sender, EventArgs e)
        {

            var tekrar_sayisi = (int)numericUpDown2.Value;

            if (tekrar_sayisi > 1)
            {
                double toplamNet = 0;
                for (int i = 0; i < tekrar_sayisi; i++)
                {
                    await Task.Run(() =>
                    {
                        toplamNet += simulate(tekrar_sayisi);
                    });
                }

                double ortalamaNet = toplamNet / tekrar_sayisi;
                MessageBox.Show($"Ortalama = {ortalamaNet}");
            }
            else
            {
                simulate(tekrar_sayisi);
            }

        }

        double simulate(int t)
        {
            int soru_sayisi = Convert.ToInt32(numericUpDown1.Value);
            double dogru_sayisi = 0;

            List<char> cevapAnahtari = new List<char>();
            List<Cevap> cevaps = new List<Cevap>();
            var rnd = new Random();
            for (int i = 0; i < soru_sayisi; i++)
            {
                var a = rnd.Next(0, 5);
                cevapAnahtari.Add(cevaplar[a]);
            }
            for (int j = 0; j < soru_sayisi; j++)
            {

                var b = rnd.Next(0, 5);
                cevaps.Add(new Cevap { DogruCevap = cevapAnahtari[j], VerilenCevap = cevaplar[b] });
            }

            for (int i = 0; i < soru_sayisi; i++)
            {
                if (cevaps[i].Dogruluk)
                {
                    dogru_sayisi++;
                }
            }

            var yanlis_sayisi = soru_sayisi - dogru_sayisi;

            double net = dogru_sayisi - (yanlis_sayisi / 4);

            

            if (t == 1)
            {
                label2.Text = $"NET = {net}";
                listView1.Items.Clear();

                for (var c = 0; c < cevaps.Count; c++)
                {
                    var item = new ListViewItem();
                    item.SubItems.Add((c + 1).ToString());
                    item.SubItems.Add(cevaps[c].VerilenCevap.ToString());
                    item.SubItems.Add(cevaps[c].DogruCevap.ToString());
                    item.BackColor = cevaps[c].Dogruluk ? Color.Green : Color.Red;
                    listView1.Items.Add(item);
                }
            }
            return net;
        }
    }
}
