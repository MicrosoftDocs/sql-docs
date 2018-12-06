---
title: "wasNull Method (SQLServerCallableStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.wasNull"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 1a27b2fe-ae12-46a9-9bca-2c5ca66b9eb3
author: MightyPen
ms.author: genemi
manager: craigg
---
# wasNull Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the last OUT parameter read had the value of SQL NULL.  
  
## Syntax  
  
```  
  
public boolean wasNull()  
```  
  
## Return Value  
 **true** if the last parameter read was null. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This wasNull method is specified by the wasNull method in the java.sql.CallableStatement interface.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
