---
description: "Increase the Size of a Database"
title: "Increase the Size of a Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [SQL Server], size"
  - "increasing database size"
  - "database size [SQL Server], increasing"
  - "size [SQL Server], databases"
ms.assetid: 14f4206d-3afa-4ba9-9849-23e81d63306d
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017"
---
# Increase the Size of a Database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to increase the size of a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The database is expanded by either increasing the size of an existing data or log file or by adding a new file to the database.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To increase the size of a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   You cannot add or remove a file while a BACKUP statement is running.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To increase the size of a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **Databases**, right-click the database to increase, and then click **Properties**.  
  
3.  In **Database Properties**, select the **Files** page.  
  
4.  To increase the size of an existing file, increase the value in the **Initial Size (MB)** column for the file. You must increase the size of the database by at least 1 megabyte.  
  
5.  To increase the size of the database by adding a new file, click **Add** and then enter the values for the new file. For more information, see [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md).  
  
6.  Click **OK**.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To increase the size of a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example increases the size of the file `test1dat3`.  
  
 [!code-sql[DatabaseDDL#AlterDatabase5](../../relational-databases/databases/codesnippet/tsql/increase-the-size-of-a-d_1.sql)]  
  
 For more examples, see [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).  
  
## See Also  
 [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)   
 [Shrink a Database](../../relational-databases/databases/shrink-a-database.md)  
  
  
