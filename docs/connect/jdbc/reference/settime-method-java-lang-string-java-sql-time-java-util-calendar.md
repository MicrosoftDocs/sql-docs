---
title: "setTime Method to time and calendar values"
description: "setTime Method (java.lang.String, java.sql.Time, java.util.Calendar)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setTime (java.lang.String, java.lang.Time, java.lang.Calendar))"
apitype: "Assembly"
---
# setTime Method (java.lang.String, java.sql.Time, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given time and calendar values.  
  
## Syntax  
  
```  
  
public void setTime(java.lang.String sCol,  
                    java.sql.Time x,  
                    java.util.Calendar c)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
 *x*  
  
 A Time object.  
  
 *c*  
  
 A Calendar object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setTime method is specified by the setTime method in the java.sql.CallableStatement interface.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## See Also  
 [setTime Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/settime-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
