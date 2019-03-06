using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] directions = { 'N', 'S', 'E','W' };
            char[] commands = { 'L', 'R', 'M'};
            int[,] board;

            Console.WriteLine("Enter space size by leaving space between width and height (e.g: 5 5)");
            string rectangleSize = Console.ReadLine().TrimEnd();
            string[] splitedSizes = rectangleSize.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitedSizes.Length == 2)
            {
                int rows;
                int cols;
                bool resInput1 = int.TryParse(splitedSizes[0], out rows);
                bool resInput2 = int.TryParse(splitedSizes[1], out cols);
                if (!resInput1 || !resInput2)
                {
                    throw new Exception("Size is not a number!");
                }
                Console.WriteLine("\nSpace size is " + splitedSizes[0] + 'x' + splitedSizes[1]);

                board = new int[rows, cols]; // create board

                var count = 1;
                /////// Rover Part
                while (count <= 2)
                {
                    Console.WriteLine("\n\nEnter starting point and starting direction of the "+ count+". rover (e.g: 1 2 N)");
                    string startingValuesOfRover = Console.ReadLine().TrimEnd();
                    string[] splitedStartingValuesOfRover = startingValuesOfRover.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitedStartingValuesOfRover.Length == 3)
                    {
                        int startingPointXOfRover;
                        int startingPointYOfRover;
                        bool resInputs1 = int.TryParse(splitedStartingValuesOfRover[0], out startingPointXOfRover);
                        bool resInputs2 = int.TryParse(splitedStartingValuesOfRover[1], out startingPointYOfRover);
                        if (!resInputs1 || !resInputs2)
                        {
                            throw new Exception("Starting points must be integers!");
                        }

                        char startingDirectionOfRover;
                        bool charResForRover = char.TryParse(splitedStartingValuesOfRover[2].ToUpper(), out startingDirectionOfRover);
                        if (charResForRover)
                        {
                            int pos = Array.IndexOf(directions, startingDirectionOfRover);
                            if (pos > -1) // char array contains direction input 
                            {
                                Console.WriteLine("\nStarting points of the "+ count +". rover are (" + splitedStartingValuesOfRover[0] + "," + splitedStartingValuesOfRover[1] + ") " + splitedStartingValuesOfRover[2].ToUpper());
                                Console.WriteLine("\nEnter Directions of the "+ count +". rover (e.g LMLMLMLMM)");
                                string commandsStr = Console.ReadLine().TrimStart().TrimEnd();
                                if (!string.IsNullOrEmpty(commandsStr))
                                {
                                    char[] commandsList = commandsStr.ToUpper().ToCharArray();
                                    bool isCommandExits = true;
                                    foreach (var item in commandsList)
                                    {
                                        int position = Array.IndexOf(commands, item);

                                        //listede var mı
                                        if (position <= -1)
                                        {
                                            // listede command yok
                                            isCommandExits = false;
                                            Console.WriteLine("Invalid Command! Commands needs to be L,R or M");
                                            break;
                                        }
                                    }

                                    if (isCommandExits)
                                    {
                                        // Create rover
                                        MarsRover rover = new MarsRover(startingPointXOfRover, 
                                            startingPointYOfRover,                                              
                                            startingDirectionOfRover);
                                        rover.CalculateCurrentPositionAndDirectionOnBoard(commandsList, board);

                                        Console.WriteLine("Current Position of the " + count + ". rover:"
                                            + rover.XCoordinate + " "
                                            + rover.YCooordinate + " "
                                            + rover.NewDirection);

                                        count++;
                                    }
                                }
                                else
                                {
                                    // string boş olamaz
                                    throw new Exception("Directions cannot be empty");
                                }
                            }
                        }
                        else
                        {
                            // char değer giriniz
                            throw new Exception("Direction has to be a char value");
                        }
                    }
                    else
                    {
                        // 3 tane değer giriniz
                        throw new Exception("There has to be 3 values");
                    }
                }
            }
            else
            {
                // 2 tane değer giriniz
                throw new Exception("There has to be 2 integer values");
            }

            Console.ReadKey();
        }
    }
}
