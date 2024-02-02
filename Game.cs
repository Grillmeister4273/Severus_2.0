using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;

namespace Severus2
{
    internal class Game
    {
        
            public static int Width { get; } = 26;
            public static int Height { get; } = 26;

            ConsoleKeyInfo keyInfo;
            ConsoleKey consoleKey;

            Snake snake;
            Fruit fruit;

            public int Score { set; get; }
            bool IsLost;
            bool IsWin;

            public Game()
            {
                Console.CursorVisible = false;
                Console.Title = "Error 404, Brain not found";
                snake = new Snake();
                fruit = new Fruit();
            }

            void Restart()
            {
                Board.Write(Width, Height);
                Menu();


                keyInfo = new ConsoleKeyInfo();
                consoleKey = new ConsoleKey();

                IsLost = false;
                IsWin = false;

                snake.Restart();
                fruit.Restart();
                Board.Write(Width, Height);
            }

            void Control()
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    consoleKey = keyInfo.Key;
                }

                switch (consoleKey)
                {
                    case ConsoleKey.W:
                        if ((snake.Y[0] - snake.Y[1]) != 1) snake.Shift(Snake.Direction.Top);
                        break;
                    case ConsoleKey.S:
                        if ((snake.Y[0] - snake.Y[1]) != -1) snake.Shift(Snake.Direction.Bottom);
                        break;
                    case ConsoleKey.A:
                        if ((snake.X[0] - snake.X[1]) != 1) snake.Shift(Snake.Direction.Left);
                        break;
                    case ConsoleKey.D:
                        if ((snake.X[0] - snake.X[1]) != -1) snake.Shift(Snake.Direction.Right);
                        break;
                    default:
                        if ((snake.Y[0] - snake.Y[1]) == 1) snake.Shift(Snake.Direction.Bottom);
                        else if ((snake.Y[0] - snake.Y[1]) == -1) snake.Shift(Snake.Direction.Top);
                        else if ((snake.X[0] - snake.X[1]) == 1) snake.Shift(Snake.Direction.Right);
                        else if ((snake.X[0] - snake.X[1]) == -1) snake.Shift(Snake.Direction.Left);
                        break;
                }
            }


            void Logic()
            {

                Control();
                fruit.Logic(ref snake);
                snake.Logic(ref IsLost, ref IsWin);
            }

            void Menu()
            {
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                ConsoleKey consoleKey = new ConsoleKey();
                Console.SetCursorPosition(Width / 2 - 12, 1);
                Console.Write("Error 404, Brain not found");
                Console.SetCursorPosition(Width / 2 - 11, 2);
                Console.Write("Press 1 to start new game");
                Console.SetCursorPosition(Width / 2 - 11, 3);
                Console.Write("Press Esc to quit a game");

                while (true)
                {
                    keyInfo = Console.ReadKey(true);
                    consoleKey = keyInfo.Key;
                    switch (consoleKey)
                    {
                        case ConsoleKey.D1:
                            return;
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
            }


            public void Play()
            {
                while (true)
                {
                    Restart();
                    while (IsLost == false && IsWin == false)
                    {
                        Logic();
                        Thread.Sleep(100);
                        Console.Clear(); //################################
                        Board.Write(Width, Height);
                    }
                    if (IsWin == true)
                    {
                        Console.SetCursorPosition(Height / 2 - 4, Width / 2);
                        Console.Write("You win!!!");
                    }
                    else if (IsLost == true)
                    {
                        Console.SetCursorPosition(Height / 2 - 9, Width / 2);
                        Console.Write("Try better next time");
                    }
                }
            }
        }
    }

