---
title: "sp_create_plan_guide (Transact-SQL)"
description: sp_create_plan_guide creates a plan guide for associating query hints or actual query plans with queries in a database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_create_plan_guide"
  - "sp_create_plan_guide_TSQL"
helpviewer_keywords:
  - "sp_create_plan_guide"
dev_langs:
  - "TSQL"
---
# sp_create_plan_guide (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a plan guide for associating query hints or actual query plans with queries in a database. For more information about plan guides, see [Plan Guides](../performance/plan-guides.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_create_plan_guide
    [ @name = ] N'name'
    [ , [ @stmt = ] N'stmt' ]
    , [ @type = ] { N'OBJECT' | N'SQL' | N'TEMPLATE' }
    [ , [ @module_or_batch = ] { N' [ schema_name. ] object_name' | N'batch_text' } ]
    [ , [ @params = ] N'@parameter_name data_type [ ,... n ]' ]
    [ , [ @hints = ] { N'OPTION ( query_hint [ , ...n ] )' | N'XML_showplan' } ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the plan guide. *@name* is **sysname**, with no default, and a maximum length of 124 characters. Plan guide names are scoped to the current database. *@name* must comply with the rules for [identifiers](../databases/database-identifiers.md) and can't start with the number sign (`#`).

#### [ @stmt = ] N'*stmt*'

A [!INCLUDE [tsql](../../includes/tsql-md.md)] statement against which to create a plan guide. *@stmt* is **nvarchar(max)**, with a default of `NULL`. When the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer recognizes a query that matches *@stmt*, *@name* takes effect. For the creation of a plan guide to succeed, *@stmt* must appear in the context specified by the *@type*, *@module_or_batch*, and *@params* parameters.

*@stmt* must be provided in a way that allows for the query optimizer to match it with the corresponding statement, supplied within the batch or module identified by *@module_or_batch* and *@params*. For more information, see the [Remarks](#remarks) section. The size of *@stmt* is limited only by available memory of the server.

#### [ @type = ] { N'OBJECT' | N'SQL' | N'TEMPLATE' }

The type of entity in which *@stmt* appears. This specifies the context for matching *@stmt* to *@name*. *@type* is **nvarchar(60)**, and can be one of these values:

- `OBJECT`

  Indicates *@stmt* appears in the context of a [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedure, scalar function, multistatement table-valued function, or [!INCLUDE [tsql](../../includes/tsql-md.md)] DML trigger in the current database.

- `SQL`

  Indicates *@stmt* appears in the context of a stand-alone statement or batch that can be submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] through any mechanism. [!INCLUDE [tsql](../../includes/tsql-md.md)] statements submitted by common language runtime (CLR) objects or extended stored procedures, or by using `EXEC N'<sql_string>'`, are processed as batches on the server and, therefore, should be identified as *@type* of `SQL`. If `SQL` is specified, the query hint `PARAMETERIZATION { FORCED | SIMPLE }` can't be specified in the *@hints* parameter.

- `TEMPLATE`

  Indicates the plan guide applies to any query that parameterizes to the form indicated in *@stmt*. If `TEMPLATE` is specified, only the `PARAMETERIZATION { FORCED | SIMPLE }` query hint can be specified in the *@hints* parameter. For more information about `TEMPLATE` plan guides, see [Specify Query Parameterization Behavior by Using Plan Guides](../performance/specify-query-parameterization-behavior-by-using-plan-guides.md).

#### [ @module_or_batch = ] { N' [ schema_name. ] object_name' | N'batch_text' }

Specifies either the name of the object in which *@stmt* appears, or the batch text in which *@stmt* appears. *@module_or_batch* is **nvarchar(max)**, with a default of `NULL`. The batch text can't include a `USE <database>` statement.

For a plan guide to match a batch submitted from an application, *@module_or_batch* must be provided in the same format, character-for-character, as it's submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. No internal conversion is performed to facilitate this match. For more information, see the [Remarks](#remarks) section.

`[ <schema_name>. ] <object_name>` specifies the name of a [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedure, scalar function, multistatement table-valued function, or [!INCLUDE [tsql](../../includes/tsql-md.md)] DML trigger that contains *@stmt*. If `<schema_name>` isn't specified, `<schema_name>` uses the schema of the current user. If `NULL` is specified and *@type* is `SQL`, the value of *@module_or_batch* is set to the value of *@stmt*. If *@type* is `TEMPLATE`, *@module_or_batch* must be `NULL`.

#### [ @params = ] N'*@parameter_name* *data_type* [ ,... *n* ]'

Specifies the definitions of all parameters that are embedded in *@stmt*. *@params* is **nvarchar(max)**, with a default of `NULL`. *@params* applies only when either of the following options is true:

- *@type* is `SQL` or `TEMPLATE`. If `TEMPLATE`, *@params* must not be `NULL`.

- *@stmt* is submitted by using `sp_executesql` and a value for the *@params* parameter is specified, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] internally submits a statement after parameterizing it. Submission of parameterized queries from database APIs (including ODBC, OLE DB, and ADO.NET) appears to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as calls to `sp_executesql` or to API server cursor routines; therefore, they can also be matched by `SQL` or `TEMPLATE` plan guides.

*@params* must be supplied in the exact same format as it's submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] either by using `sp_executesql` or submitted internally after parameterization. For more information, see the [Remarks](#remarks) section. If the batch doesn't contain parameters, `NULL` must be specified. The size of *@params* is limited only by available server memory.

#### [ @hints = ] { N'OPTION ( *query_hint* [ , *...n* ] )' | N'*XML_showplan*' }

*@hints* is **nvarchar(max)**, with a default of `NULL`.

- `OPTION ( <query_hint> [ , ...n ] )`

  Specifies an `OPTION` clause to attach to a query that matches *@stmt*. *@hints* must be syntactically the same as an `OPTION` clause in a `SELECT` statement, and can contain any valid sequence of query hints.

- `<XML_showplan>'`

  The query plan in XML format to be applied as a hint.

  We recommend assigning the XML showplan to a variable. Otherwise, you must escape any single quotation marks in the showplan by preceding them with another single quotation mark. See [example E](#e-create-a-plan-guide-by-obtaining-the-xml-showplan-from-a-cached-plan).

- `NULL`

  Indicates that any existing hint specified in the `OPTION` clause of the query isn't applied to the query. For more information, see [OPTION clause](../../t-sql/queries/option-clause-transact-sql.md).

## Remarks

The arguments to `sp_create_plan_guide` must be provided in the order that is shown. When you supply values for the parameters of `sp_create_plan_guide`, all parameter names must be specified explicitly, or none at all. For example, if `@name =` is specified, then `@stmt =`, `@type =`, and so on, must also be specified. Likewise, if `@name =` is omitted and only the parameter value is provided, the remaining parameter names must also be omitted, and only their values provided. Argument names are for descriptive purposes only, to help understand the syntax. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't verify that the specified parameter name matches the name for the parameter in the position where the name is used.

You can create more than one `OBJECT` or `SQL` plan guide for the same query and batch or module. However, only one plan guide can be enabled at any given time.

Plan guides of type `OBJECT` can't be created for an *@module_or_batch* value that references a stored procedure, function, or DML trigger that specifies the `WITH ENCRYPTION` clause or that is temporary.

Trying to drop or modify a function, stored procedure, or DML trigger that is referenced by a plan guide, either enabled or disabled, causes an error. Trying to drop a table that's a trigger defined on it that is referenced by a plan guide also causes an error.

Plan guides can't be used in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md). Plan guides are visible in any edition. You can also attach a database that contains plan guides to any edition. Plan guides remain intact when you restore or attach a database to an upgraded version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You should verify the desirability of the plan guides in each database after performing a server upgrade.

## Plan guide matching requirements

For plan guides that specify *@type* of `SQL` or `TEMPLATE` to successfully match a query, the values for *@module_or_batch* and *@params [, ...n ]* must be provided in exactly the same format as their counterparts submitted by the application. This means you must provide the batch text exactly as the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] compiler receives it. To capture the actual batch and parameter text, you can use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For more information, see [Use SQL Server Profiler to create and test plan guides](../performance/use-sql-server-profiler-to-create-and-test-plan-guides.md).

When *@type* is `SQL` and *@module_or_batch* is set to `NULL`, the value of *@module_or_batch* is set to the value of *@stmt*. This means that the value for *@stmt* must be provided in exactly the same format, character-for-character, as it's submitted to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. No internal conversion is performed to facilitate this match.

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] matches the value of *@stmt* to *@module_or_batch* and *@params [, ...n ]*, or if *@type* is `OBJECT`, to the text of the corresponding query inside `<object_name>`, the following string elements aren't considered:

- White space characters (tabs, spaces, carriage returns, or line feeds) inside the string
- Comments (`--` or `/*  */`)
- Trailing semicolons

For example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can match the *@stmt* string `N'SELECT * FROM T WHERE a = 10'` to the following *@module_or_batch*:

```sql
 N'SELECT *
 FROM T
 WHERE a = 10'
```

However, the same string wouldn't be matched to this *@module_or_batch*:

```sql
N'SELECT * FROM T WHERE b = 10'
```

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ignores the carriage return, line feed, and space characters inside the first query. In the second query, the sequence `WHERE b = 10` is interpreted differently from `WHERE a = 10`. Matching is case-sensitive and accent-sensitive (even when the collation of the database is case-insensitive), except if there are keywords, where case is insensitive. Matching is sensitive to blank spaces. Matching is insensitive to shortened forms of keywords. For example, the keywords `EXECUTE`, `EXEC`, and `execute` are considered equivalent.

## Plan guide effect on the plan cache

Creating a plan guide on a module removes the query plan for that module from the plan cache. Creating a plan guide of type `OBJECT` or `SQL` on a batch removes the query plan for a batch that's the same hash value. Creating a plan guide of type `TEMPLATE` removes all single-statement batches from the plan cache within that database.

## Permissions

To create a plan guide of type `OBJECT`, requires `ALTER` permission on the referenced object. To create a plan guide of type `SQL` or `TEMPLATE`, requires `ALTER` permission on the current database.

## Examples

### A. Create a plan guide of type OBJECT for a query in a stored procedure

The following example creates a plan guide that matches a query executed in the context of an application-based stored procedure, and applies the `OPTIMIZE FOR` hint to the query.

Here's the stored procedure:

```sql
IF OBJECT_ID(N'Sales.GetSalesOrderByCountry', N'P') IS NOT NULL
    DROP PROCEDURE Sales.GetSalesOrderByCountry;
GO

CREATE PROCEDURE Sales.GetSalesOrderByCountry (
    @Country_region NVARCHAR(60)
)
AS
BEGIN
    SELECT *
    FROM Sales.SalesOrderHeader AS h
    INNER JOIN Sales.Customer AS c
        ON h.CustomerID = c.CustomerID
    INNER JOIN Sales.SalesTerritory AS t
        ON c.TerritoryID = t.TerritoryID
    WHERE t.CountryRegionCode = @Country_region;
END
GO
```

Here's the plan guide created on the query in the stored procedure:

```sql
EXEC sp_create_plan_guide
    @name = N'Guide1',
    @stmt = N'SELECT *
              FROM Sales.SalesOrderHeader AS h
              INNER JOIN Sales.Customer AS c
                 ON h.CustomerID = c.CustomerID
              INNER JOIN Sales.SalesTerritory AS t
                 ON c.TerritoryID = t.TerritoryID
              WHERE t.CountryRegionCode = @Country_region',
    @type = N'OBJECT',
    @module_or_batch = N'Sales.GetSalesOrderByCountry',
    @params = NULL,
    @hints = N'OPTION (OPTIMIZE FOR (@Country_region = N''US''))';
```

### B. Create a plan guide of type SQL for a stand-alone query

The following example creates a plan guide to match a query in a batch submitted by an application that uses the `sp_executesql` system stored procedure.

Here's the batch:

```sql
SELECT TOP 1 *
FROM Sales.SalesOrderHeader
ORDER BY OrderDate DESC;
```

To prevent a parallel execution plan from being generated on this query, create the following plan guide:

```sql
EXEC sp_create_plan_guide
    @name = N'Guide1',
    @stmt = N'SELECT TOP 1 *
              FROM Sales.SalesOrderHeader
              ORDER BY OrderDate DESC',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = N'OPTION (MAXDOP 1)';
```

### C. Create a plan guide of type TEMPLATE for the parameterized form of a query

The following example creates a plan guide that matches any query that parameterizes to a specified form, and directs [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to force parameterization of the query. The following two queries are syntactically equivalent, but differ only in their constant literal values.

```sql
SELECT *
FROM AdventureWorks2022.Sales.SalesOrderHeader AS h
INNER JOIN AdventureWorks2022.Sales.SalesOrderDetail AS d
    ON h.SalesOrderID = d.SalesOrderID
WHERE h.SalesOrderID = 45639;

SELECT *
FROM AdventureWorks2022.Sales.SalesOrderHeader AS h
INNER JOIN AdventureWorks2022.Sales.SalesOrderDetail AS d
    ON h.SalesOrderID = d.SalesOrderID
WHERE h.SalesOrderID = 45640;
```

Here's the plan guide on the parameterized form of the query:

```sql
EXEC sp_create_plan_guide
    @name = N'TemplateGuide1',
    @stmt = N'SELECT * FROM AdventureWorks2022.Sales.SalesOrderHeader AS h
              INNER JOIN AdventureWorks2022.Sales.SalesOrderDetail AS d
                  ON h.SalesOrderID = d.SalesOrderID
              WHERE h.SalesOrderID = @0',
    @type = N'TEMPLATE',
    @module_or_batch = NULL,
    @params = N'@0 int',
    @hints = N'OPTION(PARAMETERIZATION FORCED)';
```

In the previous example, the value for the `@stmt` parameter is the parameterized form of the query. The only reliable way to obtain this value for use in `sp_create_plan_guide` is to use the [sp_get_query_template](sp-get-query-template-transact-sql.md) system stored procedure. The following script obtains the parameterized query and then creates a plan guide on it.

```sql
DECLARE @stmt NVARCHAR(MAX);
DECLARE @params NVARCHAR(MAX);

EXEC sp_get_query_template N'SELECT * FROM AdventureWorks2022.Sales.SalesOrderHeader AS h
      INNER JOIN AdventureWorks2022.Sales.SalesOrderDetail AS d
          ON h.SalesOrderID = d.SalesOrderID
      WHERE h.SalesOrderID = 45639;',
    @stmt OUTPUT,
    @params OUTPUT

EXEC sp_create_plan_guide N'TemplateGuide1',
    @stmt,
    N'TEMPLATE',
    NULL,
    @params,
    N'OPTION(PARAMETERIZATION FORCED)';
```

> [!IMPORTANT]  
> The value of the constant literals in the `@stmt` parameter passed to `sp_get_query_template` might affect the data type that is chosen for the parameter that replaces the literal. This will affect plan guide matching. You might have to create more than one plan guide to handle different parameter value ranges.

### D. Create a plan guide on a query submitted by using an API cursor request

Plan guides can match queries that are submitted from API server cursor routines. These routines include `sp_cursorprepare`, `sp_cursorprepexec`, and `sp_cursoropen`. Applications that use the ADO, OLE DB, and ODBC APIs frequently interact with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using API server cursors. You can see the invocation of API server cursor routines in [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] traces by viewing the `RPC:Starting` profiler trace event.

Suppose the following data appears in an `RPC:Starting` profiler trace event for a query you want to tune with a plan guide:

```sql
DECLARE @p1 INT;
SET @p1 = - 1;

DECLARE @p2 INT;
SET @p2 = 0;

DECLARE @p5 INT;
SET @p5 = 4104;

DECLARE @p6 INT;
SET @p6 = 8193;

DECLARE @p7 INT;
SET @p7 = 0;

EXEC sp_cursorprepexec @p1 OUTPUT,
    @p2 OUTPUT,
    N'@P1 varchar(255),@P2 varchar(255)',
    N'SELECT * FROM Sales.SalesOrderHeader AS h INNER JOIN Sales.SalesOrderDetail AS d ON h.SalesOrderID = d.SalesOrderID WHERE h.OrderDate BETWEEN @P1 AND @P2',
    @p5 OUTPUT,
    @p6 OUTPUT,
    @p7 OUTPUT,
    '20040101',
    '20050101'

SELECT @p1, @p2, @p5, @p6, @p7;
```

You notice that the plan for the `SELECT` query in the call to `sp_cursorprepexec` is using a merge join, but you want to use a hash join. The query submitted by using `sp_cursorprepexec` is parameterized, including both a query string and a parameter string. You can create the following plan guide to change the choice of plan by using the query and parameter strings exactly as they appear, character for character, in the call to `sp_cursorprepexec`.

```sql
EXEC sp_create_plan_guide
    @name = N'APICursorGuide',
    @stmt = N'SELECT * FROM Sales.SalesOrderHeader AS h
              INNER JOIN Sales.SalesOrderDetail AS d
                ON h.SalesOrderID = d.SalesOrderID
              WHERE h.OrderDate BETWEEN @P1 AND @P2',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = N'@P1 varchar(255),@P2 varchar(255)',
    @hints = N'OPTION(HASH JOIN)';
```

Subsequent executions of this query by the application are affected by this plan guide, and a hash join is used to process the query.

### E. Create a plan guide by obtaining the XML showplan from a cached plan

The following example creates a plan guide for a simple ad hoc `SQL` statement. The desired query plan for this statement is provided in the plan guide by specifying the XML showplan for the query directly in the `@hints` parameter. The example first executes the `SQL` statement to generate a plan in the plan cache. For the purposes of this example, it's assumed that the generated plan is the desired plan and no further query tuning is required. The XML showplan for the query is obtained by querying the `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, and `sys.dm_exec_text_query_plan` dynamic management views and is assigned to the `@xml_showplan` variable. The `@xml_showplan` variable is then passed to the `sp_create_plan_guide` statement in the `@hints` parameter. Or, you can create a plan guide from a query plan in the plan cache by using the [sp_create_plan_guide_from_handle](sp-create-plan-guide-from-handle-transact-sql.md) stored procedure.

```sql
USE AdventureWorks2022;
GO

SELECT City,
    StateProvinceID,
    PostalCode
FROM Person.Address
ORDER BY PostalCode DESC;
GO

DECLARE @xml_showplan NVARCHAR(MAX);

SET @xml_showplan = (
    SELECT query_plan
    FROM sys.dm_exec_query_stats AS qs
    CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st
    CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, DEFAULT, DEFAULT) AS qp
    WHERE st.TEXT LIKE N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;%'
);

EXEC sp_create_plan_guide @name = N'Guide1_from_XML_showplan',
    @stmt = N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;',
    @type = N'SQL',
    @module_or_batch = NULL,
    @params = NULL,
    @hints = @xml_showplan;
GO
```

## Related content

- [Plan Guides](../performance/plan-guides.md)
- [sp_control_plan_guide (Transact-SQL)](sp-control-plan-guide-transact-sql.md)
- [sys.plan_guides (Transact-SQL)](../system-catalog-views/sys-plan-guides-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.dm_exec_sql_text (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)
- [sys.dm_exec_cached_plans (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)
- [sys.dm_exec_query_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)
- [sp_create_plan_guide_from_handle (Transact-SQL)](sp-create-plan-guide-from-handle-transact-sql.md)
- [sys.fn_validate_plan_guide (Transact-SQL)](../system-functions/sys-fn-validate-plan-guide-transact-sql.md)
- [sp_get_query_template (Transact-SQL)](sp-get-query-template-transact-sql.md)
