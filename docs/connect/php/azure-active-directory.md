---
title: "Azure Active Directory | Microsoft Docs"
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.custom: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "azure active directory, authentication, access token"
author: "david-puglielli"
ms.author: "v-dapugl"
manager: "mbarwin"
---
# Connect Using Azure Active Directory Authentication
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Azure Active Directory](https://docs.microsoft.com/azure/active-directory/active-directory-whatis) (Azure AD) is a central user ID management technology that operates as an alternative to [SQL Server authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md). Azure AD allows connections to Microsoft Azure SQL Database and SQL Data Warehouse with federated identities in Azure AD, using username and password, Windows Integrated Authentication, or an Azure AD access token. The PHP drivers for SQL Server offer partial support for these features.

To use Azure AD, use the **Authentication** or **AccessToken** keywords (they are mutually exclusive), as shown in the following table. For more technical details, refer to [Using Azure Active Directory with the ODBC Driver](../../connect/odbc/using-azure-active-directory.md).

|Keyword|Values|Description|
|-|-|-|
|**AccessToken**|Not set (default)|Authentication mode determined by other keywords. For more information, see [Connection Options](../../connect/php/connection-options.md). |
||A byte string|The Azure AD Access Token extracted from an OAuth JSON response. The connection string must not contain user ID, password, or the Authentication keyword. |
|**Authentication**|Not set (default)|Authentication mode determined by other keywords. For more information, see [Connection Options](../../connect/php/connection-options.md). |
||`SqlPassword`|Directly authenticate to a SQL Server instance (which may be an Azure instance) using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |
||`ActiveDirectoryPassword`|Authenticate with an Azure Active Directory identity using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |
||`ActiveDirectoryMsi`|Authenticate using either a system-assigned managed identity or a user-assigned managed identity. For an overview and tutorials, refer to [What is managed identities for Azure resources?](https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview).|

The **Authentication** keyword affects the connection security settings. If it is set in the connection string, then by default the **Encrypt** keyword is set to true, which means the client will request encryption. Moreover, the server certificate will be validated irrespective of the encryption setting unless **TrustServerCertificate** is set to true (**false** by default). This feature is distinguished from the old, less secure login method, in which the server certificate is validated only when encryption is specifically requested in the connection string.

When using Azure AD with the PHP drivers for SQL Server on Windows, you may be asked to install the [Microsoft Online Services Sign-In Assistant](https://www.microsoft.com/download/details.aspx?id=41950) (not required for ODBC 17+).

#### Limitations

On Windows, the underlying ODBC driver supports one more value for the **Authentication** keyword, **ActiveDirectoryIntegrated**, but the PHP drivers do not support this value on any platform.

## Example
The following example shows how to connect using **SqlPassword** and **ActiveDirectoryPassword**.

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

## Example
The following example does the same as above with the PDO_SQLSRV driver.

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

## Example
The following examples show how to connect using Azure AD Access Token with the PDO_SQLSRV and SQLSRV drivers respectively.

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

## Example
The following examples show how to connect using managed identities for Azure resources

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

### Using the user-assigned managed identity with PDO_SQLSRV driver

A user-assigned managed identity is created as a standalone Azure resource. Through a 
create process, Azure creates an identity in the Azure AD tenant that's trusted by the
subscription in use. After the identity is created, the identity can be assigned to one or more Azure service instances. Copy the `Object ID` of this identity and set it as the user name in the connection string. 

Therefore, when connecting using the user-assigned managed identity, provide the Object ID as the user name but omit the password.

```php
<?php

$azureServer = 'myazureserver.database.windows.net';
$azureDatabase = 'myazuredatabase';
$azureUser = '2d68f56e-9547-4dae-aee8-f3g28ab9674x';    // Object ID of the identity
$connectionInfo = "Database = $azureDatabase; Authentication = ActiveDirectoryMsi;";

try {
    $conn = new PDO("sqlsrv:server = $azureServer; $connectionInfo", $azureUser);
    echo "Connected successfully with Authentication=ActiveDirectoryMsi (user-assigned).\n";
    
    $tsql = "SELECT @@Version AS SQL_VERSION";
    
    try {
        $stmt = $conn->query($tsql);
        $result = $stmt->fetchall(PDO::FETCH_ASSOC);
        print_r($result);

        unset($stmt);
    } catch (PDOException $e) {
        echo "Failed to run the simple query (user-assigned).\n";
    }
    unset($conn);
} catch (PDOException $e) {
    echo "Could not connect with Authentication=ActiveDirectoryMsi (user-assigned).\n";
    print_r($e->getMessage());
    echo "\n";
}
?>
```

## See Also
[Using Azure Active Directory with the ODBC Driver](https://docs.microsoft.com/sql/connect/odbc/using-azure-active-directory)

[What is managed identities for Azure resources?](https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview)