---
title: "Parse (Database Engine)"
description: "Parse (Database Engine)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "Parse"
  - "Parse_TSQL"
helpviewer_keywords:
  - "Parse [Database Engine]"
dev_langs:
  - "TSQL"
---
# Parse (Database Engine)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Parse converts the canonical string representation of a **hierarchyid** to a **hierarchyid** value. Parse is called implicitly when a conversion from a string type to **hierarchyid** occurs. Acts as the opposite of [ToString](../../t-sql/data-types/tostring-database-engine.md). Parse() is a static method.
  
## Syntax  
  
```syntaxsql
-- Transact-SQL syntax  
hierarchyid::Parse ( input )  
-- This is functionally equivalent to the following syntax   
-- which implicitly calls Parse():  
CAST ( input AS hierarchyid )  
```  
  
```csharp
-- CLR syntax  
static SqlHierarchyId Parse ( SqlString input )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*input*  
[!INCLUDE[tsql](../../includes/tsql-md.md)]: The character data type value that is being converted.
  
CLR: The String value that is being evaluated.
  
## Return Types  
**SQL Server return type:hierarchyid**
  
**CLR return type:SqlHierarchyId**
  
## Remarks  
If Parse receives a value that is not a valid string representation of a **hierarchyid**, an exception is raised. For example, if **char** data types contain trailing spaces, an exception is raised.
  
## Examples  
  
### A. Converting Transact-SQL values without a table  
The following code example uses `ToString` to convert a **hierarchyid** value to a string, and `Parse` to convert a string value to a **hierarchyid**.
  
```sql
DECLARE @StringValue AS NVARCHAR(4000), @hierarchyidValue AS hierarchyid  
SET @StringValue = '/1/1/3/'  
SET @hierarchyidValue = 0x5ADE  
  
SELECT hierarchyid::Parse(@StringValue) AS hierarchyidRepresentation,  
@hierarchyidValue.ToString() AS StringRepresentation ;
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```txt
hierarchyidRepresentation    StringRepresentation
-------------------------    -----------------------
0x5ADE                       /1/1/3/
```
  
### B. CLR example  
The following code snippet calls the Parse() method:
  
```csharp
string input = "/1/2/";  
SqlHierarchyId.Parse(input);  
```  
  
## See also
[hierarchyid Data Type Method Reference](./hierarchyid-data-type-method-reference.md)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
