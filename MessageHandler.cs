using System;
using System.Threading;

namespace InsuranceAppConsole
{
    public enum MessageType
    {
        Info,
        Success,
        Error
    }

    /// <summary>
    /// Handles console message display with different colors for error, information, and success messages.
    /// </summary>
    public static class MessageHandler
    {
        /// <summary>
        /// Displays a message based on the type of message.
        /// </summary>
        /// <param name="type">The type of the message (Info, Success, Error).</param>
        /// <param name="message">The message to display.</param>
        public static void ShowMessage(MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Info:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{message}");
                    Console.ResetColor();
                    break;
                case MessageType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{message}");
                    Console.ResetColor();
                    break;
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{message}");
                    Console.ResetColor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            Thread.Sleep(1500);
        }

        /// <summary>
        /// Pauses the program until a key is pressed.
        /// </summary>
        public static void WaitForKeyPress()
        {
            Console.WriteLine("\nStiskněte libovolnou klávesu pro pokračování...");
            Console.ReadKey();
        }
    }
}
