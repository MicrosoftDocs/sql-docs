---
description: "Disable Check Constraints with INSERT and UPDATE Statements"
title: "Disable check constraints with INSERT and UPDATE statements"

ms.custom: ""
ms.date: "11/24/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "CHECK constraints, disabling"
  - "constraints [SQL Server], disabling"
  - "disabling constraints"
  - "constraints [SQL Server], check"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disable Check Constraints with INSERT and UPDATE Statements
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can disable a check constraint for `INSERT` and `UPDATE` transactions in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. After you disable the check constraints, future inserts or updates to the column will not be validated against the constraint conditions. Use this option if you know that new data will violate the existing constraint or if the constraint applies only to the data already in the database.

For more information, see [Check Constraints](unique-constraints-and-check-constraints.md#Check).

> [!Note] 
> Check constraints are enabled and disabled with an `ALTER TABLE` statement, which always requires [a schema modification lock (`Sch-M`)](../sql-server-transaction-locking-and-row-versioning-guide.md#schema). Sch-M locks prevent concurrent access to the table. For more information, see [Locks and ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md#locks-and-alter-table).
   
###  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To disable a check constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, expand the table with the constraint and then expand the **Constraints** folder.  
  
2.  Right-click the constraint and select **Modify**. 
  
3.  In the grid under **Table Designer**, click **Enforce For INSERTs And UPDATEs** and select **No** from the drop-down menu.  
  
4.  Click **Close**.  
  
## <a name="TsqlExample"></a><a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To disable a check constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window and click **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Purchasing.PurchaseOrderHeader  
    NOCHECK CONSTRAINT CK_PurchaseOrderHeader_Freight;   
    GO  
    ```  
