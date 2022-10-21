using DanceMatClassLibrary;

namespace DanceMatVisualizerWindow
{
    public partial class MainForm : Form
    {
        private DanceMat _danceMat = new DanceMat();
        public MainForm()
        {
            InitializeComponent();
            _danceMat.ButtonStateChanged += _danceMat_ButtonStateChanged;
            tmrRepaint.Tick += TmrRepaint_Tick;
            tmrRepaint.Enabled = true;
        }

        private void TmrRepaint_Tick(object? sender, EventArgs e)
        {
            RepaintPictureBox();
        }

        private void _danceMat_ButtonStateChanged(object? sender, DanceMatEventArgs e)
        {
            picBoxDanceMat.ButtonPressed[e.Button] = e.Action == DanceMat.DanceMatButtonAction.Pressed;
        }

        private void RepaintPictureBox()
        {
            
            if (InvokeRequired)
            {
                Invoke(new Action(() => RepaintPictureBox()));
            }
            else
            {
                
                picBoxDanceMat.Refresh();
            }
        }
    }
}