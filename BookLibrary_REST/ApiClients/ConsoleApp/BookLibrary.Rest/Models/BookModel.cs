﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace ConsoleApp.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class BookModel
    {
        /// <summary>
        /// Initializes a new instance of the BookModel class.
        /// </summary>
        public BookModel() { }

        /// <summary>
        /// Initializes a new instance of the BookModel class.
        /// </summary>
        public BookModel(int? id = default(int?), string title = default(string), string author = default(string), string genre = default(string), string description = default(string), int? quantity = default(int?))
        {
            ID = id;
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
            Quantity = quantity;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ID")]
        public int? ID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Author")]
        public string Author { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Genre")]
        public string Genre { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Quantity")]
        public int? Quantity { get; set; }

    }
}
