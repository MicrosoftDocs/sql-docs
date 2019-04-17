---
title: "supportsResultSetHoldability Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsResultSetHoldability"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ab575792-fd11-4ff3-8847-1368e7a322c5
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsResultSetHoldability Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the given result set holdability.  
  
## Syntax  
  
```  
  
public boolean supportsResultSetHoldability(int holdability)  
```  
  
#### Parameters  
 *holdability*  
  
 An **int** that indicates the result set holdability, which can be one of the following values:  
  
 ResultSet.HOLD_CURSORS_OVER_COMMIT  
  
 ResultSet.CLOSE_CURSORS_AT_COMMIT  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsResultSetHoldability method is specified by the supportsResultSetHoldability method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
