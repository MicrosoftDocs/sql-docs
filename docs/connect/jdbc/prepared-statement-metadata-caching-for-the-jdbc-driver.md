---
title: "Prepared Statement Metadata Caching for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 
author: MightyPen
ms.author: genemi
manager: craigg
---
# Prepared Statement Metadata Caching for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article provides information on the two changes that are implemented to enhance the performance of the driver.

## Batching of Unprepare for Prepared Statements
Since version 6.1.6-preview, an improvement in performance was implemented through minimizing server round trips to SQL Server. Previously, for every prepareStatement query, a call to unprepare was also sent. Now, the driver is batching unprepare queries up to the threshold "ServerPreparedStatementDiscardThreshold", which has a default value of 10.

> [!NOTE]  
>  Users can change the default value with the following method:
>  setServerPreparedStatementDiscardThreshold(int value)

One more change introduced from 6.1.6-preview is that prior to this, driver would always call sp_prepexec. Now, for the first execution of a prepared statement, driver calls sp_executesql and for the rest it executes sp_prepexec and assigns a handle to it. More details can be found [here](https://github.com/Microsoft/mssql-jdbc/wiki/PreparedStatement-metadata-caching).

> [!NOTE]  
>  Users can change the default behavior to the previous versions of always calling sp_prepexec by setting enablePrepareOnFirstPreparedStatementCall to **true** using the following method:
>  setEnablePrepareOnFirstPreparedStatementCall(boolean value)

### List of the New APIs Introduced With This Change, for Batching of Unprepare for Prepared Statements

 **SQLServerConnection**
 
|New Method|Description|  
|-----------|-----------------|  
|int getDiscardedServerPreparedStatementCount()|Returns the number of currently outstanding prepared statement unprepare actions.|
|void closeUnreferencedPreparedStatementHandles()|Forces the unprepare requests for any outstanding discarded prepared statements to be executed.|
|boolean getEnablePrepareOnFirstPreparedStatementCall()|Returns the behavior for a specific connection instance. If false the first execution calls sp_executesql and not prepare a statement, once the second execution happens it calls sp_prepexec and actually setup a prepared statement handle. Following executions calls sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once. The default for this option can be changed by calling setDefaultEnablePrepareOnFirstPreparedStatementCall().|
|void setEnablePrepareOnFirstPreparedStatementCall(boolean value)|Specifies the behavior for a specific connection instance. If value is false the first execution calls sp_executesql and not prepare a statement, once the second execution happens it calls sp_prepexec and actually setup a prepared statement handle. Following executions calls sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once.|
|int getServerPreparedStatementDiscardThreshold()|Returns the behavior for a specific connection instance. This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1, unprepare actions are executed immediately on prepared statement close. If it is set to {@literal >} 1, these calls are batched together to avoid overhead of calling sp_unprepare too often. The default for this option can be changed by calling getDefaultServerPreparedStatementDiscardThreshold().|
|void setServerPreparedStatementDiscardThreshold(int value)|Specifies the behavior for a specific connection instance. This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1 unprepare actions are executed immediately on prepared statement close. If it is set to > 1 these calls are batched together to avoid overhead of calling sp_unprepare too often.|

 **SQLServerDataSource**
 
|New Method|Description|  
|-----------|-----------------|  
|void setEnablePrepareOnFirstPreparedStatementCall(boolean enablePrepareOnFirstPreparedStatementCall)|If this configuration is false the first execution of a prepared statement calls sp_executesql and not prepare a statement, once the second execution happens it calls sp_prepexec and actually setup a prepared statement handle. Following executions calls sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once.|
|boolean getEnablePrepareOnFirstPreparedStatementCall()|If this configuration returns false the first execution of a prepared statement calls sp_executesql and not prepare a statement, once the second execution happens, it calls sp_prepexec and actually setup a prepared statement handle. Following executions calls sp_execute. This relieves the need for sp_unprepare on prepared statement close if the statement is only executed once.|
|void setServerPreparedStatementDiscardThreshold(int serverPreparedStatementDiscardThreshold)|This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1 unprepare actions are executed immediately on prepared statement close. If it is set to {@literal >} 1 these calls are batched together to avoid overhead of calling sp_unprepare too often|
|int getServerPreparedStatementDiscardThreshold()|This setting controls how many outstanding prepared statement discard actions (sp_unprepare) can be outstanding per connection before a call to clean up the outstanding handles on the server is executed. If the setting is <= 1 unprepare actions are executed immediately on prepared statement close. If it is set to {@literal >} 1 these calls are batched together to avoid overhead of calling sp_unprepare too often.|

## Prepared Statement Metatada caching
As of 6.3.0-preview version, Microsoft JDBC driver for SQL Server supports prepared statement caching. Prior to v6.3.0-preview, if one executes a query that has been already prepared and stored in the cache, calling the same query again will not result in preparing it. Now, the driver looks up the query in cache and find the handle and execute it with sp_execute.
Prepared Statement Metadata caching is **disabled** by default. In order to enable it, you need to call the following method on the connection object:

`setStatementPoolingCacheSize(int value)   //value is the desired cache size (any value bigger than 0)`
`setDisableStatementPooling(boolean value) //false allows the caching to take place`

For example:
`connection.setStatementPoolingCacheSize(10)`
`connection.setDisableStatementPooling(false)`

### List of the New APIs Introduced With This Change, for Prepared Statement Metadata Caching

 **SQLServerConnection**
 
|New Method|Description|  
|-----------|-----------------|  
|void setDisableStatementPooling(boolean value)|Sets statement pooling to true or false.|
|boolean getDisableStatementPooling()|Returns true if statement pooling is disabled.|
|void setStatementPoolingCacheSize(int value)|Specifies the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementPoolingCacheSize()|Returns the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementHandleCacheEntryCount()|Returns the current number of pooled prepared statement handles.|
|boolean isPreparedStatementCachingEnabled()|Whether statement pooling is enabled or not for this connection.|

 **SQLServerDataSource**
 
|New Method|Description|  
|-----------|-----------------|  
|void setDisableStatementPooling(boolean disableStatementPooling)|Sets the statement pooling to true or false|
|boolean getDisableStatementPooling()|Returns true if statement pooling is disabled.|
|void setStatementPoolingCacheSize(int statementPoolingCacheSize)|Specifies the size of the prepared statement cache for this connection. A value less than 1 means no cache.|
|int getStatementPoolingCacheSize()|Returns the size of the prepared statement cache for this connection. A value less than 1 means no cache.|

## See Also  
 [Improving Performance and Reliability with the JDBC Driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)  
  
  
