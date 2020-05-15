# Module 3 - Transform the data at ingestion time

In this module we will transform the raw data we ingested in the landing table in the last module.

Our main tool to do this is [Update Policy](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/updatepolicy).  Update policy allows data to be transformed from table to another in real time.

This module builds on [module 2](../module-2)'s ingestion and [module-1](../module-1)'s queries.

We are landing the JSON data in the *landing* table.  We are going to have 2 other tables:  *landingTransformed* and *telemetry*.  The latter is the table holding the finalized data.  The former is an intermediate state.

We suggest the following steps for this module:

1. Create a stored function (see [.create-or-alter function](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/create-alter-function)) that parses the JSON ; this function will be invoked by the update policy
1. Create a table (see [.create table](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/create-table-command)) to receive the data returned by the stored function we created in previous step
1. Setup an update policy on the table created in previous step *landing* as source and the stored function as query (see [.alter table policy update](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/update-policy#alter-update-policy))
1. Test if the transform data reaches the new table

We can repeat that process more than once.  We can transform the data in multiple stages into multiple staging tables.

We recommend using the table name *telemetry* as the final table in order to make it easier to use the suggested solution queries in future modules.

We recommend to change the retention period of temporary tables (e.g. *landing*) to a few minutes (see [retention policy](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/retentionpolicy)).  We do this to save RAM & storage on the Kusto cluster.  Those tables aren't used beyond ingestion.  It is useful while we develop the ingestion to look at them.  Typically, in a producation scenario, the retention is set to zero.

It is important to note that within an update policy query, referencing the *source table* yields the data being ingested only, not the entire table.  This is why there is no special operator to say "give me the current ingested data".

In step 2, as an alternative to creating a table from an explicit schema, we can use a shortcut.  We can use a [.set](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/data-ingestion/ingest-from-query) command using the return of the function defined in step 1 with a [limit 0](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/limitoperator) to ingest no data.  This way, we'll create the table without having to list all the field names / types.

In step 3, it is important to remember the fields returned by the stored function must be in the same order as they are declared in the table.

## Suggested Solution

The suggested solution is documented in this [Kusto Query Language (KQL) file](ingestion.kql).