---
title: "What are the Microsoft SQL database functions? | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "built-in functions [SQL Server]"
  - "function [SQL Server] See functions [SQL Server]"
  - "functions [Transact-SQL]"
  - "functions [SQL Server], about functions"
  - "scalar functions"
  - "functions [SQL Server]"
ms.assetid: 17186213-5ab5-40b0-b470-b660af1ec44c
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# What are the SQL database functions?
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Learn about the categories of built-in functions you can use with SQL databases. You can use the built-in functions or create your own user-defined functions.
  
## Aggregate functions

Aggregate functions perform a calculation on a set of values and return a single value. They are allowed in the select list or the HAVING clause of a SELECT statement. You can use an aggregation in combination with the GROUP BY clause to calculate the aggregation on categories of rows. Use the OVER clause to calculate the aggregation on a specific range of value. The OVER clause cannot follow the GROUPING or GROUPING_ID aggregations.

All aggregate functions are deterministic, which means they always return the same value when they run on the same input values. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).|

## Analytic functions
Analytic functions compute an aggregate value based on a group of rows. However, unlike aggregate functions, analytic functions can return multiple rows for each group. You can use analytic functions to compute moving averages, running totals, percentages, or top-N results within a group.

## Ranking functions
Ranking functions return a ranking value for each row in a partition. Depending on the function that is used, some rows might receive the same value as other rows. Ranking functions are nondeterministic.

## Rowset functions
Rowset functions Return an object that can be used like table references in an SQL statement.

## Scalar functions
Operate on a single value and then return a single value. Scalar functions can be used wherever an expression is valid.

### Categories of scalar functions
  
|Function category|Description|  
|-----------------------|-----------------|  
|[Configuration Functions](configuration-functions-transact-sql.md)|Return information about the current configuration.|  
|[Conversion Functions](conversion-functions-transact-sql.md)|Support data type casting and converting.|  
|[Cursor Functions](cursor-functions-transact-sql.md)|Return information about cursors.|  
|[Date and Time Data Types and Functions](date-and-time-data-types-and-functions-transact-sql.md)|Perform operations on a date and time input values and return string, numeric, or date and time values.|  
|[JSON Functions](json-functions-transact-sql.md)|Validate, query, or change JSON data.|  
|[Logical Functions](https://msdn.microsoft.com/library/5b2b4546-951b-462d-91d5-e41fc5acd6f9)|Perform logical operations.|  
|[Mathematical Functions](mathematical-functions-transact-sql.md)|Perform calculations based on input values provided as parameters to the functions, and return numeric values.|  
|[Metadata Functions](metadata-functions-transact-sql.md)|Return information about the database and database objects.|  
|[Security Functions](security-functions-transact-sql.md)|Return information about users and roles.|  
|[String Functions](string-functions-transact-sql.md)|Perform operations on a string (**char** or **varchar**) input value and return a string or numeric value.|  
|[System Functions](../../relational-databases/system-functions/system-functions-for-transact-sql.md)|Perform operations and return information about values, objects, and settings in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[System Statistical Functions](system-statistical-functions-transact-sql.md)|Return statistical information about the system.|  
|[Text and Image Functions](https://msdn.microsoft.com/library/b9c70488-1bf5-4068-a003-e548ccbc5199)|Perform operations on text or image input values or columns, and return information about the value.|  
  
## Function Determinism  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] built-in functions are either deterministic or nondeterministic. Functions are deterministic when they always return the same result any time they are called by using a specific set of input values. Functions are nondeterministic when they could return different results every time they are called, even with the same specific set of input values. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md)  
  
## Function Collation  
 Functions that take a character string input and return a character string output use the collation of the input string for the output.  
  
 Functions that take non-character inputs and return a character string use the default collation of the current database for the output.  
  
 Functions that take multiple character string inputs and return a character string use the rules of collation precedence to set the collation of the output string. For more information, see [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md).  
  
## See Also  
 [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)   
 [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md)   
 [Using Stored Procedures &#40;MDX&#41;](../../mdx/using-stored-procedures-mdx.md)  
  
  
