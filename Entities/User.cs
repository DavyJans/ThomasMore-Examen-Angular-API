﻿namespace AngularAPI.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }

    public Role Role { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
}

public enum Role
{
    Guest = 1,
    User = 2,
    CompanyAdmin = 3,
    SuperAdmin = 4
}