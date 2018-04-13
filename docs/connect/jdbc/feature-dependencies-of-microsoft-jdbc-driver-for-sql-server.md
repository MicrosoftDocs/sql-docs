---
title: "Feature Dependencies of Microsoft JDBC Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: "sql"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 939a8773-2583-49a4-bf00-6b892fbe39dc
caps.latest.revision: 57
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
ms.workload: "On Demand"
---
# Feature Dependencies of Microsoft JDBC Driver for SQL Server
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

 This page contains the list of libraries that the Microsoft JDBC Driver for SQL Server depends on. The project has the following dependencies.
 
 ## Compile Time
 - `azure-keyvault` : Azure Key Vault Provider for Always Encrypted Azure Key Vault feature (optional)
 - `adal4j` : Azure ActiveDirectory Library for Java for Azure Active Directory Authentication feature and Azure Key Vault feature (optional)

 ##  Test Time
Specific projects that require either of the above two features need to explicitly declare the respective dependencies in their pom file:

***For Example:*** If you are using *Azure Active Directory Authentication feature*, then you need to redeclare *adal4j* dependency in your project's pom file. See the following snippet: 
```xml
<dependency>
	<groupId>com.microsoft.sqlserver</groupId>
	<artifactId>mssql-jdbc</artifactId>
	<version>6.4.0.jre8</version>
	<scope>compile</scope>
</dependency>

<dependency>
	<groupId>com.microsoft.azure</groupId>
	<artifactId>adal4j</artifactId>
	<version>1.4.0</version>
</dependency>
```

***For Example:*** If you are using *Azure Key Vault feature* then you need to redeclare *azure-keyvault* dependency and *adal4j* dependency in your project's pom file. See the following snippet: 
```xml
<dependency>
	<groupId>com.microsoft.sqlserver</groupId>
	<artifactId>mssql-jdbc</artifactId>
	<version>6.4.0.jre8</version>
	<scope>compile</scope>
</dependency>

<dependency>
	<groupId>com.microsoft.azure</groupId>
	<artifactId>adal4j</artifactId>
	<version>1.4.0</version>
</dependency>

<dependency>
	<groupId>com.microsoft.azure</groupId>
	<artifactId>azure-keyvault</artifactId>
	<version>1.0.0</version>
</dependency>
```
 
 ## Dependency Requirements for the JDBC Driver

 ### Azure Keyvault Feature:
- JDBC Driver version 6.0.0 
	- Dependency versions: Azure-Keyvault (version 0.9.7),  Adal4j (version 1.3.0), and their dependencies ( [Sample application](../../connect/jdbc/azure-key-vault-sample-version-6.0.0.md))
- JDBC Driver version 6.2.2 and above (including the latest 6.4.0)
	- Dependency versions:  Azure-Keyvault (version 1.0.0),  Adal4j (version 1.4.0), and their dependencies ([Sample Application](../../connect/jdbc/azure-key-vault-sample-version-6.2.2.md))

> [!NOTE]
>   As of v6.2.2, the azure-keyvault-java dependency is updated to version 1.0.0. However, the new version is not compatible with the previous version (version 0.9.7) and therefore breaks the existing implementation in the driver. The new implementation in the driver requires API changes, which in turn break client programs that use the Azure Key Vault feature.

  
 ### Azure Active Directory Authentication:
- JDBC Driver version 6.0.0 
	- Dependency versions: Adal4j (version 1.3.0), and its dependencies
		- In this version of the driver, you can connect using *ActiveDirectoryIntegrated* Authentication Mode  only on a Windows operating system and using sqljdbc_auth.dll and Active Directory Authentication Library for SQL Server (ADALSQL.DLL). 
- JDBC Driver version 6.4.0
	- Dependency versions:  Adal4j (version 1.4.0) and its dependencies
		- In this version of the driver, your application does not require using ADALSQL.DLL. Depending on the operating systems. For **Non-Windows operating systems**, the driver requires Kerberos ticket to work with ActiveDirectoryIntegrated Authentication. See [Set Kerberos ticket on Windows, Linux And Mac](https://docs.microsoft.com/sql/connect/jdbc/connecting-using-azure-active-directory-authentication#set-kerberos-ticket-on-windows-linux-and-mac) for more details. For **Windows operating systems**, driver by default checks if sqljdbc_auth.dll is loaded and does not require Kerberos ticket setup or adal4j dependency. However, If sqljdbc_auth.dll is not loaded, driver behaves the same way as non-Windows operating systems and would require setup, which is described in the following example: 
 A sample application using this feature can be found [here](../../connect/jdbc/connecting-using-azure-active-directory-authentication.md).

 ## See Also  
 [JDBC Driver GitHub Repository](https://github.com/microsoft/mssql-jdbc)  
 [JDBC Driver API Reference](../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  