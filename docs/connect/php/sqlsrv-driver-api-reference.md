---
title: "SQLSRV Driver API Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 0b55da26-ddeb-4e89-872a-91e0aba57103
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSRV Driver API Reference
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The API name for the SQLSRV driver in the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] is **sqlsrv**. All **sqlsrv** functions begin with **sqlsrv_** and are followed by a verb or a noun. Those followed by a verb perform some action and those followed by a noun return some form of metadata.  
  
## In This Section  
The SQLSRV driver contains the following functions:  
  
|Function|Description|  
|------------|---------------|  
|[sqlsrv_begin_transaction](../../connect/php/sqlsrv-begin-transaction.md)|Begins a transaction.|  
|[sqlsrv_cancel](../../connect/php/sqlsrv-cancel.md)|Cancels a statement; discards any pending results for the statement.|  
|[sqlsrv_client_info](../../connect/php/sqlsrv-client-info.md)|Provides information about the client.|  
|[sqlsrv_close](../../connect/php/sqlsrv-close.md)|Closes a connection. Frees all resources associated with the connection.|  
|[sqlsrv_commit](../../connect/php/sqlsrv-commit.md)|Commits a transaction.|  
|[sqlsrv_configure](../../connect/php/sqlsrv-configure.md)|Changes error handling and logging configurations.|  
|[sqlsrv_connect](../../connect/php/sqlsrv-connect.md)|Creates and opens a connection.|  
|[sqlsrv_errors](../../connect/php/sqlsrv-errors.md)|Returns error and/or warning information about the last operation.|  
|[sqlsrv_execute](../../connect/php/sqlsrv-execute.md)|Executes a prepared statement.|  
|[sqlsrv_fetch](../../connect/php/sqlsrv-fetch.md)|Makes the next row of data available for reading.|  
|[sqlsrv_fetch_array](../../connect/php/sqlsrv-fetch-array.md)|Retrieves the next row of data as a numerically indexed array, an associative array, or both.|  
|[sqlsrv_fetch_object](../../connect/php/sqlsrv-fetch-object.md)|Retrieves the next row of data as an object.|  
|[sqlsrv_field_metadata](../../connect/php/sqlsrv-field-metadata.md)|Returns field metadata.|  
|[sqlsrv_free_stmt](../../connect/php/sqlsrv-free-stmt.md)|Closes a statement. Frees all resources associated with the statement.|  
|[sqlsrv_get_config](../../connect/php/sqlsrv-get-config.md)|Returns the value of the specified configuration setting.|  
|[sqlsrv_get_field](../../connect/php/sqlsrv-get-field.md)|Retrieves a field in the current row by index. The PHP return type can be specified.|  
|[sqlsrv_has_rows](../../connect/php/sqlsrv-has-rows.md)|Detects if a result set has one or more rows.|  
|[sqlsrv_next_result](../../connect/php/sqlsrv-next-result.md)|Makes the next result available for processing.|  
|[sqlsrv_num_rows](../../connect/php/sqlsrv-num-rows.md)|Reports the number of rows in a result set.|  
|[sqlsrv_num_fields](../../connect/php/sqlsrv-num-fields.md)|Retrieves the number of fields in an active result set.|  
|[sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md)|Prepares a Transact-SQL query without executing it. Implicitly binds parameters.|  
|[sqlsrv_query](../../connect/php/sqlsrv-query.md)|Prepares and executes a Transact-SQL query.|  
|[sqlsrv_rollback](../../connect/php/sqlsrv-rollback.md)|Rolls back a transaction.|  
|[sqlsrv_rows_affected](../../connect/php/sqlsrv-rows-affected.md)|Returns the number of modified rows.|  
|[sqlsrv_send_stream_data](../../connect/php/sqlsrv-send-stream-data.md)|Sends up to eight kilobytes (8 KB) of data to the server with each call to the function.|  
|[sqlsrv_server_info](../../connect/php/sqlsrv-server-info.md)|Provides information about the server.|  
  
## Reference  
[PHP Manual](https://php.net/manual)  
  
## See Also  
[Overview of the Microsoft Drivers for PHP for SQL Server](../../connect/php/overview-of-the-php-sql-driver.md)

[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[Getting Started with the Microsoft Drivers for PHP for SQL Server](../../connect/php/getting-started-with-the-php-sql-driver.md)
  
