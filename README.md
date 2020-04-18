# Real Time Analytics in Azure Lab

Hands on lab for real time analytics in Azure.

## Business Context

TODO

## Modules

1.	Know the data
o	Attendees get a telemetry sample in CSV format
o	They create a cluster, db, ingest file
o	They query / chart the data to learn characteristics
2.	Setup Event Hub
o	Attendees setup event hub with an application pumping fake data (ACI)
o	Alternatively, the host does the setup and help attendees clone the telemetry to their event hub via Event Grid
* Setup real time ingestion
3.	Querying near real time data
o	Attendees query the data as it get ingested into the cluster
o	Change ingestion frequency with batch policy
* Look at CPU impact
4.	Shape the data
o	Ingest a couple of reference data tables (or reference?)
o	Author update policies to transform the data using reference tables
o	Query some more
5.	Reporting
o	Setup Power BI to query near real time data
6.	Imperfect telemetry
o	Changing upstream process to reveal "real" telemetry with late arrivals + duplicates
* Introduce ASA to the rescue
7.	Export to cold storage
o	Setup continuous exporting
8. Monitoring
