# Real Time Analytics in Azure Lab

Hands on lab for real time analytics in Azure.

## Preparation

Kusto hands on practice

## Business Context

Contonso Corporation operates a fleet of agricultural drones used for surveilling crops and monitor the environment.

They recently scaled up the size of the fleet and have had operational challenges since.  For instance, they lose drones in the fields while some are malfunctioning.  In general, they have a hard time analysing the telemetry they are capturing using their current Hadoop-based stack.  The data is piling up and they can't get value out of the data in a timely fashion.  Their Data Science unit have developped some models but they can't use it in real time.

They want to explore new ways of analysing the data and see what benefit those could bring.  They want to reuse their IoT solution based on Azure IoT Hub but change the data analysis stack to a Azure-based solution.  They have the following requirements:

*   Focus on the non-video device telemetry (e.g. temperature, pressure, GPS position, etc.)
*   Managed solution:  they do not want to manage VMs
*   Security:  part of their data is sensitive and so we need to control access and avoid data-exfiltration
*   Monitoring:  they need to monitor the platform (e.g. CPU usage)
*   Real Time:  they want the data to be available for analysis in less than one minute latency
*   Ad Hoc queries:  they want the platform to be able to perform exploratory query without shaping the data (e.g. designing indexes) in a special way for the queries
*   Visualization:  they do not want the platform to only deal with data to be visualize with other tools ; they would like for the platform to allow visualization as close to the querying tool as possible
*   Ability to deal with duplicates, late duplicates and late arrival data:  the telemetry delivery to IoT Hub isn't always perfect and the platform must be able to deal with those elements
*   Time series:  a lot of analysis they want to perform is based on time series of different measurements ; the platform must be able to do those as natively as possible
*   Export to data lake:  their data science will still need the data for long-term analysis in the data lake (Azure Data Lake Storage gen 2)
*   Reporting:  different stakeholders in the company must be able to look at dashboards for a summary look at the fleet
*   Democratization of data analysis:  Contoso is positioning themselves more and more as a *Data Enterprise* ; they want more and more of their employee to be able to look and explore operational data

## Design Session

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

*	Provision a Kusto cluster
*   Query telemetry samples
*   Prepare ingestion by developing queries

Go to [module 1 instructions](module-1).

2.	Setup Event Hub
*	Attendees setup event hub with an application pumping fake data (ACI)
*	Alternatively, the host does the setup and help attendees clone the telemetry to their event hub via Event Grid
* Setup real time ingestion
3. Update policies
*   Land data in different tables reusing the queries done in module 1
4.	Querying near real time data
*	Attendees query the data as it get ingested into the cluster
*   Different query challenges
    *   Detect duplicates
    *   Detect downtime
    *   Plot coordinates
    *   Compute speed curves
*   Look at latency of data
*	Change ingestion frequency with batch policy
* Look at CPU impact
5.	Shape the data
*	Ingest a couple of reference data tables (or reference?)
*	Author update policies to transform the data using reference tables
*	Query some more
6.	Reporting
*	Setup Power BI to query near real time data
7.	Imperfect telemetry
*	Changing upstream process to reveal "real" telemetry with late arrivals + duplicates
* Introduce ASA to the rescue
8.	Export to cold storage
*	Setup continuous exporting
9. Monitoring
10. Time series