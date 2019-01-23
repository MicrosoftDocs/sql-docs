---
title: "MERGE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "MERGE"
  - "MERGE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "updating data [SQL Server]"
  - "modifying data [SQL Server], MERGE statement"
  - "MERGE statement [SQL Server]"
  - "adding data"
  - "DML [SQL Server], MERGE statement"
  - "table modifications [SQL Server], MERGE statement"
  - "data manipulation language [SQL Server], MERGE statement"
  - "inserting data"
ms.assetid: c17996d6-56a6-482f-80d8-086a3423eecc
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# MERGE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

Performs insert, update, or delete operations on a target table based on the results of a join with a source table. For example, you can synchronize two tables by inserting, updating, or deleting rows in one table based on differences found in the other table.  
  
 **Performance Tip:** The conditional behavior described for the MERGE statement works best when the two tables have a complex mixture of matching characteristics. For example, inserting a row if it does not exist, or updating the row if it does match. When simply updating one table based on the rows of another table, improved performance and scalability can be achieved with basic INSERT, UPDATE, and DELETE statements. For example:  
  
```  
INSERT tbl_A (col, col2)  
SELECT col, col2   
FROM tbl_B   
WHERE NOT EXISTS (SELECT col FROM tbl_A A2 WHERE A2.col = tbl_B.col);  
```  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
[ WITH <common_table_expression> [,...n] ]  
MERGE   
    [ TOP ( expression ) [ PERCENT ] ]   
    [ INTO ] <target_table> [ WITH ( <merge_hint> ) ] [ [ AS ] table_alias ]  
    USING <table_source>   
    ON <merge_search_condition>  
    [ WHEN MATCHED [ AND <clause_search_condition> ]  
        THEN <merge_matched> ] [ ...n ]  
    [ WHEN NOT MATCHED [ BY TARGET ] [ AND <clause_search_condition> ]  
        THEN <merge_not_matched> ]  
    [ WHEN NOT MATCHED BY SOURCE [ AND <clause_search_condition> ]  
        THEN <merge_matched> ] [ ...n ]  
    [ <output_clause> ]  
    [ OPTION ( <query_hint> [ ,...n ] ) ]      
;  
  
<target_table> ::=  
{   
    [ database_name . schema_name . | schema_name . ]  
  target_table  
}  
  
<merge_hint>::=  
{  
    { [ <table_hint_limited> [ ,...n ] ]  
    [ [ , ] INDEX ( index_val [ ,...n ] ) ] }  
}  
  
<table_source> ::=   
{  
    table_or_view_name [ [ AS ] table_alias ] [ <tablesample_clause> ]   
        [ WITH ( table_hint [ [ , ]...n ] ) ]   
  | rowset_function [ [ AS ] table_alias ]   
        [ ( bulk_column_alias [ ,...n ] ) ]   
  | user_defined_function [ [ AS ] table_alias ]  
  | OPENXML <openxml_clause>   
  | derived_table [ AS ] table_alias [ ( column_alias [ ,...n ] ) ]   
  | <joined_table>   
  | <pivoted_table>   
  | <unpivoted_table>   
}  
  
<merge_search_condition> ::=  
    <search_condition>  
  
<merge_matched>::=  
    { UPDATE SET <set_clause> | DELETE }  
  
<set_clause>::=  
SET  
  { column_name = { expression | DEFAULT | NULL }  
  | { udt_column_name.{ { property_name = expression  
                        | field_name = expression }  
                        | method_name ( argument [ ,...n ] ) }  
    }  
  | column_name { .WRITE ( expression , @Offset , @Length ) }  
  | @variable = expression  
  | @variable = column = expression  
  | column_name { += | -= | *= | /= | %= | &= | ^= | |= } expression  
  | @variable { += | -= | *= | /= | %= | &= | ^= | |= } expression  
  | @variable = column { += | -= | *= | /= | %= | &= | ^= | |= } expression  
  } [ ,...n ]   
  
<merge_not_matched>::=  
{  
    INSERT [ ( column_list ) ]   
        { VALUES ( values_list )  
        | DEFAULT VALUES }  
}  
  
