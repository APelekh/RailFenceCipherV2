using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RailFenceCipherV2
{
    class Program
    {
        static void Main(string[] args)
        {
            //RailFence test = new RailFence(3);
            //string output = test.Encrypt("REddITCOMRDAILYPROGRAMMER");
            //Console.WriteLine(output);
            //output = test.Decrypt("RImiRAREDTORALPORMEDCDYGM");
            //Console.WriteLine(output);
            //RailFence test1 = new RailFence(4);
            //output = test1.Encrypt("THEQUIcKBrOWNFOXJUMPSOVERTHELAZYDOG");
            //Console.WriteLine(output);
            //output = test1.Decrypt("TCNMRzHIkWFUPETAYEUBOOJSVHLDGQRXOEO");
            //Console.WriteLine(output);
            RailFence newcipher = new RailFence();
            Console.ReadKey();
        }
    }


    /// <summary>
    /// Class that is responsible for encryption of a string
    /// </summary>
    class RailFence
    {
        //creating a list of strings that will hold the length of encrypted strings
        private List<string> _listOfStringsEnc = new List<string>();
        public List<string> ListOfStringsEnc
        {
            get { return _listOfStringsEnc; }
            set { _listOfStringsEnc = value; }
        }
        //creating a list of strings that will hold the length of decrypted strings
        private List<string> _listOfStringsDec = new List<string>();
        public List<string> ListOfStringsDec
        {
            get { return _listOfStringsDec; }
            set { _listOfStringsDec = value; }
        }
        //creating a list of strings that will hold decrypted letters
        private List<string> _listForDecrypt = new List<string>();
        public List<string> ListForDecrypt
        {
            get { return _listForDecrypt; }
            set { _listForDecrypt = value; }
        }
        //prorepty for the amount of rows for encryption
        private int _numberOfRows;
        public int NumberOfRows
        {
            get { return _numberOfRows; }
            set { _numberOfRows = value; }
        }
        /// <summary>
        /// Constructor that initializing the list of strings accourding to the amount of rows indicated
        /// </summary>
        /// <param name="numberOfRows">Number of rows for encryption</param>
        public RailFence()
        {
            Console.WindowHeight = 35;
            //intro with explanations
            Console.WriteLine(@"
Welcome to RailFence Cipher!

How it works.

Program encrypts an input string using the specified amounts of rows and a rail fence method. On the example below, let's encrypt a string ""RAILFENCECIPHER"" using 3 rows:

R     F      E      H
  A  L  E  C   C  P  E
   I     N      I     R

Now, we will concatenate each of 3 strings and will get the following final encrypted string ""RFEHALECCPEINIR"".
For decryption part, you will have to specify an input string as well as number of rows.");
            //mail loop
            bool mainLoop = true;
            while (mainLoop)
            {
                //asking if user wants to encrypt or decrypt
                Console.WriteLine("For encryption please type \"enc\" and for decryption type \"dec\".");
                Console.Write("Enc or Dec?: ");
                string userinput = Console.ReadLine();
                //checking for valid input in below loop
                bool userLoop = true;
                while (userLoop)
                {
                    if (userinput.ToLower() != "enc" && userinput.ToLower() != "dec")
                    {
                        Console.WriteLine("Invalid input. Please type only enc or dec.");
                        Console.Write("Enc or Dec?: ");
                        userinput = Console.ReadLine();
                    }
                    else
                    {
                        userLoop = false;
                    }
                }
                //saving user input
                string encOrDec = userinput;
                if (encOrDec == "enc")
                {
                    encOrDec = "encryption";
                }
                else
                {
                    encOrDec = "decryption";
                }
                //asking for number of rows for encryption/decryption
                Console.WriteLine("Please type the number of rows for {0}.", encOrDec);
                Console.Write("Number of rows: ");
                userinput = Console.ReadLine();
                //checking for valid input in below loop
                int result = 0;
                while (!int.TryParse(userinput, out result))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    userinput = Console.ReadLine();
                }
                //assigning properties according to number of rows entered by user
                for (int i = 0; i < int.Parse(userinput); i++)
                {
                    this.ListOfStringsEnc.Add(string.Empty);
                    this.ListOfStringsDec.Add(string.Empty);
                }
                this.NumberOfRows = int.Parse(userinput);
                //asking for input string
                Console.WriteLine("Please type the string for {0}.", encOrDec);
                Console.Write("String for {0}: ", encOrDec);
                userinput = Console.ReadLine();
                //calling a function depending of previous user's choice
                if (encOrDec == "encryption")
                {
                    Console.WriteLine("Here is your encrypted string: {0} .", this.Encrypt(userinput));
                }
                else
                {
                    Console.WriteLine("Here is your decrypted string: {0} .", this.Decrypt(userinput));
                }
                //asking if user want to exit the program
                Console.WriteLine("\nTo stop the program press \"N\". Otherwise, press any key to run Cipher again...");
                ConsoleKeyInfo userFinalInput = Console.ReadKey();
                if (userFinalInput.Key == ConsoleKey.N)
                {
                    mainLoop = false;
                }
                //reseting properties if user want to try again
                this.ListForDecrypt.Clear();
                this.ListOfStringsDec.Clear();
                this.ListOfStringsEnc.Clear();
                Console.Clear();
            }
        }
        /// <summary>
        /// Method that encrypts the input string
        /// </summary>
        /// <param name="inputString">String to be encrypted</param>
        /// <returns>Returns an encrypted string</returns>
        public string Encrypt(string inputString)
        {
            //keeping all captial letters in input string
            inputString = inputString.ToUpper();
            //decraring a counter that indicates the number of row during the main loop
            int counter = 0;
            //declaring a counter that indicates the number of row that we are at during the loop
            string direction = "down";
            //looping through each letter in the input string
            for (int i = 0; i < inputString.Length; i++)
            {
                //checking for direction
                if (direction == "down")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStringsEnc[counter] += inputString[i];
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "up";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "up")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStringsEnc[counter] += inputString[i];
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "down";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }

            }
            //joining all strings from the list and returning a full encrypted string
            return string.Join("", this.ListOfStringsEnc.Select(x => x));
        }
        /// <summary>
        /// Method that decrypts the input string
        /// </summary>
        /// <param name="inputString">String to be decrypted</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string inputString)
        {
            //saving original input string before modifying it
            string originalInputString = inputString;
            //keeping all captial letters in input string
            inputString = inputString.ToUpper();
            //declaring a counter that indicates the number of row that we are at during the loop
            int counter = 0;
            //declaring a counter that indicates the direction that we are moving at during the main loop
            string direction = "down";
            //this loop is responsible for getting correct lengths of each string to be decrypted
            //looping through each letter in the input string
            for (int i = 0; i < inputString.Length; i++)
            {
                //checking for direction
                if (direction == "down")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStringsDec[counter] += inputString[i];
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "up";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "up")
                {
                    //adding a letter to apropriate string in the list of strings according to counter value
                    this.ListOfStringsDec[counter] += inputString[i];
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "down";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }
            }
            //going through each string from list of strings, getting the length of each and adding the same number of letters into respective string in my decryption list
            for (int i = 0; i < ListOfStringsDec.Count; i++)
            {
                this.ListForDecrypt.Add(inputString.Substring(0, ListOfStringsDec[i].Length));
                //removing letters from input string that were added into decryption list
                inputString = inputString.Replace(ListForDecrypt[i], "");
            }
            //declaring a string that will have a dectypted text
            string returnString = string.Empty;
            //reseting my counters for another loop
            counter = 0;
            direction = "down";
            //adding letters from my decryption list into return string until the length of return string is equal to the original input string
            while (returnString.Length != originalInputString.Length)
            {
                //checking for direction
                if (direction == "down")
                {
                    //adding a letter from apropriate string from the decryption list according to counter value
                    returnString += ListForDecrypt[counter][0].ToString();
                    //removing the letter that was added from decryption list
                    ListForDecrypt[counter] = ListForDecrypt[counter].Remove(0, 1);
                    //checking if we reached the last row
                    if (counter == this.NumberOfRows - 1)
                    {
                        //if we reached the last row, then changing direction and starting decrementing the counter
                        direction = "up";
                        counter--;
                    }
                    else
                    {
                        //if it's not the last row, then just keed incrementing the counter
                        counter++;
                    }
                }
                //checking for direction
                else if (direction == "up")
                {
                    //adding a letter to my return string from apropriate string from the decryption list according to counter value
                    returnString += ListForDecrypt[counter][0].ToString();
                    //removing the letter that was added from decryption list
                    ListForDecrypt[counter] = ListForDecrypt[counter].Remove(0, 1);
                    //checking if we reached the first row
                    if (counter == 0)
                    {
                        //if we reached the first row, then changing direction and starting incrementing the counter
                        direction = "down";
                        counter++;
                    }
                    else
                    {
                        //if it's not the first row, then just keed decrementing the counter
                        counter--;
                    }
                }
            }
            return returnString;
        }
    }


    #region " TEST CLASS "

    //We need to use a Data Annotation [ ] to declare that this class is a Test class
    [TestFixture]
    class Test
    {
        [Test]
        public void ValidTest()
        {
            //RailFenceEnc ite = new RailFenceEnc(3);
            //Assert.IsTrue(ite.Encrypt("REDDITCOMRDAILYPROGRAMMER") == "RIMIRAREDTORALPORMEDCDYGM");
            //RailFenceDecrypt iteTwo = new RailFenceDecrypt(4);
            //Assert.IsTrue(iteTwo.Decrypt("TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO") == "THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG");
        }
        [Test]
        public void InvalidTest()
        {
            //RailFenceEnc ite = new RailFenceEnc(3);
            //Assert.IsFalse(ite.Encrypt("REDDITCOMRDAILYPROGRAMMER") == "REDDITCOMRDAILYPROGRAMMER");
        }

    }
    #endregion
}
