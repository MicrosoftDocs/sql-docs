---
title: "setMaxFieldSize Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.setMaxFieldSize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 38f7fc1d-acad-4d10-9fc8-3c0669d93b07
author: MightyPen
ms.author: genemi
manager: craigg
---
# setMaxFieldSize Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the limit for the maximum number of bytes in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) column storing character or binary values to the given number of bytes.  
  
## Syntax  
  
```  
  
public final void setMaxFieldSize(int max)  
```  
  
#### Parameters  
 *max*  
  
 An **int** that indicates the maximum number of bytes.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setMaxFieldSize method is specified by the setMaxFieldSize method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
