namespace NetSchool.Services.Actions;

using NetSchool.Services.RabbitMq;
using System.Threading.Tasks;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task PublicateBook(PublicateBookModel model)
    {
        await rabbitMq.PushAsync(QueueNames.PUBLICATE_BOOK, model);
    }
}
