# DanceMatProject
C# library for communicating with a USB dance mat using .NET

## Short description
This project includes 
 * C# class library for communicating with a USB dance mat
 * Console tester project, to illustrate how simple it is to communicate with a USB dance mat (a oneliner)
 * Windows Forms dance mat visualizer project, which shows which buttons on the mat are currently activated
 * Small maze game, which allows the player to move around a randomly generated maze top down
 * An actual dance game, with steps incoming from the bottom (Dance Dance Revolution - style, but much simpler)  

<img width="961" alt="image" src="https://user-images.githubusercontent.com/3811290/197618681-19c6fa2b-7a19-4202-b896-31174d6406e0.png">

# USB dance mat driver (DanceMatClassLibrary)
This class library uses the HIDDevice class from Joel Coenraadts' project: https://github.com/jcoenraadts/hid-sharp to communicate with the USB dance mat.
The class library has a class DanceMat:  
<img width="587" alt="image" src="https://user-images.githubusercontent.com/3811290/197620088-4eb9c20b-6afb-4644-b80b-ad1b1cddc85f.png">

The DanceMat class has an event ButtonStateChanged, which a piece of client software can subscribe to in order to be notified when a change in the dance mat state occurs.
The event sends a DanceMatEventArgs with the following info:
* Button (Start, Select, Circle, Cross, Square, Triangle, Right, Up, Down, Left)
* Action (Unchanged, Pressed, Released)

Using this code it should be possible to create games which work with a USB dance mat.
Note that different brands of dance mats will identify themselves using other Vendor IDs (VID) and Product IDs (PID) in their USB communication, and this library may not work with them, or it may be enough to change the VID/PID in the DanceMat code, to make it work. Let me know if yours doesn't work, and we can see if we can fix it 😊

The dance mat brand the code is written for:  
![image](https://user-images.githubusercontent.com/3811290/197620803-e65e9f2a-f897-4e13-83de-5b96c53ef216.png)

# Using the DanceMat class
It's as simple as this, to use the DanceMat class, if you have the right brand of dance mat:

  using DanceMatClassLibrary;

  //hooks an anonymous eventhandler (which writes the action that occurred)
  //  up to the ButtonStateChanged event
  new DanceMat().ButtonStateChanged += (object? sender, DanceMatEventArgs e) => Console.WriteLine(e);

  //Waits for an ENTER press
  Console.ReadLine();
