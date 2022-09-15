# DosWindowsNet
ASCII DOS windows for Microsoft .Net/C#. 

![Alt text](/screenshots/email2.png?raw=true "email.exe can check e-mail from gmail")

Windows are visualized character by character using ASCII symbols. The project works on Windows and Mac/Linux (with Mono).

Library usage:

  * systems with limited resources such as Raspberry Pi, virtual machines
  * to create installers before the entire OS is deployed (Linux installer)
  * you want 100% identical UI across different OS
  * you get the most responsive UI and you prefer keyboard input
  * reduce hackers's attack surface by completely eliminating the graphical subsystem
  
The library provides controls that all inherit from the DosWindow class. So they are also called "windows". The following are provided:
  * standard window
  * textbox window (for input)
  * message window
  * progress bar window 
  * login window
  * list window (in progress)

There is a support for 1 level windows overlapping. So one window over another window will visualise correctly. Closing the second window will restore the integrity of the first one. But there is a limitation on how many windows can sit on top of each other.

Programs:
  * Email.exe - an application that allows you to check your email from the Windows or Linux console. If you use Gmail and 2-Step-Verification then you need to use [Application Password](https://support.google.com/accounts/answer/185833?hl=en).

Future work:
  * more Windows/Controls (checkbox for example)
  
Add more programs:
  * rewrite of the Linux's screen command with the split screen functionality
  * chat client for WhatsApp (C# API exists)
  * weather application with current temperature and forecast
  * console pdf reader that uses .NET to convert the PDF to text
  * re-write of entire Midnight Commander or PCTools in .NET 
  


