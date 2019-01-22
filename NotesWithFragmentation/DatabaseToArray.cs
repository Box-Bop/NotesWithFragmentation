using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NotesWithFragmentation
{
    class DatabaseToArray
    {
        public static List<string> NoteTitles = new List<string>();
        public static List<string> NoteContent = new List<string>();
        public static bool Once = false;

        public static void Task(SQLite.TableQuery<Note> notes) {
            if (Once == false)
            {
                var test = notes.ToArray();
                for (int i = 0; i < notes.Count(); i++)
                {
                    NoteTitles.Add(test[i].NoteTitle.ToString());
                    NoteContent.Add(test[i].NoteContent);
                }
                Once = true;
            }
        }
    }
}