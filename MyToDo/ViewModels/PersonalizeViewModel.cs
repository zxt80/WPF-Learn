using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyToDo.ViewModels
{
    public class PersonalizeViewModel:BindableBase
    {
		private bool isDarkTheme=true;
        private readonly PaletteHelper paletteHelper = new();
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                if (SetProperty(ref isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        public DelegateCommand<object> ChangeHueCommand { get; }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public PersonalizeViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
        }



        private void ChangeHue(object obj)
        {
            System.Windows.Media.Color color = (System.Windows.Media.Color)obj!;
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}
