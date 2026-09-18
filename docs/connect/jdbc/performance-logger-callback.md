---
title: Performance logger and callback
description: Learn how to use the performance logging framework and callback infrastructure in the Microsoft JDBC Driver for SQL Server to track execution timing for driver operations.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 09/10/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---

# Performance logger and callback

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Starting with version 13.4, the Microsoft JDBC Driver for SQL Server provides a performance metrics framework for tracking the timing of critical driver operations. You can use this framework to observe and analyze connection and statement execution behavior, helping identify latency bottlenecks in your application's interactions with SQL Server.

Metrics are available through two mechanisms that can be used independently or together:

- **Programmatic callback** - Register a `PerformanceLogCallback` to receive metrics in your application code.
- **Java logging** - Subscribe to dedicated `java.util.logging` loggers to capture metrics in log output.

## Tracked activities

The driver tracks activities at two levels: connection and statement.

### Connection-level activities

| Activity | Description |
| --- | --- |
| `CONNECTION` | Total time for establishing a connection, including all subactivities. |
| `PRELOGIN` | Time for TDS prelogin negotiation with the server. |
| `LOGIN` | Time for the TDS login and authentication handshake. |
| `TOKEN_ACQUISITION` | Time to acquire federated authentication tokens when using Microsoft Entra authentication. |

### Statement-level activities

| Activity | Description |
| --- | --- |
| `STATEMENT_REQUEST_BUILD` | Client-side time to build the TDS request (parameter binding, SQL processing, packet construction). Timing-only; doesn't track exceptions. |
| `STATEMENT_FIRST_SERVER_RESPONSE` | Time from sending the request to receiving the first server response. Timing-only; doesn't track exceptions. |
| `STATEMENT_PREPARE` | Time for `sp_prepare` when `prepareMethod=prepare`. |
| `STATEMENT_PREPEXEC` | Time for combined prepare and execute via `sp_prepexec`. |
| `STATEMENT_EXECUTE` | Time for statement execution (`sp_executesql`, `sp_execute`, direct SQL, or batch). |

## Enable performance metrics

### Option 1: Register a callback

Register a `PerformanceLogCallback` to receive performance data programmatically:

```java
SQLServerDriver.registerPerformanceLogCallback(new PerformanceLogCallback() {
    @Override
    public void publish(PerformanceActivity activity, int connectionId,
            long durationMs, Exception exception) {
        // Connection-level metrics
        System.out.printf("Activity: %s, Connection: %d, Duration: %d ms%n",
                activity, connectionId, durationMs);
    }

    @Override
    public void publish(PerformanceActivity activity, int connectionId,
            int statementId, long durationMs, Exception exception) {
        // Statement-level metrics
        System.out.printf("Activity: %s, Connection: %d, Statement: %d, Duration: %d ms%n",
                activity, connectionId, statementId, durationMs);
    }
});
```

### Use nanosecond timing and statement context

Starting with version 13.6, a callback can opt in to nanosecond timing by overriding `useNanoseconds()`. The callback can also call `getCurrentUserSql()` and `getCurrentStatementType()` to correlate a statement-level event with the SQL text submitted by the application and the type of statement that executed. `getCurrentStatementType()` returns `STATEMENT`, `PREPARED_STATEMENT`, or `CALLABLE_STATEMENT`. Use `getCurrentApplicationName()` to identify the connection or pool that produced an event from its `applicationName` connection property. If `applicationName` isn't set, the method returns the driver's default application name.

```java
SQLServerDriver.registerPerformanceLogCallback(new PerformanceLogCallback() {
    @Override
    public boolean useNanoseconds() {
        return true;
    }

    @Override
    public void publish(PerformanceActivity activity, int connectionId,
            long durationNs, Exception exception) {
        // Connection-level metrics don't have SQL statement context.
        System.out.printf("Application: %s, Activity: %s, Connection: %d, Duration: %d ns%n",
            getCurrentApplicationName(), activity, connectionId, durationNs);
    }

    @Override
    public void publish(PerformanceActivity activity, int connectionId,
            int statementId, long durationNs, Exception exception) {
        String applicationName = getCurrentApplicationName();
        String userSql = getCurrentUserSql();
        StatementType statementType = getCurrentStatementType();

        System.out.printf("Application: %s, Type: %s, Activity: %s, SQL: %s, Duration: %d ns%n",
            applicationName, statementType, activity, userSql, durationNs);
    }
});
```

