# Discord-Twitter-Bot-3.0TABLE OF CONTENTS

* Description
* Requirements
* Installation and setup
* Usage
* TroubleShooting
* FAQ
* Known issues
* Maintainers
* License




DESCRIPTION
-----------

This application is a Discord bot made to relay tweets from specific users and "sample streams"


REQUIREMENTS
.NET Framework 4.5.2
A Twitter Account
A Twitter Application
A Discord Application(BotToken)
Said Discord Bot connected to target Channel



INSTALLATION AND SETUP
------------

* Install the main Executable.
* Make sure you have a Twitter Account, a Twitter Application with Tokens and secrets, and a Discord Token.
* The .zip has a Config.xml file open this in your preferred text editor for ease of use we recommend Notepad++ but any will do
* In the Config.xml you are required to fill out all of the fields with the relevant Tokens.
* If you want automatic login to twitter fill ManualAuth with False if you want to insert it everytime you open the program insert true
* Last but not least you need to add you bot to your channel to do this you will have to insert this link: https://discordapp.com/api/oauth2/authorize?client_id=CLIENTIDHERE&scope=bot&permissions=0
REMEMBER to insert the client ID 



USAGE
-----

There are several commands that this bot supports

!help lists all of the available commands
!Track [User] Streams tweets from a selected user this user can be defined from either their ID or Handle
!Spam initialized a sample stream which will try to send 1% of all public tweets from twitter this is way more than what discord allows so you will be ratelimited for this
!Sample [Amount] initialized a sample stream but stops after a set amount of tweets recieved

BEAR IN MIND THAT ALL OF THESE COMMANDS CANNOT BE STOPPED ONCE THEY HAVE BEEN STARTED ONLY !SAMPLE STOPS BY ITSELF
THE ONLY WAY TO STOP THEM IS TO SHUTDOWN THE PROGRAM


TROUBLESHOOTING
---------------

* Error messages as of this version is not fully implemented so occational crashes might occur and incomprehensible error messages might also occur if this happens,
feel free to forward these messages to the developer details are in the Maintainers section

* Before messaging the Developer please make sure that you have set up your tokens correctly 


FAQ
---

* as of right now there is no frequently asked questions.

* if you have any questions feel free to contact the current maintainer information is in the contact section.



KNOWN ISSUES
------------

* No known issues as of this versions release please forward any issues to the Current maintainer


CONTACT
-------


Current Maintainer(s)

Hans Hedegaard Byager 	Email: hans-byager@live.dk





LICENSE
-------

Copyright 2016 Hans Hedegaard Byager

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.



