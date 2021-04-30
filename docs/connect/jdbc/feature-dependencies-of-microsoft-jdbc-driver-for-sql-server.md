---
title: "Feature dependencies of the Microsoft JDBC Driver"
description: "Learn about the dependencies that the Microsoft JDBC Driver for SQL Server has and how to meet them."
ms.custom: ""
ms.date: "04/29/2021"
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

 - `com.microsoft.azure:azure-security-keyvault-keys` : Microsoft Azure Client Library For KeyVault Keys for Always Encrypted Azure Key Vault feature. (optional)
 - `com.microsoft.azure:azure-identity` : Microsoft Azure Client Library For Identity for Azure Active Directory Authentication features and Azure Key Vault feature. (optional)
 - `org.antlr:antlr4-runtime`: ANTLR 4 Runtime for useFmtOnly feature. (optional)
 - `org.osgi:org.osgi.core`: OSGi Core library for OSGi Framework support.
 - `org.osgi:org.osgi.compendium`: OSGi Compendium library for OSGi Framework support.
 - `com.google.code.gson`: JSON parser for Always Encrypted with secure enclaves feature. (optional)
 - `org.bouncycastle.bcprov-jdk15on`: Bouncy Castle Provider for Always Encrypted with secure enclaves feature when using JAVA 8 only. (optional)

## Run time

Projects that require any of the preceding features need to explicitly declare the respective dependencies in their POM file.

**For example:** If you're using the Azure Active Directory Authentication feature with JDBC driver version 9.2.1 and above, you need to declare the `azure-identity` dependency in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.2.1.jre11</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.1.3</version>
</dependency>
```

**For example:** If you're using the Azure Key Vault feature with JDBC driver version 9.2.1 and above, you need to declare the `azure-security-keyvault-keys` and the `azure-identity` dependencies in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.2.1.jre11</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.1.3</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-security-keyvault-keys</artifactId>
    <version>4.2.1</version>
</dependency>
```

> [!NOTE]
> Make sure to use the version of the POM file that's shipped with the version of the JDBC driver you're using. The dependencies and versions may have changed.

If you are using Maven to build or test your project, Maven will download all dependent libraries declared in the POM file and their transitiive libraries automatically. You can also use the
[Maven Dependency Plugin](https://maven.apache.org/plugins/maven-dependency-plugin/copy-dependencies-mojo.html) to download all project dependencies to a desired location. If you are not using Maven, you will have to download all dependencies and transitive dependencies manually and make sure you have all the correct version of each library. Once you have downloaded all the required depedendent libraries, add them to your project's classpath before running your application.



## Dependency requirements for the JDBC driver

### Working with the Azure Key Vault provider:

- JDBC driver version 9.2.1 - Dependency versions: Azure-keyvault(version 4.2.1), and Azure-identity(version 1.1.3), and their dependencies ([sample application](azure-key-vault-sample-version-9.2.md))
- JDBC driver version 8.4.1 - Dependency versions: Azure-Keyvault (version 1.2.4), Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (1.7.4), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
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

- JDBC driver version 9.2.1 - Dependency versions: Azure-identity(version 1.1.3), and their dependencies.
- JDBC Driver version 8.4.1 - Dependency versions: Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (1.7.4), and their dependencies.
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

In addition to the **mssql-jdbc_auth-\<version>-\<arch>.dll** (available in the JDBC driver package), the Azure Active Directory Authentication Library (**ADAL.DLL**) also needs to be installed for Active Directory Integrated authentication. ADAL can be installed from [Microsoft ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md) or [Microsoft OLE DB Driver for SQL Server](../oledb/download-oledb-driver-for-sql-server.md). The JDBC driver only supports version **1.0.2028.318 and higher** for ADAL.DLL.


You can get a [sample application](connecting-using-azure-active-directory-authentication.md) that uses this feature.

## See also

[JDBC Driver GitHub repository](https://github.com/microsoft/mssql-jdbc)  
[JDBC Driver API reference](reference/jdbc-driver-api-reference.md)
