---
title: "Configuring How java.sql.Time Values are Sent to the Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 07eb00dd-621a-46f9-a5a5-8cab4d6058b5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Configuring How java.sql.Time Values are Sent to the Server
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  If you use a java.sql.Time object or the java.sql.Types.TIME JDBC type to set a parameter, you can configure how the java.sql.Time value is sent to the server; either as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **time** type or as a **datetime** type.  
  
 This scenario applies when using one of the following methods:  
  
-   [SQLServerCallableStatement.registerOutParameter(int, int)](../../connect/jdbc/reference/registeroutparameter-method-int-int.md)  
  
-   [SQLServerCallableStatement.registerOutParameter(int, int, int)](../../connect/jdbc/reference/registeroutparameter-method-int-int-int.md)  
  
-   [SQLServerCallableStatement.setTime](../../connect/jdbc/reference/settime-method-sqlservercallablestatement.md)  
  
-   [SQLServerPreparedStatement.setTime](../../connect/jdbc/reference/settime-method-sqlserverpreparedstatement.md)  
  
-   [SQLServerCallableStatement.setObject](../../connect/jdbc/reference/setobject-method-sqlservercallablestatement.md)  
  
-   [SQLServerPreparedStatement.setObject](../../connect/jdbc/reference/setobject-method-sqlserverpreparedstatement.md)  
  
 You can configure how the java.sql.Time value is sent by using the **sendTimeAsDatetime** connection property. For more information, see [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md).  
  
 You can programmatically modify the value of the **sendTimeAsDatetime** connection property with [SQLServerDataSource.setSendTimeAsDatetime](../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 Versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)] don't support the **time** data type, so applications using java.sql.Time typically store java.sql.Time values either as **datetime** or **smalldatetime** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
 If you want to use the **datetime** and **smalldatetime**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types when working with java.sql.Time values, you should set the **sendTimeAsDatetime** connection property to **true**. If you want to use the **time** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type when working with java.sql.Time values, you should set the **sendTimeAsDatetime** connection property to **false**.  
  
 Be aware that sending java.sql.Time values into a parameter whose data type can also store the date, that default dates are different depending on whether the java.sql.Time value is sent as a **datetime** (1/1/1970) or **time** (1/1/1900) value. For more information about data conversions when sending data to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Using Date and Time Data](https://go.microsoft.com/fwlink/?LinkID=145211).  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] JDBC Driver 3.0, **sendTimeAsDatetime** is true by default. In a future release, the **sendTimeAsDatetime** connection property may be set to false by default.  
  
 To ensure that your application continues to work as expected regardless of the default value of the **sendTimeAsDatetime** connection property, you can:  
  
-   Use java.sql.Time when working with the **time**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
-   Use java.sql.Timestamp when working with the **datetime**, **smalldatetime**, and **datetime2**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.  
  
SendTimeAsDatetime must be false for encrypted columns as encrypted columns don't support the conversion from time to datetime. Beginning with Microsoft JDBC Driver 6.0 for SQL Server, the SQLServerConnection class has the following two methods to set/get the value of the sendTimeAsDatetime property.

```java
  public boolean getSendTimeAsDatetime()
  public void setSendTimeAsDatetime(boolean sendTimeAsDateTimeValue)
```
  
## See Also  
 [Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
  
  
