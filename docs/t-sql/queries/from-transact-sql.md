---
title: FROM clause plus JOIN, APPLY, PIVOT (T-SQL)
description: FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "JOIN"
  - "FROM_TSQL"
  - "FROM"
  - "JOIN_TSQL"
  - "OUTER_JOIN_TSQL"
  - "INNER_JOIN_TSQL"
  - "CROSS_TSQL"
  - "CROSS_APPLY_TSQL"
  - "APPLY_TSQL"
  - "CROSS_JOIN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "OUTER APPLY operator"
  - "hints [SQL Server], FROM clause"
  - "SELECT statement [SQL Server], FROM clause"
  - "ISO syntax"
  - "DELETE statement [SQL Server], FROM clause"
  - "CROSS APPLY operator"
  - "FROM clause"
  - "APPLY operator"
  - "joins [SQL Server], FROM clause"
  - "UPDATE statement [SQL Server], FROM clause"
  - "derived tables"
ms.assetid: 36b19e68-94f6-4539-aeb1-79f5312e4263
author: VanMSFT
ms.author: vanto
ms.custom: ""
ms.date: "06/01/2019"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

In Transact-SQL, the FROM clause is available on the following statements:

- [DELETE](../statements/delete-transact-sql.md)
- [UPDATE](update-transact-sql.md)
- [SELECT](select-transact-sql.md)

The FROM clause is usually required on the SELECT statement. The exception is when no table columns are listed, and the only items listed are literals or variables or arithmetic expressions.

This article also discusses the following keywords that can be used on the FROM clause:

- [JOIN](../../relational-databases/performance/joins.md)
- APPLY
- [PIVOT](from-using-pivot-and-unpivot.md)

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
[ FROM { <table_source> } [ ,...n ] ]   
<table_source> ::=   
{  
    table_or_view_name [ FOR SYSTEM_TIME <system_time> ] [ [ AS ] table_alias ]   
        [ <tablesample_clause> ]   
        [ WITH ( < table_hint > [ [ , ]...n ] ) ]   
    | rowset_function [ [ AS ] table_alias ]   
        [ ( bulk_column_alias [ ,...n ] ) ]   
    | user_defined_function [ [ AS ] table_alias ]  
    | OPENXML <openxml_clause>   
    | derived_table [ [ AS ] table_alias ] [ ( column_alias [ ,...n ] ) ]   
    | <joined_table>   
    | <pivoted_table>   
    | <unpivoted_table>  
    | @variable [ [ AS ] table_alias ]  
    | @variable.function_call ( expression [ ,...n ] )   
        [ [ AS ] table_alias ] [ (column_alias [ ,...n ] ) ]     
}  
<tablesample_clause> ::=  
    TABLESAMPLE [SYSTEM] ( sample_number [ PERCENT | ROWS ] )   
        [ REPEATABLE ( repeat_seed ) ]   
  
<joined_table> ::=   
{  
    <table_source> <join_type> <table_source> ON <search_condition>   
    | <table_source> CROSS JOIN <table_source>   
    | left_table_source { CROSS | OUTER } APPLY right_table_source   
    | [ ( ] <joined_table> [ ) ]   
}  
<join_type> ::=   
    [ { INNER | { { LEFT | RIGHT | FULL } [ OUTER ] } } [ <join_hint> ] ]  
    JOIN  
  
<pivoted_table> ::=  
    table_source PIVOT <pivot_clause> [ [ AS ] table_alias ]  
  
<pivot_clause> ::=  
        ( aggregate_function ( value_column [ [ , ]...n ])   
        FOR pivot_column   
        IN ( <column_list> )   
    )   
  
<unpivoted_table> ::=  
    table_source UNPIVOT <unpivot_clause> [ [ AS ] table_alias ]  
  
<unpivot_clause> ::=  
    ( value_column FOR pivot_column IN ( <column_list> ) )   
  
<column_list> ::=  
    column_name [ ,...n ]   
  
<system_time> ::=  
{  
       AS OF <date_time>  
    |  FROM <start_date_time> TO <end_date_time>  
    |  BETWEEN <start_date_time> AND <end_date_time>  
    |  CONTAINED IN (<start_date_time> , <end_date_time>)   
    |  ALL  
}  
  
    <date_time>::=  
        <date_time_literal> | @date_time_variable  
  
    <start_date_time>::=  
        <date_time_literal> | @date_time_variable  
  
    <end_date_time>::=  
        <date_time_literal> | @date_time_variable  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
FROM { <table_source> [ ,...n ] }  
  
<table_source> ::=   
{  
    [ database_name . [ schema_name ] . | schema_name . ] table_or_view_name [ AS ] table_or_view_alias 
    [<tablesample_clause>]  
    | derived_table [ AS ] table_alias [ ( column_alias [ ,...n ] ) ]  
    | <joined_table>  
}  
  
<tablesample_clause> ::=
    TABLESAMPLE ( sample_number [ PERCENT ] ) -- Azure Synapse Analytics Dedicated SQL pool only  
 
<joined_table> ::=   
{  
    <table_source> <join_type> <table_source> ON search_condition   
    | <table_source> CROSS JOIN <table_source> 
    | left_table_source { CROSS | OUTER } APPLY right_table_source   
    | [ ( ] <joined_table> [ ) ]   
}  
  
