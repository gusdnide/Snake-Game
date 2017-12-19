using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        static eDirecao Direcao;
        static List<Cell> Corpo;
        static Cell Cabeca;
        static Cell Fruta;
        static int Width;
        static int Heigth;
        static Random Aleatorio;
        static int Pontos;
        static bool GameOver;
        static void Main(string[] args)
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 50;
            Setup();                
            new Thread(tJogo).Start();
            new Thread(tTeclas).Start();
            
        }
        static void Setup()
        {
            Direcao = eDirecao.Direita;
            Heigth =25;
            Width = 25;

            GameOver = false;
            Pontos = 3;
            Aleatorio = new Random();
            Cabeca = new Cell(ConsoleColor.Green, (Width / 2), (Heigth / 2));
            Fruta = new Cell(ConsoleColor.Red, Aleatorio.Next(1, Width), Aleatorio.Next(1, Heigth));
            Corpo = new List<Cell>();
        }
        static void DesenharBorda( )
        {
            
            for(int i = 0; i <= Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("*");
                Console.SetCursorPosition(i, Heigth);
                Console.Write("*");
            }
            for (int i = 0; i <= Heigth; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("*");
                Console.SetCursorPosition(Width, i);
                Console.Write("*");
            }
        }
        
        static void tJogo()
        {
            while (!GameOver)
            {
                Console.Clear();
                Console.Title = $"SnakeGusd {Pontos} pontos.";
                DesenharBorda();
                Cabeca.Desenhar();
                Fruta.Desenhar();
                if(Fruta.Posicao == Cabeca.Posicao)
                {                   
                    Fruta = new Cell(ConsoleColor.Red, Aleatorio.Next(1, Width), Aleatorio.Next(1, Heigth));
                    Pontos++;
                }

                if ((Cabeca.Posicao.x == 0 || Cabeca.Posicao.y == 0) || (Cabeca.Posicao.x == (Width ) || Cabeca.Posicao.y == (Heigth )))
                    GameOver = true;

                foreach(Cell c in Corpo)
                {
                    if (Cabeca.Posicao == c.Posicao)
                        GameOver = true;
                    c.Desenhar();
                }
                
                Corpo.Add(new Cell(ConsoleColor.Gray, Cabeca.Posicao.x, Cabeca.Posicao.y));

                if (Corpo.Count > Pontos)
                    Corpo.RemoveAt(0);

                switch (Direcao)
                {
                    case eDirecao.Baixo:
                        Cabeca.Posicao.y++;
                        break;
                    case eDirecao.Cima:
                        Cabeca.Posicao.y--;
                        break;
                    case eDirecao.Direita:
                        Cabeca.Posicao.x++;
                        break;
                    case eDirecao.Esquerda:
                        Cabeca.Posicao.x--;
                        break;
                    default:
                        break;
                }
            
                Thread.Sleep(100);
            }

          
        }
        static void tTeclas()
        {
            while (!GameOver)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if(Direcao != eDirecao.Baixo)
                            Direcao = eDirecao.Cima;
                        break;
                    case ConsoleKey.DownArrow:
                        if (Direcao != eDirecao.Cima)
                            Direcao = eDirecao.Baixo;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Direcao != eDirecao.Direita)
                            Direcao = eDirecao.Esquerda;
                        break;
                    case ConsoleKey.RightArrow:
                        if (Direcao != eDirecao.Esquerda)
                            Direcao = eDirecao.Direita;
                        break;
                    default:
                        break;
                }
            }
            
        }
        
    }
    enum eDirecao
    {
        Cima,
        Baixo,
        Esquerda,
        Direita
    }
    class Vec2
    {
        public int x { get; set; }
        public int y { get; set; }
        public Vec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x + v2.x, v2.y + v1.y);
        }
        public static bool operator !=(Vec2 v1, Vec2 v2)
        {
            return !(v1.x == v2.x && v1.y == v2.y);
        }
    
        public static bool operator == (Vec2 v1, Vec2 v2)
        {
            return (v1.x == v2.x && v1.y == v2.y);
        }
        public static Vec2 operator -(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x - v2.x, v2.y - v1.y);
        }
    }
    class Cell
    {
        public Vec2 Posicao { get; set; }
        public ConsoleColor cor { get; set; }
        public Cell(ConsoleColor c, int X, int Y)
        {
            this.cor = c;
            Posicao = new Vec2(X, Y);
        }
        public void Desenhar()
        {
            Console.SetCursorPosition(Posicao.x, Posicao.y);
            Console.ForegroundColor = cor;
            Console.Write("■");
        }
    }
    
}
