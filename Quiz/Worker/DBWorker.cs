using Quiz.Model;
using Quiz.Support.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media;

namespace Quiz.Worker
{
    class DBWorker
    {
        public ObservableCollection<Question> GetQuestions() {
            ObservableCollection<Question> questions = new ObservableCollection<Question>();

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.QuizDBConnectionString)) {
                SqlCommand cmd = new SqlCommand("GetAllQuestions", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable questionsTable = new DataTable();
                questionsTable.Load(cmd.ExecuteReader());
                

                foreach(DataRow row in questionsTable.Rows) {
                    questions.Add(new Question(
                        (string)row.ItemArray[1],
                        (TimeSpan)row.ItemArray[4],
                        (float)row.ItemArray[3],
                        (string)row.ItemArray[2],
                        (string)row.ItemArray[5],
                        (string)row.ItemArray[6]
                    ));
                }
            }

            return questions;
        }

        public ObservableCollection<Player> GetAllPlayers()
        {
            ObservableCollection<Player> players = new ObservableCollection<Player>();

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.QuizDBConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetAllPlayers", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable playerTable = new DataTable();
                playerTable.Load(cmd.ExecuteReader());


                foreach (DataRow row in playerTable.Rows)
                {
                    string playerName = row.ItemArray[0].ToString();
                    double playerPoints = Convert.ToDouble(row.ItemArray[2]);
                    Color playerColor = Convert.ToInt32(row.ItemArray[1]).ToColor();

                    players.Add(new Player(playerName, playerPoints, playerColor));
                }
                connection.Close();
            }

            return players;
        }
    }
}
