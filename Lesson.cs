using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    public abstract class Lesson : DatabaseItem
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string AudioPath { get; set; }
        public bool IsLearned { get; set; }
        public static ObservableCollection<Lesson> LoadLessonsFromDB(User user)
        {
            ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>();
            ObservableCollection<Word> words = Word.LoadWordsFromDB(user);
            foreach (Word word in words)
            {
                lessons.Add(word);
            }
            ObservableCollection<Sentence> sentences = Sentence.LoadSentencesFromDB(user);
            foreach (Sentence sentence in sentences)
            {
                lessons.Add(sentence);
            }
            GrammarRule grammarRule = GrammarRule.LoadRuleFromDB(user);
            lessons.Add(grammarRule);
            GrammarTest grammarTest = GrammarTest.LoadTestFromDB(grammarRule);
            lessons.Add(grammarTest);

            return lessons;
        }
        public static void MarkLearned(User user, Lesson lesson)
        {
            if (lesson is Word)
            {
                sqlExpression = @"INSERT INTO LearnedWords (WordId, UserId)
                                  VALUES (@LessonId, @UserId)";
            }
            else if (lesson is Sentence)
            {
                sqlExpression = @"INSERT INTO LearnedSentences (SentenceId, UserId)
                                  VALUES (@LessonId, @UserId)";
            }
            else if (lesson is GrammarTest)
            {
                sqlExpression = @"INSERT INTO LearnedGrammar (GrammarRuleId, UserId)
                                  VALUES (@LessonId, @UserId)";
            }
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter lessonIdParam = new SqliteParameter("@LessonId", lesson.Id);
                command.Parameters.Add(lessonIdParam);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.Parameters.Add(userIdParam);
                command.ExecuteNonQuery();
            }
        }
    }
}
