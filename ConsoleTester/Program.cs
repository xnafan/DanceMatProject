using DanceMatClassLibrary;

//hooks an anonymous eventhandler (which writes the action that occurred)
//  up to the ButtonStateChanged event
new DanceMat().ButtonStateChanged += (object? sender, DanceMatEventArgs e) => Console.WriteLine(e);

//Waits for an ENTER press
Console.ReadLine();