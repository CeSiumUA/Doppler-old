using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Social.Chatting
{
    public class Dialogue : Conversation
    {
        //public long FirstUserId { get; set; }
        public ConversationMember FirstUser { get; set; }
        //public long SecondUserId { get; set; }
        public ConversationMember SecondUser { get; set; }

        public override string Name
        {
            get
            {
                return GetName(userContext);
            }
        }

        public override string IconUrl
        {
            get
            {
                return RetreiveProfileIconValue(userContext);
            }
        }

        public void SetUserContext(User user)
        {
            this.userContext = user;
        }
        private string GetName(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            if (FirstUser.User.Id == user.Id)
            {
                return SecondUser.DisplayName;
            }

            return FirstUser.DisplayName;
        }

        private string RetreiveProfileIconValue(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            if (FirstUser.User.Id == user.Id)
            {
                return SecondUser.User.IconUrl;
            }

            return FirstUser.User.IconUrl;
        }

        private User userContext;
    }
}
