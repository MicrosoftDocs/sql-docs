---
title: "sys.event_log (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod:
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "language-reference"
f1_keywords: 
  - "event_log"
  - "sys.event_log_TSQL"
  - "event_log_TSQL"
  - "sys.event_log"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "event_log"
  - "sys.event_log"
ms.assetid: ad5496b5-e5c7-4a18-b5a0-3f985d7c4758
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.event_log (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns successful [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] database connections, connection failures, and deadlocks. You can use this information to track or troubleshoot your database activity with [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
> [!CAUTION]  
>  For installations having a large number of databases or high numbers of logins, activity in sys.event_log can cause limitations in performance, high CPU usage, and possibly result in login failures. Queries of sys.event_log can contribute to the problem. Microsoft is working to resolve this issue. In the meantime, to reduce the impact of this issue, limit queries of sys.event_log. Users of the NewRelic SQL Server plugin should visit [Microsoft Azure SQL Database plugin tuning & performance tweaks](https://discuss.newrelic.com/t/microsoft-azure-sql-database-plugin-tuning-performance-tweaks/30729) for additional configuration information.  
  
 The `sys.event_log` view contains the following columns.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**sysname**|Name of the database. If the connection fails and the user did not specify a database name, then this column is blank.|  
|**start_time**|**datetime2**|UTC date and time of the start of the aggregation interval. For aggregated events, the time is always a multiple of 5 minutes. For example:<br /><br /> '2011-09-28 16:00:00'<br />'2011-09-28 16:05:00'<br />'2011-09-28 16:10:00'|  
|**end_time**|**datetime2**|UTC date and time of the end of the aggregation interval. For aggregated events, **End_time** is always exactly 5 minutes later than the corresponding **start_time** in the same row. For events that are not aggregated, **start_time** and **end_time** equal the actual UTC date and time of the event.|  
|**event_category**|**nvarchar(64)**|The high-level component that generated this event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_type**|**nvarchar(64)**|The type of event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_subtype**|**int**|The subtype of the occurring event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**event_subtype_desc**|**nvarchar(64)**|The description of the event subtype.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**severity**|**int**|The severity of the error. Possible values are:<br /><br /> 0 = Information<br />1 = Warning<br />2 = Error|  
|**event_count**|**int**|The number of times that this event occurred for the specified database within the time interval specified (**start_time** and **end_time**).|  
|**description**|**nvarchar(max)**|A detailed description of the event.<br /><br /> See [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) for a list of possible values.|  
|**additional_data**|**XML**|*Note: This value is always NULL for Azure SQL Database V12. See [examples](#Deadlock) section for how to retrieve deadlock events for V12.*<br /><br /> For **Deadlock** events, this column contains the deadlock graph. This column is NULL for other event types. |  
  
##  <a name="EventTypes"></a> Event Types  
 The events recorded by each row in this view are identified by a category (**event_category**), event type (**event_type**), and a subtype (**event_subtype**). The following table lists the types of events that are collected in this view.  
  
 For events in the **connectivity** category, summary information is available in the sys.database_connection_stats view.  
  
> [!NOTE]  
>  This view does not include all possible [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database events that can occur, only those listed here. Additional categories, event types, and subtypes may be added in future releases of [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
|**event_category**|**event_type**|**event_subtype**|**event_subtype_desc**|**severity**|**description**|  
|-------------------------|---------------------|------------------------|------------------------------|------------------|---------------------|  
|**connectivity**|**connection_successful**|0|**connection_successful**|0|Connected successfully to database.|  
|**connectivity**|**connection_failed**|0|**invalid_login_name**|2|Login name is not valid in this version of SQL Server.|  
|**connectivity**|**connection_failed**|1|**windows_auth_not_supported**|2|Windows logins are not supported in this version of SQL Server.|  
|**connectivity**|**connection_failed**|2|**attach_db_not_supported**|2|User requested to attach a database file which is not supported.|  
|**connectivity**|**connection_failed**|3|**change_password_not_supported**|2|User requested to change the password of the user logging in which is not supported.|  
|**connectivity**|**connection_failed**|4|**login_failed_for_user**|2|Login failed for user.|  
|**connectivity**|**connection_failed**|5|**login_disabled**|2|The login was disabled.|  
|**connectivity**|**connection_failed**|6|**failed_to_open_db**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Database could not be opened. May be caused because database does not exist or lack of authentication to open the database.|  
|**connectivity**|**connection_failed**|7|**blocked_by_firewall**|2|Client IP address is not allowed to access the server.|  
|**connectivity**|**connection_failed**|8|**client_close**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Client may have timed out when establishing connection. Try increasing the connection timeout.|  
|**connectivity**|**connection_failed**|9|**reconfiguration**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Connection failed because the database was going through a reconfiguration at the time.|  
|**connectivity**|**connection_terminated**|0|**idle_connection_timeout**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Connection has been idle for longer than system defined threshold.|  
|**connectivity**|**connection_terminated**|1|**reconfiguration**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> The session has been terminated due to a database reconfiguration.|  
|**connectivity**|**throttling**|*\<reason code>*|**reason_code**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Request is throttled.  Throttling reason code: *\<reason code>*. For more information, see [Engine Throttling](https://msdn.microsoft.com/library/windowsazure/dn338079.aspx).|  
|**connectivity**|**throttling_long_transaction**|40549|**long_transaction**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> Session is terminated because you have a long-running transaction. Try shortening your transaction. For more information, see [Resource Limits](https://msdn.microsoft.com/library/windowsazure/dn338081.aspx).|  
|**connectivity**|**throttling_long_transaction**|40550|**excessive_lock_usage**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> The session has been terminated because it has acquired too many locks. Try reading or modifying fewer rows in a single transaction. For more information, see [Resource Limits](https://msdn.microsoft.com/library/windowsazure/dn338081.aspx).|  
|**connectivity**|**throttling_long_transaction**|40551|**excessive_tempdb_usage**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> The session has been terminated because of excessive TEMPDB usage. Try modifying your query to reduce the temporary table space usage. For more information, see [Resource Limits](https://msdn.microsoft.com/library/windowsazure/dn338081.aspx).|  
|**connectivity**|**throttling_long_transaction**|40552|**excessive_log_space_usage**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> The session has been terminated because of excessive transaction log space usage. Try modifying fewer rows in a single transaction. For more information, see [Resource Limits](https://msdn.microsoft.com/library/windowsazure/dn338081.aspx).|  
|**connectivity**|**throttling_long_transaction**|40553|**excessive_memory_usage**|2|*Note: Applies only to Azure SQL Database V11.*<br /><br /> The session has been terminated because of excessive memory usage. Try modifying your query to process fewer rows. For more information, see [Resource Limits](https://msdn.microsoft.com/library/windowsazure/dn338081.aspx).|  
|**engine**|**deadlock**|0|**deadlock**|2|Deadlock occurred.|  
  
## Permissions  
 Users with permission to access the **master** database have read-only access to this view.  
  
## Remarks  
  
### Event Aggregation  
 Event information for this view is collected and aggregated within 5-minute intervals. The **event_count** column represents the number of times a particular **event_type** and **event_subtype** occurred for a specific database within a given time interval.  
  
> [!NOTE]  
>  Some events, such as deadlocks, are not aggregated. For these events, **event_count** will be 1 and **start_time** and **end_time** will equal the actual UTC date and time when the event occurred.  
  
 For example, if a user fails to connect to database Database1, because of an invalid login name, seven times between 11:00 and 11:05 on 2/5/2012 (UTC), this information is available in a single row in this view:  
  
|**database_name**|**start_time**|**end_time**|**event_category**|**event_type**|**event_subtype**|**event_subtype_desc**|**severity**|**event_count**|**description**|**additional_data**|  
|------------------------|---------------------|-------------------|-------------------------|---------------------|------------------------|------------------------------|------------------|----------------------|---------------------|--------------------------|  
|`Database1`|`2012-02-05 11:00:00`|`2012-02-05 11:05:00`|`connectivity`|`connection_failed`|`4`|`login_failed_for_user`|`2`|`7`|`Login failed for user.`|`NULL`|  
  
### Interval start_time and end_time  
 An event is included in an aggregation interval when the event occurs *on* or _after_**start_time** and _before_**end_time** for that interval. For example, an event occurring exactly at `2012-10-30 19:25:00.0000000` would be included only in the second interval shown below:  
  
```  
start_time                    end_time  
2012-10-30 19:20:00.0000000   2012-10-30 19:25:00.0000000  
2012-10-30 19:25:00.0000000   2012-10-30 19:30:00.0000000  
```  
  
### Data Updates  
 Data in this view is accumulated over time. Typically, the data is accumulated within an hour of the start of the aggregation interval, but it may take up to a maximum of 24 hours for all the data to appear in the view. During that time, the information within a single row may be updated periodically.  
  
### Data Retention  
 The data in this view is retained for a maximum of 30 days, or possibly less depending on the number of databases in the logical server and the number of unique events each database generates. To retain this information for a longer period, copy the data to a separate database. After you make an initial copy of the view, the rows in the view may be updated as data is accumulated. To keep your copy of the data up-to-date, periodically do a table scan of the rows to look for an increase in the event count of existing rows and to identify new rows (you can identify unique rows by using the start and end times), then update your copy of the data with these changes.  
  
### Errors Not Included  
 This view may not include all connection and error information:  
  
-   This view does not include all [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database errors that could occur, only those specified in [Event Types](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md#EventTypes) in this topic.  
  
-   If there is a machine failure within the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] datacenter, a small amount of data for your logical server may be missing from the event table.  
  
-   If an IP address has been blocked through DoSGuard, connection attempt events from that IP address cannot be collected and will not appear in this view.  
  
## Examples  
  
### Simple examples  
 The following query returns all events that occurred between noon on 9/25/2011 and noon on 9/28/2011 (UTC). By default, query results are sorted by **start_time** (ascending order).  
  
```  
SELECT * FROM sys.event_log   
WHERE start_time >= '2011-09-25 12:00:00'   
    AND end_time <= '2011-09-28 12:00:00';  
```  
  
 The following query returns all deadlock events for database Database1 (applies only to Azure SQL Database V11).  
  
```  
SELECT * FROM sys.event_log   
WHERE event_type = 'deadlock'   
    AND database_name = 'Database1';  
```  
<a name="Deadlock"></a> The following query returns all deadlock events for database Database1 (applies only to Azure SQL Database V12).  
  
```  
WITH CTE AS (  
       SELECT CAST(event_data AS XML)  AS [target_data_XML]   
   FROM sys.fn_xe_telemetry_blob_target_read_file('dl', null, null, null)  
)  
SELECT target_data_XML.value('(/event/@timestamp)[1]', 'DateTime2') AS Timestamp,  
target_data_XML.query('/event/data[@name=''xml_report'']/value/deadlock') AS deadlock_xml,  
target_data_XML.query('/event/data[@name=''database_name'']/value').value('(/value)[1]', 'nvarchar(100)') AS db_name  
FROM CTE   
  
```  
  
  
 The following query returns hard throttling on SQL Worker Threads events that occurred between 10:00 and 11:00 on 9/25/2011 (UTC).  
  
```  
SELECT * FROM sys.event_log   
WHERE event_type = 'throttling'   
    AND event_subtype = 4194307   
    AND start_time >= '2011-09-25 10:00:00'   
    AND end_time <= '2011-09-25 11:00:00';  
```  
  
### DB-Scoped Extended Event  
 Use the following sample code to set up the db-scoped Extended Event (XEvent) session:  
  
```sql  
IF EXISTS  
    (SELECT * from sys.database_event_sessions  
        WHERE name = 'azure_monitor_deadlock_session')  
BEGIN  
    ALTER EVENT SESSION azure_monitor_deadlock_session  
        ON DATABASE  
        DROP TARGET package0.ring_buffer;  
  
    DROP EVENT SESSION azure_monitor_deadlock_session  
        ON DATABASE;  
END  
  
CREATE EVENT SESSION azure_monitor_deadlock_session  
    ON DATABASE  
    ADD EVENT sqlserver.database_xml_deadlock_report  
    ADD TARGET package0.ring_buffer  
    (  
        SET max_memory = 2048, max_events_limit = 10  
    )  
    WITH (STARTUP_STATE = ON,  
          EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS);  
  
ALTER EVENT SESSION azure_monitor_deadlock_session  
    ON DATABASE  
    STATE = START;  
```  
  
### Check for Deadlock

Use the following query to check if there is a deadlock.  
  
```sql  
WITH CTE AS (  
    SELECT CAST(xet.target_data AS XML)  AS [target_data_XML]  
        FROM            sys.dm_xe_database_session_targets AS xet  
             INNER JOIN sys.dm_xe_database_sessions        AS xe  
                 ON (xe.address = xet.event_session_address)  
        WHERE xe.name = 'azure_monitor_deadlock_session'  
)  
, CTE2 AS (  
    SELECT  
            T2.EventData.query('.').value(  
                '(/event/@timestamp)[1]', 'DateTime2') AS Timestamp,  
            T2.EventData.query('.').query(  
                '(/event/data/value/deadlock)[1]')     AS deadlock_xml  
        FROM CTE  
            CROSS Apply [target_data_XML].nodes(  
                '/RingBufferTarget/event') AS T2(EventData)  
)  
SELECT * FROM CTE2;  
```  
  
## See Also  
 [Extended events in Azure SQL Database](https://azure.microsoft.com/documentation/articles/sql-database-xevent-db-diff-from-svr/)  
  
  
