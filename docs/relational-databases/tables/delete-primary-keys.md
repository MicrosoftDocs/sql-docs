---
description: "Delete Primary Keys"
title: "Delete Primary Keys"
ms.custom: ""
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "removing primary keys"
  - "deleting primary keys"
  - "primary keys [SQL Server], deleting"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete Primary Keys

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  You can delete (drop) a primary key in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. 

  When the primary key is deleted, the corresponding index is deleted. This may be the clustered index of the table, causing the table to become a heap. For more information, see [Heaps (Tables without Clustered Indexes)](../indexes/heaps-tables-without-clustered-indexes.md). Most tables should have a clustered index. To re-create the primary key, see [Create Primary Keys](create-primary-keys.md).
    
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
### To delete a primary key constraint using Object Explorer  
  
1.  In Object Explorer, expand the table that contains the primary key and then expand **Keys**.  
  
2.  Right-click the key and select **Delete**.  
  
3.  In the **Delete Object** dialog box, verify the correct key is specified and select **OK**.  
  
### To delete a primary key constraint using Table Designer  
  
1.  In Object Explorer, right-click the table with the primary key, and select **Design**.  
  
2.  In the table grid, right-click the row with the primary key and choose **Remove Primary Key** to toggle the setting from on to off.  
  
    > [!NOTE]  
    >  To undo this action, close the table without saving the changes. Deleting a primary key cannot be undone without losing all other changes made to the table.  
  
3.  On the **File** menu, select **Save** _table name_.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
### To delete a primary key constraint  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. The example first identifies the name of the primary key constraint and then deletes the constraint.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    -- Return the name of primary key.  
    SELECT name  
    FROM sys.key_constraints  
    WHERE type = 'PK' AND OBJECT_NAME(parent_object_id) = N'TransactionHistoryArchive';  
    GO  
    -- Delete the primary key constraint.  
    ALTER TABLE Production.TransactionHistoryArchive  
    DROP CONSTRAINT PK_TransactionHistoryArchive_TransactionID;   
    GO  
    ```  
 
## Next steps

 - [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
 - [sys.key_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-key-constraints-transact-sql.md)  
 - [Clustered and Nonclustered Indexes Described](../indexes/clustered-and-nonclustered-indexes-described.md)

  