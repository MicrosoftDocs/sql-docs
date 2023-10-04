---
title: "getBigDecimal Method (int, int) (SQLServerResultSet)"
description: "getBigDecimal Method (int, int) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getBigDecimal (int, int)"
apitype: "Assembly"
---
# getBigDecimal Method (int, int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object using the given scale.  
  
> [!NOTE]  
>  This method has been deprecated from the JDBC specification. Instead, you should use the [getBigDecimal (int)](../../../connect/jdbc/reference/getbigdecimal-method-int-sqlserverresultset.md) method.  
  
## Syntax  
  
```  
  
public java.math.BigDecimal getBigDecimal(int columnIndex,  
                                          int scale)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *scale*  
  
 An **int** that indicates the number of digits to the right of the decimal point.  
  
## Return Value  
 A BigDecimal object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBigDecimal method is specified by the getBigDecimal method in the java.sql.ResultSet interface.  
  
## See Also  
 [getBigDecimal Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getbigdecimal-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
