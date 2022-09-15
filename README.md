# DosWindowsNet

## What it is?

It is a .NET library for ASCII DOS windows allowing for text-based interfaces. It also comes with an example email eapplication. 

![Alt text](/screenshots/email2.png?raw=true "email.exe can check e-mail from gmail")

Windows are visualized character by character using ASCII symbols. The project works on Windows and Mac/Linux (with [Mono](https://www.mono-project.com)).

## Where it can be used?

  * systems with limited resources such as Raspberry Pi, virtual machines
  * dedicated devices such as cash registers
  * if you need 100% identical UI across different OSes
  * if you need a fast and responsive UI
  * for text based installers
  * it can be used from remote terminals where other UIs are unavailable
  
## Controls
The library provides controls that all inherit from the DosWindow class. So they are also called "windows". The following are provided:
  * standard window
  * textbox window (for input)
  * message window
  * progress bar window 
  * login window
  * list window

There is support for 1 level windows overlapping. So one window over another window will visualise correctly. Closing the top window will restore the integrity of the bottom one. But there are limitations.

## Applications
  * email.exe - an application that allows you to check your email from the Windows or Linux console. If you use Gmail and 2-Step-Verification then you need to use [Application Password](https://support.google.com/accounts/answer/185833?hl=en) instead of your main password.

## Future work:
Add more Windows/Controls
  * checkbox
  
Add more programs:
  * re-write of entire Midnight Commander or PCTools in .NET 
  * rewrite of the Linux's screen command with the split screen functionality
  * chat client for WhatsApp (C# API exists) or IRC
  * console pdf reader that uses .NET to convert the PDF to text
  * weather application with current temperature and forecast
  
  


