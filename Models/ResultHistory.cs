using System;
using System.Collections.Generic;

namespace EnglishTrainer.Models;

public partial class ResultHistory
{
    public int ResultHistoryId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public double CurrentAnswerPersentage { get; set; }

    public virtual User User { get; set; } = null!;
}
