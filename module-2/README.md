*	Attendees setup event hub with an application pumping fake data (ACI)
*	Alternatively, the host does the setup and help attendees clone the telemetry to their event hub via Event Grid
* Setup real time ingestion

```
//  Create a landing table for trends
.create table trendsLanding(document:dynamic)

//  Create the mapping from JSON ingestion to landing table
.create table trendsLanding ingestion json mapping 'landingMapping' '[{"column":"document","path":"$","datatype":"dynamic"}]'

//  Alter retention policy as this is only for end-user queries
.alter table trendsLanding policy retention "{'SoftDeletePeriod': '0:10:00', 'Recoverability':'Enabled'}"

//  Alter ingestion policy to ingest more often than default (5 minutes)
.alter table trendsLanding policy ingestionbatching "{'MaximumBatchingTimeSpan': '0:0:30'}"
```