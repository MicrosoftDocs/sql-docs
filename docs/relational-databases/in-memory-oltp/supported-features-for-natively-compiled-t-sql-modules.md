---
title: "Features for natively compiled T-SQL modules"
description: Learn about T-SQL surface area and supported features in the body of natively compiled T-SQL modules, like stored procedures and scalar user-defined functions.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/01/2020
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom: seo-dt-2019
ms.assetid: 05515013-28b5-4ccf-9a54-ae861448945b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Supported Features for Natively Compiled T-SQL Modules
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]


  This topic contains a list of T-SQL surface area and supported features in the body of natively compiled T-SQL modules, such as stored procedures ([CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)), scalar user-defined functions, inline table-valued functions, and triggers.  

 For supported features around the definition of native modules, see [Supported DDL for Natively Compiled T-SQL modules](../../relational-databases/in-memory-oltp/supported-ddl-for-natively-compiled-t-sql-modules.md).  

 For complete information about unsupported constructs, and for information about how to work around some of the unsupported features in natively compiled modules, see [Migration Issues for Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md). For more information about unsupported features, see [Transact-SQL Constructs Not Supported by In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp.md).  

##  <a name="qsancsp"></a> Query Surface Area in Native Modules  

The following query constructs are supported:  

CASE expression: CASE can be used in any statement or clause that allows a valid expression.
   - **Applies to:** [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)].  
    Beginning with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], CASE statements are now supported for natively compiled T-SQL modules.

SELECT clause:  

-   Columns and  name aliases (using either AS or = syntax).  

-   Scalar subqueries
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], scalar subqueries are now supported in natively compiled modules.

-   TOP*  

-   SELECT DISTINCT  
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], the DISTINCT operator is supported in natively compiled modules.

        - DISTINCT aggregates are not supported.  

-   UNION and UNION ALL
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], UNION and UNION ALL operators are now supported in natively compiled modules.

-   Variable assignments  

FROM clause:  

-   FROM \<memory optimized table or table variable>  

-   FROM \<natively compiled inline TVF>  

-   LEFT OUTER JOIN, RIGHT OUTER JOIN, CROSS JOIN and INNER JOIN.
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], JOINS are now supported in natively compiled modules.

-   Subqueries `[AS] table_alias`. For more information, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md). 
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], Subqueries are now supported in natively compiled modules.

WHERE clause:  

-   Filter predicate IS [NOT] NULL  

-   AND, BETWEEN  
-   OR, NOT, IN, EXISTS
    - **Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)].
      Beginning with [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], OR/NOT/IN/EXISTS operators are now supported in natively compiled modules.


[GROUP BY](../../t-sql/queries/select-group-by-transact-sql.md) clause:

- Aggregate functions AVG, COUNT, COUNT_BIG, MIN, MAX, and SUM.  

- MIN and MAX are not supported for types nvarchar, char, varchar, varchar, varbinary, and binary.  

[ORDER BY](../../t-sql/queries/select-order-by-clause-transact-sql.md) clause:


- There is no support for **DISTINCT** in the **ORDER BY** clause.


- Is supported with [GROUP BY &#40;Transact-SQL&#41;](../../t-sql/queries/select-group-by-transact-sql.md) if an expression in the ORDER BY list appears verbatim in the GROUP BY list.
  - For example, GROUP BY a + b ORDER BY a + b is supported, but GROUP BY a, b ORDER BY a + b is not.  


HAVING clause:

- Is subject to the same expression limitations as the WHERE clause.


#### ORDER BY and TOP are supported in natively compiled modules, with some restrictions


- There is no support for **WITH TIES** or **PERCENT** in the **TOP** clause.


- There is no support for **DISTINCT** in the **ORDER BY** clause.  


- **TOP** combined with **ORDER BY** does not support more than 8,192 when using a constant in the **TOP** clause.
  - This limit may be lowered in case the query contains joins or aggregate functions. (For example, with one join (two tables), the limit is 4,096 rows. With two joins (three tables), the limit is 2,730 rows.)  
  - You can obtain results greater than 8,192 by storing the number of rows in a variable:  

```sql
DECLARE @v INT = 9000;
SELECT TOP (@v) ... FROM ... ORDER BY ...
```

However, a constant in the **TOP** clause results in better performance compared to using a variable.  

These restrictions on natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] do not apply to interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access on memory-optimized tables.  


