---
title: "getURL Method (java.lang.String) (SQLServerResultSet)"
description: "getURL Method (java.lang.String) (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getURL (java.lang.String)"
apitype: "Assembly"
---
# getURL Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a URL object.  
  
## Syntax  
  
```  
  
public java.net.URL getURL(java.lang.String sColumn)  
```  
  
#### Parameters  
 *sColumn*  
  
 A **String** that contains the column name.  
  
## Return Value  
 A URL object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getURL method is specified by the getURL method in the java.sql.ResultSet interface.  
  
## Related content

- [getURL Method (SQLServerResultSet)](geturl-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
