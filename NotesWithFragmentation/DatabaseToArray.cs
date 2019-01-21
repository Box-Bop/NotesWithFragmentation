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
        public static string[] NoteTitles = { };
        public static string[] NoteContent = { };

        public static void Task(SQLite.TableQuery<Note> notes) {
            for (int i = 0; i < notes.Count(); i++)
            {
                NoteTitles.Append(notes.ToArray()[i].NoteTitle);
                NoteContent.Append(notes.ToArray()[i].NoteContent);
            }
        }
    }
}