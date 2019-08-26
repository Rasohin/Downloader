using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace MusicAndBooksDownloader.Droid
{
    [Activity(Label = "Music&Books", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        public override void OnBackPressed() { }

        async void SimulateStartup()
        {
            await Task.Delay(1000); // Simulate a startup work
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}