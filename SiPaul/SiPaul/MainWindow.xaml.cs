using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SiPaul
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LifeManager _lifeManager = new LifeManager(5, 5);

        public MainWindow()
        {
            bool[,] cells = _lifeManager.getCell();
            List<List<bool>> lsts = new List<List<bool>>();

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                lsts.Add(new List<bool>());

                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    lsts[i].Add(cells[i,j]);
                }
            }

            InitializeComponent();

            lst.ItemsSource = lsts;
        }

		public void MenuItem_Reset(object sender, CanExecuteRoutedEventArgs e)
		{

		}

		public void MenuItem_Start(object sender, CanExecuteRoutedEventArgs e)
		{
		}
    }
}
