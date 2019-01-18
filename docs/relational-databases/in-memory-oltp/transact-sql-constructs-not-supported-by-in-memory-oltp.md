---
title: "Transact-SQL Constructs Not Supported by In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "11/21/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e3f8009c-319d-4d7b-8993-828e55ccde11
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transact-SQL Constructs Not Supported by In-Memory OLTP
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Memory-optimized tables, natively compiled stored procedures, and user-defined functions do not support the full [!INCLUDE[tsql](../../includes/tsql-md.md)] surface area that is supported by disk-based tables, interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures, and user-defined functions. When attempting to use one of the unsupported features, the server returns an error.  
  
 The error message text mentions the type of [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (feature, operation, option, for example) and well as the name of the feature or [!INCLUDE[tsql](../../includes/tsql-md.md)] keyword. Most unsupported features will return error 10794, with the error message text indicating the unsupported feature. The following tables list the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the error message text, as well as the corrective action to resolve the error.  
  
 For more information on supported features with memory-optimized tables and natively compiled stored procedures, see:  
  
-   [Migration Issues for Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/migration-issues-for-natively-compiled-stored-procedures.md)  
  
-   [Transact-SQL Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md)  
  
-   [Unsupported SQL Server Features for In-Memory OLTP](../../relational-databases/in-memory-oltp/unsupported-sql-server-features-for-in-memory-oltp.md)  
  
-   [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md)  
  
## Databases That Use In-Memory OLTP  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features that are not supported, and the keywords that can appear in the message text of an error involving an In-Memory OLTP database. The table also lists the resolution for the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Option|AUTO_CLOSE|The database option AUTO_CLOSE=ON is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Option|ATTACH_REBUILD_LOG|The CREATE database option ATTACH_REBUILD_LOG is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|DATABASE SNAPSHOT|Creating database snapshots is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|Replication using the sync_method 'database snapshot' or 'database snapshot character'|Replication using the sync_method 'database snapshot' or 'database snapshot character' is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|DBCC CHECKDB<br /><br /> DBCC CHECKTABLE|DBCC CHECKDB skips the memory-optimized tables in the database.<br /><br /> DBCC CHECKTABLE will fail for memory-optimized tables.|  
  
## Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features that are not supported, and the keywords that can appear in the message text of an error involving a memory-optimized table. The table also lists the resolution for the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|ON|Memory-optimized tables cannot be placed on a filegroup or partition scheme. Remove the ON clause from the **CREATE TABLE** statement.<br /><br /> All memory optimized tables are mapped to memory-optimized filegroup.|  
|Data type|*Data type name*|The indicated data type is not supported. Replace the type with one of the supported data types. For more information, see [Supported Data Types for In-Memory OLTP](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md).|  
|Feature|Computed columns|**Applies to:** [!INCLUDE[ssSQL14-md](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>Computed columns are not supported for memory-optimized tables. Remove the computed columns from the **CREATE TABLE** statement.<br/><br/>[!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)] do support computed columns in memory-optimized tables and indexes.|  
|Feature|Replication|Replication is not supported with memory-optimized tables.|  
|Feature|FILESTREAM|FILESTREAM storage is not supported columns of memory-optimized tables. Remove the **FILESTREAM** keyword from the column definition.|  
|Feature|SPARSE|Columns of memory-optimized tables cannot be defined as SPARSE. Remove the **SPARSE** keyword from the column definition.|  
|Feature|ROWGUIDCOL|The option ROWGUIDCOL is not supported for columns of memory-optimized tables. Remove the **ROWGUIDCOL** keyword from the column definition.|  
|Feature|FOREIGN KEY|**Applies to:** [!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>For memory-optimized tables, FOREIGN KEY constraints are only supported for foreign keys referencing primary keys of other memory-optimized tables. Remove the constraint from the table definition if the foreign key references a unique constraint.<br/><br/>In [!INCLUDE[ssSQL14-md](../../includes/sssql14-md.md)], FOREIGN KEY constraints are not supported with memory-optimized tables.|  
|Feature|clustered index|Specify a nonclustered index. In the case of a primary key index be sure to specify **PRIMARY KEY NONCLUSTERED**.|  
|Feature|DDL inside transactions|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped in the context of a user transaction. Do not start a transaction and ensure the session setting IMPLICIT_TRANSACTIONS is OFF before executing the CREATE or DROP statement.|  
|Feature|DDL triggers|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped if there is a server or database trigger for that DDL operation. Remove the server and database triggers on CREATE/DROP TABLE and CREATE/DROP PROCEDURE.|  
|Feature|EVENT NOTIFICATION|Memory-optimized tables and natively compiled stored procedures cannot be created or dropped if there is a server or database event notification for that DDL operation. Remove the server and database event notifications on CREATE TABLE or DROP TABLE and CREATE PROCEDURE or DROP PROCEDURE.|  
|Feature|FileTable|Memory-optimized tables cannot be created as file tables. Remove the argument **AS FileTable** from the **CREATE TABLE** statement|  
|Operation|Update of primary key columns|Primary key columns in memory-optimized tables and table types cannot be updated. If the primary key needs to be updated, delete the old row and insert the new row with the updated primary key.|  
|Operation|CREATE INDEX|Indexes on memory-optimized tables must be specified inline with the **CREATE TABLE** statement, or with the **ALTER TABLE** statement.|  
|Operation|CREATE FULLTEXT INDEX|Fulltext indexes are not supported for memory-optimized tables.|  
|Operation|schema change|Memory-optimized tables and natively compiled stored procedures do not support certain schema changes:<br/> [!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)]: ALTER TABLE, ALTER PROCEDURE, and sp_rename operations are supported. Other schema changes, for example adding extended properties, are not supported.<br/><br/>[!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]: ALTER TABLE and ALTER PROCEDURE operations are supported. Other schema changes, including sp_rename, are not supported.<br/><br/>[!INCLUDE[ssSQL14-md](../../includes/sssql14-md.md)]: schema changes are not supported. To change the definition of a memory-optimized table or natively compiled stored procedure, first drop the object and then recreate it with the desired definittion.| 
|Operation|TRUNCATE TABLE|The TRUNCATE operation is not supported for memory-optimized tables. To remove all rows from a table, delete all rows using **DELETE FROM**_table_ or drop and recreate the table.|  
|Operation|ALTER AUTHORIZATION|Changing the owner of an existing memory-optimized table or natively compiled stored procedure is not supported. Drop and recreate the table or procedure to change ownership.|  
|Operation|ALTER SCHEMA|Transferring an existing table or natively compiled stored procedure to another schema is not supported. Drop and recreate the object to transfer between schemas.|  
|Operation|DBCC CHECKTABLE|DBCC CHECKTABLE are not supported with memory-optimized tables. To verify the integrity of the on-disk checkpoint files, perform a backup of the MEMORY_OPTIMIZED_DATA filegroup.|  
|Feature|ANSI_PADDING OFF|The session option **ANSI_PADDING** must be ON when creating memory-optimized tables or natively compiled stored procedures. Execute **SET ANSI_PADDING ON** before running the CREATE statement.|  
|Option|DATA_COMPRESSION|Data compression is not supported for memory-optimized tables. Remove the option from the table definition.|  
|Feature|DTC|Memory-optimized tables and natively compiled stored procedures cannot be accessed from distributed transactions. Use SQL transactions instead.|  
|Operation|Memory-optimized tables as target of MERGE|Memory-optimized tables cannot be the target of a **MERGE** operation. Use **INSERT**, **UPDATE**, and **DELETE** statements instead.|  
  
## Indexes on Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving an index on a memory-optimized table, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|Filtered index|Filtered indexes are not supported with memory-optimized tables. Omit the **WHERE** clause from the index specification.|  
|Feature|Included columns|Specifying included columns is not necessary for memory-optimized tables. All columns of the memory-optimized table are implicitly included in every memory-optimized index.|  
|Operation|DROP INDEX|Dropping indexes on memory-optimized tables is not supported. You can delete indexes using ALTER TABLE.<br /><br /> For more information, see [Altering Memory-Optimized Tables](../../relational-databases/in-memory-oltp/altering-memory-optimized-tables.md).|  
|Index option|*Index option*|Only one index option is supported - BUCKET_COUNT for HASH indexes.|  
  
## Nonclustered Hash Indexes  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving a nonclustered hash index, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Option|ASC/DESC|Nonclustered hash indexes are not ordered. Remove the keywords **ASC** and **DESC** from the index key specification.|  
  
## Natively Compiled Stored Procedures and User-Defined Functions  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving natively compiled stored procedures and user-defined functions, as well as the corrective action to resolve the error.  
  
|Type|Feature|Resolution|  
|----------|-------------|----------------|  
|Feature|Inline table variables|Table types cannot be declared inline with variable declarations. Table types must be declared explicitly using a **CREATE TYPE** statement.|  
|Feature|Cursors|Cursors are not supported on or in natively compiled stored procedures.<br /><br /> When executing the procedure from the client, use RPC rather than the cursor API. With ODBC, avoid the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement **EXECUTE**, instead specify the name of the procedure directly.<br /><br /> When executing the procedure from a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch or another stored procedure, avoid using a cursor with the natively compiled stored procedure.<br /><br /> When creating a natively compiled stored procedure, rather than using a cursor, use set-based logic or a **WHILE** loop.|  
|Feature|Non-constant parameter defaults|When using default values with parameters on natively compiled stored procedures, the values must be constants. Remove any wildcards from the parameter declarations.|  
|Feature|EXTERNAL|CLR stored procedures cannot be natively compiled. Either remove the AS EXTERNAL clause or the NATIVE_COMPILATION option from the CREATE PROCEDURE statement.|  
|Feature|Numbered stored procedures|Natively compiled stored procedures cannot be numbered. Remove the **;**_number_ from the **CREATE PROCEDURE** statement.|  
|Feature|multi-row INSERT ... VALUES statements|Cannot insert multiple rows using the same **INSERT** statement in a natively compiled stored procedure. Create **INSERT** statements for each row.|  
|Feature|Common Table Expressions (CTEs)|Common table expressions (CTE) are not supported in natively compiled stored procedures. Rewrite the query.|  
|Feature|COMPUTE|The **COMPUTE** clause is not supported. Remove it from the query.|  
|Feature|SELECT INTO|The **INTO** clause is not supported with the **SELECT** statement. Rewrite the query as **INSERT INTO** _Table_ **SELECT**.|  
|Feature|incomplete insert column list|In general, in INSERT statements values must be specified for all columns in the table.<br /><br /> However, we do support DEFAULT constraints and IDENTITY(1,1) columns on memory optimized tables. These columns can be, and in the case of IDENTITY columns must be, omitted from the INSERT column list.|  
|Feature|*Function*|Some built-in functions are not supported in natively compiled stored procedures. Remove the rejected function from the stored procedure. For more information about supported built-in functions, see<br />[Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md), or<br />[Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md).|  
|Feature|CASE|**Applies to:** [!INCLUDE[ssSQL14-md](../../includes/sssql14-md.md)] and SQL Server starting [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>**CASE** expressions are not supported in queries inside natively compiled stored procedures. Create queries for each case. For more information, see [Implementing a CASE Expression in a Natively Compiled Stored Procedure](../../relational-databases/in-memory-oltp/implementing-a-case-expression-in-a-natively-compiled-stored-procedure.md).<br/><br/>[!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] do support CASE expressions.|  
|Feature|INSERT EXECUTE|Remove the reference.|  
|Feature|EXECUTE|Supported only to execute natively compiled stored procedures and user-defined functions.|  
|Feature|user-defined aggregates|User-defined aggregate functions cannot be used in natively compiled stored procedures. Remove the reference to the function from the procedure.|  
|Feature|browse mode metadata|Natively compiled stored procedures do not support browse mode metadata. Make sure the session option **NO_BROWSETABLE** is set to OFF.|  
|Feature|DELETE with FROM clause|The **FROM** clause is not supported for **DELETE** statements with a table source in natively compiled stored procedures.<br /><br /> **DELETE** with the **FROM** clause is supported when it is used to indicate the table to delete from.|  
|Feature|UPDATE with FROM clause|The **FROM** clause is not supported for **UPDATE** statements in natively compiled stored procedures.| 
|Feature|temporary procedures|Temporary stored procedures cannot be natively compiled. Either create a permanent natively compiled stored procedure or a temporary interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure.|  
|Isolation level|READ UNCOMMITTED|The isolation level READ UNCOMMITTED is not supported for natively compiled stored procedures. Use a supported isolation level, such as SNAPSHOT.|  
|Isolation level|READ COMMITTED|The isolation level READ COMMITTED is not supported for natively compiled stored procedures. Use a supported isolation level, such as SNAPSHOT.|  
|Feature|temporary tables|Tables in tempdb cannot be used in natively compiled stored procedures. Instead, use a table variable or a memory-optimized table with DURABILITY=SCHEMA_ONLY.|  
|Feature|DTC|Memory-optimized tables and natively compiled stored procedures cannot be accessed from distributed transactions. Use SQL transactions instead.|  
|Feature|EXECUTE WITH RECOMPILE|The option **WITH RECOMPILE** is not supported for natively compiled stored procedures.|  
|Feature|Execution from the dedicated administrator connection.|Natively compiled stored procedures cannot be executed from the dedicated admin connection (DAC). Use a regular connection instead.|  
|Operation|savepoint|Natively compiled stored procedures cannot be invoked from transactions that have an active savepoint. Remove the savepoint from the transaction.|  
|Operation|ALTER AUTHORIZATION|Changing the owner of an existing memory-optimized table or natively compiled stored procedure is not supported. Drop and recreate the table or procedure to change ownership.|  
|Operator|OPENROWSET|This operator is not supported. Remove **OPENROWSET** from the natively compiled stored procedure.|  
|Operator|OPENQUERY|This operator is not supported. Remove **OPENQUERY** from the natively compiled stored procedure.|  
|Operator|OPENDATASOURCE|This operator is not supported. Remove **OPENDATASOURCE** from the natively compiled stored procedure.|  
|Operator|OPENXML|This operator is not supported. Remove **OPENXML** from the natively compiled stored procedure.|  
|Operator|CONTAINSTABLE|This operator is not supported. Remove **CONTAINSTABLE** from the natively compiled stored procedure.|  
|Operator|FREETEXTTABLE|This operator is not supported. Remove **FREETEXTTABLE** from the natively compiled stored procedure.|  
|Feature|table-valued functions|Table-valued functions cannot be referenced from natively compiled stored procedures. One possible workaround for this restriction is to add the logic in the table-valued functions to the procedure body.|  
|Operator|CHANGETABLE|This operator is not supported. Remove **CHANGETABLE** from the natively compiled stored procedure.|  
|Operator|GOTO|This operator is not supported. Use other procedural constructs such as WHILE.|  
|Operator|OFFSET|This operator is not supported. Remove **OFFSET** from the natively compiled stored procedure.|  
|Operator|INTERSECT|This operator is not supported. Remove **INTERSECT** from the natively compiled stored procedure. In some cases an INNER JOIN can be used to obtain the same result.|  
|Operator|EXCEPT|This operator is not supported. Remove **EXCEPT** from the natively compiled stored procedure.|  
|Operator|APPLY|**Applies to:** [!INCLUDE[ssSQL14-md](../../includes/sssql14-md.md)] and SQL Server starting [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>This operator is not supported. Remove **APPLY** from the natively compiled stored procedure.<br/><br/>[!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] do support the APPLY operator in natively compiled modules.|  
|Operator|PIVOT|This operator is not supported. Remove **PIVOT** from the natively compiled stored procedure.|  
|Operator|UNPIVOT|This operator is not supported. Remove **UNPIVOT** from the natively compiled stored procedure.|  
|Operator|CONTAINS|This operator is not supported. Remove **CONTAINS** from the natively compiled stored procedure.|  
|Operator|FREETEXT|This operator is not supported. Remove **FREETEXT** from the natively compiled stored procedure.|  
|Operator|TSEQUAL|This operator is not supported. Remove **TSEQUAL** from the natively compiled stored procedure.|  
|Operator|LIKE|This operator is not supported. Remove **LIKE** from the natively compiled stored procedure.|  
|Operator|NEXT VALUE FOR|Sequences cannot be referenced inside natively compiled stored procedures. Obtain the value using interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)], and then pass it into the natively compiled stored procedure. For more information, see [Implementing IDENTITY in a Memory-Optimized Table](../../relational-databases/in-memory-oltp/implementing-identity-in-a-memory-optimized-table.md).|  
|Set option|*option*|SET options cannot be changed inside natively compiled stored procedures. Certain options can be set with the BEGIN ATOMIC statement. For more information, see the section on atonic blocks in [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md).|  
|Operand|TABLESAMPLE|This operator is not supported. Remove **TABLESAMPLE** from the natively compiled stored procedure.|  
|Option|RECOMPILE|Natively compiled stored procedures are compiled at create time. Remove **RECOMPILE** from the procedure definition.<br /><br /> You can execute sp_recompile on a natively compiled stored procedure, which causes it to recompile on the next execution.|  
|Option|ENCRYPTION|This option is not supported. Remove **ENCRYPTION** from the procedure definition.|  
|Option|FOR REPLICATION|Natively compiled stored procedures cannot be created for replication. Removed **FOR REPLICATION** from the procedure definition.|  
|Option|FOR XML|This option is not supported. Remove **FOR XML** from the natively compiled stored procedure.|  
|Option|FOR BROWSE|This option is not supported. Remove **FOR BROWSE** from the natively compiled stored procedure.|  
|Join hint|HASH, MERGE|Natively compiled stored procedures only support nested-loops joins. Hash and merge joins are not supported. Remove the join hint.|  
|Query hint|*Query hint*|This query hint is not inside natively compiled stored procedures. For supported query hints see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).|  
|Option|PERCENT|This option is not supported with **TOP** clauses. Remove **PERCENT** from the query in the natively compiled stored procedure.|  
|Option|WITH TIES|**Applies to:** [!INCLUDE[ssSDS14_md](../../includes/sssql14-md.md)] and [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>This option is not supported with **TOP** clauses. Remove **WITH TIES** from the query in the natively compiled stored procedure.<br/><br/>[!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] do support **TOP WITH TIES**.|  
|Aggregate function|*Aggregate function*|Not all aggregate functions are supported. For more information about supported aggregate functions in natively compiled T-SQL modules, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).|  
|Ranking function|*Ranking function*|Ranking functions are not supported in natively compiled stored procedures. Remove them from the procedure definition.|  
|Function|*Function*|This function is not supported. For more information about supported functions in natively compiled T-SQL modules, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).|  
|Statement|*Statement*|This statement is not supported. For more information about supported functions in natively compiled T-SQL modules, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).|  
|Feature|MIN and MAX used with binary and character strings|The aggregate functions **MIN** and **MAX** cannot be used for character and binary string values inside natively compiled stored procedures.|  
|Feature|GROUP BY ALL|ALL cannot be used with GROUP BY clauses in natively compiled stored procedures. Remove ALL from the GROUP BY clause.|  
|Feature|GROUP BY ()|Grouping by an empty list is not supported. Either remove the GROUP BY clause, or include columns in the grouping list.|  
|Feature|ROLLUP|**ROLLUP** cannot be used with **GROUP BY** clauses in natively compiled stored procedures. Remove **ROLLUP** from the procedure definition.|  
|Feature|CUBE|**CUBE** cannot be used with **GROUP BY** clauses in natively compiled stored procedures. Remove **CUBE** from the procedure definition.|  
|Feature|GROUPING SETS|**GROUPING SETS** cannot be used with **GROUP BY** clauses in natively compiled stored procedures. Remove **GROUPING SETS** from the procedure definition.|  
|Feature|BEGIN TRANSACTION, COMMIT TRANSACTION, and ROLLBACK TRANSACTION|Use ATOMIC blocks to control transactions and error handling. For more information, see [Atomic Blocks](../../relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures.md).|  
|Feature|Inline table variable declarations.|Table variables must reference explicitly defined memory-optimized table types. You should create a memory-optimized table type and use that type for the variable declaration, rather than specifying the type inline.|  
|Feature|Disk-based tables|Disk-based tables cannot be accessed from natively compiled stored procedures. Remove references to disk-based tables from the natively-compiled stored procedures. Or, migrate the disk-based table(s) to memory optimized.|  
|Feature|Views|Views cannot be accessed from natively compiled stored procedures. Instead of views, reference the underlying base tables.|  
|Feature|Table valued functions|**Applies to**: [!INCLUDE[ssSDSFull_md](../../includes/ssSDSFull-md.md)] and SQL Server starting [!INCLUDE[ssSQL15-md](../../includes/sssql15-md.md)]<br/>Multi-statement table-valued functions cannot be accessed from natively compiled T-SQL modules. Inline table-valued functions are supported, but must be created WITH NATIVE_COMPILATION.<br/><br/>**Applies to**: [!INCLUDE[ssSQL14-md](../../includes/ssSQL14-md.md)]<br/>Table-valued functions cannot be referenced from natively compiled T-SQL modules.|  
|Option|PRINT|Remove reference|  
|Feature|DDL|No DDL is supported inside natively compiled T-SQL modules.|  
|Option|STATISTICS XML|Not supported. When you run a query, with STATISTICS XML enabled, the XML content is returned without the part for the natively compiled stored procedure.|  
  
## Transactions that Access Memory-Optimized Tables  
 The following table lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] features and keywords that can appear in the message text of an error involving transactions that access memory-optimized tables, as well as the corrective action to resolve the error.  
  
|Type|Name|Resolution|  
|----------|----------|----------------|  
|Feature|savepoint|Creating explicit savepoints in transactions that access memory-optimized tables is not supported.|  
|Feature|bound transaction|Bound sessions cannot participate in transactions that access memory-optimized tables. Do not bind the session before executing the procedure.|  
|Feature|DTC|Transactions that access memory-optimized tables cannot be distributed transactions.|  
  
## See Also  
 [Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)  
  
  
