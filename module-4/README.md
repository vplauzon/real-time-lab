# Module 4 - Querying real time data

We should now have drone telemetry data coming to our Kusto cluster in near real time.  It is time to get some insights into that data!

We suggest the following steps for this module:

1. Look at the number of records in the *telemetry* table (see [count operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/countoperator)) ; this should be increasing every 30 seconds or so
1. Look at different latencies:  latency between of the gateway message versus the embedeed drone events, latency between the time a gateway message is created and the time it is ingested (see [summarize](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/summarizeoperator) and [ingestion_time](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/ingestiontimefunction?pivots=azuredataexplorer))
1. Plot (see [render](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/renderoperator?pivots=azuredataexplorer)) the number of records ingested per minute (see [bin operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/binfunction))
1. Compute the average period between two gateway messages (see [prev function](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/prevfunction))
1. Compute the average period between two events from the same device (i.e. summarize by devices)
1. Plot sample GPS coordinates on a map (see [sample operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/sampleoperator) and `render scatterchart` of kind `map`)
1. Plot trajectory of a sample GPS coordinates on a map (take only a few hundreds sample points)
1. Plot the speed (see [geo_distance_2points function](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/geo-distance-2points-function)) of a sample drone over time
1.  Plot the external temperature of a sample drone
1.  Plot both the internal and external temperature of a sample drone
1.  Look at the cluster metrics if you can see the impact of queries on CPU (in the portal)
1.  Look at logs in Kusto (see [.show command-and-queries](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/commands-and-queries))

## Suggested Solution

The suggested solution is documented in this [Kusto Query Language (KQL) file](queries.kql).