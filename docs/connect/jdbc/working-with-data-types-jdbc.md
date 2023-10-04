---
title: "Working with data types (JDBC)"
description: "Learn how to work with data types in the JDBC Driver for SQL Server through these sample applications."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Working with data types (JDBC)

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The primary function of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] is to allow Java developers to access data contained in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. To accomplish this, the JDBC driver mediates the conversion between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and Java language types and objects.

> [!NOTE]
> For a detailed discussion of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and JDBC driver data types, including their differences and how they are converted to Java language data types, see [Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md).

In order to work with SQL Server data types, the JDBC driver provides get\<Type> and set\<Type> methods for the [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) classes, and it provides get\<Type> and update\<Type> methods for the [SQLServerResultSet](reference/sqlserverresultset-class.md) class. Which method you use depends on the type of data that you are working with, and whether you are using result sets or queries.

The topics in this section describe how to use the JDBC driver data types to access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data in your Java applications.

## In this section

|Topic|Description|
|-----------|-----------------|
|[Basic data types sample](basic-data-types-sample.md)|Describes how to use result set getter methods to retrieve basic [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type values, and how to use result set update methods to update those values.|
|[SQLXML data type sample](sqlxml-data-type-sample.md)|Describes how to store an XML data in a relational database, how to retrieve an XML data from a database, and how to parse an XML data with the **SQLXML** Java data type.|
|[Spatial data types sample](spatial-data-types-sample.md)|Describes how to store and retrieve data with Spatial Datatypes 'Geometry' and 'Geography' of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database with **Geometry** and **Geography** Java types defined by Microsoft JDBC Driver.|

## See also

[Sample JDBC driver applications](sample-jdbc-driver-applications.md)
