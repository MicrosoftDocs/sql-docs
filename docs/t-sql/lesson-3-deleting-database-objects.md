---
title: "T-SQL Tutorial: Delete database objects | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2018"
ms.prod: sql
ms.technology: t-sql
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting database objects"
ms.assetid: ecf26dd5-4535-4ed6-86fc-c73f9d9dedec
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Lesson 3: Delete database objects
[!INCLUDE[tsql-appliesto-ss2008-all-md](../includes/tsql-appliesto-ss2008-all-md.md)]
This short lesson removes the objects that you created in Lesson 1 and Lesson 2, and then drops the database.  
  
Before you delete objects, make sure you are in the correct database:
  
  ```sql  
  USE TestData;  
  GO  
  ```  

## Revoke stored procedure permissions
  
Use the `REVOKE` statement to remove execute permission for `Mary` on the stored procedure:
  
  ```sql  
  REVOKE EXECUTE ON pr_Names FROM Mary;  
  GO  
  ```  
  
## Drop permissions

1. Use the `DROP` statement to remove permission for `Mary` to access the `TestData` database:
  
  ```sql  
  DROP USER Mary;  
  GO  
  ```  


2. Use the `DROP` statement to remove permission for `Mary` to access this instance of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)]:
  
  ```sql  
    DROP LOGIN [<computer_name>\Mary];  
    GO   
  ```  
  
3.   Use the `DROP` statement to remove the store procedure `pr_Names`:  
  
    ```sql  
    DROP PROC pr_Names;  
    GO  
    ```  
  
6.  Use the `DROP` statement to remove the view `vw_Names`:  
  
    ```sql  
    DROP VIEW vw_Names;  
    GO  
  
    ```  

## Delete table
  
1. Use the `DELETE` statement to remove all rows from the `Products` table:  
  
    ```sql  
    DELETE FROM Products;  
    GO  
    ```  
  
2.  Use the `DROP` statement to remove the `Products` table:  
  
    ```sql  
    DROP TABLE Products;  
    GO    
    ```  

## Remove database
  
You cannot remove the `TestData` database while you are in the database; therefore, first switch context to another database, and then use the `DROP` statement to remove the `TestData` database:  
  
  ```sql  
  USE MASTER;  
  GO  
  DROP DATABASE TestData;  
  GO   
  ```  
  
This concludes the Writing [!INCLUDE[tsql](../includes/tsql-md.md)] Statements tutorial. Remember, this tutorial is a brief overview and it does not describe all the options to the statements that are used. Designing and creating an efficient database structure and configuring secure access to the data requires a more complex database than that shown in this tutorial.  

  
  
