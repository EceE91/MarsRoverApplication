using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApplication
{
    public class MarsRover
    {
        public int XCoordinate { get; set; }
        public int YCooordinate { get; set; }
        public char NewDirection { get; set; } // N, S, W,E

        Dictionary<Tuple<char,char>, char> directionCommandList = new Dictionary<Tuple<char, char>, char>();

        public MarsRover(int Xpos, int Ypos, char newDir)
        {
            XCoordinate = Xpos;
            YCooordinate = Ypos;
            NewDirection = newDir;

            // initialize matching
            directionCommandList.Add(new Tuple<char,char>('N','R' ), 'E');
            directionCommandList.Add(new Tuple<char, char>('N', 'L' ), 'W');
            directionCommandList.Add(new Tuple<char, char>('N', 'M'), 'N');

            directionCommandList.Add(new Tuple<char, char>('E','R'), 'S');
            directionCommandList.Add(new Tuple<char, char>('E', 'L'), 'N');
            directionCommandList.Add(new Tuple<char, char>('E', 'M'), 'E');

            directionCommandList.Add(new Tuple<char, char>('S', 'R'), 'W');
            directionCommandList.Add(new Tuple<char, char>('S','L' ), 'E');
            directionCommandList.Add(new Tuple<char, char>('S', 'M'), 'S');

            directionCommandList.Add(new Tuple<char, char>('W','R' ), 'N');
            directionCommandList.Add(new Tuple<char, char>('W', 'L' ), 'S');
            directionCommandList.Add(new Tuple<char, char>('W', 'M'), 'W');
        }

       
        public void CalculateCurrentPositionAndDirectionOnBoard(char[] commandsList, int[,] board)
        {
            foreach (var command in commandsList)
            {                
                if (command == 'M') // M move
                {
                    switch (NewDirection)
                    {
                        case 'N':
                                YCooordinate += 1;
                            break;
                        case 'S':
                                YCooordinate -= 1;
                            break;
                        case 'E':
                                XCoordinate += 1;
                            break;
                        case 'W':
                                XCoordinate -= 1;
                            break;
                    }

                    if (XCoordinate < 0 || YCooordinate < 0 || XCoordinate > board.GetLength(0) || YCooordinate > board.GetLength(1))
                        throw new System.InvalidOperationException("Rover cannot move");
                }
                else
                {
                    // change direction 
                    var dirCommandMatch = new Tuple<char, char>(NewDirection, command);
                    NewDirection = directionCommandList[dirCommandMatch];
                }
            }
        }
    }
}
