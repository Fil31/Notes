using Notes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

public class NoteViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand AddNoteCommand { get; set; }
    public ICommand EditNoteCommand { get; set; }
    public ICommand DeleteNoteCommand { get; set; }
    public INavigation Navigation { get; set; }

    public NoteViewModel()
    {
        Notes = new ObservableCollection<Note>();

        AddNoteCommand = new Command(async () => await AddNote());
        EditNoteCommand = new Command<Note>(async (note) => await EditNote(note));
        DeleteNoteCommand = new Command<Note>(DeleteNote);

        MessagingCenter.Subscribe<AddEditNotePage, Note>(this, "AddNote", (sender, note) =>
        {
            Notes.Add(note);
        });

        MessagingCenter.Subscribe<AddEditNotePage, Note>(this, "UpdateNote", (sender, note) =>
        {
            Note existingNote = Notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                existingNote.Title = note.Title;
                existingNote.Content = note.Content;
            }
        });
    }

    private async Task AddNote()
    {
        await Navigation.PushAsync(new AddEditNotePage());
    }

    private async Task EditNote(Note note)
    {
        await Navigation.PushAsync(new AddEditNotePage(note));
    }

    private void DeleteNote(Note note)
    {
        Notes.Remove(note);
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
