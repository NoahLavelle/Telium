using System;
using System.Collections.Generic;

namespace Telium.ConsoleFeatures
{
    public class Select
    {
        private static Select _instance;

        private readonly Dictionary<ConsoleKey, Func<int, int>> _actions = new()
        {
            {ConsoleKey.UpArrow, selectedItem => selectedItem - 1},
            {ConsoleKey.DownArrow, selectedItem => selectedItem + 1},
            {
                ConsoleKey.Enter, selectedItem =>
                {
                    _instance.ItemSelected();
                    return selectedItem;
                }
            }
        };

        private readonly int _selectedItem;
        private readonly Newtonsoft.Json.Linq.JObject _finalItem;
        private readonly RoomData _roomData;
        private bool _itemSelected;

        public Select(RoomData roomData)
        {
            _instance = this;
            int optionsCount = roomData.doors.Length;
            _roomData = roomData;

            roomData.name += ":\n";
            Console.CursorVisible = false;
            DrawMulticoloredLine.Draw(new[]
            {
                new DrawMulticoloredLine.ColoredStringSection("? ", ColorScheme.PromptColor),
                new DrawMulticoloredLine.ColoredStringSection(roomData.name, ColorScheme.DefaultColor)
            });

            while (!_itemSelected)
            {
                for (var i = 0; i < optionsCount; i++)
                {
                    if (_selectedItem == i)
                    {
                        Console.ForegroundColor = ColorScheme.SelectionColor;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine(roomData.doors[i]["name"]);
                    Console.ResetColor();
                }

                var keyPressed = Console.ReadKey(true).Key;
                if (_actions.ContainsKey(keyPressed))
                    _finalItem = roomData.doors[_selectedItem];

                if (!_itemSelected) Console.CursorTop -= optionsCount;
            }
        }

        private void ItemSelected()
        {
            _itemSelected = true;
            Console.CursorVisible = true;
            new Door().OnInteraction.Invoke(this, new EventArgs(_finalItem));
        }
    }
}