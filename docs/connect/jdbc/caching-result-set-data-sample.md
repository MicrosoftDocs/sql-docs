---
title: "Caching Result Set Data Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 13a95ebb-996c-4713-a1bd-5834fe22a334
author: MightyPen
ms.author: genemi
manager: craigg
---

# Caching Result Set Data Sample

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample application demonstrates how to retrieve a large set of data from a database, and then control the number of rows of data that are cached on the client by using the [setFetchSize](../../connect/jdbc/reference/setfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object.

> [!NOTE]  
> Limiting the number of rows cached on the client is different from limiting the total number of rows that a result set can contain. To control the total number of rows that are contained in a result set, use the [setMaxRows](../../connect/jdbc/reference/setmaxrows-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, which is inherited by both the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) objects.

To set a limit on the number of rows cached on the client, you must first use a server-side cursor when you create one of the Statement objects by specifically stating the cursor type to use when creating the Statement object. For example, the JDBC driver provides the TYPE_SS_SERVER_CURSOR_FORWARD_ONLY cursor type, which is a fast forward-only, read-only server-side cursor for use with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.

> [!NOTE]  
> An alternative to using the SQL Server specific cursor type is to use the selectMethod connection string property, setting its value to "cursor". For more information about the cursor types supported by the JDBC driver, see [Understanding Cursor Types](../../connect/jdbc/understanding-cursor-types.md).

After you have run the query contained in the Statement object and the data is returned to the client as a result set, you can call the setFetchSize method to control how much data is retrieved from the database at one time. For example, if you have a table that contains 100 rows of data, and you set the fetch size to 10, only 10 rows of data will be cached on the client at any point in time. Although this will slow down the speed at which the data is processed, it has the advantage of using less memory on the client, which can be especially useful when you need to process large amounts of data.

The code file for this sample is named CacheResultSet.java, and it can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\resultsets
```

## Requirements

To run this sample application, you must set the classpath to include the mssql-jdbc jar file. You will also need access to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. For more information about how to set the classpath, see [Using the JDBC Driver](../../connect/jdbc/using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] rovides mssql-jdbc class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database. Then it uses an SQL statement with the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object, specifies the server-side cursor type, and then runs the SQL statement and places the data that it returns into a SQLServerResultSet object.

Next, the sample code calls the custom timerTest method, passing as arguments the fetch size to use and the result set. The timerTest method then sets the fetch size of the result set by using the setFetchSize method, sets the start time of the test, and then iterates through the result set with a `While` loop. As soon as the `While` loop is exited, the code sets the stop time of the test, and then displays the result of the test including the fetch size, the number of rows processed, and the time it took to execute the test.

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import com.microsoft.sqlserver.jdbc.SQLServerResultSet;

public class CacheResultSet {

    @SuppressWarnings("serial")
    public static void main(String[] args) {

        // Create a variable for the connection string.
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(connectionUrl);
                Statement stmt = con.createStatement(SQLServerResultSet.TYPE_SS_SERVER_CURSOR_FORWARD_ONLY, SQLServerResultSet.CONCUR_READ_ONLY);) {

            String SQL = "SELECT * FROM Sales.SalesOrderDetail;";

            for (int n : new ArrayList<Integer>() {
                {
                    add(1);
                    add(10);
                    add(100);
                    add(1000);
                    add(0);
                }
            }) {
                // Perform a fetch for every nth row in the result set.
                try (ResultSet rs = stmt.executeQuery(SQL)) {
                    timerTest(n, rs);
                }
            }
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static void timerTest(int fetchSize,
            ResultSet rs) throws SQLException {

        // Declare the variables for tracking the row count and elapsed time.
        int rowCount = 0;
        long startTime = 0;
        long stopTime = 0;
        long runTime = 0;

        // Set the fetch size then iterate through the result set to
        // cache the data locally.
        rs.setFetchSize(fetchSize);
        startTime = System.currentTimeMillis();
        while (rs.next()) {
            rowCount++;
        }
        stopTime = System.currentTimeMillis();
        runTime = stopTime - startTime;

        // Display the results of the timer test.
        System.out.println("FETCH SIZE: " + rs.getFetchSize());
        System.out.println("ROWS PROCESSED: " + rowCount);
        System.out.println("TIME TO EXECUTE: " + runTime);
        System.out.println();
    }
}

```

## See Also

[Working with Result Sets](../../connect/jdbc/working-with-result-sets.md)
