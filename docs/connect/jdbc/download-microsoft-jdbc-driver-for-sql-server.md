---
title: Download Microsoft JDBC Driver for SQL Server
description: "Download the Microsoft JDBC Driver for SQL Server to develop Java applications that connect to SQL Server and Azure SQL Database."
ms.date: 08/24/2020
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 451181b8-11e6-4d01-b547-9ac5aada8238
author: David-Engel
ms.author: v-daenge
---
# Download Microsoft JDBC Driver for SQL Server

The Microsoft JDBC Driver for SQL Server is a Type 4 JDBC driver that provides database connectivity through the standard JDBC application program interfaces (APIs) available on the Java platform. The driver downloads are available to all users at no additional charge. They provide access to SQL Server from any Java application, application server, or Java-enabled applet.

## Download

Version 8.4 is the latest general availability (GA) version. It supports Java 8, 11, and 14. If you need to run on a Java runtime older than that, see the [Java and JDBC specification support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support) to see if there is a supported driver version you can use. We are continually improving Java connectivity support. As such we highly recommend that you work with the latest version of Microsoft JDBC driver.

**[![Download](../../ssms/media/download-icon.png) Download Microsoft JDBC Driver 8.4 for SQL Server (zip)](https://go.microsoft.com/fwlink/?linkid=2137600)**  
**[![Download](../../ssms/media/download-icon.png) Download Microsoft JDBC Driver 8.4 for SQL Server (tar.gz)](https://go.microsoft.com/fwlink/?linkid=2137502)**  

### Version information

- Release number: 8.4.1
- Released: August 27, 2020

When you download the driver, there are multiple JAR files. The name of the JAR file indicates the version of Java that it supports.

> [!Note]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please visit the [US-English version of the site](https://aka.ms/downloadmssqljdbcenglish). You can download different languages from the US-English version site by selecting [available languages](#available-languages).

## Available languages

This release of Microsoft JDBC Driver for SQL Server is available in the following languages:

Microsoft JDBC Driver 8.4.1 for SQL Server (zip):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2137600&clcid=0x40a)

Microsoft JDBC Driver 8.4.1 for SQL Server (tar.gz):
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2137502&clcid=0x40a)

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
    <version>8.4.1.jre11</version>
</dependency>
```  

## Unsupported drivers

Unsupported driver versions are not available for download here. We are continually improving the Java connectivity support. As such we highly recommend that you work with the latest version of Microsoft JDBC driver.  
  
## Next steps

For more information about the Microsoft JDBC Driver for SQL Server, see [Overview of the JDBC driver](overview-of-the-jdbc-driver.md) and the [JDBC driver GitHub repository](https://github.com/microsoft/mssql-jdbc/blob/dev/README.md).
