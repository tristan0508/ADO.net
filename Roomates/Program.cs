﻿using Roomates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roommates
{
    public class Program
    {

        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
            
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository repo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        ShowRooms(roomRepo);
                        break;

                    case ("Search for room"):
                        SearchRoom(roomRepo);
                        break;

                    case ("Add a room"):
                        AddRoom(roomRepo);
                        break;

                    case ("Update a room"):
                        UpdateRoom(roomRepo);
                        break;

                    case ("Delete a room"):
                        DeleteRoom(roomRepo);
                        break;

                    case ("Add a chore"):
                        List<Chore> chores = choreRepo.GetAll();
                        break;
                    case ("Find a roommate"):
                        FindARoomate(repo);
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;
                }

            }

            static void FindARoomate(RoommateRepository repo)
            {
                Console.WriteLine();
                Console.WriteLine("Roommate Id: ");

                int id = int.Parse(Console.ReadLine());
                Roommate roommate = repo.GetById(id);

                Console.WriteLine($"{roommate.Firstname} {roommate.RentPortion} {roommate.Room.Name}");

                Console.Write("Press any key to continue");
                Console.ReadKey();
            }

            static void ShowRooms(RoomRepository roomRepo)
            {
                List<Room> rooms = roomRepo.GetAll();
                foreach (Room r in rooms)
                {
                    Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                }
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }

            static void SearchRoom(RoomRepository roomRepo)
            {
                Console.Write("Room Id: ");
                int id = int.Parse(Console.ReadLine());

                Room room = roomRepo.GetById(id);

                Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }

            static void AddRoom(RoomRepository roomRepo)
            {
                Console.Write("Room name: ");
                string name = Console.ReadLine();

                Console.Write("Max occupancy: ");
                int max = int.Parse(Console.ReadLine());

                Room roomToAdd = new Room()
                {
                    Name = name,
                    MaxOccupancy = max
                };

                roomRepo.Insert(roomToAdd);

                Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }

            static void UpdateRoom(RoomRepository roomRepo)
            {
                List<Room> roomOptions = roomRepo.GetAll();
                foreach (Room r in roomOptions)
                {
                    Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                }

                Console.Write("Which room would you like to update? ");
                int selectedRoomId = int.Parse(Console.ReadLine());
                Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                Console.Write("New Name: ");
                selectedRoom.Name = Console.ReadLine();

                Console.Write("New Max Occupancy: ");
                selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                roomRepo.Update(selectedRoom);

                Console.WriteLine($"Room has been successfully updated");
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }
        


            static string GetMenuSelection()
            {
                Console.Clear();

                List<string> options = new List<string>()
                {
                    "Show all rooms",
                    "Search for room",
                    "Add a room",
                    "Update a room",
                    "Delete a room",
                    "Find a roommate",
                    "Exit"
                };

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Select an option > ");

                        string input = Console.ReadLine();
                        int index = int.Parse(input) - 1;
                        return options[index];
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }

            }
        }

       static void DeleteRoom(RoomRepository roomRepo)
        {
            Console.WriteLine("What is the ID of the room you want to delete? ");
            int roomId = int.Parse(Console.ReadLine());
            roomRepo.Delete(roomId);

        }
    }
}