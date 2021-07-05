# Leder-Efter
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

<img src="https://user-images.githubusercontent.com/57122816/124402814-ca8e8680-dd5c-11eb-9250-6c9e6d375196.png" width="550" height="300"><br>
(Screenshot Feature - Account Manager)

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

<img src="https://user-images.githubusercontent.com/57122816/124402829-ebef7280-dd5c-11eb-9559-a3d85ed49853.png" width="550" height="300"><br>
<img src="https://user-images.githubusercontent.com/57122816/124402886-38d34900-dd5d-11eb-8bd3-04011267a20c.png" width="550" height="300"><br>
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
#### 3. Leave A Room
#### 4. Destroy A Room

### Player's Progress Handler
<img src="https://user-images.githubusercontent.com/57122816/124402840-fc9fe880-dd5c-11eb-9a23-364b8e645e88.png" width="550" height="300"><br>
(Screenshot Feature - Player's Progress Manager)

### Trivia Gameplay
<img src="https://user-images.githubusercontent.com/57122816/124402878-307b0e00-dd5d-11eb-9d81-3d56505fa635.png" width="550" height="300"><br>
<img src="https://user-images.githubusercontent.com/57122816/124402879-3244d180-dd5d-11eb-9c6b-41e7a170751f.png" width="550" height="300"><br>
(Screenshot Feature - Trivia Gameplay)