<join_type> ::=   
    [ INNER ] [ <join hint> ] JOIN  
    | LEFT  [ OUTER ] JOIN  
    | RIGHT [ OUTER ] JOIN  
    | FULL  [ OUTER ] JOIN  
  
<join_hint> ::=   
    REDUCE  
    | REPLICATE  
    | REDISTRIBUTE  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
\<table_source>  
 Specifies a table, view, table variable, or derived table source, with or without an alias, to use in the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. Up to 256 table sources can be used in a statement, although the limit varies depending on available memory and the complexity of other expressions in the query. Individual queries may not support up to 256 table sources.  
  
> [!NOTE]  
>  Query performance may suffer with lots of tables referenced in a query. Compilation and optimization time is also affected by additional factors. These include the presence of indexes and indexed views on each \<table_source> and the size of the \<select_list> in the SELECT statement.  
  
 The order of table sources after the FROM keyword does not affect the result set that is returned. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns errors when duplicate names appear in the FROM clause.  
  
 *table_or_view_name*  
 Is the name of a table or view.  
  
 If the table or view exists in another database on the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use a fully qualified name in the form *database*.*schema*.*object_name*.  
  
 If the table or view exists outside the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]l, use a four-part name in the form *linked_server*.*catalog*.*schema*.*object*. For more information, see [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md). A four-part name that is constructed by using the [OPENDATASOURCE](../../t-sql/functions/opendatasource-transact-sql.md) function as the server part of the name can also be used to specify the remote table source. When OPENDATASOURCE is specified, *database_name* and *schema_name* may not apply to all data sources and is subject to the capabilities of the OLE DB provider that accesses the remote object.  
  
 [AS] *table_alias*  
 Is an alias for *table_source* that can be used either for convenience or to distinguish a table or view in a self-join or subquery. An alias is frequently a shortened table name used to refer to specific columns of the tables in a join. If the same column name exists in more than one table in the join, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that the column name be qualified by a table name, view name, or alias. The table name cannot be used if an alias is defined.  
  
 When a derived table, rowset or table-valued function, or operator clause (such as PIVOT or UNPIVOT) is used, the required *table_alias* at the end of the clause is the associated table name for all columns, including grouping columns, returned.  
  
 WITH (\<table_hint> )  
 Specifies that the query optimizer use an optimization or locking strategy with this table and for this statement. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
