using DanceMatClassLibrary;
DanceMat mat = new DanceMat();
mat.ButtonStateChanged += (object? sender, DanceMatEventArgs e) => Console.WriteLine(e);

Console.ReadLine();
