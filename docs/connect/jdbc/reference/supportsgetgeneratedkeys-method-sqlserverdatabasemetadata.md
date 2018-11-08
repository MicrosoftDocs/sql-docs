---
title: "supportsGetGeneratedKeys Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsGetGeneratedKeys"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4f0e4659-14e7-4743-aed8-1768ee2b29dd
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsGetGeneratedKeys Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether auto-generated keys can be retrieved after a statement has been executed.  
  
## Syntax  
  
```  
  
public boolean supportsGetGeneratedKeys()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsGetGeneratedKeys method is specified by the supportsGetGeneratedKeys method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
