---
title: "sys.database_connection_stats (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/25/2016"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_connection_stats"
  - "database_connection_stats"
  - "database_connection_stats_TSQL"
  - "sys.database_connection_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.database_connection_stats"
  - "database_connection_stats"
ms.assetid: 5c8cece0-63b0-4dee-8db7-6b43d94027ec
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.database_connection_stats (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Contains statistics for [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database **connectivity** events, providing an overview of database connection successes and failures. For more information about connectivity events, see Event Types in [sys.event_log &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md).  
  
|Statistic|Type|Description|  
|---------------|----------|-----------------|  
|**database_name**|**sysname**|Name of the database.|  
|**start_time**|**datetime2**|UTC date and time of the start of the aggregation interval. The time is always a multiple of 5 minutes. For example:<br /><br /> '2011-09-28 16:00:00'<br />'2011-09-28 16:05:00'<br />'2011-09-28 16:10:00'|  
|**end_time**|**datetime2**|UTC date and time of the end of the aggregation interval. **End_time** is always exactly 5 minutes later than the corresponding **start_time** in the same row.|  
|**success_count**|**int**|Number of successful connections.|  
|**total_failure_count**|**int**|Total number of failed connections. This is the sum of **connection_failure_count**, **terminated_connection_count**, and **throttled_connection_count**, and does not include deadlock events.|  
|**connection_failure_count**|**int**|Number of login failures.|  
|**terminated_connection_count**|**int**|**_Only applicable for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] v11._**<br /><br /> Number of terminated connections.|  
|**throttled_connection_count**|**int**|**_Only applicable for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] v11._**<br /><br /> Number of throttled connections.|  
  
## Remarks  
  
### Event Aggregation  
 Event information for this view is collected and aggregated within 5-minute intervals. The count columns represent the number of times a particular connectivity event occurred for a specific database within a given time interval.  
  
 For example, if a user fails to connect to database Database1 seven times between 11:00 and 11:05 on 2/5/2012 (UTC), this information is available in a single row in this view:  
  
|**database_name**|**start_time**|**end_time**|**success_count**|**total_failure_count**|**connection_failure_count**|**terminated_connection_count**|**throttled_connection_count**|  
|------------------------|---------------------|-------------------|------------------------|-------------------------------|------------------------------------|---------------------------------------|--------------------------------------|  
|`Database1`|`2012-02-05 11:00:00`|`2012-02-05 11:05:00`|`0`|`7`|`7`|`0`|`0`|  
  
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
  
-   This view does not include all [!INCLUDE[ssSDS](../../includes/sssds-md.md)] database errors that could occur, only those specified in Event Types in [sys.event_log &#40;Azure SQL Database&#41;](../../relational-databases/system-catalog-views/sys-event-log-azure-sql-database.md).  
  
-   If there is a machine failure within the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] datacenter, a small amount of data for your logical server may be missing from the event table.  
  
-   If an IP address has been blocked through DoSGuard, connection attempt events from that IP address cannot be collected and will not appear in this view.  
  
## Permissions  
 Users with permission to access the **master** database have read-only access to this view.  
  
## Example  
 The following example shows a query of **sys.database_connection_stats** to return a summary of the database connections that occurred between noon on 9/25/2011 and noon on 9/28/2011 (UTC). By default, the query results are sorted by **start_time** (ascending order).  
  
```  
SELECT *  
FROM sys.database_connection_stats   
WHERE start_time>='2011-09-25:12:00:00' and end_time<='2011-09-28 12:00:00';  
```  
  
## See Also  
 [Troubleshooting Windows Azure SQL Database](https://msdn.microsoft.com/library/windowsazure/ee730906.aspx)  
  
  
