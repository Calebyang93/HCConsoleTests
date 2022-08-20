using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HC.Model;
using HC.Utils;

namespace HC.Data
{
    public class HCRepository
    {
        private string connStr = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HCData;Data Source=DESKTOP-2AS0J7C\SQLEXPRESS";

        #region Chores
        public List<Chore> ChoresGetAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * FROM HouseholdChores", new SqlConnection(connStr));
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    List<Chore> lst = new List<Chore>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Chore c = new Chore(
                            Convert.ToInt32(dr["ID"]),
                            Convert.ToString(dr["Name"]),
                            Convert.ToString(dr["ResourceCSV"]),
                            Convert.ToDateTime(dr["CreatedOn"]));

                        lst.Add(c);
                    }
                    return lst;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception in GetChoresAll", e);
            }

        }

        public void ChoreLogAddOrUpdate()
        {
            
        }

        public void ChoreAddOrUpdate()
        {
            
        }

        public bool ChoreExists(int cid)
        {
            /*
                SqlCommand cmd = new SqlCommand("Select * FROM HouseholdChores where ChoreID = {cid}", new SqlConnection(connStr));
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0].Rows.Count() > 0;
                }
             */
            return true;
        }

        public void ChoreDelete(int id)
        {
            try
            {
                string sql = $@"DELETE FROM [dbo].[HouseholdChores]
                WHERE ID = {id}";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception($"Exception in Deleting Chore with id: {id}", e);
            }
        }

        public bool DoneOnExists(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public void ChoreAdd(Chore c)
        {
            try
            {
                string sql = $@"INSERT INTO [dbo].[HouseholdChores]
           ([ID]
           ,[Name]
           ,[ResourceCSV]
           ,[CreatedOn])
        VALUES
          ( {c.ID},
		   '{c.Name}',
		   '{c.Resources.ToCsv(",", '"')}',
            '{c.CreatedOn.ToString("yyyy-MM-dd")}'
		   )";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Exception in AddChore", e);
            }

        }

        public void ChoreUpdate(Chore c)
        {
            try
            {
                //,[CreatedOn] = '{c.CreatedOn.ToSQLDateFormat()}'

                string sqlUpdate = $@"UPDATE [dbo].[HouseholdChores]
                SET
                   [Name] = '{c.Name}'
                  ,[ResourceCSV] = '{c.Resources.ToCsv(",", '"')}'
                  ,[CreatedOn] = '{c.CreatedOn.ToSQLDateFormat()}'
                WHERE ID = {c.ID}";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Exception in Updating Chore.", e);
            }
        }

        public void ChoreAddOrUpdate(Chore c)
        {
            int cid = c.ID;
            if (ChoreExists(c.ID))
            {
                ChoreUpdate(c);
            }
            else
            {
                ChoreAdd(c);
            }


        }
        #endregion

        #region ChoreLogs
        // Implement ChoreLogs CRUD functions //
        public List<ChoreLog> ChoreLogsGetByChoreID(int choreID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand($"Select * FROM ChoresLog WHERE ID = {choreID}", new SqlConnection(connStr));
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    List<ChoreLog> lst = new List<ChoreLog>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ChoreLog cl = new ChoreLog();
                        cl.ID = new Guid(dr["ID"].ToString());
                        cl.ChoreID = Convert.ToInt32(dr["ChoreID"]);
                        cl.Note = Convert.ToString(dr["Note"]);
                        cl.DoneOn = Convert.ToDateTime(dr["DoneOn"]);
                        cl.DoneBy = Convert.ToString(dr["DoneBy"]);
                        lst.Add(cl);
                    }
                    return lst;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception in ChoreLogsGetByChoreID", e);
            }
        }

        public void ChoreLogDelete(int id)
        {
            try
            {
                string sql = $@"DELETE FROM [dbo].[ChoresLog]
                WHERE ID = {id}";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception($"Exception in Deleting Chore with id: {id}", e);
            }
        }

        public void ChoreLogAdd(ChoreLog cl)
        {
            string sql;
            try
            {
                sql = $@"INSERT INTO [dbo].[ChoresLog]
                                   ([ID]
                                   ,[ChoreID]
                                   ,[DoneOn]
                                   ,[Note]
                                   ,[DoneBy])
                             VALUES
                                   ('{cl.ID}', 
			                        '{cl.ChoreID}',
			                        '{cl.DoneOn.ToSQLDateFormat()}',
			                        '{cl.Note}',
			                        '{cl.DoneBy}')";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Exception in ChoreLogAdd", e);
            }

        }

        public void ChoreLogUpdate(ChoreLog cl)
        {
            try
            {
                string sqlUpdate = $@"UPDATE [dbo].[ChoresLog]
                SET
                   [ID] = '{cl.ID}'
                  ,[ChoreID] = '{cl.ChoreID}'
                  ,[DoneOn] = '{cl.DoneOn}'
                  ,[Note] = '{cl.Note}'
                  ,[DoneBy] = '{cl.DoneBy}'
                WHERE ID = {cl.ID}";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Exception in Updating Chore.", e);
            }
        }

        public void ChoreLogAddOrUpdate(ChoreLog cl)
        {
            int cid = cl.ChoreID;
            if (ChoreLogExist(cl))
            {
                ChoreLogUpdate(cl);
            }
            else
            {
                ChoreLogAdd(cl);
            }

        }

        private bool ChoreLogExist(ChoreLog cl)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
