---
title: "MERGE (Transact-SQL)"
description: MERGE (Transact-SQL)
author: mstehrani
ms.author: emtehran
ms.reviewer: wiassaf
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "MERGE"
  - "MERGE_TSQL"
helpviewer_keywords:
  - "updating data [SQL Server]"
  - "modifying data [SQL Server], MERGE statement"
  - "MERGE statement [SQL Server]"
  - "adding data"
  - "DML [SQL Server], MERGE statement"
  - "table modifications [SQL Server], MERGE statement"
  - "data manipulation language [SQL Server], MERGE statement"
  - "inserting data"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||azure-sqldw-latest"
---
# MERGE (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb-asa.md)]

Runs insert, update, or delete operations on a target table from the results of a join with a source table. For example, synchronize two tables by inserting, updating, or deleting rows in one table based on differences found in the other table.

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
> [!NOTE]
> Change the product version selector for important content on MERGE specific to Azure Synapse Analytics. To change document version to Azure Synapse Analytics: [Azure Synapse Analytics](merge-transact-sql.md?view=azure-sqldw-latest&preserve-view=true).

**Performance Tip:** The conditional behavior described for the MERGE statement works best when the two tables have a complex mixture of matching characteristics. For example, inserting a row if it doesn't exist, or updating a row if it matches. When simply updating one table based on the rows of another table, improve the performance and scalability with basic INSERT, UPDATE, and DELETE statements. For example:  
  
```sql
INSERT tbl_A (col, col2)  
SELECT col, col2
FROM tbl_B
WHERE NOT EXISTS (SELECT col FROM tbl_A A2 WHERE A2.col = tbl_B.col);  
```  
::: moniker-end

::: moniker range="=azure-sqldw-latest"
> [!NOTE]
> MERGE is now Generally Available in Synapse Dedicated SQL Pool with version '10.0.17829.0' or above. Connect to your dedicated SQL pool (formerly SQL DW) and run `SELECT @@VERSION`. A pause and resume may be required to ensure your instance gets the latest version.

> [!TIP]
> The conditional behavior described for the MERGE statement works best when the two tables have a complex mixture of matching characteristics. For example, inserting a row if it doesn't exist, or updating a row if it matches. When simply updating one table based on the rows of another table, consider using basic INSERT, UPDATE, and DELETE statements for better query performance and scalability. For example:  
>  
> ```sql
> INSERT tbl_A (col, col2)  
> SELECT col, col2
> FROM tbl_B
> WHERE NOT EXISTS (SELECT col FROM tbl_A A2 WHERE A2.col = tbl_B.col);  
> ```  
::: moniker-end
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"  
```syntaxsql

-- SQL Server and Azure SQL Database
[ WITH <common_table_expression> [,...n] ]  
MERGE
    [ TOP ( expression ) [ PERCENT ] ]
    [ INTO ] <target_table> [ WITH ( <merge_hint> ) ] [ [ AS ] table_alias ]  
    USING <table_source> [ [ AS ] table_alias ]
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

<merge_search_condition> ::=  
    <search_condition>  
  
<merge_matched>::=  
    { UPDATE SET <set_clause> | DELETE }  
  
<merge_not_matched>::=  
{  
    INSERT [ ( column_list ) ]
        { VALUES ( values_list )  
        | DEFAULT VALUES }  
}  
  
<clause_search_condition> ::=  
    <search_condition> 
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

::: moniker-end

::: moniker range="=azure-sqldw-latest"

```syntaxsql
-- MERGE for Azure Synapse Analytics 
[ WITH <common_table_expression> [,...n] ]  
MERGE
    [ INTO ] <target_table> [ [ AS ] table_alias ]  
    USING <table_source> [ [ AS ] table_alias ]
    ON <merge_search_condition>  
    [ WHEN MATCHED [ AND <clause_search_condition> ]  
        THEN <merge_matched> ] [ ...n ]  
    [ WHEN NOT MATCHED [ BY TARGET ] [ AND <clause_search_condition> ]  
        THEN <merge_not_matched> ]  
    [ WHEN NOT MATCHED BY SOURCE [ AND <clause_search_condition> ]  
        THEN <merge_matched> ] [ ...n ]
    [ OPTION ( <query_hint> [ ,...n ] ) ]
;  -- The semi-colon is required, or the query will return a syntax error. 
  
<target_table> ::=  
{
    [ database_name . schema_name . | schema_name . ]  
  target_table  
}  

<merge_search_condition> ::=  
    <search_condition>  
  
<merge_matched>::=  
    { UPDATE SET <set_clause> | DELETE }  
  
<merge_not_matched>::=  
{  
    INSERT [ ( column_list ) ]
        VALUES ( values_list )  
}  
  
<clause_search_condition> ::=  
    <search_condition> 
