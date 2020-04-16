# InputTransmiter

Input transmiter is a simple software that allows you to **remotly control** the keyboard of a computer.
You'll need this software on the "source computer" (client) and the computer that you need to control.

**<ins> When do I need to use this software ?</ins>**

You can use it for multiple purpose. But it was designed to play local muliplayer games remotly. What i mean by 
local mulitplayer game is a game where both players plays on the same keyboard (1st player WASD and the second one arrows).

So with this this software both players will be in their respective house and they'll be able to play a local multiplayer game !

**<ins> How to use it ?</ins>**

It's simple. First you'll need Hamachi (or similar software). After setting up Hamachi both players can open InputTransmiter.
Then the player that Host the game will click on the _server button_. This'll open a new window. In this window you have to enter a
valid port (later i'll create a button to automaticly set up the server.). Now the server is waiting for a client to connect.

On the second player (the one that wants to play remotly) software, click _Client_. This'll open another window. In the _ip_ text box, you'll have to enter the ip of the server (it can be found on hamachi) and in the _port_ box, enter the port, previously set on the server.

 Finaly on the server computer open the game and check the little box, and the remote player will have to click on _start keys sending_ and check if the software window is focused. Of course, you'll need a software like discord to share the screen.
 
 **<ins> How this works ?</ins>**
 
 This is super simple. The software reads the keyboard input, send it to the other computer. The software on the other computer 
 simulate the keys that are received.
 
 <h1>Technical informations and credits</h1>
 
 For this project I used **SimpleTcp** made by **BrandonPotter** :
 https://github.com/BrandonPotter/SimpleTCP
 
 And i used **InputSimulator** , to simulate the key presses :
 https://archive.codeplex.com/?p=inputsimulator
 
 
