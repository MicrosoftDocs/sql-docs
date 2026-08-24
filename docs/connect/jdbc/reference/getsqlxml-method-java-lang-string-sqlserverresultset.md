---
title: "getSQLXML Method (java.lang.String) (SQLServerResultSet)"
description: "getSQLXML Method (java.lang.String) (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getSQLXML Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a SQLXML object.  
  
## Syntax  
  
```  
  
public final java.sql.SQLXML getSQLXML(java.lang.String columnLabel)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that indicates the column label.  
  
## Return Value  
 ASQLXMLobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSQLXML method is specified by the getSQLXML method in the java.sql.ResultSet interface.  
  
## Related content

- [getSQLXML Method (SQLServerResultSet)](getsqlxml-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
