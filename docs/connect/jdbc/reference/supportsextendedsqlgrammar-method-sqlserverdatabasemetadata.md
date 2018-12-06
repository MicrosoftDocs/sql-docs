---
title: "supportsExtendedSQLGrammar Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsExtendedSQLGrammar"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 8deb1987-c4e3-4599-8e37-0a04ec20b480
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsExtendedSQLGrammar Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the ODBC Extended SQL grammar.  
  
## Syntax  
  
```  
  
public boolean supportsExtendedSQLGrammar()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsExtendedSQLGrammer method is specified by the supportsExtendedSQLGrammer method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
