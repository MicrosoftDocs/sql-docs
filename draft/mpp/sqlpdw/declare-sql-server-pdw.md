---
title: "DECLARE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b0870328-79a6-4627-87c4-3e9256785439
caps.latest.revision: 17
author: BarbKess
---
# DECLARE (SQL Server PDW)
Variables are declared in the body of a batch with the DECLARE statement and are assigned values by using the SET statement or the SELECT statement. After declaration, all variables are initialized as NULL, unless a value is provided as part of the declaration.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DECLARE   
{{ @local_variable [AS] data_type } [ =value [ COLLATE <collation_name> ] ] } [,...n]  
```  
  
## Arguments  
@*local_variable*  
Is the name of a variable. Variable names must begin with an at (@) sign. Local variable names must comply with the rules for identifiers. For more information, see [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
*data_type*  
Is any system-supplied data type. For a list of data types, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md). The **sysname** and **sql_variant** data types can be used for variables but cannot be used in user-defined tables.  
  
=*value*  
Assigns a value to the variable in-line. The value can be a constant or an expression, but it must either match the variable declaration type or be implicitly convertible to that type. For more information, see [Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md).  
  
## General Remarks  
Variables are often used in a batch as counters for WHILE or IF...ELSE blocks.  
  
The scope of a local variable is the batch in which it is declared.  
  
> [!WARNING]  
> Comparing a null valued variable to a literal does not fail as does in SQL Server 2012.  
  
## Limitations and Restrictions  
Variables can be used only in expressions, not in place of object names or keywords.  
  
Using a variable in its own **DECLARE** statement is not supported and may result in inconsistent behavior. For example, the following example declares @p1 and then attempts to use @p1 later in the declaration statement. This fails, returning NULL.  
  
```  
DECLARE @p1 int = 100, @p2 int = (SELECT MAX (@p1) FROM sys.types);   
SELECT MAX (@p1), MAX(@p2) FROM sys.types;  
```  
  
InSQL Server PDW, local variables of sql_variant type always have the PDW appliance-level collation, which is case-insensitive. This overrides an assigned collation when the variables are declared. Both of the following examples return 1. This behavior is different than SQL Server.  
  
```  
--@var1 and @var2 actually have the PDW-wide   
--collation Latin1_General_100_CI_AS_KS_WS  
DECLARE @var1 sql_variant = 'a' COLLATE Latin1_General_CS_AS_KS_WS;  
DECLARE @var2 sql_variant = 'A' COLLATE Latin1_General_CS_AS_KS_WS  
IF @var1 = @var2  
    SELECT 1;  
ELSE  
    SELECT 2  
  
DECLARE @var1 sql_variant = 'a' COLLATE Corsican_100_CI_AI;  
DECLARE @var2 sql_variant = 'A' COLLATE Latin1_General_CS_AS_KS_WS  
IF @var1 = @var2  
    SELECT 1;  
ELSE  
    SELECT 2  
```  
  
## Examples  
  
### A. Using DECLARE  
The following example uses a local variable named `@find` to retrieve contact information for all last names beginning with `Walt`.  
  
```  
USE AdventureWorksPDW2012;  
  
DECLARE @find varchar(30);  
/* Also allowed:   
DECLARE @find varchar(30) = 'Man%';  
*/  
SET @find = 'Walt%';  
  
SELECT LastName, FirstName, Phone  
FROM DimEmployee   
WHERE LastName LIKE @find;  
```  
  
### B. Using DECLARE with two variables  
The following example retrieves uses variables to specify the first and last names of employees in the `DimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
DECLARE @lastName varchar(30), @firstName varchar(30);  
  
SET @lastName = 'Walt%';  
SET @firstName = 'Bryan';  
  
SELECT LastName, FirstName, Phone  
FROM DimEmployee   
WHERE LastName LIKE @lastName AND FirstName LIKE @firstName;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
