---
title: "JDBC Driver API Reference | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: e4e1ae9d-18a6-41db-8bd2-9cf0eee4cccb
author: MightyPen
ms.author: genemi
manager: craigg
---
# JDBC Driver API Reference

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] provides an API that can be used within Java programming code to connect to and interact with a [!INCLUDE[msCoName](../../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.



### JavaDoc.io Website is Primary

The Microsoft JDBC API Reference documentation is hosted for your viewing on the JavaDoc.io website. JavaDoc.io is now our primary website for JDBC reference documentation. Our JDBC reference documentation on JavaDoc.io is available at the following direct link:

- [https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/](https://javadoc.io/doc/com.microsoft.sqlserver/mssql-jdbc/)

JavaDoc.io has our JDBC reference documentation starting with version 6.0.

#### Only Legacy JDBC Documentation is Here on Docs

The JDBC API Reference documentation articles here on **https://docs.microsoft.com/sql/connect/jdbc/reference/** are no longer being updated when the JDBC classes are updated for new releases. However, the articles here do contain all the reference for JDBC versions 4.1 and 4.2.

Documentation for JDBC version 6.0, and some later versions, is also here. But for any version 6.0 or later, use the JavaDoc.io website.



### Important Notes

> [!NOTE]  
>  For conceptual information about using the JDBC driver, see [Overview of the JDBC Driver](../../../connect/jdbc/overview-of-the-jdbc-driver.md).  
  
> [!IMPORTANT]  
>  For JDBC 4.1 and 4.2 compliance support, use Microsoft JDBC Driver 4.2 (or higher) for SQL Server. The previous Microsoft JDBC Drivers 4.1 and 4.0 releases do not support new methods introduced with JDBC 4.1 or 4.2.  
>   
>  API details for JDBC 4.1 compliance are not in this section. See [JDBC 4.1 Compliance for the JDBC Driver](../../../connect/jdbc/jdbc-4-1-compliance-for-the-jdbc-driver.md).  
>   
>  API details for JDBC 4.2 compliance are not found in this section. See [JDBC 4.2 Compliance for the JDBC Driver](../../../connect/jdbc/jdbc-4-2-compliance-for-the-jdbc-driver.md).  
>   
>  API details for Bulk Copy, available starting with Microsoft JDBC Driver 4.2  for SQL Server, are not found in this section. See [Using Bulk Copy with the JDBC Driver](../../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md).  
>   
>  API details for Always Encrypted, available starting with  Microsoft JDBC Driver 6.0 for SQL Server, are not found in this section. See [Always Encrypted API Reference for the JDBC Driver](../../../connect/jdbc/always-encrypted-api-reference-for-the-jdbc-driver.md)  
>   
>  API details for Using Table-Valued Parameters, available starting with  Microsoft JDBC Driver 6.0 for SQL Server, are not found in this section. See [Using Table-Valued Parameters](../../../connect/jdbc/using-table-valued-parameters.md)  
>   
>  Microsoft JDBC Driver 6.4 supports compilation with JDK 7.0, 8.0, and 9.0.  
>   
>  Microsoft JDBC Driver 6.2 supports compilation with JDK 7.0, and 8.0.  
>   
>  Microsoft JDBC Drivers 6.0 and 4.2 support compilation with JDK 5.0, 6.0, 7.0, and 8.0.  
>   
>  Microsoft JDBC Driver 4.1 supports compilation with JDK 5.0, 6.0, and 7.0.  



## Interfaces  
  
|Interface Name|Description|  
|--------------------|-----------------|  
|[ISQLServerCallableStatement Interface](../../../connect/jdbc/reference/isqlservercallablestatement-interface.md)|Lets you specify the stored procedure name to call along with input and output parameters.|  
|[ISQLServerConnection Interface](../../../connect/jdbc/reference/isqlserverconnection-interface.md)|Represents a JDBC connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.|  
|[SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)|Represents a list of properties specific to connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database by using a [ISQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.|  
|[ISQLServerPreparedStatement](../../../connect/jdbc/reference/isqlserverpreparedstatement-interface.md)|Represents the basic implementation of JDBC prepared statement functionality.|  
|[ISQLServerResultSet](../../../connect/jdbc/reference/isqlserverresultset-interface.md)|Represents a JDBC result set.|  
|[ISQLServerStatement](../../../connect/jdbc/reference/isqlserverstatement-interface.md)|Represents the basic implementation of JDBC statement functionality.|
| &nbsp; | &nbsp; |


  
## Classes  
  
|Class Name|Description|  
|----------------|-----------------|  
|[DateTimeOffset](../../../connect/jdbc/reference/datetimeoffset-class.md)|Represents an object of type microsoft.sql.DateTimeOffset.|  
|[SQLServerBlob](../../../connect/jdbc/reference/sqlserverblob-class.md)|Represents a binary large object (BLOB).|  
|[SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)|Implements ISQLServerCallableStatement.|  
|[SQLServerClob](../../../connect/jdbc/reference/sqlserverclob-class.md)|Represents a character large binary object (CLOB).|  
|[SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md)|Implements ISQLServerConnectopn.|  
|[SQLServerConnectionPoolDataSource](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md)|Represents physical database connections for connection pool managers.|  
|[SQLServerDatabaseMetaData](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)|Represents the metadata for the database.|  
|[SQLServerDataSource](../../../connect/jdbc/reference/isqlserverdatasource-interface.md)|Represents a list of properties specific to connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database by using a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.|  
|[SQLServerDataSourceObjectFactory](../../../connect/jdbc/reference/sqlserverdatasourceobjectfactory-class.md)|Represents an object factory to materialize data sources from the Java Naming and Directory Interface (JNDI).|  
|[SQLServerDriver](../../../connect/jdbc/reference/sqlserverdriver-class.md)|Represents the JDBC driver. This class includes methods for connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, and for obtaining information about the JDBC driver.|  
|[SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)|Represents an unsuccessful or incomplete running of an SQL statement.|  
|[SQLServerNClob Class](../../../connect/jdbc/reference/sqlservernclob-class.md)|Represents a character large binary object using the National Character Set.|  
|[SQLServerParameterMetaData](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)|Represents the metadata for prepared statement parameters.|  
|[SQLServerPooledConnection](../../../connect/jdbc/reference/sqlserverpooledconnection-class.md)|Represents a physical database connection in a connection pool.|  
|[SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)|Implements ISQLServerPreparedStatement.|  
|[SQLServerResource](../../../connect/jdbc/reference/sqlserverresource-class.md)|Represents a localized error string resource. This class is intended for internal use only.|  
|[SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md)|Implements ISQLServerResultSet.|  
|[SQLServerResultSetMetaData](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)|Represents the metadata of the columns that are contained within a result set.|  
|[SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md)|Represents the checkpoint to which a transaction can be rolled back.|  
|[SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md)|Implements ISQLServerStatement.|  
|[SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md)|Represents JDBC connections that can participate in distributed (XA) transactions.|  
|[SQLServerXADataSource](../../../connect/jdbc/reference/sqlserverxadatasource-class.md)|Represents a factory for [SQLServerXAConnection](../../../connect/jdbc/reference/sqlserverxaconnection-class.md) objects that is used internally.|  
|[SQLServerXAResource](../../../connect/jdbc/reference/sqlserverxaresource-class.md)|Represents an XAResource for XA distributed transaction management.|
| &nbsp; | &nbsp; |



## See Also  
 [Overview of the JDBC Driver](../../../connect/jdbc/overview-of-the-jdbc-driver.md)

