---
title: "Using Statement Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, parameters"
  - "parameters [SQL Server Native Client], ODBC"
  - "ODBC, parameters"
  - "statements [ODBC], parameters"
  - "parameter markers [ODBC]"
  - "SQL Server Native Client ODBC driver, statements"
  - "ODBC applications, statements"
ms.assetid: 2427d886-ec6c-49d7-b0b6-0d998b64cdb9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Statement Parameters
  A parameter is a variable in an SQL statement that can enable an ODBC application to:  
  
-   Efficiently provide values for columns in a table.  
  
-   Enhance user interaction in constructing query criteria.  
  
-   Manage **text**, **ntext**, and **image** data and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific C data types.  
  
 For example, a **Parts** table has columns named **PartID**, **Description**, and **Price**. To add a part without parameters requires constructing an SQL statement such as:  
  
```  
INSERT INTO Parts (PartID, Description, Price) VALUES (2100, 'Drive shaft', 50.00)  
```  
  
 Although this statement is acceptable for inserting one row with a known set of values, it is awkward when an application is required to insert several rows. ODBC addresses this by letting an application to replace any data value in an SQL statement by a parameter maker. This is denoted by a question mark (?). In the following example, three data values are replaced with parameter markers:  
  
```  
INSERT INTO Parts (PartID, Description, Price) VALUES (?, ?, ?)  
```  
  
 The parameter markers are then bound to application variables. To insert a new row, the application has only to set the values of the variables and execute the statement. The driver then retrieves the current values of the variables and sends them to the data source. If the statement is executed multiple times, the application can make the process even more efficient by preparing the statement.  
  
 Each parameter marker is referenced by its ordinal number assigned to the parameters from left to right. The leftmost parameter marker in an SQL statement has an ordinal value of 1; the next one is ordinal 2, and so on.  
  
## In This Section  
  
-   [Binding Parameters](using-statement-parameters-binding-parameters.md)  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](executing-queries-odbc.md)  
  
  
