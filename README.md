# Fortune Teller

This is a demo for Steeltoe OSS. If you are looking for slides that came along with a presentation of this demo, please look [here](https://onevmw-my.sharepoint.com/:p:/g/personal/thess_vmware_com/EScBflkveAtLhtO-bQCCw5wB8mOpxw9YIiAhQzVZTX1ghA?e=BBdawr).

This variant of the fortune teller application includes two .NET applications that can communicate with each other over HTTP and RabbitMQ. FortuneTellerService is a WebAPI project that stores fortunes in a PostgreSQL database. When fortunes are requested, sometimes they are accompanied by messages from the great beyond. When those messages are encountered, the service will send them to a queue in RabbitMQ. The FortuneTellerUI is a Razor Pages project that is used to request fortunes over HTTP from the fortune service (using Eureka or Spring Cloud Registry to locate the service) and read messages from the queue. Both applications get configuration data from a Spring Cloud Config Server.

## Notable Features

### Externalizing and reloading configuration

Both applications are configured to read from a Spring Cloud Config Server. The UI application is configured to bind config data to FortuneServiceOptions.cs using [IOptions](https://learn.microsoft.com/dotnet/api/microsoft.extensions.options.ioptions-1). As such, the values will be updated whenever the underlying values in one of the included ConfigurationProviders change. Using this pattern in combination with the polling feature in Steeltoe's ConfigServer configuration provider and/or the `/refresh` actuator will allow you to update an application's configuration values without having to restart your application.

### Working with service bindings

The connections to RabbitMQ, PostgreSQL and Eureka (Spring Cloud Service Registry) are all managed by Steeltoe Connectors. The connection parameters (default values and those configured in these examples) will be automatically updated/overridden when the application is bound to service instances in TAS/Cloud Foundry

### Actuators

Steeltoe Actuators are included in both projects and will automatically work with Apps Manager when deployed on TAS.

### Client-side load balancing and service discovery

FortuneTellerService is configured to register with Eureka. FortuneTellerUI is configured to read Eureka's service registry and through the integration with HttpClientFactory, automatically updates the URI on each request to the fortune service with the address of running service instance using a round robin algorithm to evenly distribute traffic to registered service instances.

## Running Backing Services Locally

The fortune teller requires a config server, registry server, database server and queue server. The easiest way to get these locally is using docker, as demonstrated below.

### Spring Cloud Config Server

Run or use the contents of `./config-repo/start-config-server.ps1`

### Eureka Server

```bash
docker run --rm -ti -p 8761:8761 --name steeltoe-eureka steeltoeoss/eureka-server
```

### PostgreSQL

```bash
docker run --rm -ti -p 5432:5432 --name steeltoe-postgres -e POSTGRES_DB=steeltoe -e POSTGRES_USER=steeltoe -e POSTGRES_PASSWORD=steeltoe postgres:alpine
```

### RabbitMQ

```bash
docker run --rm -ti -p 5672:5672 -p 15672:15672 --name rabbitmq rabbitmq:3-management
```
