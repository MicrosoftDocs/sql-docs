---
title: Configuring how java.sql.Time values are sent
description: Learn how to configure how java.sql.Time values are sent to the server using the sendTimeAsDatetime connection option.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Configuring how java.sql.Time values are sent

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

If you use a java.sql.Time object or the java.sql.Types.TIME JDBC type to set a parameter, you can configure how the java.sql.Time value is sent to the server; either as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **time** type or as a **datetime** type.

This scenario applies when using one of the following methods:

- [SQLServerCallableStatement.registerOutParameter(int, int)](reference/registeroutparameter-method-int-int.md)
- [SQLServerCallableStatement.registerOutParameter(int, int, int)](reference/registeroutparameter-method-int-int-int.md)
- [SQLServerCallableStatement.setTime](reference/settime-method-sqlservercallablestatement.md)
- [SQLServerPreparedStatement.setTime](reference/settime-method-sqlserverpreparedstatement.md)
- [SQLServerCallableStatement.setObject](reference/setobject-method-sqlservercallablestatement.md)
- [SQLServerPreparedStatement.setObject](reference/setobject-method-sqlserverpreparedstatement.md)

## SendTimeAsDatetime

You can configure how the java.sql.Time value is sent by using the **sendTimeAsDatetime** connection property. For more information, see [Setting the Connection Properties](setting-the-connection-properties.md).

You can programmatically modify the value of the **sendTimeAsDatetime** connection property with [SQLServerDataSource.setSendTimeAsDatetime](reference/setsendtimeasdatetime-method-sqlserverdatasource.md).

Versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] don't support the **time** data type, so applications using java.sql.Time typically store java.sql.Time values either as **datetime** or **smalldatetime** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.

If you want to use the **datetime** and **smalldatetime**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types when working with java.sql.Time values, you should set the **sendTimeAsDatetime** connection property to **true**. If you want to use the **time** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type when working with java.sql.Time values, you should set the **sendTimeAsDatetime** connection property to **false**.

Sending java.sql.Time values into a parameter whose data type can also store the date, that default dates are different depending on whether the java.sql.Time value is sent as a **datetime** (1/1/1970) or **time** (1/1/1900) value. For more information about data conversions when sending data to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Using Date and Time Data](/previous-versions/sql/sql-server-2008-r2/ms180878(v=sql.105)).

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] JDBC Driver 3.0, **sendTimeAsDatetime** is true by default. In a future release, the **sendTimeAsDatetime** connection property may be set to false by default.

To ensure that your application continues to work as expected regardless of the default value of the **sendTimeAsDatetime** connection property, you can:

- Use java.sql.Time when working with the **time**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.
- Use java.sql.Timestamp when working with the **datetime**, **smalldatetime**, and **datetime2**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types.

SendTimeAsDatetime must be false for encrypted columns as encrypted columns don't support the conversion from time to datetime. Beginning with Microsoft JDBC Driver 6.0 for SQL Server, the SQLServerConnection class has the following two methods to set/get the value of the sendTimeAsDatetime property.

```java
  public boolean getSendTimeAsDatetime()
  public void setSendTimeAsDatetime(boolean sendTimeAsDateTimeValue)
```

## See also

[Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)
