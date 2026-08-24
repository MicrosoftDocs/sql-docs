---
title: "getDate Method (java.util.Calendar) column"
description: "getDate Method (java.lang.String, java.util.Calendar) (SQLServerResultSet)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getDate (java.lang.String, java.util.Calendar)"
apitype: "Assembly"
---
# getDate Method (java.lang.String, java.util.Calendar) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Date object in the Java programming language, using the given Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Date getDate(java.lang.String colName,  
                             java.util.Calendar cal)  
```  
  
#### Parameters  
 *colName*  
  
 A **String** that contains the column name.  
  
 *cal*  
  
 A Calendar object.  
  
## Return Value  
 A Date object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDate method is specified by the getDate method in the java.sql.ResultSet interface.  
  
 This method returns a valid date part of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] datetime or smalldatetime data type, with the time part set to the Java time baseline of 00:00 (midnight) in the supplied Calendar's timezone.  
  
## Related content

- [getDate Method (SQLServerResultSet)](getdate-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
