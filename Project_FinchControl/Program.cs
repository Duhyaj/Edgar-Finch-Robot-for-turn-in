using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;
using System.Linq;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 1/22/2020
    // Last Modified: 1/25/2020
    //
    // **************************************************
    public enum Command
    {
        //NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        SPINEDGAR,
        DONE
    }
    class Program

    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        #region CONSOLE THEME
        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Show robot connect with lights sound and movement
        /// </summary>
        /// <param name="edgar"></param>
        /// 
        static void ShowEdgarConnect(Finch edgar)
        {
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 300, 300, 0, 255, 0, 255, -255);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 300, 300, 255, 0, 0, -255, 255);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 300, 300, 0, 0, 255, 0, 0);
        }

        /// <summary>
        /// Set Cursor position
        /// </summary>
        /// <param name="l"></param>
        /// <param name="t"></param>
        /// <param name="label"></param> 
        static void SetCursorPosition(int l, int t, string label = "")
        {
            Console.SetCursorPosition(l, t);
            Console.WriteLine(label);
        }

        /// <summary>
        /// Display a  Main Menu
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch edgar = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(edgar);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(edgar);
                        break;

                    case "c":
                        DataRecorderDisplayMenu(edgar);
                        break;

                    case "d":
                        AlarmSystemDisplayAlarmMenu(edgar);
                        break;

                    case "e":
                        UserProgrammingDisplayUserProgrammingMenu(edgar);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(edgar);
                        break;

                    case "q":
                        edgar.disConnect();
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }
        #endregion

        #region USER PROGRAMMING

        /// <summary>
        /// User Programming Menu
        /// </summary>
        /// <param name="edgar"></param>
        static void UserProgrammingDisplayUserProgrammingMenu(Finch edgar)
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters = (0, 0, 0);
            List<Command> commands = new List<Command>();

            //List<(Command command, int duration) > = new List<(Command command, int duration)>;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        commands = UserProgrammingDisplayGetCommands();
                        break;

                    case "c":
                        UserProgrammingDisplayViewCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteCommands(edgar, commands, commandParameters);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(edgar);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(edgar);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);

        }

        /// <summary>
        /// Make Edgar Spin
        /// </summary>
        /// <param name="edgar"></param>
        static void UserProgrammingSpinEdgar(Finch edgar)
        { 
            int note = 1056;
            int r = 0;
            int g = 255;
            int b = 255;
            int p = 255;
            int s = -255;
            int noteWait = 2;
            int afterWait = 3;

            edgar.noteOn(note);
            edgar.setLED(r, g, b);
            edgar.setMotors(p, s);
            edgar.wait(noteWait);
            edgar.noteOff();
            edgar.setLED(0, 0, 0);
            edgar.setMotors(0, 0);
            edgar.wait(afterWait);
        }

        /// <summary>
        /// Display Commands as they execute
        /// </summary>
        /// <param name="edgar"></param>
        /// <param name="commands"></param>
        /// <param name="commandParameters"></param>
        static void UserProgrammingDisplayExecuteCommands
            (Finch edgar, 
            List<Command> commands, 
            (int motorSpeed, 
            int ledBrightness, 
            double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            double waitSeconds = (int)commandParameters.waitSeconds;            

            DisplayScreenHeader("Execute Commands");

            Console.WriteLine("\tEdgar will now execute all commands");

            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {

                    case Command.MOVEFORWARD:

                        edgar.setMotors(motorSpeed, motorSpeed);

                        break;
                    case Command.MOVEBACKWARD:

                        edgar.setMotors(-motorSpeed, -motorSpeed);

                        break;

                    case Command.STOPMOTORS:

                        edgar.setMotors(0, 0);

                        break;

                    case Command.WAIT:

                        int waitMilliseconds = (int)waitSeconds * 1000;
                        edgar.wait(waitMilliseconds);

                        break;

                    case Command.TURNRIGHT:

                        edgar.setMotors(motorSpeed, -255);

                        break;

                    case Command.TURNLEFT:

                        edgar.setMotors(-255, motorSpeed);

                        break;

                    case Command.LEDON:

                        edgar.setLED(ledBrightness, ledBrightness, ledBrightness);

                        break;

                    case Command.LEDOFF:

                        edgar.setLED(0, 0, 0);

                        break;

                    case Command.GETTEMPERATURE:

                        Console.WriteLine($"\tThe temperature is {edgar.getTemperature().ToString("n2")}\n");

                        break;

                    case Command.SPINEDGAR:

                        UserProgrammingSpinEdgar(edgar);

                        break;

                    case Command.DONE:

                        edgar.setMotors(0, 0);
                        edgar.setLED(0, 0, 0);

                        break;

                    default:

                        Console.WriteLine();
                        Console.WriteLine("\tUnknown Command Error");

                        break;
                }
                Console.WriteLine($"\tCommand: {command}");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// View Commands input by user
        /// </summary>
        /// <param name="commands"></param>
        static void UserProgrammingDisplayViewCommands(List<Command> commands)
        {
            DisplayScreenHeader("View Commands");

            int commandCount = 1;

            Console.WriteLine("\tCommand List");
            Console.WriteLine("\t------------");

            foreach (Command command in commands)
            {
                Console.Write($"\t{command}");
                if (commandCount % 1 == 0) Console.Write("\n");                
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get command from user
        /// </summary>
        /// <returns></returns>
        static List<Command> UserProgrammingDisplayGetCommands()
        {
            List<Command> commands = new List<Command>();
            bool isDone = false;
            string userResponse;
            int numberShown = 0;

            DisplayScreenHeader("User Commands");
            Console.WriteLine("\tPlease enter from the following list");
            Console.WriteLine("\n\t");
            foreach (string commandName in Enum.GetNames(typeof(Command))) 
            {
                if (numberShown == 0)
                {
                    Console.Write("\t");
                }

                else if (numberShown > 6)
                {
                    numberShown = 0;
                    Console.WriteLine();
                    Console.Write("\t");
                }

                numberShown++;
                Console.Write(commandName + " | ");
            }

            Console.WriteLine("\n");

                do
            {

                Console.Write("\tCommand: ");
                userResponse = Console.ReadLine();

                if (userResponse != "done")

                {
                    if(Enum.TryParse(userResponse.ToUpper(), out Command command))
                    {

                        commands.Add(command);

                    }
                    else
                    {
                        Console.WriteLine("\tPlease enter a listed command");
                    }
                }

                else
                {
                    isDone = true;
                }   

            } while (!isDone);

            DisplayContinuePrompt();

            return commands;
        }

        /// <summary>
        /// Get command parameters from user
        /// </summary>
        /// <returns></returns>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;

            DisplayScreenHeader("Command Parameters");

            commandParameters.motorSpeed = ValidateIntInput
                ("Motor Speed", 
                "Enter Motor Speed: ", 
                "Please enter a number [1-255]",
                "for Motor Speed");

            commandParameters.ledBrightness = ValidateIntInput
                ("LED Brightness", 
                "Enter Brightness: ", 
                "Please enter a number between [1-255]",
                "for LED Brightness");

            commandParameters.waitSeconds = ValidateIntInput
                ("Wait Time", 
                "Enter Wait Time in Seconds: ", 
                "Please enter a number [1-255]",
                "Seconds");

            return commandParameters;
        }

        #endregion

        //#region Alarm System Temperature

        ///// <summary>
        ///// Main Alarm menu
        ///// </summary>
        ///// <param name="edgar"></param>
        //static void AlarmSystemDisplayTempAlarmMenu(Finch edgar)
        //{
        //    Console.CursorVisible = true;

        //    bool quitApplication = false;
        //    string menuChoice;
        //    string sensorsToMonitor = "";
        //    double numberOfDataPoints = 0;
        //    int minMaxThreshholdValue = 0;
        //    int timeToMonitor = 0;

        //    do
        //    {
        //        DisplayScreenHeader("Main Menu");

        //        //
        //        // get user menu choice
        //        //
        //        Console.WriteLine("\ta) Set Temperature Data Points");
        //        Console.WriteLine("\tb) Set range type");
        //        Console.WriteLine("\tc) Set Min/Max Threshold Value");
        //        Console.WriteLine("\td) Set time to Monitor");
        //        Console.WriteLine("\te) Set alarm");
        //        Console.WriteLine("\tf) Disconnect Finch Robot");
        //        Console.WriteLine("\tq) Quit");
        //        Console.Write("\t\tEnter Choice:");
        //        menuChoice = Console.ReadLine().ToLower();

        //        //
        //        // process user menu choice
        //        //
        //        switch (menuChoice)
        //        {
        //            case "a":

        //                numberOfDataPoints = AlarmDisplayGetNumberOfTempDataPoints(numberOfDataPoints, edgar);

        //                break;

        //            case "b":

        //                timeToMonitor = AlarmSystemDisplayTempTimeToMonitor();

        //                break;

        //            case "c":

        //                minMaxThreshholdValue = AlarmSystemDisplayTempthreshholdValue(edgar);

        //                break;

        //            case "d":

        //                AlarmSystemSetTempAlarm(edgar, sensorsToMonitor, minMaxThreshholdValue, timeToMonitor);

        //                break;

        //            case "e":

        //                break;

        //            case "f":
        //                DisplayDisconnectFinchRobot(edgar);
        //                break;

        //            case "q":
        //                DisplayDisconnectFinchRobot(edgar);
        //                quitApplication = true;
        //                break;

        //            default:
        //                Console.WriteLine();
        //                Console.WriteLine("\tPlease enter a letter for the menu choice.");
        //                DisplayContinuePrompt();
        //                break;
        //        }

        //    } while (!quitApplication);

        //}

        //static int AlarmDisplayGetNumberOfTempDataPoints(double numberOfDataPoints, Finch edgar)
        //{
        //    double numberOfDataPoints;
        //    string userResponse;
        //    bool validResponse = true;

        //    DisplayScreenHeader("Number of Data Points");

        //    do
        //    {
        //        Console.Write("\tEnter the desired number of measurements: ");
        //        userResponse = Console.ReadLine();
        //        validResponse = double.TryParse(userResponse, out numberOfDataPoints);

        //        if (!validResponse)
        //        {
        //            Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
        //            Console.WriteLine();
        //        }

        //    } while (!validResponse);

        //    Console.WriteLine($"\tYou chose {numberOfDataPoints} as the number of data points.");
        //    DisplayContinuePrompt();

        //    return (int)numberOfDataPoints;

        //}

        ///// <summary>
        ///// Getting frequency of readings
        ///// </summary>
        ///// <returns></returns>
        //static int AlarmSystemDisplayTempTimeToMonitor()
        //{
        //    DisplayScreenHeader("Time to monitor");
        //    {
        //        string userResponse;
        //        bool validResponse = true;
        //        int timeToMonitor = 0;

        //        do
        //        {
        //            Console.Write("\tEnter the desired frequency of measurements: ");
        //            userResponse = Console.ReadLine();
        //            validResponse = int.TryParse(userResponse, out timeToMonitor);

        //            if (!validResponse)
        //            {
        //                Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
        //                Console.WriteLine();
        //            }

        //        } while (!validResponse);

        //        Console.WriteLine($"\tYou chose {timeToMonitor} as the frequency data readings.");
        //        DisplayContinuePrompt();

        //        return timeToMonitor;
        //    }
        //}

        ///// <summary>
        ///// Getting the Threshold value for alarm state
        ///// </summary>
        ///// <param name="sensorsToMonitor"></param>
        ///// <param name="edgar"></param>
        ///// <returns></returns>
        //static int AlarmSystemDisplayTempthreshholdValue(Finch edgar)
        //{
        //    int thresholdValue = 0;

        //    string userResponse;
        //    bool validResponse = true;

        //    DisplayScreenHeader("threshold Value");

        //    do
        //    {
        //        Console.Write("\tEnter the desired frequency of measurements: ");
        //        userResponse = Console.ReadLine();
        //        validResponse = int.TryParse(userResponse, out thresholdValue);

        //        if (!validResponse)
        //        {
        //            Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
        //            Console.WriteLine();
        //        }

        //    } while (!validResponse);

        //    return thresholdValue;
        //}

        ///// <summary>
        ///// Get user input for min max range type including min max and full words
        ///// </summary>
        ///// <returns></returns>

        //static string AlarmSystemDisplayTempRangeType()
        //{
        //    string rangeType = "";

        //    DisplayScreenHeader("Range Type");

        //    //
        //    // validate user input
        //    //

        //    rangeType = ValidateStringInput
        //        (
        //        "\tEnter range type [Minimum, Maximum]: ",
        //        "\tPlease use Minimum or Maximum",
        //        new string[] { "Minimum", "Maximum" }
        //        );

        //    Console.WriteLine();
        //    Console.WriteLine($"\tEdgar will now monitor until {rangeType} threshold is reached");
        //    Console.CursorVisible = false;

        //    DisplayMenuPrompt("Alarm Sytem");

        //    return rangeType;
        //}

        ///// <summary>
        ///// Setting the alarm to start and display results
        ///// </summary>
        ///// <param name="edgar">Finch Robot</param>
        ///// <param name="sensorsToMonitor">which sensors were chosen</param>
        ///// <param name="rangeType">min or max</param>
        ///// <param name="minMaxThreshholdValue">threshold choice</param>
        ///// <param name="timeToMonitor">timeToMonitor</param>

        //#endregion

        #region Alarm System Lighting meter

        /// <summary>
        /// Main Alarm menu
        /// </summary>
        /// <param name="edgar"></param>
        static void AlarmSystemDisplayAlarmMenu(Finch edgar)
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;
            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThreshholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set sensors to monitor");
                Console.WriteLine("\tb) Set range type");
                Console.WriteLine("\tc) Set Min/Max Threshold Value");
                Console.WriteLine("\td) Set time to Monitor");
                Console.WriteLine("\te) Set alarm");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmSystemDisplaySetSensors();
                        break;

                    case "b":
                        rangeType = AlarmSystemDisplayRangeType();
                        break;

                    case "c":
                        minMaxThreshholdValue = AlarmSystemDisplaythreshholdValue(sensorsToMonitor, edgar);
                        break;

                    case "d":
                        timeToMonitor = AlarmSystemDisplayTimeToMonitor();
                        break;

                    case "e":
                        AlarmSystemSetAlarm(edgar, sensorsToMonitor, rangeType, minMaxThreshholdValue, timeToMonitor);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(edgar);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(edgar);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);

        }

        /// <summary>
        /// Getting frequency of readings
        /// </summary>
        /// <returns></returns>
        static int AlarmSystemDisplayTimeToMonitor()
        {
            DisplayScreenHeader("Time to monitor");
            {
                string userResponse;
                bool validResponse = true;
                int timeToMonitor = 0;

                do
                {
                    Console.Write("\tEnter the desired frequency of measurements: ");
                    userResponse = Console.ReadLine();
                    validResponse = int.TryParse(userResponse, out timeToMonitor);

                    if (!validResponse)
                    {
                        Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
                        Console.WriteLine();
                    }

                } while (!validResponse);

                Console.WriteLine($"\tYou chose {timeToMonitor} as the frequency data readings.");
                DisplayContinuePrompt();

                return timeToMonitor;
            }
        }

        /// <summary>
        /// Getting the Threshold value for alarm state
        /// </summary>
        /// <param name="sensorsToMonitor"></param>
        /// <param name="edgar"></param>
        /// <returns></returns>
        static int AlarmSystemDisplaythreshholdValue(string sensorsToMonitor, Finch edgar)
        {
            int thresholdValue = 0;
            int currentLeftSensorValue = edgar.getLeftLightSensor();
            int currentRightSensorValue = edgar.getRightLightSensor();
            string userResponse;
            bool validResponse = true;

            DisplayScreenHeader("threshold Value");

            do
            {
                Console.Write("\tEnter the desired frequency of measurements: ");
                userResponse = Console.ReadLine();
                validResponse = int.TryParse(userResponse, out thresholdValue);

                switch (sensorsToMonitor)
                {
                    case "Left":
                        Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                        break;

                    case "Right":
                        Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                        break;

                    case "Both":
                        Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                        Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                        break;

                    default:
                        Console.WriteLine("\tUnkown Sensor Reference");
                        break;
                }

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
                    Console.WriteLine();
                }

            } while (!validResponse);

            

            return thresholdValue;
        }

        /// <summary>
        /// Get user input for min max range type including min max and full words
        /// </summary>
        /// <returns></returns>
        
        static string AlarmSystemDisplayRangeType()
        {
            string rangeType = "";

            DisplayScreenHeader("Range Type");

            //
            // validate user input
            //

            rangeType = ValidateStringInput
                (
                "\tEnter range type [Minimum, Maximum]: ",
                "\tPlease use Minimum or Maximum",
                new string[] {"Minimum", "Maximum"}
                );

            Console.WriteLine();
            Console.WriteLine($"\tEdgar will now monitor until {rangeType} threshold is reached");
            Console.CursorVisible = false;

            DisplayMenuPrompt("Alarm Sytem");

            return rangeType;
        }

        /// <summary>
        /// getting user decision to choose what sensors to watch
        /// </summary>
        /// <returns>which sensors left right or both</returns>
        static string AlarmSystemDisplaySetSensors()
        {
            string sensorsToMonitor = "";
            
            DisplayScreenHeader("Sensors To Monitor");

            //
            // validate user input
            //

            sensorsToMonitor = ValidateStringInput
                (
                "\tEnter Sensors to Monitor [Left, Right, Both]: ",
                "\tPlease use Left, Right or Both",
                new string[] {"Left", "Right", "Both"}
                );

            Console.WriteLine($"\tEdgar will now monitor {sensorsToMonitor} sensors");

            DisplayMenuPrompt("Alarm Sytem");

            Console.CursorVisible = false;

            return sensorsToMonitor;
        }

        /// <summary>
        /// Setting the alarm to start and display results
        /// </summary>
        /// <param name="edgar">Finch Robot</param>
        /// <param name="sensorsToMonitor">which sensors were chosen</param>
        /// <param name="rangeType">min or max</param>
        /// <param name="minMaxThreshholdValue">threshold choice</param>
        /// <param name="timeToMonitor">timeToMonitor</param>
        static void AlarmSystemSetAlarm
            (
            Finch edgar,
            string sensorsToMonitor,
            string rangeType,
            int minMaxThreshholdValue,
            int timeToMonitor
            )

        {
            bool thresholdExceeded = false;
            int secondsElapsed = 1;
            int leftLightSensorValue;
            int rightLightSensorValue;


            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tYou have chosen to monitor {sensorsToMonitor} sensor's, {rangeType} threshold, until it reaches {minMaxThreshholdValue}, or {timeToMonitor} seconds");
            Console.WriteLine();
            Console.CursorVisible = false;
            Console.WriteLine("\tPress any key to begin");

            //promt user to start
            Console.ReadKey();

            do
            {
                //
                // get and display current light levels
                //

                leftLightSensorValue = edgar.getLeftLightSensor();
                rightLightSensorValue = edgar.getRightLightSensor();

                switch (sensorsToMonitor)
                {
                    case "Left":
                        Console.WriteLine($"\tCurrent Left Light Sensor: {leftLightSensorValue}");
                        break;

                    case "Right":
                        Console.WriteLine($"\tCurrent Right Light Sensor: {rightLightSensorValue}");
                        break;

                    case "Both":
                        Console.WriteLine($"\tCurrent Left Light Sensor: {leftLightSensorValue}");
                        Console.WriteLine($"\tCurrent Right Light Sensor: {rightLightSensorValue}");
                        break;

                    default:
                        Console.WriteLine("\tUnkown Sensor Reference");
                        break;
                }

                //
                // wait 1 second
                //

                edgar.wait(1000);
                secondsElapsed++;

                //
                // test for threshold exceeded
                //
                
                switch (sensorsToMonitor)
                {
                    case "Left":
                        if (rangeType == "minimum" || rangeType == "min" || rangeType == "Minimum" || rangeType == "Min")
                        {
                            thresholdExceeded = (leftLightSensorValue < minMaxThreshholdValue);
                        }

                        //max

                        else
                        {
                            thresholdExceeded = (leftLightSensorValue > minMaxThreshholdValue);
                        }

                        break;

                    case "Right":
                        if (rangeType == "minimum" || rangeType == "min" || rangeType == "Minimum" || rangeType == "Min")
                        {
                            if (rightLightSensorValue < minMaxThreshholdValue)
                            {
                                thresholdExceeded = true;
                                Console.WriteLine("\tThreshold has been exceeded Press any key to continue");
                                Console.ReadKey();
                            }
                        }

                        // max

                        else
                        {
                            if (rightLightSensorValue > minMaxThreshholdValue)
                            {
                                thresholdExceeded = true;
                                Console.WriteLine("\tThreshold has been exceeded Press any key to continue");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "Both":
                        if (rangeType == "minimum" || rangeType == "min" || rangeType == "Minimum" || rangeType == "Min")
                        {
                            if ((leftLightSensorValue < minMaxThreshholdValue) || (rightLightSensorValue < minMaxThreshholdValue))
                            {
                                thresholdExceeded = true;
                                Console.WriteLine("\tThreshold has been exceeded Press any key to continue");
                                Console.ReadKey();

                            }
                        }

                        // max

                        else
                        {
                            if ((leftLightSensorValue > minMaxThreshholdValue) || (rightLightSensorValue > minMaxThreshholdValue))
                            {
                                thresholdExceeded = true;
                                Console.WriteLine("\tThreshold has been exceeded Press any key to continue");
                                Console.ReadKey();
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("\tUnkown Sensor Reference");
                        break;
                }

            } while (!thresholdExceeded && (secondsElapsed <= timeToMonitor));

            if (thresholdExceeded)
            {
                Console.WriteLine();
                Console.WriteLine("\tThreshold Exceeded");
                Console.WriteLine();
                Console.WriteLine("\tPress any key to continue");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("\tThreshold Not Exceeded - time limit exceeded");
                Console.WriteLine();
                Console.WriteLine("\tPress any key to continue");
                Console.ReadKey();
            }

            //
            // Display result
            //

            switch (sensorsToMonitor)
            {
                case "Left":

                    break;

                case "Right":

                    break;

                case "Both":

                    break;

                default:
                    Console.WriteLine("\tUnkown Sensor Reference");
                    break;
            }

        }

        #endregion

        #region Data Recorder

        /// <summary>
        /// Getting number of Data points to retrieve
        /// </summary>
        /// <returns></returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            double numberOfDataPoints;
            string userResponse;
            bool validResponse = true;

            DisplayScreenHeader("Number of Data Points");

            do
            {
                Console.Write("\tEnter the desired number of measurements: ");
                userResponse = Console.ReadLine();
                validResponse = double.TryParse(userResponse, out numberOfDataPoints);                

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
                    Console.WriteLine();
                } 

            } while (!validResponse);

            Console.WriteLine($"\tYou chose {numberOfDataPoints} as the number of data points.");
            DisplayContinuePrompt();

            return (int)numberOfDataPoints;

        }

        /// <summary>
        /// Getting time between data points with validation
        /// </summary>
        /// <returns></returns>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double numberOfDataFrequency;
            string userResponse;
            bool validResponse = true;            

            DisplayScreenHeader("Data Frequency");

            do
            {
                Console.Write("\tEnter the desired frequency of measurements: ");
                userResponse = Console.ReadLine();
                validResponse = double.TryParse(userResponse, out numberOfDataFrequency);

                if (!validResponse)
                {
                    Console.WriteLine("\tPlease enter a number 1,2,3.4, ... etc.");
                    Console.WriteLine();
                }

            } while (!validResponse);

            Console.WriteLine($"\tYou chose {numberOfDataFrequency} as the frequency data readings.");
            DisplayContinuePrompt();

            return numberOfDataFrequency;
        }

        /// <summary>
        /// Setting up menu for Data points collection
        /// </summary>
        /// <param name="edgar"></param>
        static void DataRecorderDisplayMenu(Finch edgar)
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get temperature data");
                Console.WriteLine("\td) Show Temperature data");
                Console.WriteLine("\te) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, edgar);
                        break;

                    case "d":
                        DataRecorderDisplayData(temperatures);
                        break;

                    case "e":
                        DisplayDisconnectFinchRobot(edgar);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(edgar);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch edgar)
        {
            double[] conversion = new double[numberOfDataPoints];
            double[] temperatures = new double[numberOfDataPoints];
            int dataPointFrequencyMs;

            //
            // convert the frequency in seconds to milliseconds
            //

            dataPointFrequencyMs = (int)(dataPointFrequency * 1000);

            DisplayScreenHeader("Get Data");

            Console.WriteLine($"\tThe Finch Robot will now record {numberOfDataPoints} temperatures {dataPointFrequency} seconds apart.");
            Console.WriteLine("\tPress any key to begin");
            Console.ReadKey();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = edgar.getTemperature();
                edgar.wait(dataPointFrequencyMs);
                conversion[index] = ConvertCelsiusToFarenheit(temperatures[index]);

                //
                // echo new temperature
                //

                Console.WriteLine($"\tTemperature {index + 1}: {temperatures[index].ToString("n2")}");
                edgar.wait(dataPointFrequencyMs);
            }

            return temperatures;
        }

        static void DataRecorderDisplayData(double[] temperatures)
        {
            //
            // disply table of temperatures
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tReading in Reading in Farenhiet");
            Console.WriteLine();
            Console.WriteLine(
                "Reading Number".PadLeft(20) +
                "Temperature".PadLeft(15));
            Console.WriteLine(
                "-------------".PadLeft(20) +
                "-----------------".PadLeft(15));

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine
                    ((index + 1).ToString("n1").PadLeft(20) +
                    (ConvertCelsiusToFarenheit(temperatures[index])).ToString("n1").PadLeft(15));
            }

            DisplayContinuePrompt();
            }

        static double ConvertCelsiusToFarenheit(double celsiusReading)
        {
            return (double)(celsiusReading * 1.8 + 32);
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch edgar)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Play a sad song");
                Console.WriteLine("\tb) Show off Lights");
                Console.WriteLine("\tc) Dance to Music!");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowPlayTaps(edgar);
                        break;

                    case "b":

                        TalentShowFlashLights(edgar);

                        break;

                    case "c":

                        TalentShowDanceToMario(edgar);

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }        
        
        /// <summary>
        /// Dancing to Mario song menu selection
        /// </summary>
        /// <param name="edgar"></param>
        static void TalentShowDanceToMario(Finch edgar)
        {
            DisplayScreenHeader("Dance to Music");

            Console.WriteLine("\tEdgar will dance to a funky beat!");

            TalentShowPlayMario(edgar);

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// Play Song with Blue lights
        /// </summary>
        /// <param name="edgar"></param>
        static void TalentShowPlayTaps(Finch edgar)
        {

            Console.CursorVisible = false;

            DisplayScreenHeader("Play a sad song");

            Console.WriteLine("\tEdgar will now play a sad song for you!");

            DisplayContinuePrompt();

            ////
            //// d,d,g
            ////
            ///
            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1174, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 2000, 750, 0, 0, 255);

            //
            // d,g,b 1
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1975, 2000, 750, 0, 0, 255);

            //
            // d,g,b 2
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1975, 1000, 750, 0, 0, 255);

            //
            // d,g,b 3
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1975, 1000, 750, 0, 0, 255);

            //
            // d,g,b 4
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1975, 2000, 750, 0, 0, 255);

            //
            // g,b,d
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1568, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1975, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 2349, 2000, 750, 0, 0, 255);

            //
            // b,g,d
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1975, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1174, 1000, 750, 0, 0, 255);

            //
            // d,d,g
            //

            TalentShowPlayNoteAndFlashLights(edgar, 1174, 750, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1174, 500, 100, 0, 0, 255);
            TalentShowPlayNoteAndFlashLights(edgar, 1568, 2000, 750, 0, 0, 255);

            DisplayMenuPrompt("Talent Show Menu");

        }

        /// <summary>
        /// Play Mario Bros tune, dance and glow whole song with static void TalentShowPlayMarioLightsSoundMove(Finch edgar, int note, int noteWait, int afterWait, int r, int g, int b, int p, int s)
        /// </summary>
        /// <param name="edgar">Finch Robot edgar</param>
        static void TalentShowPlayMario(Finch edgar)
        {

            TalentShowPlayMarioLightsSoundMove(edgar, 659, 250, 20, 255, 0, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 300, 100, 0, 255, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 350, 80, 255, 0, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 250, 80, 0, 255, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 250, 20, 255, 0, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 300, 500, 0, 255, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 392, 350, 500, 255, 0, 0, 100, 100);

            TalentShowPlayMarioLightsSoundMove(edgar, 523, 200, 80, 255, 0, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 392, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 329, 200, 80, 255, 0, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 440, 250, 80, 0, 255, 00, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 494, 200, 80, 255, 0, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 494, 250, 60, 0, 255, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 494, 150, 80, 255, 0, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 196, 200, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 392, 150, 80, 255, 0, 0, 155, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 150, 20, 0, 255, 0, -155, -155);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 150, 80, 255, 0, 0, -155, -155);
            TalentShowPlayMarioLightsSoundMove(edgar, 880, 150, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 698, 150, 80, 255, 0, 0, -155, -155);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 150, 80, 0, 255, 0, 155, 20);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 150, 80, 255, 0, 0, 20, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 250, 80, 0, 255, 0, 20, 155);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 250, 80, 255, 0, 0, 155, 20);
            TalentShowPlayMarioLightsSoundMove(edgar, 494, 200, 80, 0, 255, 0, -155, -155);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 150, 500, 255, 0, 0, -155, -155);

            TalentShowPlayMarioLightsSoundMove(edgar, 739, 200, 80, 255, 0, 0, -155, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 698, 250, 80, 0, 255, 0, 0, -155);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 300, 80, 255, 0, 0, 155, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 250, 80, 0, 255, 0, 0, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 392, 250, 80, 255, 0, 0, 50, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 440, 250, 80, 0, 255, 0, 0, 50);
            TalentShowPlayMarioLightsSoundMove(edgar, 554, 250, 80, 255, 0, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 440, 250, 80, 0, 255, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 554, 200, 80, 255, 0, 0, 100, 100);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 250, 80, 255, 0, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 739, 250, 80, 0, 255, 0, -100, -100);
            TalentShowPlayMarioLightsSoundMove(edgar, 698, 250, 80, 255, 0, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 250, 80, 0, 255, 0, -100, -100);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 250, 80, 255, 0, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 250, 100, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 350, 100, 255, 0, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 250, 500, 0, 255, 0, 0, 0);

            TalentShowPlayMarioLightsSoundMove(edgar, 739, 200, 80, 255, 0, 0, 255, -255);
            TalentShowPlayMarioLightsSoundMove(edgar, 698, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 300, 80, 255, 0, 0, 255, -255);
            TalentShowPlayMarioLightsSoundMove(edgar, 659, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 392, 250, 80, 255, 0, 0, 255, -255);
            TalentShowPlayMarioLightsSoundMove(edgar, 440, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 554, 250, 80, 255, 0, 0, 255, -255);
            TalentShowPlayMarioLightsSoundMove(edgar, 440, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 554, 200, 80, 255, 0, 0, -255, 255);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 250, 80, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 783, 250, 80, 255, 0, 0, -255, 255);
            TalentShowPlayMarioLightsSoundMove(edgar, 622, 250, 150, 0, 255, 0, 0, 0);
            TalentShowPlayMarioLightsSoundMove(edgar, 587, 250, 150, 255, 0, 0, -255, 255);
            TalentShowPlayMarioLightsSoundMove(edgar, 523, 250, 150, 0, 255, 0, 0, 0);

        }

        /// <summary>
        /// Play lights and sound with one method
        /// </summary>
        /// <param name="edgar"></param>
        /// <param name="note"></param>
        /// <param name="noteWait"></param>
        /// <param name="afterWait"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        static void TalentShowPlayNoteAndFlashLights(Finch edgar, int note, int noteWait, int afterWait, int r = 0, int g = 0, int b = 0)
        {
            edgar.noteOn(note);
            edgar.setLED(r, g, b);
            edgar.wait(noteWait);
            edgar.noteOff();
            edgar.setLED(0, 0, 0);
            edgar.wait(afterWait);

        }

        /// <summary>
        /// Play the Mario Bros Song, Light up and dance individual notes
        /// </summary>
        /// <param name="edgar">Finch Robot edgar</param>
        /// <param name="note">frequency notes</param>
        /// <param name="noteWait">time for notes to be on</param>
        /// <param name="afterWait">time between notes</param>
        /// <param name="r">red light</param>
        /// <param name="g">green light</param>
        /// <param name="b">blue light</param>
        /// <param name="p">port (left)</param>
        /// <param name="s">starboard(right)</param>
        static void TalentShowPlayMarioLightsSoundMove(Finch edgar, int note, int noteWait, int afterWait, int r, int g, int b, int p, int s)
        {
            edgar.noteOn(note);
            edgar.setLED(r, g, b);
            edgar.wait(noteWait);
            edgar.noteOff();
            edgar.setLED(0, 0, 0);
            edgar.wait(afterWait);
            edgar.setMotors(p, s);
        }

        /// <summary>
        /// Display Light colors
        /// </summary>
        /// <param name="edgar"></param>
        /// <param name="wait"></param>
        /// <param name="afterWait"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        static void colorLight(Finch edgar, int wait, int afterWait, int r = 0, int g = 0, int b = 0)
        {
            edgar.setLED(r, g, b);
            edgar.wait(wait);
            edgar.setLED(r, g, b);
            edgar.wait(afterWait);

        }

        static void TalentShowFlashLights(Finch edgar)
        {
            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tEdgar will now show off his flashy lights!");
            DisplayContinuePrompt();

            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);

            colorLight(edgar, 500, 150, 0, 255, 0);
            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);

            colorLight(edgar, 500, 150, 255, 0, 255);
            colorLight(edgar, 500, 150, 0, 255, 255);
            colorLight(edgar, 500, 150, 255, 255, 0);

            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 255, 0, 0);
            colorLight(edgar, 500, 150, 0, 255, 0);

            colorLight(edgar, 500, 150, 0, 0, 255);
            colorLight(edgar, 500, 150, 0, 255, 0);
            colorLight(edgar, 500, 150, 255, 0, 0);

            edgar.setLED(0, 0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// Connect the Finch Robot
        /// </summary>
        /// <param name="edgar">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch edgar)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = edgar.connect();

            ShowEdgarConnect(edgar);

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //

            edgar.setLED(0, 0, 0);
            edgar.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }


        /// <summary>
        /// Validate string 
        /// created by JP 
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="error"></param>
        /// <param name="validInputs"></param>
        /// <returns></returns>
        static string ValidateStringInput(string prompt, string error, string [] validInputs)
        {
            bool validInput = false;
            string userInput = "";
            int index = 0;

            //
            // check for valid input
            //

            while (!validInput)
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();

                for (index = 0; index < validInputs.Length; index++)
                {
                    if (validInputs[index].ToLower() == userInput.ToLower())
                    {
                        validInput = true;
                        break;
                    }

                }

                //
                // if a bvalid input is not found display an error message
                //
                if (!validInput)
                {
                    Console.Write("\n{0}: ", error);
                    Console.WriteLine();
                    Console.ReadKey();
                    Console.CursorVisible = false;
                }
            }

            return validInputs[index];
        }

        static int ValidateIntInput(string header, string prompt, string error, string confirmation)
        {
            string userResponse;
            int userInteger;

            DisplayScreenHeader(header);

            {
                bool validResponse = true;

                do
                {
                    Console.Write("\t" + prompt);
                    userResponse = Console.ReadLine();
                    validResponse = int.TryParse(userResponse, out userInteger);

                    if (!validResponse)
                    {
                        Console.WriteLine("\t" + error);
                        Console.WriteLine();
                    }

                } while (!validResponse);

                Console.WriteLine($"\t you chose {userResponse} " + confirmation);
                DisplayContinuePrompt();

                return userInteger;
            }



        }


        

        #endregion

    }

}




