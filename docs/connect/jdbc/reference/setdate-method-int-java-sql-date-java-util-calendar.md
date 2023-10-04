---
title: "setDate Method to date and calendar - int"
description: "setDate Method (int, java.sql.Date, java.util.Calendar)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setDate (int, java.sql.Date, java.util.Calendar)"
apitype: "Assembly"
---
# setDate Method (int, java.sql.Date, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given date and calendar values.  
  
## Syntax  
  
```  
  
public final void setDate(int n,  
                          java.sql.Date x,  
                          java.util.Calendar cal)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A Date object.  
  
 *cal*  
  
 A Calendar object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setDate method is specified by the setDate method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [setDate Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/setdate-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
