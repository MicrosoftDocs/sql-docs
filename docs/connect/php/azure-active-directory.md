---
title: "Azure Active Directory | Microsoft Docs"
ms.date: "07/13/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.custom: ""
ms.technology: connectivity
ms.topic: conceptual
author: "david-puglielli"
ms.author: "v-dapugl"
manager: "v-hakaka"
---
# Connect Using Azure Active Directory Authentication
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Azure Active Directory](https://docs.microsoft.com/azure/active-directory/active-directory-whatis) (Azure AD) is a central user ID management technology that operates as an alternative to [SQL Server authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md). Azure AD allows connections to Microsoft Azure SQL Database and SQL Data Warehouse with federated identities in Azure AD using a username and password, Windows Integrated Authentication, or an Azure AD access token; the PHP drivers for SQL Server offer partial support for these features.

To use Azure AD, use the **Authentication** keyword. The values that **Authentication** can take on are explained in the following table.

|Keyword|Values|Description|
|-|-|-|
|**Authentication**|Not set (default)|Authentication mode determined by other keywords. For more information, see [Connection Options](../../connect/php/connection-options.md). |
||`SqlPassword`|Directly authenticate to a SQL Server instance (which may be an Azure instance) using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |
||`ActiveDirectoryPassword`|Authenticate with an Azure Active Directory identity using a username and password. The username and password must be passed into the connection string using the **UID** and **PWD** keywords. |

The **Authentication** keyword affects the connection security settings. If it is set in the connection string, then by default the **Encrypt** keyword is set to true, so the client will request encryption. Moreover, the server certificate will be validated irrespective of the encryption setting unless **TrustServerCertificate** is set to true. This is distinguished from the old, and less secure, login method, in which the server certificate is not validated unless encryption is specifically requested in the connection string.

Before using Azure AD with the PHP drivers for SQL Server on Windows, ensure that you have installed the [Microsoft Online Services Sign-In Assistant](https://www.microsoft.com/download/details.aspx?id=41950) (not required for Linux and MacOS).

#### Limitations

On Windows, the underlying ODBC driver supports one more value for the **Authentication** keyword, **ActiveDirectoryIntegrated**, but the PHP drivers do not support this value on any platform and hence also do not support Azure AD token-based authentication.

## Example

The following example shows how to connect using **SqlPassword** and **ActiveDirectoryPassword**.

```php
    <?php
    // First connect to a local SQL Server instance by setting Authentication to SqlPassword
    $serverName = "myserver.mydomain";

    $connectionInfo = array( "UID"=>$myusername, "PWD"=>$mypassword, "Authentication"=>'SqlPassword' );

    $conn = sqlsrv_connect( $serverName, $connectionInfo );
    if( $conn === false )
    {
        echo "Could not connect with Authentication=SqlPassword.\n";
        print_r( sqlsrv_errors() );
    }
    else
    {
        echo "Connected successfully with Authentication=SqlPassword.\n";
        sqlsrv_close( $conn );
    }

    // Now connect to an Azure SQL database by setting Authentication to ActiveDirectoryPassword
    $azureServer = "myazureserver.database.windows.net";
    $azureDatabase = "myazuredatabase";
    $azureUsername = "myuid";
    $azurePassword = "mypassword";
    $connectionInfo = array( "Database"=>$azureDatabase, "UID"=>$azureUsername, "PWD"=>$azurePassword,
                             "Authentication"=>'ActiveDirectoryPassword' );

    $conn = sqlsrv_connect( $azureServer, $connectionInfo );
    if( $conn === false )
    {
        echo "Could not connect with Authentication=ActiveDirectoryPassword.\n";
        print_r( sqlsrv_errors() );
    }
    else
    {
        echo "Connected successfully with Authentication=ActiveDirectoryPassword.\n";
        sqlsrv_close( $conn );
    }

    ?>
```

The following example does the same as above with the PDO_SQLSRV driver.

```php
    <?php
    // First connect to a local SQL Server instance by setting Authentication to SqlPassword
    $serverName = "myserver.mydomain";

    $connectionInfo = "Database = $databaseName; Authentication = SqlPassword;";

    try
    {
        $conn = new PDO( "sqlsrv:server = $serverName ; $connectionInfo", $myusername, $mypassword );
        echo "Connected successfully with Authentication=SqlPassword.\n";
        $conn = null;
    }
    catch( PDOException $e )
    {
        echo "Could not connect with Authentication=SqlPassword.\n";
        print_r( $e->getMessage() );
        echo "\n";
    }

    // Now connect to an Azure SQL database by setting Authentication to ActiveDirectoryPassword
    $azureServer = "myazureserver.database.windows.net";
    $azureDatabase = "myazuredatabase";
    $azureUsername = "myuid";
    $azurePassword = "mypassword";
    $connectionInfo = "Database = $azureDatabase; Authentication = ActiveDirectoryPassword;";

    try
    {
        $conn = new PDO( "sqlsrv:server = $azureServer ; $connectionInfo", $azureUsername, $azurePassword );
        echo "Connected successfully with Authentication=ActiveDirectoryPassword.\n";
        $conn = null;
    }
    catch( PDOException $e )
    {
        echo "Could not connect with Authentication=ActiveDirectoryPassword.\n";
        print_r( $e->getMessage() );
        echo "\n";
    }

    ?>
```
## See Also  
[Using Azure Active Directory with the ODBC Driver](https://docs.microsoft.com/sql/connect/odbc/using-azure-active-directory)
