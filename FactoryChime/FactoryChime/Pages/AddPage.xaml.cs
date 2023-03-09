using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FactoryChime.Pages
{
    /// <summary>
    /// AddPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();

            for(var i=0;i<24;i++)
            {
                lstHour.Items.Add(i.ToString());
            }
            for (var i = 0; i < 59; i++)
            {
                lstMin.Items.Add(i.ToString());
            }
            lstHour.SelectedValue = "0";
            lstMin.SelectedValue = "0";

            for(var i=0;i<10;i++)
            {
                lstSound.Items.Add($"Sound{i+1}.wav");
            }
            lstSound.SelectedIndex = 0;
        }
    }
}
