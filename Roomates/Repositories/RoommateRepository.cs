﻿using Microsoft.Data.SqlClient;
using Roomates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString)  { } 
        
            public Roommate GetById(int id)
            {
                using(SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                    cmd.CommandText = @"SELECT FirstName, RentPortion, r.Name AS RoomName
                                        FROM Roommate rm
                                        JOIN Room r ON rm.RoomId = r.Id
                                        WHERE rm.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                            SqlDataReader reader = cmd.ExecuteReader();

                        Roommate roommate = null;

                        if (reader.Read())
                        {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            Room = new Room
                            {
                                Name = reader.GetString(reader.GetOrdinal("RoomName"))
                            }
                        };
                    }

                    reader.Close();

                        return roommate;

                    }

                }
            }
        
    }
}
