using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapERP.Models
{
    public class VisitorsMessage:BranchSetup
    {
        private string _visitorsID;
        private string _name;
        private string _emailAddress;
        private string _phoneNumber;
        private string _messageID;
        private string _messageText;
        private int _applicationID;
        private DataTable _dtMessages;
        private string _entryDate;
        private string _replyMessageText;
        private string _ccEmailAddress;
        private string _messageSubject;
        private string _messageTypeID;
        private int _messageCategoryID;
        private int _messageSubCategoryID;
        private string _fromDate;
        private string _toDate;
        private string _emailActionType;
        private string _messageChannel;



        public string VisitorsID { get => _visitorsID; set => _visitorsID = value; }
        public string Name { get => _name; set => _name = value; }
        public string EmailAddress { get => _emailAddress; set => _emailAddress = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string MessageID { get => _messageID; set => _messageID = value; }
        public string MessageText { get => _messageText; set => _messageText = value; }
        public int ApplicationID { get => _applicationID; set => _applicationID = value; }
        public DataTable DtMessages { get => _dtMessages; set => _dtMessages = value; }
        public string EntryDate { get => _entryDate; set => _entryDate = value; }
        public string ReplyMessageText { get => _replyMessageText; set => _replyMessageText = value; }
        public string CcEmailAddress { get => _ccEmailAddress; set => _ccEmailAddress = value; }
        public string MessageSubject { get => _messageSubject; set => _messageSubject = value; }
        public string MessageTypeID { get => _messageTypeID; set => _messageTypeID = value; }
        public int MessageCategoryID { get => _messageCategoryID; set => _messageCategoryID = value; }
        public int MessageSubCategoryID { get => _messageSubCategoryID; set => _messageSubCategoryID = value; }
        public string FromDate { get => _fromDate; set => _fromDate = value; }
        public string ToDate { get => _toDate; set => _toDate = value; }
        public string EmailActionType { get => _emailActionType; set => _emailActionType = value; }
        public string MessageChannel { get => _messageChannel; set => _messageChannel = value; }
    }
}