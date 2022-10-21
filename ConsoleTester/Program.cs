using DanceMatClassLibrary;

new DanceMat().ButtonStateChanged += (object? sender, DanceMatEventArgs e) => Console.WriteLine(e);

Console.ReadLine();