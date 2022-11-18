---
title: JDBC driver API reference
description: The technical API reference for the JDBC classes in the JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 01/11/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# JDBC driver API reference

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

## Overview

The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] provides an API that can be used within Java programming code to connect to and interact with a [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.

### JavaDoc.io website is primary

The Microsoft JDBC API Reference documentation is hosted for your viewing on the JavaDoc.io website. JavaDoc.io is now our primary website for JDBC reference documentation. Our JDBC reference documentation on JavaDoc.io is available at the following direct link:

- [https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/](https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/)

JavaDoc.io has our JDBC reference documentation starting with version 6.0.

#### Only legacy JDBC documentation is here on docs

The JDBC API Reference documentation here on this website is no longer being updated. However, the articles here do contain the reference for JDBC driver versions 4.1 and 4.2.

Documentation for JDBC driver version 6.0, and some later versions, is also here. For any version 6.0 or later, use the [JavaDoc.io website](https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/).

### Important notes

> [!NOTE]
> For conceptual information about using the JDBC driver, see [Overview of the JDBC Driver](../overview-of-the-jdbc-driver.md).

> [!IMPORTANT]
> For JDBC 4.1 and 4.2 compliance support, use Microsoft JDBC Driver 4.2 (or higher) for SQL Server. The previous Microsoft JDBC Drivers 4.1 and 4.0 releases do not support new methods introduced with JDBC 4.1 or 4.2.
>
> API details for JDBC 4.1 compliance are not in this section. See [JDBC 4.1 Compliance for the JDBC Driver](../jdbc-4-1-compliance-for-the-jdbc-driver.md).
>
> API details for JDBC 4.2 compliance are not found in this section. See [JDBC 4.2 Compliance for the JDBC Driver](../jdbc-4-2-compliance-for-the-jdbc-driver.md).
>
> API details for Bulk Copy, available starting with Microsoft JDBC Driver 4.2  for SQL Server, are not found in this section. See [Using Bulk Copy with the JDBC Driver](../using-bulk-copy-with-the-jdbc-driver.md).
>
> API details for Always Encrypted, available starting with  Microsoft JDBC Driver 6.0 for SQL Server, are not found in this section. See [Always Encrypted API Reference for the JDBC Driver](../always-encrypted-api-reference-for-the-jdbc-driver.md)
>
> API details for Using Table-Valued Parameters, available starting with  Microsoft JDBC Driver 6.0 for SQL Server, are not found in this section. See [Using Table-Valued Parameters](../using-table-valued-parameters.md)
>
> Microsoft JDBC Driver 6.4 supports compilation with JDK 7.0, 8.0, and 9.0.
>
> Microsoft JDBC Driver 6.2 supports compilation with JDK 7.0, and 8.0.
>
> Microsoft JDBC Drivers 6.0 and 4.2 support compilation with JDK 5.0, 6.0, 7.0, and 8.0.
>
> Microsoft JDBC Driver 4.1 supports compilation with JDK 5.0, 6.0, and 7.0.

## Interfaces

|Interface Name|Description|
|--------------------|-----------------|
|[ISQLServerCallableStatement Interface](isqlservercallablestatement-interface.md)|Lets you specify the stored procedure name to call along with input and output parameters.|
|[ISQLServerConnection Interface](isqlserverconnection-interface.md)|Represents a JDBC connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|
|[SQLServerDataSource Class](sqlserverdatasource-class.md)|Represents a list of properties specific to connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database by using a [ISQLServerConnection](sqlserverconnection-class.md) object.|
|[ISQLServerPreparedStatement](isqlserverpreparedstatement-interface.md)|Represents the basic implementation of JDBC prepared statement functionality.|
|[ISQLServerResultSet](isqlserverresultset-interface.md)|Represents a JDBC result set.|
|[ISQLServerStatement](isqlserverstatement-interface.md)|Represents the basic implementation of JDBC statement functionality.|

## Classes

|Class Name|Description|
|----------------|-----------------|
|[DateTimeOffset](datetimeoffset-class.md)|Represents an object of type microsoft.sql.DateTimeOffset.|
|[SQLServerBlob](sqlserverblob-class.md)|Represents a binary large object (BLOB).|
|[SQLServerCallableStatement](sqlservercallablestatement-class.md)|Implements ISQLServerCallableStatement.|
|[SQLServerClob](sqlserverclob-class.md)|Represents a character large binary object (CLOB).|
|[SQLServerConnection](sqlserverconnection-class.md)|Implements ISQLServerConnectopn.|
|[SQLServerConnectionPoolDataSource](sqlserverconnectionpooldatasource-class.md)|Represents physical database connections for connection pool managers.|
|[SQLServerDatabaseMetaData](sqlserverdatabasemetadata-class.md)|Represents the metadata for the database.|
|[SQLServerDataSource](isqlserverdatasource-interface.md)|Represents a list of properties specific to connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database by using a [SQLServerConnection](sqlserverconnection-class.md) object.|
|[SQLServerDataSourceObjectFactory](sqlserverdatasourceobjectfactory-class.md)|Represents an object factory to materialize data sources from the Java Naming and Directory Interface (JNDI).|
|[SQLServerDriver](sqlserverdriver-class.md)|Represents the JDBC driver. This class includes methods for connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, and for obtaining information about the JDBC driver.|
|[SQLServerException](sqlserverexception-class.md)|Represents an unsuccessful or incomplete running of an SQL statement.|
|[SQLServerNClob Class](sqlservernclob-class.md)|Represents a character large binary object using the National Character Set.|
|[SQLServerParameterMetaData](sqlserverparametermetadata-class.md)|Represents the metadata for prepared statement parameters.|
|[SQLServerPooledConnection](sqlserverpooledconnection-class.md)|Represents a physical database connection in a connection pool.|
|[SQLServerPreparedStatement](sqlserverpreparedstatement-class.md)|Implements ISQLServerPreparedStatement.|
|[SQLServerResource](sqlserverresource-class.md)|Represents a localized error string resource. This class is intended for internal use only.|
|[SQLServerResultSet](sqlserverresultset-class.md)|Implements ISQLServerResultSet.|
|[SQLServerResultSetMetaData](sqlserverresultsetmetadata-class.md)|Represents the metadata of the columns that are contained within a result set.|
|[SQLServerSavepoint](sqlserversavepoint-class.md)|Represents the checkpoint to which a transaction can be rolled back.|
|[SQLServerStatement](sqlserverstatement-class.md)|Implements ISQLServerStatement.|
|[SQLServerXAConnection](sqlserverxaconnection-class.md)|Represents JDBC connections that can participate in distributed (XA) transactions.|
|[SQLServerXADataSource](sqlserverxadatasource-class.md)|Represents a factory for [SQLServerXAConnection](sqlserverxaconnection-class.md) objects that is used internally.|
|[SQLServerXAResource](sqlserverxaresource-class.md)|Represents an XAResource for XA distributed transaction management.|

## See also

[Overview of the JDBC driver](../overview-of-the-jdbc-driver.md)
