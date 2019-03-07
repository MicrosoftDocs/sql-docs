---
title: "getMaxStatements Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getMaxStatements"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 71d58431-b671-49c5-939a-f581d1fef7cd
author: MightyPen
ms.author: genemi
manager: craigg
---
# getMaxStatements Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of active statements to this database that can be open at the same time.  
  
## Syntax  
  
```  
  
public int getMaxStatements()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of active statements allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxStatements method is specified by the getMaxStatements method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
