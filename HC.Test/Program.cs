using HC.Data;
using HC.Model;
using System;

namespace HC.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetAll();
            //AddChore();
            //UpdateChore();
            //DeleteChore();
            //ChoreLogsGetByChoreIDTest(4);
            //ChoreLogAdd();
            //ChoreLogAddUI();
            ChoreAddOrUpdate();
            //ChoreLogAddOrUpdate();  test after ChoreAddorUpdate works...
        }

        private static void ChoreAddOrUpdate()
        {
            HCRepository hcr = new HCRepository();
            hcr.ChoreAddOrUpdate();
        }
        
        private static void ChoreLogAddOrUpdate()
        {
            HCRepository hcr = new HCRepository();
            hcr.ChoreLogAddOrUpdate();
        }

        private static void ChoreLogAddUI()
        {
            try
            {
                HCRepository hcr = new HCRepository();
                ChoreLog cl = new ChoreLog();

                string s;
                Console.WriteLine("Chore ID?");
                s = Console.ReadLine();
                //int cid = Convert.ToInt32(s);
                int cid;   
                int n = 1;
                while (Int32.TryParse(s, out cid) == false)
                {
                    Console.WriteLine("Wrong format! Please type a number.");
                    s = Console.ReadLine();
                    n++;
                    if (n > 2)
                        break;
                }
                if (n > 1)
                {
                    Console.WriteLine("Sorry, you have exceeded the number of tries!");
                    return;
                }

                if (!hcr.ChoreExists(cid))
                {
                    Console.WriteLine("Sorry, no chore with that ID!");
                    return;
                }
                cl.ChoreID = cid;

                Console.WriteLine("Done on?");
                s = Console.ReadLine();
                DateTime dt = Convert.ToDateTime(s);     
                int n2= 1;
                while (DateTime.TryParse(s,out dt) == false )
                {
                    Console.WriteLine("Wrong date format! Please enter a valid date in YYYY-MM-dd.");
                    s = Console.ReadLine();
                    n2++;
                    if (n2 > 2)
                    {
                        break;                        
                    }
                }
                if (n2 > 1)
                {
                    Console.WriteLine("Sorry, you have exceeded the number of tries!");
                    return;
                } 
                if (!hcr.DoneOnExists(dt))
                {
                    Console.WriteLine("Sorry, no Done On with that date");
                    return;
                }
                cl.DoneOn = dt;
                // TODO: validate date time based on ID done above. 
                // TODO: Check if user input a date in the past. If user input a date in the present or future,
                // prompt the user to add another date that should be in the past. The date should be in the last 30 days. 

                Console.WriteLine("Done by?");
                s = Console.ReadLine();
                cl.DoneBy = s;

                Console.WriteLine("Note [press ENTER for none]?");
                s = Console.ReadLine();
                cl.Note = s;

                hcr.ChoreLogAdd(cl);              
            }
            catch (Exception e)
            {
               Console.WriteLine($"Error: {e.Message }");
            }
        }

        private static void ChoreLogAdd()
        {
            try
            {
                HCRepository hcr = new HCRepository();
                ChoreLog cl = new ChoreLog();
                cl.ChoreID = 2;
                cl.DoneOn = DateTime.Now.AddDays(-5);
                //cl.DoneOn = Convert.ToDateTime("2022-08-17");
                cl.Note = "yy";
                cl.DoneBy = "Donald Duck";
                hcr.ChoreLogAdd(cl);
                // TODO: allow user to input properties for chorelog //
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message }");
            }
        }

        private static void ChoreLogsGetByChoreIDTest(int ChoreID)
        {
            try
            {
                HCRepository hcr = new HCRepository();
                hcr.ChoreLogsGetByChoreID(ChoreID);
            }
            catch (Exception e)
            {
                new Exception($"Error: {e.Message }");
            }
        }

        private static void DeleteChore ()
        {
            try
            {
                HCRepository hcr = new HCRepository();
                hcr.ChoreDelete(8);    
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message }");
            }
        }

        private static void UpdateChore()
        {
            try
            {
                HCRepository hcr = new HCRepository();
                //Chore c = new Chore()
                //{
                //    ID = 8,
                //    Name = "Mop the Floor",
                //    ResourceCSV = "Mop, Vaccum Cleaner, Detergent",
                //    CreatedOn = DateTime.Now
                //};
                Chore c = new Chore(8, "Mop the floor again", "Mop,Vaccum Cleaner,Detergent", DateTime.Now);
                hcr.ChoreUpdate(c);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void AddChore()
        {
            try
            {
                HCRepository hcr = new HCRepository();
                //Chore c = new Chore()
                //{
                //     ID = 8,
                //     Name = "Burn your house",
                //     ResourceCSV = "Kerosene,Lighter",
                //      CreatedOn = DateTime.Now
                //};
                Chore c = new Chore(8, "Burn your house", "Kerosene,Lighter", DateTime.Now);
                hcr.ChoreAdd(c);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void GetAll()
        {
           try
            {
                HCRepository hcr = new HCRepository();
                var lst = hcr.ChoresGetAll();
                foreach (var item in lst)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
