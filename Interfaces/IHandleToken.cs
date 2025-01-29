using System;
using _net.Models;

namespace _net.Interfaces;

public interface IHandleToken
{
    public string GenerateToken(UserModel user);
}
