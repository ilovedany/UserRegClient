namespace UserRegClient.Models
{
    public class SpecialistRank
    {
        public int Id_rank { get; set; }
        public string Rank { get; set; }
        public int Id { get; set; }
    }
}

    /*CREATE TABLE SpecialistRank (

        Id_rank SERIAL PRIMARY KEY,
        Rank VARCHAR(50),
        Id INT,
        FOREIGN KEY (Id) REFERENCES Users(Id)

    );
    */
