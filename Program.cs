using System;
using System.Collections.Generic;
using System.Drawing; //Required for eyes.Open API
using Applitools.Selenium; //Applitools Selenium SDK
using OpenQA.Selenium.Chrome; // Selenium's Chrome browser SDK


namespace ApplitoolsTutorial
{

    class Program
    {
        static void Main(string[] args)
        {
            // Open a Chrome browser.
            var driver = new ChromeDriver();

            // Initialize the eyes SDK and set your private API key.
            var eyes = new Eyes();

            //scroll and take full page screenshot
            eyes.ForceFullPageScreenshot = true;


            // Hard code the Applitools API key or get it from the environment (see the Tutorial for details)
            // eyes.ApiKey = "Your_APIKEY";
            eyes.ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY"); 

            try
            {
                // Call getTestInfoForPart to get the appropriate test information.
                Dictionary<string, string> testInfo = GetTestInfoForPart(args);


                // Start the test by setting AUT's name, window or the page name that's being tested, viewport width and height
                eyes.Open(driver, testInfo["appName"], testInfo["windowName"], new Size(
                        Int32.Parse(testInfo["viewportWidth"]), Int32.Parse(testInfo["viewportHeight"])));


                // Navigate the browser to the "hello world!" web-site.
                driver.Url = testInfo["url"];

                // Visual checkpoint #1.
                eyes.CheckWindow(testInfo["windowName"]);

                // End the test.
                eyes.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // Close the browser.
                driver.Quit();

                // If the test was aborted before eyes.Close was called, ends the test as aborted.
                eyes.AbortIfNotClosed();
            }
        }


        /**
         * getTestInfoForPart
         * 
         * This method returns details of the test for different parts of the tutorial.
         * 
         * This method receives tutorial part information from the command prompt such
         * as "1", "2" etc., and then returns test app's URL, app-name, test-name,
         * viewportHeight, viewportWidth and URL information for that part of the
         * tutorial. This information is then used by the caller to run different tests.
         * 
         * @param args String[] from the CLI such as "1", "2"
         * 
         * @return A dictionary of test app's URL, app-name, test-name, viewportHeight,
         *         viewportWidth.
         */
        public static Dictionary<string, string> GetTestInfoForPart(string[] args)
        {
            // There are 4 different parts of the tests. Default it to "1".
            string testPartNumber = "1";

            // If test part number is specified through CLI, use that
            if (args.Length != 0)
            {
                testPartNumber = args[0];
            }
            Console.WriteLine("Running tests for tutorial part: " + testPartNumber);

            int testNumber = Int32.Parse(testPartNumber);


            var hmap = new Dictionary<string, string>();
            string baseUrl = "https://demo.applitools.com/";
            string viewportWidth = "1000";
            string viewportHeight = "600";
            string testName = "Login Page C# Quickstart";
            string appName = "ACME app C#";
            string loginPageName = "Login Page C#";
            string appPageName = "App Page C#";
            switch (testNumber)
            {
                default:
                case 1:
                    hmap.Add("url", baseUrl + "index.html");
                    hmap.Add("appName", appName);
                    hmap.Add("windowName", loginPageName);
                    hmap.Add("testName", testName);
                    hmap.Add("viewportWidth", viewportWidth);
                    hmap.Add("viewportHeight", viewportHeight);
                    break;
                case 2:
                    hmap.Add("url", baseUrl + "index_v2.html");
                    hmap.Add("appName", appName);
                    hmap.Add("windowName", loginPageName);
                    hmap.Add("testName", testName);
                    hmap.Add("viewportWidth", viewportWidth);
                    hmap.Add("viewportHeight", viewportHeight);
                    break;
                case 3:
                    hmap.Add("url", baseUrl + "app.html");
                    hmap.Add("appName", appName);
                    hmap.Add("windowName", appPageName);
                    hmap.Add("testName", testName);
                    hmap.Add("viewportWidth", viewportWidth);
                    hmap.Add("viewportHeight", viewportHeight);
                    break;
                case 4:
                    hmap.Add("url", baseUrl + "app_v2.html");
                    hmap.Add("appName", appName);
                    hmap.Add("windowName", appPageName);
                    hmap.Add("testName", testName);
                    hmap.Add("viewportWidth", viewportWidth);
                    hmap.Add("viewportHeight", viewportHeight);
                    break;

            }
            return hmap;
        }


    }
}
