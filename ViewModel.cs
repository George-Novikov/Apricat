using Apricat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.CodeDom;

namespace Apricat
{
    public class ViewModel : INotifyPropertyChanged
    {
        public string Header { get; set; } = "Заполните недостающее слово";
        public string Keyword { get; set; } = "";
        public string SentenceLeftPart { get; set; } = "This is a";
        public string SentenceRightPart { get; set; } = "evening";
        public string Space { get; set; } = " ____ ";
        public string MissingWord { get; set; } = "good";
        public string Transcription { get; set; } = "";
        public string Translation { get; set; } = "Это хороший вечер";
        public string AudioPath { get; set; } = "sentence.wav";
        public bool AvailableLessons { get; set; } = true;
        public ObservableCollection<Lesson>? Lessons { get; set; }
        private Lesson selectedLesson;
        public Lesson SelectedLesson
        {
            get { return selectedLesson; }
            set
            {
                selectedLesson = value;
                OnPropertyChanged("SelectedLesson");
            }
        }
        public void CountCurrentlyLearned()
        {

        }
        public void GetUserLevel()
        {

        }
        public void UpdateUserLevel()
        {

        }
        public void GetUserDailyRate()
        {

        }
        public void UpdateUserDailyRate()
        {

        }
        public void ShowUserLearnedWords()
        {

        }
        public void ShowUserLearnedSentences()
        {

        }
        public void ShowUserLearnedGrammarRules()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
