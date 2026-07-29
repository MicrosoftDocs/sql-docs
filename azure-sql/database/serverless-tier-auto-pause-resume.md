---
title: Serverless auto-pause and auto-resume
description: Learn how auto-pause and auto-resume work in the serverless compute tier for Azure SQL Database, including triggers, troubleshooting, and connectivity.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: kendalv, moslake, mathoma, dfurman
ms.date: 07/28/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: concept-article
monikerRange: "=azuresql||=azuresql-db"
ai-usage: ai-assisted
---
# Auto-pause and auto-resume in the serverless compute tier for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains the auto-pause and auto-resume behavior for the [serverless compute tier](serverless-tier-overview.md) in Azure SQL Database, and how it interacts with various features of Azure SQL Database.

Currently, the General Purpose service tier is the only service tier that supports serverless auto-pausing and auto-resuming.

To monitor a serverless database state, see [Monitor pause and resume status](serverless-tier-monitor.md#monitor-pause-and-resume-status).

<a id="auto-pausing"></a>

## Auto-pause

Auto-pause starts if all the following conditions are true during the auto-pause delay:

- Number of sessions = 0
- CPU = 0 for user workload running in the user resource pool

 By default, there's a [one-hour auto-pause delay](serverless-tier-overview.md?view=azuresql-db&preserve-view=true#performance-configuration).

### Features that prevent auto-pause

If you use any of the following features, disable auto-pausing. The database stays online regardless of how long the database is inactive. The following features prevent auto-pausing, but they do support auto-scaling:

- Geo-replication ([active geo-replication](active-geo-replication-overview.md) and [failover groups](failover-group-sql-db.md))
- [Long-term backup retention](long-term-retention-overview.md) (LTR)
- A [DNS alias](dns-alias-overview.md) created for the logical server containing a serverless database

The following feature scenarios also prevent auto-pausing:

- The sync database used in [SQL Data Sync](sql-data-sync-data-sql-server-sql-database.md). Unlike sync databases, hub and member databases support auto-pausing.
- In [elastic jobs](elastic-jobs-overview.md), a serverless database with auto-pause enabled isn't supported as a *job database*. Serverless databases *targeted* by elastic jobs do support auto-pausing. Job connections resume a database.
- Auto-pausing is temporarily prevented during the deployment of some service updates, which require the database be online. In such cases, auto-pausing becomes allowed again once the service update completes.

<a id="auto-resuming"></a>

## Auto-resume

Auto-resume starts if any of the following conditions are true at any time:

|Feature|Auto-resume trigger|
|---|---|
|Authentication and authorization|Login attempt|
|Threat detection|Enabling or disabling threat detection settings at the database or server level.<br>Modifying threat detection settings at the database or server level.|
|Data discovery and classification|Adding, modifying, deleting, or viewing sensitivity labels|
|Auditing|Viewing auditing records.<br>Updating or viewing auditing policy.|
|Data masking|Adding, modifying, deleting, or viewing data masking rules|
|Transparent data encryption|Viewing state or status of transparent data encryption|
|Vulnerability assessment|Manually initiated scans and periodic scans if enabled|
|Query (performance) data store|Modifying or viewing Query Store settings|
|Performance recommendations|Viewing or applying performance recommendations|
|Auto-tuning|Application and verification of auto-tuning recommendations such as autoindexing|
|Database copying|Create database as copy.<br>Export to a BACPAC file.|
|SQL data sync|Synchronization between hub and member databases that run on a configurable schedule or are performed manually|
|Modifying certain database metadata|Adding or modifying Azure tags on the database.<br>Changing maximum vCores, minimum vCores, or auto-pause delay.|
|SQL Server Management Studio (SSMS)|In SSMS versions earlier than 18.1 and opening a new query window for any database in the server, any auto-paused database in the same server is resumed. This behavior doesn't occur if you use SSMS version 18.1 or later.|

Monitoring, management, or other solutions that perform any of these operations trigger auto-resuming. Auto-resuming also starts during the deployment of some service updates that require the database be online.

### Auto-resume trigger identification

The [Azure Monitor activity log](/azure/azure-monitor/platform/activity-log?tabs=log-analytics) exposes auto-resume triggers for **Resume Databases** operations under the `Caller` property in the JSON of the **Started** and **Succeeded** events. For more information, see [Monitor the serverless compute tier](serverless-tier-monitor.md).

## Latency

The latency is generally on the order of one minute to auto-resume and 1-10 minutes to auto-pause. The latency for either operation can be as low as the order of one second.

## Customer managed transparent data encryption

### Key deletion or revocation

If you use [customer managed transparent data encryption](transparent-data-encryption-byok-overview.md) (bring your own key or BYOK) and the serverless database is auto-paused when key deletion or revocation occurs, the database remains in the auto-paused state. In this case, after the database is next resumed, the database becomes inaccessible within approximately 10 minutes. Once the database becomes inaccessible, the recovery process is the same as for provisioned compute databases. If the serverless database is online when key deletion or revocation occurs, the database also becomes inaccessible within approximately 10 minutes in the same way as with provisioned compute databases.

### Key rotation

If you use [customer-managed transparent data encryption](transparent-data-encryption-byok-overview.md) (BYOK) and enable serverless auto-pausing, the database auto-resumes whenever keys are rotated. The database then auto-pauses when auto-pausing conditions are satisfied.

<a id="connectivity"></a>

## Auto-pause troubleshooting

### Troubleshooting auto-resume connectivity

If a serverless database is paused, the first connection attempt resumes the database and returns an error stating that the database is unavailable with error code 40613. Once the database resumes, retry the connection. Databases generally resume in less than one minute.

All cloud-connected applications should use [connection retry logic recommendations](/azure/architecture/patterns/retry). Applications require retry logic to succeed after transient connectivity errors. Retry logic is especially important for serverless databases, where temporary connectivity errors due to auto-resume are predictable.  

For connection retry logic options and recommendations, see:

- [Connection retry logic in SqlClient](/sql/connect/ado-net/configurable-retry-logic)
- [Connection retry logic in SQL Database using Entity Framework Core](/azure/architecture/best-practices/retry-service-specific#sql-database-using-entity-framework-core)
- [Connection retry logic in SQL Database using Entity Framework 6](/azure/architecture/best-practices/retry-service-specific#sql-database-using-entity-framework-6)
- [Connection retry logic in SQL Database using ADO.NET](/azure/architecture/best-practices/retry-service-specific#sql-database-using-adonet)
- [Connection resiliency in JDBC](/sql/connect/jdbc/connection-resiliency)
- [Connection resiliency in PHP](/sql/connect/php/connection-resiliency)
- [Connection resiliency in ODBC](/sql/connect/odbc/connection-resiliency)

### Troubleshooting auto-pause

If you enable auto-pausing and don't use features that block auto-pausing, but the database doesn't auto-pause after the delay period, application or user sessions might be preventing auto-pausing.

To see if any application or user sessions are currently connected to the database, run the following query:

```sql
SELECT session_id,
       host_name,
       program_name,
       client_interface_name,
       login_name,
       status,
       login_time,
       last_request_start_time,
       last_request_end_time
FROM sys.dm_exec_sessions AS s
INNER JOIN sys.dm_resource_governor_workload_groups AS wg
ON s.group_id = wg.group_id
WHERE s.session_id <> @@SPID
      AND
      (
          (
          wg.name like 'UserPrimaryGroup.DB%'
          AND
          TRY_CAST(RIGHT(wg.name, LEN(wg.name) - LEN('UserPrimaryGroup.DB') - 2) AS int) = DB_ID()
          )
      OR
      wg.name = 'DACGroup'
      );
```

> [!TIP]
> After running the query, make sure to disconnect from the database. Otherwise, the open session used by the query prevents auto-pausing.

- If the result set isn't empty, it indicates that sessions currently prevent auto-pausing.
- If the result set is empty, it's still possible that sessions were open, possibly for a short time, at some point earlier during the auto-pause delay period. To check for activity during the delay period, use [Auditing for Azure SQL Database and Azure Synapse Analytics](auditing-overview.md) and examine audit data for the relevant period.

> [!IMPORTANT]
> The presence of open sessions, with or without concurrent CPU utilization in the user resource pool, is the most common reason for a serverless database to not auto-pause as expected.

## Related content

- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Serverless compute tier billing](serverless-tier-billing.md)
- [Create a serverless database](serverless-tier-create-configure.md)
