using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_NEA
{
    public class Invitation : Event
    {
        private string desc;
        private string status;
        private string name;
        private DateTime start;
        private DateTime end;
        private string inviter;
        private string invitee;

        public Invitation(string invr, string invee, string n, DateTime s, DateTime e, string d, string status) : base(d, s, e, true, false, "None")
        {
            inviter = invr;
            invitee = invee;
            name = n;
            start = s;
            end = e;
            desc = d;
            this.status = status;
        }

        public void SaveInv()
        {
            string query = @"
INSERT INTO [Invitations] (Inviter, Invitee, EventName, EventStart, EventEnd, Description, Status) 
VALUES (@Inviter, @Invitee, @EventName, @EventStart, @EventEnd, @Description, @Status)";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                try
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Inviter", inviter); //inviter username 
                        cmd.Parameters.AddWithValue("@Invitee", invitee); //invitee username
                        cmd.Parameters.AddWithValue("@EventName", name); //event name 
                        cmd.Parameters.AddWithValue("@EventStart", start); //event start
                        cmd.Parameters.AddWithValue("@EventEnd", end); //event end 
                        cmd.Parameters.AddWithValue("@Description", desc); //description
                        cmd.Parameters.AddWithValue("@Status", status); //status
                        //excecutes instruction
                        cmd.ExecuteNonQuery();
                        Message(true, "Invitation sent");
                    }
                }
                catch (Exception ex)
                {
                    Message(false, "Invitation cannot be sent");
                }
            }
        }

        //returns the description of an instruction
        public string Desc()
        {
            return desc;
        }

        //returns the status of an instruction
        public string Status()
        {
            return status;
        }

        //changes status of an invitation
        public void ChangeStatus(string status, string username)
        {
            string query = @"
UPDATE [Invitations]
SET Status = @Status
WHERE Invitee = @Invitee
AND EventStart = @EventStart
AND EventEnd = @EventEnd";
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                try
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Invitee", username);
                        cmd.Parameters.AddWithValue("@EventStart", start);
                        cmd.Parameters.AddWithValue("@EventEnd", end);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.Message);
                }
            }
        }

        public void Accepted()
        {
            //removes the inviter's name from the event and adds the invitee's name so it excludes the user's name on the event so it doesn't say meeting up yourself
            string InviterEname = desc.Replace(inviter, invitee);
            //BUG BELOW!!
            var InviteeEvent = new Event($"{desc}", start, end, true, false, "None");
            var InviterEvent = new Event($"{InviterEname}", start, end , true, false, "None");
            //Meet up with {invitee}
            //Adds event to database
            InviterEvent.AddToDatabase(inviter);
            InviteeEvent.AddToDatabase(invitee);
        }

        //deletes instruction from database
        public void Delete()
        {
            string query = @"
DELETE FROM [Invitations]
WHERE Inviter = @Inviter
AND Invitee = @Invitee
AND EventStart = @EventStart
AND EventEnd = @EventEnd";
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                try
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Inviter", inviter);
                        cmd.Parameters.AddWithValue("@Invitee", invitee);
                        cmd.Parameters.AddWithValue("@EventStart", start);
                        cmd.Parameters.AddWithValue("@EventEnd", end);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.Message);
                }
            }
        }
    }
}
