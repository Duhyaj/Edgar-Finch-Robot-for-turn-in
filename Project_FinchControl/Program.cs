using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;


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

    class Program


    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool isConnected;

            Finch edgar;
            edgar = new Finch();

            //
            // call the connect method
            //

            isConnected = edgar.connect();
            edgar.connect();

            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// setting LED Lights with pauses
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

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(edgar);
                        break;

                    case "q":
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

        //static void TalentShowDisplayDance(Finch edgar)
        //{
        //    Console.CursorVisible = false;

        //    DisplayScreenHeader("Dance to Music");

        //    Console.WriteLine("\tEdgar will dance to a funky beat!");

        //    DisplayContinuePrompt();

        //    TalentShowDanceToMario(edgar);



        //}

        static void TalentShowDanceToMario(Finch edgar)
        {
            DisplayScreenHeader("Dance to Music");

            Console.WriteLine("\tEdgar will dance to a funky beat!");

            TalentShowPlayMario(edgar);

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>



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
        /// Play Mario Bros tune, dance and glow
        /// </summary>
        /// <param name="edgar"></param>
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
        /// Play the Mario Bros Song, Light up and dance
        /// </summary>
        /// <param name="edgar"></param>
        /// <param name="note"></param>
        /// <param name="noteWait"></param>
        /// <param name="afterWait"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="s"></param>
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

        //static void TalentShowDisplayLightAndSound(Finch finchRobot)
        //{
        //    Console.CursorVisible = false;

        //    DisplayScreenHeader("Light and Sound");

        //    Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
        //    DisplayContinuePrompt();

        //    for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
        //    {
        //        finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
        //        finchRobot.noteOn(lightSoundLevel * 100);
        //    }

        //    DisplayMenuPrompt("Talent Show Menu");
        //}

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

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            DisplayScreenHeader("Number of Date Points");
            Console.Write("\tNumber of Data Points: ");
            numberOfDataPoints = int.Parse(Console.ReadLine());

            //need to validate the number

            Console.WriteLine($"\tYou chose {numberOfDataPoints} as the number of data points.");
            DisplayContinuePrompt();



            return numberOfDataPoints;
        }

        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double numberOfDataFrequency;
            DisplayHeader("Number of Data Frequency");
            Console.Write("\tEnter the desired frequency of measurements: ");
            numberOfDataFrequency = double.Parse(Console.ReadLine());

            //need to validate the number

            Console.WriteLine($"\tYou chose {numberOfDataFrequency} as the number of data points.");
            DisplayContinuePrompt();



            return numberOfDataFrequency;
        }

        static double[] DataRecorderDisplayGetDataPointFrequency(int numberOfDataPoints, double dataPointFrequency, Finch edgar)
        {
            double[] temperatures = new double[numberOfDataPoints];
            int dataPointFrequencyMs;

            //
            // convert the frequency in seconds to milliseconds

            dataPointFrequencyMs = (int)(dataPointFrequency * 1000);

            DisplayHeader("Data Frequency");


            // echo the values

            Console.WriteLine($"\tThe Finch Robot will now record {numberOfDataPoints} temperatures {dataPointFrequency} seconds apart.");
            Console.WriteLine("\tPress any key to begin");
            Console.ReadKey();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = edgar.getTemperature();
                edgar.wait(dataPointFrequencyMs);

                //
                // echo new temperature

                Console.WriteLine($"\tTemperature {index + 1}: {temperatures[index]}");
                edgar.wait(dataPointFrequencyMs);

                DataRecorderDisplayData(temperatures);

            }

            return temperatures;
        }

        static void DataRecorderDisplayGetData1(double[] temperatures)
        {
            DisplayHeader("Display get data");

            //
            // disply table of temperatures

            Console.WriteLine();
            Console.WriteLine(
                "Reading Number".PadLeft(20) +
                "Temperature".PadLeft(15));
            Console.WriteLine(
                "-------------".PadLeft(20) +
                "-----------------".PadLeft(15));


            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    "(temperatures[index])".PadLeft(15));
            }
        }

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
                DisplayHeader("Main Menu");

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

            dataPointFrequencyMs = (int)(dataPointFrequency * 1000);

            DisplayHeader("Get Data");



            // echo the values

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




                Console.WriteLine(
                    (index + 1).ToString("n1").PadLeft(20) +
                    (ConvertCelsiusToFarenheit(temperatures[index])).ToString("n1").PadLeft(15));


            }

            DisplayContinuePrompt();

        }

        static double ConvertCelsiusToFarenheit(double celsiusReading)
        {
            return (double)(celsiusReading * 1.8 + 32);
        }

        #endregion

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
                DisplayHeader("Main Menu");

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

        static int AlarmSystemDisplayTimeToMonitor()
        {
            int timeToMonitor = 0;

            DisplayScreenHeader("Time to monitor");
            Console.Write("\tEnter Time to Monitor");

            timeToMonitor = int.Parse(Console.ReadLine());

            return timeToMonitor;


        }

        static int AlarmSystemDisplaythreshholdValue(string sensorsToMonitor, Finch edgar)
        {
            int thresholdValue = 0;
            int currentLeftSensorValue = edgar.getLeftLightSensor();
            int currentRightSensorValue = edgar.getRightLightSensor();

            DisplayScreenHeader("threshold Value");

            switch (sensorsToMonitor)
            {
                case "left":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    break;

                case "right":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                    break;

                case "both":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");
                    break;

                default:
                    Console.WriteLine("\tUnkown Sensor Reference");
                    break;
            }

            //
            // get threshold from user
            //

            Console.Write("Enter Threshold Value: ");
            thresholdValue = int.Parse(Console.ReadLine());

            // validate user inpute=s
            //dont int.parse liek above




            return thresholdValue;
        }

        static string AlarmSystemDisplayRangeType()
        {
            string rangeType = "";

            DisplayScreenHeader("Range Type");

            Console.Write("Enter Range Type [minimum, Maximum]");
            rangeType = Console.ReadLine();

            DisplayMenuPrompt("Alarm Ssytem");

            return rangeType; ;
        }

        static string AlarmSystemDisplaySetSensors()
        {
            string sensorsToMonitor = "";

            DisplayScreenHeader("Sensors to Monitor");

            Console.Write("\tEnter Sensors to Monitor [Left, Right, both]");
            sensorsToMonitor = Console.ReadLine();

            DisplayMenuPrompt("Alarm Ssytem");

            return sensorsToMonitor;
        }

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

            //echo values to user
            Console.WriteLine("\t\tStart");

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
                    case "left":
                        Console.WriteLine($"\tCurrent Left Light Sensor: {leftLightSensorValue}");
                        break;

                    case "right":
                        Console.WriteLine($"\tCurrent Right Light Sensor: {rightLightSensorValue}");
                        break;

                    case "both":
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
                    case "left":
                        if (rangeType == "minimum")
                        {
                            thresholdExceeded = (leftLightSensorValue < minMaxThreshholdValue);
                        }

                        //max

                        else
                        {
                            thresholdExceeded = (leftLightSensorValue > minMaxThreshholdValue);
                        }

                        break;

                    case "right":
                        if (rangeType == "minimum")
                        {
                            if (rightLightSensorValue < minMaxThreshholdValue)
                            {
                                thresholdExceeded = true;
                            }
                        }

                        // max

                        else
                        {
                            if (rightLightSensorValue > minMaxThreshholdValue)
                            {
                                thresholdExceeded = true;
                            }
                        }
                        break;

                    case "both":
                        if (rangeType == "minimum")
                        {
                            if ( (leftLightSensorValue < minMaxThreshholdValue) || (rightLightSensorValue < minMaxThreshholdValue) )
                            {
                                thresholdExceeded = true;
                            }
                        }

                        // max

                        else
                        {
                            if ((leftLightSensorValue > minMaxThreshholdValue) || (rightLightSensorValue > minMaxThreshholdValue))
                            {
                                thresholdExceeded = true;
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("\tUnkown Sensor Reference");
                        break;
                }




            } while (!thresholdExceeded && (secondsElapsed <= timeToMonitor));


            //
            // Display result
            //

            if (thresholdExceeded)
            {
                Console.WriteLine("\tThreshold Exceeded");
            }

            else
            {
                Console.WriteLine("\tThreshold Not Exceeded - time limit exceeded");
            }





            switch (sensorsToMonitor)
            {
                case "left":

                    break;

                case "right":

                    break;

                case "both":

                    break;

                default:
                    Console.WriteLine("\tUnkown Sensor Reference");
                    break;
            }




        }





    }
}



