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
using System.Media;
using System.Windows;

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
        public ObservableCollection<Lesson> Lessons { get; set; }
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
        public ViewModel()
        {
            Lessons = new ObservableCollection<Lesson>();
        }
        public void LoadLessonsFromDB(User user)
        {
            ObservableCollection<Word> words = Word.LoadWordsFromDB(user);
            foreach (Word word in words)
            {
                this.Lessons.Add(word);
                
            }
            ObservableCollection<Sentence> sentences = Sentence.LoadSentencesFromDB(user);
            foreach (Sentence sentence in sentences)
            {
                Lessons.Add(sentence);
            }
            GrammarRule grammarRule = GrammarRule.LoadRuleFromDB(user);
            Lessons.Add(grammarRule);
            GrammarTest grammarTest = GrammarTest.LoadTestFromDB(grammarRule);
            Lessons.Add(grammarTest);
        }
        public void PrepareWorkplace(ObservableCollection<Lesson> lessons)
        {
            int currentSession = lessons.Count;
            if (currentSession > 0)
            {
                foreach (Lesson lesson in lessons)
                {
                    if (lesson.GetType() == typeof(Sentence))
                    {


                    }
                    else if (lesson.GetType() == typeof(GrammarRule))
                    {

                    }
                    else if (lesson.GetType() == typeof(GrammarTest))
                    {

                    }
                    else
                    {
                        StudyWord(lesson);
                        --currentSession;
                    }
                }
            }
            else
            {
                //nextLessonButton.IsActive = False;
            }
        }
        
        public void StudyWord(Lesson lesson)
        {
            Word word = (Word)lesson;
        }
        public void StudySentence(Lesson lesson)
        {
            Sentence sentence = (Sentence)lesson;
        }
        public void StudyGrammar(Lesson lesson)
        {
            GrammarRule grammarRule = (GrammarRule)lesson;
        }
        public void TakeATest(Lesson lesson)
        {
            GrammarTest grammarTest = (GrammarTest)lesson;
        }
        public void UpdateUserLevel()
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
