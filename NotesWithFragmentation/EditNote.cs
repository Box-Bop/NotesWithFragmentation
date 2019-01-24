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
    [Activity(Label = "EditNote")]
    class EditNote : AppCompatActivity
    {
        long editingNoteId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.editnote);
            var title = Intent.GetStringExtra("EditTitle");
            var content = Intent.GetStringExtra("EditContent");
            editingNoteId = Intent.GetLongExtra("EditId", 1);
            var titleEditText = FindViewById<EditText>(Resource.Id.textInputEditText1).Text = title;
            var contentEditText = FindViewById<EditText>(Resource.Id.textInputEditText2).Text = content;
            var confirmButton = FindViewById<Button>(Resource.Id.button1);
            var deleteButton = FindViewById<Button>(Resource.Id.button2);
            confirmButton.Click += ConfirmButton_Click;
            deleteButton.Click += DeleteButton_Click;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DatabaseService.DeleteNote(editingNoteId);
            var intent = new Intent(this, typeof(MainActivity))
                .SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var noteTitle = FindViewById<EditText>(Resource.Id.textInputEditText1);
            var noteContent = FindViewById<EditText>(Resource.Id.textInputEditText2);
            DatabaseService.EditNote(noteTitle.Text, noteContent.Text, editingNoteId);
            noteTitle.Text = "";
            noteContent.Text = "";
            var intent = new Intent(this, typeof(MainActivity))
                .SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);
        }
    }
}