using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        // Dirección inicial de la serpiente
        int direccion = 0; // 0-Derecha, 1-Izquierda, 2-Arriba, 3-Abajo

        // Inicialización de la serpiente
        int serpienteX = screenwidth / 2;
        int serpienteY = screenheight / 2;
        List<int> listaColaX = new List<int>();
        List<int> listaColaY = new List<int>();
        int longitudCola = 0;
        int puntaje = 0;

        // Posición inicial de la comida
        Random random = new Random();
        int comidaX = random.Next(1, screenwidth - 1);
        int comidaY = random.Next(1, screenheight - 1);

        // Configuración del juego
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Console.SetWindowSize(screenwidth, screenheight);
        Console.SetBufferSize(screenwidth, screenheight);

        while (true)
        {
            Console.Clear();

            // Dibujar el borde del juego
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            // Dibujar la comida
            Console.SetCursorPosition(comidaX, comidaY);
            Console.Write("■");

            // Movimiento de la serpiente
            int prevX = listaColaX.Count == 0 ? serpienteX : listaColaX.First();
            int prevY = listaColaY.Count == 0 ? serpienteY : listaColaY.First();

            listaColaX.Insert(0, serpienteX);
            listaColaY.Insert(0, serpienteY);

            if (listaColaX.Count > longitudCola)
            {
                listaColaX.RemoveAt(longitudCola);
                listaColaY.RemoveAt(longitudCola);
            }

            // Detectar entrada del teclado
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (direccion != 2)
                            direccion = 3;
                        break;
                    case ConsoleKey.DownArrow:
                        if (direccion != 3)
                            direccion = 2;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direccion != 0)
                            direccion = 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direccion != 1)
                            direccion = 0;
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }

            // Mover la serpiente
            switch (direccion)
            {
                case 0:
                    serpienteX++;
                    break;
                case 1:
                    serpienteX--;
                    break;
                case 2:
                    serpienteY++;
                    break;
                case 3:
                    serpienteY--;
                    break;
            }

            // Colisión con la pared o con la cola
            if (serpienteX <= 0 || serpienteX >= screenwidth - 1 ||
                serpienteY <= 0 || serpienteY >= screenheight - 1 ||
                listaColaX.Contains(serpienteX) && listaColaY.Contains(serpienteY))
            {
                Console.Clear();
                Console.SetCursorPosition(screenwidth / 2 - 10, screenheight / 2);
                Console.Write("¡Game Over! Puntaje: " + puntaje);
                return;
            }

            // Comer la comida
            if (serpienteX == comidaX && serpienteY == comidaY)
            {
                puntaje++;
                longitudCola++;
                comidaX = random.Next(1, screenwidth - 1);
                comidaY = random.Next(1, screenheight - 1);
            }

            // Dibujar la serpiente
            Console.SetCursorPosition(prevX, prevY);
            Console.Write(" ");
            foreach (var colaSegment in listaColaX.Zip(listaColaY, Tuple.Create))
            {
                Console.SetCursorPosition(colaSegment.Item1, colaSegment.Item2);
                Console.Write("■");
            }

            Console.SetCursorPosition(serpienteX, serpienteY);
            Console.Write("■");

            // Mostrar el puntaje
            Console.SetCursorPosition(1, 0);
            Console.Write("Puntaje: " + puntaje);

            // Velocidad del juego
            Thread.Sleep(100);
        }
    }
}