```

::: moniker-end

## Arguments

#### WITH \<common_table_expression>

Specifies the temporary named result set or view, also known as common table expression, that's defined within the scope of the MERGE statement. The result set derives from a simple query and is referenced by the MERGE statement. For more information, see [WITH common_table_expression &#40;Transact-SQL&#41;](../../t-sql/queries/with-common-table-expression-transact-sql.md).  
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
#### TOP (expression* ) [ PERCENT ]

Specifies the number or percentage of affected rows. *expression* can be either a number or a percentage of the rows. The rows referenced in the TOP expression aren't arranged in any order. For more information, see [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md).  
  
The TOP clause applies after the entire source table and the entire target table  join and the joined rows that don't qualify for an insert, update, or delete action are removed. The TOP clause further reduces the number of joined rows to the specified value. The insert, update, or delete actions apply to the remaining joined rows in an unordered way. That is, there's no order in which the rows are distributed among the actions defined in the WHEN clauses. For example, specifying TOP (10) affects 10 rows. Of these rows, 7 may be updated and 3 inserted, or 1 may be deleted, 5 updated, and 4 inserted, and so on.  
  
Without filters on the source table, the MERGE statement may perform a table scan or clustered index scan on the source table, as well as a table scan  or clustered index scan of target table. Therefore, I/O performance is sometimes affected even when using the TOP clause to modify a large table by creating multiple batches. In this scenario, it's important to ensure that all successive batches target new rows.  
::: moniker-end

#### *database_name*  
The name of the database in which *target_table* is located.  
  
#### *schema_name*  
The name of the schema to which *target_table* belongs.  
  
#### *target_table*  
The table or view against which the data rows from \<table_source> are matched based on \<clause_search_condition>. *target_table* is the target of any insert, update, or delete operations specified by the WHEN clauses of the MERGE statement.  
  
If *target_table* is a view, any actions against it must satisfy the conditions for updating views. For more information, see [Modify Data Through a View](../../relational-databases/views/modify-data-through-a-view.md).  
  
*target_table* can't be a remote table. *target_table* can't have any rules defined on it.  


Hints can be specified as a <merge_hint>. 

::: moniker range="=azure-sqldw-latest"
Note that merge_hints aren't supported for [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)].
::: moniker-end
  
#### [ AS ] *table_alias*  
An alternative name to reference a table for the *target_table*.  
  
#### USING \<table_source>  
Specifies the data source that's matched with the data rows in *target_table* based on \<merge_search condition>. The result of this match dictates the actions to take by the WHEN clauses of the MERGE statement. \<table_source> can be a remote table or a derived table that accesses remote tables.
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
\<table_source> can be a derived table that uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] [table value constructor](../../t-sql/queries/table-value-constructor-transact-sql.md) to construct a table by specifying multiple rows.  
::: moniker-end

::: moniker range="=azure-sqldw-latest"
\<table_source> can be a derived table that uses `SELECT ... UNION ALL` to construct a table by specifying multiple rows.  
::: moniker-end

 #### [ AS ] *table_alias*  
An alternative name to reference a table for the table_source.   
  
For more information about the syntax and arguments of this clause, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md).  
  
#### ON \<merge_search_condition>  
Specifies the conditions on which \<table_source> joins with *target_table* to determine where they match.
  
> [!CAUTION]  
> It's important to specify only the columns from the target table to use for matching purposes. That is, specify columns from the target table that are compared to the corresponding column of the source table. Don't attempt to improve query performance by filtering out rows in the target table in the ON clause; for example, such as specifying `AND NOT target_table.column_x = value`. Doing so may return unexpected and incorrect results.  
  
#### WHEN MATCHED THEN \<merge_matched>  
Specifies that all rows of *target_table, which match the rows returned by \<table_source> ON \<merge_search_condition>, and satisfy any additional search condition, are either updated or deleted according to the \<merge_matched> clause.  
  
The MERGE statement can have, at most, two WHEN MATCHED clauses. If two clauses are specified, the first clause must be accompanied by an AND \<search_condition> clause. For any given row, the second WHEN MATCHED clause is only applied if the first isn't. If there are two WHEN MATCHED clauses, one must specify an UPDATE action and one must specify a DELETE action. When UPDATE is specified in the \<merge_matched> clause, and more than one row of \<table_source> matches a row in *target_table* based on \<merge_search_condition>, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. The MERGE statement can't update the same row more than once, or update and delete the same row.  
  
#### WHEN NOT MATCHED [ BY TARGET ] THEN \<merge_not_matched>  
Specifies that a row is inserted into *target_table* for every row returned by \<table_source> ON \<merge_search_condition> that doesn't match a row in *target_table*, but satisfies an additional search condition, if present. The values to insert are specified by the \<merge_not_matched> clause. The MERGE statement can have only one WHEN NOT MATCHED [ BY TARGET ] clause.

#### WHEN NOT MATCHED BY SOURCE THEN \<merge_matched>  
Specifies that all rows of *target_table, which don't match the rows returned by \<table_source> ON \<merge_search_condition>, and that satisfy any additional search condition, are updated or deleted according to the \<merge_matched> clause.  
  
The MERGE statement can have at most two WHEN NOT MATCHED BY SOURCE clauses. If two clauses are specified, then the first clause must be accompanied by an AND \<clause_search_condition> clause. For any given row, the second WHEN NOT MATCHED BY SOURCE clause is only applied if the first isn't. If there are two WHEN NOT MATCHED BY SOURCE clauses, then one must specify an UPDATE action and one must specify a DELETE action. Only columns from the target table can be referenced in \<clause_search_condition>.  
  
When no rows are returned by \<table_source>, columns in the source table can't be accessed. If the update or delete action specified in the \<merge_matched> clause references columns in the source table, error 207 (Invalid column name) is returned. For example, the clause `WHEN NOT MATCHED BY SOURCE THEN UPDATE SET TargetTable.Col1 = SourceTable.Col1` may cause the statement to fail because `Col1` in the source table is inaccessible.  
  
#### AND \<clause_search_condition>  
Specifies any valid search condition. For more information, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
#### \<table_hint_limited>  
Specifies one or more table hints to apply on the target table for each of the insert, update, or delete actions done by the MERGE statement. The WITH keyword and the parentheses are required.  
  
NOLOCK and READUNCOMMITTED aren't allowed. For more information about table hints, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
Specifying the TABLOCK hint on a table that's the target of an INSERT statement has the same effect as specifying the TABLOCKX hint. An exclusive lock is taken on the table. When FORCESEEK is specified, it applies to the implicit instance of the target table joined with the source table.  
  
> [!CAUTION]  
> Specifying READPAST with WHEN NOT MATCHED [ BY TARGET ] THEN INSERT may result in INSERT operations that violate UNIQUE constraints.  
  
#### INDEX ( index_val [ ,...n ] )  
Specifies the name or ID of one or more indexes on the target table for doing an implicit join with the source table. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
#### \<output_clause>  
Returns a row for every row in *target_table* that's updated, inserted, or deleted, in no particular order. **$action** can be specified in the output clause. **$action** is a column of type **nvarchar(10)** that returns one of three values for each row: 'INSERT', 'UPDATE', or 'DELETE', according to the action done on that row. The OUTPUT clause is the recommended way to query or count rows affected by a MERGE. For more information about the arguments and behavior of this clause, see [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md).  

::: moniker-end

#### OPTION ( \<query_hint> [ ,...n ] )  
Specifies that optimizer hints are used to customize the way the Database Engine processes the statement. For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
#### \<merge_matched>  
Specifies the update or delete action that's applied to all rows of *target_table* that don't match the rows returned by \<table_source> ON \<merge_search_condition>, and which satisfy any additional search condition.  
  
#### UPDATE SET \<set_clause>  
Specifies the list of column or variable names to update in the target table and the values with which to update them.  
  
For more information about the arguments of this clause, see [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md). Setting a variable to the same value as a column isn't supported.  
  
#### DELETE  
Specifies that the rows matching rows in *target_table* are deleted.  
  
#### \<merge_not_matched>  
Specifies the values to insert into the target table.  
  
#### (*column_list*)  
A list of one or more columns of the target table in which to insert data. Columns must be specified as a single-part name or else the MERGE statement will fail. *column_list* must be enclosed in parentheses and delimited by commas.  
  
#### VALUES ( *values_list*)  
A comma-separated list of constants, variables, or expressions that return values to insert into the target table. Expressions can't contain an EXECUTE statement.  
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
#### DEFAULT VALUES  
Forces the inserted row to contain the default values defined for each column.  
  
For more information about this clause, see [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md).  
::: moniker-end
  
#### \<search_condition>  
Specifies the search conditions to specify \<merge_search_condition> or \<clause_search_condition>. For more information about the arguments for this clause, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
#### \<graph search pattern>  
Specifies the graph match pattern. For more information about the arguments for this clause, see [MATCH &#40;Transact-SQL&#41;](../../t-sql/queries/match-sql-graph.md)
::: moniker-end
   
## Remarks

::: moniker range="=azure-sqldw-latest"

>[!NOTE]
> In Azure Synapse Analytics, the MERGE command has following differences compared to SQL server and Azure SQL database.  
> - Using MERGE to update a distribution key column is not supported in builds older than `10.0.17829.0`. If unable to pause or force-upgrade, use the ANSI `UPDATE FROM ... JOIN` statement as a workaround until on version `10.0.17829.0`.
> - A MERGE update is implemented as a delete and insert pair. The affected row count for a MERGE update includes the deleted and inserted rows. 
> - MERGE…WHEN NOT MATCHED INSERT is not supported for tables with IDENTITY columns.  
> - Table value constructor can't be used in the USING clause for the source table. Use `SELECT ... UNION ALL` to create a derived source table with multiple rows.
> - The support for tables with different distribution types is described in this table:
>
>|MERGE CLAUSE in Azure Synapse Analytics|Supported TARGET distribution table| Supported SOURCE distribution table|Comment|  
>|-----------------|---------------|-----------------|-----------|  
>|**WHEN MATCHED**| All distribution types |All distribution types||  
>|**NOT MATCHED BY TARGET**|HASH |All distribution types|Use UPDATE/DELETE FROM…JOIN to synchronize two tables. |
>|**NOT MATCHED BY SOURCE**|All distribution types|All distribution types||  

> [!TIP]
> If you're using the distribution hash key as the JOIN column in MERGE and performing just an equality comparison, you can omit the distribution key from the list of columns in the `WHEN MATCHED THEN UPDATE SET` clause, as this is a redundant update.

>[!IMPORTANT]
> In Azure Synapse Analytics the MERGE command on builds older than `10.0.17829.0` may, under certain conditions, leave the target table in an inconsistent state, with rows placed in the wrong distribution, causing later queries to return wrong results in some cases. This problem may happen in 2 cases:
> 
>|Scenario|Comment|  
>|---------------|-----------------|  
>|**Case 1** <br> Using MERGE on a HASH distributed TARGET table that contains secondary indices or a UNIQUE constraint. | - Fixed in Synapse SQL version ***10.0.15563.0*** and higher. <br> - If ```SELECT @@VERSION``` returns a lower version than 10.0.15563.0, manually pause and resume the Synapse SQL pool to pick up this fix. <br> - Until the fix has been applied to your Synapse SQL pool, avoid using the MERGE command on HASH distributed TARGET tables that have secondary indices or UNIQUE constraints. |
>|**Case 2** <br> Using MERGE to update a distribution key column of a HASH distributed table. | - Fixed in Synapse SQL version ***10.0.17829.0*** and higher. <br> - If ```SELECT @@VERSION``` returns a lower version than 10.0.17829.0, manually pause and resume the Synapse SQL pool to pick up this fix. <br> - Until the fix has been applied to your Synapse SQL pool, avoid using the MERGE command to update distribution key columns. |
>
> **Note that the updates in both scenarios do not repair tables already affected by previous MERGE execution.** Use scripts below to identify and repair any affected tables manually.
>
> To check which hash distributed tables in a database may be of concern (if used in the Cases above), run this statement
>
>```sql
> -- Case 1
> select a.name, c.distribution_policy_desc, b.type from sys.tables a join sys.indexes b
> on a.object_id = b.object_id
> join
> sys.pdw_table_distribution_properties c
> on a.object_id = c.object_id
> where b.type = 2 and c.distribution_policy_desc = 'HASH';
>
> -- Subject to Case 2, if distribution key value is updated in MERGE statement
> select a.name, c.distribution_policy_desc from sys.tables a 
> join
> sys.pdw_table_distribution_properties c
> on a.object_id = c.object_id
> where c.distribution_policy_desc = 'HASH';
> ```
> 
> To check if a hash distributed table for MERGE is affected by either Case 1 or Case 2, follow these steps to examine if the tables have rows landed in wrong distribution.  If 'no need for repair' is returned, this table is not affected.  
>
>```sql
> if object_id('[check_table_1]', 'U') is not null
> drop table [check_table_1]
> go
> if object_id('[check_table_2]', 'U') is not null
> drop table [check_table_2]
> go
>
> create table [check_table_1] with(distribution = round_robin) as
> select <DISTRIBUTION_COLUMN> as x from <MERGE_TABLE> group by <DISTRIBUTION_COLUMN>;
> go
>
> create table [check_table_2] with(distribution = hash(x)) as
> select x from [check_table_1];
>go
>
> if not exists(select top 1 * from (select <DISTRIBUTION_COLUMN> as x from <MERGE_TABLE> except select x from 
> [check_table_2]) as tmp)
> select 'no need for repair' as result
> else select 'needs repair' as result
> go
>
> if object_id('[check_table_1]', 'U') is not null
> drop table [check_table_1]
> go
> if object_id('[check_table_2]', 'U') is not null
> drop table [check_table_2]
> go
>```
>To repair affected tables, run these statements to copy all rows from the old table to a new table.
>```sql
> if object_id('[repair_table_temp]', 'U') is not null
> drop table [repair_table_temp];
> go
> if object_id('[repair_table]', 'U') is not null
> drop table [repair_table];
> go
> create table [repair_table_temp] with(distribution = round_robin) as select * from <MERGE_TABLE>;
> go
>
> -- [repair_table] will hold the repaired table generated from <MERGE_TABLE>
> create table [repair_table] with(distribution = hash(<DISTRIBUTION_COLUMN>)) as select * from [repair_table_temp];
> go
>if object_id('[repair_table_temp]', 'U') is not null
> drop table [repair_table_temp];
> go
> ```   

::: moniker-end

At least one of the three MATCHED clauses must be specified, but they can be specified in any order. A variable can't be updated more than once in the same MATCHED clause.  
  
Any insert, update, or delete action specified on the target table by the MERGE statement are limited by any constraints defined on it, including any cascading referential integrity constraints. If IGNORE_DUP_KEY is ON for any unique indexes on the target table, MERGE ignores this setting.  
  
The MERGE statement requires a semicolon (;) as a statement terminator. Error 10713 is raised when a MERGE statement is run without the terminator.  
  
When used after MERGE, [@@ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/rowcount-transact-sql.md) returns the total number of rows inserted, updated, and deleted to the client.  
  
MERGE is a fully reserved keyword when the database compatibility level is set to 100 or higher. The MERGE statement is available under both 90 and 100 database compatibility levels; however, the keyword isn't fully reserved when the database compatibility level is set to 90.  
  
> [!CAUTION]
> Don't use the MERGE statement when using [queued updating replication](../../relational-databases/replication/transactional/updatable-subscriptions-queued-updating-conflict-resolution.md). The MERGE and queued updating trigger aren't compatible. Replace the MERGE statement with an insert or an update statement.

::: moniker range="=azure-sqldw-latest"
### Troubleshooting
In certain scenarios, a MERGE statement may result in the error `"CREATE TABLE failed because column <> in table <> exceeds the maximum of 1024 columns."`, even when neither Target nor Source table has 1024 columns. This scenario can arise when all the below conditions are met:
- Multiple columns are specified in an UPDATE SET or INSERT operation within MERGE (not specific to any WHEN [NOT] MATCHED clause)
- Any column in the JOIN condition has a Non-Clustered Index (NCI)

If this error is found, the suggested workaround is to remove the Non-Clustered Index (NCI) from the JOIN columns, or join on columns without an NCI. If you later update the underlying tables to include an NCI on the JOIN columns, your MERGE statement may be susceptible to this error at runtime. See [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) to learn how to drop the Non-Clustered Index.
::: moniker-end

## Trigger implementation

For every insert, update, or delete action specified in the MERGE statement, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fires any corresponding AFTER triggers defined on the target table, but doesn't guarantee on which action to fire triggers first or last. Triggers defined for the same action honor the order you specify. For more information about setting trigger firing order, see [Specify First and Last Triggers](../../relational-databases/triggers/specify-first-and-last-triggers.md).  
  
If the target table has an enabled INSTEAD OF trigger defined on it for an insert, update, or delete action done by a MERGE statement, it must have an enabled INSTEAD OF trigger for all of the actions specified in the MERGE statement.  
  
If any INSTEAD OF UPDATE or INSTEAD OF DELETE triggers are defined on *target_table*, the update or delete operations aren't run. Instead, the triggers fire and the **inserted** and **deleted** tables then populate accordingly.  
  
If any INSTEAD OF INSERT triggers are defined on *target_table*, the insert operation isn't performed. Instead, the table populates accordingly.  

> [!NOTE]
> Unlike separate INSERT, UPDATE, and DELETE statements, the number of rows reflected by @@ROWCOUNT inside of a trigger may be higher. The @@ROWCOUNT inside any AFTER trigger (regardless of data modification statements the trigger captures) will reflect the total number of rows affected by the MERGE. For example, if a MERGE statement inserts one row, updates one row, and deletes one row, @@ROWCOUNT will be three for any AFTER trigger, even if the trigger is only declared for INSERT statements. 
  
## Permissions

Requires SELECT permission on the source table and INSERT, UPDATE, or DELETE permissions on the target table. For more information, see the Permissions section in the [SELECT](../../t-sql/queries/select-transact-sql.md), [INSERT](../../t-sql/statements/insert-transact-sql.md), [UPDATE](../../t-sql/queries/update-transact-sql.md), and [DELETE](../../t-sql/statements/delete-transact-sql.md) articles.  
  
## <a id="optimizing-merge-statement-performance"></a>Index best practices 

By using the MERGE statement, you can replace the individual DML statements with a single statement. This can improve query performance because the operations are performed within a single statement, therefore, minimizing the number of times the data in the source and target tables are processed. However, performance gains depend on having correct indexes, joins, and other considerations in place.

To improve the performance of the MERGE statement, we recommend the following index guidelines:

- Create indexes to facilitate the join between the source and target of the MERGE:
  - Create an index on the join columns in the source table that has keys covering the join logic to the target table. If possible, it should be unique. 
  - Also, create an index on the join columns in the target table. If possible, it should be a unique clustered index.
  - These two indexes ensure that the data in the tables is sorted, and uniqueness aids performance of the comparison. Query performance is improved because the query optimizer doesn't need to perform extra validation processing to locate and update duplicate rows and additional sort operations aren't necessary.
- Avoid tables with any form of columnstore index as the target of MERGE statements. As with any UPDATEs, you may find performance better with columnstore indexes by updating a staged rowstore table, then performing a batched DELETE and INSERT, instead of an UPDATE or MERGE.

## Concurrency considerations for MERGE

In terms of locking, MERGE is different from discrete, consecutive INSERT, UPDATE, and DELETE statements. MERGE still executes INSERT, UPDATE, and DELETE operations, however using different locking mechanisms. It may be more efficient to write discrete INSERT, UPDATE, and DELETE statements for some application needs. At scale, MERGE may introduce complicated concurrency issues or require advanced troubleshooting. As such, plan to thoroughly test any MERGE statement before deploying to production.

MERGE statements are a suitable replacement for discrete INSERT, UPDATE, and DELETE operations in (but not limited to) the following scenarios:

- ETL operations involving large row counts be executed during a time when other concurrent operations aren't* expected. When heavy concurrency is expected, separate INSERT, UPDATE, and DELETE logic may perform better, with less blocking, than a MERGE statement. 
- Complex operations involving small row counts and transactions unlikely to execute for extended duration.
- Complex operations involving user tables where indexes can be designed to ensure optimal execution plans, avoiding table scans and lookups in favor of index scans or - ideally - index seeks.

Other considerations for concurrency:

- In some scenarios where unique keys are expected to be both inserted and updated by the MERGE, specifying the HOLDLOCK will prevent against unique key violations. HOLDLOCK is a synonym for the SERIALIZABLE transaction isolation level, which doesn't allow for other concurrent transactions to modify data that this transaction has read.  SERIALIZABLE is the safest isolation level but provides for the least concurrency with other transactions that retains locks on ranges of data to prevent phantom rows from being inserted or updated while reads are in progress. For more information on HOLDLOCK, see [Hints](../queries/hints-transact-sql-table.md) and [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](set-transaction-isolation-level-transact-sql.md).

### JOIN best practices

To improve the performance of the MERGE statement and ensure correct results are obtained, we recommend the following join guidelines:

- Specify only search conditions in the ON <merge_search_condition> clause that determine the criteria for matching data in the source and target tables. That is, specify only columns from the target table that are compared to the corresponding columns of the source table. 
- Don't include comparisons to other values such as a constant.

To filter out rows from the source or target tables, use one of the following methods.

- Specify the search condition for row filtering in the appropriate WHEN clause. For example, `WHEN NOT MATCHED AND S.EmployeeName LIKE 'S%' THEN INSERT....`
- Define a view on the source or target that returns the filtered rows and reference the view as the source or target table. If the view is defined on the target table, any actions against it must satisfy the conditions for updating views. For more information about updating data by using a view, see Modifying Data Through a View.
- Use the `WITH <common table expression>` clause to filter out rows from the source or target tables. This method is similar to specifying additional search criteria in the ON clause and may produce incorrect results. We recommend that you avoid using this method or test thoroughly before implementing it.

The join operation in the MERGE statement is optimized in the same way as a join in a SELECT statement. That is, when SQL Server processes join, the query optimizer chooses the most efficient method (out of several possibilities) of processing the join. When the source and target are of similar size and the index guidelines described previously are applied to the source and target tables, a merge join operator is the most efficient query plan. This is because both tables are scanned once and there's no need to sort the data. When the source is smaller than the target table, a nested loops operator is preferable.

You can force the use of a specific join by specifying the `OPTION (<query_hint>)` clause in the MERGE statement. We recommend that you don't use the hash join as a query hint for MERGE statements because this join type doesn't use indexes.

### Parameterization best practices

If a SELECT, INSERT, UPDATE, or DELETE statement is executed without parameters, the SQL Server query optimizer may choose to parameterize the statement internally. This means that any literal values that are contained in the query are substituted with parameters. For example, the statement `INSERT dbo.MyTable (Col1, Col2) VALUES (1, 10)`, may be implemented internally as `INSERT dbo.MyTable (Col1, Col2) VALUES (@p1, @p2)`. This process, called simple parameterization, increases the ability of the relational engine to match new SQL statements with existing, previously compiled execution plans. Query performance may be improved because the frequency of query compilations and recompilations are reduced. The query optimizer doesn't apply the simple parameterization process to MERGE statements. Therefore, MERGE statements that contain literal values may not perform and individual INSERT, UPDATE, or DELETE statements because a new plan is compiled each time the MERGE statement is executed.

To improve query performance, we recommend the following parameterization guidelines:

- Parameterize all literal values in the `ON <merge_search_condition>` clause and in the `WHEN` clauses of the MERGE statement. For example, you can incorporate the MERGE statement into a stored procedure replacing the literal values with appropriate input parameters.
- If you can't parameterize the statement, create a plan guide of type `TEMPLATE` and specify the `PARAMETERIZATION FORCED` query hint in the plan guide. For more information, see [Specify Query Parameterization Behavior by Using Plan Guides](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md).
- If MERGE statements are executed frequently on the database, consider setting the PARAMETERIZATION option on the database to FORCED. Use caution when setting this option. The `PARAMETERIZATION` option is a database-level setting and affects how all queries against the database are processed. For more information, see [Forced Parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization).
- As a newer and easier alternative to plan guides, consider a similar strategy with Query Store hints. For more information, see [Query Store hints](../../relational-databases/performance/query-store-hints.md).

### TOP Clause best practices

In the MERGE statement, the TOP clause specifies the number or percentage of rows that are affected after the source table and the target table are joined, and after rows that don't qualify for an insert, update, or delete action are removed. The TOP clause further reduces the number of joined rows to the specified value and the insert, update, or delete actions are applied to the remaining joined rows in an unordered fashion. That is, there's no order in which the rows are distributed among the actions defined in the WHEN clauses. For example, specifying TOP (10) affects 10 rows; of these rows, 7 may be updated and 3 inserted, or 1 may be deleted, 5 updated, and 4 inserted and so on.

It's common to use the TOP clause to perform data manipulation language (DML) operations on a large table in batches. When using the TOP clause in the MERGE statement for this purpose, it's important to understand the following implications.

- I/O performance may be affected.

  The MERGE statement performs a full table scan of both the source and target tables. Dividing the operation into batches reduces the number of write operations performed per batch; however, each batch will perform a full table scan of the source and target tables. The resulting read activity may affect the performance of the query and other concurrent activity on the tables.

- Incorrect results can occur.

  It's important to ensure that all successive batches target new rows or undesired behavior such as incorrectly inserting duplicate rows into the target table can occur. This can happen when the source table includes a row that wasn't in a target batch but was in the overall target table. To ensure correct results:

  - Use the ON clause to determine which source rows affect existing target rows and which are genuinely new.
  - Use an additional condition in the WHEN MATCHED clause to determine if the target row has already been updated by a previous batch.
  - Use an additional condition in the WHEN MATCHED clause and SET logic to verify the same row can't be updated twice. 

Because the TOP clause is only applied after these clauses are applied, each execution either inserts one genuinely unmatched row or updates one existing row.

### Bulk Load best practices

The MERGE statement can be used to efficiently bulk load data from a source data file into a target table by specifying the `OPENROWSET(BULK…)` clause as the table source. By doing so, the entire file is processed in a single batch.

To improve the performance of the bulk merge process, we recommend the following guidelines:

- Create a clustered index on the join columns in the target table. 
- Disable other non-unique, nonclustered indexes on the target table during the bulk load MERGE, enable them afterwards. This is common and useful for nightly bulk data operations.
- Use the ORDER and UNIQUE hints in the `OPENROWSET(BULK…)` clause to specify how the source data file is sorted.

  By default, the bulk operation assumes the data file is unordered. Therefore, it's important that the source data is sorted according to the clustered index on the target table and that the ORDER hint is used to indicate the order so that the query optimizer can generate a more efficient query plan. Hints are validated at runtime; if the data stream doesn't conform to the specified hints, an error is raised.

These guidelines ensure that the join keys are unique and the sort order of the data in the source file matches the target table. Query performance is improved because additional sort operations aren't necessary and unnecessary data copies aren't required.

### Measuring and diagnosing MERGE performance

The following features are available to assist you in measuring and diagnosing the performance of MERGE statements.

- Use the **merge stmt** counter in the [sys.dm_exec_query_optimizer_info](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-info-transact-sql.md) dynamic management view to return the number of query optimizations that are for MERGE statements.
- Use the `merge_action_type` attribute in the [sys.dm_exec_plan_attributes](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md) dynamic management view to return the type of trigger execution plan used as the result of a MERGE statement.
- Use an Extended Events Session to gather troubleshooting data for the MERGE statement in the same way you would for other data manipulation language (DML) statements. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

## Examples  

### A. Using MERGE to do INSERT and UPDATE operations on a table in a single statement

A common scenario is updating one or more columns in a table if a matching row exists. Or, inserting the data as a new row if a matching row doesn't exist. You usually do either scenario by passing parameters to a stored procedure that contains the appropriate UPDATE and INSERT statements. With the MERGE statement, you can do both tasks in a single statement. The following example shows a stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database that contains both an INSERT statement and an UPDATE statement. The procedure is then modified to run the equivalent operations by using a single MERGE statement.  
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"  
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
  
    MERGE Production.UnitMeasure AS tgt  
    USING (SELECT @UnitMeasureCode, @Name) as src (UnitMeasureCode, Name)  
    ON (tgt.UnitMeasureCode = src.UnitMeasureCode)  
    WHEN MATCHED THEN
        UPDATE SET Name = src.Name  
    WHEN NOT MATCHED THEN  
        INSERT (UnitMeasureCode, Name)  
        VALUES (src.UnitMeasureCode, src.Name)  
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

::: moniker-end

::: moniker range="=azure-sqldw-latest"

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
ALTER PROCEDURE dbo.InsertUnitMeasure  
    @UnitMeasureCode nchar(3),  
    @Name nvarchar(25)  
AS
BEGIN  
    SET NOCOUNT ON;  
  
    MERGE Production.UnitMeasure AS tgt  
    USING (SELECT @UnitMeasureCode, @Name) as src (UnitMeasureCode, Name)  
    ON (tgt.UnitMeasureCode = src.UnitMeasureCode)  
    WHEN MATCHED THEN
        UPDATE SET Name = src.Name  
    WHEN NOT MATCHED THEN  
        INSERT (UnitMeasureCode, Name)  
        VALUES (src.UnitMeasureCode, src.Name);  
END;  
GO  
-- Test the procedure and return the results.  
EXEC InsertUnitMeasure @UnitMeasureCode = 'ABC', @Name = 'New Test Value';  
EXEC InsertUnitMeasure @UnitMeasureCode = 'XYZ', @Name = 'Test Value';  
EXEC InsertUnitMeasure @UnitMeasureCode = 'ABC', @Name = 'Another Test Value';  
  
-- Cleanup
DELETE FROM Production.UnitMeasure WHERE UnitMeasureCode IN ('ABC','XYZ');  
GO  
```  
::: moniker-end

