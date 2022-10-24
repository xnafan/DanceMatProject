using DanceMatClassLibrary;
using System.Collections.Generic;
using System;

namespace DanceMatMazeGame.components
{
    public struct DanceMove : IEquatable<DanceMove>
    {

        #region Properties
        public bool Up { get; set; } = false;
        public bool Down { get; set; } = false;
        public bool Left { get; set; } = false;
        public bool Right { get; set; } = false;
        #endregion

        #region Constructor

        public DanceMove(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        #endregion

        #region Methods
        public bool TestForDanceMatStateMatch(DanceMatState danceMatState)
        {
            return danceMatState.Up == Up &&
                danceMatState.Down == Down &&
                danceMatState.Left == Left &&
                danceMatState.Right == Right;
        }

        public List<bool> GetAllButtons() => new List<bool>() { Left, Down, Up, Right };

        public bool Equals(DanceMove other)
        {
            return Up == other.Up && Down == other.Down && Left == other.Left && Right == other.Right;
        }
        public bool NothingPressed { get { return !(Up || Down || Right || Left); } } 
        #endregion
    }
}