---
title: "updateBigDecimal Method (int, java.math.BigDecimal)"
description: "updateBigDecimal Method (int, java.math.BigDecimal)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
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
  
## Related content

- [updateBigDecimal Method (SQLServerResultSet)](updatebigdecimal-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
