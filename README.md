# Leder-Efter-Trivia
![Screenshot_23](https://user-images.githubusercontent.com/57122816/124402879-3244d180-dd5d-11eb-9c6b-41e7a170751f.png)

## Development Team
1. (Author) 4210181002 Farhan Muhammad
2. (Author) 4210181010 Ilham Jalu Prakosa
3. (Course) Praktikum Desain Multiplayer Game Online

## Game-Overview
LederEfter is a casual online game with the theme of knowledge trivia in several fields, such as animals, plants, countries and the world. Players will be given 10 true or false questions in one game, where the 10 questions will be displayed randomly and of course the same for each player. This game can be played by up to 30 players.

## Game-Detail
1. Genre: MMORPG, Casual, Trivia
2. Network Protocol: TCP
3. Platform: Windows

## Game-Engine
Unity 2019 LTS

## Document-Information-Detail
You can download the first released in [itch.io](https://docs.google.com/presentation/d/1IT5tO_OZ1EZySGK0vKDDZZpI3ZeyAXCc-Gt6M5B67sk/edit?usp=sharing)<br>
High Concept Document can be accessed in [here](https://docs.google.com/presentation/d/1IT5tO_OZ1EZySGK0vKDDZZpI3ZeyAXCc-Gt6M5B67sk/edit?usp=sharing)<br>
Game Concept Document can be accessed in [here](4210181002_4210181010_GDD.docx)

## Game-Feature-and-Documentation
### Packets
This feature is used to replace multithreaded and multiclient functions. For the system, the way it works is to provide an identity like a packet delivery with its address, on every data sent or received by the server or each client. This will help with faster and more accurate data processing, due to packet giving and recognition.
#### 1. Packets Identifier
In this section, the function below will provide several identities that will later be used when sending or receiving packages.
##### Server Side
```C#
public enum ServerPackets
    {
        welcome = 1,
        signIn,
        signUp,
        hostRoom,
        joinRoom,
        leaveRoom,
        destroyRoom,
        playerJoined,
        startMatch,
        playerPosition,

        chatbox,
        triviaQuestion,
        triviaDatabase,
        scorePlaySent
    }
```

##### Client Side
```C#
public enum ClientPackets
{
    welcomeRequest = 1,
    signInRequest,
    signUpRequest,
    hostRoomRequest,
    joinRoomRequest,
    leaveRoomRequest,
    destroyRoomRequest,
    startMatchRequest,
    playerPositionRequest,

    chatboxRequest,
    triviaQuestionRequest,
    triviaDatabaseRequest,
    storeScorePlay,
    scorePlayRequest
}
```

#### 2. Packet Handlers
Meanwhile, in this section, the packet handlers function is one of the functions that will receive packets and their identities. Then it will give access to special functions to process each packet that has been received.
##### Server Side
```C#
packetHandlers = new Dictionary<int, PacketHandler>()
  {
    { (int)ClientPackets.welcomeRequest, ServerHandle.WelcomeReceived },
    { (int)ClientPackets.signInRequest, ServerHandle.SignInReceived },
    { (int)ClientPackets.signUpRequest, ServerHandle.SignUpReceived },
    { (int)ClientPackets.hostRoomRequest, ServerHandle.HostRoomReceived },
    { (int)ClientPackets.joinRoomRequest, ServerHandle.JoinRoomReceived },
    { (int)ClientPackets.leaveRoomRequest, ServerHandle.LeaveRoomReceived },
    { (int)ClientPackets.destroyRoomRequest, ServerHandle.DestroyRoomReceived },
    { (int)ClientPackets.startMatchRequest, ServerHandle.StartMatchReceived },
    { (int)ClientPackets.playerPositionRequest, ServerHandle.PlayerPositionReceived },

    { (int)ClientPackets.chatboxRequest, ServerHandle.ChatboxReceived },
    { (int)ClientPackets.triviaQuestionRequest, ServerHandle.TriviaQuestionReceived },
    { (int)ClientPackets.triviaDatabaseRequest, ServerHandle.TriviaDatabaseReceived },
    { (int)ClientPackets.storeScorePlay, ServerHandle.ScorePlayReceived },
    { (int)ClientPackets.scorePlayRequest, ServerHandle.ScorePlayerSent }
  };
```

##### Client Side
```C#
packetHandlers = new Dictionary<int, PacketHandler>()
  {
    { (int)ServerPackets.welcome, ClientHandle.Welcome },
    { (int)ServerPackets.signIn, ClientHandle.SignInValidation },
    { (int)ServerPackets.signUp, ClientHandle.SignUpValidation },
    { (int)ServerPackets.hostRoom, ClientHandle.HostRoomValidation },
    { (int)ServerPackets.joinRoom, ClientHandle.JoinRoomValidation },
    { (int)ServerPackets.leaveRoom, ClientHandle.LeaveRoomValidation },
    { (int)ServerPackets.destroyRoom, ClientHandle.DestroyRoomValidation },
    { (int)ServerPackets.startMatch, ClientHandle.StartMatchValidation },
    { (int)ServerPackets.playerPosition, ClientHandle.PlayerPositionValidation },

    { (int)ServerPackets.playerJoined, ClientHandle.AddPlayerToDatabase },
    { (int)ServerPackets.chatbox, ClientHandle.ChatboxValidation },
    { (int)ServerPackets.triviaQuestion, ClientHandle.TriviaQuestionValidation },
    { (int)ServerPackets.triviaDatabase, ClientHandle.TriviaDatabaseValidation },
    { (int)ServerPackets.scorePlaySent, ClientHandle.ScorePlayReceiver }
  };
```

#### 3. Data Transfer Function
Here are some functions that can be used to send data with the appropriate protocol, TCP or UDP. However, in this project we use a function that applies only the TCP protocol.
```C#
private static void SendTCPData(int _toClient, Packet _packet)
{
    _packet.WriteLength();
    Server.clients[_toClient].tcp.SendData(_packet);
}

private static void SendUDPData(int _toClient, Packet _packet)
{
    _packet.WriteLength();
    Server.clients[_toClient].udp.SendData(_packet);
}

private static void SendTCPDataToAll(Packet _packet)
{
    _packet.WriteLength();
    for (int i = 1; i <= Server.MaxPlayers; i++)
    {
       Server.clients[i].tcp.SendData(_packet);
    }
}

private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
{
     _packet.WriteLength();
    for (int i = 1; i <= Server.MaxPlayers; i++)
    {
       if (i != _exceptClient)
       Server.clients[i].tcp.SendData(_packet);
    }
}

private static void SendUDPDataToAll(Packet _packet)
{
   _packet.WriteLength();
    for (int i = 1; i <= Server.MaxPlayers; i++)
    {
       Server.clients[i].udp.SendData(_packet);
    }
}

private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
{
    _packet.WriteLength();
    for (int i = 1; i <= Server.MaxPlayers; i++)
    {
       if (i != _exceptClient)
       Server.clients[i].udp.SendData(_packet);
    }
}
```

### Account Database
The database account used in this project is still in the form of a local database using a file with an .xml extension which will later be applied to the storage and data recall functions on the server side.

<img src="https://user-images.githubusercontent.com/57122816/124780492-d8b2f180-df6c-11eb-9076-c993de9136ac.gif" width="550" height="300"><br>
(Screenshot Feature - Sign Up and Sign In Account)

The function below is used to declare the attributes and contents of the account database owned by the server
```C#
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
```

#### 1. Sign In Account
Process Function: 
- client sends the username and password data
- server receives the data
- server validates the account on the database server
- validation results are sent back to the client
##### Server Side
- The SignInReceived() is a function that will process the data sent by the client to sign in the account.
```C#
public static void SignInReceived(int _fromClient, Packet _packet)
{
  ClientData.Account _account = _packet.ReadObject<ClientData.Account>();

  string validation = AccountHandler.SignIn(_account.username, _account.password);
  if (validation == "login was successful")
      Server.readyDatabase.Add(new ReadyDatabase(_fromClient, false));

  ServerSend.SignInValidation(_fromClient, validation);
}
```

- The SignIn() is a function that performs account validation requested by the client.
```C#
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
```

- The SignInValidation() is a function that will send the validation results that have been done by the previous server server
```C#
public static void SignInValidation(int _toClient, string _msg)
{
    using (Packet _packet = new Packet((int)ServerPackets.signIn))
    {
        _packet.Write(_msg);
        SendTCPData(_toClient, _packet);
    }
}
```

##### Client Side
- The SignInRequest() is a function used by the client to request access to the account you want to use.
```C#
public static void SignInRequest(string uname, string pass)
{
    using (Packet _packet = new Packet((int)ClientPackets.signInRequest))
    {
        ClientData.Account _account = new ClientData.Account(uname, pass);

        _packet.Write<ClientData.Account>(_account);
        SendTCPData(_packet);
    }
}
```

#### 2. Sign Up Account
Process Function:
- client sends username and password data
- server receives data
- the server validates the account on the database server
- if there is an account with the same username, then the sign up failed
- if not, then the sign up was successful
- validation results are sent back to the client
##### Server Side
- The SignUpReceived() is a function that will process the data sent by the client to sign up the account.
```C#
public static void SignUpReceived(int _fromClient, Packet _packet)
{
    ClientData.Account _account = _packet.ReadObject<ClientData.Account>();
    string validation = AccountHandler.SignUp(_account.username, _account.password);
    ServerSend.SignUpValidation(_fromClient, validation);
}
```

- The SignUp() is a function that will perform the processing of a new account registration on the database server.
```C#
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
    Console.WriteLine($"There's player joined: {uname}");

    AddDataToDatabase();
    return "your account registered successfully";
}
```

##### Client Side
- The SignInRequest() is a function used by the client to request access to the account you want to use.
```C#
public static void SignUpRequest(string uname, string pass)
{
    using (Packet _packet = new Packet((int)ClientPackets.signUpRequest))
    {
        ClientData.Account _account = new ClientData.Account(uname, pass);

        _packet.Write<ClientData.Account>(_account);
        SendTCPData(_packet);
    }
}
```

### Room Manager
In this room management feature, we use the list function as the database implementation, where the system will store room data, as well as all clients in that room.

<img src="https://user-images.githubusercontent.com/57122816/124784041-bff80b00-df6f-11eb-909d-8e0c9b4af2da.gif" width="550" height="300"><br>
(Screenshot Feature - Room Manager)

The function below is used to create and implement a room database
```C#
class RoomDatabase
{
    public string code { get; set; }
    public bool isPlay { get; set; }

    public List<PlayerJoinedDatabase> playerJoinedDatabase = new List<PlayerJoinedDatabase>();

    public RoomDatabase(string _code)
    {
        code = _code;
    }
}

class PlayerJoinedDatabase
{
    public int id { get; set; }
    public string username { get; set; }
    public int type { get; set; }

    public PlayerJoinedDatabase(int _id, string _uname, int _type)
    {
        id = _id;
        username = _uname;
        type = _type;
    }
}
```

#### 1. Host A Room
Process Function:
- the client sends the room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the process will fail
- if not, the process will be successful
- server validation results are sent to the client
##### Server Side
- The HostRoomValidation() is a function that will process room validation whether there is a room with the same code as the one sent by the client.
```C#
public static string HostRoomValidation(string code)
{
    foreach (RoomDatabase oroom in roomDatabase)
    {
        if (code == oroom.code)
        {
            return "create failed! change your room code or join with that code";
        }
    }

    HostRoom(code);
    return "room created succesfully";
}
```

- The HostRoom() is a function that will open a new room if the room code sent by the client is unique.
```C#
public static void HostRoom(string code)
{
    roomDatabase.Add(new RoomDatabase(code));
    Console.WriteLine($"Room Created Succesfully w/ Code: {code}");
}
```

##### Client Side
- The HostRoomValidation() is a function that will make the client a host if the room with the code that has been sent is successfully created.
```C#
public static void HostRoomValidation(Packet _packet)
{
    string _msg = _packet.ReadString();
    UIMenuManager.instance.notifText.text = _msg;

    if (_msg == "room created succesfully")
    {
        Client.instance.isHost = true;
    }
}
```

#### 2. Join A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will broadcast client data to other clients
- otherwise the process will fail
- server validation result sent to client
##### Server Side
- The JoinRoomValidation() is a function that will process room validation whether there is a room with the same code as the one sent by the client.
```C#
public static string JoinRoomValidation(string code, int id, string uname)
{
    foreach (RoomDatabase oroom in roomDatabase)
    {
        if (code == oroom.code)
        {
            JoinRoom(code, id, uname);
            return "joined succesfully";
        }
    }

    return "join failed! change your room code or host a new room";
}
```

- The JoinRoom() is a function that will enter the client into the room with the same code according to the client request, then send the relevant client data to all existing clients.
```C#
public static void JoinRoom(string _code, int id,string _uname)
{
    for (int i = 0; i < roomDatabase.Count; i++)
    {
        if (_code == roomDatabase[i].code)
        {
            bool playerJoined = false;
            foreach (PlayerJoinedDatabase oplayer in roomDatabase[roomDatabase.Count - 1].playerJoinedDatabase)
            {
                if (_uname == oplayer.username)
                    playerJoined = true;
            }

            if (!playerJoined)
            {
                roomDatabase[i].playerJoinedDatabase.Add(new PlayerJoinedDatabase(id, _uname, 0));

                for (int j = 0; j < roomDatabase[i].playerJoinedDatabase.Count; j++)
                {
                    ServerSend.BroadcastPlayerJoined(_code, roomDatabase[i].playerJoinedDatabase[j].id,                                               roomDatabase[i].playerJoinedDatabase[j].username);
                }
            }
        }
    }
}
```

##### Client Side
- The JoinRoomValidation() is a function that will add a client to an existing room with the code that has been sent.
```C#
public static void JoinRoomValidation(Packet _packet)
{
    string _msg = _packet.ReadString();
    UIMenuManager.instance.notifText.text = _msg;

    if (_msg == "joined succesfully")
    {
        Client.instance.isPlay = true;
    }
}
```

- The AddPlayerToDatabase() is a function that will accept all client data that joins the room with the same code.
```C#
public static void AddPlayerToDatabase(Packet _packet)
{
    string _codeRoom = _packet.ReadString();
    int _id = _packet.ReadInt();
    string _uname = _packet.ReadString();

    if (_codeRoom == RoomDatabase.instance.roomCode)
    {
        RoomDatabase.instance.AddPlayerToDatabase(_id, _uname);
    }
}
```

#### 3. Leave A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will broadcast client data to other clients for deletion from database
- otherwise the process will fail
- server validation result sent to client
##### Server Side
- The LeaveRoom() is a function that will delete client data that has sent the room code to get out of the room.
```C#
public static void LeaveRoom(string _code, string _uname)
{
    for (int i = 0; i < roomDatabase.Count; i++)
    {
        if (_code == roomDatabase[i].code)
        {
            for (int j = 0; j < roomDatabase[i].playerJoinedDatabase.Count; j++)
            {
                if (_uname == roomDatabase[i].playerJoinedDatabase[j].username)
                {
                    roomDatabase[i].playerJoinedDatabase.RemoveAt(j);
                    Console.WriteLine($"Player-{_uname} Leave Room w/ Code: {_code}");
                }
            }
        }
    }
}
```

##### Client Side
- The LeaveRoomValidation() is a function that will delete client data that has sent the room code to get out of the room.
```C#
public static void LeaveRoomValidation(Packet _packet)
{
    string _codeRoom = _packet.ReadString();
    string _uname = _packet.ReadString();

    if (_codeRoom == RoomDatabase.instance.roomCode)
    {
        RoomDatabase.instance.RemovePlayerFromDatabase(_uname);
        UILobbyManager.instance.playerLeft = true;
    }
}
```

#### 4. Destroy A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will delete the room and its database
- otherwise the process will fail
- server validation result sent to client
##### Server Side
- The DestroyRoom() is a function that will delete the database room and all clients in it.
```C#
public static void DestroyRoom(string _code)
{
    for (int i = 0; i < roomDatabase.Count; i++)
    {
        if (_code == roomDatabase[i].code)
        {
            roomDatabase.RemoveAt(i);
            Console.WriteLine($"Room Destroyed Succesfully w/ Code: {_code}");
        }
    }
}
```

##### Client Side
- The DestroyRoomValidation() is a function that will delete the database room and all clients in it.
```C#
public static void DestroyRoomValidation(Packet _packet)
{
    string _codeRoom = _packet.ReadString();

    if (_codeRoom == RoomDatabase.instance.roomCode)
    {
        RoomDatabase.instance.RemoveDatabase();
    }
}
```

### Player's Progress Handler
In this feature, all game progress data for each client will be stored in the account database previously described. All of this data will be displayed on the main menu in the game.

<img src="https://user-images.githubusercontent.com/57122816/124402840-fc9fe880-dd5c-11eb-9a23-364b8e645e88.png" width="550" height="300"><br>
(Screenshot Feature - Player's Progress Manager)

Process Function:
- one statement, players will be given 5 seconds to answer
- one game, there will be 10 statements that must be answered
- when the statement that must be answered is finished, then the game is over
- the client will send the latest total score and total game data to the server
- the server will update the data in its database
##### Server Side
- The ScorePlayReceived() is a function that will receive all the latest client total score and total game data.
```C#
public static void ScorePlayReceived(int _fromClient, Packet _packet)
{
    string uname = _packet.ReadString();
    int score = _packet.ReadInt();
    int play = _packet.ReadInt();

    AccountHandler.AddScorePlay(uname, score, play);
}
```

- The AddScorePlay() is a function that will save the previously received data into the account database.
```C#
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
```

##### Client Side
- This function is a function that will send data on the total score and total game to the server.
```C#
if (trivia.Count == 0)
{
    Client.instance.myScore += score;
    Client.instance.myPlay++;

    ClientSend.UpScorePlay(Client.instance.myUname, 
                           Client.instance.myScore,
                           Client.instance.myPlay);

    Debug.Log($"Total Score: {Client.instance.myScore}");
    Debug.Log($"Total Play: {Client.instance.myPlay}");

    gameOverPanel.SetActive(true);
    scoreFinalText.text = $"Your Score's: {score}";
    isPlay = false;
}
```

### Trivia Gameplay
The core mechanics in this game are in the gameplay section, where in this game, each player or client is required to choose between true or false answers from several statements related to knowledge about animals, plants, countries and the world.

<img src="https://user-images.githubusercontent.com/57122816/124784781-71973c00-df70-11eb-9028-3c427188e9f7.gif" width="550" height="300"><br>
<img src="https://user-images.githubusercontent.com/57122816124785216-df436800-df70-11eb-998f-495d1c84be22.gif" width="550" height="300"><br>
(Screenshot Feature - Trivia Gameplay)

Process Function:
- the server sends data questions that will appear in one game
- the question is displayed, and the client sends the control character to the server
- if the player character chooses the correct answer, the server will send the score
- when the game ends, the player will send the final score to the server
- the latest score received by the server, will be entered into the server's database
##### Server Side
- The SetDatabase() is a function that sends 10 data questions what will appear to all clients who join in one room.
```C#
public static void SetDatabase(string codeRoom, int maxCategory, int maxQuestion)
{
    List<int> numberTemp = new List<int>();

    for (int i = 0; i < maxQuestion; i++)
    {
        int categoryResult = 0;
        int questionResult = 0;

        Random rand = new Random(DateTime.Now.Millisecond);

        do
        {
            categoryResult = rand.Next(0, maxCategory);
            questionResult = rand.Next(0, maxQuestion);
        } while (numberTemp.Contains(questionResult));
        
        numberTemp.Add(questionResult);                                
        ServerSend.TriviaDatabaseBroadcast(codeRoom, categoryResult, questionResult);
    }
}
```

- The SetQuestion() is a function that sends one question out of 10 already submitted at the start of the game.
```C#
public static void SetQuestion(string codeRoom, bool ready, int maxQuestion)
{
    int questionResult = 0;

    if (ready)
    {
        Random rand = new Random();
        questionResult = rand.Next(0, maxQuestion);
        ServerSend.TriviaQuestionBroadcast(codeRoom, questionResult);
    }
}
```

##### Client Side
- The SetDatabase is a function that accepts 10 questions from the server to be displayed in one game.
```C#
public void SetDatabase(string codeRoom, int categoryResult, int questionResult)
{
    if (RoomDatabase.instance.roomCode == codeRoom) {
        if (categoryResult == 0)
            trivia.Add(TriviaDatabase.instance.triviaHewan[questionResult]);
        else if (categoryResult == 1)
            trivia.Add(TriviaDatabase.instance.triviaTumbuhan[questionResult]);
        else if (categoryResult == 2)
            trivia.Add(TriviaDatabase.instance.triviaNegara[questionResult]);
        else if (categoryResult == 3)
            trivia.Add(TriviaDatabase.instance.triviaDunia[questionResult]);
    }
}
```

- The SetQuestion is a function that receives and returns one query from the one sent by the server.
```C#
public void SetQuestion(string codeRoom, int questionResult)
{
    if (RoomDatabase.instance.roomCode == codeRoom) {
        questionTemp = questionResult;
        questionText.text = $"{trivia[questionResult].question}";
        questionFix = trivia[questionResult].question;
        answerFix = trivia[questionResult].answer;
        StartCoroutine(QuestionCountDown());
    }
}
```
