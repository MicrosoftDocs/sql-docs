---
title: Feature dependencies
description: Learn about the dependencies that the Microsoft JDBC Driver for SQL Server has and how to meet them.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Feature dependencies of the Microsoft JDBC Driver for SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article lists libraries that the Microsoft JDBC Driver for SQL Server depends on. The project has the following dependencies:

## Compile time

- `com.azure:azure-security-keyvault-keys`: Microsoft Azure Client Library For KeyVault Keys for JDBC driver version 9.2 and above or `com.microsoft.azure:azure-keyvault`: Microsoft Azure SDK For Key Vault for JDBC driver version 8.4 and below for Always Encrypted Azure Key Vault feature. (optional)
- `com.azure:azure-identity`: Microsoft Azure Client Library For Identity for JDBC driver version 9.2 and above or `com.microsoft.azure:adal4j`: Microsoft Azure Active Directory Authentication Library for JDBC driver version 8.4 and below for Azure Active Directory Authentication features and Azure Key Vault feature. (optional)
- `com.microsoft.azure:msal4j`: Microsoft Authentication Library (MSAL) For Java. (optional)
- `org.antlr:antlr4-runtime`: ANTLR 4 Runtime for useFmtOnly feature. (optional)
- `org.osgi:org.osgi.core`: OSGi Core library for OSGi Framework support.
- `org.osgi:org.osgi.compendium`: OSGi Compendium library for OSGi Framework support.
- `com.google.code.gson`: JSON parser for Always Encrypted with secure enclaves feature. (optional)
- `org.bouncycastle.bcprov-jdk15on`: Bouncy Castle Provider for Always Encrypted with secure enclaves feature for JAVA 8 only. (optional)

## Run time

Projects that require any of the preceding features must explicitly declare the respective dependencies in their POM file that match the dependencies of the version of the driver used.

**For example:** If you're using the Azure Active Directory Authentication feature with JDBC driver version 10.2 and above, you must declare the `azure-identity` dependency in your project POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>10.2.0.jre11</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.4.3</version>
</dependency>
```

**For example:** If you're using the Azure Active Directory Authentication feature with JDBC driver version 8.4 and below, you must declare the `adal4j` and `client-runtimes` dependencies in your project POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.2.1.jre11</version>
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

**For example:** If you're using the Azure Key Vault feature with JDBC driver version 10.2 and above, you must declare the `azure-security-keyvault-keys` and `azure-identity` dependencies in your project POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>10.2.0.jre11</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.4.3</version>
</dependency>

<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-security-keyvault-keys</artifactId>
    <version>4.3.6</version>
</dependency>
```

**For example:** If you're using the Azure Key Vault feature with JDBC driver version 8.4 and below, you must declare the `azure-keyvault`, `adal4j`, and `client-runtime` dependencies in your project POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>9.2.1.jre11</version>
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

> [!NOTE]
> Make sure to use the version of the POM file that's shipped with the version of the JDBC driver you're using. The dependencies and versions may have changed.

If you're using Maven to build or test your project, Maven automatically downloads dependent libraries in the POM file along with their transitive libraries. You can also use the [Maven Dependency Plugin](https://maven.apache.org/plugins/maven-dependency-plugin/copy-dependencies-mojo.html) to download all project dependencies to a desired location. If you're not using Maven, you have to download dependencies and transitive dependencies manually to make sure you have all the correct versions of each library. Once you've downloaded the required dependent libraries, add them to your project classpath to run your application.

## Dependency requirements for the JDBC driver

### Work with the Azure Key Vault provider

- JDBC driver version 11.2.0 — Dependency versions: Azure-security-keyvault-keys (version 4.4.1), and Azure-identity(version 1.5.0), and their dependencies ([sample application](azure-key-vault-sample-version-9.2.md))
- JDBC driver version 10.2.0 — Dependency versions: Azure-security-keyvault-keys (version 4.3.6), and Azure-identity(version 1.4.3), and their dependencies ([sample application](azure-key-vault-sample-version-9.2.md))
- JDBC driver version 9.4.1 — Dependency versions: Azure-security-keyvault-keys (version 4.2.8), and Azure-identity(version 1.3.3), and their dependencies ([sample application](azure-key-vault-sample-version-9.2.md))
- JDBC driver version 9.2.1 — Dependency versions: Azure-security-keyvault-keys (version 4.2.1), and Azure-identity(version 1.1.3), and their dependencies ([sample application](azure-key-vault-sample-version-9.2.md))
- JDBC driver version 8.4.1 — Dependency versions: Azure-Keyvault (version 1.2.4), Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (version 1.7.4), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 8.2.2 — Dependency versions: Azure-Keyvault (version 1.2.2), Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (version 1.7.0), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.4.1 — Dependency versions: Azure-Keyvault (version 1.2.1), Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (version 1.6.10), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.2.2 — Dependency versions: Azure-Keyvault (version 1.2.0), Azure-Keyvault-Webkey (version 1.2.0), Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 7.0.0 — Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.6.0), and their dependencies ([sample application](azure-key-vault-sample-version-7.0.md))
- JDBC driver version 6.4.0 — Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](azure-key-vault-sample-version-6.2.2.md))
- JDBC driver version 6.2.2 — Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](azure-key-vault-sample-version-6.2.2.md))
- JDBC driver version 6.0.0 — Dependency versions: Azure-Keyvault (version 0.9.7), Adal4j (version 1.3.0), and their dependencies ( [sample application](azure-key-vault-sample-version-6.0.0.md))

