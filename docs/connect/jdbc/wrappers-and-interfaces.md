---
title: Wrappers and interfaces
description: Learn how to create proxy interfaces and wrappers that let you access extensions to the JDBC API.
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Wrappers and interfaces

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports interfaces that allow you create a proxy of a class, and wrappers that let you access extensions to the JDBC API that are specific to the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] through a proxy interface.

## Wrappers

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the java.sql.Wrapper interface. This interface provides a mechanism to access extensions to the JDBC API that are specific to the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] through a proxy interface.

The java.sql.Wrapper interface defines two methods: **isWrapperFor** and **unwrap**. The **isWrapperFor** method checks whether the specified input object implements this interface. The **unwrap** method returns an object that implements this interface to allow access to the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] specific methods.

**isWrapperFor** and **unwrap** methods are exposed as follows:

- [isWrapperFor Method &#40;SQLServerCallableStatement&#41;](reference/iswrapperfor-method-sqlservercallablestatement.md)
- [unwrap Method &#40;SQLServerCallableStatement&#41;](reference/unwrap-method-sqlservercallablestatement.md)
- [isWrapperFor Method &#40;SQLServerConnectionPoolDataSource&#41;](reference/iswrapperfor-method-sqlserverconnectionpooldatasource.md)
- [unwrap Method &#40;SQLServerConnectionPoolDataSource&#41;](reference/unwrap-method-sqlserverconnectionpooldatasource.md)
- [isWrapperFor Method &#40;SQLServerDataSource&#41;](reference/iswrapperfor-method-sqlserverdatasource.md)
- [unwrap Method &#40;SQLServerDataSource&#41;](reference/unwrap-method-sqlserverdatasource.md)
- [isWrapperFor Method &#40;SQLServerPreparedStatement&#41;](reference/iswrapperfor-method-sqlserverpreparedstatement.md)
- [unwrap Method &#40;SQLServerPreparedStatement&#41;](reference/unwrap-method-sqlserverpreparedstatement.md)
- [isWrapperFor Method &#40;SQLServerStatement&#41;](reference/iswrapperfor-method-sqlserverstatement.md)
- [unwrap Method &#40;SQLServerStatement&#41;](reference/unwrap-method-sqlserverstatement.md)
- [isWrapperFor Method &#40;SQLServerXADataSource&#41;](reference/iswrapperfor-method-sqlserverxadatasource.md)
- [unwrap Method &#40;SQLServerXADataSource&#41;](reference/unwrap-method-sqlserverxadatasource.md)

## Interfaces

Beginning in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] JDBC Driver 3.0, interfaces are available for an application server to access a driver-specific method from the associated class. The application server can wrap the class by creating a proxy, exposing the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]-specific functionality from an interface. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports interfaces that have the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] specific methods and constants so an application server can create a proxy of the class.

The interfaces derive from standard Java interfaces so you can use the same object once it is unwrapped to access driver-specific functionality or generic [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] functionality.

The following interfaces are added:

- [ISQLServerCallableStatement](reference/isqlservercallablestatement-interface.md)
- [ISQLServerConnection](reference/isqlserverconnection-interface.md)
- [ISQLServerDataSource](reference/isqlserverdatasource-interface.md)
- [ISQLServerPreparedStatement](reference/isqlserverpreparedstatement-interface.md)
- [ISQLServerResultSet](reference/isqlserverresultset-interface.md)
- [ISQLServerStatement](reference/isqlserverstatement-interface.md)

## Example

### Description

This sample demonstrates how to access to a [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)]-specific function from a DataSource object. This DataSource class may have been wrapped by an application server. To access the JDBC driver-specific function or constant, you can unwrap the datasource to an ISQLServerDataSource interface and use the functions declared in this interface.

### Code

```java
import javax.sql.*;
import java.sql.*;
import com.microsoft.sqlserver.jdbc.*;

public class UnWrapTest {
   public static void main(String[] args) {
      // This is a test.  This DataSource object could be something from an appserver
      // which has wrapped the real SQLServerDataSource with its own wrapper
      SQLServerDataSource ds = new SQLServerDataSource();
      checkSendStringParametersAsUnicode(ds);
   }

   // Unwrap to the ISQLServerDataSource interface to access the getSendStringParametersAsUnicode function
   static void checkSendStringParametersAsUnicode(DataSource ds) {
      try {
         final ISQLServerDataSource sqlServerDataSource = ds.unwrap(ISQLServerDataSource.class);
         boolean sendStringParametersAsUnicode = sqlServerDataSource.getSendStringParametersAsUnicode();

         System.out.println("Send string as parameter value is:-" + sendStringParametersAsUnicode);

      } catch (SQLException sqlE) {
         System.out.println("Exception:-" + sqlE);
      }
   }
}
```

## See also

[Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)
