using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MakeFormForTIG.Models
{
    public class Jewelry
    {
        [BsonId]
        public virtual ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [Required]
        public virtual string Title { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "({0:C})")]
        public virtual float Price { get; set; }
        public string first_thumb_photo { get; set; }
        public string second_thumb_photo { get; set; }
        public string yellowPhoto { get; set; }
        public string whitePhoto { get; set; }
        public string rosePhoto { get; set; }
        [Required]
        public string etsyUrl { get; set; }
        public string amazonUrl { get; set; }
        public string amazonUKUrl { get; set; }
        public string dawandaUrl { get; set; }
        public int  isFirstPage { get; set; }
        public int orderInFirstPage { get; set; }
        public int isNecklace { get; set; }
        public string typeOfNecklace { get; set; }
        public int isRing { get; set; }
        public string typeOfRing { get; set; }
        public int isBracelet { get; set; }
        public string typeOfBracelet { get; set; }
        public int isEarring { get; set; }
        public string typeOfEarring { get; set; }
        public int isPersonalized { get; set; }
        public string typeOfPersonalized { get; set; }
        public int isBirthstone { get; set; }
        public string typeOfBirthstone { get; set; }
        public int isDiamond { get; set; } 
        public string typeOfDiamond { get; set; }
        public int orderInCatalog_lv1 { get; set; }
        public int orderInCatalog_lv2 { get; set; }
        public int orderInCatalog_lv3 { get; set; }
        [Required]
        public string quickReview { get; set; }
        [Required]
        public string moreInfo { get; set; }
        public DateTime dateCreated { get; set; } = DateTime.Now;
        [Required]
        public string JewelryId { get; set; }
    }
}