*rowset_function*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Specifies one of the rowset functions, such as OPENROWSET, that returns an object that can be used instead of a table reference. For more information about a list of rowset functions, see [Rowset Functions &#40;Transact-SQL&#41;](../functions/opendatasource-transact-sql.md).  
  
 Using the OPENROWSET and OPENQUERY functions to specify a remote object depends on the capabilities of the OLE DB provider that accesses the object.  
  
 *bulk_column_alias*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
 Is an optional alias to replace a column name in the result set. Column aliases are allowed only in SELECT statements that use the OPENROWSET function with the BULK option. When you use *bulk_column_alias*, specify an alias for every table column in the same order as the columns in the file.  
  
> [!NOTE]  
>  This alias overrides the NAME attribute in the COLUMN elements of an XML format file, if present.  
  
 *user_defined_function*  
 Specifies a table-valued function.  
  
OPENXML \<openxml_clause>  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Provides a rowset view over an XML document. For more information, see [OPENXML &#40;Transact-SQL&#41;](../../t-sql/functions/openxml-transact-sql.md).  
  
 *derived_table*  
 Is a subquery that retrieves rows from the database. *derived_table* is used as input to the outer query.  
  
 *derived_table* can use the [!INCLUDE[tsql](../../includes/tsql-md.md)] table value constructor feature to specify multiple rows. For example, `SELECT * FROM (VALUES (1, 2), (3, 4), (5, 6), (7, 8), (9, 10) ) AS MyTable(a, b);`. For more information, see [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md).  
  
 *column_alias*  
 Is an optional alias to replace a column name in the result set of the derived table. Include one column alias for each column in the select list, and enclose the complete list of column aliases in parentheses.  
  
 *table_or_view_name* FOR SYSTEM_TIME \<system_time>
gpplies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Specifies that a specific version of data is returned from the specified temporal table and its linked system-versioned history table  
  
### Tablesample clause
**Applies to:** SQL Server, SQL Database
Specifies that a sample of data from the table is returned. The sample may be approximate. This clause can be used on any primary or joined table in a SELECT or UPDATE statement. TABLESAMPLE cannot be specified with views.  
  
> [!NOTE]  
>  When you use TABLESAMPLE against databases that are upgraded to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the compatibility level of the database is set to 110 or higher, PIVOT is not allowed in a recursive common table expression (CTE) query. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
 SYSTEM  
 Is an implementation-dependent sampling method specified by ISO standards. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this is the only sampling method available and is applied by default. SYSTEM applies a page-based sampling method in which a random set of pages from the table is chosen for the sample, and all the rows on those pages are returned as the sample subset.  
  
 *sample_number*  
 Is an exact or approximate constant numeric expression that represents the percent or number of rows. When specified with PERCENT, *sample_number* is implicitly converted to a **float** value; otherwise, it is converted to **bigint**. PERCENT is the default.  
  
 PERCENT  
 Specifies that a *sample_number* percent of the rows of the table should be retrieved from the table. When PERCENT is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an approximate of the percent specified. When PERCENT is specified the *sample_number* expression must evaluate to a value from 0 to 100.  
  
 ROWS  
 Specifies that approximately *sample_number* of rows will be retrieved. When ROWS is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an approximation of the number of rows specified. When ROWS is specified, the *sample_number* expression must evaluate to an integer value greater than zero.  
  
 REPEATABLE  
 Indicates that the selected sample can be returned again. When specified with the same *repeat_seed* value, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return the same subset of rows as long as no changes have been made to any rows in the table. When specified with a different *repeat_seed* value, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will likely return some different sample of the rows in the table. The following actions to the table are considered changes: insert, update, delete, index rebuild or defragmentation, and database restore or attach.  
  
 *repeat_seed*  
 Is a constant integer expression used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate a random number. *repeat_seed* is **bigint**. If *repeat_seed* is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns a value at random. For a specific *repeat_seed* value, the sampling result is always the same if no changes have been applied to the table. The *repeat_seed* expression must evaluate to an integer greater than zero.  
  
### Tablesample clause
**Applies to:** [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]

 Specifies that a sample of data from the table is returned. The sample may be approximate. This clause can be used on any primary or joined table in a SELECT or UPDATE statement. TABLESAMPLE cannot be specified with views. 

 PERCENT  
 Specifies that a *sample_number* percent of the rows of the table should be retrieved from the table. When PERCENT is specified, [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] returns an approximate of the percent specified. When PERCENT is specified, the *sample_number* expression must evaluate to a value from 0 to 100.  


### Joined table 
A joined table is a result set that is the product of two or more tables. For multiple joins, use parentheses to change the natural order of the joins.  
  
### Join type
Specifies the type of join operation.  
  
 INNER  
 Specifies all matching pairs of rows are returned. Discards unmatched rows from both tables. When no join type is specified, this is the default.  
  
 FULL [ OUTER ]  
 Specifies that a row from either the left or right table that does not meet the join condition is included in the result set, and output columns that correspond to the other table are set to NULL. This is in addition to all rows typically returned by the INNER JOIN.  
  
 LEFT [ OUTER ]  
 Specifies that all rows from the left table not meeting the join condition are included in the result set, and output columns from the other table are set to NULL in addition to all rows returned by the inner join.  
  
 RIGHT [OUTER]  
 Specifies all rows from the right table not meeting the join condition are included in the result set, and output columns that correspond to the other table are set to NULL, in addition to all rows returned by the inner join.  
  
### Join hint  
For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)], specifies that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer use one join hint, or execution algorithm, per join specified in the query FROM clause. For more information, see [Join Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-join.md).  
  
 For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], these join hints apply to INNER joins on two distribution incompatible columns. They can improve query performance by restricting the amount of data movement that occurs during query processing. The allowable join hints for [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] are as follows:  
  
 REDUCE  
 Reduces the number of rows to be moved for the table on the right side of the join in order to make two distribution incompatible tables compatible. The REDUCE hint is also called a semi-join hint.  
  
 REPLICATE  
 Causes the values in the joining column from the table on the left side of the join to be replicated to all nodes. The table on the right is joined to the replicated version of those columns.  
  
 REDISTRIBUTE  
 Forces two data sources to be distributed on columns specified in the JOIN clause. For a distributed table, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will perform a shuffle move. For a replicated table, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will perform a trim move. To understand these move types, see the "DMS Query Plan Operations" section in the "Understanding Query Plans" topic in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)]. This hint can improve performance when the query plan is using a broadcast move to resolve a distribution incompatible join.  
  
 JOIN  
 Indicates that the specified join operation should occur between the specified table sources or views.  
  
 ON \<search_condition>  
 Specifies the condition on which the join is based. The condition can specify any predicate, although columns and comparison operators are frequently used, for example:  
  
```sql
SELECT p.ProductID, v.BusinessEntityID  
FROM Production.Product AS p   
JOIN Purchasing.ProductVendor AS v  
ON (p.ProductID = v.ProductID);  
  
```  
  
 When the condition specifies columns, the columns do not have to have the same name or same data type; however, if the data types are not the same, they must be either compatible or types that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can implicitly convert. If the data types cannot be implicitly converted, the condition must explicitly convert the data type by using the CONVERT function.  
  
 There can be predicates that involve only one of the joined tables in the ON clause. Such predicates also can be in the WHERE clause in the query. Although the placement of such predicates does not make a difference for INNER joins, they might cause a different result when OUTER joins are involved. This is because the predicates in the ON clause are applied to the table before the join, whereas the WHERE clause is semantically applied to the result of the join.  
  
 For more information about search conditions and predicates, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
 CROSS JOIN  
 Specifies the cross-product of two tables. Returns the same rows as if no WHERE clause was specified in an old-style, non-SQL-92-style join.  
  
 *left_table_source* { CROSS | OUTER } APPLY *right_table_source*  
 Specifies that the *right_table_source* of the APPLY operator is evaluated against every row of the *left_table_source*. This functionality is useful when the *right_table_source* contains a table-valued function that takes column values from the *left_table_source* as one of its arguments.  
  
 Either CROSS or OUTER must be specified with APPLY. When CROSS is specified, no rows are produced when the *right_table_source* is evaluated against a specified row of the *left_table_source* and returns an empty result set.  
  
 When OUTER is specified, one row is produced for each row of the *left_table_source* even when the *right_table_source* evaluates against that row and returns an empty result set.  
  
 For more information, see the Remarks section.  
  
 *left_table_source*  
 Is a table source as defined in the previous argument. For more information, see the Remarks section.  
  
 *right_table_source*  
 Is a table source as defined in the previous argument. For more information, see the Remarks section.  
  
