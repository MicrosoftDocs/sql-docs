---
title: "Display Data and Log Space Information for a Database | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: supportability
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
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Display Data and Log Space Information for a Database
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  This topic describes how to display the data and log space information for a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Permission to execute **sp_spaceused** is granted to the **public** role. Only members of the **db_owner** fixed database role can specify the **@updateusage** parameter.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To display data and log space information for a database  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**.  
  
3.  Right-click a database, point to **Reports**, point to **Standard Reports,**, and then click **Disk Usage**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To display data and log space information for a database by using sp_spaceused  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example uses the [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md) system stored procedure to report disk space information for the `Vendor` table and its indexes.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_spaceused N'Purchasing.Vendor';  
GO  
```  
  
#### To display data and log space information for a database by querying sys.database_files  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example queries the [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog view to return specific information about the data and log files in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT file_id, name, type_desc, physical_name, size, max_size  
FROM sys.database_files ;  
GO  
  
```  
  
## See Also  
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)   
 [Delete Data or Log Files from a Database](../../relational-databases/databases/delete-data-or-log-files-from-a-database.md)  
  
  
