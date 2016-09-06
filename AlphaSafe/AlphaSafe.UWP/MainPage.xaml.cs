
namespace AlphaSafe.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            LoadApplication(new AlphaSafe.UI.App());
        }
    }
}
