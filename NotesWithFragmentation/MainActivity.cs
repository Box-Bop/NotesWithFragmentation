using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Linq;

//For the app center
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace NotesWithFragmentation
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        public static MainActivity forRefresh { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("78100d71-17e2-4ecc-b694-9e9630039eac", typeof(Analytics), typeof(Crashes));
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            DatabaseService.CreateDatabase();
            DatabaseService.CreateTableWithData();
            //var notes = DatabaseService.GetAllNotes();
            DatabaseToArray.Update();
            forRefresh = this;
            SetContentView(Resource.Layout.activity_main);
            var floatingButton = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab);
            floatingButton.Click += FloatingButton_Click;
        }

        private void FloatingButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AddNoteClass));
            StartActivity(intent);
        }
    }
}