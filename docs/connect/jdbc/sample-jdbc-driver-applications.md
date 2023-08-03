---
title: Sample applications
description: The JDBC Driver for SQL Server sample applications demonstrate various features and good programming practices that you can follow when using the JDBC driver.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Sample JDBC driver applications

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sample applications demonstrate various features of the JDBC driver. Additionally, they demonstrate good programming practices that you can follow when using the JDBC driver with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.

All the sample applications are contained in *.java code files that can be compiled and run on your local computer, and they are located in various subfolders in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples
```

The articles in this section describe how to configure and run the sample applications, and include a discussion of what the sample applications demonstrate.

## In this section

| Article | Description |
|--|--|
| [Connecting and Retrieving Data](connecting-and-retrieving-data.md) | These sample applications demonstrate how to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. They also demonstrate different ways in which to retrieve data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| [Working with Data Types &#40;JDBC&#41;](working-with-data-types-jdbc.md) | These sample applications demonstrate how to use the JDBC driver data type methods to work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| [Working with Result Sets](working-with-result-sets.md) | These sample applications demonstrate how to use result sets to process data contained in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| [Working with Large Data](working-with-large-data.md) | These sample applications demonstrate how to use adaptive buffering to retrieve large-value data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database without the overhead of server cursors. |
| [SQL Data Discovery and Classification](data-discovery-classification-sample.md) | This sample application demonstrates how to retrieve Data Discovery and Classification information contained in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database from a ResultSet object using JDBC Driver. |

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
