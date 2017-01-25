---
title: "storesUpperCaseQuotedIdentifiers Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerDatabaseMetaData.storesUpperCaseQuotedIdentifiers"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 936ec140-2597-44e6-82d3-3994a676ee35
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# storesUpperCaseQuotedIdentifiers Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database treats mixed-case SQL identifiers that are enclosed in quotation marks as case-insensitive and stores them in uppercase.  
  
## Syntax  
  
```  
  
public boolean storesUpperCaseQuotedIdentifiers()  
```  
  
## Return Value  
 **true** if the identifiers are stored in uppercase. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This storesUpperCaseQuotedIdentifiers method is specified by the storesUpperCaseQuotedIdentifiers method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  