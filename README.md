# Custom Pomodoro

MAUI Blazor hybrid app with a customizable pomodoro timer. Developed with Windows & *Android devices in mind. WebView is required, as all pages currently make use of it.

*Disclaimer: An Android release is currently unavailable. A framework bug currently causes the "release" version (within Visual Studio) to break on Android emulators. (Untested on physical Android device). 
The same bug may also cause single device instances on Android emulators to become stuck in a crashing loop (after a couple of tests). (No framework issues are noticeable when running on an Android emuatlor using Visual Studio "debug" mode.)

Development will be kept with Android in mind should the issue be resolved. 


<br>

![Timer page](app_demo_imgs/Timer.PNG)
![Timer Settings page](app_demo_imgs/Change_Timer.PNG)
![Color Settings page](app_demo_imgs/Color_Settings.PNG)


### Features
* Timer 
    * Work session count & text displaying the current session type.
    * "Skip", "reset", & "next" buttons are available to move back & forth between session types. 
    * Option to change duration of each timer sessions & reps until long break.
    * Timer expiration sound.
* Color options
    * Customizable background & text colors for "not started", "running", & "paused" timer states.
    * Customizable (optional) colors for "work", "short break", & "long break" timers.

<br>


### Future features
* Likely
    * Quick selection of previously used times (for each type of timer).
* Considered
    * Option to select local audio file (for use as sound on timer expiration).
    * Option to auto-start timer subsequent sessions.
    * Color options for navigation bar.