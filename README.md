# Real Time Analytics in Azure Lab

Hands on lab for real time analytics in Azure.

## Business Context

TODO

## Modules

1.	Know the data
o	Telemetry sample in a few JSON files
o	Create a cluster, db, ingest files
o	They query / chart the data to learn characteristics
*   Split device telemetry from the rest
*   Strong type
2.	Setup Event Hub
o	Attendees setup event hub with an application pumping fake data (ACI)
o	Alternatively, the host does the setup and help attendees clone the telemetry to their event hub via Event Grid
* Setup real time ingestion
3. Update policies
*   Land data in different tables reusing the queries done in module 1
4.	Querying near real time data
o	Attendees query the data as it get ingested into the cluster
*   Different query challenge
*   Look at latency of data
*	Change ingestion frequency with batch policy
* Look at CPU impact
5.	Shape the data
o	Ingest a couple of reference data tables (or reference?)
o	Author update policies to transform the data using reference tables
o	Query some more
6.	Reporting
o	Setup Power BI to query near real time data
7.	Imperfect telemetry
o	Changing upstream process to reveal "real" telemetry with late arrivals + duplicates
* Introduce ASA to the rescue
8.	Export to cold storage
o	Setup continuous exporting
9. Monitoring
10. Time series