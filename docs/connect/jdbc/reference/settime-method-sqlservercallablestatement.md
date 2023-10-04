---
title: "setTime Method (SQLServerCallableStatement)"
description: "setTime Method (SQLServerCallableStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setTime"
apitype: "Assembly"
---
# setTime Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given time value.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## Overload List  
  
|Name|Description|  
|----------|-----------------|  
|[setTime (java.lang.String, java.sql.Time)](../../../connect/jdbc/reference/settime-method-java-lang-string-java-sql-time.md)|Sets the designated parameter to the given time value.|  
|[setTime (java.lang.String, java.sql.Time, java.util.Calendar)](../../../connect/jdbc/reference/settime-method-java-lang-string-java-sql-time-java-util-calendar.md)|Sets the designated parameter to the given time and calendar values.|  
  
## See Also  
 [SQLServerCallableStatement Methods](../../../connect/jdbc/reference/sqlservercallablestatement-methods.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
