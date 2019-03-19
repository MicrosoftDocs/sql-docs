---
title: "Enable and Disable Change Data Capture (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "change data capture [SQL Server], enabling tables"
  - "change data capture [SQL Server], enabling databases"
  - "change data capture [SQL Server], disabling databases"
  - "change data capture [SQL Server], disabling tables"
ms.assetid: b741894f-d267-4b10-adfe-cbc14aa6caeb
author: rothja
ms.author: jroth
manager: craigg
---
# Enable and Disable Change Data Capture (SQL Server)
  This topic describes how to enable and disable change data capture for a database and a table.  
  
## Enable Change Data Capture for a Database  
 Before a capture instance can be created for individual tables, a member of the `sysadmin` fixed server role must first enable the database for change data capture. This is done by running the stored procedure [sys.sp_cdc_enable_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-enable-db-transact-sql) in the database context. To determine if a database is already enabled, query the `is_cdc_enabled` column in the `sys.databases` catalog view.  
  
 When a database is enabled for change data capture, the `cdc` schema, `cdc` user, metadata tables, and other system objects are created for the database. The `cdc` schema contains the change data capture metadata tables and, after source tables are enabled for change data capture, the individual change tables serve as a repository for change data. The `cdc` schema also contains associated system functions used to query for change data.  
  
 Change data capture requires exclusive use of the `cdc` schema and `cdc` user. If either a schema or a database user named *cdc* currently exists in a database, the database cannot be enabled for change data capture until the schema and or user are dropped or renamed.  
  
 See the Enable Database for Change Data Capture template for an example of enabling a database.  
  
> [!IMPORTANT]  
>  To locate the templates in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], go to **View**, click **Template Explorer**, and then select **SQL Server Templates**. **Change Data Capture** is a sub-folder. Under this folder, you will find all the templates referenced in this topic. There is also a **Template Explorer** icon on the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] toolbar.  
  
```tsql  
-- ====  
-- Enable Database for CDC template   
-- ====  
USE MyDB  
GO  
EXEC sys.sp_cdc_enable_db  
GO  
```  
  
## Disable Change Data Capture for a Database  
 A member of the `sysadmin` fixed server role can run the stored procedure [sys.sp_cdc_disable_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql) in the database context to disable change data capture for a database. It is not necessary to disable individual tables before you disable the database. Disabling the database removes all associated change data capture metadata, including the `cdc` user and schema and the change data capture jobs. However, any gating roles created by change data capture will not be removed automatically and must be explicitly deleted. To determine if a database is enabled, query the `is_cdc_enabled` column in the sys.databases catalog view.  
  
 If a change data capture enabled database is dropped, change data capture jobs are automatically removed.  
  
 See the Disable Database for Change Data Capture template for an example of disabling a database.  
  
> [!IMPORTANT]  
>  To locate the templates in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], go to **View**, click **Template Explorer**, and then click **SQL Server Templates**. **Change Data Capture** is a sub-folder where you will find all the templates that are referenced in this topic. There is also a **Template Explorer** icon on the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] toolbar.  
  
```tsql  
-- =======  
-- Disable Database for Change Data Capture template   
-- =======  
USE MyDB  
GO  
EXEC sys.sp_cdc_disable_db  
GO  
```  
  
## Enable Change Data Capture for a Table  
 After a database has been enabled for change data capture, members of the `db_owner` fixed database role can create a capture instance for individual source tables by using the stored procedure `sys.sp_cdc_enable_table`. To determine whether a source table has already been enabled for change data capture, examine the is_tracked_by_cdc column in the `sys.tables` catalog view.  
  
 The following options can be specified when creating a capture instance:  
  
 `Columns in the source table to be captured`.  
  
 By default, all of the columns in the source table are identified as captured columns. If only a subset of columns need to be tracked, such as for privacy or performance reasons, use the *@captured_column_list* parameter to specify the subset of columns.  
  
 `A filegroup to contain the change table.`  
  
 By default, the change table is located in the default filegroup of the database. Database owners who want to control the placement of individual change tables can use the *@filegroup_name* parameter to specify a particular filegroup for the change table associated with the capture instance. The named filegroup must already exist. Generally, it is recommended that change tables be placed in a filegroup separate from source tables. See the `Enable a Table Specifying Filegroup Option` template for an example showing use of the *@filegroup_name* parameter.  
  
