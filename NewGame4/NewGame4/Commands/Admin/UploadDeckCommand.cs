using System;
using System.Collections.Generic;
using System.IO;
using fastJSON;
using Microsoft.AspNetCore.Http;
using NewGame4.Commands.Base;
using NewGame4.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;

namespace NewGame4.Commands.Admin
{
    public class UploadDeckCommand : ExecuteCommand
    {
        private string _name { get; }
        private string _shirt { get; }
        private Dictionary<string, object> _cards { get; }

        public UploadDeckCommand(IFormCollection data, HttpResponse response, HttpRequest request) : base(response,
            request)
        {
            NameCommand = nameof(UploadDeckCommand);
            _cards = (Dictionary<string, object>) JSON.Parse(data["json"]);
        }

        public override void Execute(ServerContext context)
        {
            var name = _cards.GetString("name");
            var shirt = _cards.GetString("shirt");
            var cards = _cards.GetNode("cards");

            FileHelper.CreateDirectory($"{name}");

            foreach (var card in cards)
            {
                Console.WriteLine(card.Key);

                var writePath = $"{name}/{card.Key}.jpg";
                
                // Image<Rgba32> image = Image.Load(Convert.FromBase64String(card.Value.ToString()));

                try
                {
                    byte[] bitmap = Convert.FromBase64String(card.Value.ToString()!);

                    using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                    {
                        using (Image image = Image.FromStream(new MemoryStream(bitmap)))
                        {
                            image.Save($"{card.Key}.jpg", ImageFormat.Jpeg);
                            sw.WriteLine(image);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            Send();
        }
    }
}