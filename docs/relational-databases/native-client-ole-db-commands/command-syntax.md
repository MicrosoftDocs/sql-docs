---
title: "Command Syntax | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, commands"
  - "commands [OLE DB]"
  - "SQL Server Native Client OLE DB provider, stored procedures"
  - "stored procedures [OLE DB], command syntax"
ms.assetid: d463d3d7-e5cb-426d-8e92-aa29980356b6
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Command Syntax
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider recognizes command syntax specified by the DBGUID_SQL macro. For the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the specifier indicates that an amalgam of ODBC SQL, ISO, and [!INCLUDE[tsql](../../includes/tsql-md.md)] is valid syntax. For example, the following SQL statement uses an ODBC SQL escape sequence to specify the LCASE string function:  
  
```  
SELECT customerid={fn LCASE(CustomerID)} FROM Customers  
```  
  
 LCASE returns a character string, converting all uppercase characters to their lowercase equivalents. The ISO string function LOWER performs the same operation, so the following SQL statement is a ISO equivalent to the ODBC statement presented above:  
  
```  
SELECT customerid=LOWER(CustomerID) FROM Customers  
```  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider processes either form of the statement successfully when specified as text for a command.  
  
## Stored Procedures  
 When executing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider command, use the ODBC CALL escape sequence in the command text. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider then uses the remote procedure call mechanism of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to optimize command processing. For example, the following ODBC SQL statement is preferred command text over the [!INCLUDE[tsql](../../includes/tsql-md.md)] form:  
  
-   ODBC SQL  
  
    ```  
    {call SalesByCategory('Produce', '1995')}  
    ```  
  
-   Transact-SQL  
  
    ```  
    EXECUTE SalesByCategory 'Produce', '1995'  
    ```  
  
## See Also  
 [Commands](../../relational-databases/native-client-ole-db-commands/commands.md)  
  
  
