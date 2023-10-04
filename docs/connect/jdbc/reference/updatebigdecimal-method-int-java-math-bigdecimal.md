---
title: "updateBigDecimal Method (int, java.math.BigDecimal)"
description: "updateBigDecimal Method (int, java.math.BigDecimal)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateBigDecimal (int, java.math.BigDecimal)"
apitype: "Assembly"
---
# updateBigDecimal Method (int, java.math.BigDecimal)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a BigDecimal object given the column index.  
  
## Syntax  
  
```  
  
public void updateBigDecimal(int index,  
                             java.math.BigDecimal x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A BigDecimal object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBigDecimal method is specified by the updateBigDecimal method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateBigDecimal Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatebigdecimal-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
