---
title: "setArray Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setArray"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b7fb66d4-6a42-43d0-ba68-8514816917cb
author: MightyPen
ms.author: genemi
manager: craigg
---
# setArray Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter number to the given Array object.  
  
## Syntax  
  
```  
  
public final void setArray(int i,  
                           java.sql.Array x)  
```  
  
#### Parameters  
 *i*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setArray method is specified by the setArray method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
