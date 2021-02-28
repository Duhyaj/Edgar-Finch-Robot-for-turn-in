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

                        break;

                    case "d":

                        break;

                    case "e":

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

            ShowEdgarConnect(edgar);

            robotConnected = edgar.connect();
                       
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




        #endregion
    }


}
