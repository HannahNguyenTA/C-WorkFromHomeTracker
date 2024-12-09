

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Transactions;


namespace WorkFromHomeTracker
{
    class MainProgram
    {
        static string fileName = "data.csv";
        public static void Main()
        {
            // Loop forever
            while (true)
            {
                StartOfProgram();
            }
        }

        public static void StartOfProgram()
        {
            // Make some sort of menu
            Console.WriteLine("*****Work From Home Tracker*****");
            Console.WriteLine("=========================");
            Console.WriteLine("[1] Enter Daily Hours Worked.");
            Console.WriteLine("[2] Produce Hours Worked Report.");
            Console.WriteLine("[3] Close Program...");
            Console.WriteLine("=========================");

            // Take an option from the user
            Console.Write("Select an option: ");
            int selection = int.Parse(Console.ReadLine());

            // Enter Hours Worked
            if (selection == 1)
            {
                EnterData();
            }
            // Read Hours from File
            else if (selection == 2)
            {
                ReadData();
            }
            // Close System
            else if (selection == 3)
            {
                Console.Write("You exit program");
                ExitProgram();

            }
            // Invalid Selection
            else
            {
                Console.WriteLine("Invalid Selection");
            }
        }

        // This method will exit the program
        static void ExitProgram()
        {
            Console.Write("You exit the program. Bye");
            Environment.Exit(0);

        }

        // Create an method that is called EnterData
        static void EnterData()
        {
            string[] employeeName = new string[7];
            string[] employeeId = new string[7];

            // Set a week Number
            Console.Write("Please enter the current week: ");
            int weekNumber = int.Parse(Console.ReadLine());

            // Create an array to store each employee overall hours
            int[] overallHours = new int[7];

            // Make a loop to get details of 7 employees
            for (int i = 0; i < 7; i++)
            {
                // Enter employee name
                Console.Write($"Enter employee's name: ");
                employeeName[i] = Console.ReadLine();

                // Enter employee ID
                Console.Write($"Enter {employeeName[i]}'s id: ");
                employeeId[i] = Console.ReadLine();

                // Enter hours worked for each day
                List<int> hoursWorked = new List<int>();
                string[] days = new string[5] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                foreach (string day in days)
                {
                    int hours;

                    while (true)
                    {
                        Console.Write($"Enter hours worked for {day}: ");

                        if (int.TryParse(Console.ReadLine(), out hours))
                        {
                            /* if hours > 24, or less than zero, give an error message
                               else add hours to hoursWorked */

                            if ((hours > 24) || (hours < 0))
                                Console.WriteLine("Invalid number. Please enter a valid number: ");
                            else
                            {

                                hoursWorked.Add(hours);
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number of hours.");
                        }
                    }
                }


                Console.WriteLine("--------------------------");
                Console.WriteLine($"Summary for emloyee {i + 1}: ");

                // Create a Int Data Structure
                int[] hourDataStruct = new int[5];

                // Calculate all of the hours - expression
                int hourSum = 0;

                // Add our values to the Structure
                for (int j = 0; j < 5; j++)

                {

                    if (hoursWorked[j] > 10)
                    {
                        Console.WriteLine("Too many hours worked on " + days[j]);
                    }

                    else if (hoursWorked[j] < 4)
                    {
                        Console.WriteLine("Insufficient hours worked on " + days[j]);
                    }

                    else if (hoursWorked[j] > 24)
                    {
                        Console.WriteLine("Invalid number. Please input a number between 0 and 24");
                    }

                    else if (hoursWorked[j] < 0)
                    {
                        Console.WriteLine("Invalid number. Please input a number between 0 and 24");
                    }

                    hourSum = hourSum + hoursWorked[j];

                } // End of [j] loop

                // Create our data string
                string dataString = $"{weekNumber},{employeeId[i]},{employeeName[i]}, {hoursWorked[0]}," +
                    $"{hoursWorked[1]},{hoursWorked[2]},{hoursWorked[3]},{hoursWorked[4]}, {hourSum}\n";

                // Now write to file
                File.AppendAllText(fileName, dataString);

                Console.WriteLine($"Total working hours for week {weekNumber}:" + hourSum);
                // Check if the hour overall for the week was good
                if (hourSum > 40)
                {
                    Console.WriteLine("You are working too hard!");
                }

                // Check if the hour was bad
                if (hourSum < 30)
                {
                    Console.WriteLine("You did not do enough work this week.");
                }

                // Add to overallHours
                overallHours[i] = hourSum;

            }// End of [i] loop

            // Create a loop for report
            int under30 = 0;
            int over40 = 0;
            int between37and39 = 0;

            // Now Loop
            foreach (int hours in overallHours)
            {
                // Check if hours are under 30
                if (hours < 30)
                {
                    under30 += 1;
                }
                // Check if hourss are over 40
                if (hours > 40)
                {
                    over40 += 1;
                }
                // Check if hours are between 37 and 39
                if (hours > 37 && hours < 39)
                {
                    between37and39 += 1;
                }
            }
            // Display our final report
            Console.WriteLine("------------ Report ------------");
            Console.WriteLine($"Employees who worked less than 30 hours : {under30}");
            Console.WriteLine($"Employees who worked more than 40 hours : {over40}");
            Console.WriteLine($"Employees who worked between 37 and 39 hours : {between37and39}");

        }


        /* Read Data Method */
        static void ReadData()
        {
            // Welcome Message
            Console.WriteLine("You have now entered the Read Data...");

            // Ask for the recent records
            Console.WriteLine("How many records do you want to load?");
            int count = int.Parse(Console.ReadLine());

            // Load all of the file into this list
            List<string> lines = new List<string>(File.ReadAllLines(fileName));

            // Reverse the List
            lines.Reverse();
            Console.WriteLine("------------------------");
            Console.WriteLine($"Employee weekly report:");
            // Check if number is okay
            if (count > lines.Count())
            {
                count = lines.Count();
            }

            // Loop through our latest
            for (int i = 0; i < count; i++)
            {
                // Create a Data Structure which holds all of our data
                string[] lineData = lines[i].Split(",");
                Console.WriteLine($"Employee {lineData[2]} submitted on Week {lineData[0]} and had total hours: {lineData[8]}");
            }
        }
    }
}

