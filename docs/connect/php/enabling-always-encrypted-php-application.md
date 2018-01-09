---
title: "Enabling Always Encrypted in a PHP Application | Microsoft Docs"
ms.date: "01/08/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "php"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "drivers"
ms.topic: "article"
author: "yuki"
ms.author: "v-kaywon"
manager: "mbarwin"
ms.workload: "Inactive"
---
# Enabling Always Encrypted in a PHP Application
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

For encryption and decryption to work in a PHP application, the PHP driver needs the CEK (Column Encryption Key) specified in the definition of the encrypted column. The CEKs are stored in an encrypted form in the database metadata. To decrypt the CEKs, each corresponding CMK (Column Master Key) is needed. The keystore name and information of the CMK is stored in the database metadata. The CMK itself is stored on the client side. Thus in order to decrypt the CEK, the driver needs to get the metadata about the CEK and its corresponding CMK first, then uses this information to call the keystore provider, accesses the CMK, and finally decrypts the CEK.

To use the Always Encrypted features in the PHP driver, the user must first set up the Keystore Provider and the CMK. The PHP Drivers support two types of Keystore Providers:
 -   Windows Certificate Store
 -   Custom Keystore Providers
 
With Windows Certificate Store as the Keystore Provider, a certificate in the Certificate Store can be used as the CMK. One of the simplest option is to create a self-signed certificate and use it as the CMK (see [Creating Column Master Keys in Windows Certificate Store](https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted#creating-column-master-keys-in-windows-certificate-store)).

With Custom Keystore Providers, a Keystore Provider needs to be implemented first and then create a CMK (see [Custom Keystore Providers](https://docs.microsoft.com/en-us/sql/connect/odbc/custom-keystore-providers)).

To enable encryption of data passing into and decryption of data retrieved from the database, set the `ColumnEncryption` connection option to `Enabled`. The following are examples of connecting with Always Encrypted enabled using the SQLSRV and PDO_SQLSRV Drivers:

SQLSRV:
```
$connectionInfo = array( "Database"=>$databaseName, "UID"=>$uid, "PWD"=>$pwd, "ColumnEncryption"=>'Enabled' );
$conn = sqlsrv_connect( $server, $connectionInfo );
```

PDO_SQLSRV:
```
$connectionInfo = "Database = $databaseName; ColumnEncryption = Enabled;";
$conn = new PDO( "sqlsrv:server = $server ; $connectionInfo", $uid, $pwd );
```

If you are using Custom Keystore Provider, provide the connection options `CEKeystoreProvider`, `CEKeystoreName`, and `CEKeystoreEncryptKey`. The following are examples of connecting with Always Encrypted using Custom KeyStore Provider:

SQLSRV:
```
$connectionInfo = array( "Database"=>$databaseName, "UID"=>$uid, "PWD"=>$pwd, "ColumnEncryption"=>'Enabled', "CEKeystoreProvider"=>$ksp_path, "CEKeystoreName"=>$ksp_name, "CEKeystoreEncryptKey"=>$encrypt_key );
$conn = sqlsrv_connect( $server, $connectionInfo );
```

PDO_SQLSRV:
```
$connectionInfo = "Database = $databaseName; ColumnEncryption = Enabled; CEKeystoreProvider = $ksp_path; CEKeystoreName = $ksp_name; CEKeystoreEncryptKey = $encrypt_key;";
$conn = new PDO( "sqlsrv:server = $server ; $connectionInfo", $uid, $pwd );
```

Note that once Column Encryption is enabled in the connection, the performance overhead on the client side is heavier due to:
 -   Cost of encryption and decryption
 -   More round trips to the database needed to get the metadata for the parameter
 -   Result set with max types may need to be retrieved several times due to the limitation with fetching streams
 -   Assess of CEK and CMK metadata from the database
 -   Assess of the CMK from the column master key store
  
## See Also  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[sqlsrv_connect](../../connect/php/sqlsrv-connect.md)  
[PDO::__construct](../../connect/php/pdo-construct.md)
[connection-options[(../../connect/php/connection-options.md)
  
