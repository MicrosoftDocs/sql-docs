---
title: Timeout properties
description: Learn about the timeouts used by JDBC, and how to manage them.
author: Jeffery Wasty
ms.author: v-jeffwasty
ms.date: 04/17/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Timeout properties

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

For the initial connection, `loginTimeout` is used:

- `loginTimeout` is the amount of time, in seconds, the driver waits to establish a connection to the server. If this amount is exceeded, an error is returned, and no open connection is formed. A zero value indicates that the timeout is the default system timeout, which is specified as 15 seconds. Any non-zero value is the number of seconds the driver should wait before timing out a failed connection. If you are having trouble establishing a connection with the JDBC driver, a good first step is to increase this timeout to 90, or even 120 seconds. 

Once the connection has been established, `queryTimeout`, `cancelQueryTimeout`, and `lockTimeout` are used during statement executions. `socketTimeout` is used for any driver communication with the server.

- `queryTimeout` is the time, in seconds, the driver will wait, after sending an execute command to the server, to receive a reply from the server with data. If this time is exceeded, the command is cancelled. Exceeding this timeout will not close the connection. The default value is -1, which means infinite timeout.
- `cancelQueryTimeout` is the time, in seconds, the driver will wait for an acknowledgement of the `queryTimeout` cancellation from the server, before forcefully terminating/closing the connection. That is, the driver waits the total amount of `cancelQueryTimeout` plus `queryTimeout` seconds, before dropping the connection and closing the channel. The default value for this property is -1 which is an infinite wait time.
- `lockTimeout` is the amount of time to wait for a lock to be freed, in cases where there is a lock blocking statement execution. This does not result in a closed connection. The default value for this property is -1 and behavior is to wait indefinitely.
- `socketTimeout` applies to socket communications with the server. If at some point, the server stops communication with the driver (i.e. not acknowledging or replying to data), the driver waits for a length equal to the socketTimeout before closing the connection. The default value is 0, which means infinite timeout.

This same information can be found summarized below:

| Property             | Description                                          | Default                | Connection Result  |
|----------------------| ---------------------------------------------------- | ---------------------- | ------------------ |
| `loginTimeout`       | Initializes a new instance of SQLServerDataTable.    | 0 [15 seconds]         | Closed connection  |
| `queryTimeout`       | Retrieves an iterator on the rows of the data table. | -1 [Infinite timeout]  | Open connection    |
| `cancelQueryTimeout` | Adds metadata for the specified column.              | -1 [Infinite timeout]  | Closed connection  |
| `lockTimeout`        | Adds metadata for the specified column.              | -1 [Infinite timeout]  | Open connection    |
| `socketTimeout`      | Adds one row of data to the data table.              | 0 [Infinite timeout]   | Closed connection  |

## See also

[Setting the connection properties](setting-the-connection-properties.md)
