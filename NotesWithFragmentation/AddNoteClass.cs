using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace NotesWithFragmentation
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    class AddNoteClass : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addnewnote);
            var addButton = FindViewById<Button>(Resource.Id.button1);
            addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var noteTitle = FindViewById<EditText>(Resource.Id.textInputEditText1);
            var noteContent = FindViewById<EditText>(Resource.Id.textInputEditText2);
            DatabaseService.AddNote(noteTitle.Text, noteContent.Text);
            noteTitle.Text = "";
            noteContent.Text = "";
            MainActivity.forRefresh.Recreate();
            Finish();
            //var intent = new Intent(this, typeof(MainActivity))
            //    .SetFlags(ActivityFlags.ReorderToFront);
            //StartActivity(intent);
        }
    }
}