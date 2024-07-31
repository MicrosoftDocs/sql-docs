---
title: Monitoring Azure SQL Database with metrics and alerts
description: An overview of Azure SQL Database monitoring using Azure Monitor metrics and alerts.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 05/06/2024
ms.service: azure-sql-database
ms.subservice: monitoring
ms.topic: conceptual
ms.custom: build-2024
monikerRange: "= azuresql || = azuresql-db"
---

# Monitor Azure SQL Database with metrics and alerts

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

You can use Azure Monitor [metrics](/azure/azure-monitor/essentials/data-platform-metrics) to monitor database and elastic pool resource consumption and health. You can use [alerts](/azure/azure-monitor/alerts/alerts-overview) to send notifications when metric values indicate a potential problem.

## Metrics

A metric is a series of numeric value measured at regular time intervals, often using units such as `count`, `percent`, `bytes`, etc. Depending on the nature of the metric, you can use aggregations such as `total`, `count`, `average`, `minimum`, `maximum` to calculate metric values over a duration of time. You can split some metrics by [dimensions](/azure/azure-monitor/essentials/data-platform-metrics#multi-dimensional-metrics). Each dimension provides an additional context to the numeric values.

Examples of available Azure SQL Database metrics are: `CPU percentage`, `Data space used`, `Deadlocks`, `Tempdb Percent Log Used`.

See [database metrics](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-servers-databases-metrics) and [elastic pool metrics](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-servers-elasticpools-metrics) for all available metrics in Azure SQL Database.

> [!NOTE]
> Some metrics apply only to specific types of databases or elastic pools. The description of each metric mentions if its use it limited to a specific database or elastic pool type, such as vCore, Hyperscale, serverless, etc.

In Azure SQL Database portal, several commonly used metrics are charted on the **Monitoring** tab of the **Overview** page. The metrics let you assess resource consumption and health of a database or an elastic pool at a glance.

:::image type="content" source="media/monitoring-metrics-alerts/portal-overview-metrics.png" alt-text="A screenshot from the Azure portal of a metrics chart shown on the Azure SQL Database Overview page." lightbox="media/monitoring-metrics-alerts/portal-overview-metrics.png":::

Under **Key metrics**, select **See all metrics** or anywhere inside the chart to open [metrics explorer](/azure/azure-monitor/essentials/analyze-metrics). In the **Metrics** page, you can view all other available metrics for the database or elastic pool resource. In metrics explorer, you can change the time range, granularity, and aggregation type for the chart, change the type of chart, expand the scope to include metrics from other Azure resources, create alert rules, etc. You can also open metrics explorer by selecting **Metrics** menu item, under **Monitoring** in the resource menu.

### Use metrics to monitor databases and elastic pools

You can use metrics to monitor database and elastic pool resource consumption and health. For example, you can:

- Right-size the database or elastic pool to your application workload
- Detect a gradual increase in resource consumption, and proactively scale up the database or elastic pool
- Detect and troubleshoot a performance problem

The following table describes commonly used metrics in Azure SQL Database.

| Metric name | Metric ID | Description |
|:--|:--|:--|
| **CPU percentage** | `cpu_percent` | This metric shows CPU consumption toward the user workload limit of a database or an elastic pool, expressed as a percentage. For more information, see [Resource consumption by user workloads and internal processes](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes). |
| **SQL instance CPU percent** | `sql_instance_cpu_percent` | This metric shows the total CPU consumption by the user and system workloads, expressed as a percentage. Because this metric and the **CPU percentage** metric are measured on different scales, they are not directly comparable with each other. For more information, see [Resource consumption by user workloads and internal processes](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes).|
| **Data IO percentage** | `physical_data_read_percent` | This metric shows the data file IO consumption toward the user workload limit of a database or an elastic pool, expressed as a percentage. For more information, see [Data IO governance](resource-limits-logical-server.md#data-io-governance). |
| **Log IO percentage** | `log_write_percent` | This metric shows the transaction log write throughput consumption toward the user workload limit of a database or an elastic pool, expressed as a percentage. For more information, see [Transaction log rate governance](resource-limits-logical-server.md#transaction-log-rate-governance). |
| **Workers percentage** | `workers_percent` | This metric shows the consumption of [worker](resource-limits-logical-server.md#sessions-workers-and-requests) threads toward the user workload limit of a database or an elastic pool, expressed as a percentage. |
| **DTU percentage** | `dtu_consumption_percent` | This metric shows [DTU](service-tiers-dtu.md) consumption toward the user workload limit of a database or an elastic pool, expressed as a percentage. **DTU percentage** is derived from three other metrics: **CPU percentage**, **Data IO percentage**, and **Log IO percentage**. At any point in time, **DTU percentage** matches the highest value among these three metrics. |
| **CPU used** | `cpu_used` | This metric shows CPU consumption toward the user workload limit of a database or an elastic pool, expressed as the number of vCores. For more information, see [Diagnose and troubleshoot high CPU on Azure SQL Database](high-cpu-diagnose-troubleshoot.md).|
| **DTU used** | `dtu_used` | This metric shows the number of [DTUs](service-tiers-dtu.md#database-transaction-units-dtus) used by a database or an elastic pool. |
| **App CPU billed** | `app_cpu_billed` | For serverless databases, this metric shows the amount of compute (CPU and memory) billed, expressed in vCore seconds. For more information, see [Billing in the serverless compute tier](serverless-tier-overview.md#billing). |
| **App CPU percentage** | `app_cpu_percent` | For serverless databases, this metric shows CPU consumption toward the app package maximum vCore limit, expressed as a percentage. For more information, see [Monitoring in the serverless compute tier](serverless-tier-overview.md#monitoring). |
| **App memory percentage** | `app_memory_percent` | For serverless databases, this metric shows memory consumption toward the app package maximum memory limit, expressed as a percentage. For more information, see [Monitoring in the serverless compute tier](serverless-tier-overview.md#monitoring). |
| **Sessions count** | `sessions_count` | This metric shows the number of established user sessions for a database or an elastic pool. |
| **Data space used** | `storage` | For databases, this metric shows the amount of storage space used in the data files of a database. |
| **Data space used** | `storage_used` | For elastic pools, this metric shows the amount of storage space used in the data files of all databases in an elastic pool. |
| **Data space allocated** | `allocated_data_storage` | This metric shows the amount of storage space occupied by the data files of a database, or by the data files of all databases in an elastic pool. Data files might contain empty space. Because of this, **Data space allocated** if often higher than **Data space used** for the same database or elastic pool. For more information, see [Manage file space for databases in Azure SQL Database](file-space-manage.md). |
| **Data space used percent** | `storage_percent` | For databases, this metric shows the amount of storage space used in the data files of a database toward the data size limit of a database. For elastic pools, it shows the amount of storage space used in the data files of all databases in an elastic pool, expressed as a percentage toward the data size limit of an elastic pool. The data size limit for a database or an elastic pool might be configured lower than the *maximum* data size limit. To find the *maximum* data size limit, see resource limits for [vCore databases](resource-limits-vcore-single-databases.md), [vCore elastic pools](resource-limits-vcore-elastic-pools.md), [DTU databases](resource-limits-dtu-single-databases.md), and [DTU elastic pools](resource-limits-vcore-elastic-pools.md). |
| **Data space allocated percent** | `allocated_data_storage_percent` | For elastic pools, this metric shows the amount of storage space occupied by the data files of all databases in an elastic pool toward the data size limit of the pool, expressed as a percentage. |
| **Tempdb Percent Log Used** | `tempdb_log_used_percent` | This metric shows the consumption of the transaction log space in the `tempdb` database toward the maximum log size, expressed as a percentage. For more information, see [tempdb in Azure SQL Database](/sql/relational-databases/databases/tempdb-database#tempdb-in-sql-database).|
| **Successful Connections** | `connection_successful` | This metric shows the number of successfully established connections to a database. This metric can be split by two dimensions, `SslProtocol` and `ValidatedDriverNameAndVersion`, to see the number of connections using a specific encryption protocol version, or using a specific client driver. |
| **Failed Connections : System Errors** | `connection_failed` | This metric shows the number of connection attempts to a database that failed because of internal service errors. Most commonly, such errors are transient. This metric can be split by two dimensions, `Error` and `ValidatedDriverNameAndVersion`, to see the number of failed connection attempts due to a specific error, or from a specific client driver. |
| **Failed Connections : User Errors** | `connection_failed_user_error` | This metric shows the number of connection attempts to a database that failed because of user-correctable errors, such as an incorrect password or connection being blocked by firewall. This metric can be split by two dimensions, `Error` and `ValidatedDriverNameAndVersion`, to see the number of failed connection attempts due to a specific error, or from a specific client driver. |
| **Deadlocks** | `deadlock` | This metric shows the number of [deadlocks](analyze-prevent-deadlocks.md) in a database.|
| **Availability** | `availability` | Availability is determined based on the database being operational for connections. For each one-minute data point, the possible values are either `100%` or `0%`. For more information, see [Availability metric](#availability-metric). |

#### Availability metric

The Availability metric tracks availability at individual Azure SQL Database level. This feature is currently in preview.

Availability is granular to one minute of connection outage. Availability is determined based on the database being operational for connections. A minute is considered as downtime or unavailable if all continuous attempts by users to establish connection to the database within the minute fail due to a service issue. If there is intermittent unavailability, the duration of continuous unavailability must cross the minute boundary to be considered as downtime. Typically, the latency to display availability is less than three minutes.

Here's the logic used for calculating Availability for every one-minute interval:

- If there is at least one successful connection, then availability is 100%.
- If all connections fail due to user errors, availability is 100%.
- If there are no connection attempts, availability is 100%.
- If all connections fail due to system errors, availability is 0%.
- Currently, availability metric data is not yet supported for the serverless compute tier and will be displayed as 100%.

Availability metric is therefore a composite metric derived from following existing metrics:

- **Successful Connections**
- **Failed Connections : User Errors**
- **Blocked by Firewall**
- **Failed Connections : System Errors**

User errors include all connections that fail due to user configuration, workload, or management. System errors include all the connections that fail due to transient issues related to Azure SQL Database service.

- Examples of errors caused by user configuration:
    - [Firewall related](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#cannot-connect-to-server-due-to-firewall-issues)
    - [Incorrect user credentials](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#unable-to-log-in-to-the-server-errors-18456-40531)
    - [Application unable to connect](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#the-serverinstance-was-not-found-or-was-not-accessible-errors-26-40-10053)
    - [Connection timeout caused by application](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#connection-timeout-expired-errors)

- Examples of errors caused by user workload:
    - [Connection failure due to resource governance](troubleshoot-common-errors-issues.md?view=azuresql-db&preserve-view=true#resource-governance-errors)

- Examples of errors caused by user management:
    - Scaling up or down the database or elastic pool
    - Geo replication planned or unplanned failover
    - Failover group planned or unplanned failover
    - Geo secondary database in seeding state
    - Database that is in restoring state due to Point In Time Restore (PITR), Long Term Restore (LTR), or restore from a deleted database
    - Database that is not yet finished being copied (Database Copy)

## Alerts

You can create alert rules to notify you that the value of one metric or multiple metrics is outside of an expected range. 

You can set the scope of an alert rule in multiple ways to suit your needs. For example, alert rule scope can be set to:

- A single database
- An elastic pool
- All databases or elastic pools in a resource group
- All databases or elastic pools in a subscription within an Azure region
- All databases or elastic pools in a subscription within all regions

Alert rules periodically evaluate aggregated metric values over a lookback period, comparing them to a threshold value. You can configure the threshold value, evaluation frequency, and lookback period.

If an [alert rule](/azure/azure-monitor/alerts/alerts-create-new-alert-rule?tabs=metric) is triggered, you are notified according to your notification preferences, which you specify in the [action group](/azure/azure-monitor/alerts/action-groups) linked to the alert rule. For example, you can receive an email, an SMS, or a voice notification. An alert rule can also trigger actions such as webhooks, [automation](/azure/automation/overview) runbooks, [functions](/azure/azure-functions/functions-overview), [logic apps](/azure/logic-apps/logic-apps-overview), etc. You can [integrate](/azure/azure-monitor/alerts/itsmc-overview) alerts with supported IT service management products.

To learn more about Azure Monitor alerts, see [Azure Monitor alerts overview](/azure/azure-monitor/alerts/alerts-overview). To get familiar with metric alerts, review [Metric alerts](/azure/azure-monitor/alerts/alerts-types#metric-alerts), [Manage alert rules](/azure/azure-monitor/alerts/alerts-manage-alert-rules), and [Action groups](/azure/azure-monitor/alerts/action-groups).

## Recommended alert rules

The metrics and optimal thresholds to use in alert rules vary across the wide spectrum of customer workloads in Azure SQL Database.

The recommended alerts in the following table are a starting point to help you define the optimal alerting configuration for your Azure SQL Database resources. Depending on your requirements, your configuration might differ from this example. You might use different thresholds, evaluation frequencies, or lookback periods. You might choose to create additional alerts, or use different alert rule configurations for different applications and environments.

Here are examples of typical alert rule configurations.

| Alert rule name | Metric (signal) | Alert logic | When to evaluate | Suggested severity |
|:--|:--|:--|:--|:--|
| High user CPU usage | **CPU percentage** | Threshold: `Static`</br>Aggregation: `Average`</br>Operator: `Greater than`</br>Threshold value: `90` | Check every: `1 minute`</br>Lookback period: `10 minutes` | 2 - Warning |
| High total CPU usage | **SQL instance CPU percent** | Threshold: `Static`</br>Aggregation: `Average`</br>Operator: `Greater than`</br>Threshold value: `90` | Check every: `1 minute`</br>Lookback period: `10 minutes` | 2 - Warning |
| High worker usage | **Workers percentage** | Threshold: `Static`</br>Aggregation: `Minimum`</br>Operator: `Greater than`</br>Threshold value: `60` | Check every: `1 minute`</br>Lookback period: `5 minutes` | 1 - Error |
| High data IO usage | **Data IO percentage** | Threshold: `Static`</br>Aggregation: `Average`</br>Operator: `Greater than`</br>Threshold value: `90` | Check every: `1 minute`</br>Lookback period: `15 minutes` | 3 - Informational |
| Low data space | **Data space used percent** | Threshold: `Static`</br>Aggregation: `Minimum`</br>Operator: `Greater than`</br>Threshold value: `95` | Check every: `15 minute`</br>Lookback period: `15 minutes` | 1 - Error |
| Low `tempdb` log space | **Tempdb Percent Log Used** | Threshold: `Static`</br>Aggregation: `Minimum`</br>Operator: `Greater than`</br>Threshold value: `60` | Check every: `1 minute`</br>Lookback period: `5 minutes` | 1 - Error |
| Deadlocks | **Deadlocks** | Threshold: `Dynamic`</br>Aggregation: `Total`</br>Operator: `Greater than`</br>Threshold sensitivity: `Medium` | Check every: `15 minutes`</br>Lookback period: `1 hour` | 3 - Informational |
| Failed connections (user errors) | **Failed Connections : User Errors** | Threshold: `Dynamic`</br>Aggregation: `Total`</br>Operator: `Greater than`</br>Threshold sensitivity: `Medium` | Check every: `5 minutes`</br>Lookback period: `15 minutes` | 2 - Warning |
| Failed connections (system errors) | **Failed Connections : System Errors** | Threshold: `Static`</br>Aggregation: `Total`</br>Operator: `Greater than`</br>Unit: `Count`</br>Threshold value: `10` | Check every: `1 minute`</br>Lookback period: `5 minutes` | 2 - Warning |
| Anomalous connection rate | **Successful Connections** | Threshold: `Dynamic`</br>Aggregation: `Total`</br>Operator: `Greater or Less than`</br>Threshold sensitivity: `Low` | Check every: `5 minutes`</br>Lookback period: `15 minutes` | 2 - Warning |

Some of the recommended alert rules use dynamic thresholds to detect anomalous metric patterns that might require attention. Alert rules based on dynamic thresholds do not trigger until sufficient historical data has been collected to establish normal patterns. For more information, see [Dynamic thresholds in metric alerts](/azure/azure-monitor/alerts/alerts-dynamic-thresholds).

By default, metric alerts are stateful. This means that once an alert rule is triggered, the alert is fired only once. The alert remains in the `fired` state until it is resolved, at which point a `resolved` notification is sent. An alert rule triggers a new alert only once the previous alert is resolved. Stateful alerts avoid frequent notifications about an ongoing condition. For more information about stateful and stateless alerts, see [Alerts and state](/azure/azure-monitor/alerts/alerts-overview#alerts-and-state).

## Related content

- [Azure Monitor metrics overview](/azure/azure-monitor/essentials/data-platform-metrics)
- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Analyze metrics with Azure Monitor metrics explorer](/azure/azure-monitor/essentials/analyze-metrics)
- [Azure Monitor metrics aggregation and display explained](/azure/azure-monitor/essentials/metrics-aggregation-explained)
- [Azure Monitor alerts overview](/azure/azure-monitor/alerts/alerts-overview)
- [Tutorial: Create a metric alert for an Azure resource](/azure/azure-monitor/alerts/tutorial-metric-alert)
- [Best practices for Azure Monitor alerts](/azure/azure-monitor/best-practices-alerts)
- [Troubleshooting problems in Azure Monitor alerts](/azure/azure-monitor/alerts/alerts-troubleshoot)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](monitor-tune-overview.md)
- [Configure streaming export of Azure SQL Database and SQL Managed Instance diagnostic telemetry](metrics-diagnostic-telemetry-logging-streaming-export-configure.md)
