---
title: "Using Parameter Metadata | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: db2c1957-91c6-4989-a07b-9f8be6d2033a
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using Parameter Metadata

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To query a [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) or a [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) object about the parameters that they contain, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] implements the [SQLServerParameterMetaData](../../connect/jdbc/reference/sqlserverparametermetadata-class.md) class. This class contains numerous fields and methods that return information in the form of a single value.

To create a SQLServerParameterMetaData object, you can use the [getParameterMetaData](../../connect/jdbc/reference/getparametermetadata-method-sqlserverpreparedstatement.md) methods of the SQLServerPreparedStatement and SQLServerCallableStatement classes.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, the getParameterMetaData method of the SQLServerCallableStatement class is used to return a SQLServerParameterMetaData object, and then various methods of the SQLServerParameterMetaData object are used to display information about the type and mode of the parameters that are contained within the HumanResources.uspUpdateEmployeeHireInfo stored procedure.

[!code[JDBC#UsingParamMetaData1](../../connect/jdbc/codesnippet/Java/using-parameter-metadata_1.java)]  

> [!NOTE]  
> There are some limitations when using the SQLServerParameterMetaData class with prepared statements.
>
> **With Microsoft JDBC Driver 6.0 (or higher) for SQL Server**:
> When using SQL Server 2008 or 2008 R2, the JDBC driver supports SELECT, DELETE, INSERT, and UPDATE statements as long as these statements does not contain subqueries and/or joins.

MERGE queries are also not supported for SQLServerParameterMetaData class when using SQL Server 2008 or 2008 R2. For SQL Server 2012 and higher versions parameter metadata with complex queries are supported.

Retrieval of parameter metadata for encrypted columns are not supported. **With Microsoft JDBC Driver 4.1 or 4.2 for SQL Server**: The JDBC driver supports SELECT, DELETE, INSERT, and UPDATE statements as long as these statements does not contain subqueries and/or joins. MERGE queries are also not supported for SQLServerParameterMetaData class.
