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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int size = 8;
        private const int mine_count = 8;
        private bool[,] map;

        private int true_selected_count;
        private int selected_count;

        public MainWindow()
        {
            InitializeComponent();

            CreateCell();
        }

        private void Reset()
        {
            true_selected_count = 0;
            selected_count = 0;

            ClearCell();
            CreateCell();
        }

        private void CreateMine()
        {
            var rand = new Random();

            map = new bool[size, size];

            var count = 0;
            while (count != mine_count)
            {
                var x = rand.Next(size);
                var y = rand.Next(size);

                if (map[x, y] == false)
                {
                    map[x, y] = true;
                    count++;
                }
            }
        }

        private void CreateCell()
        {
            for (int i = 0; i < size; i++)
            {
                game_map.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < size; j++)
            {
                game_map.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var btn = new CellControl(i, j);
                    btn.OnOpenCell += OnOpenCell;
                    btn.OnSelectCell += OnSelectCell;
                    btn.OnUnselectCell += OnUnselectCell;

                    game_map.Children.Add(btn);

                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                }
            }

            CreateMine();
        }

        private void OnSelectCell(CellControl sender)
        {
            if (map[sender.X, sender.Y])
                true_selected_count++;

            selected_count++;
            CheckWin();
        }

        private void OnUnselectCell(CellControl sender)
        {
            if (map[sender.X, sender.Y])
                true_selected_count--;

            selected_count--;
            CheckWin();
        }

        private void OnOpenCell(CellControl sender)
        {
            if (map[sender.X, sender.Y])
            {
                MessageBox.Show("Mine!");

                Reset();
                return;
            }

            int count = 0;
            ForeachCell(sender, (x, y) =>
            {
                if (map[x, y])
                    count++;
            });

            sender.SetMineCount(count);

            if (count == 0)
            {
                ForeachCell(sender, (x, y) => FindCell(x, y).OpenCell());
            }
        }

        private void CheckWin()
        {
            if (selected_count == mine_count && selected_count == true_selected_count)
            {
                MessageBox.Show("You win!");
                Reset();
            }
        }

        private void ForeachCell(CellControl CenterCell, Action<int, int> Action)
        {
            for (int x = CenterCell.X - 1; x <= CenterCell.X + 1; x++)
            {
                for (int y = CenterCell.Y - 1; y <= CenterCell.Y + 1; y++)
                {
                    if (x < 0 || y < 0 || x >= size || y >= size)
                        continue;

                    Action.Invoke(x, y);
                }
            }
        }

        private CellControl FindCell(int x, int y)
        {
            foreach (CellControl cell in game_map.Children)
            {
                if (cell.X == x && cell.Y == y)
                    return cell;
            }

            return null;
        }

        private void ClearCell()
        {
            game_map.RowDefinitions.Clear();
            game_map.ColumnDefinitions.Clear();

            game_map.Children.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
