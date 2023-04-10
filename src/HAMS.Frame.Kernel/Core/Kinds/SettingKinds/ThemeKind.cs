using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace HAMS.Frame.Kernel.Core
{
    public class ThemeKind : SettingKind
    {
        public BaseTheme BaseTheme { get; set; }
        public PrimaryColor PrimaryColor { get; set; }
        public SecondaryColor SecondaryColor { get; set; }
    }
}
