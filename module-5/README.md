# Module 5 - Time series

Now that we got comfortable slicing and dicing the data in the last module, we'll start using special type of queries around time series.

Time series is a powerful tool when looking at data moving over time.  It allows to automatically analyse many time series at once and picking the problematic ones.

In this module we are going to look at a problem with the internal temperature sensor of the drone.  The hardware maintenance team is telling us the sensor tend to break a lot.  Engineering is saying that the sensor reading should give us hints of emminent failure in advance.

We suggest the following steps for this module:

1. Plot a sample of 4 drones internal temperature over time
1. Find out how many drones had their internal temperature failed (hint:  a failed device won't emit telemetry while the other device on the same drone will)
1. Plot the internal temperature curve of a few drones where that device failed (hint:  see [make-series operator](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/make-seriesoperator))
1. Explore if we can discriminate between failed and non-failed devices using a linear regression (see [series_fit_line_dynamic](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/series-fit-line-dynamicfunction))
1. Similar exercise with a 2 segments linear regression (see [series_fit_2lines_dynamic](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/series-fit-2lines-dynamicfunction))
1. Similar exercise but this time using anomaly detection (see [series_decompose_anomalies](https://docs.microsoft.com/en-us/azure/data-explorer/kusto/query/series-decompose-anomaliesfunction) and `render anomalychart`)

This module is much more exploratory.  The different steps / challenges are there only to orient the exploration.

## Suggested Solution

The suggested solution is documented in this [Kusto Query Language (KQL) file](time-series-queries.kql).