### PIVOT clause

 *table_source* PIVOT \<pivot_clause>  
 Specifies that the *table_source* is pivoted based on the *pivot_column*. *table_source* is a table or table expression. The output is a table that contains all columns of the *table_source* except the *pivot_column* and *value_column*. The columns of the *table_source*, except the *pivot_column* and *value_column*, are called the grouping columns of the pivot operator. For more information about PIVOT and UNPIVOT, see [Using PIVOT and UNPIVOT](../../t-sql/queries/from-using-pivot-and-unpivot.md).  
  
 PIVOT performs a grouping operation on the input table with regard to the grouping columns and returns one row for each group. Additionally, the output contains one column for each value specified in the *column_list* that appears in the *pivot_column* of the *input_table*.  
  
 For more information, see the Remarks section that follows.  
  
 *aggregate_function*  
 Is a system or user-defined aggregate function that accepts one or more inputs. The aggregate function should be invariant to null values. An aggregate function invariant to null values does not consider null values in the group while it is evaluating the aggregate value.  
  
 The COUNT(*) system aggregate function is not allowed.  
  
 *value_column*  
 Is the value column of the PIVOT operator. When used with UNPIVOT, *value_column* cannot be the name of an existing column in the input *table_source*.  
  
 FOR *pivot_column*  
 Is the pivot column of the PIVOT operator. *pivot_column* must be of a type implicitly or explicitly convertible to **nvarchar()**. This column cannot be **image** or **rowversion**.  
  
 When UNPIVOT is used, *pivot_column* is the name of the output column that becomes narrowed from the *table_source*. There cannot be an existing column in *table_source* with that name.  
  
 IN (*column_list* )  
 In the PIVOT clause, lists the values in the *pivot_column* that will become the column names of the output table. The list cannot specify any column names that already exist in the input *table_source* that is being pivoted.  
  
 In the UNPIVOT clause, lists the columns in *table_source* that will be narrowed into a single *pivot_column*.  
  
 *table_alias*  
 Is the alias name of the output table. *pivot_table_alias* must be specified.  
  
 UNPIVOT \<unpivot_clause>  
 Specifies that the input table is narrowed from multiple columns in *column_list* into a single column called *pivot_column*. For more information about PIVOT and UNPIVOT, see [Using PIVOT and UNPIVOT](../../t-sql/queries/from-using-pivot-and-unpivot.md).  
  
AS OF \<date_time>  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Returns a table with single record for each row containing the values that were actual (current) at the specified point in time in the past. Internally, a union is performed between the temporal table and its history table and the results are filtered to return the values in the row that was valid at the point in time specified by the *\<date_time>* parameter. The value for a row is deemed valid if the *system_start_time_column_name* value is less than or equal to the *\<date_time>* parameter value and the *system_end_time_column_name* value is greater than the *\<date_time>* parameter value.   
  
FROM \<start_date_time> TO \<end_date_time>
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].
Returns a table with the values for all record versions that were active within the specified time range, regardless of whether they started being active before the *\<start_date_time>* parameter value for the FROM argument or ceased being active after the *\<end_date_time>* parameter value for the TO argument. Internally, a union is performed between the temporal table and its history table and the results are filtered to return the values for all row versions that were active at any time during the time range specified. Rows that became active exactly on the lower boundary defined by the FROM endpoint are included and rows that became active exactly on the upper boundary defined by the TO endpoint are not included.  
  
BETWEEN \<start_date_time> AND \<end_date_time>  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Same as above in the  **FROM \<start_date_time> TO \<end_date_time>** description, except it includes rows that became active on the upper boundary defined by the \<end_date_time> endpoint.  
  
CONTAINED IN (\<start_date_time> , \<end_date_time>)  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
Returns a table with the values for all record versions that were opened and closed within the specified time range defined by the two datetime values for the CONTAINED IN argument. Rows that became active exactly on the lower boundary or ceased being active exactly on the upper boundary are included.  
  
ALL  
Returns a table with the values from all rows from both the current table and the history table.  
  
## Remarks  
 The FROM clause supports the SQL-92-SQL syntax for joined tables and derived tables. SQL-92 syntax provides the INNER, LEFT OUTER, RIGHT OUTER, FULL OUTER, and CROSS join operators.  
  
 UNION and JOIN within a FROM clause are supported within views and in derived tables and subqueries.  
  
 A self-join is a table that is joined to itself. Insert or update operations that are based on a self-join follow the order in the FROM clause.  
  
 Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] considers distribution and cardinality statistics from linked servers that provide column distribution statistics, the REMOTE join hint is not required to force evaluating a join remotely. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query processor considers remote statistics and determines whether a remote-join strategy is appropriate. REMOTE join hint is useful for providers that do not provide column distribution statistics.  
  
