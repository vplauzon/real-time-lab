# Real Time Analytics in Azure Lab

Hands on lab for real time analytics in Azure

1.	Know the data
o	Attendees get a sample of the telemetry in CSV format
o	They create a cluster, db, ingest file
o	They query / chat the data to learn characteristics
2.	Setup Event Hub
o	Attendees setup event hub with an application pumping fake data
o	Alternatively, the host does the setup and help attendees clone the telemetry to their event hub via Event Grid
3.	Querying near real time data
o	Attendees query the data as it get ingested into the cluster
o	They can change the frequency of ingestion
4.	Shape the data
o	Ingest a couple of reference data tables
o	Author update policies to transform the data using reference tables
o	Query some more
5.	Reporting
o	Setup Power BI to query near real time data
6.	Real time requirement
o	Introduce a requirement demanding more real time
o	Switch to streaming cluster
7.	Export to cold storage
o	Setup continuous exporting
8.	Fetch reference data
o	Setup Azure function to periodically refresh reference data from an external SQL table
9.	…  that’s probably more than a day already so I guess 5, 6, 7, 8 could be alternatives
