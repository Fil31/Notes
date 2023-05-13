using Notes;
using System;
using Xamarin.Forms;

namespace Notes
{
    public partial class AddEditNotePage : ContentPage
    {
        private readonly Note _note;

        public AddEditNotePage(Note note = null)
        {
            InitializeComponent();

            _note = note;

            if (_note != null)
            {
                TitleEntry.Text = _note.Title;
                ContentEditor.Text = _note.Content;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_note == null)
            {
                // Add a new note
                Note newNote = new Note
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = TitleEntry.Text,
                    Content = ContentEditor.Text
                };

                MessagingCenter.Send(this, "AddNote", newNote);
            }
            else
            {
                // Update the existing note
                _note.Title = TitleEntry.Text;
                _note.Content = ContentEditor.Text;

                MessagingCenter.Send(this, "UpdateNote", _note);
            }

            await Navigation.PopAsync();
        }
    }
}
