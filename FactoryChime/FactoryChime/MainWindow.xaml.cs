using ModernWpf;
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
using ui = ModernWpf.Controls;

namespace FactoryChime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum NaviIcon
    {
        Home,
        Add,
        Edit,

        None,
    }

    public partial class MainWindow : Window
    {
        static IReadOnlyDictionary<NaviIcon, Type> _pages = new Dictionary<NaviIcon, Type>()
        {
            {NaviIcon.Home, typeof(Pages.HomePage)},
            {NaviIcon.Add, typeof(Pages.AddPage)},
            {NaviIcon.Edit, typeof(Pages.EditPage)},
        };

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                MemoryStorage.Instance.Load();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"chimeschedule.txtの読み込みに失敗しました。\n書式およびサウンドファイル(wav)を確認してください。", "", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            

            NaviView.SelectionChanged += NaviView_SelectionChanged;

            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
        }
        
        private void NaviView_SelectionChanged(ui.NavigationView sender, ui.NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var selectedItem = (ui.NavigationViewItem)args.SelectedItem;
                // Tag取得
                string iconName = selectedItem.Tag.ToString();
                // ヘッダー設定
                //sender.Header = iconName;

                if (Enum.TryParse(iconName, out NaviIcon icon))
                {
                    // 対応するページ表示
                    ContentFrame.Navigate(_pages[icon]);
                }
                else
                {
                    // 空ページ表示
                    //ContentFrame.Navigate(_pages[NaviIcon.None]);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
