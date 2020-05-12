# Real Time Analytics in Azure Lab

Hands on lab for real time analytics in Azure.

This is a one day hands-on lab on Real Time Analytics in Azure.

Azure services used in this lab:

*   [Azure Data Explorer](https://docs.microsoft.com/en-us/azure/data-explorer/data-explorer-overview)
*   [Azure Event Hub](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-about)
*   [Azure Stream Analytics](https://docs.microsoft.com/en-us/azure/stream-analytics/stream-analytics-introduction)
*   [Power BI](https://docs.microsoft.com/en-us/power-bi/)
*   [Azure SQL Database](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-technical-overview)
*   [Azure Monitor](https://docs.microsoft.com/en-us/azure/azure-monitor/overview)

## Preparation

Getting familiar with Kusto, Azure Data Explorer query language.  We suggest the following resources:
* [Tutorial](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/tutorial)
* [Kusto cheat sheet](https://aka.ms/sql-analytics)

## Design Session

Follow [this link](business-context.md) for the business scenario for the hands on lab.

Break into teams of 3 to 8 to design the solution.

The solution should address each of the business requirements from the previous section.  Draw a diagram with the main components.

Each team will then present their solution.

##  Prefered solution

The instructor will present the *prefered solution*.

The hands on lab modules are based on that solution.

## Hands on Lab Modules

The different modules contain instructions about the challenges of the module.  They also contain a suggested solution.

We recommend you try to do the module on your own as much as possible in order to get challenged and get more benefit out of the lab.

### Module 1 - Know the data

This is the first module in which we'll explore the data we are going to work with.

*	Provision a Azure Data Explorer (Kusto) cluster
*   Query telemetry samples
*   Prepare ingestion by developing queries

Go to [module 1 instructions](module-1).

### Module 2 - Setup real-time ingestion

In this module we will setup the real time ingestion of data into our Kusto Cluster.

*	Setup a generator of data for Azure Event Hub using Azure Container Instance (ACI)
*   Setup ingestion in Azure Data Explorer

Go to [module 2 instructions](module-2).

### Module 3 - Transform the data at ingestion time

In this module we will take the raw JSON data we ingested in Module 2 and transform it into strongly-typed data, at ingestion time, using the queries we developped in module 1.

*   Create Stored Function
*   Setup update policies

Go to [module 3 instructions](module-3).

### Module 4 - Querying real time data

Now that we have data ingested in real time, we are going to query it to get insights.

*   Compute ingestion latency
*   Query and chart data
*   Look at Azure Data Explorer metrics (monitoring)

Go to [module 4 instructions](module-4).

### Module 5 - Time series

Use Kusto's time series' functionalities to detect failure on one of the sensor.

### Module 6 - Imperfect telemetry

*	Changing upstream process to reveal "real" telemetry with late arrivals + duplicates
* Introduce ASA to the rescue

### Module 7 - Integrate external data

In this module we are going to integrate external data from Azure SQL DB to enrich the ingested data.

*	Ingest a couple of reference data tables (or reference?)
*	Author update policies to transform the data using reference tables
*	Query some more
*	Setup continuous exporting

Go to [module 5 instructions](module-5).

### Module 8 - Reporting

*	Setup Power BI to query near real time data

### Module 9 - Monitoring

*	...

