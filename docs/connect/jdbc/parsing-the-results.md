---
title: Parsing the results
description: Learn about fully processing results, including multiple result sets, from a query execution in the JDBC driver.
author: DavidEngel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Parsing the results

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article describes how SQL Server expects users to fully process results returned from any query.

## Update counts and result sets

This section will talk about the two most common results returned from SQL Server: Update Count and ResultSet. In general, any query a user executes will cause one or more of these results to be returned. Users are expected to handle both when processing results.

The following code is an example of how a user could iterate through all results from the server:

```java
try (Connection connection = DriverManager.getConnection(URL); Statement statement = connection.createStatement()) {
    boolean resultsAvailable = statement.execute(USER_SQL);
    int updateCount = -2;
    while (true) {
        updateCount = statement.getUpdateCount();
        if (!resultsAvailable && updateCount == -1)
            break;
        if (resultsAvailable) {
            // handle ResultSet
        } else {
            // handle Update Count
        }
        resultsAvailable = statement.getMoreResults();
    }
}
```

## Exceptions

When you execute a statement that results in an error or an informational message, SQL Server can respond differently. For example, if it can't generate an execution plan, the error message will be thrown immediately on `execute()`. If an error is related to statement processing, the application needs to parse the result set to see the exception.

When SQL Server is unable to generate an execution plan, the exception is thrown immediately.

```java
String SQL = "SELECT * FROM nonexistentTable;";
try (Statement statement = connection.createStatement();) {
    statement.execute(SQL);
} catch (SQLException e) {
    e.printStackTrace();
}
```

When SQL Server returns an error message in a result set, the result set needs to be processed to retrieve the exception.

```java
String SQL = "SELECT 1/0;";
try (Statement statement = connection.createStatement();) {
    boolean hasResult = statement.execute(SQL);
    if (hasResult) {
        try (ResultSet rs = statement.getResultSet()) {
            // Exception is thrown on next().
            while (rs.next()) {}
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
```

If statement execution generates multiple result sets, each result set needs to be processed until the one with the exception is reached.

```java
String SQL = "SELECT 1; SELECT * FROM nonexistentTable;";
try (Statement statement = connection.createStatement();) {
    // Does not throw an exception on execute().
    boolean hasResult = statement.execute(SQL);
    while (hasResult) {
        try (ResultSet rs = statement.getResultSet()) {
            while (rs.next()) {
                System.out.println(rs.getString(1));
            }
        }
        // Moves the next result set that generates the exception.
        hasResult = statement.getMoreResults();
    }
} catch (SQLException e) {
    e.printStackTrace();
}
```

For example, `String SQL = "SELECT * FROM nonexistentTable; SELECT 1;";`, throws an exception immediately on `execute()` and `SELECT 1` isn't executed at all.

If the error from SQL Server has severity of `0` to `9`, it's considered as an informational message and returned as `SQLWarning`.

```java
String SQL = "RAISERROR ('WarningLevel5', 5, 2);";
try (Statement statement = connection.createStatement();) {
    boolean hasResult = statement.execute(SQL);
    SQLWarning warning = statement.getWarnings();
    System.out.println(warning);
}
```

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
