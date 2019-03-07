---
title: "setTime Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerPreparedStatement.setTime"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b3a83ea3-6636-4096-842b-71b37340fa07
author: MightyPen
ms.author: genemi
manager: craigg
---
# setTime Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given time value.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## Overload List  
  
|Name|Description|  
|----------|-----------------|  
|[setTime (int, java.sql.Time)](../../../connect/jdbc/reference/settime-method-int-java-sql-time.md)|Sets the designated parameter to the given time value.|  
|[setTime (int, java.sql.Time, java.util.Calendar)](../../../connect/jdbc/reference/settime-method-int-java-sql-time-java-util-calendar.md)|Sets the designated parameter to the given time and calendar values.|  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
