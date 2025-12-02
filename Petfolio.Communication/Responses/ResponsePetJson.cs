using Petfolio.Communication.Enums;

namespace Petfolio.Communication.Responses;

public class ResponsePetJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public PetType PetType { get; set; }
}