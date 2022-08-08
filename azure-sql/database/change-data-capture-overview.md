---
description: "Learn about change data capture (CDC), which records insert, update, and delete activity that applies to a SQL Server table. Use with SQL Server, Azure SQL Managed Instance, and Azure SQL Database"
title: "What is change data capture (CDC)?"
ms.custom:
  - seo-dt-2019
  - intro-overview
ms.date: "03/04/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: "vanto"
ms.technology: 
ms.topic: conceptual
helpviewer_keywords:
  - "change data capture, about"
  - "change data capture"
  - "22832 (Database Engine error)"
ms.assetid: 7d8c4684-9eb1-4791-8c3b-0f0bb15d9634
author: MikeRayMSFT
ms.author: mikeray
---
# What is change data capture (CDC)?
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](change-data-capture-overview.md)
> * [SQL Server & Azure SQL Managed Instance](/sql/relational-databases/track-changes/about-change-data-capture-sql-server)


In this article, learn about how change data capture (CDC) is implemented in Azure SQL Database to record activity on a database when tables and rows have been modified. For details about the CDC feature, including how it's implemented in SQL Server, see [CDC with SQL Server](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). 

## Overview

Change data capture records insert, update, and delete activity that applies to a table. This makes the details of the changes available in an easily consumed relational format. Column information and the metadata that is required to apply the changes to a target environment is captured for the modified rows and stored in change tables that mirror the column structure of the tracked source tables. Table-valued functions are provided to allow systematic access to the change data by consumers.  

A good example of a data consumer that this technology targets is an extraction, transformation, and loading (ETL) application. An ETL application incrementally loads change data from Azure SQL Database source tables to a data warehouse or data mart. Although the representation of the source tables within the data warehouse must reflect changes in the source tables, an end-to-end technology that refreshes a replica of the source is not appropriate. Instead, you need a reliable stream of change data that is structured so that consumers can apply it to dissimilar target representations of the data. Azure SQL Database change data capture provides this technology.  

In Azure SQL Database, the recording of change activity is done through a change data capture scheduler that invokes stored procedures to start periodic capture and cleanup of the change data capture tables. The scheduler runs capture and cleanup automatically within SQL Database, without any external dependency for reliability or performance. Users still have the option to run capture and cleanup manually on demand. 

To learn how about Change Data Capture, you can also refer to this Data Exposed episode.
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Track-and-Record-Data-Changes-with-Change-Data-Capture-CDC-in-Azure-SQL/player?WT.mc_id=dataexposed-c9-niner]

## Performance considerations

The performance impact from enabling change data capture on Azure SQL Database is similar to the performance impact of enabling CDC for SQL Server or Azure SQL Managed Instance. Below are some of the aspects that influence performance impact of enabling CDC: 

- The number of tracked CDC-enabled tables 
- Frequency of changes in the tracked tables  
- Space available in the source database, since CDC artifacts (e.g. CT tables, cdc_jobs etc.) are stored in the same database 
- Whether the database is single or pooled. For databases in elastic pools, in addition to considering the number of tables that have CDC enabled, pay attention to the number of databases those tables belong to. Databases in a pool share resources among them (such as disk space), so enabling CDC on multiple databases runs the risk of reaching the max size of the elastic pool disk size. Monitor resources such as CPU, memory and log throughput. 

To provide more specific performance optimization guidance to customers, more details are needed on each customer’s workload. However, below is some additional general guidance, based on performance tests ran on TPCC workload:

- Consider increasing the number of vCores or shift to a higher database tier (e.g. Hyperscale) to ensure the same performance level as before CDC was enabled on your Azure SQL Database.
- Monitor space utilization closely and test your workload thoroughly before enabling CDC on databases in production.
- Monitor log generation rate. To learn more, review [resource consumption by workloads](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes). 
- Scan/cleanup are part of user workload (user’s resources are used). Performance impact can be substantial since entire rows are added to change tables and for updates operations pre-image is also included.  
- Elastic Pools - Number of CDC-enabled databases should not exceed the number of vCores of the pool, in order to avoid latency increase. To learn more about resource management in dense elastic pools, review [elastic pool resource management.](elastic-pool-resource-management.md). 
- Cleanup – based on the customer's workload, it may be advised to keep the retention period smaller than the default of 3 days, to ensure that the cleanup catches up with all changes in change table. In general, it is good to keep the retention low and track the database size.  
- No Service Level Agreement (SLA) provided for when changes will be populated to the change tables. Sub-second latency is also not supported.
    

  

