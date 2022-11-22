---
title: "View list of databases on SQL Server"
description: Learn how to view a list of databases on an instance of SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "current databases"
  - "databases currently on server [SQL Server]"
  - "database list [SQL Server]"
  - "viewing database list"
  - "displaying database list"
  - "databases [SQL Server], viewing"
  - "servers [SQL Server], databases listed on"
  - "listing databases"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: "seo-lt-2019, FY22Q2Fresh"
ms.date: "10/21/2021"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# View list of databases on SQL Server

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to view a list of databases on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
 
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 If the caller of `sys.databases` is not the owner of the database and the database is not `master` or `tempdb`, the minimum permissions required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the `master` database. The database to which the caller is connected can always be viewed in `sys.databases`.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To view a list of databases on an instance of SQL Server  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  To see a list of all databases on the instance, expand **Databases**.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To view a list of databases on an instance of SQL Server  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example returns a list of databases on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The list includes the names of the databases, their database IDs, and the dates when the databases were created.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT name, database_id, create_date  
FROM sys.databases;  
GO  
  
```  
  
## Next steps

 - [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 - [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
  
