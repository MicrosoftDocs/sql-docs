---
title: "previous Method (SQLServerResultSet)"
description: "previous Method (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.previous"
apitype: "Assembly"
---
# previous Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the previous row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public boolean previous()  
```  
  
## Return Value  
 **true** if the new current row is valid. **false** if there are no more rows to process.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This previous method is specified by the previous method in the java.sql.ResultSet interface.  
  
## Related content

- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
