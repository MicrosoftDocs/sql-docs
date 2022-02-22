---
title: "View or Change the Compatibility Level of a Database"
description: Learn how to view or change the compatibility level of a database in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility levels [SQL Server], viewing"
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], changing"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View or change the compatibility level of a database

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

This article describes how to view or change the compatibility level of a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. 

> [!IMPORTANT]
> Before you change the compatibility level of a database, you should understand the impact of the change on your applications. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a>Use SQL Server Management Studio  
  
 To view or change the compatibility level of a database:
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, select the server name.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, and then select **Properties**.  
  
     The **Database Properties** dialog box opens.  
  
4.  In the **Select a page** pane, select **Options**. 

5.  The current compatibility level is displayed in the **Compatibility level** list box.  
  
    To change the compatibility level, select a different option from the list. The available options for different [!INCLUDE[ssde_md](../../includes/ssde_md.md)] versions are listed in the [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#supported-dbcompats) page.  

## <a name="TsqlProcedure"></a>Use Transact-SQL  
  
### View the compatibility level of a database
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example returns the compatibility level of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT compatibility_level  
FROM sys.databases WHERE name = 'AdventureWorks2012';  
GO  
```  
  
### Change the compatibility level of a database
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example changes the compatibility level of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `120`, which is the compatibility level for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET COMPATIBILITY_LEVEL = 120;  
GO  
```  
  
## Next steps
 - [ALTER DATABASE &#40;Transact-SQL&#41; Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
