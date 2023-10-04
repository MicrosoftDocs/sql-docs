---
title: "getDate Method (int, java.util.Calendar) (SQLServerResultSet)"
description: "getDate Method (int, java.util.Calendar) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getDate (int, java.util.Calendar)"
apitype: "Assembly"
---
# getDate Method (int, java.util.Calendar) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Date object in the Java programming language, using the given Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Date getDate(int columnIndex,  
                             java.util.Calendar cal)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *cal*  
  
 A Calendar object.  
  
## Return Value  
 A Date object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDate method is specified by the getDate method in the java.sql.ResultSet interface.  
  
 This method returns a valid date part of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] datetime or smalldatetime data type, with the time part set to the Java time baseline of 00:00 (midnight) in the supplied Calendar's timezone.  
  
## See Also  
 [getDate Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getdate-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
