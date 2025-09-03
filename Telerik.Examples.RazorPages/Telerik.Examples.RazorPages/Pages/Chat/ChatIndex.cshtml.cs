using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Pages.Chat
{   
    public class ChatIndexModel : PageModel
    {
        public static List<ChatMessage> messages;

        public void OnGet(string culture)
        {
            if (!String.IsNullOrEmpty(culture))
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);
            }

            if (messages == null)
            {
                messages = GetData();
            }
        }
        public JsonResult OnPostReadMessages()
        {
            var dsResult = new DataSourceResult
            {
                Data = messages.Select(message => new
                {
                    message.Id,
                    message.AuthorId,
                    message.AuthorName,
                    message.AuthorImageUrl,
                    message.AuthorImageAltText,
                    message.Text,
                    message.TimeStamp,
                    message.IsDeleted,
                    message.IsPinned,
                    message.IsTyping,
                    Files = message.Files.Select(file => new
                    {
                        extension = file.Extension,
                        size = file.Size,
                        type = file.Type,
                        name = file.Name
                    })
                })
            };
            return new JsonResult(dsResult);
        }

        public JsonResult OnPostCreateMessage([DataSourceRequest] DataSourceRequest request, ChatMessage message)
        {
            message.Id = Guid.NewGuid().ToString();
            // Set the Message ID explicitly and perform a custom create operation to the database.
            var messageObject = new
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                AuthorName = message.AuthorName,
                AuthorImageUrl = message.AuthorImageUrl,
                AuthorImageAltText = message.AuthorImageAltText,
                Text = message.Text,
                TimeStamp = message.TimeStamp,
                IsDeleted = message.IsDeleted,
                IsPinned = message.IsPinned,
                IsTyping = message.IsTyping,
                Files = message.Files?.Select(file => new
                {
                    extension = file.Extension,
                    size = file.Size,
                    type = file.Type,
                    name = file.Name
                }).ToList() ?? []
            };
            return new JsonResult(new[] { messageObject }.ToDataSourceResult(request));
        }

        public JsonResult OnPostUpdateMessage([DataSourceRequest] DataSourceRequest request, ChatMessage message)
        {
            // Perform a custom update operation to the existing database.
            var messageObject = new
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                AuthorName = message.AuthorName,
                AuthorImageUrl = message.AuthorImageUrl,
                AuthorImageAltText = message.AuthorImageAltText,
                Text = message.Text,
                TimeStamp = message.TimeStamp,
                IsDeleted = message.IsDeleted,
                IsPinned = message.IsPinned,
                IsTyping = message.IsTyping,
                Files = message.Files?.Select(file => new
                {
                    extension = file.Extension,
                    size = file.Size,
                    type = file.Type,
                    name = file.Name
                }).ToList() ?? []
            };
            return new JsonResult(new[] { messageObject }.ToDataSourceResult(request));
        }

        private static List<ChatMessage> GetData()
        {
            return new List<ChatMessage>()
            {
                 new ChatMessage {
                     Id = "1",
                     AuthorId = "1",
                     AuthorName = "Lora",
                     AuthorImageUrl = "https://demos.telerik.com/aspnet-core/shared/web/Customers/ANATR.jpg",
                     AuthorImageAltText = "Lora's profile picture",
                     Text = "Hey Emma! I just booked my trip to Japan for next month! I'm so excited but also a bit nervous since it's my first time there. Any tips?",
                     TimeStamp = new DateTime(2025, 7, 20),
                     IsPinned = true,
                     IsTyping = false,
                     IsDeleted = false
                 },
                 new ChatMessage {
                     Id = "2",
                     AuthorId = "2",
                     AuthorName = "Emma",
                     AuthorImageUrl = "https://demos.telerik.com/aspnet-core/shared/web/Customers/DUMON.jpg",
                     AuthorImageAltText = "Emma's profile picture",
                     Text = "Don't miss the cherry blossoms if they're still blooming! And try the street food in Osaka - it's the best!",
                     TimeStamp = new DateTime(2025, 7, 20),
                     IsPinned = false,
                     IsTyping = false,
                     IsDeleted = false
                 },
                 new ChatMessage {
                     Id = "3",
                     AuthorId = "1",
                     AuthorName = "Lora",
                     AuthorImageUrl = "https://demos.telerik.com/aspnet-core/shared/web/Customers/ANATR.jpg",
                     AuthorImageAltText = "Lora's profile picture",
                     Text = "Perfect! I'm definitely going during cherry blossom season. Should I book accommodations in advance, or can I find places as I go?",
                     TimeStamp = new DateTime(2025, 7, 20),
                     IsPinned = false,
                     IsTyping = false,
                     IsDeleted = false
                 },
                 new ChatMessage
                 {
                     Id = "4",
                     AuthorId = "2",
                     AuthorName = "Emma",
                     AuthorImageUrl = "https://demos.telerik.com/aspnet-core/shared/web/Customers/DUMON.jpg",
                     AuthorImageAltText = "Emma's profile picture",
                     Text = "Definitely book in advance, especially during cherry blossom season! It gets super busy. I recommend staying in a ryokan in Kyoto for the traditional experience. Here you will find the schedule we used when we were there.",
                     TimeStamp = new DateTime(2025, 7, 20),
                     IsPinned = false,
                     IsTyping = false,
                     IsDeleted = false,
                     Files = new List<ChatFile>
                     {
                         new ChatFile
                         {
                             Name = "Sightseeing_schedule.pdf",
                             Size = 245760,
                             Extension = "pdf",
                             Type = "application/pdf"
                         },
                         new ChatFile
                         {
                             Name = "Favourite_place.jpg",
                             Size = 156000,
                             Extension = "jpg",
                             Type = "image/jpeg"
                         }
                     }
                 },
                 new ChatMessage
                 {
                     Id = "5",
                     AuthorId = "1",
                     AuthorName = "Lora",
                     AuthorImageUrl = "https://demos.telerik.com/aspnet-core/shared/web/Customers/ANATR.jpg",
                     AuthorImageAltText = "Lora's profile picture",
                     Text = "Thanks for all the advice, Emma. I'll definitely send you photos!",
                     TimeStamp = new DateTime(2025, 7, 20),
                     IsPinned = false,
                     IsTyping = false,
                     IsDeleted = false
                 }
            };
        }
    }
}
