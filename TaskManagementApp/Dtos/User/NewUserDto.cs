﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Dtos.User
{
    public class NewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}