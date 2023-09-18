using FortuneTeller;
using FortuneTellerService.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Steeltoe.Connectors.RabbitMQ;
using Steeltoe.Connectors;
using FortuneTellerCommon;
using System.Text;

namespace FortuneTellerService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FortunesController(ILogger<FortunesController> logger, IFortuneRepository fortunes, ConnectorFactory<RabbitMQOptions, IConnection> connectorFactory) : ControllerBase
{
    private readonly Connector<RabbitMQOptions, IConnection> _connector = connectorFactory.Get();

    // GET: api/fortunes/all
    [HttpGet("all")]
    public async Task<IEnumerable<Fortune>> AllFortunesAsync()
    {
        logger?.LogTrace("AllFortunesAsync");
        var entities = await fortunes.GetAllAsync();
        return entities.Select(fortune => new Fortune(fortune.Id, fortune.Text));
    }

    // GET api/fortunes/random
    [HttpGet("random")]
    public async Task<Fortune> RandomFortuneAsync()
    {
        logger?.LogTrace("RandomFortuneAsync");
        var fortuneEntity = await fortunes.RandomFortuneAsync();
        SendMessage(fortuneEntity.MessageFromBeyond);
        return new Fortune(fortuneEntity.Id, fortuneEntity.Text);
    }

    private void SendMessage(string? messageToSend)
    {
        if (messageToSend == null)
        {
            return;
        }

        IConnection connection = _connector.GetConnection();
        using IModel channel = connection.CreateModel();

        CreateQueue(channel);

        byte[] body = Encoding.UTF8.GetBytes(messageToSend);
        channel.BasicPublish("", RabbitMQHelper.QueueName, null, body);
    }

    private static void CreateQueue(IModel channel)
    {
        channel.QueueDeclare(RabbitMQHelper.QueueName, false, false, false, null);
    }
}
