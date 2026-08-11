---
title: Support Matrix for Microsoft JDBC Driver
description: This page contains the support matrix and support life-cycle policy for the Microsoft JDBC Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, machavan, sunilbs
ms.date: 05/11/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: feature-availability
---
# Microsoft JDBC Driver for SQL Server support matrix

[!INCLUDE [Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page contains the support matrix and support lifecycle policy for the Microsoft JDBC Driver for SQL Server.

## Microsoft JDBC Driver support lifecycle matrix and policy

JDBC driver support follows the [fixed lifecycle policy](/lifecycle/policies/fixed), with at least five years of mainstream support from the driver release date of each major version. The latest minor version must be installed within 12 months of the minor version release date to continue to receive full support for the duration of the major version mainstream support period.

Extended and custom support options aren't available for the Microsoft JDBC Driver.

The following Microsoft JDBC Drivers major versions are supported, until the indicated End of Mainstream Support date. Each minor version must be upgraded to the latest minor version by the indicated upgrade deadline to continue receiving support:

| Major version | End of mainstream support | Driver name | Minor version | Upgrade by | Applicable JARs |
| --- | --- | --- | --- | --- | --- |
| 13 | March 13, 2031 | Microsoft JDBC Driver 13.4 for SQL Server | 13.4 | Current version | `mssql-jdbc-13.4.0.jre11.jar`<br />`mssql-jdbc-13.4.0.jre8.jar` |
| | | Microsoft JDBC Driver 13.2 for SQL Server | 13.2 | March 13, 2027 | `mssql-jdbc-13.2.0.jre11.jar`<br />`mssql-jdbc-13.2.0.jre8.jar` |

JDBC driver versions 12.x and older receive five years of mainstream support from the minor version release date:

| Driver name | Driver package version | Applicable JARs | End of mainstream support |
| --- | --- | --- | --- |
| Microsoft JDBC Driver 12.10 for SQL Server | 12.10 | `mssql-jdbc-12.10.0.jre11.jar`<br />`mssql-jdbc-12.10.0.jre8.jar` | January 31, 2030 |
| Microsoft JDBC Driver 12.8 for SQL Server | 12.8 | `mssql-jdbc-12.8.0.jre11.jar`<br />`mssql-jdbc-12.8.0.jre8.jar` | July 31, 2029 |
| Microsoft JDBC Driver 12.6 for SQL Server | 12.6 | `mssql-jdbc-12.6.0.jre11.jar`<br />`mssql-jdbc-12.6.0.jre8.jar` | January 31, 2029 |
| Microsoft JDBC Driver 12.4 for SQL Server | 12.4 | `mssql-jdbc-12.4.0.jre11.jar`<br />`mssql-jdbc-12.4.0.jre8.jar` | July 31, 2028 |
| Microsoft JDBC Driver 12.2 for SQL Server | 12.2 | `mssql-jdbc-12.2.0.jre11.jar`<br />`mssql-jdbc-12.2.0.jre8.jar` | January 31, 2028 |
| Microsoft JDBC Driver 11.2 for SQL Server | 11.2 | `mssql-jdbc-11.2.0.jre18.jar`<br />`mssql-jdbc-11.2.0.jre17.jar`<br />`mssql-jdbc-11.2.0.jre11.jar`<br />`mssql-jdbc-11.2.0.jre8.jar` | August 4, 2027 |
| Microsoft JDBC Driver 10.2 for SQL Server | 10.2 | `mssql-jdbc-10.2.0.jre17.jar`<br />`mssql-jdbc-10.2.0.jre11.jar`<br />`mssql-jdbc-10.2.0.jre8.jar` | January 31, 2027 |

The following Microsoft JDBC Drivers are no longer supported:

| Driver name | Driver package version | End of mainstream support |
| --- | --- | --- |
| Microsoft JDBC Driver 9.4 for SQL Server | 9.4 | July 30, 2026 |
| Microsoft JDBC Driver 9.2 for SQL Server | 9.2 | January 29, 2026 |
| Microsoft JDBC Driver 8.4 for SQL Server | 8.4 | July 31, 2025 |
| Microsoft JDBC Driver 8.2 for SQL Server | 8.2 | January 31, 2025 |
| Microsoft JDBC Driver 7.4 for SQL Server | 7.4 | July 31, 2024 |
| Microsoft JDBC Driver 7.2 for SQL Server | 7.2 | January 31, 2024 |
| Microsoft JDBC Driver 7.0 for SQL Server | 7.0 | July 31, 2023 |
| Microsoft JDBC Driver 6.4 for SQL Server | 6.4 | February 27, 2023 |
| Microsoft JDBC Driver 6.2 for SQL Server | 6.2 | June 30, 2022 |
| Microsoft JDBC Driver 6.0 for SQL Server | 6.0 | July 14, 2021 |
| Microsoft JDBC Driver 4.2 for SQL Server | 4.2 | August 24, 2020 |
| Microsoft JDBC Driver 4.1 for SQL Server | 4.1 | December 12, 2019 |
| Microsoft JDBC Driver 4.0 for SQL Server | 4.0 | March 6, 2017 |
| Microsoft SQL Server JDBC Driver 3.0 | 3.0 | April 23, 2015 |
| Microsoft SQL Server JDBC Driver 2.0 | 2.0 | December 31, 2012 |
| Microsoft SQL Server 2005 JDBC Driver 1.2 | 1.2 | June 25, 2011 |
| Microsoft SQL Server 2005 JDBC Driver 1.1 | 1.1 | June 25, 2011 |
| Microsoft SQL Server 2005 JDBC Driver 1.0 | 1.0 | June 25, 2011 |
| Microsoft SQL Server 2000 JDBC Driver | 2000 | July 9, 2010 |

### Support policy for dependency vulnerabilities

Microsoft JDBC Driver for SQL Server defines external dependencies in its Maven package definition. Maven tooling resolves those dependencies at application build time. The package definition is updated with secure versions of direct dependencies in every minor release. Dependencies with known vulnerabilities are updated in hot fixes of supported versions only when it's possible to update the dependency without causing transitive dependency compatibility breaks. If it's not possible to update a vulnerable dependency in this manner, it's up to applications to update the dependency and ensure they don't have dependency conflicts.

## SQL version compatibility

All currently supported JDBC driver versions, as shown in the previous matrix, support all supported versions of Microsoft SQL including:

- Microsoft SQL Server
- Azure SQL Database
- Azure Synapse Analytics
- Azure SQL Managed Instance
- SQL database in Microsoft Fabric
- Microsoft Fabric Data Warehouse

Feature support is separate from compatibility with server versions. For details on feature support, see the [Driver Feature Support Matrix](../driver-feature-matrix.md#table2) or the release notes for each driver version.

For the best experience, use the latest JDBC driver.

## Java and JDBC specification support

| JDBC driver version | JRE versions | JDBC API version |
| --- | --- | --- |
| [13.4](release-notes-for-the-jdbc-driver.md#134) | 1.8, 11, 17, 21, 25 | 4.2, 4.3 (partially) |
| [13.2](release-notes-for-the-jdbc-driver.md#132) | 1.8, 11, 17, 21, 24 | 4.2, 4.3 (partially) |
| [12.10](release-notes-for-the-jdbc-driver.md#1210) | 1.8, 11, 17, 21, 23 | 4.2, 4.3 (partially) |
| [12.8](release-notes-for-the-jdbc-driver.md#128) | 1.8, 11, 17, 21, 22 | 4.2, 4.3 (partially) |
| [12.6](release-notes-for-the-jdbc-driver.md#126) | 1.8, 11, 17, 21 | 4.2, 4.3 (partially) |
| [12.4](release-notes-for-the-jdbc-driver.md#124) | 1.8, 11, 17, 20 | 4.2, 4.3 (partially) |
| [12.2](release-notes-for-the-jdbc-driver.md#122) | 1.8, 11, 17, 19 | 4.2, 4.3 (partially) |
| [11.2](release-notes-for-the-jdbc-driver.md#112) | 1.8, 11, 17, 18 | 4.2, 4.3 (partially) |
| [10.2](release-notes-for-the-jdbc-driver.md#102) | 1.8, 11, 17 | 4.2, 4.3 (partially) |
| [9.4](release-notes-for-the-jdbc-driver.md#94) | 1.8, 11, 16 | 4.2, 4.3 (partially) |
| [9.2](release-notes-for-the-jdbc-driver.md#92) | 1.8, 11, 15 | 4.2, 4.3 (partially) |
| [8.4](release-notes-for-the-jdbc-driver.md#84) | 1.8, 11, 14 | 4.2, 4.3 (partially) |
| [8.2](release-notes-for-the-jdbc-driver.md#82) | 1.8, 11, 13 | 4.2, 4.3 (partially) |
| [7.4](release-notes-for-the-jdbc-driver.md#74) | 1.8, 11, 12 | 4.2, 4.3 (partially) |
| [7.2](release-notes-for-the-jdbc-driver.md#72) | 1.8, 11 | 4.2, 4.3 (partially) |
| [7.0](release-notes-for-the-jdbc-driver.md#70) | 1.8, 10 | 4.2, 4.3 (partially) |
| [6.4](release-notes-for-the-jdbc-driver.md#64) | 1.7, 1.8, 9 | 4.1, 4.2, 4.3 (partially) |
| [6.2](release-notes-for-the-jdbc-driver.md#62) | 1.7, 1.8 | 4.1, 4.2 |
| [6.1](release-notes-for-the-jdbc-driver.md#61) | 1.7, 1.8 | 4.1, 4.2 |
| [6.0](release-notes-for-the-jdbc-driver.md#60) | 1.7, 1.8 | 4.1, 4.2 |
| 4.2 | 1.7, 1.8 | 4.1, 4.2 |
| 4.1 | 1.7 | 4.0 |
| 4.0 | 1.5, 1.6, 1.7 | 3.0, 4.0 |
| 3.0 | 1.5, 1.6, | 3.0, 4.0 |
| 2.0 | 1.5, 1.6 | 3.0, 4.0 |
| 1.2 | 1.4, 1.5, 1.6 | 3.0 |
| 1.1 | 1.4 | 3.0 |
| 1.0 | 1.4 | 3.0 |
| 2000 | 1.4 | 3.0 |

### Java 4.3 partial compatibility

The JRE 11+ jars are built against the JDBC 4.3 interface surface, but not every method added in JDBC 4.3 is implemented. The following table summarizes runtime behavior on current drivers (7.0 and later).

| JDBC 4.3 addition | Behavior |
| --- | --- |
| `Connection.beginRequest()`<br>`Connection.endRequest()` | Supported. Used by connection pools to mark request boundaries. |
| `Statement.enquoteLiteral`<br>`enquoteIdentifier`<br>`isSimpleIdentifier`<br>`enquoteNCharLiteral` | Supported through the JDK default implementations on `java.sql.Statement`. |
| `Connection.setShardingKey`<br>`Connection.setShardingKeyIfValid`<br>`DataSource.createConnectionBuilder`<br>`XADataSource.createXAConnectionBuilder`<br>`ConnectionPoolDataSource.createPooledConnectionBuilder`<br>`DataSource.createShardingKeyBuilder` | Throws `SQLFeatureNotSupportedException`. |

Applications that rely on JDBC 4.3 sharding APIs need an alternative driver or a custom implementation; the Microsoft JDBC Driver for SQL Server doesn't support sharding.

## Supported operating systems

The Microsoft JDBC driver is designed to work on any operating system that supports the use of a Java Virtual Machine (JVM). Some commonly used platforms include Windows, Windows Server, Linux, Unix, AIX, macOS, and others.

The JDBC product team tests our driver on Windows, Ubuntu Linux, and macOS.

## Application server support

The Microsoft JDBC Driver for SQL Server is tested with various application servers. Consult your application server vendor for more details on which driver version is compatible with their product.
