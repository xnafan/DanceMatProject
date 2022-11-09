namespace DanceMatClassLibrary;
public enum DanceMatButton
{

    //For easy reference
    //I have added comments with the bits in the 7th and 8th bytes
    //of the 9 bytes that are sent from the dance map in each event

    Start = 8192,      //0 0 1 0 0 0 0 0  0 0 0 0 0 0 0 0
    Select = 4096,     //0 0 0 1 0 0 0 0  0 0 0 0 0 0 0 0
    Circle = 2048,      //0 0 0 0 1 0 0 0  0 0 0 0 0 0 0 0
    Cross = 1024,      //0 0 0 0 0 1 0 0  0 0 0 0 0 0 0 0
    Square = 512,      //0 0 0 0 0 0 1 0  0 0 0 0 0 0 0 0
    Triangle = 256,    //0 0 0 0 0 0 0 1  0 0 0 0 0 0 0 0    
    Right = 128,       //0 0 0 0 0 0 0 0  1 0 0 0 0 0 0 0
    Up = 64,            //0 0 0 0 0 0 0 0  0 1 0 0 0 0 0 0
    Down = 32,         //0 0 0 0 0 0 0 0  0 0 1 0 0 0 0 0
    Left = 16,         //0 0 0 0 0 0 0 0  0 0 0 1 0 0 0 0
};