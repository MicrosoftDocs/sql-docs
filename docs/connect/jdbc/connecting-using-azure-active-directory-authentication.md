---
title: Connecting using Azure Active Directory authentication
description: Learn how to develop Java applications that use the Azure Active Directory authentication feature with the Microsoft JDBC Driver for SQL Server.
ms.custom: ""
ms.date: 04/14/2021
ms.reviewer: ""
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 9c9d97be-de1d-412f-901d-5d9860c3df8c
author: David-Engel
ms.author: v-daenge
---
# Connecting using Azure Active Directory authentication

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article provides information on how to develop Java applications that use the Azure Active Directory authentication feature with the Microsoft JDBC Driver for SQL Server.

You can use Azure Active Directory (Azure AD) authentication, which is a mechanism of connecting to Azure SQL Database v12 using identities in Azure Active Directory. Use Azure Active Directory authentication to centrally manage identities of database users and as an alternative to SQL Server authentication. The JDBC Driver allows you to specify your Azure Active Directory credentials in the JDBC connection string to connect to Azure SQL Database. For information on how to configure Azure Active Directory authentication visit [Connecting to SQL Database By Using Azure Active Directory Authentication](/azure/azure-sql/database/authentication-aad-overview).

Connection properties to support Azure Active Directory authentication in the Microsoft JDBC Driver for SQL Server are:

- **authentication**:  Use this property to indicate which SQL authentication method to use for the connection.
  Possible values are:
  - **ActiveDirectoryMSI**
    - Supported since driver version **v7.2**, `authentication=ActiveDirectoryMSI` can be used to connect to an Azure SQL Database/Synapse Analytics from inside of an Azure Resource with "Identity" support enabled. Optionally, **msiClientId** can also be specified in the Connection/DataSource properties along with this authentication mode, which must contain the Client ID of a Managed Identity to be used to acquire the **accessToken** for establishing the connection.
  - **ActiveDirectoryIntegrated**
    - Supported since driver version **v6.0**, `authentication=ActiveDirectoryIntegrated` can be used to connect to an Azure SQL Database/Synapse Analytics using integrated authentication. To use this authentication mode, you need to federate the on-premise Active Directory Federation Services (ADFS) with Azure Active Directory in the cloud. Once it is set up, you can connect by either adding the native library 'mssql-jdbc_auth-\<version>-\<arch>.dll' to the application class path on Windows OS, or setting up a Kerberos ticket for cross-platform authentication support. You will be able to access Azure SQL Database/Azure Synapse Analytics without being prompted for credentials when you're logged in to a domain joined machine.
  - **ActiveDirectoryPassword**
    - Supported since driver version **v6.0**, `authentication=ActiveDirectoryPassword` can be used to connect to an Azure SQL Database/Synapse Analytics using an Azure AD user name and password.
  - **ActiveDirectoryInteractive**
    - Supported since driver version **v9.2**, `authentication=ActiveDirectoryInteractive` can be used to connect to an Azure SQL Database/Synapse Analytics using an interactive authentication flow (multi-factor authentication).
  - **ActiveDirectoryServicePrincipal**
    - Supported since driver version **v9.2**, `authentication=ActiveDirectoryServicePrincipal` can be used to connect to an Azure SQL Database/Synapse Analytics using the client ID and secret of a service principal identity.
  - **SqlPassword**
    - Use `authentication=SqlPassword` to connect to a SQL Server using userName/user and password properties.
  - **NotSpecified**
    - Use `authentication=NotSpecified` or leave it as the default when none of these authentication methods are needed.
  - **accessToken**: Use this connection property to connect to a SQL Database using an access token. accessToken can only be set using the Properties parameter of the getConnection() method in the DriverManager class. It cannot be used in the connection URL.

For more information, see the authentication property on the [Setting the Connection Properties](setting-the-connection-properties.md) page.

## Client setup requirements

For **ActiveDirectoryMSI** authentication, the below components must be installed on the client machine:

- Java 8 or above
- Microsoft JDBC Driver 7.2 (or higher) for SQL Server
- Client Environment must be an Azure Resource and must have "Identity" feature support enabled.
- A contained database user representing your Azure Resource's System Assigned Managed Identity or User Assigned Managed Identity, or one of the groups your Managed Identity belongs to, must exist in the target database and must have the CONNECT permission.

For other authentication modes, the below components must be installed on the client machine:

