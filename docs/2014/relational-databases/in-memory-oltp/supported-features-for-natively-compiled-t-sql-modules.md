---
title: "Supported Constructs in Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 05515013-28b5-4ccf-9a54-ae861448945b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported Constructs in Natively Compiled Stored Procedures
  This topic contains a list of supported features for natively compiled stored procedures ([CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql)):  
  
-   [Programmability in Natively Compiled Stored Procedures](#pncsp)  
  
-   [Supported Operators](#so)  
  
-   [Built-in Functions in Natively Compiled Stored Procedures](#bfncsp)  
  
-   [Query Surface Area in Natively Compiled Stored Procedures](#qsancsp)  
  
-   [Auditing](#auditing)  
  
-   [Table, Query, and Join Hints](#tqh)  
  
-   [Limitations on Sorting](#los)  
  
 For information on data types supported in natively compiled stored procedures, see [Supported Data Types](supported-data-types-for-in-memory-oltp.md).  
  
 For complete information about unsupported constructs, and for information about how to work around some of the unsupported features in natively compiled stored procedures, see [Migration Issues for Natively Compiled Stored Procedures](migration-issues-for-natively-compiled-stored-procedures.md). For more information about unsupported features, see [Transact-SQL Constructs Not Supported by In-Memory OLTP](transact-sql-constructs-not-supported-by-in-memory-oltp.md).  
  
##  <a name="pncsp"></a> Programmability in Natively Compiled Stored Procedures  
 The following are supported:  
  
-   BEGIN ATOMIC (at the outer level of the stored procedure), LANGUAGE, ISOLATION LEVEL, DATEFORMAT, and DATEFIRST.  
  
-   Declaring variables as NULL or NOT NULL. If a variable is declared as NOT NULL, the declaration must have an initializer. If a variable is not declared as NOT NULL, an initializer is optional.  
  
-   IF and WHILE  
  
-   INSERT/UPDATE/DELETE  
  
     Subqueries are not supported. In the WHERE or HAVING clause, AND and BETWEEN are supported; OR, NOT, and IN are not supported.  
  
-   Memory-optimized table types and table variables.  
  
-   RETURN  
  
-   SELECT  
  
-   SET  
  
-   TRY/CATCH/THROW  
  
     To optimize performance, use a single TRY/CATCH block for an entire natively compiled stored procedure.  
  
##  <a name="so"></a> Supported Operators  
 The following operators are supported.  
  
-   [Comparison Operators &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/comparison-operators-transact-sql) (for example, >, \<, >=, and <=) are supported in conditionals (IF, WHILE).  
  
-   Unary operators (+, -).  
  
-   Binary operators (*, /, +, -, % (modulo)).  
  
     The plus operator (+) is supported on both numbers and strings.  
  
-   Logical operators (AND, OR, NOT). OR and NOT are supported in IF and WHILE statements but not in WHERE or HAVING clauses.  
  
-   Bitwise operators ~, &, |, and ^  
  
##  <a name="bfncsp"></a> Built-in Functions in Natively Compiled Stored Procedures  
 The following functions are supported in default constraints on memory-optimized tables and in natively compiled stored procedures.  
  
-   Math Functions: ACOS, ASIN, ATAN, ATN2, COS, COT, DEGREES, EXP, LOG, LOG10, PI, POWER, RADIANS, RAND, SIN, SQRT, SQUARE, and TAN  
  
-   Date Functions: CURRENT_TIMESTAMP, DATEADD, DATEDIFF, DATEFROMPARTS, DATEPART, DATETIME2FROMPARTS, DATETIMEFROMPARTS, DAY, EOMONTH, GETDATE, GETUTCDATE, MONTH, SMALLDATETIMEFROMPARTS, SYSDATETIME, SYSUTCDATETIME, and YEAR.  
  
-   String Functions: LEN, LTRIM, RTRIM, and SUBSTRING  
  
-   Identity Function: SCOPE_IDENTITY  
  
-   NULL functions: ISNULL  
  
-   Uniqueidentifier functions: NEWID and NEWSEQUENTIALID  
  
-   Error Functions: ERROR_LINE, ERROR_MESSAGE, ERROR_NUMBER, ERROR_PROCEDURE, ERROR_SEVERITY, and ERROR_STATE  
  
-   Conversions: CAST and CONVERT. Conversions between Unicode and non-Unicode character strings (n(var)char and (var)char) are not supported.  
  
-   System Functions: @@rowcount. Statements inside natively compiled stored procedures update @@rowcount and you can use @@rowcount in a natively compiled stored procedure to determine the number of rows affected by the last statement executed within that natively compiled stored procedure. However, @@rowcount is reset to 0 at the start and at the end of the execution of a natively compiled stored procedure.  
  
##  <a name="qsancsp"></a> Query Surface Area in Natively Compiled Stored Procedures  
 The following are supported:  
  
-   BETWEEN  
  
-   Column name aliases (using either AS or = syntax).  
  
-   CROSS JOIN and INNER JOIN, supported only with SELECT queries.  
  
-   Expressions are supported in SELECT list and [WHERE &#40;Transact-SQL&#41;](/sql/t-sql/queries/where-transact-sql) clause if they use a supported operator. See [Supported Operators](#so) for the list of currently-supported operators.  
  
-   Filter predicate IS [NOT] NULL  
  
-   FROM \<memory optimized table>  
  
-   [GROUP BY &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-group-by-transact-sql) is supported, along with the aggregate functions AVG, COUNT, COUNT_BIG, MIN, MAX, and SUM. MIN and MAX are not supported for types nvarchar, char, varchar, varchar, varbinary, and binary. [ORDER BY Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-order-by-clause-transact-sql) is supported with [GROUP BY &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-group-by-transact-sql) if an expression in the ORDER BY list appears verbatim in the GROUP BY list. For example, GROUP BY a + b ORDER BY a + b is supported, but GROUP BY a, b ORDER BY a + b is not.  
  
-   HAVING, subject to the same expression limitations as the WHERE clause.  
  
-   INSERT VALUES (one row per statement) and INSERT SELECT  
  
-   ORDER BY <sup>1</sup>  
  
-   Predicates not referencing a column.  
  
-   SELECT, UPDATE, and DELETE  
  
-   TOP <sup>1</sup>  
  
-   Variable assignment in the SELECT list.  
  
-   WHERE ... AND  
  
 <sup>1</sup> ORDER BY and TOP are supported in natively compiled stored procedures, with some restrictions:  
  
-   There is no support for `DISTINCT` in the `SELECT` or `ORDER BY` clause.  
  
-   There is no support for `WITH TIES` or `PERCENT` in the `TOP` clause.  
  
-   `TOP` combined with `ORDER BY` does not support more than 8,192 when using a constant in the `TOP` clause. This limit may be lowered in case the query contains joins or aggregate functions. (For example, with one join (two tables), the limit is 4,096 rows. With two joins (three tables), the limit is 2,730 rows.)  
  
     You can obtain results greater than 8,192 by storing the number of rows in a variable:  
  
    ```tsql  
    DECLARE @v INT = 9000  
    SELECT TOP (@v) ... FROM ... ORDER BY ...  
    ```  
  
 However, a constant in the `TOP` clause results in better performance compared to using a variable.  
  
 These restrictions do not apply to interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access on memory-optimized tables.  
  
##  <a name="auditing"></a> Auditing  
 Procedure level auditing is supported in natively compiled stored procedures. Statement level auditing is not supported.  
  
 For more information about auditing, see [Create a Server Audit and Database Audit Specification](../security/auditing/create-a-server-audit-and-database-audit-specification.md).  
  
##  <a name="tqh"></a> Table, Query, and Join Hints  
 The following are supported:  
  
-   INDEX, FORCESCAN, and FORCESEEK hints, either in table hints syntax or in [OPTION Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/option-clause-transact-sql) of the query.  
  
-   FORCE ORDER  
  
-   INNER LOOP JOIN  
  
-   OPTIMIZE FOR  
  
 For more information, see [Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql).  
  
##  <a name="los"></a> Limitations on Sorting  
 You can sort greater than 8,000 rows in a query that uses [TOP &#40;Transact-SQL&#41;](/sql/t-sql/queries/top-transact-sql) and an [ORDER BY Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-order-by-clause-transact-sql). However, without [ORDER BY Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-order-by-clause-transact-sql), [TOP &#40;Transact-SQL&#41;](/sql/t-sql/queries/top-transact-sql) can sort up to 8,000 rows (fewer rows if there are joins).  
  
 If your query uses both the [TOP &#40;Transact-SQL&#41;](/sql/t-sql/queries/top-transact-sql) operator and an [ORDER BY Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-order-by-clause-transact-sql), you can specify up to 8192 rows for the TOP operator. If you specify more than 8192 rows you get the error message: **Msg 41398, Level 16, State 1, Procedure *\<procedureName>*, Line *\<lineNumber>* The TOP operator can return a maximum of 8192 rows; *\<number>* was requested.**  
  
 If you do not have a TOP clause, you can sort any number of rows with ORDER BY.  
  
 If you do not use an ORDER BY clause, you can use any integer value with the TOP operator.  
  
 Example with TOP N = 8192: Compiles  
  
```tsql  
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
  
```tsql  
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
  
```tsql  
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
 [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)   
 [Migration Issues for Natively Compiled Stored Procedures](migration-issues-for-natively-compiled-stored-procedures.md)  
  
  
