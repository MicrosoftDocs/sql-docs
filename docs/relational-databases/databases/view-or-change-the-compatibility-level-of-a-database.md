---
title: "View or Change the Compatibility Level of a Database | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility levels [SQL Server], viewing"
  - "compatibility [SQL Server], databases"
  - "compatibility levels [SQL Server], changing"
ms.assetid: 579867ec-57cb-4cb8-af35-9688c1e9e15d
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View or Change the Compatibility Level of a Database
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes how to view or change the compatibility level of a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Before you change the compatibility level of a database, you should understand the impact of the change on your applications. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view or change the compatibility level of a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or change the compatibility level of a database  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, and then click **Properties**.  
  
     The **Database Properties** dialog box opens.  
  
4.  In the **Select a page** pane, click **Options**.  
  
     The current compatibility level is displayed in the **Compatibility level** list box.  
  
5.  To change the compatibility level, select a different option from the list. The choices are **SQL Server 2008 (100)**, **SQL Server 2012 (110)**, **SQL Server 2014 (120)**, **SQL Server 2016 (130)**, and **SQL Server 2017 (140)**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the compatibility level of a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example returns the compatibility level of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT compatibility_level  
FROM sys.databases WHERE name = 'AdventureWorks2012';  
GO  
```  
  
#### To change the compatibility level of a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example changes the compatibility level of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `120`, which is the compatibility level for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
```sql  
ALTER DATABASE AdventureWorks2012  
SET COMPATIBILITY_LEVEL = 120;  
GO  
```  
  
## See Also
 [ALTER DATABASE &#40;Transact-SQL&#41; Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
