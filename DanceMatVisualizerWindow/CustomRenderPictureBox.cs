using DanceMatClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceMatVisualizerWindow
{
    internal class CustomRenderPictureBox : PictureBox
    {
        private static Random _random = new Random();
        public Dictionary<DanceMat.DanceMatButton, bool> ButtonPressed { get; set; } = new Dictionary<DanceMat.DanceMatButton, bool>();
        private static readonly Pen _indicatorPenYellow = new Pen(Brushes.Yellow, 11);
        //private static readonly Pen _indicatorPenBlack = new Pen(Brushes.Black, 11);
        private static readonly Pen _indicatorPenRed = new Pen(Brushes.Red, 13);
        private static readonly Pen[] _allIndicatorPens = new Pen[]{_indicatorPenYellow,  _indicatorPenRed};
        private static readonly Dictionary<DanceMat.DanceMatButton, Rectangle> _focusRectangles = new()
        {
            { DanceMat.DanceMatButton.Select, new Rectangle(5,5,250,100) },
            { DanceMat.DanceMatButton.Start, new Rectangle(540,5,230,100) },
            { DanceMat.DanceMatButton.Cross, new Rectangle(5,135,230,230) },
            { DanceMat.DanceMatButton.Up, new Rectangle(280,135,220,185) },
            { DanceMat.DanceMatButton.Circle, new Rectangle(570,135,200,220) },
            { DanceMat.DanceMatButton.Left, new Rectangle(5,410,200,220)},
            { DanceMat.DanceMatButton.Right, new Rectangle(585,410,190,220) },
            { DanceMat.DanceMatButton.Triangle, new Rectangle(25,710,180,180) },
            { DanceMat.DanceMatButton.Down, new Rectangle(280,740,220,165) },
            { DanceMat.DanceMatButton.Square, new Rectangle(570,710,190,190) },
        };
        public CustomRenderPictureBox()
        {
            CheckForIllegalCrossThreadCalls = true;
            foreach (var buttonType in Enum.GetValues<DanceMat.DanceMatButton>())
            {
                ButtonPressed.Add(buttonType, false);
            }

        } 
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            foreach (var keyValue in ButtonPressed)
            {
                if(keyValue.Value)
                {
                    pe.Graphics.DrawRectangle(_allIndicatorPens[(DateTime.Now.Millisecond/100)%2], _focusRectangles[keyValue.Key]);
                }
            }

                
        }
    }
}