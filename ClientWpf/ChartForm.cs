using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWpf
{
    public partial class ChartForm : Form
    {
        public ChartForm(double[] values)
        {
            InitializeComponent();

            int[] xs = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                xs[i] = i + 1;
            }


            var bar = formsPlot1.Plot.AddBar(values);
            bar.ShowValuesAboveBars = true;
            formsPlot1.Plot.XAxis.ManualTickSpacing(1);
            formsPlot1.Plot.XAxis.SetBoundary(-1, values.Length);
            formsPlot1.Plot.XAxis.Label("Итерации алгоритма");
            formsPlot1.Plot.YAxis.Label("Лучшие значения");
            formsPlot1.Plot.Title("График изменения результатов");
            formsPlot1.Refresh();
        }
    }
}
