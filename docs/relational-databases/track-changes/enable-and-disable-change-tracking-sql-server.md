---
description: "Enable and Disable Change Tracking (SQL Server)"
title: "Enable and Disable Change Tracking"
ms.custom: seo-dt-2019
ms.date: "08/08/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change tracking [SQL Server], disabling"
  - "data changes [SQL Server]"
  - "change tracking [SQL Server], enabling"
  - "tracking data changes [SQL Server]"
  - "change tracking [SQL Server], configuring"
  - "data [SQL Server], changing"
ms.assetid: 1c92ec7e-ae53-4498-8bfd-c66a42a24d54
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Enable and Disable Change Tracking (SQL Server)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This topic describes how to enable and disable change tracking for a database and a table.  
  
## Enable Change Tracking for a Database  
 Before you can use change tracking, you must enable change tracking at the database level. The following example shows how to enable change tracking by using [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET CHANGE_TRACKING = ON  
(CHANGE_RETENTION = 2 DAYS, AUTO_CLEANUP = ON)  
```  
  
 You can also enable change tracking in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the [Database Properties &#40;ChangeTracking Page&#41;](../../relational-databases/databases/database-properties-changetracking-page.md) dialog box. If a database contains memory optimized tables, you can't enable change tracking with SQL Server Management Studio. To enable, use T-SQL.
  
 You can specify the CHANGE_RETENTION and AUTO_CLEANUP options when you enable change tracking, and you can change the values at any time after change tracking is enabled.  
  
 The change retention value specifies the time period for which change tracking information is kept. Change tracking information that is older than this time period is removed periodically. When you are setting this value, you should consider how often applications will synchronize with the tables in the database. The specified retention period must be at least as long as the maximum time period between synchronizations. If an application obtains changes at longer intervals, the results that are returned might be incorrect because some of the change information has probably been removed. To avoid obtaining incorrect results, an application can use the CHANGE_TRACKING_MIN_VALID_VERSION system function to determine whether the interval between synchronizations has been too long.  
  
 You can use the AUTO_CLEANUP option to enable or disable the cleanup task that removes old change tracking information. This can be useful when there is a temporary problem that prevents applications from synchronizing and the process for removing change tracking information older than the retention period must be paused until the problem is resolved.  
  
 For any database that uses change tracking, be aware of the following:  
  
-   To use change tracking, the database compatibility level must be set to 90 or greater. If a database has a compatibility level of less than 90, you can configure change tracking. However, the CHANGETABLE function, which is used to obtain change tracking information, will return an error.  
  
-   Using snapshot isolation is the easiest way for you to help ensure that all change tracking information is consistent. For this reason, we strongly recommend that snapshot isolation be set to ON for the database. For more information, see [Work with Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/work-with-change-tracking-sql-server.md).  
  
## Enable Change Tracking for a Table  
 Change tracking must be enabled for each table that you want tracked. When change tracking is enabled, change tracking information is maintained for all rows in the table that are affected by a DML operation.  
  
 The following example shows how to enable change tracking for a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
```sql  
ALTER TABLE Person.Contact  
ENABLE CHANGE_TRACKING  
WITH (TRACK_COLUMNS_UPDATED = ON)  
```  
  
 You can also enable change tracking for a table in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the [Database Properties &#40;ChangeTracking Page&#41;](../../relational-databases/databases/database-properties-changetracking-page.md) dialog box.  
  
 When the TRACK_COLUMNS_UPDATED option is set to ON, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] stores extra information about which columns were updated to the internal change tracking table. Column tracking can enable an application to synchronize only those columns that were updated. This can improve efficiency and performance. However, because maintaining column tracking information adds some extra storage overhead, this option is set to OFF by default.  
  
## Disable Change Tracking for a Database or Table  
 Change tracking must first be disabled for all change-tracked tables before change tracking can be set to OFF for the database. To determine the tables that have change tracking enabled for a database, use the [sys.change_tracking_tables](../../relational-databases/system-catalog-views/change-tracking-catalog-views-sys-change-tracking-tables.md) catalog view.  
  
 When no tables in a database track changes, you can disable change tracking for the database. The following example shows how to disable change tracking for a database by using [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET CHANGE_TRACKING = OFF  
```  
  
 The following example shows how to disable change tracking for a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
```sql  
ALTER TABLE Person.Contact  
DISABLE CHANGE_TRACKING;  
```  
  
## See Also  
 [Database Properties &#40;ChangeTracking Page&#41;](../../relational-databases/databases/database-properties-changetracking-page.md)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [sys.change_tracking_databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/change-tracking-catalog-views-sys-change-tracking-databases.md)   
 [sys.change_tracking_tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/change-tracking-catalog-views-sys-change-tracking-tables.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)   
 [About Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)   
 [Work with Change Data &#40;SQL Server&#41;](../../relational-databases/track-changes/work-with-change-data-sql-server.md)   
 [Manage Change Tracking &#40;SQL Server&#41;](../../relational-databases/track-changes/manage-change-tracking-sql-server.md)  
  
  
