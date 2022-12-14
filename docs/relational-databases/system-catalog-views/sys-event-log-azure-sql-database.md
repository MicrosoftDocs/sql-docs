---
title: "sys.event_log (Azure SQL Database)"
description: sys.event_log (Azure SQL Database)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/18/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "event_log"
  - "sys.event_log_TSQL"
  - "event_log_TSQL"
  - "sys.event_log"
helpviewer_keywords:
  - "event_log"
  - "sys.event_log"
dev_langs:
  - "TSQL"
ms.assetid: ad5496b5-e5c7-4a18-b5a0-3f985d7c4758
monikerRange: "=azuresqldb-current"
---
# sys.event_log (Azure SQL Database)

[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns successful [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] database connections and connection failures. You can use this information to track or troubleshoot your database activity.  
  
> [!CAUTION]  
> For [logical servers](/azure/azure-sql/database/logical-servers) with a large number of databases and/or high numbers of logins, querying sys.event_log can cause high resource usage in the master database, possibly resulting in login failures. To reduce the impact of this issue, limit queries of sys.event_log.

The `sys.event_log` view contains the following columns.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**sysname**|Name of the database. If the connection fails and the user did not specify a database name, then this column is blank.|  
|**start_time**|**datetime2**|UTC date and time of the start of the aggregation interval. For aggregated events, the time is always a multiple of 5 minutes. For example:<br /><br /> '2022-03-30 16:00:00'<br />'2022-03-30 16:05:00'<br />'2022-03-30 16:10:00'|  
|**end_time**|**datetime2**|UTC date and time of the end of the aggregation interval. For aggregated events, **End_time** is always exactly 5 minutes later than the corresponding **start_time** in the same row. For events that are not aggregated, **start_time** and **end_time** equal the actual UTC date and time of the event.|  
|**event_category**|**nvarchar(64)**|The high-level component that generated this event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_type**|**nvarchar(64)**|The type of event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_subtype**|**int**|The subtype of the occurring event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_subtype_desc**|**nvarchar(64)**|The description of the event subtype.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**severity**|**int**|The severity of the error. Possible values are:<br /><br /> 0 = Information<br />1 = Warning<br />2 = Error|  
|**event_count**|**int**|The number of times that this event occurred for the specified database within the time interval specified (**start_time** and **end_time**).|  
|**description**|**nvarchar(max)**|A detailed description of the event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**additional_data**|**XML**|This column is unused and is preserved for backwards compatibility. |  
  
##  <a name="EventTypes"></a> Event types

 The events recorded by each row in this view are identified by a category (**event_category**), event type (**event_type**), and a subtype (**event_subtype**). The following table lists the types of events that are collected in this view.  
  
 For events in the **connectivity** category, summary information is available in the sys.database_connection_stats view.  
  
> [!NOTE]  
> This view does not include all possible [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database events that can occur, only those listed here. Additional categories, event types, and subtypes may be added in future releases of [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
|**event_category**|**event_type**|**event_subtype**|**event_subtype_desc**|**severity**|**description**|  
|-------------------------|---------------------|------------------------|------------------------------|------------------|---------------------|  
|**connectivity**|**connection_successful**|0|**connection_successful**|0|Connected successfully to database.|  
|**connectivity**|**connection_failed**|0|**invalid_login_name**|2|Login name is not valid in this version of SQL Server.|  
|**connectivity**|**connection_failed**|1|**windows_auth_not_supported**|2|Windows logins are not supported in this version of SQL Server.|  
|**connectivity**|**connection_failed**|2|**attach_db_not_supported**|2|User requested to attach a database file, which is not supported.|  
|**connectivity**|**connection_failed**|3|**change_password_not_supported**|2|User requested to change the password of the user logging in which is not supported.|  
|**connectivity**|**connection_failed**|4|**login_failed_for_user**|2|Login failed for user.|  
|**connectivity**|**connection_failed**|5|**login_disabled**|2|The login was disabled.|  
|**connectivity**|**connection_failed**|7|**blocked_by_firewall**|2|Client IP address is not allowed to access the server.|  
  
## Permissions

Users with permission to access the **master** database on the [logical server](/azure/azure-sql/database/logical-servers) in Azure SQL Database have read-only access to this view.  
  
## Remarks  
  
### Event aggregation

 Event information for this view is collected and aggregated within 5-minute intervals. The **event_count** column represents the number of times a particular **event_type** and **event_subtype** occurred for a specific database within a given time interval.  
  
> [!NOTE]  
> Some events, such as deadlocks, are not aggregated. For these events, **event_count** will be 1 and **start_time** and **end_time** will equal the actual UTC date and time when the event occurred.  
  
 For example, if a user fails to connect to database Database1, because of an invalid login name, seven times between 11:00 and 11:05 on 3/30/2022 (UTC), this information is available in a single row in this view:  
  
|**database_name**|**start_time**|**end_time**|**event_category**|**event_type**|**event_subtype**|**event_subtype_desc**|**severity**|**event_count**|**description**|**additional_data**|  
|------------------------|---------------------|-------------------|-------------------------|---------------------|------------------------|------------------------------|------------------|----------------------|---------------------|--------------------------|  
|`Database1`|`2022-03-30 11:00:00`|`2022-03-30 11:05:00`|`connectivity`|`connection_failed`|`4`|`login_failed_for_user`|`2`|`7`|`Login failed for user.`|`NULL`|  
  
### Interval start_time and end_time  

An event is included in an aggregation interval when the event occurs *on* or _after_ **start_time** and _before_ **end_time** for that interval. For example, an event occurring exactly at `2022-03-30 19:25:00.0000000` would be included only in the second interval shown below:  
  
```
start_time                    end_time  
2022-03-30 19:20:00.0000000   2022-03-30 19:25:00.0000000  
2022-03-30 19:25:00.0000000   2022-03-30 19:30:00.0000000  
```

### Data updates

Data in this view is accumulated over time. Typically, the data is accumulated within an hour of the start of the aggregation interval, but it may take up to a maximum of 24 hours for all the data to appear in the view. During that time, the information within a single row may be updated periodically.  
  
### Data retention

The data in this view is retained for a maximum of 30 days, or possibly less depending on the number of databases and the number of unique events each database generates. To retain this information for a longer period, copy the data to a separate database. After you make an initial copy of the view, the rows in the view may be updated as data is accumulated. To keep your copy of the data up-to-date, periodically do a table scan of the rows to look for an increase in the event count of existing rows and to identify new rows (you can identify unique rows by using the start and end times), then update your copy of the data with these changes.  
  
### Errors not included

This view may not include all connection and error information:  
  
- This view does not include all [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database errors that could occur, only those specified in [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) in this article.  
- If there is a machine failure within the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] datacenter, a small amount of data may be missing from the event table.  
- If an IP address has been blocked through DoSGuard, connection attempt events from that IP address cannot be collected and will not appear in this view.  
  
## Examples  

Connect to the **master** database on the [logical server](/azure/azure-sql/database/logical-servers) in Azure SQL Database to run the following Transact-SQL queries.
  
### Query the sys.event_log view

The following query returns all events that occurred between noon on March 25, 2022 and noon on March 30, 2022 (UTC). By default, query results are sorted by **start_time** (ascending order).  

```sql
SELECT database_name, start_time, end_time, event_category,
	event_type, event_subtype, event_subtype_desc, severity,
	event_count, description
FROM sys.event_log
WHERE start_time >= '2022-03-25 12:00:00'
    AND end_time <= '2022-03-30 12:00:00';  
```

### Query login failures for users

The following query returns connection failures that are failed logins for users that occurred between 10:00 and 11:00 on March 25, 2022 (UTC).  

```sql
SELECT database_name, start_time, end_time, event_category,
	event_type, event_subtype, event_subtype_desc, severity,
	event_count, description
FROM sys.event_log
WHERE event_type = 'connection_failed'
    AND event_subtype = 4
    AND start_time >= '2022-03-25 10:00:00'
    AND end_time <= '2022-03-25 11:00:00';  
```

## Next steps

Learn more about Azure SQL Database in these articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Azure SQL Database Catalog Views](azure-sql-database-catalog-views.md)
- [sys.database_connection_stats (Azure SQL Database)](sys-database-connection-stats-azure-sql-database.md)
- [Troubleshooting connectivity issues and other errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-errors-issues)
- [Troubleshoot transient connection errors in SQL Database and SQL Managed Instance](/azure/azure-sql/database/troubleshoot-common-connectivity-issues)
