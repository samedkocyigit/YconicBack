using Yconic.Domain.Enums;

namespace Yconic.Domain.Dtos.Ai;
public class AiUserDto{
    public Personas  persona { get; set; }
    public AiGarderobeDto userGarderobe { get; set; }
}

public class AiGarderobeDto
{
    public Dictionary<string, List<ClotheItemDto>> categories { get; set; }
}
public class ClotheItemDto
{
    public string categoryId {get; set;}
    public string clotheId { get; set; }
    public string image_path { get; set; }
}
//public class AiUserDtoForAI
//{
//    public string userPersona { get; set; }
//    public GarderobeDto garderobe { get; set; }
//}
