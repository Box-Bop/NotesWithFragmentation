﻿using System;
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
    [Activity(Label = "NoteActivity")]
    public class NoteActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                Finish();
            }

            var noteId = Intent.Extras.GetInt("current_note_id", 0);
            var noteFrag = NoteFragment.NewInstance(noteId);
            FragmentManager.BeginTransaction()
                            .Add(Android.Resource.Id.Content, noteFrag)
                            .Commit();
        }
    }
}