---
title: "Use the inserted and deleted Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "inserted tables"
  - "UPDATE statement [SQL Server], DML triggers"
  - "DELETE statement [SQL Server], DML triggers"
  - "INSTEAD OF triggers"
  - "deleted tables"
  - "INSERT statement [SQL Server], DML triggers"
  - "DML triggers, deleted or inserted tables"
ms.assetid: ed84567f-7b91-4b44-b5b2-c400bda4590d
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use the inserted and deleted Tables
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
  DML trigger statements use two special tables: the deleted table and the inserted tables. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically creates and manages these tables. You can use these temporary, memory-resident tables to test the effects of certain data modifications and to set conditions for DML trigger actions. You cannot directly modify the data in the tables or perform data definition language (DDL) operations on the tables, such as CREATE INDEX.  
  
 In DML triggers, the inserted and deleted tables are primarily used to perform the following:  
  
-   Extend referential integrity between tables.  
  
-   Insert or update data in base tables underlying a view.  
  
-   Test for errors and take action based on the error.  
  
-   Find the difference between the state of a table before and after a data modification and take actions based on that difference.  
  
 The deleted table stores copies of the affected rows during DELETE and UPDATE statements. During the execution of a DELETE or UPDATE statement, rows are deleted from the trigger table and transferred to the deleted table. The deleted table and the trigger table ordinarily have no rows in common.  
  
 The inserted table stores copies of the affected rows during INSERT and UPDATE statements. During an insert or update transaction, new rows are added to both the inserted table and the trigger table. The rows in the inserted table are copies of the new rows in the trigger table.  
  
 An update transaction is similar to a delete operation followed by an insert operation; the old rows are copied to the deleted table first, and then the new rows are copied to the trigger table and to the inserted table.  
  
 When you set trigger conditions, use the inserted and deleted tables appropriately for the action that fired the trigger. Although referencing the deleted table when testing an INSERT or the inserted table when testing a DELETE does not cause any errors, these trigger test tables do not contain any rows in these cases.  
  
> [!NOTE]  
>  If trigger actions depend on the number of rows a data modification effects, use tests (such as an examination of @@ROWCOUNT) for multirow data modifications (an INSERT, DELETE, or UPDATE based on a SELECT statement), and take appropriate actions.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] does not allow for **text**, **ntext**, or **image** column references in the inserted and deleted tables for AFTER triggers. However, these data types are included for backward compatibility purposes only. The preferred storage for large data is to use the **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types. Both AFTER and INSTEAD OF triggers support **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data in the inserted and deleted tables. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
 **An Example of Using the inserted Table in a Trigger to Enforce Business Rules**  
  
 Because CHECK constraints can reference only the columns on which the column-level or table-level constraint is defined, any cross-table constraints (in this case, business rules) must be defined as triggers.  
  
 The following example creates a DML trigger. This trigger checks to make sure the credit rating for the vendor is good when an attempt is made to insert a new purchase order into the `PurchaseOrderHeader` table. To obtain the credit rating of the vendor corresponding to the purchase order that was just inserted, the `Vendor` table must be referenced and joined with the inserted table. If the credit rating is too low, a message is displayed and the insertion does not execute. Note that this example does not allow for multirow data modifications. For more information, see [Create DML Triggers to Handle Multiple Rows of Data](../../relational-databases/triggers/create-dml-triggers-to-handle-multiple-rows-of-data.md).  
  
 [!code-sql[TriggerDDL#CreateTrigger3](../../relational-databases/triggers/codesnippet/tsql/use-the-inserted-and-del_1.sql)]  
  
## Using the inserted and deleted Tables in INSTEAD OF Triggers  
 The inserted and deleted tables passed to INSTEAD OF triggers defined on tables follow the same rules as the inserted and deleted tables passed to AFTER triggers. The format of the inserted and deleted tables is the same as the format of the table on which the INSTEAD OF trigger is defined. Each column in the inserted and deleted tables maps directly to a column in the base table.  
  
 The following rules regarding when an INSERT or UPDATE statement referencing a table with an INSTEAD OF trigger must supply values for columns are the same as if the table did not have an INSTEAD OF trigger:  
  
-   Values cannot be specified for computed columns or columns with a **timestamp** data type.  
  
-   Values cannot be specified for columns with an IDENTITY property, unless IDENTITY_INSERT is ON for that table. When IDENTITY_INSERT is ON, INSERT statements must supply a value.  
  
-   INSERT statements must supply values for all NOT NULL columns that do not have DEFAULT constraints.  
  
-   For any columns except computed, identity, or **timestamp** columns, values are optional for any column that allows nulls, or any NOT NULL column that has a DEFAULT definition.  
  
 When an INSERT, UPDATE, or DELETE statement references a view that has an INSTEAD OF trigger, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] calls the trigger instead of taking any direct action against any table. The trigger must use the information presented in the inserted and deleted tables to build any statements required to implement the requested action in the base tables, even when the format of the information in the inserted and deleted tables built for the view is different from the format of the data in the base tables.  
  
 The format of the inserted and deleted tables passed to an INSTEAD OF trigger defined on a view matches the select list of the SELECT statement defined for the view. For example:  
  
```  
USE AdventureWorks2012;  
GO  
CREATE VIEW dbo.EmployeeNames (BusinessEntityID, LName, FName)  
AS  
SELECT e.BusinessEntityID, p.LastName, p.FirstName  
FROM HumanResources.Employee AS e   
JOIN Person.Person AS p  
ON e.BusinessEntityID = p.BusinessEntityID;  
```  
  
 The result set for this view has three columns: an **int** column and two **nvarchar** columns. The inserted and deleted tables passed to an INSTEAD OF trigger defined on the view also have an **int** column named `BusinessEntityID`, an **nvarchar** column named `LName`, and an **nvarchar** column named `FName`.  
  
 The select list of a view can also contain expressions that do not directly map to a single base-table column. Some view expressions, such as a constant or function invocation, may not reference any columns and can be ignored. Complex expressions can reference multiple columns, yet the inserted and deleted tables have only one value for each inserted row. The same issues apply to simple expressions in a view if they reference a computed column that has a complex expression. An INSTEAD OF trigger on the view must handle these types of expressions.  
  
  
