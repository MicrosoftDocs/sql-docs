---
title: "supportsMultipleOpenResults Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsMultipleOpenResults"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 9480d280-5e3d-46ae-80e6-1bba3ac5a641
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsMultipleOpenResults Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether it is possible to have multiple [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects returned from a [SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md) object simultaneously.  
  
## Syntax  
  
```  
  
public boolean supportsMultipleOpenResults()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsMultipleOpenResults method is specified by the supportsMultipleOpenResults method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
