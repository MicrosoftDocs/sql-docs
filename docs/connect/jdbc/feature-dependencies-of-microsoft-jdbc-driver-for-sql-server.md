---
title: Feature Dependencies for Microsoft JDBC Driver
description: Add optional Java libraries and native components for Microsoft JDBC Driver features such as Microsoft Entra authentication, Always Encrypted, OSGi, and useFMTOnly.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 11/06/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Feature dependencies of the Microsoft JDBC Driver for SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A basic application that declares only the `com.microsoft.sqlserver:mssql-jdbc` dependency resolves only the driver JAR at runtime. Some driver features use optional Java libraries or native components. Add those dependencies only when your application uses the corresponding feature.

Use the dependency versions that match your driver version. The versions in this article come from the published POMs for driver 13.6.2. For any other release, read the POM that shipped with that release. This article lists the current release first and keeps historical dependency information for older applications.

<a id="compile-time"></a>

## Compile-time dependencies

The published POMs for the `jre8` and `jre11` artifacts declare these feature dependencies:

| Feature | Dependency | Version | POM status |
| --- | --- | --- | --- |
| Azure Key Vault provider for Always Encrypted | `com.azure:azure-security-keyvault-keys` | `4.11.1` | Optional. |
| Microsoft Entra authentication and Azure Key Vault provider identity | `com.azure:azure-identity` | `1.18.4` | Optional. |
| Azure SDK HTTP transport used by Azure SDK dependencies | `com.azure:azure-core-http-netty` | `1.16.6` | Optional. |
| `useFmtOnly` parsing support | `org.antlr:antlr4-runtime` | `4.9.3` | Optional. |
| Always Encrypted with secure enclaves | `com.google.code.gson:gson` | `2.11.0` | Optional. |
| Always Encrypted with secure enclaves | `org.bouncycastle:bcprov-jdk18on` | `1.84` | Optional. |
| Client certificate authentication | `org.bouncycastle:bcpkix-jdk18on` | `1.84` | Optional. |
| OSGi support | `org.osgi:org.osgi.core` | `6.0.0` | Provided by the OSGi framework. |
| OSGi service JDBC support | `org.osgi:org.osgi.service.jdbc` | `1.1.0` | Provided by the OSGi framework. |

The POM marks the feature libraries in the previous table as optional. Maven and Gradle don't add optional dependencies transitively for a normal application dependency, so add the dependencies your feature needs to your application directly. The two OSGi API dependencies use `provided` scope instead. An OSGi framework supplies them at runtime, so don't add them to a standard Java application.

<a id="run-time"></a>

## Runtime dependencies

For a base application, add only the driver dependency:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>13.6.2.jre11</version>
</dependency>
```

If your application uses Microsoft Entra authentication modes that rely on Azure Identity, add `azure-identity` and the pinned Azure SDK HTTP transport. `azure-identity` brings `azure-core-http-netty` transitively, but at a lower version than the driver POM declares, so declare the transport yourself to get the version the driver was built against:

```xml
<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.18.4</version>
</dependency>
<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-core-http-netty</artifactId>
    <version>1.16.6</version>
</dependency>
```

If your application uses the Azure Key Vault provider for Always Encrypted, add `azure-security-keyvault-keys`, `azure-identity`, and the pinned Azure SDK HTTP transport:

```xml
<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-security-keyvault-keys</artifactId>
    <version>4.11.1</version>
</dependency>
<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-identity</artifactId>
    <version>1.18.4</version>
</dependency>
<dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-core-http-netty</artifactId>
    <version>1.16.6</version>
</dependency>
```

If your application uses Always Encrypted with secure enclaves, add the enclave dependencies that your attestation path uses:

```xml
<dependency>
    <groupId>com.google.code.gson</groupId>
    <artifactId>gson</artifactId>
    <version>2.11.0</version>
</dependency>
<dependency>
    <groupId>org.bouncycastle</groupId>
    <artifactId>bcprov-jdk18on</artifactId>
    <version>1.84</version>
</dependency>
```

If your application loads a Public-Key Cryptography Standards (PKCS) #1 private key for client certificate authentication, add `bcpkix-jdk18on`:

```xml
<dependency>
    <groupId>org.bouncycastle</groupId>
    <artifactId>bcpkix-jdk18on</artifactId>
    <version>1.84</version>
</dependency>
```

If your application uses `useFmtOnly`, add the ANTLR runtime:

```xml
<dependency>
    <groupId>org.antlr</groupId>
    <artifactId>antlr4-runtime</artifactId>
    <version>4.9.3</version>
