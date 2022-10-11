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
        private string header;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }
        private string keyword;
        public string Keyword
        {
            get { return keyword; }
            set
            {
                keyword = value;
                OnPropertyChanged("Keyword");
            }
        }
        private string sentenceLeftPart;
        public string SentenceLeftPart
        {
            get { return sentenceLeftPart; }
            set
            {
                sentenceLeftPart = value;
                OnPropertyChanged("SentenceLeftPart");
            }
        }
        private string sentenceRightPart;
        public string SentenceRightPart
        {
            get { return sentenceRightPart; }
            set
            {
                sentenceRightPart = value;
                OnPropertyChanged("SentenceRightPart");
            }
        }
        private string missingWord;
        public string MissingWord
        {
            get { return missingWord; }
            set
            {
                missingWord = value;
                OnPropertyChanged("MissingWord");
            }
        }
        private string space;
        public string Space
        {
            get { return space; }
            set
            {
                space = value;
                OnPropertyChanged("Space");
            }
        }
        private string transcription;
        public string Transcription
        {
            get { return transcription; }
            set
            {
                transcription = value;
                OnPropertyChanged("Transcription");
            }
        }
        private string translation;
        public string Translation
        {
            get { return translation; }
            set
            {
                translation = value;
                OnPropertyChanged("Translation");
            }
        }
        private string answer1;
        public string Answer1
        {
            get { return answer1; }
            set
            {
                answer1 = value;
                OnPropertyChanged("Answer1");
            }
        }
        private string answer2;
        public string Answer2
        {
            get { return answer2; }
            set
            {
                answer2 = value;
                OnPropertyChanged("Answer2");
            }
        }
        private string answer3;
        public string Answer3
        {
            get { return answer3; }
            set
            {
                answer3 = value;
                OnPropertyChanged("Answer3");
            }
        }
        private string audioPath;
        public string AudioPath
        {
            get { return audioPath; }
            set
            {
                audioPath = value;
                OnPropertyChanged("AudioPath");
            }
        }

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
        private Sentence selectedSentence;
        public Sentence SelectedSentence
        {
            get { return selectedSentence; }
            set
            {
                selectedSentence = value;
                OnPropertyChanged("SelectedSentence");
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
            if (grammarRule.Title != null)
            {
                Lessons.Add(grammarRule);
            }
            GrammarTest grammarTest = GrammarTest.LoadTestFromDB(grammarRule);
            if (grammarTest.Title != null)
            {
                Lessons.Add(grammarTest);
            }
        }
        public void LoadRepetition(User user)
        {
            ObservableCollection<Word> words = Word.LoadLearnedWords(user);
            foreach (Word word in words)
            {
                Lessons.Add(word);
                wordsBuffer.Add(word);
            }
            ObservableCollection<Sentence> sentences = Sentence.LoadLearnedSentences(user);
            foreach (Sentence sentence in sentences)
            {
                Lessons.Add(sentence);
            }
            GrammarRule grammarRule = GrammarRule.LoadLearnedRule(user);
            if (grammarRule.Title != null)
            {
                Lessons.Add(grammarRule);
            }
            GrammarTest grammarTest = GrammarTest.LoadTestFromDB(grammarRule);
            if (grammarTest.Title != null)
            {
                Lessons.Add(grammarTest);
            }
        }
        public void PrepareWorkplace()
        {
            if (Lessons.Count > 0)
            {
                selectedLesson = Lessons[0];

                if (selectedLesson.GetType() == typeof(Sentence))
                {
                    if (selectedSentence == null)
                    {
                        StudySentence(selectedLesson);
                        selectedSentence = (Sentence)selectedLesson;
                    }
                    else
                    {
                        TestLearnedSentence(selectedSentence);
                        selectedSentence = null;
                    }                    
                }
                else if (selectedLesson.GetType() == typeof(GrammarRule))
                {
                    StudyGrammar(selectedLesson);
                }
                else if (selectedLesson.GetType() == typeof(GrammarTest))
                {
                    TakeATest(selectedLesson);
                }
                else
                {
                    StudyWord(selectedLesson);
                }
            }
            else
            {
                if (wordsBuffer.Count > 0)
                {
                    selectedWord = wordsBuffer[0];
                    TestLearnedWords(selectedWord);
                }
            }
        }
        private void StudyWord(Lesson lesson)
        {
            ClearViewModelFields();
            Word word = (Word)lesson;
            Header = "Новое слово: " + word.Keyword;
            Keyword = word.Keyword;
            Transcription = word.Transcription;
            Translation = word.Translation;
            AudioPath = word.AudioPath;
        }
        private void StudySentence(Lesson lesson)
        {
            ClearViewModelFields();
            Sentence sentence = (Sentence)lesson;
            Header = "Прочитайте и запомните предложение";
            SentenceLeftPart = sentence.SentenceLeftPart;
            SentenceRightPart = sentence.SentenceRightPart;
            MissingWord = sentence.MissingWord;
            Space = " " + sentence.MissingWord + " ";
            Translation = sentence.Translation;
            AudioPath = sentence.AudioPath;
        }
        private void StudyGrammar(Lesson lesson)
        {
            ClearViewModelFields();
            GrammarRule grammarRule = (GrammarRule)lesson;
            Header = grammarRule.Title;
            Translation = grammarRule.Content;
            AudioPath = grammarRule.AudioPath;
        }
        private void TakeATest(Lesson lesson)
        {
            ClearViewModelFields();
            GrammarTest grammarTest = (GrammarTest)lesson;
            Header = grammarTest.Title;
            Keyword = grammarTest.ExerciseText;
            Translation = "Выберите правильный ответ:";

            Random random = new Random();
            int answerOrder = random.Next(1, 3);

            if (answerOrder == 1)
            {
                Answer1 = grammarTest.RightAnswer;
                Answer2 = grammarTest.WrongAnswer1;
                Answer3 = grammarTest.WrongAnswer2;
            }
            if (answerOrder == 2)
            {
                Answer1 = grammarTest.WrongAnswer1;
                Answer2 = grammarTest.RightAnswer;
                Answer3 = grammarTest.WrongAnswer2;
            }
            if (answerOrder == 3)
            {
                Answer1 = grammarTest.WrongAnswer1;
                Answer2 = grammarTest.WrongAnswer2;
                Answer3 = grammarTest.RightAnswer;
            }
        }
        private void TestLearnedWords(Lesson lesson)
        {
            ClearViewModelFields();
            Word word = (Word)lesson;
            Header = "Как это по-английски? Напишите слово вместо звёздочек:";
            while (Space.Length < word.Keyword.Length)
            {
                Space += "*";
            }
            Space = "\" " + Space + " \"";
            Translation = word.Translation;
            AudioPath = word.AudioPath;
        }
        private void TestLearnedSentence(Sentence sentence)
        {
            ClearViewModelFields();
            Header = "Заполните пропущенное слово";
            SentenceLeftPart = sentence.SentenceLeftPart;
            SentenceRightPart = sentence.SentenceRightPart;
            MissingWord = sentence.MissingWord;
            while (Space.Length < sentence.MissingWord.Length)
            {
                Space += "_";
            }
            Space = " " + Space + " ";
            Translation = sentence.Translation;
            AudioPath = sentence.AudioPath;
        }
        public bool CheckLesson(string userInput)
        {
            if (Lessons.Count > 0)
            {
                if (selectedLesson.GetType() == typeof(Sentence) && selectedSentence != null)
                {
                    Sentence testedSentence = (Sentence)selectedLesson;
                    if (userInput == testedSentence.MissingWord)
                    {
                        if (testedSentence.IsLearned == false)
                        {
                            Lesson.MarkLearned(User.CurrentUser, testedSentence);
                        }

                        Lessons.Remove(selectedLesson);
                        return true;
                    }
                    else
                    {
                        Lessons.Remove(selectedLesson);
                        return false;
                    }
                }
                else if (selectedLesson.GetType() == typeof(GrammarTest))
                {
                    GrammarTest grammarTest = (GrammarTest)selectedLesson;
                    if (userInput == grammarTest.RightAnswer)
                    {
                        if (grammarTest.IsLearned == false)
                        {
                            Lesson.MarkLearned(User.CurrentUser, grammarTest);
                        }

                        Lessons.Remove(selectedLesson);
                        return true;
                    }
                    else
                    {
                        Lessons.Remove(selectedLesson);
                        return false;
                    }
                }
                else
                {
                    Lessons.Remove(selectedLesson);
                    return true; //If there is nothing to be checked - pass through
                }
            }
            else
            {
                if (wordsBuffer.Count > 0)
                {
                    if (userInput == selectedWord.Keyword)
                    {
                        if (selectedWord.IsLearned == false)
                        {
                            Lesson.MarkLearned(User.CurrentUser, selectedWord);
                        }
                        
                        wordsBuffer.Remove(selectedWord);
                        return true;
                    }
                    else
                    {
                        wordsBuffer.Remove(selectedWord);
                        return false;
                    }
                }
                else
                {
                    wordsBuffer.Remove(selectedWord); //Does it need to be here?
                    return true;
                }
            }
        }
        public bool CheckAvailability()
        {
            if (Lessons.Count > 0 || wordsBuffer.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ClearViewModelFields()
        {
            Header = "";
            Keyword = "";
            SentenceLeftPart = "";
            SentenceRightPart = "";
            MissingWord = "";
            Space = "";
            Transcription = "";
            Translation = "";
            Answer1 = null;
            Answer2 = null;
            Answer3 = null;
            AudioPath = "";
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
