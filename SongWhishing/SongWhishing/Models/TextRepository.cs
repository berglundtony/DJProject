using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SongWishing.Models
{
    public class TextRepository
    {
        const string _csvFilename = "App_Data/Text.csv";
        static readonly List<MessageViewModel> _texts;

        static TextRepository()
        {
            string path = Path.Combine(HttpRuntime.AppDomainAppPath, _csvFilename);
            _texts = new List<MessageViewModel>();

            int id = 1;
            foreach (var line in File.ReadAllLines(path))
            {
                var lineSegments = line.Split(',');
                var text = lineSegments[0];

                var texts = new MessageViewModel { Id = id++, Text = text };
                _texts.Add(texts);
            }
        }


        public MessageViewModel[] FindTexts(string filter)
        {

            var texts = from text in _texts
                        where text.DisplayName.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase)
                        select text;
            return texts.ToArray();
        }

    }
}