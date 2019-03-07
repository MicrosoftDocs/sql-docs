---
title: "getTimeDateFunctions Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getTimeDateFunctions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: a56e08ae-6f4e-4dc6-b175-ff457d0d7a81
author: MightyPen
ms.author: genemi
manager: craigg
---
# getTimeDateFunctions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a comma-separated list of the time and date functions that are available with this database.  
  
## Syntax  
  
```  
  
public java.lang.String getTimeDateFunctions()  
```  
  
## Return Value  
 A **String** that contains a list of the time and date functions.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTimeDateFunctions method is specified by the getTimeDateFunctions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
