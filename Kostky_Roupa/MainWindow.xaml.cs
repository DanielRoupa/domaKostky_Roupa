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
using System.Windows.Threading;

namespace Kostky_Roupa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private kostky[] kostka = new kostky[6];

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < kostka.Length; i++)
            {
                kostka[i] = new kostky();
            }
            vykreslitkostky();
            
            
        }

        private void vykreslitkostku(int x, int y, int vzdalenost, int hodnota)
        {
            Rectangle obryskostky = new Rectangle
            {
                Width = vzdalenost,
                Height = vzdalenost,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetLeft(obryskostky, x);
            Canvas.SetTop(obryskostky, y);
            platno.Children.Add(obryskostky);

            cisla(x, y, hodnota, vzdalenost);

        }
        private void cisla(int x, int y, int hodnota, int vzdalenost)
        {
            double cislovelikost = vzdalenost / 5;
            double odsazeni = vzdalenost / 4;
            double vycentrovano = vzdalenost / 2;
            Point[] pozice = new Point[]
            {
                new Point(vycentrovano, vycentrovano),
                new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                new Point(vycentrovano + odsazeni, vycentrovano + odsazeni),
                new Point(vycentrovano - odsazeni, vycentrovano + odsazeni),
                new Point(vycentrovano + odsazeni, vycentrovano - odsazeni),
                new Point(vycentrovano - odsazeni, vycentrovano),
                new Point(vycentrovano + odsazeni, vycentrovano)
            };

            Ellipse[] cisla = new Ellipse[hodnota];
            for (int i = 0; i < hodnota; i++)
            {
                cisla[i] = new Ellipse
                {
                    Width = cislovelikost,
                    Height = cislovelikost,
                    Fill = Brushes.Black
                };
                Canvas.SetTop(cisla[i], y + pozice[i].Y - cislovelikost / 2);
                Canvas.SetLeft(cisla[i], x + pozice[i].X - cislovelikost / 2);
                platno.Children.Add(cisla[i]);
            }
        }

        private void vykreslitkostky()
        {
            platno.Children.Clear();
            int vzdalenost = 100;
            for (int i = 0; i < kostka.Length; i++)
            {
                vykreslitkostku(i * (vzdalenost + 10), 10, vzdalenost, kostka[i].hodnota);
            }
        }

        private async Task animacehodu()
        {
            for (int i = 0; i < 20; i++)
            {
                foreach (var kostky in kostka)
                {
                    kostky.Hod();
                }
                vykreslitkostky();
                await Task.Delay(100);

            }
        }
        private async void btnhod_Click(object sender, RoutedEventArgs e)
        {
            await animacehodu();
            vykreslitkostky();
        }
    }
}
