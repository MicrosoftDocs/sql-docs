---
title: "Feature Dependencies of Microsoft JDBC Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
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

# Feature Dependencies of Microsoft JDBC Driver for SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page lists down libraries that the Microsoft JDBC Driver for SQL Server depends on. The project has the following dependencies.

## Compile Time

- `azure-keyvault`: Azure Key Vault Provider for Always Encrypted Azure Key Vault feature (optional)
- `adal4j`: Azure ActiveDirectory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature (optional)

## Test Time

Specific projects that require either of the above two features need to explicitly declare the respective dependencies in their pom file:

**_For Example:_** When using _Azure Active Directory Authentication feature_, then you need to redeclare _adal4j_ dependency in your project's pom file. See the following snippet:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>7.0.0.jre10</version>
    <scope>compile</scope>
</dependency>

<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>adal4j</artifactId>
    <version>1.6.0</version>
</dependency>
```

**_For Example:_** When using _Azure Key Vault feature_, then you need to redeclare _azure-keyvault_ dependency and _adal4j_ dependency in your project's pom file. See the following snippet:

```xml
<dependency>
	<groupId>com.microsoft.sqlserver</groupId>
	<artifactId>mssql-jdbc</artifactId>
	<version>7.0.0.jre10</version>
	<scope>compile</scope>
</dependency>

<dependency>
	<groupId>com.microsoft.azure</groupId>
	<artifactId>adal4j</artifactId>
	<version>1.6.0</version>
</dependency>

<dependency>
	<groupId>com.microsoft.azure</groupId>
	<artifactId>azure-keyvault</artifactId>
	<version>1.0.0</version>
</dependency>
```

## Dependency Requirements for the JDBC Driver

### Working with Azure Key Vault Provider:

- JDBC Driver version 7.0.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.6.0), and their dependencies ([Sample Application](../../connect/jdbc/azure-key-vault-sample-version-7-0-0.md))
- JDBC Driver version 6.4.0 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([Sample Application](../../connect/jdbc/azure-key-vault-sample-version-6.2.2.md))
- JDBC Driver version 6.2.2 - Dependency versions: Azure-Keyvault (version 1.0.0), Adal4j (version 1.4.0), and their dependencies ([Sample Application](../../connect/jdbc/azure-key-vault-sample-version-6.2.2.md))
- JDBC Driver version 6.0.0 - Dependency versions: Azure-Keyvault (version 0.9.7), Adal4j (version 1.3.0), and their dependencies ( [Sample application](../../connect/jdbc/azure-key-vault-sample-version-6.0.0.md))

> [!NOTE]
> With v6.2.2 and v6.4.0 driver versions, the azure-keyvault-java dependency had been updated to version 1.0.0. However, the new version was not compatible with the previous version (version 0.9.7) and therefore breaks the existing implementation in the driver. The new implementation in the driver required API changes, which in turn breaks client programs that use the Azure Key Vault Provider.

> This problem has been resolved with latest driver version v7.0.0 as the removed constructor using authentication callback mechanism is added back to Azure Key Vault provider for backwards compatibility.

### Working with Azure Active Directory Authentication:

- JDBC Driver version 7.0.0 - Dependency versions: Ada4j (version 1.6.0) and its dependencies
- JDBC Driver version 6.4.0 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.2.2 - Dependency versions: Adal4j (version 1.4.0) and its dependencies
- JDBC Driver version 6.0.0 - Dependency versions: Adal4j (version 1.3.0), and its dependencies - In this version of the driver, you can connect using _ActiveDirectoryIntegrated_ Authentication Mode only on a Windows operating system and using sqljdbc_auth.dll and Active Directory Authentication Library for SQL Server (ADALSQL.DLL).

From driver version 6.4.0 onwards, applications don't necessarily require using ADALSQL.DLL on Windows Operating Systems. For **Non-Windows operating systems**, the driver requires Kerberos ticket to work with ActiveDirectoryIntegrated Authentication. For more information about how to connect to Active Directory using Kerberos, see [Set Kerberos ticket on Windows, Linux And Mac](https://docs.microsoft.com/sql/connect/jdbc/connecting-using-azure-active-directory-authentication#set-kerberos-ticket-on-windows-linux-and-mac).

For **Windows operating systems**, driver looks for sqljdbc_auth.dll by default and doesn't require Kerberos ticket setup or Azure library dependencies. However, If sqljdbc_auth.dll isn't available, driver looks for Kerberos ticket for authenticating to Active Directory as on other Operating Systems.

A sample application using this feature can be found [here](../../connect/jdbc/connecting-using-azure-active-directory-authentication.md).

## See Also

[JDBC Driver GitHub Repository](https://github.com/microsoft/mssql-jdbc)  
 [JDBC Driver API Reference](../../connect/jdbc/reference/jdbc-driver-api-reference.md)