<clause_search_condition> ::=  
    <search_condition>  
  
<search condition> ::=  
    MATCH(<graph_search_pattern>) | <search_condition_without_match> | <search_condition> AND <search_condition>
    
<search_condition_without_match> ::=
    { [ NOT ] <predicate> | ( <search_condition_without_match> ) 
    [ { AND | OR } [ NOT ] { <predicate> | ( <search_condition_without_match> ) } ]   
[ ,...n ]  

<predicate> ::=   
    { expression { = | < > | ! = | > | > = | ! > | < | < = | ! < } expression   
    | string_expression [ NOT ] LIKE string_expression   
  [ ESCAPE 'escape_character' ]   
    | expression [ NOT ] BETWEEN expression AND expression   
    | expression IS [ NOT ] NULL   
    | CONTAINS   
  ( { column | * } , '< contains_search_condition >' )   
    | FREETEXT ( { column | * } , 'freetext_string' )   
    | expression [ NOT ] IN ( subquery | expression [ ,...n ] )   
    | expression { = | < > | ! = | > | > = | ! > | < | < = | ! < }   
  { ALL | SOME | ANY} ( subquery )   
    | EXISTS ( subquery ) }   

<graph_search_pattern> ::=
    { <node_alias> { 
                      { <-( <edge_alias> )- } 
                    | { -( <edge_alias> )-> }
                    <node_alias> 
                   } 
    }
  
<node_alias> ::=
    node_table_name | node_table_alias 

<edge_alias> ::=
    edge_table_name | edge_table_alias

<output_clause>::=  
{  
    [ OUTPUT <dml_select_list> INTO { @table_variable | output_table }  
        [ (column_list) ] ]  
    [ OUTPUT <dml_select_list> ]  
}  
  
<dml_select_list>::=  
    { <column_name> | scalar_expression }   
        [ [AS] column_alias_identifier ] [ ,...n ]  
  
<column_name> ::=  
    { DELETED | INSERTED | from_table_name } . { * | column_name }  
    | $action  
```  
  
## Arguments  
 WITH \<common_table_expression>  
 Specifies the temporary named result set or view, also known as common table expression, defined within the scope of the MERGE statement. The result set is derived from a simple query and is referenced by the MERGE statement. For more information, see [WITH common_table_expression &#40;Transact-SQL&#41;](../../t-sql/queries/with-common-table-expression-transact-sql.md).  
  
 TOP ( *expression* ) [ PERCENT ]  
 Specifies the number or percentage of rows that are affected. *expression* can be either a number or a percentage of the rows. The rows referenced in the TOP expression are not arranged in any order. For more information, see [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
  
 The TOP clause is applied after the entire source table and the entire target table are joined and the joined rows that do not qualify for an insert, update, or delete action are removed. The TOP clause further reduces the number of joined rows to the specified value and the insert, update, or delete actions are applied to the remaining joined rows in an unordered fashion. That is, there is no order in which the rows are distributed among the actions defined in the WHEN clauses. For example, specifying TOP (10) affects 10 rows; of these rows, 7 may be updated and 3 inserted, or 1 may be deleted, 5 updated, and 4 inserted and so on.  
  
 Because the MERGE statement performs a full table scan of both the source and target tables, I/O performance can be affected when using the TOP clause to modify a large table by creating multiple batches. In this scenario, it is important to ensure that all successive batches target new rows.  
  
 *database_name*  
 Is the name of the database in which *target_table* is located.  
  
 *schema_name*  
 Is the name of the schema to which *target_table* belongs.  
  
 *target_table*  
 Is the table or view against which the data rows from \<table_source> are matched based on \<clause_search_condition>. *target_table* is the target of any insert, update, or delete operations specified by the WHEN clauses of the MERGE statement.  
  
 If *target_table* is a view, any actions against it must satisfy the conditions for updating views. For more information, see [Modify Data Through a View](../../relational-databases/views/modify-data-through-a-view.md).  
  
 *target_table* cannot be a remote table. *target_table* cannot have any rules defined on it.  
  
 [ AS ] *table_alias*  
 Is an alternative name used to reference a table.  
  
 USING \<table_source>  
 Specifies the data source that is matched with the data rows in *target_table* based on \<merge_search condition>. The result of this match dictates the actions to take by the WHEN clauses of the MERGE statement. \<table_source> can be a remote table or a derived table that accesses remote tables. 
  
 \<table_source> can be a derived table that uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] [table value constructor](../../t-sql/queries/table-value-constructor-transact-sql.md) to construct a table by specifying multiple rows.  
  
 For more information about the syntax and arguments of this clause, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md).  
  
 ON \<merge_search_condition>  
 Specifies the conditions on which \<table_source> is joined with *target_table* to determine where they match. 
  
> [!CAUTION]  
>  It is important to specify only the columns from the target table that are used for matching purposes. That is, specify columns from the target table that are compared to the corresponding column of the source table. Do not attempt to improve query performance by filtering out rows in the target table in the ON clause, such as by specifying `AND NOT target_table.column_x = value`. Doing so may return unexpected and incorrect results.  
  
 WHEN MATCHED THEN \<merge_matched>  
 Specifies that all rows of *target_table* that match the rows returned by \<table_source> ON \<merge_search_condition>, and satisfy any additional search condition, are either updated or deleted according to the \<merge_matched> clause.  
  
 The MERGE statement can have at most two WHEN MATCHED clauses. If two clauses are specified, then the first clause must be accompanied by an AND \<search_condition> clause. For any given row, the second WHEN MATCHED clause is only applied if the first is not. If there are two WHEN MATCHED clauses, then one must specify an UPDATE action and one must specify a DELETE action. If UPDATE is specified in the \<merge_matched> clause, and more than one row of \<table_source>matches a row in *target_table* based on \<merge_search_condition>, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. The MERGE statement cannot update the same row more than once, or update and delete the same row.  
  
 WHEN NOT MATCHED [ BY TARGET ] THEN \<merge_not_matched>  
 Specifies that a row is inserted into *target_table* for every row returned by \<table_source> ON \<merge_search_condition> that does not match a row in *target_table*, but does satisfy an additional search condition, if present. The values to insert are specified by the \<merge_not_matched> clause. The MERGE statement can have only one WHEN NOT MATCHED clause.  
  
 WHEN NOT MATCHED BY SOURCE THEN \<merge_matched>  
 Specifies that all rows of *target_table* that do not match the rows returned by \<table_source> ON \<merge_search_condition>, and that satisfy any additional search condition, are either updated or deleted according to the \<merge_matched> clause.  
  
 The MERGE statement can have at most two WHEN NOT MATCHED BY SOURCE clauses. If two clauses are specified, then the first clause must be accompanied by an AND \<clause_search_condition> clause. For any given row, the second WHEN NOT MATCHED BY SOURCE clause is only applied if the first is not. If there are two WHEN NOT MATCHED BY SOURCE clauses, then one must specify an UPDATE action and one must specify a DELETE action. Only columns from the target table can be referenced in \<clause_search_condition>.  
  
 When no rows are returned by \<table_source>, columns in the source table cannot be accessed. If the update or delete action specified in the \<merge_matched> clause references columns in the source table, error 207 (Invalid column name) is returned. For example, the clause `WHEN NOT MATCHED BY SOURCE THEN UPDATE SET TargetTable.Col1 = SourceTable.Col1` may cause the statement to fail because `Col1` in the source table is inaccessible.  
  
 AND \<clause_search_condition>  
 Specifies any valid search condition. For more information, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
 \<table_hint_limited>  
 Specifies one or more table hints that are applied on the target table for each of the insert, update, or delete actions that are performed by the MERGE statement. The WITH keyword and the parentheses are required.  
  
 NOLOCK and READUNCOMMITTED are not allowed. For more information about table hints, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
 Specifying the TABLOCK hint on a table that is the target of an INSERT statement has the same effect as specifying the TABLOCKX hint. An exclusive lock is taken on the table. When FORCESEEK is specified, it is applied to the implicit instance of the target table joined with the source table.  
  
> [!CAUTION]  
>  Specifying READPAST with WHEN NOT MATCHED [ BY TARGET ] THEN INSERT may result in INSERT operations that violate UNIQUE constraints.  
  
 INDEX ( index_val [ ,...n ] )  
 Specifies the name or ID of one or more indexes on the target table for performing an implicit join with the source table. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
 \<output_clause>  
 Returns a row for every row in *target_table* that is updated, inserted, or deleted, in no particular order. **$action** can be specified in the output clause. **$action** is a column of type **nvarchar(10)** that returns one of three values for each row: 'INSERT', 'UPDATE', or 'DELETE', according to the action that was performed on that row. For more information about the arguments of this clause, see [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md).  
  
 OPTION ( \<query_hint> [ ,...n ] )  
 Specifies that optimizer hints are used to customize the way the Database Engine processes the statement. For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
 \<merge_matched>  
 Specifies the update or delete action that is applied to all rows of *target_table* that do not match the rows returned by \<table_source> ON \<merge_search_condition>, and that satisfy any additional search condition.  
  
 UPDATE SET \<set_clause>  
 Specifies the list of column or variable names to be updated in the target table and the values with which to update them.  
  
 For more information about the arguments of this clause, see [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md). Setting a variable to the same value as a column is not permitted.  
  
 DELETE  
 Specifies that the rows matching rows in *target_table* are deleted.  
  
 \<merge_not_matched>  
 Specifies the values to insert into the target table.  
  
 (*column_list*)  
 Is a list of one or more columns of the target table in which to insert data. Columns must be specified as a single-part name or else the MERGE statement will fail. *column_list* must be enclosed in parentheses and delimited by commas.  
  
 VALUES ( *values_list*)  
 Is a comma-separated list of constants, variables, or expressions that return values to insert into the target table. Expressions cannot contain an EXECUTE statement.  
  
 DEFAULT VALUES  
 Forces the inserted row to contain the default values defined for each column.  
  
 For more information about this clause, see [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md).  
  
 \<search condition>  
 Specifies the search conditions used to specify \<merge_search_condition> or \<clause_search_condition>. For more information about the arguments for this clause, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  

 \<graph search pattern>  
 Specifies the graph match pattern. For more information about the arguments for this clause, see [MATCH &#40;Transact-SQL&#41;](../../t-sql/queries/match-sql-graph.md)
  
## Remarks  
 At least one of the three MATCHED clauses must be specified, but they can be specified in any order. A variable cannot be updated more than once in the same MATCHED clause.  
  
 Any insert, update, or delete actions specified on the target table by the MERGE statement are limited by any constraints defined on it, including any cascading referential integrity constraints. If IGNORE_DUP_KEY is set to ON for any unique indexes on the target table, MERGE ignores this setting.  
  
 The MERGE statement requires a semicolon (;) as a statement terminator. Error 10713 is raised when a MERGE statement is run without the terminator.  
  
 When used after MERGE, [@@ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/rowcount-transact-sql.md) returns the total number of rows inserted, updated, and deleted to the client.  
  
 MERGE is a fully reserved keyword when the database compatibility level is set to 100 or higher. The MERGE statement is available under both 90 and 100 database compatibility levels; however the keyword is not fully reserved when the database compatibility level is set to 90.  
  
 The **MERGE** statement should not be used when using queued updating replication. The **MERGE** and queued updating trigger are not compatible. Replace the **MERGE** statement with an insert or an update statement.  
  
## Trigger Implementation  
 For every insert, update, or delete action specified in the MERGE statement, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fires any corresponding AFTER triggers defined on the target table, but does not guarantee on which action to fire triggers first or last. Triggers defined for the same action honor the order you specify. For more information about setting trigger firing order, see [Specify First and Last Triggers](../../relational-databases/triggers/specify-first-and-last-triggers.md).  
  
 If the target table has an enabled INSTEAD OF trigger defined on it for an insert, update, or delete action performed by a MERGE statement, then it must have an enabled INSTEAD OF trigger for all of the actions specified in the MERGE statement.  
  
 If there are any INSTEAD OF UPDATE or INSTEAD OF DELETE triggers defined on *target_table*, the update or delete operations are not performed. Instead, the triggers fire and the **inserted** and **deleted** tables are populated accordingly.  
  
 If there are any INSTEAD OF INSERT triggers defined on *target_table*, the insert operation is not performed. Instead, the triggers fire and the **inserted** table is populated accordingly.  
  
## Permissions  
 Requires SELECT permission on the source table and INSERT, UPDATE, or DELETE permissions on the target table. For additional information, see the Permissions section in the [SELECT](../../t-sql/queries/select-transact-sql.md), [INSERT](../../t-sql/statements/insert-transact-sql.md), [UPDATE](../../t-sql/queries/update-transact-sql.md), and [DELETE](../../t-sql/statements/delete-transact-sql.md) topics.  
  
## Examples  
  
### A. Using MERGE to perform INSERT and UPDATE operations on a table in a single statement  
 A common scenario is updating one or more columns in a table if a matching row exists, or inserting the data as a new row if a matching row does not exist. This is usually done by passing parameters to a stored procedure that contains the appropriate UPDATE and INSERT statements. With the MERGE statement, you can perform both tasks in a single statement. The following example shows a stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)]database that contains both an INSERT statement and an UPDATE statement. The procedure is then modified to perform the equivalent operations by using a single MERGE statement.  
  
```sql  
CREATE PROCEDURE dbo.InsertUnitMeasure  
    @UnitMeasureCode nchar(3),  
    @Name nvarchar(25)  
AS   
BEGIN  
    SET NOCOUNT ON;  
-- Update the row if it exists.      
    UPDATE Production.UnitMeasure  
SET Name = @Name  
WHERE UnitMeasureCode = @UnitMeasureCode  
-- Insert the row if the UPDATE statement failed.  
IF (@@ROWCOUNT = 0 )  
BEGIN  
    INSERT INTO Production.UnitMeasure (UnitMeasureCode, Name)  
    VALUES (@UnitMeasureCode, @Name)  
END  
END;  
GO  
-- Test the procedure and return the results.  
EXEC InsertUnitMeasure @UnitMeasureCode = 'ABC', @Name = 'Test Value';  
SELECT UnitMeasureCode, Name FROM Production.UnitMeasure  
WHERE UnitMeasureCode = 'ABC';  
GO  
  
-- Rewrite the procedure to perform the same operations using the 
-- MERGE statement.  
-- Create a temporary table to hold the updated or inserted values 
-- from the OUTPUT clause.  
CREATE TABLE #MyTempTable  
    (ExistingCode nchar(3),  
     ExistingName nvarchar(50),  
     ExistingDate datetime,  
     ActionTaken nvarchar(10),  
     NewCode nchar(3),  
     NewName nvarchar(50),  
     NewDate datetime  
    );  
GO  
ALTER PROCEDURE dbo.InsertUnitMeasure  
    @UnitMeasureCode nchar(3),  
    @Name nvarchar(25)  
AS   
BEGIN  
    SET NOCOUNT ON;  
  
    MERGE Production.UnitMeasure AS target  
    USING (SELECT @UnitMeasureCode, @Name) AS source (UnitMeasureCode, Name)  
    ON (target.UnitMeasureCode = source.UnitMeasureCode)  
    WHEN MATCHED THEN   
        UPDATE SET Name = source.Name  
WHEN NOT MATCHED THEN  
    INSERT (UnitMeasureCode, Name)  
    VALUES (source.UnitMeasureCode, source.Name)  
    OUTPUT deleted.*, $action, inserted.* INTO #MyTempTable;  
END;  
GO  
-- Test the procedure and return the results.  
EXEC InsertUnitMeasure @UnitMeasureCode = 'ABC', @Name = 'New Test Value';  
EXEC InsertUnitMeasure @UnitMeasureCode = 'XYZ', @Name = 'Test Value';  
EXEC InsertUnitMeasure @UnitMeasureCode = 'ABC', @Name = 'Another Test Value';  
  
SELECT * FROM #MyTempTable;  
-- Cleanup   
DELETE FROM Production.UnitMeasure WHERE UnitMeasureCode IN ('ABC','XYZ');  
DROP TABLE #MyTempTable;  
GO  
```  
  
### B. Using MERGE to perform UPDATE and DELETE operations on a table in a single statement  
 The following example uses MERGE to update the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database on a daily basis, based on orders that are processed in the `SalesOrderDetail` table. The `Quantity` column of the `ProductInventory` table is updated by subtracting the number of orders placed each day for each product in the `SalesOrderDetail` table. If the number of orders for a product drops the inventory level of a product to 0 or less, the row for that product is deleted from the `ProductInventory` table.  
  
```sql  
CREATE PROCEDURE Production.usp_UpdateInventory  
    @OrderDate datetime  
AS  
MERGE Production.ProductInventory AS target  
USING (SELECT ProductID, SUM(OrderQty) FROM Sales.SalesOrderDetail AS sod  
    JOIN Sales.SalesOrderHeader AS soh  
    ON sod.SalesOrderID = soh.SalesOrderID  
    AND soh.OrderDate = @OrderDate  
    GROUP BY ProductID) AS source (ProductID, OrderQty)  
ON (target.ProductID = source.ProductID)  
WHEN MATCHED AND target.Quantity - source.OrderQty <= 0  
    THEN DELETE  
WHEN MATCHED   
    THEN UPDATE SET target.Quantity = target.Quantity - source.OrderQty,   
                    target.ModifiedDate = GETDATE()  
OUTPUT $action, Inserted.ProductID, Inserted.Quantity, 
    Inserted.ModifiedDate, Deleted.ProductID,  
    Deleted.Quantity, Deleted.ModifiedDate;  
GO  
  
EXECUTE Production.usp_UpdateInventory '20030501'  
```  
  
### C. Using MERGE to perform UPDATE and INSERT operations on a target table by using a derived source table  
 The following example uses MERGE to modify the `SalesReason` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database by either updating or inserting rows. When the value of `NewName` in the source table matches a value in the `Name` column of the target table, (`SalesReason`), the `ReasonType` column is updated in the target table. When the value of `NewName` does not match, the source row is inserted into the target table. The source table is a derived table that uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] table value constructor to specify multiple rows for the source table. For more information about using the table value constructor in a derived table, see [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md). The example also shows how to store the results of the OUTPUT clause in a table variable and then summarize the results of the MERGE statement by performing a simple select operation that returns the count of inserted and updated rows.  
  
```sql  
-- Create a temporary table variable to hold the output actions.  
DECLARE @SummaryOfChanges TABLE(Change VARCHAR(20));  
  
MERGE INTO Sales.SalesReason AS Target  
USING (VALUES ('Recommendation','Other'), ('Review', 'Marketing'), 
              ('Internet', 'Promotion'))  
       AS Source (NewName, NewReasonType)  
ON Target.Name = Source.NewName  
WHEN MATCHED THEN  
UPDATE SET ReasonType = Source.NewReasonType  
WHEN NOT MATCHED BY TARGET THEN  
INSERT (Name, ReasonType) VALUES (NewName, NewReasonType)  
OUTPUT $action INTO @SummaryOfChanges;  
  
-- Query the results of the table variable.  
SELECT Change, COUNT(*) AS CountPerChange  
FROM @SummaryOfChanges  
GROUP BY Change;  
```  
  
### D. Inserting the results of the MERGE statement into another table  
 The following example captures data returned from the OUTPUT clause of a MERGE statement and inserts that data into another table. The MERGE statement updates the `Quantity` column of the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database, based on orders that are processed in the `SalesOrderDetail` table. The example captures the rows that are updated and inserts them into another table that is used to track inventory changes.  
  
```sql  
CREATE TABLE Production.UpdatedInventory  
    (ProductID INT NOT NULL, LocationID int, NewQty int, PreviousQty int,  
     CONSTRAINT PK_Inventory PRIMARY KEY CLUSTERED (ProductID, LocationID));  
GO  
INSERT INTO Production.UpdatedInventory  
SELECT ProductID, LocationID, NewQty, PreviousQty   
FROM  
(    MERGE Production.ProductInventory AS pi  
     USING (SELECT ProductID, SUM(OrderQty)   
            FROM Sales.SalesOrderDetail AS sod  
            JOIN Sales.SalesOrderHeader AS soh  
            ON sod.SalesOrderID = soh.SalesOrderID  
            AND soh.OrderDate BETWEEN '20030701' AND '20030731'  
            GROUP BY ProductID) AS src (ProductID, OrderQty)  
     ON pi.ProductID = src.ProductID  
    WHEN MATCHED AND pi.Quantity - src.OrderQty >= 0   
        THEN UPDATE SET pi.Quantity = pi.Quantity - src.OrderQty  
    WHEN MATCHED AND pi.Quantity - src.OrderQty <= 0   
        THEN DELETE  
    OUTPUT $action, Inserted.ProductID, Inserted.LocationID, 
        Inserted.Quantity AS NewQty, Deleted.Quantity AS PreviousQty)  
 AS Changes (Action, ProductID, LocationID, NewQty, PreviousQty) 
 WHERE Action = 'UPDATE';  
GO  
```  

### E. Using MERGE to perform INSERT or UPDATE on a target edge table in a graph database
In this example, we create node tables `Person` and `City` and an edge table `livesIn`. We will use the MERGE statement on `livesIn` edge insert a new row if the edge does not already exist between a `Person` and `City`. If the edge already exists, then we will just update the StreetAddress attribute on the `livesIn` edge.

```sql
-- CREATE node and edge tables
CREATE TABLE Person
    (
        ID INTEGER PRIMARY KEY, 
        PersonName VARCHAR(100)
    )
AS NODE
GO

CREATE TABLE City
    (
        ID INTEGER PRIMARY KEY, 
        CityName VARCHAR(100), 
        StateName VARCHAR(100)
    )
AS NODE
GO

CREATE TABLE livesIn
    (
        StreetAddress VARCHAR(100)
    )
AS EDGE
GO

-- INSERT some test data into node and edge tables
INSERT INTO Person VALUES (1, 'Ron'), (2, 'David'), (3, 'Nancy')
GO

INSERT INTO City VALUES (1, 'Redmond', 'Washington'), (2, 'Seattle', 'Washington')
GO

INSERT livesIn SELECT P.$node_id, C.$node_id, c
FROM Person P, City C, (values (1,1, '123 Avenue'), (2,2,'Main Street')) v(a,b,c)
WHERE P.id = a AND C.id = b
GO

-- Use MERGE to update/insert edge data
CREATE OR ALTER PROCEDURE mergeEdge
    @PersonId integer,
    @CityId integer,
    @StreetAddress varchar(100)
AS
BEGIN
    MERGE livesIn
        USING ((SELECT @PersonId, @CityId, @StreetAddress) AS T (PersonId, CityId, StreetAddress)
                JOIN Person ON T.PersonId = Person.ID
                JOIN City ON T.CityId = City.ID)
        ON MATCH (Person-(livesIn)->City)
    WHEN MATCHED THEN
        UPDATE SET StreetAddress = @StreetAddress
    WHEN NOT MATCHED THEN
        INSERT ($from_id, $to_id, StreetAddress)
        VALUES (Person.$node_id, City.$node_id, @StreetAddress) ;
END
GO

-- Following will insert a new edge in the livesIn edge table
EXEC mergeEdge 3, 2, '4444th Avenue'
GO

-- Following will update the StreetAddress on the edge that connects Ron to Redmond
EXEC mergeEdge 1, 1, '321 Avenue'
GO

-- Verify that all the address were added/updated correctly
SELECT PersonName, CityName, StreetAddress
FROM Person , City , livesIn 
WHERE MATCH(Person-(livesIn)->city)
GO
```
  
## See Also  
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md)   
 [MERGE in Integration Services Packages](../../integration-services/control-flow/merge-in-integration-services-packages.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)   
 [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md)  
  
  

