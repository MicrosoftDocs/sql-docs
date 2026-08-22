---
title: "absolute Method (SQLServerResultSet)"
description: "absolute Method (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.absolute"
apitype: "Assembly"
---
# absolute Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the given row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public boolean absolute(int row)  
```  
  
#### Parameters  
 *row*  
  
 An **int** that indicates the row number to move to. Can be positive, negative, or 0.  
  
## Return Value  
 **true** if the cursor is moved to the given position. **false** if it is before the first row or after the last row.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This absolute method is specified by the absolute method in the java.sql.ResultSet interface.  
  
## Related content

- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
