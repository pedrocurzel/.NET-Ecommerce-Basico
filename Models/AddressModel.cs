using System;
using System.ComponentModel.DataAnnotations;

namespace _net.Models;

public class AddressModel
{
    [Key]
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Code { get; set; }

    public int UserModelId { get; set; }
    public UserModel UserModel {get;set;}
}
