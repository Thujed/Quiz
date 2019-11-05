using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Quiz.Supports.Worker
{
    class DBWorker
    {
        private SqlConnection _connection = new SqlConnection(Quiz.Properties.Settings.Default.QuizDBConnectionString);


        public ObservableCollection<Question> GetQuestions() {
            ObservableCollection<Question> questions = new ObservableCollection<Question>();
            using (_connection) {
                SqlCommand cmd = new SqlCommand("GetAllQuestions", _connection);
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
            using (_connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllPlayers", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable playerTable = new DataTable();
                playerTable.Load(cmd.ExecuteReader());


                foreach (DataRow row in playerTable.Rows)
                {
                    players.Add(new Player(
                        (string)row.ItemArray[0],
                        (int)row.ItemArray[2],
                        (Color)row.ItemArray[1]
                    ));
                }
            }

            return players;
        }
    }
}
