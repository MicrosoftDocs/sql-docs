---
title: "Azure Active Directory | Microsoft Docs"
ms.custom: ""
ms.date: "07/10/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology:
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords:
  - ""
ms.assetid:
caps.latest.revision: 1
author: ""
ms.author: ""
manager: ""
---
# Azure Active Directory
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[Azure Active Directory](https://docs.microsoft.com/en-us/azure/active-directory/active-directory-whatis) (Azure AD) is a central user ID management technology that operates as an alternative to [SQL Server authentication](../../connect/php/how-to-connect-using-sql-server-authentication.md). Azure AD allows connections to Microsoft Azure SQL Database and SQL Data Warehouse with federated identities in Azure AD using a username and password, Windows Integrated Authentication, or an Azure AD access token; the PHP drivers for SQL Server offer partial support for these features.

To use Azure AD, use the **Authentication** keyword. This keyword can take on two values: **SqlPassword** and **ActiveDirectoryPassword**. To connect to a SQL Server instance (which may be an Azure instance), set **Authentication** to **SqlPassword**. To connect to an Azure SQL database using an Azure AD account username and password, set **Authentication** to **ActiveDirectoryPassword** in the connection string.

When using the **Authentication** keyword, the username and password must be passed into the connection string using the **UID** and **PWD** keywords. By default, the **Encrypt** keyword is set to true, so the client will request encryption; moreover, the server certificate will be validated irrespective of the encryption setting unless **TrustServerCertificate** is set to true. This is distinguished from the old, and less secure, login method, in which the server certificate is not validated unless encryption is specifically requested in the connection string.

Before using Azure AD with the PHP drivers for SQL Server, ensure that you have installed the [Active Directory Authentication Library for SQL Server](https://www.microsoft.com/en-us/download/details.aspx?id=48742) and the [Microsoft Online Services Sign-In Assistant](https://www.microsoft.com/en-ca/download/details.aspx?id=41950).

#### Limitations

On Windows, the underlying ODBC driver supports a third value for the **Authentication** keyword, **ActiveDirectoryIntegrated**, but the PHP drivers do not support this value on any platform and hence also do not support Azure AD token-based authentication.

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
[Using Azure Active Directory with the ODBC Driver](https://docs.microsoft.com/en-us/sql/connect/odbc/using-azure-active-directory)
