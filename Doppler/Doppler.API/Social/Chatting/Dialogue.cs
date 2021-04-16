using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Social.Chatting
{
    public class Dialogue : Conversation
    {
        public ConversationMember FirstUser { get; set; }
        public ConversationMember SecondUser { get; set; }

        public override string Name
        {
            get
            {
                return $"Dialogue between {FirstUser?.DisplayName} and {SecondUser?.DisplayName}";
            }
        }

        public string GetName(User user)
        {
            if (FirstUser.User.Id == user.Id)
            {
                return SecondUser.DisplayName;
            }

            return FirstUser.DisplayName;
        }

        public string GetProfileImage(User user)
        {
            if (FirstUser.User.Id == user.Id)
            {
                return SecondUser.User.IconUrl;
            }

            return FirstUser.User.IconUrl;
        }
    }
}
