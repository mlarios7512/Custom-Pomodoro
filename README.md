# Custom Pomodoro

A MAUI Blazor hybrid app with a customizable pomodoro timer. Developed with Windows & *Android devices in mind. WebView is required, as all pages currently make use of it.

*Disclaimer: A stable Android release is currently unavailable. A glitch currently causes the "release" version to break on Android devices. (This is a bug on the framework's behalf. Also potentially responsible for crashing Android emulators on "release" mode.) Development will be kept with Android in mind should the issue be resolved.

**For cloning the project:** A `Properties` directory containing `launchSettings.json` is missing from the online repo (but essential for functionality). After cloning, create a`Properties` directory within the project. From a template MAUI-Blazor hybrid project, any default `launchSettings.json` can be copied into the recently created `Properties` directory to make this project functional.


<br>

![Timer page](app_demo_imgs/Timer.PNG)
![Timer Settings page](app_demo_imgs/Change_Timer.PNG)
![Color Settings page](app_demo_imgs/Color_Settings.PNG)


## Features
* Timer display 
    * Work session count & text displaying the current session type.
    * "Skip", "reset", & "next" buttons are available to move back & forth between session types. 
    * Simple timer expiration sound.
* Color options
    * Customizable background colors for "not started", "running", & "paused" timer states.
    * Customizable (optional) colors for "work", "short break", & "long break" timers.

<br>


## Future features
* Likely
    * Choice of black, gray, or white text for the timer page.
    * Quick selection of previously used timers (for each type of timer).
* Considered
    * Selection of local audio file (for use as sound on timer expiration).
    * Auto-start timer sessions (after pressing "start" button once).
    * Color options for navigation bar.

<br>

## Contributions
Public contributions are not currently accepted (as I've yet to come up with a clear workflow that would allow for that). This may change in the future.