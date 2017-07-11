---
title: "supportsCoreSQLGrammar Method (SQLServerDatabaseMetaData) | Microsoft Docs"
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
  - "SQLServerDatabaseMetaData.supportsCoreSQLGrammar"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6b82f300-f906-4d11-b810-525bda4a88ee
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# supportsCoreSQLGrammar Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the ODBC Core SQL grammar.  
  
## Syntax  
  
```  
  
public boolean supportsCoreSQLGrammar()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsCoreSQLGrammer method is specified by the supportsCoreSQLGrammer method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  