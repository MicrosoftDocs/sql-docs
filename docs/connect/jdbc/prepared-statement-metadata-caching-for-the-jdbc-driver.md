---
title: Prepared statement metadata caching
description: Learn how the JDBC Driver for SQL Server caches prepared statements to improve performance by minimizing calls to the database and how you can control its behavior.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Prepared statement metadata caching for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article provides information on the two changes that are implemented to enhance the performance of the driver.

## Batching of unprepare for prepared statements

Since version 6.1.6-preview, an improvement in performance was implemented through minimizing server round trips to SQL Server. Previously, for every prepareStatement query, a call to unprepare was also sent. Now, the driver is batching unprepare queries up to the threshold "ServerPreparedStatementDiscardThreshold", which has a default value of 10.

> [!NOTE]
> Users can change the default value with the following method:
> setServerPreparedStatementDiscardThreshold(int value)

One more change introduced from 6.1.6-preview is that before this version, the driver would always call `sp_prepexec`. Now, for the first execution of a prepared statement, driver calls `sp_executesql` and for the rest it executes `sp_prepexec` and assigns a handle to it. More details can be found [here](https://github.com/Microsoft/mssql-jdbc/wiki/PreparedStatement-metadata-caching).

Starting from the 11.2 release, following the initial `sp_executesql` call, the driver can execute either `sp_prepare` or `sp_prepexec` for additional calls, depending on the value specified in the `prepareMethod` connection string property. For more information, see [Setting the connection properties](setting-the-connection-properties.md).

> [!NOTE]
> Users can change the default behavior to the previous versions of always calling `sp_prepexec` by setting enablePrepareOnFirstPreparedStatementCall to **true** using the following method:
> setEnablePrepareOnFirstPreparedStatementCall(boolean value)

### List of the new APIs introduced with this change, for batching of unprepare for prepared statements

#### SQLServerConnection

|New Method|Description|
|-----------|-----------------|
|int getDiscardedServerPreparedStatementCount()|Returns the number of currently outstanding unprepare actions.|
|void closeUnreferencedPreparedStatementHandles()|Forces the unprepare requests for any outstanding discarded prepared statements to be executed.|
|boolean getEnablePrepareOnFirstPreparedStatementCall()|Returns the behavior for a specific connection instance. If false, the first execution calls `sp_executesql` and doesn't prepare a statement. If a second execution happens, it calls `sp_prepare` or `sp_prepexec` and actually sets up a prepared statement handle. Later executions call `sp_execute`. This behavior relieves the need for `sp_unprepare` on prepared statement close if the statement is only executed once. The default for this option can be changed by calling setDefaultEnablePrepareOnFirstPreparedStatementCall().|
|void setEnablePrepareOnFirstPreparedStatementCall(boolean value)|Specifies the behavior for a specific connection instance. If the value is false, the first execution calls `sp_executesql` and doesn't prepare a statement. If a second execution happens, it calls `sp_prepare` or `sp_prepexec` and actually sets up a prepared statement handle. Later executions call `sp_execute`. This behavior relieves the need for `sp_unprepare` on prepared statement close if the statement is only executed once.|
|int getServerPreparedStatementDiscardThreshold()|Returns the behavior for a specific connection instance. This setting controls how many outstanding discard actions (`sp_unprepare`) there can be per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1, unprepare actions are executed immediately on prepared statement close. If it's set to {@literal >} 1, these calls are batched together to avoid the overhead of calling `sp_unprepare` too often. The default for this option can be changed by calling getDefaultServerPreparedStatementDiscardThreshold().|
|void setServerPreparedStatementDiscardThreshold(int value)|Specifies the behavior for a specific connection instance. This setting controls how many outstanding discard actions (`sp_unprepare`) there can be per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1, unprepare actions are executed immediately on prepared statement close. If it is set to > 1, these calls are batched together to avoid overhead of calling `sp_unprepare` too often.|

#### SQLServerDataSource

|New Method|Description|
|-----------|-----------------|
|void setEnablePrepareOnFirstPreparedStatementCall(boolean enablePrepareOnFirstPreparedStatementCall)|If this configuration is false, the first execution of a prepared statement calls `sp_executesql` and doesn't prepare a statement. If a second execution happens it calls `sp_prepare` or `sp_prepexec` and actually sets up a prepared statement handle. Later executions call `sp_execute`. This behavior relieves the need for `sp_unprepare` on prepared statement close if the statement is only executed once.|
|boolean getEnablePrepareOnFirstPreparedStatementCall()|If this configuration returns false, the first execution of a prepared statement calls `sp_executesql` and doesn't prepare a statement. If a second execution happens, it calls `sp_prepare` or `sp_prepexec` and actually sets up a prepared statement handle. Later executions call `sp_execute`. This behavior relieves the need for `sp_unprepare` on prepared statement close if the statement is only executed once.|
|void setServerPreparedStatementDiscardThreshold(int serverPreparedStatementDiscardThreshold)|This setting controls how many outstanding discard actions (`sp_unprepare`) there can be per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1, unprepare actions are executed immediately on prepared statement close. If it's set to {@literal >} 1, these calls are batched together to avoid the overhead of calling `sp_unprepare` too often|
|int getServerPreparedStatementDiscardThreshold()|This setting controls how many outstanding discard actions (`sp_unprepare`) there can be per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1, unprepare actions are executed immediately on prepared statement close. If it's set to {@literal >} 1, these calls are batched together to avoid the overhead of calling `sp_unprepare` too often.|

## Prepared statement metadata caching

Starting with version 6.3.0-preview, Microsoft JDBC driver for SQL Server supports prepared statement caching. Before v6.3.0-preview, if one executes a query that has been already prepared and stored in the cache, calling the same query again won't result in preparing it. Now, the driver looks up the query in the cache and finds the handle and executes it with `sp_execute`.
Prepared Statement Metadata caching is **disabled** by default. To enable it, you need to call the following method on the connection object:

`setStatementPoolingCacheSize(int value)   //value is the desired cache size (any value bigger than 0)`
`setDisableStatementPooling(boolean value) //false allows the caching to take place`

For example:
`connection.setStatementPoolingCacheSize(10)`
`connection.setDisableStatementPooling(false)`

### List of the new APIs introduced with this change, for prepared statement metadata caching

#### SQLServerConnection

|New Method|Description|
|-----------|-----------------|
|void setDisableStatementPooling(boolean value)|Sets statement pooling to true or false.|
|boolean getDisableStatementPooling()|Returns true if statement pooling is disabled.|
|void setStatementPoolingCacheSize(int value)|Specifies the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementPoolingCacheSize()|Returns the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementHandleCacheEntryCount()|Returns the current number of pooled prepared statement handles.|
|boolean isPreparedStatementCachingEnabled()|Whether statement pooling is enabled or not for this connection.|

#### SQLServerDataSource

|New Method|Description|
|-----------|-----------------|
|void setDisableStatementPooling(boolean disableStatementPooling)|Sets the statement pooling to true or false|
|boolean getDisableStatementPooling()|Returns true if statement pooling is disabled.|
|void setStatementPoolingCacheSize(int statementPoolingCacheSize)|Specifies the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementPoolingCacheSize()|Returns the size of the prepared statement cache for this connection. A value less than 1 means no cache.|

## See also

[Improving performance and reliability with the JDBC driver](improving-performance-and-reliability-with-the-jdbc-driver.md)
