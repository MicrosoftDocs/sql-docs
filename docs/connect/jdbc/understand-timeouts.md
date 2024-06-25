---
title: Understanding timeouts
description: Learn about the timeouts used by JDBC. LoginTimeout, queryTimeout, cancelQueryTimeout, and socketTimeout can be used to ensure application responsiveness.
author: David-Engel
ms.author: davidengel
ms.date: 06/05/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Understanding timeout properties in the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Timeout settings in the JDBC driver can be used to prioritize application responsiveness. By default, most of the driver's timeouts prioritize waiting for a result to ensure data consistency. Ensure you choose timeouts that are appropriate for your application's needs.

For the initial connection, `loginTimeout` is used:

- `loginTimeout` is the amount of time, in seconds, the driver waits to establish a connection to the server. If this amount is exceeded, an error is returned, and no open connection is established. A zero value indicates that the timeout is the default system timeout, which is 30 seconds in versions 11.2 and above. For versions 10.2 and below, the default timeout is 15 seconds. Any nonzero value is the number of seconds the driver should wait before timing out a failed connection. If you're consistently having trouble establishing a connection with the JDBC driver, you may need to increase this timeout to 90, or even 120, seconds. 

Once the connection has been established, `queryTimeout`, `cancelQueryTimeout`, and `lockTimeout` are used during statement executions. `socketTimeout` is used for any driver communication with the server.

- `queryTimeout` is the time, in seconds, the driver will wait, after sending an execute command to the server, to receive a reply from the server with data. If this time is exceeded, the command is canceled. Exceeding this timeout doesn't close the connection. The default value is -1, which means infinite timeout.
- `cancelQueryTimeout` is the time, in seconds, the driver waits for an acknowledgment of the `queryTimeout` cancellation from the server, before forcefully terminating/closing the connection. That is, the driver waits the total amount of `cancelQueryTimeout` plus `queryTimeout` seconds, before the connection is closed. Setting this timeout to a nonzero value ensures applications can remain responsive if there's network or communication failure with the server, when a query has timed out. The default value for this property is -1, which is an infinite wait time.
- `lockTimeout` is the amount of time to wait for a lock to be freed, in cases where there's a lock blocking statement execution. Exceeding this timeout doesn't result in a closed connection. The default value for this property is -1, which is an infinite wait time.
- `socketTimeout` applies to all socket communications with the server. If the server stops communication with the driver, either by not acknowledging or replying to data, the driver waits for the value of `socketTimeout` before it closes the connection. Setting this timeout to a nonzero value ensures applications can remain responsive if there's network or communication failure with the server. The default value is 0, which means an infinite timeout. Ensure `socketTimeout` is greater than `queryTimeout` to avoid socket timeout exceptions during the `queryTimeout` window. Similarly, ensure `socketTimeout` is greater than `cancelQueryTimeout` to avoid socket timeout exceptions during the `cancelQueryTimeout` window.

Reasonable timeout values for your application depend on the application's priorities. Setting lower values for timeouts prioritizes application responsiveness over data consistency. When timeouts are reached, applications need to decide the best course of action. That decision is based on the database action being performed. For example, for a `SELECT` statement, the decision might be to report an error to the user, or it may be to reconnect and retry. For `INSERT` or `UPDATE` statements, that decision may be different.

For a responsive application, `loginTimeout` and `queryTimeout` should be set to relatively low values. Similarly, `cancelQueryTimeout` should also be set to a low value to ensure the driver doesn't wait too long for the server to acknowledge the query cancellation, when a `queryTimeout` is exceeded. Finally, `socketTimeout` should be set to guard against the driver waiting too long during any scenario where connectivity to the server is broken (network interruption, server crash, etc.).

## Property summary

| Property             | Description                                                                           | Default                                       | Connection result  |
|----------------------|---------------------------------------------------------------------------------------|-----------------------------------------------| ------------------ |
| `loginTimeout`       | The number of seconds the driver should wait before timing out a failed connection.   | 30 seconds [11.2+], <br/>otherwise 15 seconds | Closed connection  |
| `queryTimeout`       | The number of seconds to wait before canceling a query.                               | -1 [Infinite timeout]                         | Open connection    |
| `cancelQueryTimeout` | The number of seconds to wait for an acknowledgment of the QueryTimeout cancellation. | -1 [Infinite timeout]                         | Closed connection  |
| `lockTimeout`        | The number of milliseconds to wait before the database returns a lock time-out error. | -1 [Infinite timeout]                         | Open connection    |
| `socketTimeout`      | The number of milliseconds to wait on a socket read or write.                         | Zero [Infinite timeout]                       | Closed connection  |

## See also

[Setting the connection properties](setting-the-connection-properties.md)
