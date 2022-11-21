---
description: "Create Check Constraints"
title: "Create Check Constraints"
ms.custom: ""
ms.date: "01/28/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table constraints [SQL Server]"
  - "attaching check constraints"
  - "columns [SQL Server], constraints"
  - "constraints [SQL Server], check"
  - "CHECK constraints, attaching"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Check Constraints
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  You can create a check constraint in a table to specify the data values that are acceptable in one or more columns in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. For more information on adding column constraints, see [ALTER TABLE column_constraint](../../t-sql/statements/alter-table-column-constraint-transact-sql.md).

  For more information, see [Unique Constraints and Check Constraints](unique-constraints-and-check-constraints.md).

## Remarks

 To query existing check constraints, use the [sys.check_constraints](../system-catalog-views/sys-check-constraints-transact-sql.md) system catalog view.
  
##  <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permissions on the table.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### Create a new check constraint  
  
1.  In **Object Explorer**, expand the table to which you want to add a check constraint, right-click **Constraints** and select **New Constraint**.  
  
2.  In the **Check Constraints** dialog box, select in the **Expression** field and then select the ellipses **(...)**.  
  
3.  In the **Check Constraint Expression** dialog box, type the SQL expressions for the check constraint. For example, to limit the entries in the `SellEndDate` column of the `Product` table to a value that is either greater than or equal to the date in the `SellStartDate` column or is a NULL value, type:  
  
    ```sql  
    SellEndDate >= SellStartDate OR SellEndDate IS NULL  
    ```  
  
     Or, to require entries in the `zip` column to be five digits, type:  
  
    ```sql  
    zip LIKE '[0-9][0-9][0-9][0-9][0-9]'  
    ```  
  
    > [!NOTE]  
    >  Make sure to enclose any non-numeric constraint values in single quotation marks (').  
  
4.  Select **OK**.  
  
5.  In the **Identity** category, you can change the name of the check constraint and add a description (extended property) for the constraint.  
  
6.  In the **Table Designer** category, you can set when the constraint is enforced.  
  
    |**To:**|**Select Yes in the Following Fields:**|  
    |-------------|---------------------------------------------|  
    |Test the constraint on data that existed before you created the constraint|**Check Existing Data On Creation Or Enabling**|  
    |Enforce the constraint whenever a replication operation occurs on this table|**Enforce For Replication**|  
    |Enforce the constraint whenever a row of this table is inserted or updated|**Enforce For INSERTs And UPDATEs**|  
  
7.  Select **Close**.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### Create a new check constraint  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. 

    First, create the constraint.  

    ```sql
    ALTER TABLE dbo.DocExc   
       ADD ColumnD int NULL   
       CONSTRAINT CHK_ColumnD_DocExc   
       CHECK (ColumnD > 10 AND ColumnD < 50);  
    GO  
    ```

    To test the constraint, first add values that will pass the check constraint. 

    ```sql
    INSERT INTO dbo.DocExc (ColumnD) VALUES (49);  
    ```

    Next, attempt to add values that will fail the check constraint.

    ```sql
    INSERT INTO dbo.DocExc (ColumnD) VALUES (55);  
    ```  

## See also

- [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
- [ALTER TABLE column_constraint](../../t-sql/statements/alter-table-column-constraint-transact-sql.md)
- [Unique Constraints and Check Constraints](unique-constraints-and-check-constraints.md)
- [Column properties](column-properties-general-page.md)

## Next steps

- [Add columns to a table](add-columns-to-a-table-database-engine.md)
- [Specify default values for columns](specify-default-values-for-columns.md)
- [Create unique constraints](create-unique-constraints.md)
