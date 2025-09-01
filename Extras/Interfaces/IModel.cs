namespace LeCiel.Extras.Interfaces;

public interface IModel
{
    uint Id { set; get; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
