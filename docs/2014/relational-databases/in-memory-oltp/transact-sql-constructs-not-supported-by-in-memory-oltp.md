---
title: "Transact-SQL Constructs Not Supported by In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e3f8009c-319d-4d7b-8993-828e55ccde11
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transact-SQL Constructs Not Supported by In-Memory OLTP
  Memory-optimized tables and natively compiled stored procedures do not support the full [!INCLUDE[tsql](../../includes/tsql-md.md)] surface area that is supported by disk-based tables and interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures. When attempting to use one of the unsupported features, the server returns an error.  
  
 The error message text mentions the type of [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (feature, operation, option, for example) and well as the name of the feature or [!INCLUDE[tsql](../../includes/tsql-md.md)] keyword. Most unsupported features will return error 10794, with the error message text indicating the unsupported feature. The following tables list the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the error message text, as well as the corrective action to resolve the error.  
  
 For more information on supported features with memory-optimized tables and natively compiled stored procedures, see:  
  
-   [Migration Issues for Natively Compiled Stored Procedures](migration-issues-for-natively-compiled-stored-procedures.md)  
  
-   [Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)  
  
-   [Supported SQL Server Features](unsupported-sql-server-features-for-in-memory-oltp.md)  
  
-   [Natively Compiled Stored Procedures](../in-memory-oltp/natively-compiled-stored-procedures.md)  
  
## Databases That Use In-Memory OLTP  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving an In-Memory OLTP database.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Option|AUTO_CLOSE|The database option AUTO_CLOSE=ON is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Option|ATTACH_REBUILD_LOG|The CREATE database option ATTACH_REBUILD_LOG is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|DATABASE SNAPSHOT|Creating database snapshots is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|Replication using the sync_method 'database snapshot' or 'database snapshot character'|Replication using the sync_method 'database snapshot' or 'database snapshot character' is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|DBCC CHECKDB<br /><br /> DBCC CHECKTABLE|DBCC CHECKDB skips the memory-optimized tables in the database.<br /><br /> DBCC CHECKTABLE will fail for memory-optimized tables.|  
  
## Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving a memory-optimized table, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|ON|Memory-optimized tables cannot be placed on a filegroup or partition scheme. Remove the ON clause from the `CREATE TABLE` statement.|  
|Data type|*Data type name*|The indicated data type is not supported. Replace the type with one of the supported data types. For more information, see [Supported Data Types](supported-data-types-for-in-memory-oltp.md).|  
|Feature|Computed columns|Computed columns are not supported for memory-optimized tables. Remove the computed columns from the `CREATE TABLE` statement.|  
|Feature|Replication|Replication is not supported with memory-optimized tables.|  
|Feature|FILESTREAM|FILESTREAM storage is not supported columns of memory-optimized tables. Remove the `FILESTREAM` keyword from the column definition.|  
|Feature|SPARSE|Columns of memory-optimized tables cannot be defined as SPARSE. Remove the `SPARSE` keyword from the column definition.|  
|Feature|ROWGUIDCOL|The option ROWGUIDCOL is not supported for columns of memory-optimized tables. Remove the `ROWGUIDCOL` keyword from the column definition.|  
|Feature|FOREIGN KEY|FOREIGN KEY constraints are not supported for memory-optimized tables. Remove the constraint from the table definition.<br /><br /> For information about how to mitigate the lack of support for constraints, see [Migrating Check and Foreign Key Constraints](../../database-engine/migrating-check-and-foreign-key-constraints.md).|  
|Feature|CHECK|CHECK constraints are not supported for memory-optimized tables. Remove the constraint from the table definition.<br /><br /> For information about how to mitigate the lack of support for constraints, see [Migrating Check and Foreign Key Constraints](../../database-engine/migrating-check-and-foreign-key-constraints.md).|  
|Feature|UNIQUE|UNIQUE constraints are not supported for memory-optimized tables. Remove the constraint from the table definition.<br /><br /> For information about how to mitigate the lack of support for constraints, see [Migrating Check and Foreign Key Constraints](../../database-engine/migrating-check-and-foreign-key-constraints.md).|  
|Feature|COLUMNSTORE|COLUMNSTORE indexes are not supported with memory-optimized tables. Specify a NONCLUSTERED or NONCLUSTERED HASH index instead.|  
|Feature|clustered index|Specify a nonclustered index. In the case of a primary key index be sure to specify `PRIMARY KEY NONCLUSTERED [HASH]`.|  
|Feature|non-1252 code page|Columns in memory-optimized tables with data types `char` and `varchar` must use code page 1252. Use n(var)char instead of (var)char, or use a collation with code page 1252 (for example, Latin1_General_BIN2). For more information, see [Collations and Code Pages](../../database-engine/collations-and-code-pages.md).|  
|Feature|DDL inside transactions|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped in the context of a user transaction. Do not start a transaction and ensure the session setting IMPLICIT_TRANSACTIONS is OFF before executing the CREATE or DROP statement.|  
|Feature|DDL triggers|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped if there is a server or database trigger for that DDL operation. Remove the server and database triggers on CREATE/DROP TABLE and CREATE/DROP PROCEDURE.|  
|Feature|EVENT NOTIFICATION|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped if there is a server or database event notification for that DDL operation. Remove the server and database event notifications on CREATE TABLE or DROP TABLE and CREATE PROCEDURE or DROP PROCEDURE.|  
|Feature|FileTable|Memory-optimized tables cannot be created as file tables. Remove the argument `AS FileTable` from the `CREATE TABLE` statement|  
|Operation|Update of primary key columns|Primary key columns in memory-optimized tables and table types cannot be updated. If the primary key needs to be updated, delete the old row and insert the new row with the updated primary key.|  
|Operation|CREATE INDEX|Indexes on memory-optimized tables must be specified inline with the `CREATE TABLE` statement. To add an index to a memory-optimized table, drop and recreate the table, including the new index specification.|  
|Operation|ALTER TABLE|Altering memory-optimized tables is not supported. Drop and recreate the table using the updated table definition.|  
|Operation|CREATE FULLTEXT INDEX|Fulltext indexes are not supported for memory-optimized tables.|  
|Operation|schema change|Memory-optimized tables and natively compiled stored procedures do not support shema changes, for example, `sp_rename`.<br /><br /> Attempting to make schema changes, such as renaming a table, will generate error 12320, Operations that require a change to the schema version, for example renaming, are not supported with memory-optimized tables.<br /><br /> To change the schema, drop and recreate the table or procedure using an updated definition.|  
|Operation|CREATE TRIGGER|Triggers on memory-optimized tables are not supported.|  
|Operation|TRUNCATE TABLE|The TRUNCATE operation is not supported for memory-optimized tables. To remove all rows from a table, delete all rows using `DELETE FROM`*table* or drop and recreate the table.|  
|Operation|ALTER AUTHORIZATION|Changing the owner of an existing memory-optimized table or natively compiled stored procedure is not supported. Drop and recreate the table or procedure to change ownership.|  
|Operation|ALTER SCHEMA|Changing the schema of an existing memory-optimized table or natively compiled stored procedure is not supported. Drop and recreate the table or procedure to change the schema.|  
|Operation|DBCC CHECKTABLE|DBCC CHECKTABLE is not supported with memory-optimized tables.|  
|Feature|ANSI_PADDING OFF|The session option `ANSI_PADDING` must be ON when creating memory-optimized tables or natively compiled stored procedures. Execute `SET ANSI_PADDING ON` before running the CREATE statement.|  
|Option|DATA_COMPRESSION|Data compression is not supported for memory-optimized tables. Remove the option from the table definition.|  
|Feature|DTC|Memory-optimized tables and natively compiled stored procedures cannot be accessed from distributed transactions. Use SQL transactions instead.|  
|Feature|Multiple Active Result Sets (MARS)|Multiple Active Result Sets (MARS) is not supported with memory-optimized tables. This error can also indicate linked server use. Linked server can use MARS. Linked servers are not supported with memory-optimized tables. Instead, connect directly to the server and database that hosts the memory-optimized tables.|  
|Operation|Memory-optimized tables as target of MERGE|Memory-optimized tables cannot be the target of a `MERGE` operation. Use `INSERT`, `UPDATE`, or `DELETE` statements instead.|  
  
## Indexes on Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving an index on a memory-optimized table, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|Filtered index|Filtered indexes are not supported with memory-optimized tables. Omit the `WHERE` clause from the index specification.|  
|Feature|UNIQUE|Unique indexes are not supported for memory-optimized tables. Remove the argument `UNIQUE` from the index specification.|  
|Feature|Nullable columns|All columns in the key of an index on a memory-optimized table must be specified as `NOT NULL`. Include the `NOT NULL` constraint with all columns in the index keys.|  
|Feature|non-bin2 collation|All character columns in the key of a memory-optimized index must be declared using a BIN2 collation. Use the `COLLATE` clause to set the collation in the column definition. For more information, see [Collations and Code Pages](../../database-engine/collations-and-code-pages.md).|  
|Feature|Included columns|Specifying included columns is not necessary for memory-optimized tables. All columns of the memory-optimized table are implicitly included in every memory-optimized index.|  
|Operation|ALTER INDEX|Altering indexes on memory-optimized tables is not supported. Instead, drop the table and recreate it using the updated index specification.|  
|Operation|DROP INDEX|Dropping indexes on memory-optimized tables is not supported. Instead, drop the table and recreate it with the desired indexes.|  
|Index option|*Index option*|The indicated index option is not supported with indexes on memory-optimized tables. Remove the option from the index specification.|  
  
## Nonclustered Hash Indexes  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving a nonclustered hash index, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Option|ASC/DESC|Nonclustered hash indexes are not ordered. Remove the keywords `ASC` and `DESC` from the index key specification.|  
  
## Natively Compiled Stored Procedures  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving natively compiled stored procedures, as well as the corrective action to resolve the error.  
  
|Type|Feature|Resolution|  
|----------|-------------|----------------|  
|Feature|Inline table variables|Table types cannot be declared inline with variable declarations. Table types must be declared explicitly using a `CREATE TYPE` statement.|  
|Feature|Cursors|Cursors are not supported on or in natively compiled stored procedures.<br /><br /> -When executing the procedure from the client, use RPC rather than the cursor API. With ODBC, avoid the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement `EXECUTE`, instead specify the name of the procedure directly.<br /><br /> -When executing the procedure from a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch or another stored procedure, avoid using a cursor with the natively compiled stored procedure.<br /><br /> -When creating a natively compiled stored procedure, rather than using a cursor, use set-based logic or a `WHILE` loop.|  
|Feature|Non-constant parameter defaults|When using default values with parameters on natively compiled stored procedures, the values must be constants. Remove any wildcards from the parameter declarations.|  
|Feature|EXTERNAL|CLR stored procedures cannot be natively compiled. Either remove the AS EXTERNAL clause or the NATIVE_COMPILATION option from the CREATE PROCEDURE statement.|  
|Feature|Numbered stored procedures|Natively compiled stored procedures cannot be numbered. Remove the `;`*number* from the `CREATE PROCEDURE` statement.|  
|Feature|multi-row INSERT ... VALUES statements|Cannot insert multiple rows using the same `INSERT` statement in a natively compiled stored procedure. Create `INSERT` statements for each row.|  
|Feature|Common Table Expressions (CTEs)|Common table expressions (CTE) are not supported in natively compiled stored procedures. Rewrite the query.|  
|Feature|subquery|Subqueries (queries nested inside another query) are not supported. Rewrite the query.|  
|Feature|COMPUTE|The `COMPUTE` clause is not supported. Remove it from the query.|  
|Feature|SELECT INTO|The `INTO` clause is not supported with the `SELECT` statement. Rewrite the query as `INSERT INTO`*Table*`SELECT`.|  
|Feature|OUTPUT|The `OUTPUT` clause is not supported. Remove it from the query.|  
|Feature|incomplete insert column list|In `INSERT` statements, values must be specified for all columns in the table.|  
|Function|*Function*|The built-in function is not supported in natively compiled stored procedures. Remove the function from the stored procedure. For more information about supported built-in functions, see [Natively Compiled Stored Procedures](../in-memory-oltp/natively-compiled-stored-procedures.md).|  
|Feature|CASE|The `CASE` statement is not supported in queries inside natively compiled stored procedures. Create queries for each case. For more information, see [Implementing a CASE Statement](implementing-a-case-expression-in-a-natively-compiled-stored-procedure.md).|  
|Feature|user-defined functions|User-defined functions cannot be used in natively compiled stored procedures. Remove the reference to the function from the procedure definition.|  
|Feature|user-defined aggregates|User-defined aggregate functions cannot be used in natively compiled stored procedures. Remove the reference to the function from the procedure.|  
|Feature|browse mode metadata|Natively compiled stored procedures do not support browse mode metadata. Make sure the session option `NO_BROWSETABLE` is set to OFF.|  
|Feature|DELETE with FROM clause|The `FROM` clause is not supported for `DELETE` statements with a table source in natively compiled stored procedures.<br /><br /> `DELETE` with the `FROM` clause is supported when it is used to indicate the table to delete from.|  
|Feature|UPDATE with FROM clause|The `FROM` clause is not supported for `UPDATE` statements in natively compiled stored procedures.|  
|Feature|temporary procedures|Temporary stored procedures cannot be natively compiled. Either create a permanent natively compiled stored procedure or a temporary interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure.|  
|Isolation level|READ UNCOMMITTED|The isolation level READ UNCOMMITTED is not supported for natively compiled stored procedures. Use a supported isolation level, such as SNAPSHOT.|  
|Isolation level|READ COMMITTED|The isolation level READ UNCOMMITTED is not supported for natively compiled stored procedures. Use a supported isolation level, such as SNAPSHOT.|  
|Feature|temporary tables|Tables in tempdb cannot be used in natively compiled stored procedures. Instead, use a table variable or a memory-optimized table with DURABILITY=SCHEMA_ONLY.|  
|Feature|MARS|Multiple Active Result Sets (MARS) is not supported with natively compiled stored procedures. This error can also indicate linked server use. Linked server can use MARS. Linked servers are not supported with natively compiled stored procedures. Instead, connect directly to the server and database that hosts the natively compiled stored procedures.|  
|Feature|DTC|Memory-optimized tables and natively compiled stored procedures cannot be accessed from distributed transactions. Use SQL transactions instead.|  
|Feature|non-bin2 collation|Comparison, sorting, and other operations on character strings in natively compiled stored procedures require using a BIN2 collation. Use the COLLATE clause or use columns and variables with an appropriate collation. For more information, see [Collations and Code Pages](../../database-engine/collations-and-code-pages.md).|  
|Feature|Truncation of character strings with an SC collation.|Character strings with an `_SC` collation use the UTF-16 encoding. Converting an n(var)char value to an n(var)char value with a shorted length involves truncation. This is not supported for UTF-16 values in natively compiled stored procedures. Avoid truncation of UTF-16 strings.|  
|Feature|EXECUTE WITH RECOMPILE|The option `WITH RECOMPILE` is not supported for natively compiled stored procedures.|  
|Feature|LEN and SUBSTRING with an argument in an SC collation|Character strings with an _SC collation use the UTF-16 encoding. The built-in functions LEN and SUBSTRING, when used inside natively compiled stored procedures, do not support the UTF-16 encoding. Use a different collation or avoid using these functions.|  
|Feature|Execution from the dedicated administrator connection.|Natively compiled stored procedures cannot be executed from the dedicated admin connection (DAC). Use a regular connection instead.|  
|Operation|ALTER PROCEDURE|Natively compiled stored procedures cannot be altered. To change the procedure definition, drop and recreate the stored procedure.|  
|Operation|savepoint|Natively compiled stored procedures cannot be invoked from transactions that have an active savepoint. Remove the savepoint from the transaction.|  
|Operation|ALTER AUTHORIZATION|Changing the owner of an existing memory-optimized table or natively compiled stored procedure is not supported. Drop and recreate the table or procedure to change ownership.|  
|Operator|OPENROWSET|This operator is not supported. Remove `OPENROWSET` from the natively compiled stored procedure.|  
|Operator|OPENQUERY|This operator is not supported. Remove `OPENQUERY` from the natively compiled stored procedure.|  
|Operator|OPENDATASOURCE|This operator is not supported. Remove `OPENDATASOURCE` from the natively compiled stored procedure.|  
|Operator|OPENXML|This operator is not supported. Remove `OPENXML` from the natively compiled stored procedure.|  
|Operator|CONTAINSTABLE|This operator is not supported. Remove `CONTAINSTABLE` from the natively compiled stored procedure.|  
|Operator|FREETEXTTABLE|This operator is not supported. Remove `FREETEXTTABLE` from the natively compiled stored procedure.|  
|Feature|table-valued functions|Table-valued functions cannot be referenced from natively compiled stored procedures. One possible workaround for this restriction is to add the logic in the table-valued functions to the procedure body.|  
|Operator|CHANGETABLE|This operator is not supported. Remove `CHANGETABLE` from the natively compiled stored procedure.|  
|Operator|GOTO|This operator is not supported. Use other procedural constructs such as WHILE.|  
|Operator|EXECUTE, INSERT EXEC|Nesting natively compiled stored procedures is not supported. The required operations can be specified inline, as part of the stored procedure definition.|  
|Operator|OFFSET|This operator is not supported. Remove `OFFSET` from the natively compiled stored procedure.|  
|Operator|UNION|This operator is not supported. Remove `UNION` from the natively compiled stored procedure. Combining several result sets into a single result set can be done using a table variable.|  
|Operator|INTERSECT|This operator is not supported. Remove `INTERSECT` from the natively compiled stored procedure. In some cases an INNER JOIN can be used to obtain the same result.|  
|Operator|EXCEPT|This operator is not supported. Remove `EXCEPT` from the natively compiled stored procedure.|  
|Operator|OUTER JOIN|This operator is not supported. Remove `OUTER JOIN` from the natively compiled stored procedure. For more information, see [Implementing an Outer Join](implementing-an-outer-join.md).|  
|Operator|APPLY|This operator is not supported. Remove `APPLY` from the natively compiled stored procedure.|  
|Operator|PIVOT|This operator is not supported. Remove `PIVOT` from the natively compiled stored procedure.|  
|Operator|UNPIVOT|This operator is not supported. Remove `UNPIVOT` from the natively compiled stored procedure.|  
|Operator|OR, IN|Disjunction (OR, IN) is not supported in the WHERE clause of queries in natively compiled stored procedures. Create queries for each of the cases.|  
|Operator|CONTAINS|This operator is not supported. Remove `CONTAINS` from the natively compiled stored procedure.|  
|Operator|FREETEXT|This operator is not supported. Remove `FREETEXT` from the natively compiled stored procedure.|  
|Operator|NOT|This operator is not supported. Remove `NOT` from the natively compiled stored procedure. In some cases, `NOT` can be replaced with inequality. For example, `NOT a=b` can be replaced with `a!=b`.|  
|Operator|TSEQUAL|This operator is not supported. Remove `TSEQUAL` from the natively compiled stored procedure.|  
|Operator|LIKE|This operator is not supported. Remove `LIKE` from the natively compiled stored procedure.|  
|Operator|NEXT VALUE FOR|Sequences cannot be referenced inside natively compiled stored procedures. Obtain the value using interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)], and then pass it into the natively compiled stored procedure. For more information, see [Implementing IDENTITY in a Memory-Optimized Table](implementing-identity-in-a-memory-optimized-table.md).|  
|Set option|*option*|SET options cannot be changed inside natively compiled stored procedures. Certain options can be set with the BEGIN ATOMIC statement. For more information, see the section on atonic blocks in [Natively Compiled Stored Procedures](../in-memory-oltp/natively-compiled-stored-procedures.md).|  
|Operand|TABLESAMPLE|This operator is not supported. Remove `TABLESAMPLE` from the natively compiled stored procedure.|  
|Option|RECOMPILE|Natively compiled stored procedures are compiled at create time. To recompile a natively compiled stored procedure, drop and recreate it. Remove `RECOMPILE` from the procedure definition.|  
|Option|ENCRYPTION|This option is not supported. Remove `ENCRYPTION` from the procedure definition.|  
|Option|FOR REPLICATION|Natively compiled stored procedures cannot be created for replication. Removed `FOR REPLICATION` from the procedure definition.|  
|Option|FOR XML|This option is not supported. Remove `FOR XML` from the natively compiled stored procedure.|  
|Option|FOR BROWSE|This option is not supported. Remove `FOR BROWSE` from the natively compiled stored procedure.|  
|Join hint|HASH, MERGE|Natively compiled stored procedures only support nested-loops joins. Hash and merge joins are not supported. Remove the join hint.|  
|Query hint|*Query hint*|This query hint is not inside natively compiled stored procedures. For supported query hints see [Query Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-query).|  
|Option|DISTINCT|This option is not supported. Remove `DISTINCT` from the query in the natively compiled stored procedure.|  
|Option|PERCENT|This option is not supported with `TOP` clauses. Remove `PERCENT` from the query in the natively compiled stored procedure.|  
|Option|WITH TIES|This option is not supported with `TOP` clauses. Remove `WITH TIES` from the query in the natively compiled stored procedure.|  
|Aggregate function|*Aggregate function*|This clause is not supported. For more information about aggregate functions in natively compiled stored procedures, see [Natively Compiled Stored Procedures](../in-memory-oltp/natively-compiled-stored-procedures.md).|  
|Ranking function|*Ranking function*|Ranking functions are not supported in natively compiled stored procedures. Remove them from the procedure definition.|  
|Function|*Function*|This function is not supported. Remove it from the natively compiled stored procedure.|  
|Statement|*Statement*|This statement is not supported. Remove it from the natively compiled stored procedure.|  
|Feature|MIN and MAX used with binary and character strings|The aggregate functions `MIN` and `MAX` cannot be used for character and binary string values inside natively compiled stored procedures.|  
|Feature|GROUP BY without aggregate function|In natively compiled stored procedures, when a query has a `GROUP BY` clause, the query must also use an aggregate function in the SELECT or the HAVING clause. Add an aggregate function to the query.|  
|Feature|GROUP BY ALL|ALL cannot be used with GROUP BY clauses in natively compiled stored procedures. Remove ALL from the GROUP BY clause.|  
|Feature|GROUP BY ()|Grouping by an empty list is not supported. Either remove the GROUP BY clause, or include columns in the grouping list.|  
|Feature|ROLLUP|`ROLLUP` cannot be used with `GROUP BY` clauses in natively compiled stored procedures. Remove `ROLLUP` from the procedure definition.|  
|Feature|CUBE|`CUBE` cannot be used with `GROUP BY` clauses in natively compiled stored procedures. Remove `CUBE` from the procedure definition.|  
|Feature|GROUPING SETS|`GROUPING SETS` cannot be used with `GROUP BY` clauses in natively compiled stored procedures. Remove `GROUPING SETS` from the procedure definition.|  
|Feature|BEGIN TRANSACTION, COMMIT TRANSACTION, and ROLLBACK TRANSACTION|Use ATOMIC blocks to control transactions and error handling. For more information, see [Atomic Blocks](atomic-blocks-in-native-procedures.md).|  
|Feature|Inline table variable declarations.|Table variables must reference explicitly defined memory-optimized table types. You should create a memory-optimized table type and use that type for the variable declaration, rather than specifying the type inline.|  
|Feature|sp_recompile|Recompiling natively compiled stored procedures is not supported. Drop and recreate the procedure.|  
|Feature|EXECUTE AS CALLER|The `EXECUTE AS` clause is required. But `EXECUTE AS CALLER` is not supported. Use `EXECUTE AS OWNER`, `EXECUTE AS`*user*, or `EXECUTE AS SELF`.|  
|Feature|Disk-based tables|Disk-based tables cannot be accessed from natively compiled stored procedures. Remove references to disk-based tables from the natively-compiled stored procedures. Or, migrate the disk-based table(s) to memory optimized.|  
|Feature|Views|Views cannot be accessed from natively compiled stored procedures. Instead of views, reference the underlying base tables.|  
|Feature|Table valued functions|Table-valued functions cannot be accessed from natively compiled stored procedures. Remove references to table-valued functions from the natively compiled stored procedure.|  
  
## Transactions that Access Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving transactions that access memory-optimized tables, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|savepoint|Creating explicit savepoints in transactions that access memory-optimized tables is not supported.|  
|Feature|bound transaction|Bound sessions cannot participate in transactions that access memory-optimized tables. Do not bind the session before executing the procedure.|  
|Feature|DTC|Transactions that access memory-optimized tables cannot be distributed transactions.|  
  
## See Also  
 [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md)  
  
  
