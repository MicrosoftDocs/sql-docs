---
title: "sp_create_plan_guide (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_create_plan_guide"
  - "sp_create_plan_guide_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_create_plan_guide"
ms.assetid: 5a8c8040-4f96-4c74-93ab-15bdefd132f0
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_create_plan_guide (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a plan guide for associating query hints or actual query plans with queries in a database. For more information about plan guides, see [Plan Guides](../../relational-databases/performance/plan-guides.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_create_plan_guide [ @name = ] N'plan_guide_name'  
    , [ @stmt = ] N'statement_text'  
    , [ @type = ] N'{ OBJECT | SQL | TEMPLATE }'  
    , [ @module_or_batch = ]  
      {   
                    N'[ schema_name. ] object_name'  
        | N'batch_text'  
        | NULL  
      }  
    , [ @params = ] { N'@parameter_name data_type [ ,...n ]' | NULL }   
    , [ @hints = ] { N'OPTION ( query_hint [ ,...n ] )'   
                 | N'XML_showplan'  
                 | NULL }  
```  
  
## Arguments  
 [ \@name = ] N'*plan_guide_name*'  
 Is the name of the plan guide. Plan guide names are scoped to the current database. *plan_guide_name* must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md) and cannot start with the number sign (#). The maximum length of *plan_guide_name* is 124 characters.  
  
 [ \@stmt = ] N'*statement_text*'  
 Is a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement against which to create a plan guide. When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer recognizes a query that matches *statement_text*, *plan_guide_name* takes effect. For the creation of a plan guide to succeed, *statement_text* must appear in the context specified by the \@type, \@module_or_batch, and \@params parameters.  
  
 *statement_text* must be provided in a way that allows for the query optimizer to match it with the corresponding statement supplied within the batch or module identified by \@module_or_batch and \@params. For more information, see the "Remarks" section. The size of *statement_text* is limited only by available memory of the server.  
  
 [\@type = ]N'{ OBJECT | SQL | TEMPLATE }'  
 Is the type of entity in which *statement_text* appears. This specifies the context for matching *statement_text* to *plan_guide_name*.  
  
 OBJECT  
 Indicates *statement_text* appears in the context of a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, scalar function, multistatement table-valued function, or [!INCLUDE[tsql](../../includes/tsql-md.md)] DML trigger in the current database.  
  
 SQL  
 Indicates *statement_text* appears in the context of a stand-alone statement or batch that can be submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through any mechanism. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements submitted by common language runtime (CLR) objects or extended stored procedures, or by using EXEC N'*sql_string*', are processed as batches on the server and, therefore, should be identified as \@type **=** 'SQL'. If SQL is specified, the query hint PARAMETERIZATION { FORCED | SIMPLE } cannot be specified in the \@hints parameter.  
  
 TEMPLATE  
 Indicates the plan guide applies to any query that parameterizes to the form indicated in *statement_text*. If TEMPLATE is specified, only the PARAMETERIZATION { FORCED | SIMPLE } query hint can be specified in the \@hints parameter. For more information about TEMPLATE plan guides, see [Specify Query Parameterization Behavior by Using Plan Guides](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md).  
  
 [\@module_or_batch =]{ N'[ *schema_name*. ] *object_name*' | N'*batch_text*' | NULL }  
 Specifies either the name of the object in which *statement_text* appears, or the batch text in which *statement_text* appears. The batch text cannot include a USE*database* statement.  
  
 For a plan guide to match a batch submitted from an application, *batch_tex*t must be provided in the same format, character-for-character, as it is submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. No internal conversion is performed to facilitate this match. For more information, see the Remarks section.  
  
 [*schema_name*.]*object_name* specifies the name of a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, scalar function, multistatement table-valued function, or [!INCLUDE[tsql](../../includes/tsql-md.md)] DML trigger that contains *statement_text*. If *schema_name* is not specified, *schema_name* uses the schema of the current user. If NULL is specified and \@type = 'SQL', the value of \@module_or_batch is set to the value of \@stmt. If \@type = 'TEMPLATE**'**, \@module_or_batch must be NULL.  
  
 [ \@params = ]{ N'*\@parameter_name data_type* [ ,*...n* ]' | NULL }  
 Specifies the definitions of all parameters that are embedded in *statement_text*. \@params applies only when either of the following is true:  
  
-   \@type = 'SQL' or 'TEMPLATE'. If 'TEMPLATE', \@params must not be NULL.  
  
-   *statement_text* is submitted by using sp_executesql and a value for the \@params parameter is specified, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internally submits a statement after parameterizing it. Submission of parameterized queries from database APIs (including ODBC, OLE DB, and ADO.NET) appear to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as calls to sp_executesql or to API server cursor routines; therefore, they can also be matched by SQL or TEMPLATE plan guides.  
  
 *\@parameter_name data_type* must be supplied in the exact same format as it is submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either by using sp_executesql or submitted internally after parameterization. For more information, see the Remarks section. If the batch does not contain parameters, NULL must be specified. The size of \@params is limited only by available server memory.  
  
 [\@hints = ]{ N'OPTION (*query_hint* [ ,*...n* ] )' | N'*XML_showplan*' | NULL }  
 N'OPTION (*query_hint* [ ,*...n* ] )  
 Specifies an OPTION clause to attach to a query that matches \@stmt. \@hints must be syntactically the same as an OPTION clause in a SELECT statement, and can contain any valid sequence of query hints.  
  
 N'*XML_showplan*'  
 Is the query plan in XML format to be applied as a hint.  
  
 We recommend assigning the XML Showplan to a variable; otherwise, you must escape any single quotation marks in the Showplan by preceding them with another single quotation mark. See example E.  
  
 NULL  
 Indicates that any existing hint specified in the OPTION clause of the query is not applied to the query. For more information, see [OPTION Clause &#40;Transact-SQL&#41;](../../t-sql/queries/option-clause-transact-sql.md).  
  
## Remarks  
 The arguments to sp_create_plan_guide must be provided in the order that is shown. When you supply values for the parameters of **sp_create_plan_guide**, all parameter names must be specified explicitly, or none at all. For example, if **\@name =** is specified, then **\@stmt =** , **\@type =**, and so on, must also be specified. Likewise, if **\@name =** is omitted and only the parameter value is provided, the remaining parameter names must also be omitted, and only their values provided. Argument names are for descriptive purposes only, to help understand the syntax. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not verify that the specified parameter name matches the name for the parameter in the position where the name is used.  
  
 You can create more than one OBJECT or SQL plan guide for the same query and batch or module. However, only one plan guide can be enabled at any given time.  
  
 Plan guides of type OBJECT cannot be created for an \@module_or_batch value that references a stored procedure, function, or DML trigger that specifies the WITH ENCRYPTION clause or that is temporary.  
  
 Trying to drop or modify a function, stored procedure, or DML trigger that is referenced by a plan guide, either enabled or disabled, causes an error. Trying to drop a table that has a trigger defined on it that is referenced by a plan guide also causes an error.  
  
> [!NOTE]
>  Plan guides cannot be used in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md). Plan guides are visible in any edition. You can also attach a database that contains plan guides to any edition. Plan guides remain intact when you restore or attach a database to an upgraded version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You should verify the desirability of the plan guides in each database after performing a server upgrade.  
  
## Plan Guide Matching Requirements  
 For plan guides that specify \@type = 'SQL' or \@type = 'TEMPLATE' to successfully match a query, the values for *batch_text* and *\@parameter_name data_type* [,*...n* ] must be provided in exactly the same format as their counterparts submitted by the application. This means you must provide the batch text exactly as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compiler receives it. To capture the actual batch and parameter text, you can use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. For more information, see [Use SQL Server Profiler to Create and Test Plan Guides](../../relational-databases/performance/use-sql-server-profiler-to-create-and-test-plan-guides.md).  
  
 When \@type = 'SQL' and \@module_or_batch is set to NULL, the value of \@module_or_batch is set to the value of \@stmt. This means that the value for *statement_text* must be provided in exactly the same format, character-for-character, as it is submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. No internal conversion is performed to facilitate this match.  
  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] matches the value of *statement_text* to *batch_text* and *\@parameter_name data_type* [,*...n* ], or if \@type = **'**OBJECT', to the text of the corresponding query inside *object_name*, the following string elements are not considered:  
  
-   White space characters (tabs, spaces, carriage returns, or line feeds) inside the string.  
  
-   Comments (**--** or **/\*   \*/**).  
  
-   Trailing semicolons  
  
 For example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can match the *statement_text* string `N'SELECT * FROM T WHERE a = 10'` to the following *batch_text*:  
  
 `N'SELECT *`  
  
 `FROM T`  
  
 `WHERE a=10'`  
  
 However, the same string would not be matched to this *batch_text*:  
  
 `N'SELECT * FROM T WHERE b = 10'`  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ignores the carriage return, line feed, and space characters inside the first query. In the second query, the sequence `WHERE b = 10` is interpreted differently from `WHERE a = 10`. Matching is case- and accent-sensitive (even when the collation of the database is case-insensitive), except in the case of keywords, where case is insensitive. Matching is insensitive to shortened forms of keywords. For example, the keywords `EXECUTE`, `EXEC`, and `execute` are considered equivalent.  
  
## Plan Guide Effect on the Plan Cache  
 Creating a plan guide on a module removes the query plan for that module from the plan cache. Creating a plan guide of type OBJECT or SQL on a batch removes the query plan for a batch that has the same hash value. Creating a plan guide of type TEMPLATE removes all single-statement batches from the plan cache within that database.  
  
## Permissions  
 To create a plan guide of type OBJECT, requires ALTER permission on the referenced object. To create a plan guide of type SQL or TEMPLATE, requires ALTER permission on the current database.  
  
## Examples  
  
### A. Creating a plan guide of type OBJECT for a query in a stored procedure  
 The following example creates a plan guide that matches a query executed in the context of an application-based stored procedure, and applies the `OPTIMIZE FOR` hint to the query.  
  
 Here is the stored procedure:  
  
```  
IF OBJECT_ID(N'Sales.GetSalesOrderByCountry', N'P') IS NOT NULL  
    DROP PROCEDURE Sales.GetSalesOrderByCountry;  
GO  
CREATE PROCEDURE Sales.GetSalesOrderByCountry   
    (@Country_region nvarchar(60))  
AS  
BEGIN  
    SELECT *  
    FROM Sales.SalesOrderHeader AS h   
    INNER JOIN Sales.Customer AS c ON h.CustomerID = c.CustomerID  
    INNER JOIN Sales.SalesTerritory AS t   
        ON c.TerritoryID = t.TerritoryID  
    WHERE t.CountryRegionCode = @Country_region;  
END  
GO  
```  
  
 Here is the plan guide created on the query in the stored procedure:  
  
```  
EXEC sp_create_plan_guide   
    @name =  N'Guide1',  
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
  
### B. Creating a plan guide of type SQL for a stand-alone query  
 The following example creates a plan guide to match a query in a batch submitted by an application that uses the sp_executesql system stored procedure.  
  
 Here is the batch:  
  
```  
SELECT TOP 1 * FROM Sales.SalesOrderHeader ORDER BY OrderDate DESC;  
```  
  
 To prevent a parallel execution plan from being generated on this query, create the following plan guide:  
  
```  
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
  
### C. Creating a plan guide of type TEMPLATE for the parameterized form of a query  
 The following example creates a plan guide that matches any query that parameterizes to a specified form, and directs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to force parameterization of the query. The following two queries are syntactically equivalent, but differ only in their constant literal values.  
  
```  
SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
    ON h.SalesOrderID = d.SalesOrderID  
WHERE h.SalesOrderID = 45639;  
  
SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
    ON h.SalesOrderID = d.SalesOrderID  
WHERE h.SalesOrderID = 45640;  
```  
  
 Here is the plan guide on the parameterized form of the query:  
  
```  
EXEC sp_create_plan_guide   
    @name = N'TemplateGuide1',  
    @stmt = N'SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
              INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
                  ON h.SalesOrderID = d.SalesOrderID  
              WHERE h.SalesOrderID = @0',  
    @type = N'TEMPLATE',  
    @module_or_batch = NULL,  
    @params = N'@0 int',  
    @hints = N'OPTION(PARAMETERIZATION FORCED)';  
```  
  
 In the previous example, the value for the `@stmt` parameter is the parameterized form of the query. The only reliable way to obtain this value for use in sp_create_plan_guide is to use the [sp_get_query_template](../../relational-databases/system-stored-procedures/sp-get-query-template-transact-sql.md) system stored procedure. The following script can be used both to obtain the parameterized query and then create a plan guide on it.  
  
```  
DECLARE @stmt nvarchar(max);  
DECLARE @params nvarchar(max);  
EXEC sp_get_query_template   
    N'SELECT * FROM AdventureWorks2012.Sales.SalesOrderHeader AS h  
      INNER JOIN AdventureWorks2012.Sales.SalesOrderDetail AS d   
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
>  The value of the constant literals in the `@stmt` parameter passed to sp_get_query_template might affect the data type that is chosen for the parameter that replaces the literal. This will affect plan guide matching. You may have to create more than one plan guide to handle different parameter value ranges.  
  
### D. Creating a plan guide on a query submitted by using an API cursor request  
 Plan guides can match queries that are submitted from API server cursor routines. These routines include sp_cursorprepare, sp_cursorprepexec, and sp_cursoropen. Applications that use the ADO, OLE DB, and ODBC APIs frequently interact with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using API server cursors. You can see the invocation of API server cursor routines in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] traces by viewing the RPC:Starting profiler trace event.  
  
 Suppose the following data appears in an RPC:Starting profiler trace event for a query you want to tune with a plan guide:  
  
```  
DECLARE @p1 int;  
SET @p1=-1;  
DECLARE @p2 int;  
SET @p2=0;  
DECLARE @p5 int;  
SET @p5=4104;  
DECLARE @p6 int;  
SET @p6=8193;  
DECLARE @p7 int;  
SET @p7=0;  
EXEC sp_cursorprepexec @p1 output,@p2 output,N'@P1 varchar(255),@P2 varchar(255)',N'SELECT * FROM Sales.SalesOrderHeader AS h INNER JOIN Sales.SalesOrderDetail AS d ON h.SalesOrderID = d.SalesOrderID WHERE h.OrderDate BETWEEN @P1 AND @P2',@p5 OUTPUT,@p6 OUTPUT,@p7 OUTPUT,'20040101','20050101'  
SELECT @p1, @p2, @p5, @p6, @p7;  
```  
  
 You notice that the plan for the `SELECT` query in the call to `sp_cursorprepexec` is using a merge join, but you want to use a hash join. The query submitted by using `sp_cursorprepexec` is parameterized, including both a query string and a parameter string. You can create the following plan guide to change the choice of plan by using the query and parameter strings exactly as they appear, character for character, in the call to `sp_cursorprepexec`.  
  
```  
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
  
 Subsequent executions of this query by the application will be affected by this plan guide, and a hash join will be used to process the query.  
  
### E. Creating a plan guide by obtaining the XML Showplan from a cached plan  
 The following example creates a plan guide for a simple ad hoc SQL statement. The desired query plan for this statement is provided in the plan guide by specifying the XML Showplan for the query directly in the `@hints` parameter. The example first executes the SQL statement to generate a plan in the plan cache. For the purposes of this example, it is assumed that the generated plan is the desired plan and no additional query tuning is required. The XML Showplan for the query is obtained by querying the `sys.dm_exec_query_stats`, `sys.dm_exec_sql_text`, and `sys.dm_exec_text_query_plan` dynamic management views and is assigned to the `@xml_showplan` variable. The `@xml_showplan` variable is then passed to the `sp_create_plan_guide` statement in the `@hints` parameter. Or, you can create a plan guide from a query plan in the plan cache by using the [sp_create_plan_guide_from_handle](../../relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md) stored procedure.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;  
GO  
DECLARE @xml_showplan nvarchar(max);  
SET @xml_showplan = (SELECT query_plan  
    FROM sys.dm_exec_query_stats AS qs   
    CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st  
    CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, DEFAULT, DEFAULT) AS qp  
    WHERE st.text LIKE N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;%');  
  
EXEC sp_create_plan_guide   
    @name = N'Guide1_from_XML_showplan',   
    @stmt = N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;',   
    @type = N'SQL',  
    @module_or_batch = NULL,   
    @params = NULL,   
    @hints =@xml_showplan;  
GO  
```  
  
## See Also  
 [Plan Guides](../../relational-databases/performance/plan-guides.md)   
 [sp_control_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-control-plan-guide-transact-sql.md)   
 [sys.plan_guides &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-plan-guides-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)   
 [sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)   
 [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)   
 [sys.fn_validate_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-validate-plan-guide-transact-sql.md)   
 [sp_get_query_template &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-query-template-transact-sql.md)  
  
  
