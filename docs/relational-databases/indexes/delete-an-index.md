---
title: "Delete an Index"
description: Delete an Index
author: MikeRayMSFT
ms.author: mikeray
ms.date: "02/17/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "removing indexes"
  - "deleting indexes"
  - "dropping indexes"
  - "indexes [SQL Server], dropping"
  - "index deletions [SQL Server]"
ms.assetid: fd38a0ed-26c4-4c76-9ef7-e0a16147329d
monikerRange: "=azuresqldb-current||>=sql-server-2016"
---
# Delete an Index
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  This topic describes how to delete (drop) an index in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To delete an index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Indexes created as the result of a PRIMARY KEY or UNIQUE constraint cannot be deleted by using this method. Instead, the constraint must be deleted. To remove the constraint and corresponding index, use [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) with the DROP CONSTRAINT clause in [!INCLUDE[tsql](../../includes/tsql-md.md)]. For more information, see [Delete Primary Keys](../../relational-databases/tables/delete-primary-keys.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. This permission is granted by default to the **sysadmin** fixed server role and the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete an index by using Object Explorer  
  
1.  In Object Explorer, expand the database that contains the table on which you want to delete an index.  
  
2.  Expand the **Tables** folder.  
  
3.  Expand the table that contains the index you want to delete.  
  
4.  Expand the **Indexes** folder.  
  
5.  Right-click the index you want to delete and select **Delete**.  
  
6.  In the **Delete Object** dialog box, verify that the correct index is in the **Object to be deleted** grid and click **OK**.  
  
#### To delete an index using Table Designer  
  
1.  In Object Explorer, expand the database that contains the table on which you want to delete an index.  
  
2.  Expand the **Tables** folder.  
  
3.  Right-click the table that contains the index you want to delete and click Design.  
  
4.  On the **Table Designer** menu, click **Indexes/Keys**.  
  
5.  In the **Indexes/Keys** dialog box, select the index you want to delete.  
  
6.  Click **Delete**.  
  
7.  Click **Close**.  
  
8.  On the **File** menu, select **Save**_table_name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- delete the IX_ProductVendor_BusinessEntityID index  
    -- from the Purchasing.ProductVendor table  
    DROP INDEX IX_ProductVendor_BusinessEntityID   
        ON Purchasing.ProductVendor;  
    GO  
    ```  
  
 For more information, see [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md).  
  
  
