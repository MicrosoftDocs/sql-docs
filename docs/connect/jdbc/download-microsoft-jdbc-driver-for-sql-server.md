---
title: Download
description: Download the Microsoft JDBC Driver for SQL Server to develop Java applications that connect to SQL Server and Azure SQL Database.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/30/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Download Microsoft JDBC Driver for SQL Server

The Microsoft JDBC Driver for SQL Server is a Type 4 JDBC driver that provides database connectivity through the standard JDBC application program interfaces (APIs) available on the Java platform. The driver downloads are available to all users at no extra charge. They provide access to SQL Server from any Java application, application server, or Java-enabled applet.

## Download

Version 11.2 is the latest general availability (GA) version. It supports Java 8, 11, 17, and 18. If you need to use an older Java runtime, see the [Java and JDBC specification support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support) to see if there's a supported driver version you can use. We're continually improving Java connectivity support. As such we highly recommend that you work with the latest version of Microsoft JDBC driver.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft JDBC Driver 11.2 for SQL Server (zip)](https://go.microsoft.com/fwlink/?linkid=2217800)**  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft JDBC Driver 11.2 for SQL Server (tar.gz)](https://go.microsoft.com/fwlink/?linkid=2217551)**

### Version information

- Release number: 11.2.1
- Released: September 8, 2022

When you download the driver, there are multiple JAR files. The name of the JAR file indicates the version of Java that it supports.

> [!Note]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft JDBC Driver for SQL Server is available in the following languages:

Microsoft JDBC Driver 11.2.1 for SQL Server (zip):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2217800&clcid=0x40a)

Microsoft JDBC Driver 11.2.1 for SQL Server (tar.gz):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2217551&clcid=0x40a)

### Release notes

For details about this release, see [the release notes](release-notes-for-the-jdbc-driver.md) and [system requirements](system-requirements-for-the-jdbc-driver.md).

### Previous releases

To download previous releases, see [previous Microsoft JDBC Driver for SQL Server releases](release-notes-for-the-jdbc-driver.md#previous-releases).

## Using the JDBC driver with Maven Central

The JDBC driver can be added to a Maven project by adding it as a dependency in the POM.xml file with the following code:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>11.2.1.jre17</version>
</dependency>
```

## Unsupported drivers

Unsupported driver versions aren't available for download here. We're continually improving the Java connectivity support. As such we highly recommend that you work with the latest version of Microsoft JDBC driver.

## Next steps

For more information about the Microsoft JDBC Driver for SQL Server, see [Overview of the JDBC driver](overview-of-the-jdbc-driver.md) and the [JDBC driver GitHub repository](https://github.com/microsoft/mssql-jdbc/blob/dev/README.md).
