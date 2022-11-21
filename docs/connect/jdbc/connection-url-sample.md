---
title: Connection URL sample
description: This Microsoft JDBC Driver for SQL Server sample application demonstrates how to connect to a SQL Server database by using a connection URL.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Connection URL sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a connection URL. It also demonstrates how to retrieve data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using an SQL statement.

The code file for this sample is named ConnectURL.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\connections
```

## Requirements

To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You'll also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](using-the-jdbc-driver.md).

> [!NOTE]
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code sets various connection properties in the connection URL, and then calls the getConnection method of the DriverManager class to return a [SQLServerConnection](reference/sqlserverconnection-class.md) object.

Next, the sample code uses the [createStatement](reference/createstatement-method-sqlserverconnection.md) method of the SQLServerConnection object to create a [SQLServerStatement](reference/sqlserverstatement-class.md) object, and then the [executeQuery](reference/executequery-method-sqlserverstatement.md) method is called to execute the SQL statement.

Finally, the sample uses the [SQLServerResultSet](reference/sqlserverresultset-class.md) object returned from the executeQuery method to iterate through the results returned by the SQL statement.

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class ConnectURL {
    public static void main(String[] args) {

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;encrypt=true;databaseName=AdventureWorks;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(connectionUrl); Statement stmt = con.createStatement();) {
            String SQL = "SELECT TOP 10 * FROM Person.Contact";
            ResultSet rs = stmt.executeQuery(SQL);

            // Iterate through the data in the result set and display it.
            while (rs.next()) {
                System.out.println(rs.getString("FirstName") + " " + rs.getString("LastName"));
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
