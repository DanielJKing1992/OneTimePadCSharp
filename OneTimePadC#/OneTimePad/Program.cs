using System;
using System.IO;

namespace OneTimePad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool quit = false;
			do {
				Console.WriteLine ("Choose a function: ");
				Console.WriteLine ("1 -- Encode a message");
				Console.WriteLine ("2 -- Decode a message");
				Console.WriteLine ("3 -- Quit");

				string choice = Console.ReadLine ();
				//Console.WriteLine ("You entered " + choice);

				if (choice == "1" || choice == "2")
				{
					string sourcePath;
					string keytextPath =@"/Users/Daniel/NetBeansProjects/OneTimePad/Key.txt";
					string outputPath;

					string sourceText;

					string keytextText = File.ReadAllText(keytextPath);
					//string keytextText = "sadflasdkfgkjblahjdsbfhjasdbfhjadsfbhjkdsbfjhkaksbdjf";

					string outputText;
					char[] sourceArray;
					char[] keyArray;
					int msgLength;



					if (choice == "1")
					{
						//Select correct files - run keytext against plaintext, store result as ciphertext
						sourcePath = @"/Users/Daniel/NetBeansProjects/OneTimePad/TestPlain.txt";
						outputPath = @"/Users/Daniel/NetBeansProjects/OneTimePad/TestCipher.txt";


					}

					else
					{
						//Select correct files - run keytext against ciphertext, store result as decoded
						sourcePath = @"/Users/Daniel/NetBeansProjects/OneTimePad/TestCipher.txt";
						outputPath = @"/Users/Daniel/NetBeansProjects/OneTimePad/TestDecoded.txt";
					}
						

					Console.WriteLine("Doing stuff");
					//Run the texts against each other

					sourceText = File.ReadAllText(sourcePath);


					sourceArray=sourceText.ToCharArray();
					msgLength = sourceArray.Length;
					char[] outputArray = new char[msgLength];

					keyArray = keytextText.ToCharArray(0, msgLength);
					int[] sourceCharInt = new int[msgLength];
					int[] keyCharInt = new int[msgLength];
					int[] outputCharInt = new int[msgLength];

					for (int i = 0; i < msgLength; i++)
					{
						char s = sourceArray[i];
						char k = keyArray[i];
						//get ith source char and ith key char, turn into ASCII, add, fix, turn back into character, store in output array
						int sourceCharAscii = (int)s;
						if (sourceCharAscii == 32)
						{
							sourceCharInt[i] = 36;
							//"space" is assigned an int value of 36
						}
						else if (sourceCharAscii < 58)
						{
							sourceCharInt[i] = sourceCharAscii - 48;
							//0-9 becomes 0-9. Anything w/ ASCII value less than 58 that's not "space" is a number.
						}
						else
						{
							sourceCharInt[i] = sourceCharAscii - 55;
							//A-Z becomes 10-35, special chars become 36-41, a-z becomes 42-67, other special chars become 68-71.
							//No other characters will be accounted for, since they will not be in the plain text or the key.
						}

						int keyCharAscii = (int)k;
						if (keyCharAscii == 32)
						{
							keyCharInt[i] = 36;
						}
						else if (keyCharAscii < 58)
						{
							keyCharInt[i] = keyCharAscii - 48;
						}
						else
						{
							keyCharInt[i] = keyCharAscii - 55;
						}

						if (choice == "1")
							outputCharInt[i] = (sourceCharInt[i]+keyCharInt[i])%37;
						else
						{
							outputCharInt[i] = (sourceCharInt[i] - keyCharInt[i]);
							do
							{
								if (outputCharInt[i] < 0)
									outputCharInt[i] += 37;
							} while (outputCharInt[i] < 0);
						}

						if (outputCharInt[i] == 36)
						{
							outputArray[i] = ' ';
						}
						else if (outputCharInt[i] < 10)
						{
							outputArray[i] = (char)(outputCharInt[i] + 48);
						}
						else
						{
							outputArray[i] = (char)(outputCharInt[i] + 55);
						}
						//Console.Write(sourceCharAscii + " ");


					}

					//turn output array into output text
					outputText = new string(outputArray);
					Console.WriteLine(outputText);
					File.WriteAllText(outputPath, outputText);
				}

				if (choice == "3")
				{
					quit = true;
				}
			} while (quit == false);
		}
	}
}