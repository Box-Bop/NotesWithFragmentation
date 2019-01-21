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

        public TitlesFragment()
        {
            // Being explicit about the requirement for a default constructor.
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var notes = DatabaseService.GetAllNotes();
            string[] titlesArray = new string[notes.Count()];
            for (int i = 0; i < notes.Count(); i++)
            {
                titlesArray.Append(notes.ToArray()[i].NoteTitle);
            }
            ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, titlesArray);

            if (savedInstanceState != null)
            {
                selectedNoteId = savedInstanceState.GetInt("current_note_id", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_note_id", selectedNoteId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowNote(position);
        }

        void ShowNote(int noteId)
        {
            var intent = new Intent(Activity, typeof(NoteActivity));
            intent.PutExtra("current_note_id", noteId);
            StartActivity(intent);
        }
    }
}