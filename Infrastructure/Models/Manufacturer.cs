using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public class Manufacturer
{
    public int id { get; set; }
    
    public string name { get; set; }
    
}