using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kostky_Roupa
{
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
            Point[] pozice = null;

            switch (hodnota)
            {
                case 1:
                    pozice = new Point[] { new Point(vycentrovano, vycentrovano) };
                    break;
                case 2:
                    pozice = new Point[]
                    {
                        new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano + odsazeni)
                    };
                    break;
                case 3:
                    pozice = new Point[]
                    {
                        new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano, vycentrovano),
                        new Point(vycentrovano + odsazeni, vycentrovano + odsazeni)
                    };
                    break;
                case 4:
                    pozice = new Point[]
                    {
                        new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano - odsazeni, vycentrovano + odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano + odsazeni)
                    };
                    break;
                case 5:
                    pozice = new Point[]
                    {
                        new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano, vycentrovano),
                        new Point(vycentrovano - odsazeni, vycentrovano + odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano + odsazeni)
                    };
                    break;
                case 6:
                    pozice = new Point[]
                    {
                        new Point(vycentrovano - odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano - odsazeni),
                        new Point(vycentrovano - odsazeni, vycentrovano),
                        new Point(vycentrovano + odsazeni, vycentrovano),
                        new Point(vycentrovano - odsazeni, vycentrovano + odsazeni),
                        new Point(vycentrovano + odsazeni, vycentrovano + odsazeni)
                    };
                    break;
            }

            Ellipse[] tecky = new Ellipse[hodnota];
            for (int i = 0; i < hodnota; i++)
            {
                tecky[i] = new Ellipse
                {
                    Width = cislovelikost,
                    Height = cislovelikost,
                    Fill = Brushes.Black
                };
                Canvas.SetTop(tecky[i], y + pozice[i].Y - cislovelikost / 2);
                Canvas.SetLeft(tecky[i], x + pozice[i].X - cislovelikost / 2);
                platno.Children.Add(tecky[i]);
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
                foreach (var k in kostka)
                {
                    k.Hod();
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

    public class kostky
    {
        private static Random random = new Random();
        public int hodnota { get; private set; }

        public kostky()
        {
            Hod();
        }

        public void Hod()
        {
            hodnota = random.Next(1, 7);
        }
    }
}
