---
title: JDBC 4.3 compliance
description: Read about how the JDBC Driver for SQL Server is compliant with the JDBC 4.3 specification.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# JDBC 4.3 compliance for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

> [!NOTE]
> Versions prior to Microsoft JDBC Driver 6.4 for SQL Server are only compliant for Java Database Connectivity (JDBC) API 4.2 specifications. This section does not apply for versions including and prior to the 6.4 release.

As of version 6.4, Microsoft JDBC Driver for SQL Server is JAVA 9 compatible and throws  `SQLFeatureNotSupportedException` for new JDBC 4.3 APIs that have unimplemented methods.

With Microsoft JDBC Driver 7.0 for SQL Server release, the driver is now JAVA 10 compatible, and supports below mentioned APIs. The driver throws `SQLFeatureNotSupportedException` for other unimplemented methods from JDBC 4.3 Specifications.

|New API|Description|Noteworthy Implementation|
|-----------------|-----------------|-------------------------------|
|void java.sql.connection.beginRequest()|Hints to the driver that a request, an independent unit of work, is beginning on this connection. For more details, see [java.sql.Connection](https://docs.oracle.com/javase/9/docs/api/java/sql/Connection.html#beginRequest--).|Saves the values of the connection fields that are modifiable through public API methods: `databaseAutoCommitMode`, `transactionIsolationLevel`, `networkTimeout`, `holdability`, `sendTimeAsDatetime`, `statementPoolingCacheSize`, `disableStatementPooling`, `serverPreparedStatementDiscardThreshold`, `enablePrepareOnFirstPreparedStatementCall`, `catalogName`, `sqlWarnings`, `useBulkCopyForBatchInsert`.|
|void java.sql.connection.endRequest()|Hints to the driver that a request, an independent unit of work, has completed. For more details, see [java.sql.Connection](https://docs.oracle.com/javase/9/docs/api/java/sql/Connection.html#endRequest--).|Closes the statements that are created during the work unit and rolls back any open transactions. The method also reverts the changes to the connection fields that are listed above.|