## Using APPLY  
 Both the left and right operands of the APPLY operator are table expressions. The main difference between these operands is that the *right_table_source* can use a table-valued function that takes a column from the *left_table_source* as one of the arguments of the function. The *left_table_source* can include table-valued functions, but it cannot contain arguments that are columns from the *right_table_source*.  
  
The APPLY operator works in the following way to produce the table source for the FROM clause:  
  
1.  Evaluates *right_table_source* against each row of the *left_table_source* to produce rowsets.  
  
    The values in the *right_table_source* depend on *left_table_source*. *right_table_source* can be represented approximately this way: `TVF(left_table_source.row)`, where `TVF` is a table-valued function.  
  
2.  Combines the result sets that are produced for each row in the evaluation of *right_table_source* with the *left_table_source* by performing a UNION ALL operation.  
  
    The list of columns produced by the result of the APPLY operator is the set of columns from the *left_table_source* that is combined with the list of columns from the *right_table_source*.  
  
## Using PIVOT and UNPIVOT  
 The *pivot_column* and *value_column* are grouping columns that are used by the PIVOT operator. PIVOT follows the following process to obtain the output result set:  
  
1.  Performs a GROUP BY on its *input_table* against the grouping columns and produces one output row for each group.  
  
     The grouping columns in the output row obtain the corresponding column values for that group in the *input_table*.  
  
2.  Generates values for the columns in the column list for each output row by performing the following:  
  
    1.  Grouping additionally the rows generated in the GROUP BY in the previous step against the *pivot_column*.  
  
         For each output column in the *column_list*, selecting a subgroup that satisfies the condition:  
  
         `pivot_column = CONVERT(<data type of pivot_column>, 'output_column')`  
  
    2.  *aggregate_function* is evaluated against the *value_column* on this subgroup and its result is returned as the value of the corresponding *output_column*. If the subgroup is empty, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a null value for that *output_column*. If the aggregate function is COUNT and the subgroup is empty, zero (0) is returned.  

> [!NOTE]
> The column identifiers in the `UNPIVOT` clause follow the catalog collation. For [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], the collation is always `SQL_Latin1_General_CP1_CI_AS`. For [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] partially contained databases, the collation is always `Latin1_General_100_CI_AS_KS_WS_SC`. If the column is combined with other columns, then a collate clause (`COLLATE DATABASE_DEFAULT`) is required to avoid conflicts.   
  
 For more information about PIVOT and UNPIVOT including examples, see [Using PIVOT and UNPIVOT](../../t-sql/queries/from-using-pivot-and-unpivot.md).  
  
## Permissions  
 Requires the permissions for the DELETE, SELECT, or UPDATE statement.  
  
## Examples  
  
### A. Using a simple FROM clause  
 The following example retrieves the `TerritoryID` and `Name` columns from the `SalesTerritory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database.  
  
```sql    
SELECT TerritoryID, Name  
FROM Sales.SalesTerritory  
ORDER BY TerritoryID ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
TerritoryID Name                            
----------- ------------------------------  
1           Northwest                       
2           Northeast                       
3           Central                         
4           Southwest                       
5           Southeast                       
6           Canada                          
7           France                          
8           Germany                         
9           Australia                       
10          United Kingdom                  
(10 row(s) affected)  
```  
  
### B. Using the TABLOCK and HOLDLOCK optimizer hints  
 The following partial transaction shows how to place an explicit shared table lock on `Employee` and how to read the index. The lock is held throughout the whole transaction.  
  
```sql    
BEGIN TRAN  
SELECT COUNT(*)   
FROM HumanResources.Employee WITH (TABLOCK, HOLDLOCK) ;  
```  
  
### C. Using the SQL-92 CROSS JOIN syntax  
 The following example returns the cross product of the two tables `Employee` and `Department` in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. A list of all possible combinations of `BusinessEntityID` rows and all `Department` name rows are returned.  
  
```sql    
SELECT e.BusinessEntityID, d.Name AS Department  
FROM HumanResources.Employee AS e  
CROSS JOIN HumanResources.Department AS d  
ORDER BY e.BusinessEntityID, d.Name ;  
```  
  
### D. Using the SQL-92 FULL OUTER JOIN syntax  
 The following example returns the product name and any corresponding sales orders in the `SalesOrderDetail` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. It also returns any sales orders that have no product listed in the `Product` table, and any products with a sales order other than the one listed in the `Product` table.  
  
```sql  
-- The OUTER keyword following the FULL keyword is optional.  
SELECT p.Name, sod.SalesOrderID  
FROM Production.Product AS p  
FULL OUTER JOIN Sales.SalesOrderDetail AS sod  
ON p.ProductID = sod.ProductID  
ORDER BY p.Name ;  
```  
  
### E. Using the SQL-92 LEFT OUTER JOIN syntax  
 The following example joins two tables on `ProductID` and preserves the unmatched rows from the left table. The `Product` table is matched with the `SalesOrderDetail` table on the `ProductID` columns in each table. All products, ordered and not ordered, appear in the result set.  
  
```sql    
SELECT p.Name, sod.SalesOrderID  
FROM Production.Product AS p  
LEFT OUTER JOIN Sales.SalesOrderDetail AS sod  
ON p.ProductID = sod.ProductID  
ORDER BY p.Name ;  
```  
  
### F. Using the SQL-92 INNER JOIN syntax  
 The following example returns all product names and sales order IDs.  
  
```sql    
-- By default, SQL Server performs an INNER JOIN if only the JOIN   
-- keyword is specified.  
SELECT p.Name, sod.SalesOrderID  
FROM Production.Product AS p  
INNER JOIN Sales.SalesOrderDetail AS sod  
ON p.ProductID = sod.ProductID  
ORDER BY p.Name ;  
```  
  
### G. Using the SQL-92 RIGHT OUTER JOIN syntax  
 The following example joins two tables on `TerritoryID` and preserves the unmatched rows from the right table. The `SalesTerritory` table is matched with the `SalesPerson` table on the `TerritoryID` column in each table. All salespersons appear in the result set, whether or not they are assigned a territory.  
  
```sql    
SELECT st.Name AS Territory, sp.BusinessEntityID  
FROM Sales.SalesTerritory AS st   
RIGHT OUTER JOIN Sales.SalesPerson AS sp  
ON st.TerritoryID = sp.TerritoryID ;  
```  
  
### H. Using HASH and MERGE join hints  
 The following example performs a three-table join among the `Product`, `ProductVendor`, and `Vendor` tables to produce a list of products and their vendors. The query optimizer joins `Product` and `ProductVendor` (`p` and `pv`) by using a MERGE join. Next, the results of the `Product` and `ProductVendor` MERGE join (`p` and `pv`) are HASH joined to the `Vendor` table to produce (`p` and `pv`) and `v`.  
  
> [!IMPORTANT]  
>  After a join hint is specified, the INNER keyword is no longer optional and must be explicitly stated for an INNER JOIN to be performed.  
  
```sql    
SELECT p.Name AS ProductName, v.Name AS VendorName  
FROM Production.Product AS p   
INNER MERGE JOIN Purchasing.ProductVendor AS pv   
ON p.ProductID = pv.ProductID  
INNER HASH JOIN Purchasing.Vendor AS v  
ON pv.BusinessEntityID = v.BusinessEntityID  
ORDER BY p.Name, v.Name ;  
```  
  
### I. Using a derived table  
 The following example uses a derived table, a `SELECT` statement after the `FROM` clause, to return the first and last names of all employees and the cities in which they live.  
  
```sql    
SELECT RTRIM(p.FirstName) + ' ' + LTRIM(p.LastName) AS Name, d.City  
FROM Person.Person AS p  
INNER JOIN HumanResources.Employee e ON p.BusinessEntityID = e.BusinessEntityID   
INNER JOIN  
   (SELECT bea.BusinessEntityID, a.City   
    FROM Person.Address AS a  
    INNER JOIN Person.BusinessEntityAddress AS bea  
    ON a.AddressID = bea.AddressID) AS d  
