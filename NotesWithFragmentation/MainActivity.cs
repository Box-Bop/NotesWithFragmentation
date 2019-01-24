using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Linq;

namespace NotesWithFragmentation
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            DatabaseService.CreateDatabase();
            DatabaseService.CreateTableWithData();
            //var notes = DatabaseService.GetAllNotes();
            DatabaseToArray.Update();
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