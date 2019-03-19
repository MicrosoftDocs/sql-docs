---
title: "ToString (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ToString"
  - "ToString_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ToString [Database Engine]"
ms.assetid: 5fc11ca5-c26d-4518-9512-67aa0270f110
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# ToString (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a string with the logical representation of *this*. ToString is called implicitly when a conversion from **hierarchyid** to a string type occurs. Acts as the opposite of [Parse &#40;Database Engine&#41;](../../t-sql/data-types/parse-database-engine.md).
  
## Syntax  
  
```sql
-- Transact-SQL syntax  
node.ToString  ( )   
-- This is functionally equivalent to the following syntax  
-- which implicitly calls ToString():  
CAST(node AS nvarchar(4000))  
```  
  
```sql
-- CLR syntax  
string ToString  ( )   
```  
  
## Return types
**SQL Server return type:nvarchar(4000)**
  
**CLR return type:String**
  
## Remarks  
Returns the logical location in the hierarchy. For example, `/2/1/` represents the fourth row ( [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) in the following hierarchical structure of a file system:
  
```sql
/        C:\  
/1/      C:\Database Files  
/2/      C:\Program Files  
/2/1/    C:\Program Files\Microsoft SQL Server  
/2/2/    C:\Program Files\Microsoft Visual Studio  
/3/      C:\Windows  
```  
  
## Examples  
  
### A. Transact-SQL example in a table  
The following example returns both the `OrgNode` column as both the **hierarchyid** data type and in the more readable string format:
  
```sql
SELECT OrgNode,  
OrgNode.ToString() AS Node  
FROM HumanResources.EmployeeDemo  
ORDER BY OrgNode ;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
OrgNode   Node  
0x        /  
0x58      /1/  
0x5AC0    /1/1/  
0x5B40    /1/2/  
0x5BC0    /1/3/  
0x5C20    /1/4/  
...  
```  
  
### B. Converting Transact-SQL values without a table  
The following code example uses `ToString` to convert a **hierarchyid** value to a string, and `Parse` to convert a string value to a **hierarchyid**.
  
```sql
DECLARE @StringValue AS nvarchar(4000), @hierarchyidValue AS hierarchyid  
SET @StringValue = '/1/1/3/'  
SET @hierarchyidValue = 0x5ADE  
  
SELECT hierarchyid::Parse(@StringValue) AS hierarchyidRepresentation,  
@hierarchyidValue.ToString() AS StringRepresentation ;
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
hierarchyidRepresentation    StringRepresentation
-------------------------    -----------------------
0x5ADE                       /1/1/3/
```
  
### C. CLR example  
The following code snippet calls the ToString() method:
  
```sql
this.ToString()  
```  
  
## See also
[hierarchyid Data Type Method Reference](https://msdn.microsoft.com/library/01a050f5-7580-4d5f-807c-7f11423cbb06)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
  
