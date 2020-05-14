# Module 2 - Setup real-time ingestion

In this module we setup ingestion from event hub into Azure Data Explorer.  We will only ingest the raw data ; the transformation will be done in the next module.

We replaced Azure IoT Hub from the preferred solution into Azure Event Hub for simpliciy (it is easier to simulate events).  We also replace the drones by a simulator running on Azure Container Instance ; more details in the next sub section.

We suggest the following steps for this module:

1. Attendees (or instructor) setup the simulator
1. Create a landing table in Kusto (see [.create table](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/create-table-command))
1. Setup real time ingestion (see [Ingest data from Event Hub into Azure Data Explorer](https://docs.microsoft.com/en-us/azure/data-explorer/ingest-data-event-hub))
1. Test the data is getting to the landing table

For step 2, we recommend creating a landing table with only one column of type dynamic.

### Drone events simulation

We recommend the instructor setup one simulator for all attendees.  This simplifies the lab since attendees do not have to worry about that part.  It will also allow all attendees to work with the same data (as the simulation uses random values).

Given how the Portal experience, it is required for the attendee to be able to at least "read" the event hub.  This would be impossible if the instructor and attendees do not share a subscription.  For that reason, we also provide an ARM template setting up the database connection and passing the event hub connection string in parameter.

#### Deploying simulator

In order to deploy the simulator, simply deploy this ARM Template:

<TODO>

The template deploys an Event Hub namespace with one Event Hub (named *telemetry*) and an Azure Container Instance running the [simulator code](../code).

The event hub is deployed with many consumer groups allowing for multiple attendees to connect to it.

The simulator code was compiled and packaged into a Docker Image which is published on [Docker Hub](https://hub.docker.com/repository/docker/vplauzon/perf-streaming).

#### Deploying connection

<TODO>

#### Adjusting Event Hub scale

The Azure Event Hub namespace is deployed with one (1) throughput unit.

This is enough if each attendee deploys its own Event Hub.

In the case the Event Hub is shared, it might be required to scale out the Event Hub.

Note the simulator pushes about 16K events per minute.  A [throughput unit allows for 4096 events per second to be pulled](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-scalability#throughput-units).  Considering Azure Data Explorer does pull at interval, it might be require to bump the throughput unit to accomodate all attendees to have a nice ingestion experience.

Consult the [metrics of the Event Hub Namespace](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-metrics-azure-monitor) to determine if the hub is saturating.

## Suggested Solution

The suggested solution contains little Kusto.

Step 2 consists in creating a landing table:

```sql
//  Create a landing table for telemetry
.create table landing(document:dynamic)
```

While step 4 consists in testing that table is receiving data:

```sql
//  Testing that table (after setting up ingestion)
landing
| limit 10
```
