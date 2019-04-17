---
title: "setNull Method (int, int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setNull (int, int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7e7f08e9-278a-495a-8ce3-ca173d055021
author: MightyPen
ms.author: genemi
manager: craigg
---
# setNull Method (int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to a null value, given the type of parameter to set.  
  
## Syntax  
  
```  
  
public final void setNull(int index,  
                          int jdbcType)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter number.  
  
 *jdbcType*  
  
 A JDBC type code that is defined by java.sql.Types.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setNull method is specified by the setNull method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [setNull Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/setnull-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
