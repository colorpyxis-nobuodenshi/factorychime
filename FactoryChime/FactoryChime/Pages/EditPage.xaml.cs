using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// EditPage.xaml の相互作用ロジック
    /// </summary>
    public partial class EditPage : Page
    {
        public EditPage()
        {
            InitializeComponent();

            var sch = new ObservableCollection<FactoryChimeSchedule>();
            foreach(var s in MemoryStorage.Instance.FactoryChimeSchedules.Values)
            {
                sch.Add(s);
            }
            lstPlaySchedule.ItemsSource = sch;
        }
    }
}