ON p.BusinessEntityID = d.BusinessEntityID  
ORDER BY p.LastName, p.FirstName;  
```  
  
### J. Using TABLESAMPLE to read data from a sample of rows in a table  
 The following example uses `TABLESAMPLE` in the `FROM` clause to return approximately `10` percent of all the rows in the `Customer` table.  
  
```sql    
SELECT *  
FROM Sales.Customer TABLESAMPLE SYSTEM (10 PERCENT) ;  
```  
  
### K. Using APPLY  
The following example assumes that the following tables and table-valued function exist in the database:  

|Object Name|Column Names|      
|---|---|   
|Departments|DeptID, DivisionID, DeptName, DeptMgrID|      
|EmpMgr|MgrID, EmpID|     
|Employees|EmpID, EmpLastName, EmpFirstName, EmpSalary|  
|GetReports(MgrID)|EmpID, EmpLastName, EmpSalary|     
  
The `GetReports` table-valued function, returns the list of all employees that report directly or indirectly to the specified `MgrID`.  
  
The example uses `APPLY` to return all departments and all employees in that department. If a particular department does not have any employees, there will not be any rows returned for that department.  
  
```sql
SELECT DeptID, DeptName, DeptMgrID, EmpID, EmpLastName, EmpSalary  
FROM Departments d    
CROSS APPLY dbo.GetReports(d.DeptMgrID) ;  
```  
  
If you want the query to produce rows for those departments without employees, which will produce null values for the `EmpID`, `EmpLastName` and `EmpSalary` columns, use `OUTER APPLY` instead.  
  
```sql
SELECT DeptID, DeptName, DeptMgrID, EmpID, EmpLastName, EmpSalary  
FROM Departments d   
OUTER APPLY dbo.GetReports(d.DeptMgrID) ;  
```  
  
### L. Using CROSS APPLY  
The following example retrieves a snapshot of all query plans residing in the plan cache, by querying the `sys.dm_exec_cached_plans` dynamic management view to retrieve the plan handles of all query plans in the cache. Then the `CROSS APPLY` operator is specified to pass the plan handles to `sys.dm_exec_query_plan`. The XML Showplan output for each plan currently in the plan cache is in the `query_plan` column of the table that is returned.  
  
```sql
USE master;  
GO  
SELECT dbid, object_id, query_plan   
FROM sys.dm_exec_cached_plans AS cp   
CROSS APPLY sys.dm_exec_query_plan(cp.plan_handle);   
GO  
```  
  
### M. Using FOR SYSTEM_TIME  
  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later and [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
The following example uses the FOR SYSTEM_TIME AS OF date_time_literal_or_variable argument to return table rows that were actual (current) as of January 1, 2014.  
  
```sql
SELECT DepartmentNumber,   
    DepartmentName,   
    ManagerID,   
    ParentDepartmentNumber   
