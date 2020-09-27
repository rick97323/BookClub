using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice_46
{
    class Example1
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            SqlConnection myConnection = new SqlConnection(connstring);
            var mySqlCommand = new SqlCommand(commandString, myConnection);

            myConnection.Open();
            mySqlCommand.ExecuteNonQuery();
        }
    }

    class Example2
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            SqlConnection myConnection = new SqlConnection(connstring);
            var mySqlCommand = new SqlCommand(commandString, myConnection);

            myConnection.Open();
            mySqlCommand.ExecuteNonQuery();

            myConnection.Dispose();
            mySqlCommand.Dispose();
        }
    }

    class Example3
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            using (SqlConnection myConnection = new SqlConnection(connstring))
            {
                using (var mySqlCommand =
                    new SqlCommand(commandString, myConnection))
                {
                    myConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
        }
    }

    class SqlExample3_Continue
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            using (SqlConnection myConnection = new SqlConnection(connstring))
            {
                using (var mySqlCommand =
                    new SqlCommand(commandString, myConnection))
                {
                    myConnection.Open();
                    mySqlCommand.ExecuteNonQuery();
                }
            }
        }
    }

    class DisposableTest
    {
        public void UsingTest()
        {
            //using (var test = new DisposableTest())
            //{

            //}
        }
    }

    class Example5
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            SqlConnection myConnection = null;
            SqlCommand mySqlCommand = null;
            try
            {
                myConnection = new SqlConnection(connstring);
                mySqlCommand = new SqlCommand(commandString, myConnection);
                myConnection.Open();
                mySqlCommand.ExecuteNonQuery();
            }
            finally
            {
                myConnection?.Dispose();
                mySqlCommand?.Dispose();
            }
        }
    }

    class Example6
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            SqlConnection myConnection = new SqlConnection(connstring);
            SqlCommand mySqlCommand = new SqlCommand(commandString, myConnection);

            using (myConnection as IDisposable)
            using (mySqlCommand as IDisposable)
            {
                myConnection.Open();
                mySqlCommand.ExecuteNonQuery();
            }
        }
    }

    class Example_Close
    {
        public void ExcuteCommand(string connstring, string commandString)
        {
            SqlConnection myConnection = null;
            try
            {
                myConnection = new SqlConnection(connstring);
                SqlCommand mySqlCommand = 
                    new SqlCommand(commandString, myConnection);
                myConnection.Open();
                mySqlCommand.ExecuteNonQuery();
            }
            finally
            {
                myConnection?.Close();
            }
        }
    }
}
