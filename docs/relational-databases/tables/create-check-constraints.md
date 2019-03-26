---
title: "Create Check Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table constraints [SQL Server]"
  - "attaching check constraints"
  - "columns [SQL Server], constraints"
  - "constraints [SQL Server], check"
  - "CHECK constraints, attaching"
ms.assetid: b8756304-9454-4d39-996a-64516831b7df
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Check Constraints
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  You can create a check constraint in a table to specify the data values that are acceptable in one or more columns in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To create a new check constraint using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permissions on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a new check constraint  
  
1.  In **Object Explorer**, expand the table to which you want to add a check constraint, right-click **Constraints** and click **New Constraint**.  
  
2.  In the **Check Constraints** dialog box, click in the **Expression** field and then click the ellipses **(...)**.  
  
3.  In the **Check Constraint Expression** dialog box, type the SQL expressions for the check constraint. For example, to limit the entries in the `SellEndDate` column of the `Product` table to a value that is either greater than or equal to the date in the `SellStartDate` column or is a NULL value, type:  
  
    ```  
    SellEndDate >= SellStartDate OR SellEndDate IS NULL  
    ```  
  
     Or, to require entries in the `zip` column to be 5 digits, type:  
  
    ```  
    zip LIKE '[0-9][0-9][0-9][0-9][0-9]'  
    ```  
  
    > [!NOTE]  
    >  Make sure to enclose any non-numeric constraint values in single quotation marks (').  
  
4.  Click **OK**.  
  
5.  In the **Identity** category, you can change the name of the check constraint and add a description (extended property) for the constraint.  
  
6.  In the **Table Designer** category, you can set when the constraint is enforced.  
  
    |**To:**|**Select Yes in the Following Fields:**|  
    |-------------|---------------------------------------------|  
    |Test the constraint on data that existed before you created the constraint|**Check Existing Data On Creation Or Enabling**|  
    |Enforce the constraint whenever a replication operation occurs on this table|**Enforce For Replication**|  
    |Enforce the constraint whenever a row of this table is inserted or updated|**Enforce For INSERTs And UPDATEs**|  
  
7.  Click **Close**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a new check constraint  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    ALTER TABLE dbo.DocExc   
       ADD ColumnD int NULL   
       CONSTRAINT CHK_ColumnD_DocExc   
       CHECK (ColumnD > 10 AND ColumnD < 50);  
    GO  
    -- Adding values that will pass the check constraint  
    INSERT INTO dbo.DocExc (ColumnD) VALUES (49);  
    GO  
    -- Adding values that will fail the check constraint  
    INSERT INTO dbo.DocExc (ColumnD) VALUES (55);  
    GO  
  
    ```  
  
 For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
###  <a name="TsqlExample"></a>  
