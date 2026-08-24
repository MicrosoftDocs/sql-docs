---
title: "updateRow Method (SQLServerResultSet)"
description: "updateRow Method (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "MSQLServerResultSet.updateRow"
apitype: "Assembly"
---
# updateRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the underlying database with the new contents of the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void updateRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateRow method is specified by the updateRow method in the java.sql.ResultSet interface.  
  
 This method cannot be called when the cursor is on the insert row.  
  
 If this method is called when no column values have changed, an exception will be thrown.  
  
## Related content

- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
