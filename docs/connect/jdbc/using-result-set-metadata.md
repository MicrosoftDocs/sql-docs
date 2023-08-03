---
title: "Using result set metadata"
description: "Using result set metadata"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using result set metadata

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To query a result set for information about the columns that it contains, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] implements the [SQLServerResultSetMetaData](../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md) class. This class contains numerous methods that return information in the form of a single value.

To create a SQLServerResultSetMetaData object, you can use the [getMetaData](../../connect/jdbc/reference/getmetadata-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function, the getMetaData method of the SQLServerResultSet class is used to return a SQLServerResultSetMetaData object, and then various methods of the SQLServerResultSetMetaData object are used to display information about the name and data type of the columns contained within the result set.

[!code[JDBC#UsingResultSetMetaData1](../../connect/jdbc/codesnippet/Java/using-result-set-metadata_1.java)]

## See also

[Handling metadata with the JDBC driver](../../connect/jdbc/handling-metadata-with-the-jdbc-driver.md)
