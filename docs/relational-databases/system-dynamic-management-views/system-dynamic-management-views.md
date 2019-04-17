---
title: "Dynamic Management Views (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database scoped dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], about dynamic management objects"
  - "server state information [SQL Server]"
  - "dynamic management functions [SQL Server]"
  - "metadata [SQL Server], dynamic management objects"
  - "dynamic management views [SQL Server]"
  - "DMVs [SQL Server]"
  - "functions [SQL Server], dynamic management"
  - "server scoped dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server]"
ms.assetid: cf893ecb-0bf6-4cbf-ac00-8a1099e405b1
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System Dynamic Management Views
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Dynamic management views and functions return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance.  
  
> [!IMPORTANT]  
>  Dynamic management views and functions return internal, implementation-specific state data. Their schemas and the data they return may change in future releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, dynamic management views and functions in future releases may not be compatible with the dynamic management views and functions in this release. For example, in future releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Microsoft may augment the definition of any dynamic management view by adding columns to the end of the column list. We recommend against using the syntax `SELECT * FROM dynamic_management_view_name` in production code because the number of columns returned might change and break your application.  
  
 There are two types of dynamic management views and functions:  
  
-   Server-scoped dynamic management views and functions. These require VIEW SERVER STATE permission on the server.  
  
-   Database-scoped dynamic management views and functions. These require VIEW DATABASE STATE permission on the database.  
  
## Querying Dynamic Management Views  
 Dynamic management views can be referenced in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using two-part, three-part, or four-part names. Dynamic management functions on the other hand can be referenced in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using either two-part or three-part names. Dynamic management views and functions cannot be referenced in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by using one-part names.  
  
 All dynamic management views and functions exist in the sys schema and follow this naming convention dm_*. When you use a dynamic management view or function, you must prefix the name of the view or function by using the sys schema. For example, to query the dm_os_wait_stats dynamic management view, run the following query:  
  
 ```sql
SELECT wait_type, wait_time_ms  
FROM sys.dm_os_wait_stats;  
```  
  
### Required Permissions  
 To query a dynamic management view or function requires SELECT permission on object and VIEW SERVER STATE or VIEW DATABASE STATE permission. This lets you selectively restrict access of a user or login to dynamic management views and functions. To do this, first create the user in master and then deny the user SELECT permission on the dynamic management views or functions that you do not want them to access. After this, the user cannot select from these dynamic management views or functions, regardless of database context of the user.  
  
> [!NOTE]  
>  Because DENY takes precedence, if a user has been granted VIEW SERVER STATE permissions but denied VIEW DATABASE STATE permission, the user can see server-level information, but not database-level information.  
  
## In This Section  
 Dynamic management views and functions have been organized into the following categories.  
  
|||  
|-|-|  
|[Always On Availability Groups Dynamic Management Views and Functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)|[Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)|  
|[Change Data Capture Related Dynamic Management Views &#40;Transact-SQL&#41;](change-data-capture-sys-dm-cdc-errors.md)|[Object Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/object-related-dynamic-management-views-and-functions-transact-sql.md)|  
|[Change Tracking Related Dynamic Management Views](change-tracking-sys-dm-tran-commit-table.md)|[Query Notifications Related Dynamic Management Views &#40;Transact-SQL&#41;](query-notifications-sys-dm-qn-subscriptions.md)|  
|[Common Language Runtime Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/common-language-runtime-related-dynamic-management-views-transact-sql.md)|[Replication Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/replication-related-dynamic-management-views-transact-sql.md)|  
|[Database Mirroring Related Dynamic Management Views &#40;Transact-SQL&#41;](database-mirroring-sys-dm-db-mirroring-auto-page-repair.md)|[Resource Governor Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md)|  
|[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)|[Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)|  
|[Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)|[Server-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/server-related-dynamic-management-views-and-functions-transact-sql.md)|  
|[Extended Events Dynamic Management Views](../../relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views.md)|[Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)|  
|[Filestream and FileTable Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)|[Spatial Data Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/c542ac38-451f-43a5-bf8c-4edd38bb738e)|  
|[Full-Text Search and Semantic Search Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)|[SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)|  
|[Geo-Replication Dynamic Management Views and Functions &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)|[SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)|  
|[Index Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/index-related-dynamic-management-views-and-functions-transact-sql.md)|[Stretch Database Dynamic Management Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/1193efce-a105-49a9-a8b8-26b063485567)|  
|[I O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)|[Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)|  

  
## See Also  
 [GRANT Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-permissions-transact-sql.md)   
 [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md)   
 [System Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/35a6161d-7f43-4e00-bcd3-3091f2015e90)  
  
  
