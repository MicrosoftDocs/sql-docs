---
title: "Rename Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "renaming indexes"
  - "index names [SQL Server]"
  - "indexes [SQL Server], renaming"
ms.assetid: d3d612a1-ea1b-4d99-85d2-0a2ad54f4b0e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Rename Indexes
  This topic describes how to rename an index in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Renaming an index replaces the current index name with the new name that you provide. The specified name must be unique within the table or view. For example, two tables can have an index named **XPK_1**, but the same table cannot have two indexes named **XPK_1**. You cannot create an index with the same name as an existing disabled index. Renaming an index does not cause the index to be rebuilt.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To rename an index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 When you create a PRIMARY KEY or UNIQUE constraint on a table, an index with the same name as the constraint is automatically created for the table. Because index names must be unique within the table, you cannot create or rename an index to have the same name as an existing PRIMARY KEY or UNIQUE constraint on the table.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the index.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rename an index by using the Table Designer  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to rename an index.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Right-click the table on which you want to rename an index and select **Design**.  
  
4.  On the **Table Designer** menu, click **Indexes/Keys**.  
  
5.  Select the index you want to rename in the **Selected Primary/Unique Key or Index** text box.  
  
6.  In the grid, click **Name** and type a new name into the text box.  
  
7.  Click **Close**.  
  
8.  On the **File** menu, click **Save**_table_name_.  
  
#### To rename an index by using Object Explorer  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to rename an index.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to rename an index.  
  
4.  Click the plus sign to expand the **Indexes** folder.  
  
5.  Right-click the index you want to rename and select **Rename**.  
  
6.  Type the index's new name and press Enter.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To rename an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    --Renames the IX_ProductVendor_VendorID index on the Purchasing.ProductVendor table to IX_VendorID.   
  
    EXEC sp_rename N'Purchasing.ProductVendor.IX_ProductVendor_VendorID', N'IX_VendorID', N'INDEX';   
    GO  
    ```  
  
 For more information, see  [sp_rename &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-rename-transact-sql).  
  
  
