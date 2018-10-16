---
title: "supportsExpressionsInOrderBy Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsExpressionsInOrderBy"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 858f3c02-4531-4775-97e9-a03b316bdaba
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsExpressionsInOrderBy Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports expressions in ORDER BY lists.  
  
## Syntax  
  
```  
  
public boolean supportsExpressionsInOrderBy()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsExpressionsInOrderBy method is specified by the supportsExpressionsInOrderBy method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
