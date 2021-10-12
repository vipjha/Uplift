﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Service Name")]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name = "Description")]
        public string LongDesc { get; set; }


        [Column(TypeName = "varchar(MAX)")]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }

    }
}