FROM DEPARTMENT  
FOR SYSTEM_TIME AS OF '2014-01-01'  
WHERE ManagerID = 5;
```  
  
The following example uses the FOR SYSTEM_TIME FROM date_time_literal_or_variable TO date_time_literal_or_variable argument to return all rows that were active during the period defined as starting with January 1, 2013 and ending with January 1, 2014, exclusive of the upper boundary.  
  
```sql
SELECT DepartmentNumber,   
    DepartmentName,   
    ManagerID,   
    ParentDepartmentNumber   
FROM DEPARTMENT  
FOR SYSTEM_TIME FROM '2013-01-01' TO '2014-01-01'  
WHERE ManagerID = 5;
```  
  
 The following example uses the FOR SYSTEM_TIME BETWEEN date_time_literal_or_variable AND date_time_literal_or_variable argument to return all rows that were active during the period defined as starting with January 1, 2013 and ending with January 1, 2014, inclusive of the upper boundary.  
  
```sql
SELECT DepartmentNumber,   
    DepartmentName,   
    ManagerID,   
    ParentDepartmentNumber   
FROM DEPARTMENT  
FOR SYSTEM_TIME BETWEEN '2013-01-01' AND '2014-01-01'  
WHERE ManagerID = 5;
```  
  
 The following example uses the FOR SYSTEM_TIME CONTAINED IN ( date_time_literal_or_variable, date_time_literal_or_variable ) argument to return all rows that were opened and closed during the period defined as starting with January 1, 2013 and ending with January 1, 2014.  
  
```sql
SELECT DepartmentNumber,   
    DepartmentName,   
    ManagerID,   
    ParentDepartmentNumber   
FROM DEPARTMENT  
FOR SYSTEM_TIME CONTAINED IN ( '2013-01-01', '2014-01-01' )  
WHERE ManagerID = 5;
```  
  
 The following example uses a variable rather than a literal to provide the date boundary values for the query.  
  
```sql
DECLARE @AsOfFrom datetime2 = dateadd(month,-12, sysutcdatetime());
DECLARE @AsOfTo datetime2 = dateadd(month,-6, sysutcdatetime());
  
SELECT DepartmentNumber,   
    DepartmentName,   
    ManagerID,   
    ParentDepartmentNumber   
FROM DEPARTMENT  
FOR SYSTEM_TIME FROM @AsOfFrom TO @AsOfTo  
WHERE ManagerID = 5;
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### N. Using the INNER JOIN syntax  
 The following example returns the `SalesOrderNumber`, `ProductKey`, and `EnglishProductName` columns from the `FactInternetSales` and `DimProduct` tables where the join key, `ProductKey`, matches in both tables. The `SalesOrderNumber` and `EnglishProductName` columns each exist in one of the tables only, so it is not necessary to specify the table alias with these columns, as is shown; these aliases are included for readability. The word **AS** before an alias name is not required but is recommended for readability and to conform to the ANSI standard.  
  
```sql
-- Uses AdventureWorks  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis 
INNER JOIN DimProduct AS dp  
    ON dp.ProductKey = fis.ProductKey;  
```  
  
 Since the `INNER` keyword is not required for inner joins, this same query could be written as:  
  
```sql
-- Uses AdventureWorks  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis 
JOIN DimProduct AS dp  
ON dp.ProductKey = fis.ProductKey;  
```  
  
 A `WHERE` clause could also be used with this query to limit results. This example limits results to `SalesOrderNumber` values higher than 'SO5000':  
  
```sql
-- Uses AdventureWorks  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis 
JOIN DimProduct AS dp  
    ON dp.ProductKey = fis.ProductKey  
WHERE fis.SalesOrderNumber > 'SO50000'  
ORDER BY fis.SalesOrderNumber;  
```  
  
### O. Using the LEFT OUTER JOIN and RIGHT OUTER JOIN syntax  
 The following example joins the `FactInternetSales` and `DimProduct` tables on the `ProductKey` columns. The left outer join syntax preserves the unmatched rows from the left (`FactInternetSales`) table. Since the `FactInternetSales` table does not contain any `ProductKey` values that do not match the `DimProduct` table, this query returns the same rows as the first inner join example above.  
  
```sql
-- Uses AdventureWorks  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM FactInternetSales AS fis 
LEFT OUTER JOIN DimProduct AS dp  
    ON dp.ProductKey = fis.ProductKey;  
```  
  
 This query could also be written without the `OUTER` keyword.  
  
 In right outer joins, the unmatched rows from the right table are preserved. The following example returns the same rows as the left outer join example above.  
  
```sql
-- Uses AdventureWorks  
  
SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
FROM DimProduct AS dp 
RIGHT OUTER JOIN FactInternetSales AS fis  
    ON dp.ProductKey = fis.ProductKey;  
```  
  
 The following query uses the `DimSalesTerritory` table as the left table in a left outer join. It retrieves the `SalesOrderNumber` values from the `FactInternetSales` table. If there are no orders for a particular `SalesTerritoryKey`, the query will return a NULL for the `SalesOrderNumber` for that row. This query is ordered by the `SalesOrderNumber` column, so that any NULLs in this column will appear at the top of the results.  
  
