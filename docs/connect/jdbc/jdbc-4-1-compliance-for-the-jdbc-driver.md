---
title: "JDBC 4.1 Compliance for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: f087fd40-8451-478e-b465-43112c711515
author: MightyPen
ms.author: genemi
manager: craigg
---
# JDBC 4.1 Compliance for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

    
> [!NOTE]  
>  Versions prior to Microsoft JDBC Driver 4.2 for SQL Server are compliant for Java Database Connectivity API 4.0 specifications. This section does not apply for versions prior to the 4.2 release.  
  
 The Java Database Connectivity API 4.1 specification is supported by the Microsoft JDBC Driver 4.2 for SQL Server, with the following API methods.  
  
 **SQLServerConnection class**  
  
|New Method|Description|JDBC Driver Implementation|  
|----------------|-----------------|--------------------------------|  
|void abort(Executor executor)|Terminates an open connection to SQL Server.|Implemented as described in the java.sql.Connection interface. For more details see [java.sql.Connection](https://docs.oracle.com/javase/7/docs/api/java/sql/Connection.html).|  
|void setSchema(String schema)|Sets schema for the current connection.|SQL Server does not support setting schema for the current session. The driver silently logs a warning message if this method is called. For more details see [java.sql.Connection](https://docs.oracle.com/javase/7/docs/api/java/sql/Connection.html).|  
|String getSchema()|Returns the schema name for the current connection.|Since SQL Server does not support setting schema for the current connection, the driver instead returns the default schema of the user. For more details see [java.sql.Connection](https://docs.oracle.com/javase/7/docs/api/java/sql/Connection.html).|  
  
 **SQLServerDatabaseMetaData class**  
  
|New Method|Description|JDBC Driver Implementation|  
|----------------|-----------------|--------------------------------|  
|boolean generatedKeyAlwaysReturned()|Returns true as the driver supports retrieving generated keys|Implemented as described in the java.sql. DatabaseMetaData interface. For more details, see [java.sql.DatabaseMetaData](https://docs.oracle.com/javase/7/docs/api/java/sql/DatabaseMetaData.html).|  
|ResultSet getPseudoColumns(String catalog, String schemaPattern,String tableNamePattern,String columnNamePattern)|Retrieves a description of the pseudo/hidden columns|Return an empty result set as SQL Server does not have a formal notion of pseudo-columns. For more details, see [java.sql.DatabaseMetaData](https://docs.oracle.com/javase/7/docs/api/java/sql/DatabaseMetaData.html).|  
  
 **SQLServerStatement class**  
  
|New Method|Description|JDBC Driver Implementation|  
|----------------|-----------------|--------------------------------|  
|void closeOnCompletion()|Specifies that this Statement will be closed when all its dependent result sets are closed.|Implemented as described in the java.sql.Statement interface. For more details, see [java.sql.Statement](https://docs.oracle.com/javase/7/docs/api/java/sql/Statement.html).|  
|boolean isCloseOnCompletion()|Returns a value indicating whether this Statement will be closed when all its dependent result sets are closed.|Implemented as described in the java.sql.Statement interface. For more details, see [java.sql.Statement](https://docs.oracle.com/javase/7/docs/api/java/sql/Statement.html).|  
  
 The Java Database Connectivity API 4.1 specification is supported by the Microsoft JDBC Driver 4.2 for SQL Server, with the following features.  
  
|New Feature|Description|  
|-----------------|-----------------|  
|New Escape Function<br /><br /> Limited Return Rows Escape|Partially supported<br /><br /> Escape syntax: LIMIT \<rows> [OFFSET <row_offset>](using-sql-escape-sequences.md).|  
  
 The Java Database Connectivity API 4.1 specification is supported by the Microsoft JDBC Driver 4.2 for SQL Server, with the following Data Type Mappings.  
  
|Data Type Mappings|Description|  
|------------------------|-----------------|  
|New data type mappings are now supported in PreparedStatement.setObject() and PreparedStatement.setNull() methods.|1. New Java to JDBC type mapping<br /><br /> (a) java.math.BigInteger to JDBC BIGINT<br /><br /> (b) java.util.Date and java.util.Calendar to JDBC TIMESTAMP<br /><br /> 2. New data type conversions:<br /><br /> (a) java.math.BigInteger to CHAR, VARCHAR, LONGVARCHAR and BIGINT<br /><br /> (b) java.util.Date and java.util.Calendar to CHAR, VARCHAR, LONGVARCHAR, DATE, TIME and TIMESTAMP<br /><br /> For more details, see JDBC 4.1 specification.|  
  
  