## CDC cleanup

In Azure SQL Database, a change data capture scheduler takes the place of the SQL Server Agent that invokes stored procedures to start periodic capture and cleanup of the change data capture tables. The scheduler runs capture and cleanup automatically within SQL Database, without any external dependency for reliability or performance. Users still have the option to run capture and cleanup manually on demand using the [sp_cdc_scan](/sql/system-stored-procedures/sys-sp-cdc-scan-transact-sql) and [sp_cdc_cleanup_change_tables](/sql/system-stored-procedures/sys-sp-cdc-cleanup-change-table-transact-sql) procedures.

Azure SQL Database includes two dynamic management views to help you monitor change data capture: [sys.dm_cdc_log_scan_sessions](/sql/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions) and [sys.dm_cdc_errors](/sql/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors).  
 

## Permissions required

The db_owner role is required to enable change data capture for Azure SQL Database. 

## Limitations

Change data capture in Azure SQL Database has the following limitations: 

**Service tier limits**  
CDC can only be enabled on databases tiers S3 and above. Sub-core (Basic, S0, S1, S2) Azure SQL Databases are not supported for CDC.

Dbcopy from database tiers above S3 having CDC enabled to a sub-core SLO presently retains the CDC artifacts, but CDC artifacts may be removed in the future.

**Capture and Cleanup customization**   
Configuring the frequency of the capture and the cleanup processes for CDC in Azure SQL Databases is not possible. Capture and cleanup are run automatically by the scheduler.

**Point-in-time restore (PITR)**  
If you enable CDC on your database as an Microsoft Azure Active Directory (Azure AD) user, it is not possible to Point-in-time restore (PITR) to a sub-core SLO. It is recommended that you restore the database to the same as the source or higher SLO, and then disable CDC if necessary.

**Microsoft Azure Active Directory (Azure AD)**  
If you create a database in Azure SQL Database as an Microsoft Azure Active Directory (Azure AD) user and enable change data capture (CDC) on it, a SQL user (for example, even sysadmin role) won't be able to disable/make changes to CDC artifacts. However, another Azure AD user will be able to enable/disable CDC on the same database.

Similarly, if you create an Azure SQL Database as a SQL user, enabling/disabling change data capture as an Azure AD user won't work.

**Aggressive log truncation**  
While enabling change data capture on your Azure SQL Database, please be aware that aggressive log truncation is disabled (the CDC scan uses the database transaction log).

Enabling change data capture  on a database disables aggressive log truncation behavior. Active transactions will continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or transaction aborts. This might result in the transaction log getting full and the database going into read-only mode.

**Enabling CDC fails on restored database created with Microsoft Azure Active Directory (Azure AD)**  
Enabling CDC will fail if you create a database in Azure SQL Database as a Microsoft Azure Active Directory (Azure AD) user and don't enable CDC, then restore the database and enable CDC on the restored database.

To resolve this issue, follow these steps: 

- Login as Azure AD admin of the server
- Run ALTER AUTHORIZATION command on the database: 

```sql
ALTER AUTHORIZATION ON DATABASE::[<restored_db_name>] TO [<azuread_admin_login_name>];

EXEC sys.sp_cdc_enable_db
```

For other limitations with CDC, see [SQL Server CDC](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). 


## Next steps 
  
 [Track Data Changes &#40;SQL Server&#41;](/sql/relational-databases/track-changes/track-data-changes-sql-server)   
 [Enable and Disable change data capture &#40;SQL Server&#41;](/sql/relational-databases/track-changes/enable-and-disable-change-data-capture-sql-server)   
 [Work with Change Data &#40;SQL Server&#41;](/sql/relational-databases/track-changes/work-with-change-data-sql-server)   
 [Administer and Monitor change data capture &#40;SQL Server&#41;](/sql/relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server)  
 [Temporal Tables](/sql/relational-databases/tables/temporal-tables)
