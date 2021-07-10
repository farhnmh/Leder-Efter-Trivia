# Leder-Efter-Trivia
![Screenshot_23](https://user-images.githubusercontent.com/57122816/124785216-df436800-df70-11eb-998f-495d1c84be22.gif)

LederEfter is a casual online game with the theme of knowledge trivia in several fields, such as animals, plants, countries and the world. Players will be given 10 true or false questions in one game, where the 10 questions will be displayed randomly and of course the same for each player. This game can be played by up to 30 players.
1. Genre: MMORPG, Casual, Trivia
2. Network Protocol: TCP
3. Platform: Windows

## Credit
(Author) 4210181002 Farhan Muhammad
<br>(Author) 4210181010 Ilham Jalu Prakosa
<br>(Course) Praktikum Desain Multiplayer Game Online

[![github](https://user-images.githubusercontent.com/57122816/125156839-cbc31780-e191-11eb-86d7-5b846c25a4fa.png)](http://www.github.com/farhnmh)
[![instagram](https://user-images.githubusercontent.com/57122816/125156881-0e84ef80-e192-11eb-955b-fbb996962df7.png)](https://www.instagram.com/farhaanmuha)
[![linkedin](https://user-images.githubusercontent.com/57122816/125156940-4f7d0400-e192-11eb-8b9c-5bc9cfa3f879.png)](https://www.linkedin.com/in/farhaanmuha)
[![itchio](https://user-images.githubusercontent.com/57122816/125157313-7d634800-e194-11eb-9c52-8ef389bd43b4.png)](https://farhaanada.itch.io/leder-efter-trivia)

## Resources Technology
Made With [Unity 2019 LTS](https://unity.com/)<br>
Server Deployed On [Google Cloud Service](https://cloud.google.com/)

## Diagram-Overview
### Flowchart-Explanation
<img src="https://user-images.githubusercontent.com/57122816/125156401-6bcb7180-e18f-11eb-8317-aa0b3aa17d31.png" width="650" height="450"><br>

### Class-Diagram-Explanation
<img src="https://user-images.githubusercontent.com/57122816/125160206-ec956800-e1a5-11eb-9bb6-67a477f9cf81.png" width="650" height="450"><br>

## Game-Feature
### Packets
This feature is used to replace multithreaded and multiclient functions. For the system, the way it works is to provide an identity like a packet delivery with its address, on every data sent or received by the server or each client. This will help with faster and more accurate data processing, due to packet giving and recognition.
#### 1. Packets Identifier
In this section, the function below will provide several identities that will later be used when sending or receiving packages.
#### 2. Packet Handlers
Meanwhile, in this section, the packet handlers function is one of the functions that will receive packets and their identities. Then it will give access to special functions to process each packet that has been received.
#### 3. Data Transfer Function
Here are some functions that can be used to send data with the appropriate protocol, TCP or UDP. However, in this project we use a function that applies only the TCP protocol.

### Account Database
The database account used in this project is still in the form of a local database using a file with an .xml extension which will later be applied to the storage and data recall functions on the server side.
#### 1. Sign In Account
Process Function: 
- client sends the username and password data
- server receives the data
- server validates the account on the database server
- validation results are sent back to the client
#### 2. Sign Up Account
Process Function:
- client sends username and password data
- server receives data
- the server validates the account on the database server
- if there is an account with the same username, then the sign up failed
- if not, then the sign up was successful
- validation results are sent back to the client

### Room Manager
In this room management feature, we use the list function as the database implementation, where the system will store room data, as well as all clients in that room.
#### 1. Host A Room
Process Function:
- the client sends the room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the process will fail
- if not, the process will be successful
- server validation results are sent to the client
#### 2. Join A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will broadcast client data to other clients
- otherwise the process will fail
- server validation result sent to client
#### 3. Leave A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will broadcast client data to other clients for deletion from database
- otherwise the process will fail
- server validation result sent to client
#### 4. Destroy A Room
Process Function:
- client sends room code
- the server receives the code and validates whether there is a room with the same code
- if there is, the server will delete the room and its database
- otherwise the process will fail
- server validation result sent to client

### Player's Progress Handler
In this feature, all game progress data for each client will be stored in the account database previously described. All of this data will be displayed on the main menu in the game.
Process Function:
- one statement, players will be given 5 seconds to answer
- one game, there will be 10 statements that must be answered
- when the statement that must be answered is finished, then the game is over
- the client will send the latest total score and total game data to the server
- the server will update the data in its database

### Trivia Gameplay
The core mechanics in this game are in the gameplay section, where in this game, each player or client is required to choose between true or false answers from several statements related to knowledge about animals, plants, countries and the world.
Process Function:
- the server sends data questions that will appear in one game
- the question is displayed, and the client sends the control character to the server
- if the player character chooses the correct answer, the server will send the score
- when the game ends, the player will send the final score to the server
- the latest score received by the server, will be entered into the server's database

## Game-Documentation
### Sign Up Sign In Account
<img src="https://user-images.githubusercontent.com/57122816/124780492-d8b2f180-df6c-11eb-9076-c993de9136ac.gif" width="550" height="300"><br>
(Screenshot Feature - Sign Up and Sign In Account)

### Room Manager
<img src="https://user-images.githubusercontent.com/57122816/124784041-bff80b00-df6f-11eb-909d-8e0c9b4af2da.gif" width="550" height="300"><br>
(Screenshot Feature - Room Manager)

### Progress Handler
<img src="https://user-images.githubusercontent.com/57122816/124402840-fc9fe880-dd5c-11eb-9a23-364b8e645e88.png" width="550" height="300"><br>
(Screenshot Feature - Player's Progress Manager)

### Trivia Gameplay
<img src="https://user-images.githubusercontent.com/57122816/124784781-71973c00-df70-11eb-9028-3c427188e9f7.gif" width="550" height="300"><br>
(Screenshot Feature - Trivia Singleplayer Gameplay)

<img src="https://user-images.githubusercontent.com/57122816/124785216-df436800-df70-11eb-998f-495d1c84be22.gif" width="550" height="300"><br>
(Screenshot Feature - Trivia Multiplayer Gameplay)
