---
title: "Disable Check Constraints with INSERT and UPDATE Statements | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "CHECK constraints, disabling"
  - "constraints [SQL Server], disabling"
  - "disabling constraints"
  - "constraints [SQL Server], check"
ms.assetid: c7287ad1-50d2-4e80-bc0c-b5570f7e5f69
author: stevestein
ms.author: sstein
manager: craigg
---
# Disable Check Constraints with INSERT and UPDATE Statements
  You can disable a check constraint for INSERT and UPDATE transactions in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. After you disable the check constraints, future inserts or updates to the column will not be validated against the constraint conditions. Use this option if you know that new data will violate the existing constraint or if the constraint applies only to the data already in the database.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To disable a check constraint for INSERT and UPDATE statements, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To disable a check constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, expand the table with the constraint and then expand the **Constraints** folder.  
  
2.  Right-click the constraint and select **Modify**.  
  
3.  In the grid under **Table Designer**, click **Enforce For INSERTs And UPDATEs** and select **No** from the drop-down menu.  
  
4.  Click **Close**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To disable a check constraint for INSERT and UPDATE statements  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Purchasing.PurchaseOrderHeader  
    NOCHECK CONSTRAINT CK_PurchaseOrderHeader_Freight;   
    GO  
    ```  
  
 For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql).  
  
###  <a name="TsqlExample"></a>  
