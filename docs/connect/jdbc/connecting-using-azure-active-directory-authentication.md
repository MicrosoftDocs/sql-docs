---
title: Connect using Microsoft Entra authentication
description: Learn how to develop Java applications that use the Microsoft Entra authentication feature with the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: davidengel
ms.date: 08/29/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connect using Microsoft Entra authentication

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article provides information on how to develop Java applications that use the Microsoft Entra authentication feature with the Microsoft JDBC Driver for SQL Server.

You can use Microsoft Entra authentication, which is a mechanism to connect to Azure SQL Database, Azure SQL Manged Instance, and Azure Synapse Analytics using identities in Microsoft Entra ID. Use Microsoft Entra authentication to centrally manage identities of database users and as an alternative to SQL Server authentication. The JDBC driver allows you to specify your Microsoft Entra credentials in the JDBC connection string to connect to Azure SQL. For information on how to configure Microsoft Entra authentication visit [Connecting to Azure SQL By Using Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).


Connection properties to support Microsoft Entra authentication in the Microsoft JDBC Driver for SQL Server are:

- **authentication**:  Use this property to indicate which SQL authentication method to use for the connection.
  Possible values are:
  - **ActiveDirectoryManagedIdentity**
    - Since driver version 8.3.1, `authentication=ActiveDirectoryMSI` can be used to connect to an Azure SQL Database/Synapse Analytics from an Azure Resource with "Identity" support enabled. Optionally, **msiClientId** can be specified in the Connection/DataSource properties along with this authentication mode. `msiClientId` must contain the Client ID of a Managed Identity to be used to acquire the **accessToken** for establishing the connection. Since driver version v12.2, `authentication=ActiveDirectoryManagedIdentity` can also be used to connect to an Azure SQL Database/Synapse Analytics from an Azure Resource with "Identity" support enabled. Optionally, the Client ID of a Managed Identity can also now be set in the `user` property. For more information, see [Connect using ActiveDirectoryManagedIdentity authentication mode](#connect-using-activedirectorymanagedidentity-authentication-mode).
  - **ActiveDirectoryDefault**
    - Since driver version 12.2, `authentication=ActiveDirectoryDefault` can be used to connect to Azure SQL/Synapse Analytics via the **DefaultAzureCredential** from the Azure Identity client library. For more information, see [Connect using ActiveDirectoryDefault authentication mode](#connect-using-activedirectorydefault-authentication-mode).

  - **ActiveDirectoryIntegrated**
    - Since driver version 6.0, `authentication=ActiveDirectoryIntegrated` can be used to connect to Azure SQL/Synapse Analytics via integrated authentication. To use this authentication mode, you must [federate](/azure/active-directory/hybrid/connect/whatis-fed) the on-premises Active Directory Federation Services (ADFS) with Microsoft Entra ID in the cloud. Once you set it up, you can connect by either adding the native library `mssql-jdbc_auth-<version>-<arch>.dll` to the application class path on Windows, or by setting up a Kerberos ticket for cross-platform authentication support. You're able to access Azure SQL/Azure Synapse Analytics without being prompted for credentials when you're logged in to a domain-joined machine. For more information, see [Connect using ActiveDirectoryIntegrated authentication mode](#connect-using-activedirectoryintegrated-authentication-mode).

  - **ActiveDirectoryPassword**
    - Since driver version 6.0, `authentication=ActiveDirectoryPassword` can be used to connect to Azure SQL/Synapse Analytics with Microsoft Entra username and password. For more information, see [Connect using ActiveDirectoryPassword authentication mode](#connect-using-activedirectorypassword-authentication-mode).

  - **ActiveDirectoryInteractive**
    - Since driver version 9.2, `authentication=ActiveDirectoryInteractive` can be used to connect to an Azure SQL/Synapse Analytics via interactive authentication flow (multifactor authentication). For more information, see [Connect using ActiveDirectoryInteractive authentication mode](#connect-using-activedirectoryinteractive-authentication-mode).

  - **ActiveDirectoryServicePrincipal**
    - Since driver version 9.2, `authentication=ActiveDirectoryServicePrincipal` can be used to connect to an Azure SQL/Synapse Analytics by specifying the application/client ID in the userName property and secret of a service principal identity in the password property. For more information, see [Connect using ActiveDirectoryServicePrincipal authentication mode](#connect-using-activedirectoryserviceprincipal-authentication-mode).

  - **ActiveDirectoryServicePrincipalCertificate**
    - Since driver version 12.4, `authentication=ActiveDirectoryServicePrincipalCertificate` can be used to connect to an Azure SQL Database/Synapse Analytics by specifying the application/client ID in the userName property and the location of the Service Principal certificate in the `clientCertificate` property. For more information, see [Connect using ActiveDirectoryServicePrincipalCertificate authentication mode](#connect-using-activedirectoryserviceprincipalcertificate-authentication-mode).
  - **SqlPassword**
    - Use `authentication=SqlPassword` to connect to a SQL Server using userName/user and password properties.
  - **NotSpecified**
    - Use `authentication=NotSpecified` or leave it as the default when none of these authentication methods are needed.
  - **accessToken**: Use this connection property to connect to a SQL Database with access token. `accessToken` can only be set using the Properties parameter of the `getConnection()` method in the DriverManager class. It can't be used in the connection URL.

For more information, see the authentication property on the [Setting the Connection Properties](setting-the-connection-properties.md) page.

## Client setup requirements

In addition to the basic driver [System requirements](system-requirements-for-the-jdbc-driver.md), the following authentication modes have more requirements.

The following table lists required library dependencies for each authentication mode and driver version. Dependencies of dependencies are also required.

> [!NOTE]
> In cases where the hotfix for a major release has a different dependency version than its major release, the hotfix is also listed.

| Authentication option | Driver versions | Library dependencies |
|-----------------------|-----------------|----------------------|
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 6.0 | `Adal4j` 1.3.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 6.2.2 - 6.4 | `Adal4j` 1.4.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 7.0 | `Adal4j` 1.6.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 7.2 | `Adal4j` 1.6.3<br/>`Client-Runtime-for-AutoRest` 1.6.5 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 7.4 - 8.2 | `Adal4j`l4j 1.6.4<br/>`Client-Runtime-for-AutoRest` 1.7.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated` | 8.4 | `Adal4j` 1.6.5<br/>`Client-Runtime-for-AutoRest` 1.7.4 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 9.2 | `msal4j` 1.7.1 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 9.4 | `msal4j` 1.10.1 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 10.2 | `msal4j` 1.11.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 11.2 | `msal4j` 1.11.3 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 11.2.3 | `msal4j` 1.13.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal` | 12.2 | `msal4j` 1.13.3 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.2 | `azure-identity` 1.7.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal`<br/>`ActiveDirectoryServicePrincipalCertificate` | 12.4 | `msal4j` 1.13.8 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.4 | `azure-identity` 1.9.0 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal`<br/>`ActiveDirectoryServicePrincipalCertificate` | 12.6 | `msal4j` 1.14.1 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.6 | `azure-identity` 1.11.1 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal`<br/>`ActiveDirectoryServicePrincipalCertificate` | 12.6.3 | `msal4j` 1.15.1 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.6.3 | `azure-identity` 1.12.2 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal`<br/>`ActiveDirectoryServicePrincipalCertificate` | 12.6.4 | `msal4j` 1.15.1 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.6.4 | `azure-identity` 1.12.2 |
| `ActiveDirectoryPassword`<br/>`ActiveDirectoryIntegrated`<br/>`ActiveDirectoryInteractive`<br/>`ActiveDirectoryServicePrincipal`<br/>`ActiveDirectoryServicePrincipalCertificate` | 12.8 | `msal4j` 1.15.1 |
| `ActiveDirectoryManagedIdentity`<br/>`ActiveDirectoryMSI`<br/>`ActiveDirectoryDefault` | 12.8 | `azure-identity` 1.12.2 |

## Connect using ActiveDirectoryManagedIdentity authentication mode

This authentication mode is supported starting with version 7.2. To use it, specify `authentication=ActiveDirectoryMSI`. Starting in version 12.2, `authentication=ActiveDirectoryManagedIdentity` can also be specified.

In addition to the library dependency requirements listed in [Client setup requirements](#client-setup-requirements), this feature has the following requirements:

- The target database must have a contained database user, with CONNECT permission. The contained user must represent your Azure Resource's System Assigned Managed Identity or User Assigned Managed Identity, or one of the groups your Managed Identity belongs to.
- The client environment must be an Azure Resource and must have "Identity" feature support enabled. The following table lists Azure services supported by each JDBC driver version:

  | Driver version | Required dependencies | Azure services supported |
  |----------------|------------------------|--------------|
  | 7.2 - 11.2 | None | [Azure App Service and Azure Functions](/azure/app-service/overview-managed-identity)<br/>[Azure Virtual Machines](/azure/active-directory/managed-identities-azure-resources/how-to-use-vm-token) |
  | 12.2<br/>12.4<br/>12.6<br/>12.8 | [azure-identity 1.7.0](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.7.0/index.html#managed-identity-support)<br/>[azure-identity 1.9.0](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.9.0/index.html#managed-identity-support)<br/>[azure-identity 1.11.1](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.11.1/index.html#managed-identity-support)<br/>[azure-identity 1.12.2](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.12.2/index.html#managed-identity-support) | [Azure App Service and Azure Functions](/azure/app-service/overview-managed-identity)<br/>[Azure Arc](/azure/azure-arc/servers/managed-identity-authentication)<br/>[Azure Cloud Shell](/azure/cloud-shell/msi-authorization)<br/>[Azure Kubernetes Service](/azure/aks/use-managed-identity)<br/>[Azure Service Fabric](/azure/service-fabric/concepts-managed-identity)<br/>[Azure Virtual Machines](/azure/active-directory/managed-identities-azure-resources/how-to-use-vm-token)<br/>[Azure Virtual Machines Scale Sets](/azure/active-directory/managed-identities-azure-resources/qs-configure-powershell-windows-vmss) |

The following example shows how to use `authentication=ActiveDirectoryManagedIdentity` mode. Run this example from inside an Azure Resource that is configured for Managed Identity.

To run the example, replace the server/database name with your server/database name on the following lines:

```java
ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
ds.setDatabaseName("demo"); // replace with your database name
//Optional
ds.setMSIClientId("<managed_identity_client>"); // Replace with Client ID of user-assigned managed identity to be used
```

The example to use `ActiveDirectoryMSI` authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MsEntraMSI {
    public static void main(String[] args) throws Exception {

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database name
        ds.setAuthentication("ActiveDirectoryMSI");
        // Optional
        ds.setMSIClientId("<managed_identity_client_guid>"); // Replace with Client ID of user-assigned managed identity to be used

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

The following example demonstrates how to use `authentication=ActiveDirectoryManagedIdentity` mode.

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraManagedIdentity {
    public static void main(String[] args) throws Exception {

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database name
        ds.setAuthentication("ActiveDirectoryManagedIdentity"); // ActiveDirectoryManagedIdentity for JDBC driver version v12.2.0+
        // Optional
        ds.setUser("<managed_identity_client>"); // Replace with Client ID of User-Assigned Managed Identity to be used

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

These examples on an Azure Virtual Machine fetch an access token from _System Assigned Managed Identity_ or _User Assigned Managed Identity_ (if `msiClientId` or `user` is specified with a Client ID of a Managed Identity) and establishes a connection using the fetched access token. If a connection is established, you should see the following message:

```output
You have successfully logged on as: <your Managed Identity username>
```

## Connect using ActiveDirectoryDefault authentication mode

The `ActiveDirectoryDefault` authentication option uses the Azure Identity client library's `DefaultAzureCredential` chained `TokenCredential` implementation. The credential combines commonly used authentication methods chained together.

`ActiveDirectoryDefault` authentication requires a run time dependency on the Azure Identity client library for Managed Identity. For library version details, see [Client setup requirements](#client-setup-requirements).

The following table lists the `DefaultAzureCredential` credential chain for each JDBC driver version.

| Driver version | azure-identity version docs | `DefaultAzureCredential` chain |
|----------------|------------------------|--------------|
| 12.2 | [azure-identity 1.7.0](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.7.0/index.html#defaultazurecredential) | Environment<br/>Managed Identity<br/>IntelliJ<br/>Azure CLI<br/>Azure PowerShell |
| 12.4 | [azure-identity 1.9.0](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.9.0/index.html#defaultazurecredential) | Environment<br/>Workload Identity<br/>Managed Identity<br/>Azure Developer CLI<br/>IntelliJ<br/>Azure CLI<br/>Azure PowerShell |
| 12.6 | [azure-identity 1.11.1](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.11.1/index.html#defaultazurecredential) | Environment<br/>Workload Identity<br/>Managed Identity<br/>Azure Developer CLI<br/>IntelliJ<br/>Azure CLI<br/>Azure PowerShell |
| 12.8 | [azure-identity 1.12.2](https://azuresdkdocs.blob.core.windows.net/$web/java/azure-identity/1.12.2/index.html#defaultazurecredential) | Environment<br/>Workload Identity<br/>Managed Identity<br/>Azure Developer CLI<br/>IntelliJ<br/>Azure CLI<br/>Azure PowerShell |

There are many variables that can be set to configure the `Environment` credential. For details on configuring the `DefaulAzureCredential` chain, including the `Environment` credential, see the relevant version of the azure-identity docs linked in the previous table.

To use the `IntellijCredential` on Windows, set the environment variable `INTELLIJ_KEEPASS_PATH` to the location of your `keepass` file. For example, `INTELLIJ_KEEPASS_PATH=C:\user\your\path\to\the\keepass\file`.

To provide more tenants to the `DefaultAzureCredential`, use the `ADDITIONALLY_ALLOWED_TENANTS` environment variable. This variable takes a comma delimited list. For example, `ADDITIONALLY_ALLOWED_TENANTS=<your-tenant-id-0>,<your-tenant-id-1>,<your-tenant-id-2>,...`

The following example demonstrates how to use `authentication=ActiveDirectoryDefault` mode with the [AzureCliCredential](/java/api/com.azure.identity.azureclicredential) within the `DefaultAzureCredential`.

1. First sign in to the Azure CLI with the following command.

    ```bash
    az login
    ```

1. After successfully logging in to the Azure CLI, run the following code.

    ```java
    import java.sql.Connection;
    import java.sql.ResultSet;
    import java.sql.Statement;
    
    import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
    
    public class MSEntraDefault {
        public static void main(String[] args) throws Exception {
    
            SQLServerDataSource ds = new SQLServerDataSource();
            ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
            ds.setDatabaseName("demo"); // Replace with your database name
            ds.setAuthentication("ActiveDirectoryDefault");
    
            try (Connection connection = ds.getConnection();
                    Statement stmt = connection.createStatement();
                    ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
                if (rs.next()) {
                    System.out.println("You have successfully logged on as: " + rs.getString(1));
                }
            }
        }
    }
    ```

## Connect using ActiveDirectoryIntegrated authentication mode

There are two ways to use `ActiveDirectoryIntegrated` authentication in the Microsoft JDBC Driver for SQL Server:

- On Windows, `mssql-jdbc_auth-<version>-<arch>.dll` from the [downloaded package](download-microsoft-jdbc-driver-for-sql-server.md) can be copied to a location in the system path.
- If you can't use the DLL, starting with version 6.4, you can configure a Kerberos ticket. This method is supported across platforms (Windows, Linux, and macOS). For more information, see [Set Kerberos ticket on Windows, Linux And macOS](#set-kerberos-ticket-on-windows-linux-and-macos).

Ensure you have the required dependent libraries from the [Client setup requirements](#client-setup-requirements).

The following example shows how to use `authentication=ActiveDirectoryIntegrated` mode. This example runs on a domain-joined machine that is federated with Microsoft Entra ID. A database user that represents your Windows user must exist in the database and must have the CONNECT permission.


Replace the server/database name with your server/database name in the following lines before executing the example:

```java
ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
ds.setDatabaseName("demo"); // replace with your database name
```

The example to use ActiveDirectoryIntegrated authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraIntegrated {
    public static void main(String[] args) throws Exception {

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database name
        ds.setAuthentication("ActiveDirectoryIntegrated");

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

Running this example automatically uses your Kerberos ticket from the client platform and no password is required. If a connection is established, you should see the following message:

```output
You have successfully logged on as: <your domain user name>
```

### Set Kerberos ticket on Windows, Linux And macOS

You must up a Kerberos ticket to link your current user to a Windows domain account. Following is a summary of the key steps.

#### Windows

> [!NOTE]
> On Windows, `mssql-jdbc_auth-<version>-<arch>.dll` from the [downloaded package](download-microsoft-jdbc-driver-for-sql-server.md) can be used instead of these Kerberos configuration steps. These steps are only required if you can't use the DLL.

JDK comes with `kinit`, which you can use to get a TGT from Key Distribution Center (KDC) on a domain joined machine that is federated with Microsoft Entra ID.

##### Step 1: Ticket granting ticket retrieval

- **Run on**: Windows
- **Action**:
  - Use the command `kinit username@DOMAIN.COMPANY.COM` to get a TGT from KDC, then it prompts you for your domain password.
  - Use `klist` to see the available tickets. If the kinit was successful, you should see a ticket from `krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM`.

  > [!NOTE]
  > You might have to specify a `.ini` file with `-Djava.security.krb5.conf` for your application to locate KDC.

#### Linux and macOS

##### Requirements

Access to a Windows domain-joined machine to query your Kerberos Domain Controller.

##### Step 1: Find Kerberos KDC

- **Run on**: Windows command line
- **Action**: `nltest /dsgetdc:DOMAIN.COMPANY.COM` (where `DOMAIN.COMPANY.COM` maps to your domain's name)
- **Sample Output**

  ```output
  DC: \\co1-red-dc-33.domain.company.com
  Address: \\2111:4444:2111:33:1111:ecff:ffff:3333
  ...
  The command completed successfully
  ```

- **Information to extract**
  The DC name, in this case `co1-red-dc-33.domain.company.com`

##### Step 2: Configuring KDC in krb5.conf

- **Run on**: Linux/macOS
- **Action**: Edit the `/etc/krb5.conf` in an editor of your choice. Configure the following keys

  ```bash
  [libdefaults]
    default_realm = DOMAIN.COMPANY.COM

  [realms]
  DOMAIN.COMPANY.COM = {
     kdc = co1-red-dc-28.domain.company.com
  }
  ```

  Then save the `krb5.conf` file and exit

  > [!NOTE]
  > Domain must be in ALL CAPS.

##### Step 3: Test the ticket granting ticket retrieval

- **Run on**: Linux/macOS
- **Action**:
  - Use the command `kinit username@DOMAIN.COMPANY.COM` to get a TGT from KDC, then it prompts you for your domain password.
  - Use `klist` to see the available tickets. If the kinit was successful, you should see a ticket from `krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM`.

## Connect using ActiveDirectoryPassword authentication mode

The following example shows how to use `authentication=ActiveDirectoryPassword` mode.

To build and run the example:

1. Ensure you have the required dependent libraries from the [Client setup requirements](#client-setup-requirements).
1. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

1. Locate the following lines of code. Replace user name with the name of the Microsoft Entra user that you want to connect as.

    ```java
    ds.setUser("bob@example.com"); // replace with your username
    ds.setPassword("password");     // replace with your password
    ```

The example to use `ActiveDirectoryPassword` authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraUserPassword {

    public static void main(String[] args) throws Exception{

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
        ds.setUser("bob@example.com"); // Replace with your username
        ds.setPassword("password"); // Replace with your password
        ds.setAuthentication("ActiveDirectoryPassword");

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If the connection is established, you should see the following message as output:

```output
You have successfully logged on as: <your user name>
```


## Connect using ActiveDirectoryInteractive authentication mode

The following example shows how to use `authentication=ActiveDirectoryInteractive` mode.

To build and run the example:

1. Ensure you have the required dependent libraries from the [Client setup requirements](#client-setup-requirements).
1. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

1. Locate the following lines of code. Replace username with the name of the Microsoft Entra user that you want to connect as.

    ```java
    ds.setUser("bob@example.com"); // replace with your username
    ```

The example to use `ActiveDirectoryInteractive` authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraInteractive {
    public static void main(String[] args) throws Exception{

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
        ds.setAuthentication("ActiveDirectoryInteractive");

        // Optional login hint
        ds.setUser("bob@example.com"); // Replace with your user name

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

When you run the program, a browser is displayed to authenticate the user. Exactly what you see depends on how you configure your Microsoft Entra ID. It might or might not include multifactor authentication prompts for username, password, PIN, or second device authentication via a phone. If multiple interactive authentication requests are done in the same program, later requests might not even prompt you if the authentication library can reuse a previously cached authentication token.

For information about how to configure Microsoft Entra ID to require multifactor authentication, see [Getting started with Microsoft Entra multifactor authentication in the cloud](/azure/active-directory/authentication/howto-mfa-getstarted).

For screenshots of these dialog boxes, see [Using Microsoft Entra multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).

If user authentication is completed successfully, you should see the following message in the browser:

```output
Authentication complete. You can close the browser and return to the application.
```

This message only indicates that user authentication was successful but not necessarily a successful connection to the server. Upon return to the application, if a connection is established to the server, you should see the following message as output:

```output
You have successfully logged on as: <your user name>
```

## Connect using ActiveDirectoryServicePrincipal authentication mode

The following example shows how to use `authentication=ActiveDirectoryServicePrincipal` mode.

To build and run the example:

1. Ensure you have the required dependent libraries from the [Client setup requirements](#client-setup-requirements).

1. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

1. Locate the following lines of code. Replace the value of `principalId` with the Application ID / Client ID of the Microsoft Entra service principal that you want to connect as. Replace the value of `principalSecret` with the secret.

    ```java
    String principalId = "<service_principal_guid>"; // Replace with your Microsoft Entra service principal ID.
    String principalSecret = "..."; // Replace with your Microsoft Entra principal secret.
    ```

1. Set the principal ID and principal secret using `setUser` and `setPassword` in version 10.2 and up, and `setAADSecurePrincipalId` and `setAADSecurePrincipalSecret` in version 9.4 and below.

The example to use `ActiveDirectoryServicePrincipal` authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraServicePrincipal {
    public static void main(String[] args) throws Exception{
        String principalId = "<service_principal_guid>"; // Replace with your Microsoft Entra service principal ID.
        String principalSecret = "..."; // Replace with your Microsoft Entra principal secret.

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
        ds.setAuthentication("ActiveDirectoryServicePrincipal");
        ds.setUser(principalId); // setAADSecurePrincipalId for JDBC Driver 9.4 and below
        ds.setPassword(principalSecret); // setAADSecurePrincipalSecret for JDBC Driver 9.4 and below 

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If a connection is established, you should see the following message as output:

```output
You have successfully logged on as: <your app/client ID>
```

## Connect using ActiveDirectoryServicePrincipalCertificate authentication mode

The following example shows how to use `authentication=ActiveDirectoryServicePrincipalCertificate` mode.

To build and run the example:

1. Ensure you have the required dependent libraries from the [Client setup requirements](#client-setup-requirements).

1. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("msentra-managed-demo.database.windows.net"); // replace 'msentra-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

1. Locate the following lines of code. Replace the value of `principalId` with the Application ID / Client ID of the Microsoft Entra service principal that you want to connect as. Replace the value of `clientCertificate` with the location of the service principal certificate.

    ```java
    String principalId = "<service_principal_guid>"; // Replace with your Microsoft Entra service principal ID.

    String clientCertificate = "..."; // Replace with the location for your Microsoft Entra service principal certificate.
    ```

1. If the previously mentioned certificate needs a password, set the principal Secret using `setPassword` in version 10.2 and up or `setAADSecurePrincipalSecret` in version 9.4 and below.

1. If the certificate has an associated private key, set the private key using `setClientKey`. If this key requires a password, set the password for the private key using `setClientKeyPassword`.

The example to use `ActiveDirectoryServicePrincipalCertificate` authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class MSEntraServicePrincipalCertificate {
    public static void main(String[] args) throws Exception{
        String principalId = "<service_principal_guid>"; // Replace with your Microsoft Entra service principal ID.
        String clientCertificate = "..."; // Replace with the location of your service principal certificate.

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
        ds.setAuthentication("ActiveDirectoryServicePrincipalCertificate");
        ds.setUser(principalId); // setAADSecurePrincipalId for JDBC Driver 9.4 and below
        ds.setClientCertificate(clientCertificate);

        try (Connection connection = ds.getConnection();
             Statement stmt = connection.createStatement();
             ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If a connection is established, you should see the following message as output:

```output
You have successfully logged on as: <your app/client ID>
```


## Connect using access token

Applications/services can retrieve an access token from Microsoft Entra ID and use that to connect to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics.


> [!NOTE]
> `accessToken` can only be set using the Properties parameter of the `getConnection()` method in the DriverManager class. It can't be used in the connection string. Starting with driver version 12.2, users can implement and provide an `accessToken` callback to the driver for token renewal in connection pooling scenarios. Connection pooling scenarios require the connection pool implementation to use the standard [JDBC connection pooling classes](using-connection-pooling.md).

The following example contains a simple Java application that connects to Azure using access token-based authentication.

To build and run the example:

1. Create an application account in Microsoft Entra ID for your service.
    1. Sign in to the Azure portal.
    2. Go to  [Microsoft Entra ID](https://portal.azure.com/#view/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/~/Overview) in the left-hand navigation.
    3. Select **App registrations**.
    4. Select **New registration**.
    5. Enter `mytokentest` as a friendly name for the application.
    6. Leave the default selection for supported account types, which can use the application.
    7. Select **Register** at the bottom.
    6. Don't need SIGN-ON URL. Provide anything: `https://mytokentest`.
    7. Select `Create` at the bottom.
    8. Upon selecting **Register**, the app is immediately created, and you're taken to its resource page.
    9. In the **Essentials** box, find the **Application (client) ID** and copy it. You need this value later to configure your application.
    10. Select **Certificates & secrets** from the navigation pane. On the **Client secrets (0)** tab, select **New client secret**. Enter a description for the secret and select an expiration (the default is fine). Select **Add** at the bottom. **Important** before leaving this page, copy the generated **Value** for your client secret. This value can't be viewed after leaving the page. This value is the client secret.
    11. Return to the [App registrations](https://ms.portal.azure.com/#view/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/~/RegisteredApps) pane for Microsoft Entra ID and find the **Endpoints** tab. Copy the URL under `OAuth 2.0 token endpoint`. This URL is your STS URL.

    ![Azure Portal App Registration End Point - STS URL](media/jdbc_aad_token.png)

1. Connect to your database as a Microsoft Entra admin and use a T-SQL command to provision a contained database user for your application principal. For more information on how to create a Microsoft Entra admin and a contained database user, see the [Connecting by using Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).


    ```sql
    CREATE USER [mytokentest] FROM EXTERNAL PROVIDER
    ```

1. On the client machine where you run the example, download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) library and its dependencies. MSAL is only required to run this specific example. The example uses the APIs from this library to retrieve the access token from Microsoft Entra ID. If you already have an access token, you can skip this step and remove the section in the example that retrieves an access token.

In the following example, replace the STS URL, Client ID, Client Secret, server and database name with your values.

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

// The microsoft-authentication-library-for-java is needed to retrieve the access token in this example.
import com.microsoft.aad.msal4j.ClientCredentialFactory;
import com.microsoft.aad.msal4j.ClientCredentialParameters;
import com.microsoft.aad.msal4j.ConfidentialClientApplication;
import com.microsoft.aad.msal4j.IAuthenticationResult;
import com.microsoft.aad.msal4j.IClientCredential;

public class MSEntraTokenBased {

    public static void main(String[] args) throws Exception {

        // Retrieve the access token from Microsoft Entra ID.
        String spn = "https://database.windows.net/";
        String stsurl = "https://login.microsoftonline.com/..."; // Replace with your STS URL.
        String clientId = "<service_principal_guid>"; // Replace with your client ID.
        String clientSecret = "..."; // Replace with your client secret.

        String scope = spn +  "/.default";
        Set<String> scopes = new HashSet<>();
        scopes.add(scope);

        ExecutorService executorService = Executors.newSingleThreadExecutor();
        IClientCredential credential = ClientCredentialFactory.createFromSecret(clientSecret);
        ConfidentialClientApplication clientApplication = ConfidentialClientApplication
            .builder(clientId, credential).executorService(executorService).authority(stsurl).build();
        CompletableFuture<IAuthenticationResult> future = clientApplication
            .acquireToken(ClientCredentialParameters.builder(scopes).build());

        IAuthenticationResult authenticationResult = future.get();
        String accessToken = authenticationResult.accessToken();

        System.out.println("Access Token: " + accessToken);

        // Connect with the access token.
        SQLServerDataSource ds = new SQLServerDataSource();

        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replace with your server name.
        ds.setDatabaseName("demo"); // Replace with your database name.
        ds.setAccessToken(accessToken);

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If the connection is successful, you should see the following message as output:

```output
Access Token: <your access token>
You have successfully logged on as: <your client ID>
```

## Connect using access token callback

Like the access token property, the access token callback allows you to register a method that provides an access token to the driver. The benefit of this callback over the property is the callback allows the driver to request a new access token when the token is expired. For example, a connection pool creating a new connection can request a new token with a new expiration date. For more information, see [Using connection pooling](using-connection-pooling.md).

The following example demonstrates implementing and setting the accessToken callback.

```java
import com.microsoft.aad.msal4j.IClientCredential;
import com.microsoft.aad.msal4j.ClientCredentialFactory;
import com.microsoft.aad.msal4j.ConfidentialClientApplication;
import com.microsoft.aad.msal4j.IAuthenticationResult;
import com.microsoft.aad.msal4j.ClientCredentialParameters;
import java.sql.Connection;
import java.util.HashSet;
import java.util.Set;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class MSEntraAccessTokenCallback {

    public static void main(String[] args) {

        SQLServerAccessTokenCallback callback = new SQLServerAccessTokenCallback() {
            @Override
            public SqlAuthenticationToken getAccessToken(String spn, String stsurl) {

                String clientSecret = "..."; // Replace with your client secret.
                String clientId = "<service_principal_guid>"; // Replace with your client ID.

                String scope = spn + "/.default";
                Set<String> scopes = new HashSet<>();
                scopes.add(scope);

                try {
                    ExecutorService executorService = Executors.newSingleThreadExecutor();
                    IClientCredential credential = ClientCredentialFactory.createFromSecret(clientSecret);
                    ConfidentialClientApplication clientApplication = ConfidentialClientApplication
                            .builder(clientId, credential).executorService(executorService).authority(stsurl).build();
                    CompletableFuture<IAuthenticationResult> future = clientApplication
                            .acquireToken(ClientCredentialParameters.builder(scopes).build());

                    IAuthenticationResult authenticationResult = future.get();
                    String accessToken = authenticationResult.accessToken();

                    return new SqlAuthenticationToken(accessToken, authenticationResult.expiresOnDate().getTime());
                } catch (Exception e) {
                    e.printStackTrace();
                }
                return null;
            }
        };

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replaces with your server name.
        ds.setDatabaseName("demo"); // Replace with your database name.
        ds.setAccessTokenCallback(callback);

        try (Connection connection = ds.getConnection();
                Statement stmt = connection.createStatement();
                ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If the connection is successful, you should see the following message as output:

```output
You have successfully logged on as: <your client ID>
```

Starting from version 12.4, the `accessToken` callback can be set through the `accessTokenCallbackClass` connection string property. The following example shows how to set the `accessToken` callback using this property:

```java
import com.microsoft.aad.msal4j.IClientCredential;
import com.microsoft.aad.msal4j.ClientCredentialFactory;
import com.microsoft.aad.msal4j.ConfidentialClientApplication;
import com.microsoft.aad.msal4j.IAuthenticationResult;
import com.microsoft.aad.msal4j.ClientCredentialParameters;
import java.sql.Connection;
import java.util.HashSet;
import java.util.Set;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

 

public class MSEntraAccessTokenCallbackClass {
    public static class AccessTokenCallbackClass implements SQLServerAccessTokenCallback {
        @Override
        public SqlAuthenticationToken getAccessToken(String spn, String stsurl) {
            String clientSecret = "..."; // Replace with your client secret.
            String clientId = "<service_principal_guid>"; // Replace with your client ID.
            
            String scope = spn + "/.default";
            Set<String> scopes = new HashSet<>();
            scopes.add(scope);
            
            try {
                ExecutorService executorService = Executors.newSingleThreadExecutor();
                IClientCredential credential = ClientCredentialFactory.createFromSecret(clientSecret);
                ConfidentialClientApplication clientApplication = ConfidentialClientApplication

                        .builder(clientId, credential).executorService(executorService).authority(stsurl).build();
                
                CompletableFuture<IAuthenticationResult> future = clientApplication
                        .acquireToken(ClientCredentialParameters.builder(scopes).build());
                
                IAuthenticationResult authenticationResult = future.get();
                String accessToken = authenticationResult.accessToken();
                
                return new SqlAuthenticationToken(accessToken, authenticationResult.expiresOnDate().getTime());
            } catch (Exception e) {
                e.printStackTrace();
            }
            return null;
        }
    }
    
    public static void main(String[] args) throws Exception {
        
        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("msentra-managed-demo.database.windows.net"); // Replaces with your server name.
        ds.setDatabaseName("demo"); // Replace with your database name.
        ds.setAccessTokenCallbackClass(AccessTokenCallbackClass.class.getName());
        
        try (Connection connection = ds.getConnection();
             Statement stmt = connection.createStatement();
             ResultSet rs = stmt.executeQuery("SELECT SUSER_SNAME()")) {
            if (rs.next()) {
                System.out.println("You have successfully logged on as: " + rs.getString(1));
            }
        }
    }
}
```

If the connection is successful, you should see the following message as output:

```output
You have successfully logged on as: <your client ID>
```

## Next steps

Learn more about related concepts in the following articles:

- [Connecting to Azure SQL by using Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview)
- [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java)
- [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java)
- [Troubleshoot connection issues to Azure SQL Database](/azure/sql-database/sql-database-troubleshoot-common-connection-issues)
