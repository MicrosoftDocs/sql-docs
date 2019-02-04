---
title: "Feature dependencies of the Microsoft JDBC Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 939a8773-2583-49a4-bf00-6b892fbe39dc
author: MightyPen
ms.author: genemi
manager: craigg
---

# Feature dependencies of the Microsoft JDBC Driver for SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article lists libraries that the Microsoft JDBC Driver for SQL Server depends on. The project has the following dependencies.

## Compile time

 - `com.microsoft.azure:azure-keyvault` : Azure Key Vault Provider for Always Encrypted Azure Key Vault feature (optional)
 - `com.microsoft.azure:azure-keyvault-webkey` : Azure Key Vault Provider for Always Encrypted Azure Key Vault feature (optional)
 - `com.microsoft.azure:adal4j` : Azure Active Directory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature (optional)
 - `com.microsoft.rest:client-runtime` : Azure Active Directory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature (optional)
- `org.osgi:org.osgi.core`: OSGi Core library for OSGi Framework support.
- `org.osgi:org.osgi.compendium`: OSGi Compendium library for OSGi Framework support.

## Test time

Specific projects that require either of the preceding features need to explicitly declare the respective dependencies in their POM file.

**For example:** When you're using the Azure Active Directory Authentication feature, you need to redeclare the `adal4j` dependency in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.2.0.jre11</version>
    <scope>compile</scope>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>adal4j</artifactId>
    <version>1.6.3</version>
</dependency>

<dependency>
    <groupId>com.microsoft.rest</groupId>
    <artifactId>client-runtime</artifactId>
    <version>1.6.5</version>
</dependency>
```

**For example:** When you're using the Azure Key Vault feature, you need to redeclare the `azure-keyvault` dependency and the `adal4j` dependency in your project's POM file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.2.0.jre11</version>
    <scope>compile</scope>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>adal4j</artifactId>
    <version>1.6.3</version>
</dependency>

<dependency>
    <groupId>com.microsoft.rest</groupId>
    <artifactId>client-runtime</artifactId>
    <version>1.6.5</version>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>azure-keyvault</artifactId>
    <version>1.2.0</version>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>azure-keyvault-webkey</artifactId>
    <version>1.2.0</version>
</dependency>
```

## Dependency requirements for the JDBC Driver

### Working with the Azure Key Vault Provider:

- JDBC Driver version 7.2.0 - Dependency versions: Azure-Keyvault (version 1.2.0), Azure-Keyvault-Webkey (version 1.2.0), Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies ([sample application](../../connect/jdbc/azure-key-vault-sample.md))
- JDBC Driver version 7.0.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.6.0), and their dependencies ([sample application](../../connect/jdbc/azure-key-vault-sample.md))
- JDBC Driver version 6.4.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](../../connect/jdbc/azure-key-vault-sample-version-6.2.2.md))
- JDBC Driver version 6.2.2 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([sample application](../../connect/jdbc/azure-key-vault-sample-version-6.2.2.md))
- JDBC Driver version 6.0.0 - Dependency versions: Azure-Keyvault (version 0.9.7), Adal4j (version 1.3.0), and their dependencies ( [sample application](../../connect/jdbc/azure-key-vault-sample-version-6.0.0.md))

> [!NOTE]
> With 6.2.2 and 6.4.0 driver versions, the azure-keyvault-java dependency had been updated to version 1.0.0. However, the new version was not compatible with the previous version (0.9.7) and breaks the existing implementation in the driver. The new implementation in the driver required API changes, which in turn breaks client programs that use the Azure Key Vault Provider.
>
> This problem is resolved with latest driver version (7.0.0). The removed constructor that used the authentication callback mechanism is added back to the Azure Key Vault Provider for backward compatibility.

### Working with Azure Active Directory Authentication:

- JDBC Driver version 7.2.0 - Dependency versions: Adal4j (version 1.6.3), Client-Runtime-for-AutoRest (1.6.5), and their dependencies
- JDBC Driver version 7.0.0 - Dependency versions: Adal4j (version 1.6.0) and its dependencies
- JDBC Driver version 6.4.0 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.2.2 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.0.0 - Dependency versions: Adal4j (version 1.3.0), and its dependencies. In this version of the driver, you can connect by using _ActiveDirectoryIntegrated_ Authentication Mode only on a Windows operating system and by using sqljdbc_auth.dll and Active Directory Authentication Library for SQL Server (ADALSQL.DLL).

From driver version 6.4.0 onward, applications don't necessarily require using ADALSQL.DLL on Windows operating systems. For *non-Windows operating systems*, the driver requires a Kerberos ticket to work with ActiveDirectoryIntegrated Authentication. For more information about how to connect to Active Directory by using Kerberos, see [Set Kerberos ticket on Windows, Linux, and Mac](https://docs.microsoft.com/sql/connect/jdbc/connecting-using-azure-active-directory-authentication#set-kerberos-ticket-on-windows-linux-and-mac).

For *Windows operating systems*, the driver looks for sqljdbc_auth.dll by default and doesn't require Kerberos ticket setup or Azure library dependencies. If sqljdbc_auth.dll isn't available, the driver looks for the Kerberos ticket for authenticating to Active Directory as on other operating systems.

You can get a [sample application](../../connect/jdbc/connecting-using-azure-active-directory-authentication.md) that uses this feature.

## See also

[JDBC Driver GitHub repository](https://github.com/microsoft/mssql-jdbc)  
 [JDBC Driver API reference](../../connect/jdbc/reference/jdbc-driver-api-reference.md)