```tsql  
-- =========  
-- Enable a Table Specifying Filegroup Option Template  
-- =========  
USE MyDB  
GO  
  
EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'MyTable',  
@role_name     = N'MyRole',  
@filegroup_name = N'MyDB_CT',  
@supports_net_changes = 1  
GO  
```  
  
 `A role for controlling access to a change table.`  
  
 The purpose of the named role is to control access to the change data. The specified role can be an existing fixed server role or a database role. If the specified role does not already exist, a database role of that name is created automatically. Members of either the `sysadmin` or `db_owner` role have full access to the data in the change tables. All other users must have SELECT permission on all the captured columns of the source table. In addition, when a role is specified, users who are not members of either the `sysadmin` or `db_owner` role must also be members of the specified role.  
  
 If you do not want to use a gating role, explicitly set the *@role_name* parameter to NULL. See the `Enable a Table Without Using a Gating Role` template for an example of enabling a table without a gating role.  
  
```tsql  
-- =========  
-- Enable a Table Without Using a Gating Role template   
-- =========  
USE MyDB  
GO  
EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'MyTable',  
@role_name     = NULL,  
@supports_net_changes = 1  
GO  
  
```  
  
 `A function to query for net changes.`  
  
 A capture instance will always include a table valued function for returning all change table entries that occurred within a defined interval. This function is named by appending the capture instance name to "cdc.fn_cdc_get_all_changes_". For more information, see [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql).  
  
 If the parameter *@supports_net_changes* is set to 1, a net changes function is also generated for the capture instance. This function returns only one change for each distinct row changed in the interval specified in the call. For more information, see [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql).  
  
 To support net changes queries, the source table must have a primary key or unique index to uniquely identify rows. If a unique index is used, the name of the index must be specified using the *@index_name* parameter. The columns defined in the primary key or unique index must be included in the list of source columns to be captured.  
  
 See the `Enable a Table for All and Net Changes Queries` template for an example demonstrating the creation of a capture instance with both query functions.  
  
```tsql  
-- =============  
-- Enable a Table for All and Net Changes Queries template   
-- =============  
USE MyDB  
GO  
EXEC sys.sp_cdc_enable_table  
@source_schema = N'dbo',  
@source_name   = N'MyTable',  
@role_name     = N'MyRole',  
@supports_net_changes = 1  
GO  
```  
  
> [!NOTE]
>  If change data capture is enabled on a table with an existing primary key, and the *@index_name* parameter is not used to identify an alternative unique index, the change data capture feature will use the primary key. Subsequent changes to the primary key will not be allowed without first disabling change data capture for the table. This is true regardless of whether support for net changes queries was requested when change data capture was configured. If there is no primary key on a table at the time it is enabled for change data capture, the subsequent addition of a primary key is ignored by change data capture. Because change data capture will not use a primary key that is created after the table was enabled, the key and key columns can be removed without restrictions.  
  
## Disable Change Data Capture for a Table  
 Members of the `db_owner` fixed database role can remove a capture instance for individual source tables by using the stored procedure `sys.sp_cdc_disable_table`. To determine whether a source table is currently enabled for change data capture, examine the `is_tracked_by_cdc` column in the `sys.tables` catalog view. If there are no tables enabled for the database after the disabling takes place, the change data capture jobs are also removed.  
  
 If a change data capture-enabled table is dropped, change data capture metadata that is associated with the table is automatically removed.  
  
 See the Disable a Capture Instance for a Table template for an example of disabling a table.  
  
```tsql  
-- =====  
-- Disable a Capture Instance for a Table template   
-- =====  
USE MyDB  
GO  
EXEC sys.sp_cdc_disable_table  
@source_schema = N'dbo',  
@source_name   = N'MyTable',  
@capture_instance = N'dbo_MyTable'  
GO  
```  
  
## See Also  
 [Track Data Changes &#40;SQL Server&#41;](track-data-changes-sql-server.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../track-changes/about-change-data-capture-sql-server.md)   
 [Work with Change Data &#40;SQL Server&#41;](work-with-change-data-sql-server.md)   
 [Administer and Monitor Change Data Capture &#40;SQL Server&#41;](../track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
  
