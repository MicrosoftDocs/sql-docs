---
title: "setRef Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setRef"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 1a09bbf9-6f8f-4a21-85d2-2182111b5ce7
author: MightyPen
ms.author: genemi
manager: craigg
---
# setRef Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given Ref object.  
  
## Syntax  
  
```  
  
public final void setRef(int i,  
                         java.sql.Ref x)  
```  
  
#### Parameters  
 *i*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A Ref object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setRef method is specified by the setRef method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
