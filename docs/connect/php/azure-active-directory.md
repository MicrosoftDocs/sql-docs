---
title: Azure Active Directory
description: Learn how to use Azure Active Directory authentication with the Microsoft Drivers for PHP for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 12/14/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "azure active directory, authentication, access token"
---
# Connect Using Azure Active Directory Authentication

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Azure Active Directory](/azure/active-directory/active-directory-whatis) (Azure AD) is a central user ID management technology that operates as an alternative to [SQL Server authentication](how-to-connect-using-sql-server-authentication.md). Azure AD allows connections to Microsoft Azure SQL Database and Azure Synapse Analytics with federated identities in Azure AD using a username and password, Windows Integrated Authentication, or an Azure AD access token. The PHP drivers for SQL Server offer partial support for these features.

 You must configure your Azure SQL data source before you can use Azure AD authentication. For more information, see [Configure and manage Azure AD authentication with Azure SQL](/azure/azure-sql/database/authentication-aad-configure).

To use Azure AD, use the **Authentication** or **AccessToken** keywords (they are mutually exclusive), as shown in the following table. For more technical details, refer to [Using Azure Active Directory with the ODBC Driver](../odbc/using-azure-active-directory.md).

|Keyword|Values|Description|
|-|-|-|
|**AccessToken**|Not set (default)|Authentication mode determined by other keywords. For more information, see [Connection Options](connection-options.md). |
||A byte string|The Azure AD Access Token extracted from an OAuth JSON response. The connection string must not contain user ID, password, or the Authentication keyword (requires ODBC Driver version 17 or above in Linux or macOS). |
|**Authentication**|Not set (default)|Authentication mode determined by other keywords. For more information, see [Connection Options](connection-options.md). |
||`SqlPassword`|Directly authenticate to a SQL Server instance (which may be an Azure instance) using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |
||`ActiveDirectoryPassword`|Authenticate with an Azure Active Directory identity using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |
||`ActiveDirectoryMsi`|Authenticate using either a system-assigned managed identity or a user-assigned managed identity (requires ODBC Driver version 17.3.1.1 or above). For an overview and tutorials, refer to [What is managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview).|
||`ActiveDirectoryServicePrincipal`|Authenticate using service principal objects (requires ODBC Driver version 17.7 or above). For more details and examples, refer to [Application and service principal objects in Azure Active Directory](/azure/active-directory/develop/app-objects-and-service-principals).|

The **Authentication** keyword affects the connection security settings. If it is set in the connection string, then by default the **Encrypt** keyword is set to true, which means the client will request encryption. Moreover, the server certificate will be validated irrespective of the encryption setting unless **TrustServerCertificate** is set to true (**false** by default). This feature is distinguished from the old, less secure login method, in which the server certificate is validated only when encryption is specifically requested in the connection string.

#### Limitations

On Windows, the underlying ODBC driver supports one more value for the **Authentication** keyword, **ActiveDirectoryIntegrated**, but the PHP drivers do not support this value on any platform.

## Example - connect using SqlPassword and ActiveDirectoryPassword

```php
<?php
// First connect to a local SQL Server instance by setting Authentication to SqlPassword
$serverName = "myserver.mydomain";

$connectionInfo = array("UID"=>$myusername, "PWD"=>$mypassword, "Authentication"=>'SqlPassword');

$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
    echo "Could not connect with Authentication=SqlPassword.\n";
    print_r(sqlsrv_errors());
} else {
    echo "Connected successfully with Authentication=SqlPassword.\n";
    sqlsrv_close($conn);
}

// Now connect to an Azure SQL database by setting Authentication to ActiveDirectoryPassword
$azureServer = "myazureserver.database.windows.net";
$azureDatabase = "myazuredatabase";
$azureUsername = "myuid";
$azurePassword = "mypassword";
$connectionInfo = array("Database"=>$azureDatabase,
                        "UID"=>$azureUsername,
                        "PWD"=>$azurePassword,
                        "Authentication"=>'ActiveDirectoryPassword');

$conn = sqlsrv_connect($azureServer, $connectionInfo);
if ($conn === false) {
    echo "Could not connect with Authentication=ActiveDirectoryPassword.\n";
    print_r(sqlsrv_errors());
} else {
    echo "Connected successfully with Authentication=ActiveDirectoryPassword.\n";
    sqlsrv_close($conn);
}

?>
```

## Example - connect using the PDO_SQLSRV driver

```php
<?php
// First connect to a local SQL Server instance by setting Authentication to SqlPassword
$serverName = "myserver.mydomain";

$connectionInfo = "Database = $databaseName; Authentication = SqlPassword;";

try {
    $conn = new PDO("sqlsrv:server = $serverName ; $connectionInfo", $myusername, $mypassword);
    echo "Connected successfully with Authentication=SqlPassword.\n";
    $conn = null;
} catch (PDOException $e) {
    echo "Could not connect with Authentication=SqlPassword.\n";
    print_r($e->getMessage());
    echo "\n";
}

// Now connect to an Azure SQL database by setting Authentication to ActiveDirectoryPassword
$azureServer = "myazureserver.database.windows.net";
$azureDatabase = "myazuredatabase";
$azureUsername = "myuid";
$azurePassword = "mypassword";
$connectionInfo = "Database = $azureDatabase; Authentication = ActiveDirectoryPassword;";

try {
    $conn = new PDO("sqlsrv:server = $azureServer ; $connectionInfo", $azureUsername, $azurePassword);
    echo "Connected successfully with Authentication=ActiveDirectoryPassword.\n";
    unset($conn);
} catch (PDOException $e) {
    echo "Could not connect with Authentication=ActiveDirectoryPassword.\n";
    print_r($e->getMessage());
    echo "\n";
}
?>
```

