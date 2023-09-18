using FortuneTellerCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;
using Steeltoe.Connectors.RabbitMQ;
using Steeltoe.Connectors;
using System.Text;

namespace FortuneTellerUI.Pages
{
    public class MessagesModel : PageModel
    {
        public string? ConnectionString;
        public string? MessageReceived;
        private readonly Connector<RabbitMQOptions, IConnection> _connector;

        public MessagesModel(ConnectorFactory<RabbitMQOptions, IConnection> connectorFactory)
        {
            _connector = connectorFactory.Get();
        }

        public void OnGet()
        {
            IConnection connection = _connector.GetConnection();
            ConnectionString = _connector.Options.ConnectionString ?? "not available";
            using IModel channel = connection.CreateModel();

            CreateQueue(channel);

            BasicGetResult? result = channel.BasicGet(RabbitMQHelper.QueueName, true);

            MessageReceived = 
                result == null
                    ? "The spirits have nothing for you at this time."
                    : Encoding.UTF8.GetString(result.Body.ToArray());
        }

        private static void CreateQueue(IModel channel)
        {
            channel.QueueDeclare(RabbitMQHelper.QueueName, false, false, false, null);
        }
    }
}
