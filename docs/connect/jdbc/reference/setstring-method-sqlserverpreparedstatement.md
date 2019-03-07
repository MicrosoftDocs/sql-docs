---
title: "setString Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setString"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 25dabdc9-c60f-485a-87eb-306067964765
author: MightyPen
ms.author: genemi
manager: craigg
---
# setString Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given **String** value.  
  
## Syntax  
  
```  
  
public final void setString(int index,  
                            java.lang.String str)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter number.  
  
 *str*  
  
 A **String** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setString method is specified by the setString method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