</dependency>
```

If you're not using Maven or Gradle, download the same libraries and their transitive dependencies, then place them on the same application classpath as the driver.

## Dependency requirements for the JDBC driver

### Work with the Azure Key Vault provider

Use these direct dependencies:

| Dependency | Version |
| --- | --- |
| `com.azure:azure-security-keyvault-keys` | `4.11.1` |
| `com.azure:azure-identity` | `1.18.4` |
| `com.azure:azure-core-http-netty` | `1.16.6` |

The current Azure Key Vault sample targets the modern Azure SDK path. For more information, see [Azure Key Vault sample version 9.2 and later](azure-key-vault-sample-version-9.2.md).

### Choose authentication dependencies

The following table lists authentication dependencies beyond the driver JAR. Microsoft Authentication Library for Java (MSAL4J), published as `com.microsoft.azure:msal4j`, provides several Microsoft Entra authentication paths. Include each Java library's transitive dependencies. Native dynamic-link libraries (DLLs) must match your Java virtual machine (JVM) architecture.

| Authentication method or path | Additional Java libraries | Native components or configuration |
| --- | --- | --- |
| SQL password authentication (`authentication=SqlPassword`) | None. | None. |
| Windows integrated authentication (`integratedSecurity=true`, `authenticationScheme=NativeAuthentication`) | None. | The versioned `mssql-jdbc_auth` DLL for your JVM architecture. Windows only. |
| Java Kerberos (`integratedSecurity=true`, `authenticationScheme=JavaKerberos`) | None beyond the driver and Java runtime. | Configure Kerberos credentials. The Windows authentication DLL isn't required. |
| NT LAN Manager (NTLM) (`integratedSecurity=true`, `authenticationScheme=NTLM`) | None. | Configure the domain, user name, and password. The Windows authentication DLL isn't required. |
| `ActiveDirectoryPassword` (deprecated) | MSAL4J. | No authentication DLL required. |
| `ActiveDirectoryManagedIdentity` | `azure-identity` and the pinned `azure-core-http-netty` transport. | Run in an environment that provides the managed identity. |
| `ActiveDirectoryServicePrincipal` | MSAL4J. | Configure the application's client ID and secret. |
| `ActiveDirectoryServicePrincipalCertificate` | MSAL4J. Also add `bcpkix-jdk18on` when loading a PKCS#1 private key. | Configure the application's client ID and certificate. |
| `ActiveDirectoryIntegrated`, Windows native path | None. | The versioned `mssql-jdbc_auth` wrapper and `mssql-auth.dll`, as described in the following section. |
| `ActiveDirectoryIntegrated`, Java fallback | MSAL4J. | Configure Kerberos credentials. The Windows authentication DLLs aren't required. |
| `ActiveDirectoryInteractive` | MSAL4J. | Interactive sign-in through a browser. |
| `ActiveDirectoryDefault` | `azure-identity` and the pinned `azure-core-http-netty` transport. | Configure a credential supported by the default credential chain. |
| Caller-supplied `accessToken` or access-token callback | The driver doesn't require a token-acquisition library for this path. | Your application or callback supplies the token and its own token-acquisition dependencies. |

> [!IMPORTANT]
> `ActiveDirectoryPassword` is deprecated and doesn't support multifactor authentication. For migration guidance, see [Microsoft Entra password authentication deprecation](connecting-using-azure-active-directory-authentication.md#connect-using-activedirectorypassword-authentication-mode).

The `azure-identity` dependency shown in this article includes MSAL4J transitively. You can use that dependency for the MSAL4J paths, or add MSAL4J and its transitive dependencies directly. The two `ActiveDirectoryIntegrated` rows describe alternative paths, not dependencies that every application must install together.

<a id="work-with-azure-active-directory-authentication"></a>

### Work with Microsoft Entra authentication

Add `com.azure:azure-identity` when your authentication mode uses Azure Identity credentials. Microsoft Entra authentication modes include `ActiveDirectoryPassword`, `ActiveDirectoryIntegrated`, `ActiveDirectoryManagedIdentity`, `ActiveDirectoryServicePrincipal`, `ActiveDirectoryServicePrincipalCertificate`, `ActiveDirectoryInteractive`, and `ActiveDirectoryDefault`.

For `authentication=ActiveDirectoryIntegrated`, the driver selects one of two authentication paths:

- On Windows, if the native wrapper loads, the driver uses the native authentication path. The Java virtual machine (JVM) loads the versioned, architecture-specific wrapper `mssql-jdbc_auth-<version>-<arch>.dll`, which ships with the JDBC driver download and is also published to Maven Central as `com.microsoft.sqlserver:mssql-jdbc_auth`. For example, driver 13.6.2 for an x64 JVM loads `mssql-jdbc_auth-13.6.2.x64.dll`. Place that wrapper on the `java.library.path`. The wrapper then loads `mssql-auth.dll` when the driver requests a token. The JDBC package doesn't include `mssql-auth.dll`. Install Microsoft ODBC Driver 18 for SQL Server or Microsoft OLE DB Driver 19 for SQL Server to get it, and let that installer place the file.
- If the native wrapper isn't loaded, the driver uses Microsoft Authentication Library for Java (MSAL4J) on both Windows and non-Windows systems. Configure Kerberos for this path and include MSAL4J with its transitive dependencies on the application classpath. The `azure-identity` dependency shown in this article includes MSAL4J transitively. The Windows native DLLs aren't required for this Java authentication path.

If neither the native wrapper nor MSAL4J is available, the driver reports that it failed to load both the authentication DLL and the MSAL4J Java library. Add the Java dependencies and configure Kerberos, or configure the native path on Windows.

If the wrapper loads but can't load `mssql-auth.dll`, the driver raises a separate error that names `mssql-auth.dll` and includes a hexadecimal Windows error code. It doesn't switch to MSAL4J after this failure. Install Microsoft ODBC Driver 18 for SQL Server or Microsoft OLE DB Driver 19 for SQL Server, and confirm that the installed architecture matches the JVM.

For the authentication walkthrough and mode-specific setup, see [Connect using Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md).

## Historical dependency notes

Applications on older driver versions might still use previous-generation Azure SDK libraries such as `com.microsoft.azure:adal4j`, `com.microsoft.azure:azure-keyvault`, and `com.microsoft.rest:client-runtime`. Keep those versions aligned with the POM for the driver version you run. Don't mix dependency sets from different major driver versions unless you tested the exact application.

## Related content

- [Install the JDBC driver](using-the-jdbc-driver.md)
- [Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md)
- [Using Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md)
- [Using Always Encrypted with secure enclaves](using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md)
- [JDBC driver GitHub repository](https://github.com/microsoft/mssql-jdbc)
- [JDBC driver API reference](reference/jdbc-driver-api-reference.md)
