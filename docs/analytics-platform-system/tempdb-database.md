---
title: Tempdb database
description: Tempdb database in Parallel Data Warehouse.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# tempdb database in Parallel Data Warehouse
**tempdb** is a SQL Server PDW system database that stores local temporary tables for user databases. Temporary tables are often used to improve query performance. For example, you can use a temporary table to modularize a script, and reuse computed data.  
  
For more information about system databases, see [System Databases](system-databases.md).  
  
## <a name="Basics"></a>Key Terms and Concepts  
*local temporary table*  
A *local temporary table* uses the # prefix before the table name and is a temporary table created by a local user session. Each session can only access the data in local temporary tables for its own session.  
  
Each session can view the metadata for local temporary tables in all sessions. For example, all sessions can view the metadata for all local temporary tables with the `SELECT * FROM tempdb.sys.tables` query.  
  
global temporary table  
*Global temporary tables*, supported in SQL Server with the ## syntax, are not supported in this release of SQL Server PDW.  
  
pdwtempdb  
**pdwtempdb** is the database that stores local temporary tables.  
  
PDW does not implement temporary tables by using the SQL Server**tempdb** database. Instead, PDW stores them in a database called pdwtempdb. This database exists on each Compute node and is invisible to the user through the PDW interfaces. In the Admin Console, on the storage page, you will see these accounted for in a PDW system database called **tempdb-sql**.  
  
tempdb  
**tempdb** is the SQL Server tempdb database. It uses minimal logging. SQL Server uses tempdb on the Compute nodes to store temporary tables that it needs in the course of performing SQL Server operations.  
  
SQL Server PDW drops tables from **tempdb** when:  
  
-   The DROP TABLE statement is executed.  
  
-   A session is disconnected. Only temporary tables for the session are dropped.  
  
-   The appliance is shutdown.  
  
-   The Control node has a cluster failover.  
  
## General Remarks  
SQL Server PDW performs the same operations on temporary tables and permanent tables unless explicitly stated otherwise. For example, the data in local temporary tables, just like permanent tables, is either distributed or replicated across the Compute nodes.  
  
## <a name="LimitationsRestrictions"></a>Limitations and Restrictions  
Limitations and restrictions on the SQL Server PDW**tempdb** database. You *cannot:*  
  
-   Create a global temporary table that begins with ##.  
  
-   Perform a backup or restore of **tempdb**.  
  
-   Modify permissions to **tempdb** with the **GRANT**, **DENY**, or **REVOKE** statements.  
  
-   Execute **DBCC SHRINKLOG** for **tempdb**tempdb.  
  
-   Perform DDL operations on **tempdb**. There are a couple exceptions to this. For details, see the following list of limitations and restrictions on local temporary tables.  
  
Limitations and restrictions on local temporary tables. You *cannot:*  
  
-   Rename a temporary table  
  
-   Create partitions, views, or nonclustered indexes on a temporary table. **ALTER INDEX** can be used to rebuild a clustered index for a table created with one.  
  
-   Modify permissions to temporary tables with the GRANT, DENY, or REVOKE statements.  
  
-   Run database console commands on temporary tables.  
  
-   Use the same name for two or more temporary tables within the same batch. If more than one local temporary table is used within a batch, each must have a unique name. If multiple sessions are running the same batch and creating the same local temporary table, SQL Server PDW internally appends a numeric suffix to the local temporary table name to maintain a unique name for each local temporary table.  
  
> [!NOTE]  
> You *can* create and update statistics on a temporary table.**ALTER INDEX** can be used to rebuild a clustered index.  
  
## Permissions  
Any user can create temporary objects in tempdb. Users can only access their own objects, unless they receive additional permissions. It is possible to revoke the connect permission to tempdb to prevent a user from using tempdb, but this is not recommended as some routine operations require the use of tempdb.  
  
## <a name="RelatedTasks"></a>Related Tasks  
  
|Tasks|Description|  
|---------|---------------|  
|Create a table in **tempdb**.|You can create a user temporary table with the CREATE TABLE and CREATE TABLE AS SELECT statements. For more information, see [CREATE TABLE](../t-sql/statements/create-table-azure-sql-data-warehouse.md) and [CREATE TABLE AS SELECT](../t-sql/statements/create-table-as-select-azure-sql-data-warehouse.md).|  
|View a list of existing tables in **tempdb**.|`SELECT * FROM tempdb.sys.tables;`|  
|View a list of existing columns in **tempdb**.|`SELECT * FROM tempdb.sys.columns;`|  
|View a list of existing objects in **tempdb**.|`SELECT * FROM tempdb.sys.objects;`|  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
