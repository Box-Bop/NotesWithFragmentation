using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace NotesWithFragmentation
{
    public class TitlesFragment : ListFragment
    {
        int selectedNoteId;
        bool showingTwoFragments;
        private long NoteCreationId;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemActivated1, DatabaseToArray.NoteTitles);

            if (savedInstanceState != null)
            {
                selectedNoteId = savedInstanceState.GetInt("current_note_id", 0);
            }


            var quoteContainer = Activity.FindViewById(Resource.Id.playquote_container);
            showingTwoFragments = quoteContainer != null &&
                                    quoteContainer.Visibility == ViewStates.Visible;
            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowNoteContent(selectedNoteId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_note_id", selectedNoteId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                ShowNoteContent(position);
            }
            else
            {
                var intent = new Intent(Activity, typeof(EditNote));
                intent.PutExtra("EditTitle", DatabaseToArray.NoteTitles.ToList()[position]);
                intent.PutExtra("EditContent", DatabaseToArray.NoteContent.ToList()[position]);
                intent.PutExtra("EditId", DatabaseToArray.NoteIds.ToList()[position]);
                StartActivity(intent);
            }
        }

        void ShowNoteContent(int noteId)
        {
            selectedNoteId = noteId;
            if (showingTwoFragments)
            {
                ListView.SetItemChecked(selectedNoteId, true);

                var NoteFragment = FragmentManager.FindFragmentById(Resource.Id.playquote_container) as NoteFragment;

                if (NoteFragment == null || NoteFragment.NoteId != noteId)
                {
                    var container = Activity.FindViewById(Resource.Id.playquote_container);
                    var quoteFrag = NoteFragment.NewInstance(selectedNoteId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.playquote_container, quoteFrag);
                    ft.AddToBackStack(null);
                    ft.SetTransition(FragmentTransit.FragmentFade);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(NoteActivity));
                intent.PutExtra("current_note_id", noteId);
                StartActivity(intent);
            }
            var deleteButton = Activity.FindViewById<Button>(Resource.Id.button2);
            var updateButton = Activity.FindViewById<Button>(Resource.Id.button1);
            
            if (noteId > DatabaseToArray.NoteIds.Count() - 1)
            {
                noteId -= 1;
            }
            NoteCreationId = DatabaseToArray.NoteIds[noteId];
            deleteButton.Click += DeleteButton_Click;
            updateButton.Click += UpdateButton_Click;
            
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var noteTitle = Activity.FindViewById<EditText>(Resource.Id.textInputEditText1);
            var noteContent = Activity.FindViewById<EditText>(Resource.Id.textInputEditText2);
            DatabaseService.EditNote(noteTitle.Text, noteContent.Text, NoteCreationId);
            DatabaseToArray.Update();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DatabaseService.DeleteNote(NoteCreationId);
            DatabaseToArray.Update();
        }
    }
}