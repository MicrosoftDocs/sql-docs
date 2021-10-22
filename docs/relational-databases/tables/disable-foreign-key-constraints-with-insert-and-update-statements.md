---
description: "Disable Foreign Key Constraints with INSERT and UPDATE Statements"
title: Disable Foreign Key Constraints in INSERT and UPDATE Statements
ms.custom: "seo-lt-2019"
ms.date: "10/21/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "constraints [SQL Server], foreign keys"
  - "foreign keys [SQL Server], disabling constraints"
  - "disabling constraints"
  - "UPDATE statement [SQL Server], foreign key constraints"
  - "INSERT statement [SQL Server], foreign key constraints"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disable Foreign Key Constraints with INSERT and UPDATE Statements
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  You can disable a foreign key constraint during INSERT and UPDATE transactions in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Use this option if you know that new data will not violate the existing constraint or if the constraint applies only to the data already in the database.  
  
##  <a name="Restrictions"></a> Limitations and Restrictions  
 After you disable these constraints, future inserts or updates to the column will not be validated against the constraint conditions.  
  
##  <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To disable a foreign key constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, expand the table with the constraint and then expand the **Keys** folder.  
  
2.  Right-click the constraint and select **Modify**.  
  
3.  In the grid under **Table Designer**, select **Enforce Foreign Key Constraint** and select **No** from the drop-down menu.  
  
4.  Select **Close**.  

5.  To re-enable the constraint when desired, reverse the above steps. Select **Enforce Foreign Key Constraint** and select **Yes** from the drop-down menu.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To disable a foreign key constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Purchasing.PurchaseOrderHeader  
    NOCHECK CONSTRAINT FK_PurchaseOrderHeader_Employee_EmployeeID;  
    GO  
    ```  

4.  To re-enable the constraint when desired, copy and paste the following example into the query window and select **Execute**.

    ```sql  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Purchasing.PurchaseOrderHeader  
    CHECK CONSTRAINT FK_PurchaseOrderHeader_Employee_EmployeeID;  
    GO  
    ``` 
  
## Next Steps

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  