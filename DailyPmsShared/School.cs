﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace DailyPmsShared
{
    public class School : MongoEntity
    {
        //public string SchoolID { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [StringLength(100, ErrorMessage = "Le nom doit comporter au moins 2 caractères !", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [StringLength(20, ErrorMessage = "Le nom ne peut pas comporter plus de 20 caractères !")]
        public string Moniker { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        public string City { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [Phone(ErrorMessage = "Veuillez insérer un numéro de téléphone valide")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        [EmailAddress(ErrorMessage = "Veuillez insérer une adresse email valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez compléter ce champ")]
        public string DirectorName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PmsCenterID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ClasseIDs { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentIDs { get; set; }
    }
}
