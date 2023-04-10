using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace HAMS.Extension.Control.BasicConfiguration.Models
{
    public class SecondaryColorKind
    {
        public string Name { get; set; }
        public Color BackGroundColor { get; set; }

        public Color ForeGroundColor
        {
            get => BackGroundColor.ContrastingForegroundColor();
        }
    }
}