##  <a name="dml"></a> Data Modification  

The following DML statements are supported.  

-   INSERT VALUES (one row per statement) and INSERT ... SELECT  

-   UPDATE  

-   DELETE  

-   WHERE is supported with UPDATE and DELETE statements.  

##  <a name="cof"></a> Control-of-flow language  
 The following control-of-flow language constructs are supported.  

-   [IF...ELSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/if-else-transact-sql.md)  

-   [WHILE &#40;Transact-SQL&#41;](../../t-sql/language-elements/while-transact-sql.md)  

-   [RETURN &#40;Transact-SQL&#41;](../../t-sql/language-elements/return-transact-sql.md)  

-   [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md) can use all [Supported Data Types for In-Memory OLTP](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md), as well as memory-optimized table types. Variables can be declared as NULL or NOT NULL.  

-   [SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)  

-   [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)  

    - To achieve optimal performance, use a single TRY/CATCH block for an entire natively compiled T-SQL module.  

-   [THROW &#40;Transact-SQL&#41;](../../t-sql/language-elements/throw-transact-sql.md)  

-   BEGIN ATOMIC (at the outer level of the stored procedure). For more detail see [Atomic Blocks](../../relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures.md).  

##  <a name="so"></a> Supported Operators  
 The following operators are supported.  

-   [Comparison Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/comparison-operators-transact-sql.md) (for example, >, \<, >=, and <=)  

-   Unary operators (+, -).  

-   Binary operators (*, /, +, -, % (modulo)).  

    - The plus operator (+) is supported on both numbers and strings.  

-   Logical operators (AND, OR, NOT).  

-   Bitwise operators ~, &, |, and ^  

-   APPLY operator
    - **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].  
      Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the APPLY operator is supported in natively compiled modules.

##  <a name="bfncsp"></a> Built-in Functions in Natively Compiled Modules  
 The following functions are supported in constraints on memory-optimized tables and in natively compiled T-SQL modules.  

-   All [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  

-   Date functions: CURRENT_TIMESTAMP, DATEADD, DATEDIFF, DATEFROMPARTS, DATEPART, DATETIME2FROMPARTS, DATETIMEFROMPARTS, DAY, EOMONTH, GETDATE, GETUTCDATE, MONTH, SMALLDATETIMEFROMPARTS, SYSDATETIME, SYSUTCDATETIME, and YEAR.  

-   String functions: LEN, LTRIM, RTRIM, and SUBSTRING.  
    - **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].  
      Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the following built-in functions are also supported: TRIM, TRANSLATE, and CONCAT_WS.  

-   Identity functions: SCOPE_IDENTITY  

-   NULL functions: ISNULL  

-   Uniqueidentifier functions: NEWID and NEWSEQUENTIALID  

-   JSON functions  
    - **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].  
      Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the JSON functions are supported in natively compiled modules.

-   Error functions: ERROR_LINE, ERROR_MESSAGE, ERROR_NUMBER, ERROR_PROCEDURE, ERROR_SEVERITY, and ERROR_STATE  

-   System Functions: @@rowcount. Statements inside natively compiled stored procedures update @@rowcount and you can use @@rowcount in a natively compiled stored procedure to determine the number of rows affected by the last statement executed within that natively compiled stored procedure. However, @@rowcount is reset to 0 at the start and at the end of the execution of a natively compiled stored procedure.  

-   Security functions: IS_MEMBER({'group' | 'role'}), IS_ROLEMEMBER ('role' [, 'database_principal']), IS_SRVROLEMEMBER ('role' [, 'login']), ORIGINAL_LOGIN(), SESSION_USER, CURRENT_USER, SUSER_ID(['login']), SUSER_SID(['login'] [, Param2]), SUSER_SNAME([server_user_sid]), SYSTEM_USER, SUSER_NAME, USER, USER_ID(['user']), USER_NAME([id]), CONTEXT_INFO().

-   Executions of native modules can be nested.

##  <a name="auditing"></a> Auditing  
 Procedure level auditing is supported in natively compiled stored procedures.  

 For more information about auditing, see [Create a Server Audit and Database Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification.md).  

##  <a name="tqh"></a> Table and Query Hints  
 The following are supported:  

-   INDEX, FORCESCAN, and FORCESEEK hints, either in table hints syntax or in [OPTION Clause &#40;Transact-SQL&#41;](../../t-sql/queries/option-clause-transact-sql.md) of the query. For more information, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  

-   FORCE ORDER  

-   LOOP JOIN hint  

-   OPTIMIZE FOR  

 For more information, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  

##  <a name="los"></a> Limitations on Sorting  
 You can sort greater than 8,000 rows in a query that uses [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md) and an [ORDER BY Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-order-by-clause-transact-sql.md). However, without [ORDER BY Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-order-by-clause-transact-sql.md), [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md) can sort up to 8,000 rows (fewer rows if there are joins).  

 If your query uses both the [TOP &#40;Transact-SQL&#41;](../../t-sql/queries/top-transact-sql.md) operator and an [ORDER BY Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-order-by-clause-transact-sql.md), you can specify up to 8192 rows for the TOP operator. If you specify more than 8192 rows you get the error message: **Msg 41398, Level 16, State 1, Procedure *\<procedureName>*, Line *\<lineNumber>* The TOP operator can return a maximum of 8192 rows; *\<number>* was requested.**  

 If you do not have a TOP clause, you can sort any number of rows with ORDER BY.  

 If you do not use an ORDER BY clause, you can use any integer value with the TOP operator.  

 Example with TOP N = 8192: Compiles  

```sql  
CREATE PROCEDURE testTop  
WITH EXECUTE AS OWNER, SCHEMABINDING, NATIVE_COMPILATION  
  AS  
  BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
    SELECT TOP 8192 ShoppingCartId, CreatedDate, TotalPrice FROM dbo.ShoppingCart  
    ORDER BY ShoppingCartId DESC  
  END;  
GO  
```

 Example with TOP N > 8192: Fails to compile.  

```sql  
CREATE PROCEDURE testTop  
WITH EXECUTE AS OWNER, SCHEMABINDING, NATIVE_COMPILATION  
  AS  
  BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
    SELECT TOP 8193 ShoppingCartId, CreatedDate, TotalPrice FROM dbo.ShoppingCart  
    ORDER BY ShoppingCartId DESC  
  END;  
GO  
```

 The 8192 row limitation only applies to `TOP N` where `N` is a constant, as in the preceding examples.  If you need `N` greater than 8192 you can assign the value to a variable and use that variable with `TOP`.  

 Example using a variable: Compiles  

```sql  
CREATE PROCEDURE testTop  
WITH EXECUTE AS OWNER, SCHEMABINDING, NATIVE_COMPILATION  
  AS  
  BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
    DECLARE @v int = 8193   
    SELECT TOP (@v) ShoppingCartId, CreatedDate, TotalPrice FROM dbo.ShoppingCart  
    ORDER BY ShoppingCartId DESC  
  END;  
GO  
```

 **Limitations on rows returned:** There are two cases where that can potentially reduce the number of rows that can be returned by the TOP operator:  

-   Using JOINs in the query.  The influence of JOINs on the limitation depends on the query plan.  

-   Using aggregate functions or references to aggregate functions in the ORDER BY clause.  

 The formula to calculate a worst case maximum supported N in TOP N is: `N = floor ( 65536 / number_of_tables * 8 + total_size+of+aggs )`.  

## See Also  
 [Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)   
 [Migration Issues for Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)
