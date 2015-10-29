using TDD.Demo.Domain;

namespace TDD.Demo.Presentation
{
    public interface IViewModel<TModel> where TModel : EntityBase
    {
        string WarningMessage { get; set; }

        TModel Model { get; set; }
    }
}
