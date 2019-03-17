---
title: "getSystemFunctions Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getSystemFunctions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6d390ec5-9ee2-49c4-b661-a93b55691dd1
author: MightyPen
ms.author: genemi
manager: craigg
---
# getSystemFunctions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a comma-separated list of system functions that are available with this database.  
  
## Syntax  
  
```  
  
public java.lang.String getSystemFunctions()  
```  
  
## Return Value  
 A **String** that contains the list of system functions.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSystemFunctions method is specified by the getSystemFunctions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
