---
title: Data source sample
description: This sample demonstrates how to connect to SQL Server and retrieve data by using a stored procedure.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Data source sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a data source object. It also demonstrates how to retrieve data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a stored procedure.

The code file for this sample is named ConnectDataSource.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\connections
```

## Requirements

To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You'll also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code sets various connection properties by using setter methods of the [SQLServerDataSource](reference/sqlserverdatasource-class.md) object, and then calls the [getConnection](reference/getconnection-method-sqlserverdatasource.md) method of the SQLServerDataSource object to return a [SQLServerConnection](reference/sqlserverconnection-class.md) object.

Next, the sample code uses the [prepareCall](reference/preparecall-method-sqlserverconnection.md) method of the SQLServerConnection object to create a [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) object, and then the [executeQuery](reference/executequery-method-sqlserverpreparedstatement.md) method is called to execute the stored procedure.

Finally, the sample uses the [SQLServerResultSet](reference/sqlserverresultset-class.md) object returned from the executeQuery method to iterate through the results returned by the stored procedure.

```java
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class ConnectDataSource {

    public static void main(String[] args) {

        // Create datasource.
        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setUser("<user>");
        ds.setPassword("<password>");
        ds.setServerName("<server>");
        ds.setPortNumber(<port>);
        ds.setDatabaseName("AdventureWorks");

        try (Connection con = ds.getConnection();
                CallableStatement cstmt = con.prepareCall("{call dbo.uspGetEmployeeManagers(?)}");) {
            // Execute a stored procedure that returns some data.
            cstmt.setInt(1, 50);
            ResultSet rs = cstmt.executeQuery();

            // Iterate through the data in the result set and display it.
            while (rs.next()) {
                System.out.println("EMPLOYEE: " + rs.getString("LastName") + ", " + rs.getString("FirstName"));
                System.out.println("MANAGER: " + rs.getString("ManagerLastName") + ", " + rs.getString("ManagerFirstName"));
                System.out.println();
            }
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
```

## See also

[Connecting and retrieving data](connecting-and-retrieving-data.md)
