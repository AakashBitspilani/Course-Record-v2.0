﻿using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    [DebuggerDisplay("{Name} - by \"{Author}\"")]
    public class EBookItem : EElementItemBase, ISerializable
    {
        #region Properties

        public TextBookType BookType { get; private set; }
        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Edition { get; private set; }
        public string Press { get; private set; }
        public bool IsBest { get; private set; }

        #endregion

        #region DisplayItems

        internal TextBlock NameViewBlock { get; private set; }
        internal TextBlock AuthorViewBlock { get; private set; }
        internal TextBlock BookTypeViewBlock { get; private set; }
        internal CheckBox IsBestViewBox { get; private set; }

        #endregion

        #region Serialization

        protected EBookItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            BookType = (TextBookType)info.GetValue(nameof(BookType), typeof(TextBookType));
            Author = (string)info.GetValue(nameof(Author), typeof(string));
            Name = (string)info.GetValue(nameof(Name), typeof(string));
            Edition = (int)info.GetValue(nameof(Edition), typeof(int));
            Press = (string)info.GetValue(nameof(Press), typeof(string));
            IsBest = (bool)info.GetValue(nameof(IsBest), typeof(bool));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(BookType), BookType, typeof(TextBookType));
            info.AddValue(nameof(Author), Author, typeof(string));
            info.AddValue(nameof(Name), Name, typeof(string));
            info.AddValue(nameof(Edition), Edition, typeof(int));
            info.AddValue(nameof(Press), Press, typeof(string));
            info.AddValue(nameof(IsBest), IsBest, typeof(bool));
        }

        #endregion

        public EBookItem(TextBookType bookType, string author, string name, int edition, string press, bool isBest)
        {
            UpdateData(bookType, author, name, edition, press, isBest);
        }

        internal void UpdateData(TextBookType bookType, string author, string name, int edition, string press, bool isBest)
        {
            BookType = bookType;
            Author = author;
            Name = name;
            Edition = edition;
            Press = press;
            IsBest = isBest;
        }

        internal void UpdateDataWithViews(TextBookType bookType, string author, string name, int edition, string press, bool isBest)
        {
            UpdateData(bookType, author, name, edition, press, isBest);
            UpdateViews();
        }

        internal override void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 2), (typeof(string), 2), (typeof(string), 1), (typeof(bool), 1));

            NameViewBlock = controls[0] as TextBlock;
            AuthorViewBlock = controls[1] as TextBlock;
            BookTypeViewBlock = controls[2] as TextBlock;
            IsBestViewBox = controls[3] as CheckBox;

            IsBestViewBox.Click += (object sender, RoutedEventArgs e) => IsBest = IsBestViewBox.IsChecked == true ? true : false;

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            NameViewBlock.Text = Name;
            AuthorViewBlock.Text = Author;
            BookTypeViewBlock.Text = BookType.ToString();
            IsBestViewBox.IsChecked = IsBest;
        }

        internal override void DestructViews()
        {
            NameViewBlock = null;
            AuthorViewBlock = null;
            BookTypeViewBlock = null;
            IsBestViewBox = null;

            base.DestructViews();
        }
    }
}