### B. Using MERGE to do UPDATE and DELETE operations on a table in a single statement

The following example uses MERGE to update the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database, daily, based on orders that are processed in the `SalesOrderDetail` table. The `Quantity` column of the `ProductInventory` table is updated by subtracting the number of orders placed each day for each product in the `SalesOrderDetail` table. If the number of orders for a product drops the inventory level of a product to 0 or less, the row for that product is deleted from the `ProductInventory` table.  
  
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017" 

```sql  
CREATE PROCEDURE Production.usp_UpdateInventory  
    @OrderDate datetime  
AS  
MERGE Production.ProductInventory AS tgt  
USING (SELECT ProductID, SUM(OrderQty) FROM Sales.SalesOrderDetail AS sod  
    JOIN Sales.SalesOrderHeader AS soh  
    ON sod.SalesOrderID = soh.SalesOrderID  
    AND soh.OrderDate = @OrderDate  
    GROUP BY ProductID) as src (ProductID, OrderQty)  
ON (tgt.ProductID = src.ProductID)  
WHEN MATCHED AND tgt.Quantity - src.OrderQty <= 0  
    THEN DELETE  
WHEN MATCHED
    THEN UPDATE SET tgt.Quantity = tgt.Quantity - src.OrderQty,
                    tgt.ModifiedDate = GETDATE()  
OUTPUT $action, Inserted.ProductID, Inserted.Quantity,
    Inserted.ModifiedDate, Deleted.ProductID,  
    Deleted.Quantity, Deleted.ModifiedDate;  
GO  
  
EXECUTE Production.usp_UpdateInventory '20030501';  
```

