---
title: "Using Result Set Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 5e37529a-30db-48c8-b90a-ae9657d0f6b0
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using Result Set Metadata

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To query a result set for information about the columns that it contains, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] implements the [SQLServerResultSetMetaData](../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md) class. This class contains numerous methods that return information in the form of a single value.

To create a SQLServerResultSetMetaData object, you can use the [getMetaData](../../connect/jdbc/reference/getmetadata-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, the getMetaData method of the SQLServerResultSet class is used to return a SQLServerResultSetMetaData object, and then various methods of the SQLServerResultSetMetaData object are used to display information about the name and data type of the columns contained within the result set.

[!code[JDBC#UsingResultSetMetaData1](../../connect/jdbc/codesnippet/Java/using-result-set-metadata_1.java)]

## See Also

[Handling Metadata with the JDBC Driver](../../connect/jdbc/handling-metadata-with-the-jdbc-driver.md)
