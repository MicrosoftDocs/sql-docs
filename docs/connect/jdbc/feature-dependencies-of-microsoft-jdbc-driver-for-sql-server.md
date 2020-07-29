---
title: "Feature dependencies of the Microsoft JDBC Driver"
description: "Learn about the dependencies that the Microsoft JDBC Driver for SQL Server has and how to meet them."
ms.custom: ""
ms.date: "07/31/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 939a8773-2583-49a4-bf00-6b892fbe39dc
author: David-Engel
ms.author: v-daenge
---
# Feature dependencies of the Microsoft JDBC Driver for SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article lists libraries that the Microsoft JDBC Driver for SQL Server depends on. The project has the following dependencies.

## Compile time

 - `com.microsoft.azure:azure-keyvault` : Azure Key Vault Provider for Always Encrypted Azure Key Vault feature. (optional)
 - `com.microsoft.azure:adal4j` : Azure Active Directory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature. (optional)
 - `com.microsoft.rest:client-runtime` : Azure Active Directory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature. (optional)
 - `org.antlr:antlr4-runtime`: ANTLR 4 Runtime for useFmtOnly feature. (optional)
 - `org.osgi:org.osgi.core`: OSGi Core library for OSGi Framework support.
 - `org.osgi:org.osgi.compendium`: OSGi Compendium library for OSGi Framework support.
 - `com.google.code.gson`: JSON parser for Always Encrypted with secure enclaves feature. (optional)
 - `org.bouncycastle.bcprov-jdk15on`: Bouncy Castle Provider for Always Encrypted with secure enclaves feature when using JAVA 8 only. (optional)

## Test time

Specific projects that require either of the preceding features need to explicitly declare the respective dependencies in their POM file.

**For example:** When you're using the Azure Active Directory Authentication feature, you need to redeclare the `adal4j` dependency in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>8.4.0.jre14</version>
    <scope>compile</scope>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>adal4j</artifactId>
    <version>1.6.5</version>
</dependency>

<dependency>
    <groupId>com.microsoft.rest</groupId>
    <artifactId>client-runtime</artifactId>
    <version>1.7.4</version>
</dependency>
```

**For example:** When you're using the Azure Key Vault feature, you need to redeclare the `azure-keyvault` dependency and the `adal4j` dependency in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>8.4.0.jre14</version>
    <scope>compile</scope>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>adal4j</artifactId>
    <version>1.6.5</version>
</dependency>

<dependency>
    <groupId>com.microsoft.rest</groupId>
    <artifactId>client-runtime</artifactId>
    <version>1.7.4</version>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>azure-keyvault</artifactId>
    <version>1.2.4</version>
</dependency>
```

## Dependency requirements for the JDBC driver

### Working with the Azure Key Vault provider:

- JDBC driver version 8.4.0 - Dependency versions: Azure-Keyvault (version 1.2.4), Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (1.7.4), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 8.2.2 - Dependency versions: Azure-Keyvault (version 1.2.2), Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.7.0), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.4.1 - Dependency versions: Azure-Keyvault (version 1.2.1), Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.6.10), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.2.2 - Dependency versions: Azure-Keyvault (version 1.2.0), Azure-Keyvault-Webkey (version 1.2.0), Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.0.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.6.0), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 6.4.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](azure-key-vault-sample-version-6.2.2.md))
- JDBC driver version 6.2.2 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](azure-key-vault-sample-version-6.2.2.md))
- JDBC driver version 6.0.0 - Dependency versions: Azure-Keyvault (version 0.9.7), Adal4j (version 1.3.0), and their dependencies ( [sample application](azure-key-vault-sample-version-6.0.0.md))

> [!NOTE]
> With 6.2.2 and 6.4.0 driver versions, the azure-keyvault-java dependency had been updated to version 1.0.0. However, the new version was not compatible with the previous version (0.9.7) and breaks the existing implementation in the driver. The new implementation in the driver required API changes, which in turn breaks client programs that use the Azure Key Vault Provider.
>
> This problem is resolved with latest driver version(s) (7.0.0 onwards). The removed constructor that used the authentication callback mechanism is added back to the Azure Key Vault Provider for backward compatibility.

### Working with Azure Active Directory authentication:

- JDBC Driver version 8.4.0 - Dependency versions: Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (1.7.4), and their dependencies.
- JDBC Driver version 8.2.2 - Dependency versions: Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.7.0), and their dependencies. In this version of the driver, 'sqljdbc_auth.dll' has been renamed to 'mssql-jdbc_auth-\<version>-\<arch>.dll'.
- JDBC Driver version 7.4.1 - Dependency versions: Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.6.10), and their dependencies
- JDBC Driver version 7.2.2 - Dependency versions: Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies
- JDBC Driver version 7.0.0 - Dependency versions: Adal4j (version 1.6.0) and its dependencies
- JDBC Driver version 6.4.0 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.2.2 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.0.0 - Dependency versions: Adal4j (version 1.3.0), and its dependencies. In this version of the driver, you can connect by using _ActiveDirectoryIntegrated_ Authentication Mode only on a Windows operating system and by using sqljdbc_auth.dll and Active Directory Authentication Library for SQL Server (ADALSQL.DLL).

From driver version 6.4.0 onward, applications don't necessarily require using ADALSQL.DLL on Windows operating systems. For *non-Windows operating systems*, the driver requires a Kerberos ticket to work with ActiveDirectoryIntegrated Authentication. For more information about how to connect to Active Directory by using Kerberos, see [Set Kerberos ticket on Windows, Linux, and macOS](connecting-using-azure-active-directory-authentication.md#set-kerberos-ticket-on-windows-linux-and-macos).

For *Windows operating systems*, the driver looks for sqljdbc_auth.dll by default and doesn't require Kerberos ticket setup or Azure library dependencies. If sqljdbc_auth.dll isn't available, the driver looks for the Kerberos ticket for authenticating to Active Directory as on other operating systems.

From driver version 8.2.2 onward, 'sqljdbc_auth.dll' is renamed to 'mssql-jdbc_auth-\<version>-\<arch>.dll'. E.g. 'mssql-jdbc_auth-8.2.2.x64.dll'.

You can get a [sample application](connecting-using-azure-active-directory-authentication.md) that uses this feature.

## See also

[JDBC Driver GitHub repository](https://github.com/microsoft/mssql-jdbc)  
[JDBC Driver API reference](reference/jdbc-driver-api-reference.md)
