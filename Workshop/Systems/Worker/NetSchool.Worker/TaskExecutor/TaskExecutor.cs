namespace NetSchool.Worker;

using NetSchool.Services.RabbitMq;
using System.Threading.Tasks;
using NetSchool.Services.Actions;
using NetSchool.Services.Logger;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
    }

    public void Start()
    {
        rabbitMq.Subscribe<PublicateBookModel>(QueueNames.PUBLICATE_BOOK, async data =>
        {
            logger.Information($"Starting publication of the book::: {data.Id}");

            await Task.Delay(3000);

            logger.Information($"The book was publicated::: {data.Id} | {data.Title} | {data.Description}");
        });
    }
}