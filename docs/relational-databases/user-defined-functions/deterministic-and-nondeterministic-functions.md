---
title: "Deterministic and Nondeterministic Functions"
description: "Deterministic and Nondeterministic Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "built-in functions [SQL Server]"
  - "nondeterministic functions"
  - "extended stored procedures [SQL Server], functions that call"
  - "deterministic functions"
  - "user-defined functions [SQL Server], deterministic"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Deterministic and nondeterministic functions

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Deterministic functions always return the same result any time they're called with a specific set of input values and given the same state of the database. Nondeterministic functions may return different results each time they're called with a specific set of input values even if the database state that they access remains the same. For example, the function AVG always returns the same result given the qualifications stated above, but the GETDATE function, which returns the current datetime value, always returns a different result.

There are several properties of user-defined functions that determine the ability of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to index the results of the function, either through indexes on computed columns that call the function, or through indexed views that reference the function. The determinism of a function is one such property. For example, a clustered index can't be created on a view if the view references any nondeterministic functions. For more information about the properties of functions, including determinism, see [User-Defined Functions](../../relational-databases/user-defined-functions/user-defined-functions.md).

Deterministic functions must be [schema-bound](user-defined-functions.md#SchemaBound). Use the `SCHEMABINDING` clause when creating a deterministic function.

This article identifies the determinism of built-in system functions and the effect on the deterministic property of user-defined functions when it contains a call to extended stored procedures.

### Determining if a function is deterministic

You can check whether a function is deterministic by querying the `is_deterministic` object property for the function. The example below determines if the function `Sales.CalculateSalesTax` is deterministic.


```sql
SELECT OBJECTPROPERTY(OBJECT_ID('Sales.CalculateSalesTax'), 'IsDeterministic');
```

## Built-in function determinism

You can't influence the determinism of any built-in function. Each built-in function is deterministic or nondeterministic based on how the function is implemented by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, specifying an ORDER BY clause in a query doesn't change the determinism of a function that is used in that query.

All of the string built-in functions are deterministic, except for [FORMAT](../../t-sql/functions/format-transact-sql.md). For a list of these functions, see [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md).

The following built-in functions from categories of built-in functions other than string functions are always deterministic.

- ABS
- ACOS
- ASIN
- ATAN
- ATN2
- CEILING
- COALESCE
- COS
- COT
- DATALENGTH
- DATEADD
- DATEDIFF
- DAY
- DEGREES
- EXP
- FLOOR
- ISNULL
- ISNUMERIC
- LOG
- LOG10
- MONTH
- NULLIF
- POWER
- RADIANS
- ROUND
- SIGN
- SIN
- SQRT
- SQUARE
- TAN
- YEAR

The following functions aren't always deterministic, but can be used in indexed views or indexes on computed columns when they're specified in a deterministic manner.

|Function|Comments|
| --- | --- |
|all aggregate functions|All aggregate functions are deterministic unless they're specified with the OVER and ORDER BY clauses. For a list of these functions, see [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md).|
|CAST|Deterministic unless used with **datetime**, **smalldatetime**, or **sql_variant**.|
|CONVERT|Deterministic unless one of these conditions exists:<br /><br />Source type is **sql_variant**.<br /><br /> Target type is **sql_variant** and its source type is nondeterministic.<br /><br /> Source or target type is **datetime** or **smalldatetime**, the other source or target type is a character string, and a nondeterministic style is specified. To be deterministic, the style parameter must be a constant. Additionally, styles less than or equal to 100 are nondeterministic, except for styles 20 and 21. Styles greater than 100 are deterministic, except for styles 106, 107, 109 and 113.|
|CHECKSUM|Deterministic, except for CHECKSUM(*).|
|ISDATE|Deterministic only if used with the CONVERT function, the CONVERT style parameter is specified and style is not equal to 0, 100, 9, or 109.|
|RAND|RAND is deterministic only when a *seed* parameter is specified.|

All the configuration, cursor, metadata, security, and system statistical functions are nondeterministic. You can see a [list of these functions](#see-also).

The following built-in functions from other categories are always nondeterministic.

- @@CONNECTIONS
- @@CPU_BUSY
- @@DBTS
- @@IDLE
- @@IO_BUSY
- @@MAX_CONNECTIONS
- @@PACKET_ERRORS
- @@PACK_RECEIVED
- @@PACK_SENT
- @@TIMETICKS
- @@TOTAL_ERRORS
- @@TOTAL_READ
- @@TOTAL_WRITE
- AT TIME ZONE
- CUME_DIST
- CURRENT_TIMESTAMP
- DENSE_RANK
- FIRST_VALUE
- FORMAT
- GETDATE
- GETUTCDATE
- GET_TRANSMISSION_STATUS
- LAG
- LAST_VALUE
- LEAD
- MIN_ACTIVE_ROWVERSION
- NEWID
- NEWSEQUENTIALID
- NEXT VALUE FOR
- NTILE
- PARSENAME
- PERCENTILE_CONT
- PERCENTILE_DISC
- PERCENT_RANK
- RAND
- RANK
- ROW_NUMBER
- TEXTPTR

## Call extended stored procedures from functions

Functions that call extended stored procedures are nondeterministic, because the extended stored procedures can cause side effects on the database. Side effects are changes to a global state of the database, such as an update to a table, or to an external resource, such as a file or the network; for example, modifying a file or sending an e-mail message. Don't rely on returning a consistent result set when executing an extended stored procedure from a user-defined function. User-defined functions that create side effects on the database aren't recommended.

When called from inside a function, the extended stored procedure can't return result sets to the client. Any Open Data Services API that returns result sets to the client, has a return code of FAIL.

The extended stored procedure can connect back to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, the procedure can't join the same transaction as the original function that invoked the extended stored procedure.

Similar to invocations from a batch or stored procedure, the extended stored procedure is executed in the context of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows security account under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The owner of the extended stored procedure should consider the permissions of this security context, when granting permissions to other users to execute the procedure.

## See also

- [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)
- [Cursor Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cursor-functions-transact-sql.md)
- [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)
- [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)
- [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)
