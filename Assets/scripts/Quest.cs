public class Quest
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; set; }

    public int ImportanceScore { get; private set; } // сколько очков дает
    public string FailureExplanation { get; private set; } // пояснение при провале

    public Quest(string title, string description, int importanceScore = 10, string failureExplanation = "")
    {
        Title = title;
        Description = description;
        ImportanceScore = importanceScore;
        FailureExplanation = failureExplanation;
        IsCompleted = false;
    }
}