- Java 8 or above
- Microsoft JDBC Driver 6.0 (or higher) for SQL Server
- If you're using the access token-based authentication mode, you need either [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, to run the examples from this article. For more information, see the [Connecting using access token](#connecting-using-access-token) section.
- If you're using the **ActiveDirectoryPassword** authentication mode, you need either [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1. For more information, see the [Connecting using ActiveDirectoryPassword authentication mode](#connecting-using-activedirectorypassword-authentication-mode) section.
- If you're using the **ActiveDirectoryIntegrated** mode, you need either [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1. For more information, see the [Connecting using ActiveDirectoryIntegrated authentication mode](#connecting-using-activedirectoryintegrated-authentication-mode) section.
- If you're using the **ActiveDirectoryInteractive** mode, you need either [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1. For more information, see the [Connecting using ActiveDirectoryInteractive authentication mode](#connecting-using-activedirectoryinteractive-authentication-mode) section.
- If you're using the **ActiveDirectoryServicePrincipal** mode, you need either [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies. For more information, see the [Connecting using ActiveDirectoryServicePrincipal authentication mode](#connecting-using-activedirectoryserviceprincipal-authentication-mode) section.

## Connecting using ActiveDirectoryMSI authentication mode

The following example shows how to use `authentication=ActiveDirectoryMSI` mode. Run this example from inside an Azure Resource, e,g an Azure Virtual Machine, App Service, or a Function App that is federated with Azure Active Directory.

Replace the server/database name with your server/database name in the following lines before executing the example:

```java
ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
ds.setDatabaseName("demo"); // replace with your database name
//Optional
ds.setMSIClientId("94de34e9-8e8c-470a-96df-08110924b814"); // Replace with Client ID of User-Assigned Managed Identity to be used
```

