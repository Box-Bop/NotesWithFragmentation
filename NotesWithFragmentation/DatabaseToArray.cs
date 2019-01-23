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
        public static List<long> NoteIds = new List<long>();

        public static void Update() {
            NoteTitles.Clear();
            NoteContent.Clear();
            NoteIds.Clear();
            var notes = DatabaseService.GetAllNotes();
            var array = notes.ToArray();
            for (int i = 0; i < notes.Count(); i++)
            {
                NoteTitles.Add(array[i].NoteTitle);
                NoteContent.Add(array[i].NoteContent);
                NoteIds.Add(array[i].Id);
            }
        }
    }
}