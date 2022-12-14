---
title: "Display data & log space info for a database"
description: Learn how to display the data and log space information for a database in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.date: "06/16/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], space"
  - "status information [SQL Server], space"
  - "displaying space information"
  - "disk space [SQL Server], displaying"
  - "databases [SQL Server], space used"
  - "viewing space information"
  - "space allocation [SQL Server], displaying"
  - "data space [SQL Server]"
ms.assetid: c7b99463-4bab-4e9b-9217-fcb0898dc757
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Display data and log space information for a database
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  This article describes how to display the data and log space information for a database in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

  
##  <a name="BeforeYouBegin"></a> Before you begin  
  
Permission to run **sp_spaceused** is granted to the **public** role. Only members of the **db_owner** fixed database role can specify the **\@updateusage** parameter.  
  
## <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To display data and log space information for a database  
  
1. In Object Explorer, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and then expand that instance.  
  
2. Expand **Databases**.  
  
3. Right-click a database, point to **Reports**, point to **Standard Reports**, and then select **Disk Usage**.  

## <a name="TsqlProcedure"></a> Using Transact-SQL

#### To display data and log space information for a database by using sp_spaceused
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. On the Standard toolbar, select **New Query**.  
  
3. Paste the following example into the query window and then select **Execute**. This example uses the [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md) system stored procedure to report disk space information for the entire database, including tables and indexes.  
  
   ```sql  
   USE AdventureWorks2012;  
   GO  
   EXEC sp_spaceused;  
   GO  
   ```  

#### To display data space used, by object and allocation unit, for a database
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. On the Standard toolbar, select **New Query**.  
  
3. Paste the following example into the query window and then select **Execute**. This example queries [object catalog views](../system-catalog-views/object-catalog-views-transact-sql.md) to report disk space usage per table and within each table per [allocation unit](../pages-and-extents-architecture-guide.md#IAM).  
  
   ```sql  
   SELECT
     t.object_id,
     OBJECT_NAME(t.object_id) ObjectName,
     sum(u.total_pages) * 8 Total_Reserved_kb,
     sum(u.used_pages) * 8 Used_Space_kb,
     u.type_desc,
     max(p.rows) RowsCount
   FROM
     sys.allocation_units u
     JOIN sys.partitions p on u.container_id = p.hobt_id

     JOIN sys.tables t on p.object_id = t.object_id

   GROUP BY
     t.object_id,
     OBJECT_NAME(t.object_id),
     u.type_desc
   ORDER BY
     Used_Space_kb desc,
     ObjectName;

   ```  

#### To display data and log space information for a database by querying sys.database_files  
  
1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2. On the Standard toolbar, select **New Query**.  
  
3. Paste the following example into the query window then select **Execute**. This example queries the [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog view to return specific information about the data and log files in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
   ```sql  
   USE AdventureWorks2012;  
   GO  
   SELECT file_id, name, type_desc, physical_name, size, max_size  
   FROM sys.database_files;  

   GO  
  
   ```  
  
## See also

 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [Add data or log files to a database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)   
 [Delete data or log files from a database](../../relational-databases/databases/delete-data-or-log-files-from-a-database.md)  
