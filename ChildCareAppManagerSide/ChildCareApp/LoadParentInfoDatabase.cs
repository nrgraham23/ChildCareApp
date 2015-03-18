﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
//using System.DateTime; 

namespace ChildCareApp {

    class LoadParentInfoDatabase {

        /*private SQLiteConnection dbCon;

        public LoadParentInfoDatabase()
        {
            dbCon = new SQLiteConnection("Data Source=../../ChildCare_v3.s3db;Version=3;");  
        }//end Database*/


        private MySql.Data.MySqlClient.MySqlConnection dbConn;
        private string server;
        private string port;
        private string database;
        private string UID;
        private string password;
        private string connectionString;

        public LoadParentInfoDatabase() {
            this.server = "146.187.135.22";
            this.port = "3306";
            this.database = "childcare_v5";
            this.UID = "ccdev";
            this.password = "devpw821";
            connectionString = "SERVER="+server+"; PORT="+port+"; DATABASE="+database+"; UID="+UID+"; PASSWORD="+password+";";
            dbConn = new MySql.Data.MySqlClient.MySqlConnection();
            dbConn.ConnectionString = connectionString;
        }//end Database(default constructor)

        public LoadParentInfoDatabase(string server, string port, string database, string UID, string password) {
            this.server = server;
            this.port = port;
            this.database = database;
            this.UID = UID;
            this.password = password;
            connectionString = server + "; PORT=" + port + "; DATABASE=" + database + "; UID=" + UID + "; PASSWORD=" + password + ";";
            dbConn = new MySql.Data.MySqlClient.MySqlConnection();
            dbConn.ConnectionString = connectionString;
        }//end Database

        public DataSet GetParentInfo(string parentID) {

            dbConn.Open();
            string sql = "select * from Guardian where Guardian_ID = " + parentID;
            //SQLiteCommand command = new SQLiteCommand(sql, this.conn);
            MySqlCommand command = new MySqlCommand(sql, dbConn);

            //SQLiteDataAdapter DB = new SQLiteDataAdapter(command);
            MySqlDataAdapter DB = new MySqlDataAdapter(command); 
            DataSet DS = new DataSet();
            DB.Fill(DS);


            dbConn.Close();        
            return DS; 
        }//end GetFirstName

        public void DeleteParentInfo(string parentID)
        {

            dbConn.Open();
            try
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd"); 
                //string sql = "DELETE from Guardian where Guardian_ID = " + parentID;
                string sql = @"UPDATE Guardian SET GuardianDeletionDate = @today WHERE Guardian_ID = @parentID;"; 
                //SQLiteCommand command = new SQLiteCommand(sql, this.dbConn);
                MySqlCommand command = new MySqlCommand(sql, dbConn);
                command.CommandText = sql;
                command.Parameters.Add(new MySqlParameter("@today", today));
                command.Parameters.Add(new MySqlParameter("@parentID", parentID));

                command.ExecuteNonQuery();

                MessageBox.Show("Completed");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Failed");
            }
            dbConn.Close();
            
        }//end GetFirstName

        public void AddNewParent(string ID, string PIN, string firstName, string lastName, string phone, string email, string address, string address2, string city, string state, string zip, string photo) {
            dbConn.Open();

            try
            {
           
                string sql = @"INSERT INTO Guardian(Guardian_ID, GuardianPIN, FirstName, LastName, Phone, Email, Address1, Address2, City, StateAbrv, Zip, PhotoLocation) "+
                "VALUES(" + ID + ", " + PIN + ", " + firstName + ", " + lastName + ", " + phone + ", " + email + ", " + address + ", " + address2 + ", " + city + ", " + state + ", " + zip + ", " + photo + ");";
                MySqlCommand mycommand = new MySqlCommand(sql, dbConn);
                mycommand.ExecuteNonQuery();
                MessageBox.Show("Completed");
            }

            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString()); 
            }
            dbConn.Close();  
        }


        public void UpdateParentInfo(string ID, string firstName, string lastName, string phone, string email, string address, string address2, string city, string state, string zip) {
            dbConn.Open();

            try
            {

                string sql = @"UPDATE Guardian SET FirstName = @firstName, LastName = @lastName, Phone = @phone, Email = @email," +
                                    "Address1 = @address, Address2 = @address2, City = @city, StateAbrv = @state, Zip  = @zip WHERE Guardian_ID = @ID;";
                //SQLiteCommand mycommand = new SQLiteCommand(sql, this.dbConn);
                MySqlCommand mycommand = new MySqlCommand(sql, dbConn);
                mycommand.CommandText = sql;
                mycommand.Parameters.Add(new MySqlParameter("@firstName", firstName));
                mycommand.Parameters.Add(new MySqlParameter("@lastName", lastName));
                mycommand.Parameters.Add(new MySqlParameter("@phone", phone));
                mycommand.Parameters.Add(new MySqlParameter("@email", email));
                mycommand.Parameters.Add(new MySqlParameter("@address", address));
                mycommand.Parameters.Add(new MySqlParameter("@address2", address2));
                mycommand.Parameters.Add(new MySqlParameter("@city", city));
                mycommand.Parameters.Add(new MySqlParameter("@state", state));
                mycommand.Parameters.Add(new MySqlParameter("@zip", zip));
                mycommand.Parameters.Add(new MySqlParameter("@ID", ID));

                mycommand.ExecuteNonQuery();
                MessageBox.Show("Completed");
            }

            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            dbConn.Close();
        }
        public DataSet checkIfFamilyExists(string familyID) {
            dbConn.Open();
            string sql = "select * from Family where Family_ID = " + familyID;
            MySqlCommand command = new MySqlCommand(sql, dbConn);

            MySqlDataAdapter DB = new MySqlDataAdapter(command);
            DataSet DS = new DataSet();
            DB.Fill(DS);


            dbConn.Close();
            return DS; 
        }
        public void AddNewFamily(string familyID, double ballance) {

            dbConn.Open();

            try
            {

                string sql = @"INSERT INTO Family(Family_ID, FamilyTotal) " +
                "VALUES(" + familyID + ", " + ballance + ");";
                MySqlCommand mycommand = new MySqlCommand(sql, dbConn);
                mycommand.ExecuteNonQuery();
                MessageBox.Show("Completed");
            }

            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            dbConn.Close();
        }

    }//end LoadParentInfoDatabase

}//end namespace
