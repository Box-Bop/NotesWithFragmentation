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
    public class NoteFragment : Fragment
    {
        public int NoteId
        {
            get => Arguments.GetInt("current_note_id", 0);
            set { }
        }
        //private string NewNoteTitle;
        //private string NewNoteContent;

        public static NoteFragment NewInstance(int noteId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_note_id", noteId);
            return new NoteFragment { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }
            if (NoteId > DatabaseToArray.NoteIds.Count() - 1)
            {
                NoteId -= 1;
            }
            var noteTitle = Activity.FindViewById<EditText>(Resource.Id.textInputEditText1);
            var noteContent = Activity.FindViewById<EditText>(Resource.Id.textInputEditText2);
            try
            {
                noteTitle.Text = DatabaseToArray.NoteTitles[NoteId];
                noteContent.Text = DatabaseToArray.NoteContent[NoteId];
            }
            catch (Exception)
            {
                noteTitle.Text = "";
                noteContent.Text = "";
            }

            return View;
        }
    }
}