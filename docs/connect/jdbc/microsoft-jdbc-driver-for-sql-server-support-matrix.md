---
title: Support matrix
description: This page contains the support matrix and support life-cycle policy for the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Microsoft JDBC Driver for SQL Server support matrix

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page contains the support matrix and support lifecycle policy for the Microsoft JDBC Driver for SQL Server.

## Microsoft JDBC Driver support lifecycle matrix and policy

The Microsoft Support Lifecycle (MSL) policy provides transparent, predictable information regarding the support lifecycle of Microsoft products. JDBC driver versions 4.x, 6.x, 7.x, 8.x, 9.x, 10.x, and 11.x have five-year Mainstream support from the driver release date. Mainstream support is defined on the Microsoft support lifecycle website.

Extended and custom support options are not available for the Microsoft JDBC Driver.

The following Microsoft JDBC Drivers are supported, until the indicated End of Support date.

|Driver Name|Driver Package Version|Applicable JAR(s)|End of Mainstream Support|
|-|-|-|-|
|Microsoft JDBC Driver 11.2 for SQL Server|11.2|mssql-jdbc-11.2.0.jre18.jar<br> mssql-jdbc-11.2.0.jre17.jar<br> mssql-jdbc-11.2.0.jre11.jar<br> mssql-jdbc-11.2.0.jre8.jar|August 4, 2027|
|Microsoft JDBC Driver 10.2 for SQL Server|10.2|mssql-jdbc-10.2.0.jre17.jar<br> mssql-jdbc-10.2.0.jre11.jar<br> mssql-jdbc-10.2.0.jre8.jar|January 31, 2027|
|Microsoft JDBC Driver 9.4 for SQL Server|9.4|mssql-jdbc-9.4.1.jre16.jar<br> mssql-jdbc-9.4.1.jre11.jar<br> mssql-jdbc-9.4.1.jre8.jar|July 30, 2026|
|Microsoft JDBC Driver 9.2 for SQL Server|9.2|mssql-jdbc-9.2.1.jre15.jar<br> mssql-jdbc-9.2.1.jre11.jar<br> mssql-jdbc-9.2.1.jre8.jar|January 29, 2026|
|Microsoft JDBC Driver 8.4 for SQL Server|8.4|mssql-jdbc-8.4.1.jre14.jar<br> mssql-jdbc-8.4.1.jre11.jar<br> mssql-jdbc-8.4.1.jre8.jar|July 31, 2025|
|Microsoft JDBC Driver 8.2 for SQL Server|8.2|mssql-jdbc-8.2.2.jre13.jar<br> mssql-jdbc-8.2.2.jre11.jar<br> mssql-jdbc-8.2.2.jre8.jar|January 31, 2025|
|Microsoft JDBC Driver 7.4 for SQL Server|7.4|mssql-jdbc-7.4.1.jre12.jar<br> mssql-jdbc-7.4.1.jre11.jar<br> mssql-jdbc-7.4.1.jre8.jar|July 31, 2024|
|Microsoft JDBC Driver 7.2 for SQL Server|7.2|mssql-jdbc-7.2.2.jre11.jar<br> mssql-jdbc-7.2.2.jre8.jar|January 31, 2024|
|Microsoft JDBC Driver 7.0 for SQL Server|7.0|mssql-jdbc-7.0.0.jre10.jar<br> mssql-jdbc-7.0.0.jre8.jar|July 31, 2023|
|Microsoft JDBC Driver 6.4 for SQL Server|6.4|mssql-jdbc-6.4.0.jre9.jar<br> mssql-jdbc-6.4.0.jre8.jar<br> mssql-jdbc-6.4.0.jre7.jar|February 27, 2023|

 The following Microsoft JDBC Drivers are no longer supported.

|Driver Name|Driver Package Version|End of Mainstream Support|
|-|-|-|
|Microsoft JDBC Driver 6.2 for SQL Server|6.2|June 30, 2022|
|Microsoft JDBC Driver 6.0 for SQL Server|6.0|July 14, 2021|
|Microsoft JDBC Driver 4.2 for SQL Server|4.2|August 24, 2020|
|Microsoft JDBC Driver 4.1 for SQL Server|4.1|December 12, 2019|
|Microsoft JDBC Driver 4.0 for SQL Server|4.0|March 6, 2017|
|Microsoft SQL Server JDBC Driver 3.0|3.0|April 23, 2015|
|Microsoft SQL Server JDBC Driver 2.0|2.0|December 31, 2012|
|Microsoft SQL Server 2005 JDBC Driver 1.2|1.2|June 25, 2011|
|Microsoft SQL Server 2005 JDBC Driver 1.1|1.1|June 25, 2011|
|Microsoft SQL Server 2005 JDBC Driver 1.0|1.0|June 25, 2011|
|Microsoft SQL Server 2000 JDBC Driver|2000|July 9, 2010|

## SQL version compatibility

