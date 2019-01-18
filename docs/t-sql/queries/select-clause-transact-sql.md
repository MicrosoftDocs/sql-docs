---
title: "SELECT Clause (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SELECT Clause"
  - "SELECT_Clause_TSQL"
  - "DISTINCT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "parentheses [SQL Server]"
  - "identity columns [SQL Server], SELECT clause"
  - "SELECT clause"
  - "$IDENTITY keyword"
  - "user-defined types [SQL Server], invoking methods and properties"
  - "SELECT statement [SQL Server], processing orders"
  - "clauses [SQL Server], SELECT"
  - "$ROWGUID keyword"
  - "queries [SQL Server], results"
ms.assetid: 2616d800-4853-4cf1-af77-d32d68d8c2ef
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SELECT Clause (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Specifies the columns to be returned by the query.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SELECT [ ALL | DISTINCT ]  
[ TOP ( expression ) [ PERCENT ] [ WITH TIES ] ]   
<select_list>   
<select_list> ::=   
    {   
      *   
      | { table_name | view_name | table_alias }.*   
      | {  
          [ { table_name | view_name | table_alias }. ]  
               { column_name | $IDENTITY | $ROWGUID }   
          | udt_column_name [ { . | :: } { { property_name | field_name }   
            | method_name ( argument [ ,...n] ) } ]  
          | expression  
          [ [ AS ] column_alias ]   
         }  
      | column_alias = expression   
    } [ ,...n ]   
```  
  
## Arguments  
 **ALL**  
 Specifies that duplicate rows can appear in the result set. ALL is the default.  
  
 DISTINCT  
 Specifies that only unique rows can appear in the result set. Null values are considered equal for the purposes of the DISTINCT keyword.  
  
 TOP (*expression* ) [ PERCENT ] [ WITH TIES ]  
 Indicates that only a specified first set or percent of rows will be returned from the query result set. *expression* can be either a number or a percent of the rows.  
  
 For backward compatibility, using the TOP *expression* without parentheses in SELECT statements is supported, but we do not recommend it. For more information, see [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
  
\< select_list >
 The columns to be selected for the result set. The select list is a series of expressions separated by commas. The maximum number of expressions that can be specified in the select list is 4096.  
  
 \*  
 Specifies that all columns from all tables and views in the FROM clause should be returned. The columns are returned by table or view, as specified in the FROM clause, and in the order in which they exist in the table or view.  
  
 *table_name* | *view_name* | *table*_*alias*.*  
 Limits the scope of the \* to the specified table or view.  
  
 *column_name*  
 Is the name of a column to return. Qualify *column_name* to prevent an ambiguous reference, such as occurs when two tables in the FROM clause have columns with duplicate names. For example, the SalesOrderHeader and SalesOrderDetail tables in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database both have a column named ModifiedDate. If the two tables are joined in a query, the modified date of the SalesOrderDetail entries can be specified in the select list as SalesOrderDetail.ModifiedDate.  
  
 *expression*  
 Is a constant, function, any combination of column names, constants, and functions connected by an operator or operators, or a subquery.  
  
 $IDENTITY  
 Returns the identity column. For more information, see [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md), [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md), and [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).  
  
 If more than one table in the FROM clause has a column with the IDENTITY property, $IDENTITY must be qualified with the specific table name, such as T1.$IDENTITY.  
  
 $ROWGUID  
 Returns the row GUID column.  
  
 If there is more than one table in the FROM clause with the ROWGUIDCOL property, $ROWGUID must be qualified with the specific table name, such as T1.$ROWGUID.  
  
 *udt_column_name*  
 Is the name of a common language runtime (CLR) user-defined type column to return.  
  
> [!NOTE]  
>  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] returns user-defined type values in binary representation. To return user-defined type values in string or XML format, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) or [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
 { . | :: }  
 Specifies a method, property, or field of a CLR user-defined type. Use . for an instance (nonstatic) method, property, or field. Use :: for a static method, property, or field. To invoke a method, property, or field of a CLR user-defined type, you must have EXECUTE permission on the type.  
  
 *property_name*  
 Is a public property of *udt_column_name*.  
  
 *field_name*  
 Is a public data member of *udt_column_name*.  
  
 *method_name*  
 Is a public method of *udt_column_name* that takes one or more arguments. *method_name* cannot be a mutator method.  
  
 The following example selects the values for the `Location` column, defined as type `point`, from the `Cities` table, by invoking a method of the type called `Distance`:  
  
```  
CREATE TABLE dbo.Cities (  
     Name varchar(20),  
     State varchar(20),  
     Location point );  
GO  
DECLARE @p point (32, 23), @distance float;  
GO  
SELECT Location.Distance (@p)  
FROM Cities;  
```  
  
 *column_ alias*  
 Is an alternative name to replace the column name in the query result set. For example, an alias such as Quantity, or Quantity to Date, or Qty can be specified for a column named quantity.  
  
 Aliases are used also to specify names for the results of expressions, for example:  
  
 ```sql
 USE AdventureWorks2012;  
 GO  
 SELECT AVG(UnitPrice) AS [Average Price]  
 FROM Sales.SalesOrderDetail;
 ```  
  
 *column_alias* can be used in an ORDER BY clause. However, it cannot be used in a WHERE, GROUP BY, or HAVING clause. If the query expression is part of a DECLARE CURSOR statement, *column_alias* cannot be used in the FOR UPDATE clause.  
  
## Remarks  
 The length of data returned for **text** or **ntext** columns that are included in the select list is set to the smallest value of the following: the actual size of the **text** column, the default TEXTSIZE session setting, or the hard-coded application limit. To change the length of returned text for the session, use the SET statement. By default, the limit on the length of text data returned with a SELECT statement is 4,000 bytes.  
  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] raises exception 511 and rolls back the current running statement if either of the following behavior occurs:  
  
-   The SELECT statement produces a result row or an intermediate work table row exceeding 8,060 bytes.  
  
-   The DELETE, INSERT, or UPDATE statement tries an action on a row exceeding 8,060 bytes.  
  
 An error occurs if no column name is specified to a column created by a SELECT INTO or CREATE VIEW statement.  
  
## See Also  
 [SELECT Examples &#40;Transact-SQL&#41;](../../t-sql/queries/select-examples-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)  
  
  
