using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnglishTrainer.Models;

public partial class Word
{
    public int WordId { get; set; }

    public int UserId { get; set; }

    public string EnglishVersion { get; set; } = null!;

    public string RussianVersion { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    //[NotMapped] public bool IsAnswered { get; set; } = false;

    //[NotMapped] public bool IsCorrect { get; set; } = false;
}
