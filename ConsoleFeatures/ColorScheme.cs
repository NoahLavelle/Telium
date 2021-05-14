using System;

namespace Telium.ConsoleFeatures
{
    
    // Contains the colors for different things so it is easy to reference and change them
    public static class ColorScheme
    {
        // The const qualifier acts as static and readonly
        public const ConsoleColor PromptColor = ConsoleColor.DarkGreen;
        public const ConsoleColor SelectionColor = ConsoleColor.Cyan;
        public const ConsoleColor DefaultColor = ConsoleColor.White;
        public const ConsoleColor NameColor = ConsoleColor.Cyan;
    }
}