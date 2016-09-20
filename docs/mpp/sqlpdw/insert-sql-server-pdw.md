---
title: "INSERT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d26f24b7-5fe2-4ee8-99b0-6c054d513300
caps.latest.revision: 53
author: BarbKess
---
# INSERT (SQL Server PDW)
Appends one or more new rows to a table in SQL Server PDW. You can add one row by listing the row values or you can add multiple rows by inserting the results of a SELECT statement.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
INSERT INTO [ database_name . [ schema_name ] . | schema_name . ] table_name   
    [ ( column_name [ ,...n ] ) ]  
    {   
      VALUES ( { NULL | expression } [ ,...n ] )  
      | SELECT <select_criteria>  
    }  
    [ OPTION ( <query_option> [ ,...n ] ) ]  
[;]  
```  
  
## Arguments  
*database_name*  
The name of the database that is to receive the data. The default is the current database.  
  
*schema_name*  
The schema of the table or view.  
  
*table_name*  
The name of the table or view that is to receive the data.  
  
*column_name* [ ,...n ]  
A list of one or more columns in *table_name*. The list of columns corresponds to the list of values being inserted. The columns do not need to be listed in the same order as the table definition. If the list of columns is omitted, the list of values needs to contain all the columns in the order they are defined in the table definition.  
  
If the list of columns is present and a column is not in the list, SQL Server PDW must be able to insert a value based on the definition of the column; otherwise, the row cannot be loaded. For example, if a column is not listed in the column list and the column is nullable, SQL Server PDW automatically inserts NULL into the column.  
  
VALUES ( { NULL | *expression* } [ ,...n ] )  
The list of one or more data values to insert as a new row in the table. If a list of columns is specified, each column in the column list must contain one value and the values must be in the same order as the columns listed. If the column list is omitted, each column in the table must contain one value and the values must be in the same order as the column definition for the table.  
  
*expression*  
A constant or an expression. At this time, functions are not supported in *expression*.  
  
SELECT <select_criteria>  
A SELECT statement specifying source data that matches the column types to be inserted into the table. For more information, see [SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md).  
  
OPTION ( <query_option> [ ,...n ] )  
Specifies query options for the INSERT  statement. The possible options include a query label and one or more query join hints. For more information about the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/option-sql-server-pdw.md)  
  
## Permissions  
Requires **INSERT** permission on the target table or membership in the **db_datawriter** fixed database role. **INSERT** permission is denied to members of the **db_denydatawriter** fixed database role.  
  
## Error Handling  
If an INSERT statement has a value incompatible with the data type of the column, the statement fails and an error message is returned.  
  
If INSERT is loading multiple rows with SELECT, any violation that occurs from the values being loaded (such as an invalid value) stops the statement and no rows are loaded.  
  
## General Remarks  
Joins are supported with INSERT-SELECT, and are not supported for INSERT with values.  
  
When an INSERT-SELECT is based on a self-join, a table joined to itself, the INSERT operations use the order of columns listed in the FROM clause.  
  
For information about supported data formats, implicit data conversions, and explicit data conversions, see [Load Data With INSERT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/load-data-with-insert-sql-server-pdw.md).  
  
For distributed tables, SQL Server PDW performs inserts in parallel across the Compute nodes, and sequentially across the distributions within each Compute node. This provides the ability to rollback inserts in case an insert operation fails.  
  
## Limitations and Restrictions  
The INSERT statement cannot be used to modify or replace existing data; to modify column values in existing rows, use the Update statement.Examples  
  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement. To achieve a similar behavior, use [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md).  
  
## Locking  
INSERT (into replicated tables) requires an exclusive lock. INSERT into distributed table acquire SharedUpdate locks at some level on the table being changed.  
  
## Examples  
  
### A. Using a simple INSERT statement  
The following example inserts one row in the `Production.UnitMeasure` table. Because values for all columns are supplied and are listed in the same order as the columns in the table, the column names do not have to be specified in the column list*.*  
  
```  
USE AdventureWorksPDW2012;   
GO  
INSERT INTO DimCurrency   
VALUES (500, N'C1', N'Currency1');  
```  
  
### B. Inserting data that is not in same order as table columns  
The following example uses a column list to explicitly specify the values inserted into each column. The column order in the `DimCurrency` table is `CurrencyKey`, `CurrencyAlternateKey`, `CurrencyName`. However, the columns are not listed in that order in *column_list*.  
  
```  
USE AdventureWorksPDW2012;   
GO  
INSERT INTO DimCurrency (CurrencyName, CurrencyKey, CurrencyAlternateKey)  
VALUES (N'Currency1', 500, N'C1');  
```  
  
### C. Inserting data using the SELECT option  
The following example shows how to insert multiple rows of data using an INSERT statement with a SELECT option. The first `INSERT` statement uses a `SELECT` statement directly to retrieve data from the source table, and then to store the result set in the `EmployeeTitles` table.  
  
```  
CREATE TABLE EmployeeTitles  
( EmployeeKey   INT NOT NULL,  
  LastName     varchar(40) NOT NULL,  
  Title      varchar(50) NOT NULL  
);  
INSERT INTO EmployeeTitles  
    SELECT EmployeeKey, LastName, Title   
    FROM AdventureWorksPDW2012.dbo.DimEmployee  
    WHERE EndDate IS NULL;  
```  
  
### D. Specifying a label with the INSERT statement  
The following example shows the use of a label with an INSERT statement.  
  
```  
USE AdventureWorksPDW2012;   
  
INSERT INTO DimCurrency   
VALUES (500, N'C1', N'Currency1')  
OPTION ( LABEL = N'label1' );  
```  
  
### E. Using a label and a query hint with the INSERT statement  
This query shows the basic syntax for using a label and a query join hint with the INSERT statement. After the query is submitted to the Control node, SQL Server, running on the Compute nodes, will apply the hash join strategy when it generates the SQL Server query plan. For more information on join hints and how to use the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/option-sql-server-pdw.md).  
  
```  
USE AdventureWorksPDW2012;  
INSERT INTO DimCustomer (CustomerKey, CustomerAlternateKey, FirstName, MiddleName, LastName )   
SELECT ProspectiveBuyerKey, ProspectAlternateKey, FirstName, MiddleName, LastName  
FROM ProspectiveBuyer p JOIN DimGeography g ON p.PostalCode = g.PostalCode  
WHERE g.CountryRegionCode = 'FR'  
OPTION ( LABEL = 'Add French Prospects', HASH JOIN)  
;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[UPDATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/update-sql-server-pdw.md)  
[DELETE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/delete-sql-server-pdw.md)  
  
