---
title: "Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 38
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Functions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides many built-in functions and also lets you create user-defined functions. The categories of built-in functions are listed on this page.  
  
## Types of Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Rowset Functions](../../t-sql/functions/rowset-functions-transact-sql.md)|Return an object that can be used like table references in an SQL statement.|  
|[Aggregate Functions](../../t-sql/functions/aggregate-functions-transact-sql.md)|Operate on a collection of values but return a single, summarizing value.|  
|[Ranking Functions](../../t-sql/functions/ranking-functions-transact-sql.md)|Return a ranking value for each row in a partition.|  
|Scalar Functions (Described below)|Operate on a single value and then return a single value. Scalar functions can be used wherever an expression is valid.|  
  
## Scalar Functions  
  
|Function category|Description|  
|-----------------------|-----------------|  
|[Configuration Functions](../../t-sql/functions/configuration-functions-transact-sql.md)|Return information about the current configuration.|  
|[Conversion Functions](../../t-sql/functions/conversion-functions-transact-sql.md)|Support data type casting and converting.|  
|[Cursor Functions](../../t-sql/functions/cursor-functions-transact-sql.md)|Return information about cursors.|  
|[Date and Time Data Types and Functions](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)|Perform operations on a date and time input values and return string, numeric, or date and time values.|  
|[JSON Functions](../../t-sql/functions/json-functions-transact-sql.md)|Validate, query, or change JSON data.|  
|[Logical Functions](http://msdn.microsoft.com/library/5b2b4546-951b-462d-91d5-e41fc5acd6f9)|Perform logical operations.|  
|[Mathematical Functions](../../t-sql/functions/mathematical-functions-transact-sql.md)|Perform calculations based on input values provided as parameters to the functions, and return numeric values.|  
|[Metadata Functions](../../t-sql/functions/metadata-functions-transact-sql.md)|Return information about the database and database objects.|  
|[Security Functions](../../t-sql/functions/security-functions-transact-sql.md)|Return information about users and roles.|  
|[String Functions](../../t-sql/functions/string-functions-transact-sql.md)|Perform operations on a string (**char** or **varchar**) input value and return a string or numeric value.|  
|[System Functions](../../relational-databases/system-functions/system-functions-for-transact-sql.md)|Perform operations and return information about values, objects, and settings in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[System Statistical Functions](../../t-sql/functions/system-statistical-functions-transact-sql.md)|Return statistical information about the system.|  
|[Text and Image Functions](http://msdn.microsoft.com/library/b9c70488-1bf5-4068-a003-e548ccbc5199)|Perform operations on text or image input values or columns, and return information about the value.|  
  
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
  
  