---
title: "Rename Indexes"
description: Rename Indexes
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/17/2017"
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "renaming indexes"
  - "index names [SQL Server]"
  - "indexes [SQL Server], renaming"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Rename Indexes
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  This topic describes how to rename an index in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Renaming an index replaces the current index name with the new name that you provide. The specified name must be unique within the table or view. For example, two tables can have an index named **XPK_1**, but the same table cannot have two indexes named **XPK_1**. You cannot create an index with the same name as an existing disabled index. Renaming an index does not cause the index to be rebuilt.  

<a id="BeforeYouBegin"></a>

##  <a name="Restrictions"></a> Limitations and Restrictions  
 When you create a PRIMARY KEY or UNIQUE constraint on a table, an index with the same name as the constraint is automatically created for the table. Because index names must be unique within the table, you cannot create or rename an index to have the same name as an existing PRIMARY KEY or UNIQUE constraint on the table.  
  
  
<a id="Security"></a>
<a id="Permissions"></a>

## Permissions

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
    USE AdventureWorks2022;  
    GO  
    --Renames the IX_ProductVendor_VendorID index on the Purchasing.ProductVendor table to IX_VendorID.   
  
    EXEC sp_rename N'Purchasing.ProductVendor.IX_ProductVendor_VendorID', N'IX_VendorID', N'INDEX';   
    GO  
    ```  
  
 For more information, see  [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md).  
  
  
