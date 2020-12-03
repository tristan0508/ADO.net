using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Roomates.Models;

namespace Roommates.Repositories
{

    public class RoomRepository : BaseRepository
    {
        public RoomRepository(string connectionString) : base(connectionString) { }

        public List<Room> GetAll()
        {

            using (SqlConnection conn = Connection)
            {
           
                conn.Open();

            
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = "SELECT Id, Name, MaxOccupancy FROM Room";

                    
                    SqlDataReader reader = cmd.ExecuteReader();

                  
                    List<Room> rooms = new List<Room>();

                   
                    while (reader.Read())
                    {
                
                        int idColumnPosition = reader.GetOrdinal("Id");

                        
                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int maxOccupancyColunPosition = reader.GetOrdinal("MaxOccupancy");
                        int maxOccupancy = reader.GetInt32(maxOccupancyColunPosition);

                        Room room = new Room
                        {
                            Id = idValue,
                            Name = nameValue,
                            MaxOccupancy = maxOccupancy,
                        };

                        
                        rooms.Add(room);
                    }

                    reader.Close();

                    return rooms;
                }
            }
        }
    }
}