---
title: "Command Syntax | Microsoft Docs"
description: "Command syntax and Stored Procedures"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, commands"
  - "commands [OLE DB]"
  - "OLE DB Driver for SQL Server, stored procedures"
  - "stored procedures [OLE DB], command syntax"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Command Syntax
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server recognizes command syntax specified by the DBGUID_SQL macro. For the OLE DB Driver for SQL Server, the specifier indicates that an amalgam of ODBC SQL, ISO, and [!INCLUDE[tsql](../../../includes/tsql-md.md)] is valid syntax. For example, the following SQL statement uses an ODBC SQL escape sequence to specify the LCASE string function:  
  
```  
SELECT customerid={fn LCASE(CustomerID)} FROM Customers  
```  
  
 LCASE returns a character string, converting all uppercase characters to their lowercase equivalents. The ISO string function LOWER performs the same operation, so the following SQL statement is a ISO equivalent to the ODBC statement presented above:  
  
```  
SELECT customerid=LOWER(CustomerID) FROM Customers  
```  
  
 The OLE DB Driver for SQL Server processes either form of the statement successfully when specified as text for a command.  
  
## Stored Procedures  
 When executing a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stored procedure using a OLE DB Driver for SQL Server command, use the ODBC CALL escape sequence in the command text. The OLE DB Driver for SQL Server then uses the remote procedure call mechanism of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to optimize command processing. For example, the following ODBC SQL statement is preferred command text over the [!INCLUDE[tsql](../../../includes/tsql-md.md)] form:  
  
-   ODBC SQL  
  
    ```  
    {call SalesByCategory('Produce', '1995')}  
    ```  
  
-   Transact-SQL  
  
    ```  
    EXECUTE SalesByCategory 'Produce', '1995'  
    ```  
  
## See Also  
 [Commands](../../oledb/ole-db-commands/commands.md)  
  
  
