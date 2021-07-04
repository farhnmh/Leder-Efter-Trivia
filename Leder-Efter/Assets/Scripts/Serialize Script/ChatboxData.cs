namespace ClientData
{
    [System.Serializable]
    class Chatbox
    {
        public string username;
        public string message;

        public Chatbox(string uname, string msg)
        {
            username = uname;
            message = msg;
        }
    }
}