```sql
-- Uses AdventureWorks  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst 
LEFT OUTER JOIN FactInternetSales AS fis  
    ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
 This query could be rewritten with a right outer join to retrieve the same results:  
  
```sql
-- Uses AdventureWorks  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM FactInternetSales AS fis 
RIGHT OUTER JOIN DimSalesTerritory AS dst  
    ON fis.SalesTerritoryKey = dst.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
### P. Using the FULL OUTER JOIN syntax  
 The following example demonstrates a full outer join, which returns all rows from both joined tables but returns NULL for values that do not match from the other table.  
  
```sql
-- Uses AdventureWorks  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst 
FULL OUTER JOIN FactInternetSales AS fis  
    ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
 This query could also be written without the `OUTER` keyword.  
  
```sql
-- Uses AdventureWorks  
  
SELECT dst.SalesTerritoryKey, dst.SalesTerritoryRegion, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst 
FULL JOIN FactInternetSales AS fis  
    ON dst.SalesTerritoryKey = fis.SalesTerritoryKey  
ORDER BY fis.SalesOrderNumber;  
```  
  
### Q. Using the CROSS JOIN syntax  
 The following example returns the cross-product of the `FactInternetSales` and `DimSalesTerritory` tables. A list of all possible combinations of `SalesOrderNumber` and  `SalesTerritoryKey` are returned. Notice the absence of the `ON` clause in the cross join query.  
  
```sql
-- Uses AdventureWorks  
  
SELECT dst.SalesTerritoryKey, fis.SalesOrderNumber  
FROM DimSalesTerritory AS dst 
CROSS JOIN FactInternetSales AS fis  
ORDER BY fis.SalesOrderNumber;  
```  
  
### R. Using a derived table  
 The following example uses a derived table (a `SELECT` statement after the `FROM` clause) to return the `CustomerKey` and `LastName` columns of all customers in the `DimCustomer` table with `BirthDate` values later than January 1, 1970 and the last name 'Smith'.  
  
```sql
-- Uses AdventureWorks  
  
SELECT CustomerKey, LastName  
FROM  
   (SELECT * FROM DimCustomer  
    WHERE BirthDate > '01/01/1970') AS DimCustomerDerivedTable  
WHERE LastName = 'Smith'  
ORDER BY LastName;  
```  
  
### S. REDUCE join hint example  
 The following example uses the `REDUCE` join hint to alter the processing of the derived table within the query. When using the `REDUCE` join hint in this query, the `fis.ProductKey` is projected, replicated and made distinct, and then joined to `DimProduct` during the shuffle of `DimProduct` on `ProductKey`. The resulting derived table is distributed on `fis.ProductKey`.  
  
```sql
-- Uses AdventureWorks  
  
EXPLAIN SELECT SalesOrderNumber  
FROM  
   (SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
    FROM DimProduct AS dp   
      INNER REDUCE JOIN FactInternetSales AS fis   
          ON dp.ProductKey = fis.ProductKey  
   ) AS dTable  
ORDER BY SalesOrderNumber;  
```  
  
### T. REPLICATE join hint example  
 This next example shows the same query as the previous example, except that a `REPLICATE` join hint is used instead of the `REDUCE` join hint. Use of the `REPLICATE` hint causes the values in the `ProductKey` (joining) column from the `FactInternetSales` table to be replicated to all nodes. The `DimProduct` table is joined to the replicated version of those values.  
  
```sql
-- Uses AdventureWorks  
  
EXPLAIN SELECT SalesOrderNumber  
FROM  
   (SELECT fis.SalesOrderNumber, dp.ProductKey, dp.EnglishProductName  
    FROM DimProduct AS dp   
      INNER REPLICATE JOIN FactInternetSales AS fis  
          ON dp.ProductKey = fis.ProductKey  
   ) AS dTable  
ORDER BY SalesOrderNumber;  
```  
  
### U. Using the REDISTRIBUTE hint to guarantee a Shuffle move for a distribution incompatible join  
 The following query uses the REDISTRIBUTE query hint on a distribution incompatible join. This guarantees the query optimizer will use a Shuffle move in the query plan. This also guarantees the query plan will not use a Broadcast move which moves a distributed table to a replicated table.  
  
 In the following example, the REDISTRIBUTE hint forces a Shuffle move on the FactInternetSales table because ProductKey is the distribution column for DimProduct, and is not the distribution column for FactInternetSales.  
  
```sql
-- Uses AdventureWorks  
  
EXPLAIN  
SELECT dp.ProductKey, fis.SalesOrderNumber, fis.TotalProductCost  
FROM DimProduct AS dp 
INNER REDISTRIBUTE JOIN FactInternetSales AS fis  
    ON dp.ProductKey = fis.ProductKey;  
```  

### V. Using TABLESAMPLE to read data from a sample of rows in a table  
 The following example uses `TABLESAMPLE` in the `FROM` clause to return approximately `10` percent of all the rows in the `Customer` table.  
  
```sql    
SELECT *  
FROM Sales.Customer TABLESAMPLE SYSTEM (10 PERCENT) ;
```
  
## See Also  
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [OPENQUERY &#40;Transact-SQL&#41;](../../t-sql/functions/openquery-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)