> [!NOTE]
> With 6.2.2 and 6.4.0 driver versions, the azure-keyvault-java dependency had been updated to version 1.0.0. However, the new version was not compatible with the previous version (0.9.7) and breaks the existing implementation in the driver. The new implementation in the driver required API changes, which in turn breaks client programs that use the Azure Key Vault Provider.
>
> This problem is resolved with latest driver version(s) (7.0.0 onwards). The removed constructor that used the authentication callback mechanism is added back to the Azure Key Vault Provider for backward compatibility.

### Work with Azure Active Directory authentication

- JDBC driver version 11.2.0 — Dependency versions: Azure-identity(version 1.5.0), and their dependencies.
- JDBC driver version 10.2.0 — Dependency versions: Azure-identity(version 1.4.3), and their dependencies.
- JDBC driver version 9.4.1 — Dependency versions: Azure-identity(version 1.3.3), and their dependencies.
- JDBC driver version 9.2.1 — Dependency versions: Azure-identity(version 1.1.3), and their dependencies.
- JDBC Driver version 8.4.1 — Dependency versions: Adal4j (version 1.6.5), Client-Runtime-for-AutoRest (1.7.4), and their dependencies.
- JDBC Driver version 8.2.2 — Dependency versions: Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.7.0), and their dependencies. In this version of the driver, 'sqljdbc_auth.dll' has been renamed to 'mssql-jdbc_auth-\<version>-\<arch>.dll'.
- JDBC Driver version 7.4.1 — Dependency versions: Adal4j (version 1.6.4), Client-Runtime-for-AutoRest (1.6.10), and their dependencies
- JDBC Driver version 7.2.2 — Dependency versions: Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies
- JDBC Driver version 7.0.0 — Dependency versions: Adal4j (version 1.6.0) and its dependencies
- JDBC Driver version 6.4.0 — Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.2.2 — Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.0.0 — Dependency versions: Adal4j (version 1.3.0), and its dependencies. In this version of the driver, you can connect by using _ActiveDirectoryIntegrated_ Authentication Mode only on a Windows operating system and by using sqljdbc_auth.dll and Active Directory Authentication Library for SQL Server (ADALSQL.DLL).

From driver version 6.4.0 onward, applications don't necessarily require using ADALSQL.DLL on Windows operating systems. For *non-Windows operating systems*, the driver requires a Kerberos ticket to work with ActiveDirectoryIntegrated Authentication. For more information about how to connect to Active Directory by using Kerberos, see [Set Kerberos ticket on Windows, Linux, and macOS](connecting-using-azure-active-directory-authentication.md#set-kerberos-ticket-on-windows-linux-and-macos).

For *Windows operating systems*, the driver looks for sqljdbc_auth.dll by default and doesn't require Kerberos ticket setup or Azure library dependencies. If sqljdbc_auth.dll isn't available, the driver looks for the Kerberos ticket to authenticate to Active Directory as on other operating systems.

From driver version 8.2.2 onward, 'sqljdbc_auth.dll' is renamed to 'mssql-jdbc_auth-\<version>-\<arch>.dll'. For example, 'mssql-jdbc_auth-8.2.2.x64.dll'.

In addition to the **mssql-jdbc_auth-\<version>-\<arch>.dll** (available in the JDBC driver package), the Azure Active Directory Authentication Library (**ADAL.DLL**) also must be installed for Active Directory Integrated authentication. Microsoft Azure Active Directory Authentication Library can be installed from [Microsoft ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md) or [Microsoft OLE DB Driver for SQL Server](../oledb/download-oledb-driver-for-sql-server.md). The JDBC driver only supports version **1.0.2028.318 and higher** for ADAL.DLL.

You can get a [sample application](connecting-using-azure-active-directory-authentication.md) that uses this feature.

## See also

[JDBC Driver GitHub repository](https://github.com/microsoft/mssql-jdbc)  
[JDBC Driver API reference](reference/jdbc-driver-api-reference.md)
