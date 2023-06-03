using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

using PZ6;
using System.Threading.Tasks;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace TicTacToe
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Game game;
        private int oWins;
        private int xWins;
        public MainPage()
        {
            this.InitializeComponent();
            Stats.Text = $"Wins:\n x: {xWins}\no: {oWins}";
            xWins = 0;
            oWins = 0;
            game = new Game();
        }

        private void Cell_Click(object sender, RoutedEventArgs args)
        {
            Button button = sender as Button;

            int[] result = ParseXY(button.Name.ToString());

            if (game.GameOver) {
                Info.Text = $"Game over! Winner = {GetSymbol(game.Value)}";
                if(game.Value == 1)
                {
                    xWins++;
                }
                else
                {
                    oWins++;
                }
                return;
            }

            if (!game.SetValue(result[0], result[1]))
            {
                return;
            }

            Render();

            if (game.GameOver = game.GameOverCheck())
            {
                Info.Text = $"Game over! Winner = {GetSymbol(game.Value)}";
                if (game.Value == 1)
                {
                    xWins++;
                }
                else
                {
                    oWins++;
                }
                return;
            }
            
            game.SwitchQueue();
        }

        private void Restart_Click(object sender, RoutedEventArgs args)
        {
            Info.Text = "";
            game.RestartGame();
            Render();
        }

        private void Render()
        {
            cell00.Content = $"{GetSymbol(game.Data[0][0])}";
            cell01.Content = $"{GetSymbol(game.Data[0][1])}";
            cell02.Content = $"{GetSymbol(game.Data[0][2])}";
            cell10.Content = $"{GetSymbol(game.Data[1][0])}";
            cell11.Content = $"{GetSymbol(game.Data[1][1])}";
            cell12.Content = $"{GetSymbol(game.Data[1][2])}";
            cell20.Content = $"{GetSymbol(game.Data[2][0])}";
            cell21.Content = $"{GetSymbol(game.Data[2][1])}";
            cell22.Content = $"{GetSymbol(game.Data[2][2])}";
            Stats.Text = $"Wins:\n x: {xWins}\no: {oWins}";
        }

        private int[] ParseXY(string button_name)
        {

            int[] result = new int[2];
            string values = button_name.Replace("cell", "");
            result[0] = int.Parse(values[0].ToString());
            result[1] = int.Parse(values[1].ToString());
            return result;
        }

        private char GetSymbol(sbyte value) {
            switch (value) {
                
                case 1:
                    return 'x';
                case -1:
                    return 'o';
                default:
                    return (char)0;
            }
        }
    }
}
