using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Leder_Efter_Server
{
    [DataContract]
    class AccountDatabase
    {
        [DataMember]
        public int identity { get; set; }

        [DataMember]
        public bool active { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public int totalScore { get; set; }

        [DataMember]
        public int totalPlay { get; set; }

        public AccountDatabase(int id, bool on, string uname, string pass, int score, int play)
        {
            identity = id;
            active = on;
            username = uname;
            password = pass;
            totalScore = score;
            totalPlay = play;
        }
    }

    class AccountHandler
    {
        public static List<AccountDatabase> accountDatabaseTemp = new List<AccountDatabase>();

        public static string SignIn(string uname, string pass)
        {
            Server.accountDatabase = LoadDatabase<List<AccountDatabase>>("AccountDatabase.xml");

            foreach (AccountDatabase oacc in Server.accountDatabase)
            {
                if (uname == oacc.username && !oacc.active)
                {
                    if (pass == oacc.password)
                    {
                        oacc.active = true;
                        Console.WriteLine($"There's player signIn: {uname}");

                        oacc.identity = Client.identity;
                        SaveDatabase(Server.accountDatabase, "AccountDatabase.xml");
                        return "login was successful";
                    }
                    else
                    {
                        return "login failed! your password is wrong";
                    }
                }
                else if (uname == oacc.username && oacc.active)
                {
                    return "login failed! another user is using your account";
                }
            }

            return "login failed! your account's not found";
        }

        public static string SignUp(string uname, string pass)
        {
            foreach (AccountDatabase oacc in Server.accountDatabase)
            {
                if (uname == oacc.username)
                {
                    return "login failed! change your username";
                }
            }

            Server.accountDatabase.Add(new AccountDatabase(Client.identity, false, uname, pass, 0, 0));
            //SaveDatabase(Server.accountDatabase, "AccountDatabase.xml");
            Console.WriteLine($"There's player joined: {uname}");

            AddDataToDatabase();
            return "your account registered successfully";
        }

        public static void SignOut(int id)
        {
            foreach (AccountDatabase oacc in Server.accountDatabase)
            {
                if (id == oacc.identity)
                {
                    oacc.active = false;
                    oacc.identity = 0;
                    SaveDatabase(Server.accountDatabase, "AccountDatabase.xml");
                    Console.WriteLine($"{oacc.username} has disconnected.");
                }
            }
        }

        public static void AddScorePlay(string uname, int score, int play)
        {
            Server.accountDatabase = LoadDatabase<List<AccountDatabase>>("AccountDatabase.xml");

            foreach (AccountDatabase oacc in Server.accountDatabase)
            {
                if (uname == oacc.username)
                {
                    oacc.totalScore = score;
                    oacc.totalPlay = play;

                    Console.WriteLine($"Database Score {uname}: {oacc.totalScore}");
                    Console.WriteLine($"Database Play {uname}: {oacc.totalPlay}");
                }
            }
        }

        public static void ShowScorePlay(int fromClient, string uname)
        {
            Server.accountDatabase = LoadDatabase<List<AccountDatabase>>("AccountDatabase.xml");

            foreach (AccountDatabase oacc in Server.accountDatabase)
            {
                if (uname == oacc.username)
                {
                    ServerSend.SendScorePlay(fromClient, oacc.totalScore, oacc.totalPlay);
                }
            }
        }
        
        public static void AddDataToDatabase()
        {
            accountDatabaseTemp = LoadDatabase<List<AccountDatabase>>("AccountDatabase.xml");
            accountDatabaseTemp.AddRange(Server.accountDatabase);
            SaveDatabase(Server.accountDatabase, "AccountDatabase.xml");
        }

        public static void SaveDatabase<T>(T _serialazable, string _fileName)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            };
            var writer = XmlWriter.Create(_fileName, settings);

            serializer.WriteObject(writer, _serialazable);
            writer.Close();
        }

        public static T LoadDatabase<T>(string _fileName)
        {
            var fileStream = new FileStream(_fileName, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            var serializer = new DataContractSerializer(typeof(T));
            T serializable = (T)serializer.ReadObject(reader, true);

            reader.Close();
            fileStream.Close();
            return serializable;
        }
    }
}
