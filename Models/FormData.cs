using Microsoft.AspNetCore.Http;
using MakeFormForTIG.ViewModels;
using System.ComponentModel.DataAnnotations;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson;

namespace MakeFormForTIG.Models
{
    public class FormData
    {

        public Jewelry jewelry { get; set; }
        public EarringViewModels Earrings { get; set; } = new EarringViewModels();
        public PersonalizedViewModels Personalized { get; set; } = new PersonalizedViewModels();
        public BirthstonesViewModels Birthstones{ get; set; } = new BirthstonesViewModels();
        public DiamondViewModels Diamond { get; set; } = new DiamondViewModels();
        public RingsViewModels Rings { get; set; } = new RingsViewModels();
        public NecklacesViewModels Necklaces { get; set; } = new NecklacesViewModels();
        public BraceletsViewModels Bracelets { get; set; } = new BraceletsViewModels();
        public OrderInFirstPageViewModel OrderInFirstPage { get; set; } = new OrderInFirstPageViewModel();

        [Required]
        public IFormFile first_thumb_file { get; set; }
        [Required]
        public IFormFile second_thumb_file { get; set; }
        [Required]
        public IFormFile yellowPhoto_file { get; set; }
        [Required]
        public IFormFile whitePhoto_file { get; set; }
        [Required]
        public IFormFile rosePhoto_file { get; set; }

    }
}

