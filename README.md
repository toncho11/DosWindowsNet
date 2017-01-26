# DosWindowsNet
ASCII DOS windows for Microsoft .Net/C#. Windows are visualized character by character using ASCII symbols. The project works on Windows and Mac/Linux (with Mono).

An example app email.exe is provided. It allows you to check your gmail from the Windows or Linux console.

Library usage:

  * systems with limited resources such as Raspberry Pi, virtual machines
  * to create installers before the entire OS is deployed (Linux installer)
  * you want 100% identical UI across different OS
  * you get the most responsive UI and you prefer keyboard input
  * reduce hackers's attack surface by completely eliminating the graphical subsystem
  
The library provides controls that all inherit from the DosWindow class. So they are also called "windows". The following are provided:
  * standard window
  * textbox window
  * message window
  * progress bar window 
  * login window
  * list window (in progress)
  
