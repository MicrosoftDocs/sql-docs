---
title: System Requirements for Microsoft JDBC Driver
description: Choose the right Java runtime, JDBC driver artifact, operating system, and SQL target for Microsoft JDBC Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 11/06/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: limits-and-quotas
ai-usage: ai-assisted
---
# System requirements for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To use Microsoft JDBC Driver for SQL Server, install a supported Java runtime and add the driver artifact that matches that runtime. The driver is a Type 4 JDBC driver. Base SQL authentication and standard JDBC connectivity don't require native client libraries. Some authentication and security features need extra Java libraries or native components. For those requirements, see [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md).

<a id="java-runtime-environment-requirements"></a>

## Java runtime requirements

Microsoft JDBC Driver 13.6.2 publishes two current artifacts:

| Artifact | Use with | JDBC API support | Notes |
| --- | --- | --- | --- |
| `mssql-jdbc-13.6.2.jre8.jar` or Maven version `13.6.2.jre8` | Java 8 | JDBC 4.2. | Use only for applications that must run on Java 8. |
| `mssql-jdbc-13.6.2.jre11.jar` or Maven version `13.6.2.jre11` | Java 11 and later supported runtimes | JDBC 4.3 partial compatibility. | Use this artifact for Java 11, 17, 21, 25, and 26. |

For driver 13.6, the documented supported Java runtime versions are Java 8, 11, 17, 21, 25, and 26. The driver team tests driver 13.6 on OpenJDK 8, 11, 17, 21, 25, and 26, and on Azul Zulu JRE 8, 11, and 17.

Java support and JDBC API coverage vary by driver version. For the full per-version matrix, see [Java and JDBC specification support](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support).

<a id="sql-server-requirements"></a>

## Microsoft SQL requirements

Current supported JDBC driver versions support connections to the following Microsoft SQL products:

- SQL Server.
- Azure SQL Database.
- Azure SQL Managed Instance.
- Azure Synapse Analytics, for supported SQL endpoints.
- SQL database in Microsoft Fabric.
- Warehouse in Microsoft Fabric.

Connectivity doesn't mean every SQL product supports every driver feature. For feature availability by driver and SQL target, see [Driver Feature Support Matrix](../driver-feature-matrix.md#table2) and the release notes for the driver version that you use.

For current support policy, see [Microsoft JDBC Driver support lifecycle matrix and policy](microsoft-jdbc-driver-for-sql-server-support-matrix.md#microsoft-jdbc-driver-support-lifecycle-matrix-and-policy).

## Operating system requirements

The driver is designed to work on any operating system that supports a compatible Java Virtual Machine (JVM). Common platforms include Windows, Windows Server, Linux, Unix, AIX, and macOS.

The JDBC product team tests current drivers on Windows, Ubuntu Linux, and macOS. Native authentication components are platform-specific. For feature-specific native requirements, see [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md).

## Supported languages

The driver supports all SQL Server column collations. For more information about language, collation, and globalization behavior, see [International features of the JDBC driver](international-features-of-the-jdbc-driver.md) and [National character set support](national-character-set-support.md).

## Related content

- [Install the JDBC driver](using-the-jdbc-driver.md)
- [Download Microsoft JDBC Driver for SQL Server](download-microsoft-jdbc-driver-for-sql-server.md)
- [Microsoft JDBC Driver for SQL Server support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md)
- [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md)
