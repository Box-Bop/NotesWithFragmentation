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
        public int NoteId => Arguments.GetInt("current_note_id", 0);

        public static NoteFragment NewInstance(int playId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_note_id", playId);
            return new NoteFragment { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }
            var notes = DatabaseService.GetAllNotes();
            var textView = new TextView(Activity);
            var padding = Convert.ToInt32(TypedValue.ApplyDimension(ComplexUnitType.Dip, 4, Activity.Resources.DisplayMetrics));
            textView.SetPadding(padding, padding, padding, padding);
            textView.TextSize = 24;
            textView.Text = notes.ToList()[NoteId].ToString();

            var scroller = new ScrollView(Activity);
            scroller.AddView(textView);

            return scroller;
        }
    }
}