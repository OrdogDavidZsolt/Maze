﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MazeManiac
{
    class Program
    {
        static void Main(string[] args)
        {
            MapHandler map1 = new MapHandler();
            bool gameIsRunning = true;
            map1.showMap();
            Console.WriteLine("Üdvözöllek a világ legjobb játékában");
            while (gameIsRunning)
            {
                char command = Console.ReadKey(true).KeyChar;
                Console.Clear();
                switch (command)
                {
                    case 'w': map1.up(); break;
                    case 's': map1.down(); break;
                    case 'a': map1.left(); break;
                    case 'd': map1.right(); break;
                    case 'f': Console.WriteLine(map1.whereAmI()[0] + "|" + map1.whereAmI()[1]); break;
                    default: Console.WriteLine("Nincs ilyen parancs!"); break;
                }
                map1.showMap();
                map1.showAndClearMessage();
            }
            Console.ReadKey();
        }
    }
    class MapHandler
    {
        private char[,] map;
        private string mapname;
        private string message;
        private int level=0;
        public MapHandler()
        {
            this.mapname = "Default";
            this.message = "";
            this.map = new char[,]
            {
                {'#','.','#','#','#'},
                {'#','@','.','.','#'},
                {'#','.','#','.','#'},
                {'#','.','#','.','x'},
                {'#','#','#','#','#'}
            };
        }

        public MapHandler(string filename) {
            this.mapname = filename;
            this.message = "";
            try
            {
                readNewMap(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Nem tudtam beolvasni a fájlt");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Üres a fájl");
            }
        }
        public String getName()
        {
            return mapname;
        }
        public char[,] getMap()
        {
            return map;
        }
        public void showMap()
        {
            int meret = this.map.GetLength(0);
            for (int row = 0; row < meret; row++)
            {
                for (int col = 0; col < meret; col++)
                {
                    Console.Write(this.map[row, col] + " ");
                    /*switch (this.map[row, col]) {
                        case '.': Console.Write('.');break;
                        case '#': Console.Write('#');break;
                        case '@': Console.Write('@');break;
                        case 'x': Console.Write('x');break;
                        default: Console.Write('?');break;
                    }*/
                }
                Console.WriteLine();
            }
        }
        public void showAndClearMessage() {
            Console.WriteLine(this.message);
            this.message = "";
        }
        public int[] whereAmI()
        {
            int[] pos = { 0, 0 };
            int meret = this.map.GetLength(0);
            for (int row = 0; row < meret; row++)
            {
                for (int col = 0; col < meret; col++)
                {
                    if (this.map[row, col] == '@')
                    {
                        pos[0] = row;
                        pos[1] = col;
                    }
                }
            }
            return pos;
        }
        private void Move(int xmod, int ymod) {
            int[] pos = whereAmI();
            int x = pos[0];
            int y = pos[1];
            try
            {
                if (map[x + xmod, y + ymod] == '.')
                {
                    map[x + xmod, y + ymod] = '@';
                    map[x, y] = '.';
                }
                else if(map[x + xmod, y + ymod] == 'x')
                {
                    lvlUp();
                }
                else
                {
                    this.message += "Falba ütköztél";
                }
            }
            catch (IndexOutOfRangeException e)
            {
                this.message += "Elérted a pálya szélét";
            }
        }
        private void lvlUp() {
            level++;
            readNewMap(""+level+".txt");
        }

        private void readNewMap(string filename) {
            string[] lines = File.ReadAllLines(filename);
            int width = lines[0].Length;
            int height = lines.Length;
            char[,] mapFromFile = new char[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mapFromFile[i, j] = lines[i][j];
                }
            }
            this.map = mapFromFile;
        }
        public void up() {
            Move(-1, 0);
        }
        public void down() {
            Move(1, 0);
        }
        public void right() {
            Move(0, 1);
        }
        public void left() {
            Move(0, -1);
        }
    }
}

