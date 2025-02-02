using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public class CheckerPictureBox : PictureBox
    {
        private readonly Point r_CheckerPoint;
        private bool m_IsCheckerPointAvailable;

        public CheckerPictureBox(int i_CheckerLocationX, int i_CheckerLocationY)
        {
            r_CheckerPoint = new Point(i_CheckerLocationX, i_CheckerLocationY);
        }

        public Point CheckerPoint
        {
            get
            {
                return r_CheckerPoint;
            }
        }

        public bool IsCheckerPointAvailable
        {
            get
            {
                return m_IsCheckerPointAvailable;
            }

            set
            {
                m_IsCheckerPointAvailable = value;
            }
        }
    }
}