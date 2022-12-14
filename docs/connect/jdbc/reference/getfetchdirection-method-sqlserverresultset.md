---
title: "getFetchDirection Method (SQLServerResultSet)"
description: "getFetchDirection Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getFetchDirection"
apitype: "Assembly"
---
# getFetchDirection Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the fetch direction for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public int getFetchDirection()  
```  
  
## Return Value  
 An **int** that indicates the current fetch direction.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFetchDirection method is specified by the getFetchDirection method in the java.sql.ResultSet interface.  
  
 This method returns FETCH_FORWARD for forward-only cursors, the last setting made by a call to the [setFetchDirection](../../../connect/jdbc/reference/setfetchdirection-method-sqlserverresultset.md) method for other cursor types, and will return FETCH_UNKNOWN these cursor types if the setFetchDirection method has never been called.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
