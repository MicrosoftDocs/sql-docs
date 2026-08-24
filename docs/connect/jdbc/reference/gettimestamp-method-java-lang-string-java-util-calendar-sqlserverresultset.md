---
title: "getTimestamp Method (java.lang.String, java.util.Calendar) (SQLServerResultSet)"
description: "getTimestamp Method (java.lang.String, java.util.Calendar) (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getTimestamp (java.lang.String, java.util.Calendar)"
apitype: "Assembly"
---
# getTimestamp Method (java.lang.String, java.util.Calendar) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Timestamp object in the Java programming language, using a Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Timestamp getTimestamp(java.lang.String colName,  
                                       java.util.Calendar cal)  
```  
  
#### Parameters  
 *colName*  
  
 A **String** that contains the column name.  
  
 *cal*  
  
 A Calendar object.  
  
## Return Value  
 A Timestamp object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTimestamp method is specified by the getTimestamp method in the java.sql.ResultSet interface.  
  
 This method returns values only from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] datetime and smalldatetime columns.  
  
## Related content

- [getTimestamp Method (SQLServerResultSet)](gettimestamp-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
