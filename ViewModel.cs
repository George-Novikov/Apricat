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
        public void PrepareWorksplace(ObservableCollection<Lesson> lessons)
        {
            foreach (Lesson lesson in lessons)
            {
                if (lesson.GetType() == typeof(Sentence))
                {

                } else if (lesson.GetType() == typeof(GrammarRule))
                {

                } else if (lesson.GetType() == typeof(GrammarTest))
                {

                }
                else
                {

                }
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
