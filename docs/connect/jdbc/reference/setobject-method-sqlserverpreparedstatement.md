---
title: "setObject Method (SQLServerPreparedStatement)"
description: "setObject Method (SQLServerPreparedStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setObject"
apitype: "Assembly"
---
# setObject Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the value of the designated parameter by using the given object.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## Overload List  
  
|Name|Description|  
|----------|-----------------|  
|[setObject (int, java.lang.Object)](../../../connect/jdbc/reference/setobject-method-int-java-lang-object.md)|Sets the value of the designated parameter by using the given object.|  
|[setObject (int, java.lang.Object, int)](../../../connect/jdbc/reference/setobject-method-int-java-lang-object-int.md)|Sets the value of the designated parameter by using the given object and target type.|  
|[setObject (int, java.lang.Object, int, int)](../../../connect/jdbc/reference/setobject-method-int-java-lang-object-int-int.md)|Sets the value of the designated parameter by using the given object, target type, and scale.|  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
