---
title: "SET PATH Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SET PATH command [ODBC]"
ms.assetid: db488d1e-0963-4f45-8c76-a23b9bde9e9d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SET PATH Command
Specifies a path for file searches. For driver-specific information, see the Remarks.  
  
## Syntax  
  
```  
  
SET PATH TO [Path]  
```  
  
## Arguments  
 TO [ *Path*]  
 Specifies the directories you want Visual FoxPro to search. Use commas or semicolons to separate the directories.  
  
## Remarks  
 SET PATH allows you to specify search paths for other Visual FoxPro programs that can be called within stored procedures. SET PATH will not change the path of the data source that you've specified for the connection.  
  
 Issue SET PATH TO without *Path* to restore the path to the default directory or folder.  
  
## Driver Remarks  
 If you issue SET PATH in a stored procedure, it will be ignored by the following functions and commands:  
  
-   Catalog functions such as [SQLTables](../../odbc/microsoft/sqltables-visual-foxpro-odbc-driver.md) and [SQLColumns](../../odbc/microsoft/sqlcolumns-visual-foxpro-odbc-driver.md) will ignore the new path and continue to reference the path specified by the data source in [SQLPrepare](../../odbc/microsoft/sqlprepare-visual-foxpro-odbc-driver.md) or [SQLExecDirect](../../odbc/microsoft/sqlexecdirect-visual-foxpro-odbc-driver.md).  
  
-   Commands such as SELECT, INSERT, UPDATE, DELETE, and CREATE TABLE will ignore the new path and continue to reference the path specified by the data source in **SQLPrepare** or **SQLExecDirect**.  
  
 If you issue SET PATH in a stored procedure and don't subsequently set the path back to its original state, other connections to the database will use the new path (because SET PATH is not scoped to data sessions).  
  
 If you want to create, select, or update tables in a directory other than that specified by the data source, specify the full path of the file with your command.  
  
## See Also  
 [ODBC Visual FoxPro Setup Dialog Box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md)   
 [SQLColumns (Visual FoxPro ODBC Driver)](../../odbc/microsoft/sqlcolumns-visual-foxpro-odbc-driver.md)   
 [SQLDriverConnect (Visual FoxPro ODBC Driver)](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md)   
 [SQLTables (Visual FoxPro ODBC Driver)](../../odbc/microsoft/sqltables-visual-foxpro-odbc-driver.md)
