﻿namespace ToDoList.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Item> items{ get; set; }
    }
}