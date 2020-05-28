# Module 1 - Know the data

In this module we look at a [sample of the telemetry](sample.json) provided by the drones' team.

The sample is an array of gateway *messages*.  Each gateway message contains multiple *drone device's events*.

Drones have the following devices onboard:

* External Temperature (in [Celcius](https://en.wikipedia.org/wiki/Celsius))
* Internal Temperature (in [Celcius](https://en.wikipedia.org/wiki/Celsius))
* GPS (as [GeoJSON](https://geojson.org/) point, cf [RFC 7946](https://tools.ietf.org/html/rfc7946))

This is a **query only** module.  There is no need to create a table.  Look at the notes below for hints at how to handle the JSON payload.

We suggest the following steps for this module:

1. Provision an Azure Data Explorer (ADX, Kusto) cluster
1. [Create a database](https://docs.microsoft.com/en-us/azure/data-explorer/create-cluster-database-portal#create-a-database) named "telemetry"
1. In Web UI, integrate the sample in a query (hint:  paste it and use [dynamic literals](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/scalar-data-types/dynamic#dynamic-literals))
1. Explode each gateway message on its row (hint:  look at [mv-expand operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/mvexpandoperator))
1. Extract each gateway message field as a strong-type column (hint:  use casting, e.g. [tostring](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/tostringfunction))
1. Explode each drone event on its own row (i.e. each row will have the gateway message fields an drone event data)
1. Extract each drone event field in its own strong-type column
1. Parse the drone id compound field into 2 fields:  software version and drone id (hint:  look at [parse operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/parseoperator))

For step 1, we suggest using the [portal deployment](https://docs.microsoft.com/en-us/azure/data-explorer/create-cluster-database-portal) for expediency.  Also, for expediency, we recommend going "vanilla":
* Leave *Streaming Ingestion* to off
* Leave *Enable purge* to off
* Leave *System Assigned Identity* to off
* Leave *Deploy in a Virtual Network* to off

The *Dev/Test* [cluster type](https://docs.microsoft.com/en-us/azure/data-explorer/manage-cluster-choose-sku#select-a-cluster-type) is the cheapest option to run this lab.

For step 3 onward, we recommend storing the sample in a [stored function](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/create-alter-function) to facilitate reuse (instead of copying the sample over and over).

For step 8, let's look at a sample drone ID:  `1.2.19;df12e8f6`.  This is actually two fields compounded into one:  the drone embeded software version (i.e. `1.2.19`) and the drone id itself (i.e. `df12e8f6`).

## Suggested Solution

The suggested solution is documented in this [Kusto Query Language (KQL) file](sample-queries.kql).