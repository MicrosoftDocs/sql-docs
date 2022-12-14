---
title: "HOST_NAME (Transact-SQL)"
description: "HOST_NAME (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "09/21/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "HOST_NAME_TSQL"
  - "HOST_NAME"
helpviewer_keywords:
  - "HOST_NAME function"
  - "workstation names [SQL Server]"
dev_langs:
  - "TSQL"
---
# HOST_NAME (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Returns the workstation name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
HOST_NAME ()  
```  

## Return Types
 **nvarchar(128)**  
  
## Remarks  
 When the parameter to a system function is optional, the current database, host computer, server user, or database user is assumed. Built-in functions must always be followed by parentheses.  
  
 System functions can be used in the select list, in the WHERE clause, and anywhere an expression is allowed.  
  
> [!IMPORTANT]  
>  The client application provides the workstation name and can provide inaccurate data. Do not rely upon HOST_NAME as a security feature.  
  
## Examples  
 The following example creates a table that uses `HOST_NAME()` in a `DEFAULT` definition to record the workstation name of computers that insert rows into a table recording orders.  
  
```sql  
CREATE TABLE Orders  
   (OrderID     INT        PRIMARY KEY,  
    CustomerID  NCHAR(5)   REFERENCES Customers(CustomerID),  
    Workstation NCHAR(30)  NOT NULL DEFAULT HOST_NAME(),  
    OrderDate   DATETIME   NOT NULL,  
    ShipDate    DATETIME   NULL,  
    ShipperID   INT        NULL REFERENCES Shippers(ShipperID));  
GO  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  
