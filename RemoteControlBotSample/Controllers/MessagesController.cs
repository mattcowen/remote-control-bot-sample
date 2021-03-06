﻿using Microsoft.Bot.Connector;
using RemoteControlBotSample.Dialogs;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;

namespace RemoteControlBotSample
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // Option 1: Direct handling
                // If you want to go with this option, do remember to remove the registration
                // of the scorables in Global.asax.cs.
                /*MessageRouting.MessageRouterManager.Instance.MakeSurePartiesAreTracked(activity);
                string notificationData = string.Empty;

                if (Notifications.NotificationsManager.TryGetNotificationData(activity, out notificationData))
                {
                    // A notification related backchannel message was detected
                    await Notifications.NotificationsManager.SendNotificationAsync(notificationData);
                }
                else
                {
                    await Conversation.SendAsync(activity, () => new RootDialog());
                }*/

                // Option 2: Using inversion of control (IoC) container
                // See Global.asax.cs and GlobalMessageHandlerModule.cs
                await Conversation.SendAsync(activity, () => new RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}