::: moniker-end

::: moniker range="=azure-sqldw-latest"

```sql  
CREATE PROCEDURE Production.usp_UpdateInventory  
    @OrderDate datetime  
AS  
MERGE Production.ProductInventory AS tgt  
USING (SELECT ProductID, SUM(OrderQty) FROM Sales.SalesOrderDetail AS sod  
    JOIN Sales.SalesOrderHeader AS soh  
    ON sod.SalesOrderID = soh.SalesOrderID  
    AND soh.OrderDate = @OrderDate  
    GROUP BY ProductID) as src (ProductID, OrderQty)  
ON (tgt.ProductID = src.ProductID)  
WHEN MATCHED AND tgt.Quantity - src.OrderQty <= 0  
    THEN DELETE  
WHEN MATCHED
    THEN UPDATE SET tgt.Quantity = tgt.Quantity - src.OrderQty,
                    tgt.ModifiedDate = GETDATE();  
GO  
  
EXECUTE Production.usp_UpdateInventory '20030501';  
```

::: moniker-end
  
### C. Using MERGE to do UPDATE and INSERT operations on a target table by using a derived source table

The following example uses MERGE to modify the `SalesReason` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database by either updating or inserting rows. 

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
When the value of `NewName` in the source table matches a value in the `Name` column of the target table, (`SalesReason`), the `ReasonType` column is updated in the target table. When the value of `NewName` doesn't match, the source row is inserted into the target table. The source table is a derived table that uses the [!INCLUDE[tsql](../../includes/tsql-md.md)] table value constructor to specify multiple rows for the source table. For more information about using the table value constructor in a derived table, see [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md). 

