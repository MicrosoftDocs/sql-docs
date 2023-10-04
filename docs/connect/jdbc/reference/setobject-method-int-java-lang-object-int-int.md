---
title: "setObject Method (int, java.lang.Object, int, int)"
description: "setObject Method (int, java.lang.Object, int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.setObject (int, java.lang.Object, int, int)"
apitype: "Assembly"
---
# setObject Method (int, java.lang.Object, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the value of the designated parameter by using the given object, target type, and scale.  
  
## Syntax  
  
```  
  
public final void setObject(int n,  
                            java.lang.Object obj,  
                            int targetSqlType,  
                            int scale)  
```  
  
#### Parameters  
 *n*  
  
 An **int** that indicates the parameter number.  
  
 *obj*  
  
 An object.  
  
 *targetSqlType*  
  
 An **int** that indicates the target type as defined in java.sql.Types.  
  
 *scale*  
  
 An **int** that indicates the number of digits to the right of the decimal point. This parameter is ignored for all types other than NUMERIC and DECIMAL.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setObject method is specified by the setObject method in the java.sql.PreparedStatement interface.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## See Also  
 [setObject Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/setobject-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
