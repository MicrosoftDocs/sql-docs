---
title: "prepareCall Method (java.lang.String, int, int, int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection.prepareCall (java.lang.String, int, int, int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 81104fd5-75b0-4540-9f48-c3dbf59a8564
author: MightyPen
ms.author: genemi
manager: craigg
---
# prepareCall Method (java.lang.String, int, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md) object that generates [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects with the given type, concurrency, and holdability.  
  
## Syntax  
  
```  
  
public java.sql.CallableStatement prepareCall(java.lang.String sql,  
                                              int nType,  
                                              int nConcur,  
                                              int nHold)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** containing an SQL statement.  
  
 *nType*  
  
 An **int** that indicates the result set type.  
  
 *nConcur*  
  
 An **int** that indicates the result set concurrency type.  
  
 *nHold*  
  
 An **int** that indicates the result set holdability.  
  
## Return Value  
 A CallableStatement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This prepareCall method is specified by the prepareCall method in the java.sql.Connection interface.  
  
## See Also  
 [prepareCall Method &#40;SQLServerConnection&#41;](../../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  
