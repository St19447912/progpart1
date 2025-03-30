using System;
using System.IO;
using System.Media;
using System.Collections.Generic;

namespace progpart1
{
    class CyberSecurityChatbot
    {
        private Dictionary<string, string> responses;

        public CyberSecurityChatbot()
        {
            responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "A password is a secret key used to protect accounts. Use a mix of letters, numbers, and symbols." },
                { "cybersecurity", "Cybersecurity involves protecting systems and networks from digital attacks." },
                { "how are you", "I'm good, how are you?" },
                { "what is your purpose", "My purpose is to help you with cybersecurity-related matters!" },
                { "phishing", "Phishing is a scam where attackers trick you into providing personal information. Be cautious of unknown emails." },
                { "malware", "Malware is malicious software designed to harm your system. Keep your antivirus updated." },
                { "hardware", "Hardware refers to the physical components of a computer, such as the CPU, RAM, and hard drive." },
                { "protect information", "To stay safe online, use strong passwords, avoid clicking unknown links, and enable two-factor authentication." }
            };
        }

        public void Run()
        {
            PlaySound("greeting.wav");
            DisplayAsciiLogo();
            WelcomeUser();
            StartChat();
        }

        private void PlaySound(string filePath)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(filePath);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing audio: " + ex.Message);
                Console.ResetColor();
            }
        }

        private void DisplayAsciiLogo()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ascii-art.txt");
                string asciiArt = File.ReadAllText(filePath);
                Console.WriteLine(asciiArt);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error loading ASCII logo: " + ex.Message);
                Console.ResetColor();
            }
        }

        private void WelcomeUser()
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("          Chatbot          ");
            Console.WriteLine("===============================");
        }

        private void StartChat()
        {
            string userName = GetUserInput("\nChatbot: Hey! Welcome to the cybersecurity chatbot, what is your name?");
            Console.WriteLine($"\nChatbot: Hey {userName}! What do you need help with?");

            string userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\n{userName}: ");
                Console.ResetColor();

                userInput = Console.ReadLine()?.ToLower();

                if (userInput == "exit")
                {
                    if (ConfirmExit())
                    {
                        return;
                    }
                    continue;
                }

                ProcessUserInput(userInput);
            } while (true);
        }

        private string GetUserInput(string prompt)
        {
            string input;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(prompt);
                Console.ResetColor();
                Console.Write("User: ");
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Name cannot be empty! Please enter your name.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        private void ProcessUserInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var key in responses.Keys)
            {
                if (input.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"\nChatbot: {responses[key]}");
                    Console.ResetColor();
                    return;
                }
            }

            Console.WriteLine("\nChatbot: Sorry, I can't help you with that. I'm here to assist with cybersecurity topics.");
            Console.ResetColor();
        }

        private bool ConfirmExit()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nChatbot: Are you sure you want to exit? (yes/no): ");
            Console.ResetColor();

            string response = Console.ReadLine()?.ToLower();
            if (response == "yes")
            {
                Console.WriteLine("Chatbot: Goodbye! Stay safe online.");
                return true;
            }
            else
            {
                Console.WriteLine("Chatbot: Alright, let's continue!");
                return false;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            CyberSecurityChatbot chatbot = new CyberSecurityChatbot();
            chatbot.Run();
        }
    }
}
