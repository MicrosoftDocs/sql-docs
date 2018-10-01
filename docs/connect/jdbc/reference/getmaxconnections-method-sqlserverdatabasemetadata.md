---
title: "getMaxConnections Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getMaxConnections"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 745410f7-e59b-4423-9728-c903adedc399
author: MightyPen
ms.author: genemi
manager: craigg
---
# getMaxConnections Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of concurrent connections to this database that are possible.  
  
## Syntax  
  
```  
  
public int getMaxConnections()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of concurrent connections allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxConnections method is specified by the getMaxConnections method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