|Database version&nbsp;&#8594;<br />&#8595; Driver Version|Azure SQL Database|Azure Synapse Analytics|Azure SQL Managed Instance|SQL Server 2019|SQL Server 2017|SQL Server 2016|SQL Server 2014|SQL Server 2012|PDW 2008R2 AU3<sup>4</sup>|SQL Server 2008 R2|SQL Server 2008|
|---|---|---|---|---|---|---|---|---|---|---|---|
|11.2|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |   |
|10.2|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
| 9.4|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
| 9.2|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
| 8.4|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
| 8.2|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
| 7.4|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
| 7.2|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
| 7.0|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
| 6.4|Yes|Yes|Yes|   |Yes|Yes|Yes|Yes|Yes|Yes|   |
| 6.2|Yes|Yes|   |   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|
| 6.1|Yes|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
| 6.0|Yes|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
| 4.2|Yes|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
| 4.1|Yes|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
| 4.0|Yes|   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
| 3.0|Yes<sup>2</sup>|   |   |   |   |   |Yes<sup>5</sup>|Yes<sup>1</sup>|   |Yes|Yes|
| 2.0|   |   |   |   |   |   |   |   |   |Yes<sup>3</sup>|Yes<sup>3</sup>|
| 1.2|   |   |   |   |   |   |   |   |   |   |Yes<sup>3</sup>|

 <sup>1</sup> Microsoft SQL Server JDBC Driver version 3.0 can connect to SQL Server 2012 as a down-level client.

 <sup>2</sup> Support for Azure SQL Database was introduced in the 3.0 driver as a hotfix. We recommend that Azure SQL Database customers use the latest driver version available.

 <sup>3</sup> Microsoft SQL Server JDBC Driver version 2.0 and Microsoft SQL Server 2005 JDBC Driver version 1.2 can connect to SQL Server 2008 as a down-level client. When down-level conversions are allowed, applications can execute queries and perform updates on the new SQL Server 2008 data types, such as time, date, datetime2, datetimeoffset, and FILESTREAM. For more information about how to use these new data types with the JDBC driver, see  [Working with SQL Server 2008 Date/Time Data Types using JDBC Driver](/archive/blogs/jdbcteam/) and  [Working with SQL Server 2008 FileStream using JDBC Driver](/archive/blogs/jdbcteam/). For more information about the down-level compatibility of these new data types, see  [Using Date and Time Data](/previous-versions/sql/sql-server-2008-r2/ms180878(v=sql.105))and  [FILESTREAM Support](../../relational-databases/native-client/features/filestream-support.md) topics in SQL Server Books Online.

 <sup>4</sup> Support for connections between the Microsoft JDBC Driver and Parallel Data Warehouse was first introduced in the Microsoft JDBC Driver 4.0 for SQL Server and Microsoft SQL Server 2008 R2 Parallel Data Warehouse Appliance Update 3.

 <sup>5</sup> Microsoft SQL Server JDBC Driver version 3.0 can connect to SQL Server 2014 as a down-level client.

## Java and JDBC specification support

|JDBC Driver Version|JRE Versions|JDBC API Version|
|-|-|-|
|[11.2](release-notes-for-the-jdbc-driver.md#112)|1.8, 11, 17, 18|4.2, 4.3 (partially)|
|[10.2](release-notes-for-the-jdbc-driver.md#102)|1.8, 11, 17|4.2, 4.3 (partially)|
|[9.4](release-notes-for-the-jdbc-driver.md#94)|1.8, 11, 16|4.2, 4.3 (partially)|
|[9.2](release-notes-for-the-jdbc-driver.md#92)|1.8, 11, 15|4.2, 4.3 (partially)|
|[8.4](release-notes-for-the-jdbc-driver.md#84)|1.8, 11, 14|4.2, 4.3 (partially)|
|[8.2](release-notes-for-the-jdbc-driver.md#82)|1.8, 11, 13|4.2, 4.3 (partially)|
|[7.4](release-notes-for-the-jdbc-driver.md#74)|1.8, 11, 12|4.2, 4.3 (partially)|
|[7.2](release-notes-for-the-jdbc-driver.md#72)|1.8, 11|4.2, 4.3 (partially)|
|[7.0](release-notes-for-the-jdbc-driver.md#70)|1.8, 10|4.2, 4.3 (partially)|
|[6.4](release-notes-for-the-jdbc-driver.md#64)|1.7, 1.8, 9|4.1, 4.2, 4.3 (partially)|
|[6.2](release-notes-for-the-jdbc-driver.md#62)|1.7, 1.8|4.1, 4.2|
|[6.1](release-notes-for-the-jdbc-driver.md#61)|1.7, 1.8|4.1, 4.2|
|[6.0](release-notes-for-the-jdbc-driver.md#60)|1.7, 1.8|4.1, 4.2|
|4.2|1.7, 1.8|4.1, 4.2|
|4.1|1.7|4.0|
|4.0|1.5, 1.6, 1.7|3.0, 4.0|
|3.0|1.5, 1.6,|3.0, 4.0|
|2.0|1.5, 1.6|3.0, 4.0|
|1.2|1.4, 1.5, 1.6|3.0|
|1.1|1.4|3.0|
|1.0|1.4|3.0|
|2000|1.4|3.0|

## Supported operating systems

The Microsoft JDBC driver is designed to work on any operating system that supports the use of a Java Virtual Machine (JVM). Some commonly used platforms include Windows 10, Windows 8.1, Windows 8, Windows 7, Windows Server 2008 R2, Linux, Unix, AIX, macOS, and others.

The JDBC product team tests our driver on Windows, Sun Solaris, SUSE Linux, Ubuntu Linux, CentOS Linux, and macOS.

## Application server support

The Microsoft JDBC Driver for SQL Server is tested with various application servers.  Consult your application server vendor for additional details on which driver version is compatible with their product.
