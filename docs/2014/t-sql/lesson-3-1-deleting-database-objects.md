---
title: "Deleting Database Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting database objects"
ms.assetid: dbb94fdf-c85b-477b-8e84-f830d259bade
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Deleting Database Objects
  To remove all traces of this tutorial, you could just delete the database. However, in this topic, you will go through the steps to reverse every action you took doing the tutorial.  
  
### Removing permissions and objects  
  
1.  Before you delete objects, make sure you are in the correct database:  
  
    ```  
    USE TestData;  
    GO  
    ```  
  
2.  Use the `REVOKE` statement to remove execute permission for `Mary` on the stored procedure:  
  
    ```  
    REVOKE EXECUTE ON pr_Names FROM Mary;  
    GO  
  
    ```  
  
3.  Use the `DROP` statement to remove permission for `Mary` to access the `TestData` database:  
  
    ```  
    DROP USER Mary;  
    GO  
  
    ```  
  
4.  Use the `DROP` statement to remove permission for `Mary` to access this instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]:  
  
    ```  
    DROP LOGIN [<computer_name>\Mary];  
    GO  
  
    ```  
  
5.  Use the `DROP` statement to remove the store procedure `pr_Names`:  
  
    ```  
    DROP PROC pr_Names;  
    GO  
  
    ```  
  
6.  Use the `DROP` statement to remove the view `vw_Names`:  
  
    ```  
    DROP View vw_Names;  
    GO  
  
    ```  
  
7.  Use the `DELETE` statement to remove all rows from the `Products` table:  
  
    ```  
    DELETE FROM Products;  
    GO  
  
    ```  
  
8.  Use the `DROP` statement to remove the `Products` table:  
  
    ```  
    DROP Table Products;  
    GO  
  
    ```  
  
9. You cannot remove the `TestData` database while you are in the database; therefore, first switch context to another database, and then use the `DROP` statement to remove the `TestData` database:  
  
    ```  
    USE MASTER;  
    GO  
    DROP DATABASE TestData;  
    GO  
  
    ```  
  
 This concludes the Writing [!INCLUDE[tsql](../includes/tsql-md.md)] Statements tutorial. Remember, this tutorial is a brief overview and it does not describe all the options to the statements that are used. Designing and creating an efficient database structure and configuring secure access to the data requires a more complex database than that shown in this tutorial.  
  
## Return to SQL Server Tools Portal  
 [Tutorial: Writing Transact-SQL Statements](tutorial-writing-transact-sql-statements.md)  
  
## See Also  
 [REVOKE &#40;Transact-SQL&#41;](/sql/t-sql/statements/revoke-transact-sql)   
 [DROP USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-user-transact-sql)   
 [DROP LOGIN &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-login-transact-sql)   
 [DROP PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-procedure-transact-sql)   
 [DROP VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-view-transact-sql)   
 [DELETE &#40;Transact-SQL&#41;](/sql/t-sql/statements/delete-transact-sql)   
 [DROP TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-table-transact-sql)   
 [DROP DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-database-audit-specification-transact-sql)  
  
  