The OUTPUT clause can be useful to query the result of MERGE statements, for more information, see [OUTPUT Clause](../../t-sql/queries/output-clause-transact-sql.md). The example also shows how to store the results of the OUTPUT clause in a table variable. And, then you summarize the results of the MERGE statement by running a simple select operation that returns the count of inserted and updated rows.  
  
```sql  
-- Create a temporary table variable to hold the output actions.  
DECLARE @SummaryOfChanges TABLE(Change VARCHAR(20));  
  
MERGE INTO Sales.SalesReason AS tgt  
USING (VALUES ('Recommendation','Other'), ('Review', 'Marketing'),
              ('Internet', 'Promotion'))  
       as src (NewName, NewReasonType)  
ON tgt.Name = src.NewName  
WHEN MATCHED THEN  
UPDATE SET ReasonType = src.NewReasonType  
WHEN NOT MATCHED BY TARGET THEN  
INSERT (Name, ReasonType) VALUES (NewName, NewReasonType)  
OUTPUT $action INTO @SummaryOfChanges;  
  
-- Query the results of the table variable.  
SELECT Change, COUNT(*) AS CountPerChange  
FROM @SummaryOfChanges  
GROUP BY Change;  
```  
::: moniker-end

::: moniker range="=azure-sqldw-latest"
When the value of `NewName` in the source table matches a value in the `Name` column of the target table, (`SalesReason`), the `ReasonType` column is updated in the target table. When the value of `NewName` doesn't match, the source row is inserted into the target table. The source table is a derived table that uses `SELECT ... UNION ALL` to specify multiple rows for the source table.
  
