﻿namespace DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}