namespace Yconic.Domain.Dtos.Ai;

public class AiSuggestionResponse
{
    public string userPersona { get; set; }
    public string prompt { get; set; }
    public List<AiSuggestionItem> suggested_combination { get; set; }
}

public class AiSuggestionItem
{
    public string clotheId {get; set;}
    public string image_path { get; set; }
    public float primary_score { get; set; }
    public float minimalist_score { get; set; }
    public float luxury_score { get; set; }
    public float casual_score { get; set; }
}