namespace NetSchool.Services.Actions;

using System.Threading.Tasks;

public interface IAction
{
    Task PublicateBook(PublicateBookModel model);
}
