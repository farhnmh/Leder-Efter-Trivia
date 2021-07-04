namespace ClientData
{
    [System.Serializable]
    public class Account
    {
        public string username;
        public string password;

        public Account(string uname, string pass)
        {
            username = uname;
            password = pass;
        }
    }
}