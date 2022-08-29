using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricat
{
    internal class Lesson : DatabaseItem
    {
        public int Id { get; set; }
        public bool Learned { get; set; } = false;
        public string Level { get; set; }
        public string AudioPath { get; set; }
        public void LoadLessonsFromDB(User user)
        {
            List<Word> words = Word.LoadWordsFromDB(user);
            List<Sentence> sentences = Sentence.LoadSentencesFromDB(user);
            //GrammarRule
            //GrammarTest
        }
        internal void MarkLearned(User user, Lesson lesson)
        {
            this.Learned = true;
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
            else if (lesson is GrammarRule)
            {
                sqlExpression = @"INSERT INTO LearnedGrammar (GrammarRuleId, UserId)
                                  VALUES (@LessonId, @UserId)";
            }
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                SqliteParameter lessonIdParam = new SqliteParameter("@LessonId", this.Id);
                command.Parameters.Add(lessonIdParam);
                SqliteParameter userIdParam = new SqliteParameter("@UserId", user.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