## Example - connect using Azure AD Access Token

### SQLSRV driver

```php
<?php
// Using an access token to connect: do not use UID or PWD connection options
// Assume $accToken is the valid byte string extracted from an OAuth JSON response
$connectionInfo = array("Database"=>$azureAdDatabase, "AccessToken"=>$accToken);
$conn = sqlsrv_connect($azureAdServer, $connectionInfo);
if ($conn === false) {
    echo "Could not connect with Azure AD Access Token.\n";
    print_r(sqlsrv_errors());
} else {
    echo "Connected successfully with Azure AD Access Token.\n";
    sqlsrv_close($conn);
}
?>
```

### PDO_SQLSRV driver

```php
<?php
try {
    // Using an access token to connect: do not pass in $uid or $pwd
    // Assume $accToken is the valid byte string extracted from an OAuth JSON response
    $connectionInfo = "Database = $azureAdDatabase; AccessToken = $accToken;";
    $conn = new PDO("sqlsrv:server = $azureAdServer; $connectionInfo");
    echo "Connected successfully with Azure AD Access Token\n";
    unset($conn);
} catch (PDOException $e) {
    echo "Could not connect with Azure AD Access Token.\n";
    print_r($e->getMessage());
    echo "\n";
}
?>
```

## Example - connect using managed identities for Azure resources

### Using the system-assigned managed identity with SQLSRV driver

When connecting using the system-assigned managed identity, do not use the UID or PWD options.

```php
<?php

$azureServer = 'myazureserver.database.windows.net';
$azureDatabase = 'myazuredatabase';
$connectionInfo = array('Database'=>$azureDatabase,
                        'Authentication'=>'ActiveDirectoryMsi');
$conn = sqlsrv_connect($azureServer, $connectionInfo);

if ($conn === false) {
    echo "Could not connect with Authentication=ActiveDirectoryMsi (system-assigned).\n";
    print_r(sqlsrv_errors());
} else {
    echo "Connected successfully with Authentication=ActiveDirectoryMsi (system-assigned).\n";
    
    $tsql = "SELECT @@Version AS SQL_VERSION";
    $stmt = sqlsrv_query($conn, $tsql);
    if ($stmt === false) {
        echo "Failed to run the simple query (system-assigned).\n";
        print_r(sqlsrv_errors());
    } else {
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            echo $row['SQL_VERSION'] . PHP_EOL;
        }

        sqlsrv_free_stmt($stmt);
    }
    
    sqlsrv_close($conn);
}
?>
```

## Example - connect using service principal objects in Azure Active Directory

To authenticate using a service principal object, you will need the corresponding [application client ID](/azure/active-directory/develop/howto-create-service-principal-portal#get-tenant-and-app-id-values-for-signing-in) and the [client secret](/azure/active-directory/develop/howto-create-service-principal-portal#option-2-create-a-new-application-secret).


### SQLSRV driver

```php
<?php

$adServer = 'myazureserver.database.windows.net';
$adDatabase = 'myazuredatabase';
$adSPClientId = 'myAppClientId';
$adSPClientSecret = 'myClientSecret';

$conn = false;
$connectionInfo = array("Database"=>$adDatabase, 
                        "Authentication"=>"ActiveDirectoryServicePrincipal",
                        "UID"=>$adSPClientId,
                        "PWD"=>$adSPClientSecret);

$conn = sqlsrv_connect($adServer, $connectionInfo);
if ($conn === false) {
    echo "Could not connect using Azure AD Service Principal." . PHP_EOL;
    print_r(sqlsrv_errors());
}

sqlsrv_close($conn);

?>
```

### PDO_SQLSRV driver

```php
<?php

$adServer = 'myazureserver.database.windows.net';
$adDatabase = 'myazuredatabase';
$adSPClientId = 'myAppClientId';
$adSPClientSecret = 'myClientSecret';

$conn = false;
try {
    $connectionInfo = "Database = $adDatabase; Authentication = ActiveDirectoryServicePrincipal;";
    $conn = new PDO("sqlsrv:server = $adServer; $connectionInfo", $adSPClientId, $adSPClientSecret);
} catch (PDOException $e) {
    echo "Could not connect using Azure AD Service Principal.\n";
    print_r($e->getMessage());
    echo PHP_EOL;
}

unset($conn);
?>
```


## See Also
[Using Azure Active Directory with the ODBC Driver](../odbc/using-azure-active-directory.md)

[What is managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview)
