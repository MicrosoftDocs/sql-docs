---
title: "setBigDecimal Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setBigDecimal"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 860f86db-d840-401a-a5c2-cd22e8cc1e4e
author: MightyPen
ms.author: genemi
manager: craigg
---
# setBigDecimal Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter number to the given BigDecimal object.  
  
## Syntax  
  
```  
  
public final void setBigDecimal(int n,  
                                java.math.BigDecimal x)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A BigDecimal object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setBigDecimal method is specified by the setBigDecimal method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
