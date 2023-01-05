---
title: Managing transaction size
description: Learn how to manage transaction size to ensure you don't introduce locks in your application that would block other users.
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Managing transaction size

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you work with transactions, it's important to keep your transactions as brief as possible. The default mode of autocommit, which you can enable or disable by using the [setAutoCommit](reference/setautocommit-method-sqlserverconnection.md) method, will commit every action for you. This mode is the easiest mode to work with for most developers.

When you use manual transactions, make sure that your code commits the transaction as quickly as possible. Holding open a transaction blocks other users from accessing the data. For example, a good programming practice might be to put a rollback call in your catch block and a commit call in a `finally` block. However, this practice depends on the design of your application.

Keep the size of your transactions small to improve concurrency. For example, if you start a manual transaction and modify 10,000 rows in a 20,000-row table, you will have half the table blocked from all other users, even if they are only reading the data. Reducing your modifications to 2,000 rows leaves 90 percent of the table available.

Additionally, be sure to use the lock time-out setting if your application expects some blocking issues. You can set the time-out by using the [setLockTimeout](reference/setlocktimeout-method-sqlserverdatasource.md) method. The default for the lock time-out is -1, which means that it will block indefinitely while waiting for the lock. You can set the lock time-out to 30 seconds, which will cause the blocked connection to time out in 30 seconds if blocked by another connection.

## See also

[Improving performance and reliability with the JDBC driver](improving-performance-and-reliability-with-the-jdbc-driver.md)
