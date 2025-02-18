
namespace UserRegClient.Models
{
    public class User
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public string Surname{get; set;}
        public int Age{get; set;}
        public string Email{get; set;}
        public List<SpecialistRank> SpecialistRank { get; set; }
    }
}

    /*CREATE TABLE Users (

        Id SERIAL PRIMARY KEY,
        Name VARCHAR(50),
        Surname VARCHAR(50),
        Age INT,
        Email VARCHAR(50)
    );


    ALTER TABLE users
    ADD COLUMN XamlData xml;
    */


    