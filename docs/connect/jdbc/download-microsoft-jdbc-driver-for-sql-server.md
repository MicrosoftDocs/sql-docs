---
title: Download Microsoft JDBC Driver for SQL Server
description: Download the Microsoft JDBC Driver for SQL Server to develop Java applications that connect to SQL Server.
ms.date: 09/30/2019
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 451181b8-11e6-4d01-b547-9ac5aada8238
author: MightyPen
ms.author: genemi
---
# Download Microsoft JDBC Driver for SQL Server

This article provides download links to the Microsoft JDBC Driver for SQL Server. This driver enables you to develop Java applications that connect to SQL Server.  

## Available downloads of JDBC Driver for SQL Server

Use the links in the following table to download the latest Microsoft JDBC Driver for SQL Server that matches your Java Runtime Environment (JRE):

| Version | Release date | Java versions |
|---|---|---|
| [Microsoft JDBC Driver 7.4](https://go.microsoft.com/fwlink/?linkid=2099962) | 8/1/2019 | JRE 8, 11, 12 |
| [Microsoft JDBC Driver 7.2](https://go.microsoft.com/fwlink/?linkid=2063159) | 4/17/2019 | JRE 8, 11 |
| [Microsoft JDBC Driver 7.0](https://go.microsoft.com/fwlink/?linkid=2005972) | 7/31/2018 | JRE 8, 10 |
| [Microsoft JDBC Driver 6.4](https://go.microsoft.com/fwlink/?linkid=868290)  | 3/26/2018 | JRE 7, 8, 9 |
| [Microsoft JDBC Driver 6.2](https://go.microsoft.com/fwlink/?linkid=852460) | 2/12/2018 | JRE 7, 8 |
| [Microsoft JDBC Driver 6.0](https://go.microsoft.com/fwlink/?LinkId=245496) | 2/27/2018 | JRE 7, 8 |
| [Microsoft JDBC Driver 4.2](https://go.microsoft.com/fwlink/?linkid=841534) | 2/26/2018 | JRE 7, 8 |
| [Microsoft JDBC Driver 4.1](https://go.microsoft.com/fwlink/?linkid=841533) | 2/27/2018 | JRE 7 |

When you download the driver, there are multiple JAR files. The name of the JAR file indicates the version of Java that it supports. For more information about each release, see the [Release notes](release-notes-for-the-jdbc-driver.md) and [System requirements](system-requirements-for-the-jdbc-driver.md).

## Using the JDBC driver with Maven Central

The JDBC driver can be added to a Maven project by adding it as a dependency in the POM.xml file with the following code:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.4.1.jre11</version>
</dependency>
```  

## Unsupported drivers

Unsupported driver versions are not available for download here. We are continually improving the Java connectivity support. As such we highly recommend that you work with the latest version of Microsoft JDBC driver.  
  
## Next steps

For more information about the Microsoft JDBC Driver for SQL Server, see [Overview of the JDBC driver](overview-of-the-jdbc-driver.md) and the [JDBC driver GitHub repository](https://github.com/microsoft/mssql-jdbc/blob/dev/README.md).