```sql  
MERGE INTO Sales.SalesReason AS tgt  
USING (SELECT 'Recommendation','Other' UNION ALL 
       SELECT 'Review', 'Marketing' UNION ALL 
       SELECT 'Internet', 'Promotion')
   as src (NewName, NewReasonType)  
ON tgt.Name = src.NewName  
WHEN MATCHED THEN  
UPDATE SET ReasonType = src.NewReasonType  
WHEN NOT MATCHED BY TARGET THEN  
INSERT (Name, ReasonType) VALUES (NewName, NewReasonType);  
```  
::: moniker-end


::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
### D. Inserting the results of the MERGE statement into another table

The following example captures data returned from the OUTPUT clause of a MERGE statement and inserts that data into another table. The MERGE statement updates the `Quantity` column of the `ProductInventory` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database, based on orders that are processed in the `SalesOrderDetail` table. The example captures the updated rows and inserts them into another table that's used to track inventory changes. 
  
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

### E. Using MERGE to do INSERT or UPDATE on a target edge table in a graph database

In this example, you create node tables `Person` and `City` and an edge table `livesIn`. You use the MERGE statement on the `livesIn` edge and insert a new row if the edge doesn't already exist between a `Person` and `City`. If the edge already exists, then you just update the StreetAddress attribute on the `livesIn` edge.

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
::: moniker-end
  
## See also
::: moniker range="= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017"
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)
- [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)
- [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)
- [OUTPUT Clause &#40;Transact-SQL&#41;](../../t-sql/queries/output-clause-transact-sql.md)
- [MERGE in Integration Services Packages](../../integration-services/control-flow/merge-in-integration-services-packages.md)
- [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)
- [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md)  
::: moniker-end

::: moniker range="=azure-sqldw-latest"
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)
- [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)
- [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)
- [MERGE in Integration Services Packages](../../integration-services/control-flow/merge-in-integration-services-packages.md)
- [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)
- [Table Value Constructor &#40;Transact-SQL&#41;](../../t-sql/queries/table-value-constructor-transact-sql.md)  
::: moniker-end
