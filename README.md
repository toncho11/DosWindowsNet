# DosWindowsNet
ASCII DOS windows for Microsoft .Net/C#. Windows are visualized character by character using ASCII symbols. The project works on Windows and Mac/Linux (with Mono).

Library usage:

  * systems with limited resources such as Raspberry Pi, virtual machines
  * to create installers before the entire OS is deployed (Linux installer)
  * you want 100% identical UI across different OS
  * you get the most responsive UI and you prefer keyboard input
  * reduce hackers's attack surface by completely eliminating the graphical subsystem

Programs:
  * provided: email.exe - allows you to check your gmail from the Windows or Linux console
  * future: console pdf reader that uses .NET to convert the PDF to text
  * future: chat client for What's up (C# API exists)
  * future: re-write of entire Midnight Commander in .NET

The library provides controls that all inherit from the DosWindow class. So they are also called "windows". The following are provided:
  * standard window
  * textbox window (for input)
  * message window
  * progress bar window 
  * login window
  * list window (in progress)
  
Things to add:
  * more Windows/Controls
  * implement Windows overlay (several windows on top of each other), currently there is a simplified implementation


