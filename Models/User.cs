using System;
using System.Collections.Generic;

namespace EnglishTrainer.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public virtual ICollection<Word> Words { get; } = new List<Word>();
}
