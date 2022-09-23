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
        public string Header { get; set; } = "";
        public string Keyword { get; set; } = "";
        public string SentenceLeftPart { get; set; } = "";
        public string SentenceRightPart { get; set; } = "";
        public string MissingWord { get; set; } = "";
        public string Space { get; set; } = "";
        public string Transcription { get; set; } = "";
        public string Translation { get; set; } = "";
        public string AudioPath { get; set; } = "";
        private bool availableLessons;
        public ObservableCollection<Lesson> Lessons { get; set; }
        public ObservableCollection<Word> wordsBuffer { get; set; }
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
        private Word selectedWord;
        public Word SelectedWord
        {
            get { return selectedWord; }
            set
            {
                selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
        }
        public ViewModel()
        {
            Lessons = new ObservableCollection<Lesson>();
            wordsBuffer = new ObservableCollection<Word>();
        }
        public void LoadLessonsFromDB(User user)
        {
            ObservableCollection<Word> words = Word.LoadWordsFromDB(user);
            foreach (Word word in words)
            {
                Lessons.Add(word);
                wordsBuffer.Add(word);
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
        public void PrepareWorkplace()
        {
            if (Lessons.Count > 0)
            {
                foreach (Lesson lesson in Lessons)
                {
                    if (lesson.GetType() == typeof(Sentence))
                    {
                        StudySentence(lesson);
                    }
                    else if (lesson.GetType() == typeof(GrammarRule))
                    {
                        StudyGrammar(lesson);
                    }
                    else if (lesson.GetType() == typeof(GrammarTest))
                    {

                    }
                    else
                    {
                        StudyWord(lesson);
                    }
                }
            }
            else
            {
                if (wordsBuffer.Count > 0)
                {
                    foreach (Word word in wordsBuffer)
                    {
                        TestLearnedWords(word);
                    }
                }
                else
                {
                    availableLessons = false;
                }
            }
        }
        public bool CheckLessonsAvailability()
        {
            if (availableLessons)
            {
                return true;
            }
            else return false;
        }
        private void StudyWord(Lesson lesson)
        {
            Word word = (Word)lesson;
            Header = word.Keyword;
            Keyword = word.Keyword;
            SentenceLeftPart = "";
            SentenceRightPart = "";
            MissingWord = "";
            Space = "";
            Transcription = word.Transcription;
            Translation = word.Translation;
            AudioPath = word.AudioPath;
        }
        private void StudySentence(Lesson lesson)
        {
            Sentence sentence = (Sentence)lesson;
            Header = "Заполните пропущенное слово";
            Keyword = "";
            SentenceLeftPart = sentence.SentenceLeftPart;
            SentenceRightPart = sentence.SentenceRightPart;
            MissingWord = sentence.MissingWord;
            while (Space.Length <= MissingWord.Length)
            {
                Space += "_";
            }
            Space = " " + Space + " ";
            Transcription = "";
            Translation = sentence.Translation;
            AudioPath = sentence.AudioPath;
        }
        private void StudyGrammar(Lesson lesson)
        {
            GrammarRule grammarRule = (GrammarRule)lesson;
            Header = "";
            Keyword = "";
            SentenceLeftPart = "";
            SentenceRightPart = "";
            MissingWord = "";
            Space = "";
            Transcription = "";
            Translation = grammarRule.Content;
            AudioPath = grammarRule.AudioPath;
        }
        private void TakeATest(Lesson lesson)
        {
            GrammarTest grammarTest = (GrammarTest)lesson;

        }
        private void TestLearnedWords(Word word)
        {
            Header = "Как это по-английски? Напишите слово вместо звёздочек:";
            Keyword = "";
            SentenceLeftPart = "";
            SentenceRightPart = "";
            MissingWord = "";
            while (Space.Length <= MissingWord.Length)
            {
                Space += "*";
            }
            Space = "\" " + Space + " \"";
            Transcription = "";
            Translation = word.Translation;
            AudioPath = word.AudioPath;
        }
        public bool CheckIfCorrect(string userInput)
        {
            if (Lessons.Count > 0)
            {
                
                if (selectedLesson.GetType() == typeof(Sentence))
                {
                    Sentence testedSentence = (Sentence)selectedLesson;
                    if (userInput == testedSentence.MissingWord)
                    {
                        return true;
                    }
                    else return false;
                    Lessons.Remove(selectedLesson);
                }
                else
                {
                    return true;
                    Lessons.Remove(selectedLesson);
                }
            }
            else
            {
                if (wordsBuffer.Count > 0)
                {
                    if (userInput == selectedWord.Keyword)
                    {
                        return true;
                    }
                    else return false;
                    wordsBuffer.Remove(selectedWord);
                }
                else return true; //What to return here?
            }
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
