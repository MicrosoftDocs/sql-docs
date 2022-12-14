---
description: "MSSQLSERVER_605"
title: "MSSQLSERVER_605 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "605 (Database Engine error)"
ms.assetid: d8d3a22e-1ff8-48a4-891f-4c8619437e24
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_605
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|605|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|WRONGPAGE|  
|Message Text|Attempt to fetch logical page %S_PGID in database %d failed. It belongs to allocation unit %I64d not to %I64d.|  
  
## Explanation  
This error generally signifies page or allocation corruption in the specified database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detects corruption when reading pages belonging to a table either by following the page linkages or by using the Index Allocation Map (IAM). All pages allocated to a table must belong to one of the allocation units associated with the table. If the allocation unit ID contained in the page header does not match an allocation unit ID associated with the table, this exception is raised. The first allocation unit ID listed in the error message is the ID present in the page header, and the second allocation unit value is the ID associated with the table.  
  
**Data Corruption Errors**  
  
A severity level of 21 indicates potential data corruption. Possible causes are a damaged page chain, a corrupt IAM, or an invalid entry in the [sys.objects](~/relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view for that object. These errors are often caused by hardware or disk device driver failure.  
  
**Transient Errors**  
  
A severity level of 12 indicates a potential transient error; that is, it occurs in the cache and does not indicate damage to data on disk. Transient 605 errors can be caused by the following conditions:  
  
-   The operating system prematurely notifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that an I/O operation has completed; the error message is displayed even though no actual data corruption exists.  
  
 - Running a query with the Optimizer hint NOLOCK or setting the transaction isolation level to READ UNCOMMITTED. When a query that is using NOLOCK or READ UNCOMMITTED transaction isolation level tries to read data that is being moved or changed by another user, a 605 error occurs. To verify that it is a transient 605 error, rerun the query later. 
  
In general, if the error occurs during data access but subsequent DBCC CHECKDB operations complete without error, the 605 error was probably transient.  
  
## User Action  
If the 605 error is not transient, the problem is severe and must be corrected by performing the following tasks:  
  
1.  Identify the tables associated with the allocation units specified in the message by running the following query. Replace `allocation_unit_id` with the allocation units specified in the error message.  
  
    ```sql  
    USE [database_name];  
  
    GO  
  
    SELECT au.allocation_unit_id, OBJECT_NAME(p.object_id) AS table_name, fg.name AS filegroup_name,  
  
    au.type_desc AS allocation_type, au.data_pages, partition_number  
  
    FROM sys.allocation_units AS au  
  
    JOIN sys.partitions AS p ON au.container_id = p.partition_id  
  
    JOIN sys.filegroups AS fg ON fg.data_space_id = au.data_space_id  
  
    WHERE au.allocation_unit_id = '<allocation_unit_id>' OR au.allocation_unit_id = '<allocation_unit_id>'  
  
    ORDER BY au.allocation_unit_id;  
  
    GO  
    ```
  
2.  Execute DBCC CHECKTABLE without a REPAIR clause on the table associated with the second allocation unit ID specified in the error message.  
  
3.  Execute DBCC CHECKDB without a REPAIR clause as soon as possible to determine the full extent of the corruption in the entire database.  
  
4.  Check the error log for other errors that often accompany a 605 error and examine the Windows Event Log for any system or hardware related issues. Fix any hardware-related problems that are contained in the logs.  
  
If the problem is not hardware related, perform one of the following tasks:  
  
1.  Restore the database from a known clean backup. You can leverage the page restore backup feature to restore just the damaged pages.  
  
2.  Run DBCC CHECKDB with the REPAIR clause recommended by the DBCC CHECKDB operation performed in step 3 to repair the corruption. If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider. Have the output from DBCC CHECKDB available for review.  
  
    > [!CAUTION]  
    > If you are not sure what effect DBCC CHECKDB with a REPAIR clause has on your data, contact your primary support provider before running this statement.  
  
## See Also  
[DBCC CHECKTABLE &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checktable-transact-sql.md)  
  
