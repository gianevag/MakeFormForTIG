using Microsoft.AspNetCore.Http;
using MakeFormForTIG.ViewModels;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson;

namespace MakeFormForTIG.Models
{
    public class JewelryTable
    {
        public string  Id { get; set; }
        public  string Title { get; set; }
        public  float Price { get; set; }
        public string photo { get; set; }
    }
}

