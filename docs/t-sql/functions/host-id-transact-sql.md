---
title: "HOST_ID (Transact-SQL)"
description: "HOST_ID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "HOST_ID"
  - "HOST_ID_TSQL"
helpviewer_keywords:
  - "IDs [SQL Server], workstations"
  - "HOST_ID function"
  - "workstation IDs [SQL Server]"
  - "identification numbers [SQL Server], workstations"
dev_langs:
  - "TSQL"
---
# HOST_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the workstation identification number. The workstation identification number is the process ID (PID) of the application on the client computer that is connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
HOST_ID ()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **char(10)**  
  
## Remarks  
 When the parameter to a system function is optional, the current database, host computer, server user, or database user is assumed. Built-in functions must always be followed by parentheses.  
  
 System functions can be used in the select list, in the WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
 The following example creates a table that uses `HOST_ID()` in a `DEFAULT` definition to record the terminal ID of computers that insert rows into a table recording orders.  
  
```sql  
CREATE TABLE Orders  
   (OrderID     INT       PRIMARY KEY,  
    CustomerID  NCHAR(5)  REFERENCES Customers(CustomerID),  
    TerminalID  CHAR(8)   NOT NULL DEFAULT HOST_ID(),  
    OrderDate   DATETIME  NOT NULL,  
    ShipDate    DATETIME  NULL,  
    ShipperID   INT       NULL REFERENCES Shippers(ShipperID));  
GO  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  
