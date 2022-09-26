﻿using Apricat;
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
        public string Asnwer1
        {
            get { return answer1; }
            set
            {
                answer1 = value;
                OnPropertyChanged("Annswer1");
            }
        }
        private string answer2;
        public string Asnwer2
        {
            get { return answer2; }
            set
            {
                answer2 = value;
                OnPropertyChanged("Annswer1");
            }
        }
        private string answer3;
        public string Asnwer3
        {
            get { return answer3; }
            set
            {
                answer3 = value;
                OnPropertyChanged("Annswer1");
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
        private bool availableLessons = true;

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
                selectedLesson = Lessons[0];

                if (selectedLesson.GetType() == typeof(Sentence))
                {
                    StudySentence(selectedLesson);
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
                else
                {
                    availableLessons = false;
                }
            }
        }
        private void StudyWord(Lesson lesson)
        {
            Word word = (Word)lesson;
            Header = "Новое слово: " + word.Keyword;
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
            Header = grammarRule.Title;
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
            Header = grammarTest.Title;
            Keyword = "";
            SentenceLeftPart = "";
            SentenceRightPart = "";
            MissingWord = "";
            Space = "";
            Transcription = "";
            Translation = "";
            AudioPath = "";

        }
        private void TestLearnedWords(Lesson lesson)
        {
            Word word = (Word)lesson;
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
        public bool CheckLesson(string userInput)
        {
            if (Lessons.Count > 0)
            {
                if (selectedLesson.GetType() == typeof(Sentence))
                {
                    Sentence testedSentence = (Sentence)selectedLesson;
                    if (userInput == testedSentence.MissingWord)
                    {
                        Lesson.MarkLearned(User.CurrentUser, testedSentence);
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
                        Lesson.MarkLearned(User.CurrentUser, grammarTest);
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
                        Lesson.MarkLearned(User.CurrentUser, selectedWord);
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
                    return false;
                }
            }
        }
        public bool CheckAvailability()
        {
            if (availableLessons)
            {
                return true;
            }
            else
            {
                return false;
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
