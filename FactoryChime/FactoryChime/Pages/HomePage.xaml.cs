using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
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
using System.Windows.Threading;

namespace FactoryChime.Pages
{
    /// <summary>
    /// HomePage.xaml の相互作用ロジック
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();


            FactoryChimeSchedule GetNextSchedule()
            {
                var nct = MemoryStorage.Instance.FactoryChimeSchedules.Keys.Where(x => x.CompareTo(DateTime.Now.ToString("HH:mm")) > 0).FirstOrDefault();
                if (nct != null)
                {
                    return MemoryStorage.Instance.FactoryChimeSchedules[nct];
                    
                }

                return MemoryStorage.Instance.FactoryChimeSchedules.FirstOrDefault().Value;
            }

            tbClock.Text = DateTime.Now.ToString("yyyy年 \n M月dd日(ddd) \n HH:mm:ss");

            var schedule = GetNextSchedule();
            tbNextPlayChime.Text = $"{schedule!.Name}\n{schedule!.Time}";

            var timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += delegate 
            {
                var now = DateTime.Now;
                tbClock.Text = now.ToString("yyyy年 \n M月dd日(ddd) \n HH:mm:ss");
                var ct = now.ToString("HH:mm");
                var ss = now.Second;
                
                if(MemoryStorage.Instance.FactoryChimeSchedules.ContainsKey(ct) && ss == 0)
                {
                    var schedule = MemoryStorage.Instance.FactoryChimeSchedules[ct];
                    
                    //if (schedule.Replayed) { return; }

                    var player = schedule.SoundPlayer;
                    
                    Task.Run(() =>
                    {
                        player.PlaySync();

                        var schedule = GetNextSchedule();
                        Dispatcher.Invoke(() =>
                        {
                            tbNextPlayChime.Text = $"{schedule!.Name}\n{schedule!.Time}";
                        });
                    });

                    //schedule.Replayed = true;
                }
            };
            timer.Start();

            
        }
    }
}
