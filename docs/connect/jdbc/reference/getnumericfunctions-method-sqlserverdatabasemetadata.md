---
title: "getNumericFunctions Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getNumericFunctions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 8d1c3848-bdb7-452a-862f-6421e1a7ce8b
author: MightyPen
ms.author: genemi
manager: craigg
---
# getNumericFunctions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a comma-separated list of math functions that are available with this database.  
  
## Syntax  
  
```  
  
public java.lang.String getNumericFunctions()  
```  
  
## Return Value  
 A **String** that contains the available math functions.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNumericFunctions method is specified by the getNumericFunctions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
