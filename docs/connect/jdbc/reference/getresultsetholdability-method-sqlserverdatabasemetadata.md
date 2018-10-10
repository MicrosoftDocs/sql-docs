---
title: "getResultSetHoldability Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData,getResultSetHoldability"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f0bd6283-83ab-4a0a-b825-ec4cdccf03e1
author: MightyPen
ms.author: genemi
manager: craigg
---
# getResultSetHoldability Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the default holdability of result sets for this database.  
  
## Syntax  
  
```  
  
public int getResultSetHoldability()  
```  
  
## Return Value  
 An **int** that indicates the default holdability.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getResultSetHoldability method is specified by the getResultSetHoldability method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this methods returns 1, which is equivalent to the ResultSet.HOLD_CURSORS_OVER_COMMIT constant.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