The example to use ActiveDirectoryMSI authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class AAD_MSI {
    public static void main(String[] args) throws Exception {

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database name
        ds.setAuthentication("ActiveDirectoryMSI");
        // Optional
        ds.setMSIClientId("94de34e9-8e8c-470a-96df-08110924b814"); // Replace with Client ID of User-Assigned Managed Identity to be used

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

Running this example on an Azure Virtual Machine fetches an access token from _System Assigned Managed Identity_ or _User Assigned Managed Identity_ (if **msiClientId** is specified) and establishes a connection using the fetched access token. If a connection is established, you should see the following message:

```output
You have successfully logged on as: <your Managed Identity username>
```

## Connecting using ActiveDirectoryIntegrated authentication mode

With version 6.4, Microsoft JDBC Driver adds support for ActiveDirectoryIntegrated Authentication using a Kerberos ticket on multiple platforms (Windows, Linux, and macOS).
For more information, see [Set Kerberos ticket on Windows, Linux And macOS](#set-kerberos-ticket-on-windows-linux-and-macos) for more details. Alternatively, on Windows, mssql-jdbc_auth-\<version>-\<arch>.dll can also be used for ActiveDirectoryIntegrated authentication with JDBC Driver.

> [!NOTE]
> If you are using an older version of the driver, check this [link](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md) for the respective dependencies that are required to use this authentication mode.

The following example shows how to use `authentication=ActiveDirectoryIntegrated` mode. Run this example on a domain joined machine that is federated with Azure Active Directory. A contained database user representing your Azure AD user, or one of the groups you belong to, must exist in the database and must have the CONNECT permission.

Before building and running the example, on the client machine (on which, you want to run the example), download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, and include them in the Java build path

Replace the server/database name with your server/database name in the following lines before executing the example:

```java
ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
ds.setDatabaseName("demo"); // replace with your database name
```

The example to use ActiveDirectoryIntegrated authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class AADIntegrated {
    public static void main(String[] args) throws Exception {

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
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

Running this example on a client machine automatically uses your Kerberos ticket and no password is required. If a connection is established, you should see the following message:

```output
You have successfully logged on as: <your domain user name>
```

### Set Kerberos ticket on Windows, Linux And macOS

You need to set up a Kerberos ticket linking your current user to a Windows domain account. A summary of key steps is included below.

#### Windows

JDK comes with `kinit`, which you can use to get a TGT from Key Distribution Center (KDC) on a domain joined machine that is federated with Azure Active Directory.

##### Step 1: Ticket granting ticket retrieval

- **Run on**: Windows
- **Action**:
  - Use the command `kinit username@DOMAIN.COMPANY.COM` to get a TGT from KDC, then it will prompt you for your domain password.
  - Use `klist` to see the available tickets. If the kinit was successful, you should see a ticket from krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.

> [!NOTE]
> You may need to specify a `.ini` file with `-Djava.security.krb5.conf` for your application to locate KDC.

#### Linux and macOS

##### Requirements

Access to a Windows domain-joined machine to query your Kerberos Domain Controller.

##### Step 1: Find Kerberos KDC

- **Run on**: Windows command line
- **Action**: `nltest /dsgetdc:DOMAIN.COMPANY.COM` (where "DOMAIN.COMPANY.COM" maps to your domain's name)
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
- **Action**: Edit the /etc/krb5.conf in an editor of your choice. Configure the following keys

  ```bash
  [libdefaults]
    default_realm = DOMAIN.COMPANY.COM

  [realms]
  DOMAIN.COMPANY.COM = {
     kdc = co1-red-dc-28.domain.company.com
  }
  ```

  Then save the krb5.conf file and exit

> [!NOTE]
> Domain must be in ALL CAPS.

##### Step 3: Testing the ticket granting ticket retrieval

- **Run on**: Linux/macOS
- **Action**:
  - Use the command `kinit username@DOMAIN.COMPANY.COM` to get a TGT from KDC, then it will prompt you for your domain password.
  - Use `klist` to see the available tickets. If the kinit was successful, you should see a ticket from krbtgt/DOMAIN.COMPANY.COM@ DOMAIN.COMPANY.COM.

## Connecting using ActiveDirectoryPassword authentication mode

The following example shows how to use `authentication=ActiveDirectoryPassword` mode.

Before building and running the example:

1. On the client machine (on which, you want to run the example), download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, and include them in the Java build path.
1. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

1. Locate the following lines of code and replace user name, with the name of the Azure AD user you want to connect as.

    ```java
    ds.setUser("bob@cqclinic.onmicrosoft.com"); // replace with your user name
    ds.setPassword("password");     // replace with your password
    ```

The example to use ActiveDirectoryPassword authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class AADUserPassword {

    public static void main(String[] args) throws Exception{

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
        ds.setUser("bob@cqclinic.onmicrosoft.com"); // Replace with your user name
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

If connection is established, you should see the following message as output:

```output
You have successfully logged on as: <your user name>
```

> [!NOTE]
> A contained user database must exist and a contained database user representing the specified Azure AD user or one of the groups, the specified Azure AD user belongs to, must exist in the database, and must have the CONNECT permission (except for Azure Active Directory server admin or group)

## Connecting using ActiveDirectoryInteractive authentication mode

The following example shows how to use `authentication=ActiveDirectoryInteractive` mode.

Before building and running the example:

1. On the client machine (on which, you want to run the example), download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, and include them in the Java build path
2. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

3. Locate the following lines of code and replace user name, with the name of the Azure AD user you want to connect as.

    ```java
    ds.setUser("bob@cqclinic.onmicrosoft.com"); // replace with your user name
    ```

The example to use ActiveDirectoryInteractive authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class AADInteractive {
    public static void main(String[] args) throws Exception{

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
    ds.setAuthentication("ActiveDirectoryInteractive");

    // Optional
        ds.setUser("bob@cqclinic.onmicrosoft.com"); // Replace with your user name

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

When running the program, a browser will be displayed to authenticate the user. Exactly what the user sees depends on how their Azure AD has been configured. It may or may not include multi-factor authentication prompts for things like a username, a password, a PIN, or second device authentication via a phone, for example. If multiple interactive authentication requests are done in the same program, subsequent requests may not even prompt the user if the authentication library can re-use a previously cached authentication token.

For information about how to configure Azure AD to require Multi-Factor Authentication, see [Getting started with Azure AD Multi-Factor Authentication in the cloud](/azure/active-directory/authentication/howto-mfa-getstarted).

For screenshots of these dialog boxes, see [Configure multi-factor authentication for SQL Server Management Studio and Azure AD](/azure/azure-sql/database/authentication-mfa-ssms-configure).

If user authentication is completed successfully, you should see the following message in the browser:

```output
Authentication complete. You can close the browser and return to the application.
```

Note this only indicates user authentication was successful but not necessarily a successful connection to the server. Upon returning to the application, if connection is established to the server, you should see the following message as output:

```output
You have successfully logged on as: <your user name>
```

> [!NOTE]
> A contained user database must exist and a contained database user representing the specified Azure AD user or one of the groups the specified Azure AD user belongs to, must exist in the database and must have the CONNECT permission (except for an Azure Active Directory server admin or group)

## Connecting using ActiveDirectoryServicePrincipal authentication mode

The following example shows how to use `authentication=ActiveDirectoryServicePrincipal` mode.

Before building and running the example:

1. On the client machine (on which, you want to run the example), download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, and include them in the Java build path
2. Locate the following lines of code and replace the server/database name with your server/database name.

    ```java
    ds.setServerName("aad-managed-demo.database.windows.net"); // replace 'aad-managed-demo' with your server name
    ds.setDatabaseName("demo"); // replace with your database name
    ```

3. Locate the following lines of code and replace user name, with the name of the Azure AD user you want to connect as.

    ```java
    ds.setUser("bob@cqclinic.onmicrosoft.com"); // replace with your user name
    ```

The example to use ActiveDirectoryInteractive authentication mode:

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class AADServicePrincipal {
    public static void main(String[] args) throws Exception{
        String principalId = "1846943b-ad04-4808-aa13-4702d908b5c1"; // Replace with your AAD secure principal ID.
        String principalSecret = "..."; // Replace with your AAD principal secret.

        SQLServerDataSource ds = new SQLServerDataSource();
        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name
        ds.setDatabaseName("demo"); // Replace with your database
    ds.setAuthentication("ActiveDirectoryServicePrincipal");
    ds.setAADSecurePrincipalId(principalId);
    ds.setAADSecurePrincipalSecret(principalSecret);

    // Optional
        ds.setUser("bob@cqclinic.onmicrosoft.com"); // Replace with your user name

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
You have successfully logged on as: <your user name>
```

> [!NOTE]
> A contained user database must exist and a contained database user representing the specified Azure AD user or one of the groups the specified Azure AD user belongs to, must exist in the database and must have the CONNECT permission (except for an Azure Active Directory server admin or group)

## Connecting using access token

Applications/services can retrieve an access token from the Azure Active Directory and use that to connect to Azure SQL Database/Synapse Analytics.

> [!NOTE]
> **accessToken** can only be set using the Properties parameter of the getConnection() method in the DriverManager class. It can't be used in the connection string.

The example below contains a simple Java application that connects to Azure SQL Database/Synapse Analytics using access token-based authentication. Before building and running the example, perform the following steps:

1. Create an application account in Azure Active Directory for your service.
    1. Sign in to the Azure portal.
    2. Click on Azure Active Directory in the left-hand navigation.
    3. Click the "App registrations" tab.
    4. In the drawer, click "New application registration".
    5. Enter mytokentest as a friendly name for the application, select "Web App/API".
    6. We don't need SIGN-ON URL. Just provide anything: "https://mytokentest".
    7. Click "Create" at the bottom.
    8. While still in the Azure portal, click the "Settings" tab of your application, and open the "Properties" tab.
    9. Find the "Application ID" (also known as Client ID) value and copy it aside, you need this later when configuring your application (for example, 1846943b-ad04-4808-aa13-4702d908b5c1). See the following snapshot.
    10. Under section "Keys", create a key by filling in the name field, selecting the duration of the key, and saving the configuration (leave the value field empty). After saving, the value field should be filled automatically, copy the generated value. This is the client Secret.
    11. Click Azure Active Directory on the left side panel. Under "App Registrations", find the "End points" tab. Copy the URL under "OATH 2.0 TOKEN ENDPOINT", this is your STS URL.

    ![Azure Portal App Registration End Point - STS URL](media/jdbc_aad_token.png)
2. Sign in to your Azure SQL Server's user database as an Azure Active Directory admin and using a T-SQL command
provision a contained database user for your application principal. For more information, see the [Connecting to SQL Database or Azure Synapse Analytics By Using Azure Active Directory authentication](/azure/azure-sql/database/authentication-aad-overview) for more details on how to create an Azure Active Directory admin and a contained database user.

    ```sql
    CREATE USER [mytokentest] FROM EXTERNAL PROVIDER
    ```

3. On the client machine (on which, you want to run the example), download the [Microsoft Authentication Library (MSAL) for Java](https://github.com/AzureAD/microsoft-authentication-library-for-java) library and its dependencies for JDBC Driver 9.1 and above, or [Microsoft Azure Active Directory Authentication Library (ADAL) for Java](https://github.com/AzureAD/azure-activedirectory-library-for-java) and its dependencies for driver versions prior to JDBC Driver 9.1, and include them in the Java build path. Note that the microsoft-authentication-library-for-java is only needed to run this specific example. The example uses the APIs from this library to retrieve the access token from Azure Azure AD. If you already have an access token, you can skip this step. Note that you also need to remove the section in the example that retrieves access token.

In the following example, replace the STS URL, Client ID, Client Secret, server and database name with your values.

```java
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

// The azure-activedirectory-library-for-java is needed to retrieve the access token from the AD.
import com.microsoft.aad.msal4j.ClientCredentialFactory;
import com.microsoft.aad.msal4j.ClientCredentialParameters;
import com.microsoft.aad.msal4j.ConfidentialClientApplication;
import com.microsoft.aad.msal4j.IAuthenticationResult;
import com.microsoft.aad.msal4j.IClientCredential;

public class AADTokenBased {

    public static void main(String[] args) throws Exception {

        // Retrieve the access token from the AD.
        String spn = "https://database.windows.net/";
        String stsurl = "https://login.microsoftonline.com/..."; // Replace with your STS URL.
        String clientId = "1846943b-ad04-4808-aa13-4702d908b5c1"; // Replace with your client ID.
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

        ds.setServerName("aad-managed-demo.database.windows.net"); // Replace with your server name.
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

```bash
Access Token: <your access token>
You have successfully logged on as: <your client ID>
```