Nanosecond timing is opt-in. Callbacks that don't override `useNanoseconds()` continue to receive durations in milliseconds. When a registered callback opts in, the dedicated performance logger output also uses nanoseconds and labels durations with `ns`; otherwise, it uses milliseconds and `ms`. Calling `System.nanoTime()` can have more overhead than `System.currentTimeMillis()` on some platforms.

Callback context is available only during the corresponding `publish()` invocation. `getCurrentApplicationName()` is available for connection-level and statement-level events and returns `null` outside `publish()`. It can also return `null` when connection properties haven't been parsed, such as when a connection fails before the application name is resolved. The SQL context methods return `null` for connection-level events, statement subactivity metrics that don't have SQL context, and calls made outside `publish()`.

> [!CAUTION]
> SQL text can contain sensitive data in literals or comments. Sanitize it and apply your organization's data-handling requirements before writing it to logs or telemetry.

### Option 2: Configure Java logging

Configure `java.util.logging` for the performance metrics loggers at `FINE` level.

In a `logging.properties` file:

```properties
com.microsoft.sqlserver.jdbc.PerformanceMetrics.Connection.level = FINE
com.microsoft.sqlserver.jdbc.PerformanceMetrics.Statement.level = FINE
handlers = java.util.logging.ConsoleHandler
java.util.logging.ConsoleHandler.level = FINE
```

Or programmatically:

```java
Logger.getLogger("com.microsoft.sqlserver.jdbc.PerformanceMetrics.Connection")
      .setLevel(Level.FINE);
Logger.getLogger("com.microsoft.sqlserver.jdbc.PerformanceMetrics.Statement")
      .setLevel(Level.FINE);
```

## Prepare method effect on statement activities

The activities tracked for `PreparedStatement` depend on the `prepareMethod` connection property. For more information about `prepareMethod`, see [Setting the connection properties](setting-the-connection-properties.md).

| `prepareMethod` setting | First execution | Second execution | Third+ execution |
| --- | --- | --- | --- |
| `prepexec` (default) | `STATEMENT_EXECUTE` (`sp_executesql`) | `STATEMENT_PREPEXEC` (`sp_prepexec`) | `STATEMENT_EXECUTE` (`sp_execute`) |
| `prepare` | `STATEMENT_PREPARE` + `STATEMENT_EXECUTE` | `STATEMENT_EXECUTE` | `STATEMENT_EXECUTE` |
| `none` | `STATEMENT_EXECUTE` (direct SQL) | `STATEMENT_EXECUTE` (direct SQL) | `STATEMENT_EXECUTE` (direct SQL) |

> [!NOTE]
> With the default `prepexec` setting, the driver defers preparation assuming single use. The second execution uses `sp_prepexec` (combined prepare and execute). From the third execution onward, the cached handle is reused via `sp_execute`. To force `sp_prepexec` on the first call, set the connection property `enablePrepareOnFirstPreparedStatementCall` to `true`.

## Sample log output

The following output shows the activities tracked across three consecutive executions of a `PreparedStatement` with the default `prepexec` setting:

```output
ConnectionID:1, StatementID:1 Request build time, duration: 8ms
ConnectionID:1, StatementID:1 First server response, duration: 17ms
ConnectionID:1, StatementID:1 Statement execute, duration: 75ms        ← 1st call: sp_executesql
ConnectionID:1, StatementID:1 Request build time, duration: 9ms
ConnectionID:1, StatementID:1 First server response, duration: 0ms
ConnectionID:1, StatementID:1 Statement prepexec, duration: 0ms        ← 2nd call: sp_prepexec
ConnectionID:1, StatementID:1 Request build time, duration: 0ms
ConnectionID:1, StatementID:1 First server response, duration: 0ms
ConnectionID:1, StatementID:1 Statement execute, duration: 0ms         ← 3rd call: sp_execute
```

## Related content

- [Improving performance and reliability (JDBC)](improving-performance-and-reliability-with-the-jdbc-driver.md)
- [Set the connection properties](setting-the-connection-properties.md)
