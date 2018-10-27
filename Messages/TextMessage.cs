using System;

namespace Messages
{
    public class TextMessage
    {
        public string Text { get; set; }
    }

    public class ObjectMessage
    {
        public string Text { get; set; }
        public DateTime OperationDate { get; set; }
    }
}