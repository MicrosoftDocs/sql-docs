---
description: "Compares OPENROWSET, OPENQUERY, EXECUTE AT as options for remote execution"
title: "Query remote servers (Database Engine)"
ms.date: "12/02/2021"
ms.service: sql
ms.subservice: 
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "OLE DB, linked servers"
  - "OLE DB provider, linked servers"
  - "server management [SQL Server], linked servers"
  - "linked servers [SQL Server]"
  - "distributed queries [SQL Server], linked servers"
  - "servers [SQL Server], linked"
  - "remote servers [SQL Server], linked servers"
  - "linked servers [SQL Server], about linked servers"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-dt-2019
---

# Compare query remote execution options

In SQL Server, there are three ways to execute a query remotely:

- `OPENQUERY`
- `OPENROWSET`
- `EXECUTE AT`

This article describes these three methods.

## OPENQUERY 

[OPENQUERY (Transact-SQL)](../../t-sql/functions/openquery-transact-sql.md)

Executes the specified pass-through query on the specified linked server. This server is an OLE DB data source. In a query, use `OPENQUERY` in the `FROM` as if it were a table name. You can also reference `OPENQUERY` as the target table of an `INSERT`, `UPDATE`, or `DELETE` statement. This is subject to the capabilities of the OLE DB provider. Although the query may return multiple result sets, `OPENQUERY` returns only the first one.

`OPENQUERY` requires a pre-added and configured linked server and a request text to a remote server. `OPENQUERY` does not require a four part name convention to access objects.
  
## OPENROWSET

[OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)

Includes all connection information that is required to access remote data from an OLE DB data source. This method is an alternative to accessing tables in a linked server and is a one-time, ad-hoc method of connecting and accessing remote data by using OLE DB. For more frequent references to OLE DB data sources, instead consider using [linked servers](linked-servers-database-engine.md), [PolyBase](../polybase/polybase-guide.md), or direct connections between the two data sources via tools like [SQL Server Integration Services (SSIS)](../../integration-services/sql-server-integration-services.md) or custom applications. 

In a query, use `OPENROWSET` in the `FROM` clause of a query. You can also use `OPENROWSET` as the target table of an `INSERT`, `UPDATE`, or `DELETE` statement, subject to the capabilities of the OLE DB provider. Although the query might return multiple result sets, `OPENROWSET` returns only the first one.

`OPENROWSET` also supports bulk operations through a built-in `BULK` provider that enables data from a file to be read and returned as a rowset.

For additional information, `OPENROWSET` use an explicitly written connection string. 
  
## EXECUTE AT

[EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)

Allows dynamic SQL to run against a linked server. One of the parameters of the `EXECUTE` call is `AT`, which is designed to bypass the `OPENQUERY` and `OPENROWSET` restrictions. `EXECUTE (``<query>``) AT [<linked server>]` is dynamic SQL that can return any number of result sets from a remote server.

### Guidance for dynamic SQL

Avoid the use of dynamic SQL commands in applications, and restrict the permissions on users with access to dynamic SQL commands. Constructing queries to execute via `EXECUTE` can create vulnerabilities to websites and applications via SQL Injection attacks. For more information, see [SQL Injection](../../relational-databases/security/sql-injection.md).  

When performance is a concern, test remote queries:

- Ensure that as much logic as possible is executed on the remote server
- Verify indexes on the remote server index tables appropriately to support the query
- Be aware that the use of remote queries in a codebase complicates database code version control and maintenance of development and test environments

## Examples

### A. Execute a SELECT pass-through query with OPENQUERY

The following example uses a pass-through SELECT query to select the rows with `OPENQUERY`:

```sql
SELECT * 
    FROM OPENQUERY ([linkedserver], 
        'SELECT * FROM AdventureWorksLT.SalesLT.Customer');  
```

### B. Execute a SELECT pass-through query with OPENROWSET

The following example uses a pass-through `SELECT` query to select the rows with `OPENROWSET`

```sql
SELECT a.*
FROM OPENROWSET('MSOLEDBSQL', [linkedserver],
     'SELECT * FROM AdventureWorksLT.SalesLT.Customer') AS a;
```

[!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

### C. Execute a SELECT pass-through query with EXECUTE AT

The following example uses a pass-through `SELECT` query to select the rows with `EXECUTE ... AT`

```sql
EXECUTE ('SELECT * FROM AdventureWorksLT.SalesLT.Customer') AT [linkedserver]
```

### D. Execute multiple SELECT statements

The following example uses a pass-through `SELECT` query and getting multiple result sets

```sql
EXECUTE ('SELECT TOP 10 * FROM AdventureWorksLT.SalesLT.Customer;
    SELECT TOP 10 * FROM AdventureWorksLT.SalesLT.CustomerAddress;') AT [linkedserver];
```

### E. Execute a SELECT and pass two arguments

The following example uses a pass-through `SELECT` with two arguments

```sql
EXECUTE ('SELECT TOP 10 * FROM AdventureWorksLT.SalesLT.Customer 
WHERE CustomerID = ? AND LastName = ?', 10, 'Garza') AT [linkedserver];
```

### F. Execute a SELECT and pass two arguments, using variables

The following example uses a pass-through SELECT with two arguments by using variables

```sql
DECLARE @CustomerID AS INT
DECLARE @LastName AS VARCHAR(100)
SET @CustomerID = 10
SET @LastName = 'Garza'
EXECUTE ('SELECT TOP 10 * FROM AdventureWorksLT.SalesLT.Customer 
WHERE CustomerID = ? AND LastName = ?', @CustomerID, @LastName) AT [linkedserver];
```

### G. Execute DDL statements with EXECUTE using linked servers

The following example uses a DDL statement on Linked Server

```sql
EXECUTE (
'USE TempDB
IF OBJECT_ID(''dbo.Table1'') IS NOT NULL
DROP TABLE dbo.Table1
CREATE TABLE dbo.Table1
(
    Column1 INT
)' ) AT [linkedserver];
```


Once you are done with your testing, clean up created objects

```sql
EXECUTE (
'USE TempDB
IF OBJECT_ID(''dbo.Table1'') IS NOT NULL
DROP TABLE dbo.Table1'
) AT [linkedserver];
EXECUTE sp_dropserver 'linkedserver'
```

### Additional Examples

For additional examples that show using `INSERT...SELECT * FROM OPENROWSET(BULK...)`, see the following topics:

- [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep Identity Values When Bulk Importing Data &#40;SQL Server&#41;](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use Character Format to Import or Export Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)
- [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

## See Also

- [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)
- [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)
- [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)
- [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)
- [OPENDATASOURCE &#40;Transact-SQL&#41;](../../t-sql/functions/opendatasource-transact-sql.md)
- [OPENQUERY &#40;Transact-SQL&#41;](../../t-sql/functions/openquery-transact-sql.md)
- [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)
- [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
- [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)
- [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)