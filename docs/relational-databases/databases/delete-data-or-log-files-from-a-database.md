---
title: "Delete Data or Log Files from a Database | Microsoft Docs"
description: Learn how to delete data or log files in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], files"
  - "deleting files"
  - "removing files"
  - "removing data"
  - "data deletions [SQL Server]"
  - "file deletion [SQL Server]"
  - "deleting data"
ms.assetid: 0db4018c-ce2c-4ba1-bb29-1e4f3791c925
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Delete Data or Log Files from a Database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to delete data or log files in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To delete data or logs files from a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   A file must be empty before it can be deleted. For more information, see [Shrink a File](../../relational-databases/databases/shrink-a-file.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete data or log files from a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, right-click the database from which to delete the file, and then click **Properties**.  
  
3.  Select the **Files** page.  
  
4.  In the **Database files** grid, select the file to delete and then click **Remove**.  
  
5.  Click **OK**.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete data or log files from a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example removes the file `test1dat4`.  
  
 [!code-sql[DatabaseDDL#AlterDatabase4](../../relational-databases/databases/codesnippet/tsql/delete-data-or-log-files_1.sql)]  
  
 For more examples, see [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).  
  
## See Also  
 [Shrink a Database](../../relational-databases/databases/shrink-a-database.md)   
 [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)  
  
  
