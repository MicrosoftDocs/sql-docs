---
title: "Always Encrypted with secure enclaves with the PHP Drivers"
description: "Learn how to use Always Encrypted with secure enclaves with the Microsoft Drivers for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: 01/31/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Always Encrypted with secure enclaves with the PHP Drivers for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

## Applicable to

- Microsoft Drivers 5.8.0 for PHP for SQL Server

## Introduction

[Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) is the second iteration of the Always Encrypted feature for SQL Server. Always Encrypted with secure enclaves allows users to perform rich computations against encrypted data by creating a secure enclave - a region of memory on the server where encrypted data in a database is decrypted so that computations may be performed. The supported operations include comparison and pattern matching with the `LIKE` clause.

## Enabling Always Encrypted with secure enclaves

Support for Always Encrypted with secure enclaves is available in the PHP Drivers for SQL Server starting with 5.8.0. Always Encrypted with secure enclaves requires SQL Server 2019 or later and version 17.4+ of the ODBC driver. Further details on general requirements for Always Encrypted with the PHP Drivers for SQL Server are available [here](using-always-encrypted-php-drivers.md).

Always Encrypted with secure enclaves ensures the security of encrypted data by attesting the enclave - that is, verifying the enclave against an external attestation service. To use secure enclaves, the `ColumnEncryption` keyword must identify the attestation type and protocol along with associated attestation data, separated by a comma. Version 17.4 of the ODBC driver supports only Virtualization-Based Security (VBS) and the Host Guardian Service (HGS) protocol for the enclave type and protocol. The associated attestation data is the URL of the attestation server. Thus, the following setting would be added to the connection string:

```
ColumnEncryption=VBS-HGS,http://attestationserver.mydomain/Attestation
```

If the protocol is incorrect, the driver will not recognize it, connection will fail, and an error will be returned. If only the attestation URL is incorrect, connection will succeed and an error will be thrown when an enclave-enabled computation is attempted, but otherwise the behavior will be identical to the original Always Encrypted behavior. Setting `ColumnEncryption` to `enabled` will provide regular Always Encrypted functionality, but attempting an enclave-enabled operation will return an error.

Full details for configuring your environment to support Always Encrypted with secure enclaves, including setting up the Host Guardian Service and creating the required encryption keys, can be found [here](../../relational-databases/security/encryption/configure-always-encrypted-enclaves.md).

## Examples

The following examples, one for SQLSRV and one for PDO_SQLSRV, create a table with several data types in plaintext, then encrypt it and carry out comparisons and pattern matching. Note the following details:

- When encrypting a table with `ALTER TABLE`, only one column may be encrypted for each call to `ALTER TABLE`, so multiple calls are required to encrypt multiple columns.
- When passing the comparison threshold as a parameter for comparing char and nchar types, the column width must be specified in the corresponding `SQLSRV_SQLTYPE_*`, or the error `HY104`, `Invalid precision value`, will be returned.
- For pattern matching, the collation must be specified as `Latin1_General_BIN2` using the `COLLATE` clause.
- When passing the pattern matching string as a parameter for matching char and nchar types, the `SQLSRV_SQLTYPE_*` passed to `sqlsrv_query` or `sqlsrv_prepare` should specify the length of the string to be matched and not the size of the column because char and nchar types pad whitespace on the end of the string. For example, when matching the string `%abc%` against a char(10) column, specify `SQLSRV_SQLTYPE_CHAR(5)`. If you instead specify `SQLSRV_SQLTYPE_CHAR(10)`, the query will match `%abc%     ` (with five spaces appended), and any data in the column with fewer than five spaces appended will not match (so `abcdef` would not match `%abc%` because it has four spaces of padding). For Unicode strings, use the `mb_strlen` or `iconv_strlen` functions to get the number of characters.
- The PDO interface does not allow specifying the length of a parameter. Instead, specify a length of 0 or `null` in `PDOStatement::bindParam`. If the length is explicitly set to another number, the parameter is treated as an output parameter.
- Pattern matching does not work against non-string types in Always Encrypted.
- Error checking is excluded for clarity.

The following data is common for both examples:

```php
<?php
// Data for testing - integer, datetime2, char, nchar, varchar, and nvarchar
// String data is random, showing that we can match or compare anything
$testValues = array(array(1, "2019-12-31 01:00:00", "abcd", "㬚㔈♠既", "abcd", "㬚㔈♠既"),
                    array(-100, "1753-01-31 14:25:25.25", "#e@?q&zy+", "ઔܛ᎓Ե⅜", "#e@?q&zy+", "ઔܛ᎓Ե⅜"),
                    array(100, "2112-03-15 23:40:10.1594", "zyxwv", "㶋㘚ᐋꗡ", "zyxwv", "㶋㘚ᐋꗡ"),
                    array(0, "8888-08-08 08:08:08.08", "7t", "㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ", "7t", "㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ"),
                    );

// Queries to create the table and insert data
$createTable = "DROP TABLE IF EXISTS $myTable; 
                CREATE TABLE $myTable (c_integer int NULL, 
                                       c_datetime2 datetime2(7) NULL, 
                                       c_char char(32) NULL, 
                                       c_nchar nchar(32) NULL, 
                                       c_varchar varchar(32) NULL, 
                                       c_nvarchar nvarchar(32) NULL);";
$insertData = "INSERT INTO $myTable (c_integer, c_datetime2, c_char, c_nchar, c_varchar, c_nvarchar) VALUES (?, ?, ?, ?, ?, ?)";

// This is the query that encrypts the table in place
$encryptQuery = " ALTER TABLE $myTable
                      ALTER COLUMN [c_integer] integer
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER TABLE $myTable
                      ALTER COLUMN [c_datetime2] datetime2(7)
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER TABLE $myTable
                      ALTER COLUMN [c_char] char(32) COLLATE Latin1_General_BIN2
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER TABLE $myTable
                      ALTER COLUMN [c_nchar] nchar(32) COLLATE Latin1_General_BIN2
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER TABLE $myTable
                      ALTER COLUMN [c_varchar] varchar(32) COLLATE Latin1_General_BIN2
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER TABLE $myTable
                      ALTER COLUMN [c_nvarchar] nvarchar(32) COLLATE Latin1_General_BIN2
                      ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK-enclave], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
                      WITH (ONLINE = ON);
                  ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;";
?>
```

### SQLSRV

```php
<?php
// Specify Azure Key Vault credentials using the KeyStoreAuthentication, KeyStorePrincipalId, and KeyStoreSecret keywords
// Otherwise, the local Windows Certificate Store will be used
$options = array('database'=>$myDatabase,
                 'uid'=>$myUsername,
                 'pwd'=>$myPassword,
                 'CharacterSet'=>'UTF-8',
                 'ReturnDatesAsStrings'=>true,
                 'ColumnEncryption'=>"VBS-HGS,http://myattestationserver.mydomain/Attestation",
                 );
                 
$conn = sqlsrv_connect($myServer, $options);

// Create the table and insert the test data
$stmt = sqlsrv_query($conn, $createTable);

foreach ($testValues as $values) {
    $stmt = sqlsrv_prepare($conn, $insertData, $values);
    sqlsrv_execute($stmt);
}

// Encrypt the table in place
$stmt = sqlsrv_query($conn, $encryptQuery);

// Test comparison and pattern matching on the encrypted table
echo "Test comparisons:\n";

$intThreshold = 0;
$testGreater = "SELECT c_integer FROM $myTable WHERE c_integer > ?";
$param = array($intThreshold, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_INT);
$stmt = sqlsrv_prepare($conn, $testGreater, array($param));
getResults($stmt);
// Expect:
// 1
// 100

$datetimeThreshold = "3000-01-01 00:00:00.0";
$testLess = "SELECT c_datetime2 FROM $myTable WHERE c_datetime2 < ?";
$param = array($datetimeThreshold, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_DATETIME2);
$stmt = sqlsrv_prepare($conn, $testLess, array($param));
getResults($stmt);
// Expect:
// 2019-12-31 01:00:00.0000000
// 1753-01-31 14:25:25.2500000
// 2112-03-15 23:40:10.1594000

$charThreshold = "abcd";
$ncharThreshold = "㬚㔈♠既";

$testGreaterEqual = "SELECT c_char FROM $myTable WHERE c_char >= ?";
$param = array($charThreshold, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_CHAR(32));
$stmt = sqlsrv_prepare($conn, $testGreaterEqual, array($param));
getResults($stmt);
// Expect:
// abcd                            
// zyxwv                           

$testLessEqual = "SELECT c_nchar FROM $myTable WHERE c_nchar <= ?";
$param = array($ncharThreshold, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING('UTF-8'), SQLSRV_SQLTYPE_NCHAR(32));
$stmt = sqlsrv_prepare($conn, $testLessEqual, array($param));
getResults($stmt);
// Expect:
// 㬚㔈♠既                            
// ઔܛ᎓Ե⅜                           
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    

$testNotGreater = "SELECT c_varchar FROM $myTable WHERE c_varchar !> ?";
$param = array($charThreshold, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_VARCHAR);
$stmt = sqlsrv_prepare($conn, $testNotGreater, array($param));
getResults($stmt);
// Expect:
// abcd
// #e@?q&zy+
// 7t

$testNotLess = "SELECT c_nvarchar FROM $myTable WHERE c_nvarchar !< ?";
$param = array($ncharThreshold, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING('UTF-8'), SQLSRV_SQLTYPE_NVARCHAR);
$stmt = sqlsrv_prepare($conn, $testNotLess, array($param));
getResults($stmt);
// Expect:
// 㬚㔈♠既
// 㶋㘚ᐋꗡ

echo "\nTest pattern matching:\n";

$charMatch = "%zy%";
$ncharMatch = "%㔈♠既%";

$param = array($charMatch, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_CHAR(strlen($charMatch)));
$testCharMatch = "SELECT c_char FROM $myTable WHERE c_char LIKE ? COLLATE Latin1_General_BIN2";
$stmt = sqlsrv_prepare($conn, $testCharMatch, array($param));
getResults($stmt);
// Expect:
// #e@?q&zy+                       
// zyxwv                           

$param = array($ncharMatch, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING("UTF-8"), SQLSRV_SQLTYPE_NCHAR(iconv_strlen($ncharMatch)));
$testNCharMatch = "SELECT c_nchar FROM $myTable WHERE c_nchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = sqlsrv_prepare($conn, $testNCharMatch, array($param));
getResults($stmt);
// Expect:
// 㬚㔈♠既                            
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    

$param = array($charMatch, SQLSRV_PARAM_IN, null, SQLSRV_SQLTYPE_VARCHAR(strlen($charMatch)));
$testVarcharMatch = "SELECT c_varchar FROM $myTable WHERE c_varchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = sqlsrv_prepare($conn, $testVarcharMatch, array($param));
getResults($stmt);
// Expect:
// #e@?q&zy+
// zyxwv

$param = array($ncharMatch, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING("UTF-8"), SQLSRV_SQLTYPE_NVARCHAR(iconv_strlen($ncharMatch)));
$testNVarcharMatch = "SELECT c_nvarchar FROM $myTable WHERE c_nvarchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = sqlsrv_prepare($conn, $testNVarcharMatch, array($param));
getResults($stmt);
// Expect:
// 㬚㔈♠既
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ

function getResults($stmt)
{
    sqlsrv_execute($stmt);
    while ($res = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_NUMERIC)) {
        print_r($res[0]);
        echo "\n";
    }
}
?>
```

### PDO_SQLSRV

```php
<?php
// Specify Azure Key Vault credentials using the KeyStoreAuthentication, KeyStorePrincipalId, and KeyStoreSecret keywords
// Otherwise, the local Windows Certificate Store will be used
$options = "sqlsrv:server=$myServer;database=$myDatabase;driver={ODBC Driver 18 for SQL Server};";
$options .= "ColumnEncryption=VBS-HGS,http://myattestationserver.mydomain/Attestation",

$conn = new PDO($options, $myUsername, $myPassword);

// Create the table and insert the test data
$stmt = $conn->query($createTable);

foreach ($testValues as $values) {
    $stmt = $conn->prepare($insertData);
    $stmt->execute($values);
}

// Encrypt the table in place
$stmt = $conn->query($encryptQuery);

// Test comparison and pattern matching on the encrypted table
echo "Test comparisons:\n";

$intThreshold = 0;
$testGreater = "SELECT c_integer FROM $myTable WHERE c_integer > ?";
$stmt = $conn->prepare($testGreater);
$stmt->bindParam(1, $intThreshold, PDO::PARAM_INT);
getResults($stmt);
// Expect:
// 1
// 100

$datetimeThreshold = "3000-01-01 00:00:00.0";
$testLess = "SELECT c_datetime2 FROM $myTable WHERE c_datetime2 < ?";
$stmt = $conn->prepare($testLess);
$stmt->bindParam(1, $datetimeThreshold, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// 2019-12-31 01:00:00.0000000
// 1753-01-31 14:25:25.2500000
// 2112-03-15 23:40:10.1594000

$charThreshold = "abcd";
$ncharThreshold = "㬚㔈♠既";

$testGreaterEqual = "SELECT c_char FROM $myTable WHERE c_char >= ?";
$stmt = $conn->prepare($testGreaterEqual);
$stmt->bindParam(1, $charThreshold, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// abcd                            
// zyxwv                           

$testLessEqual = "SELECT c_nchar FROM $myTable WHERE c_nchar <= ?";
$stmt = $conn->prepare($testLessEqual);
$stmt->bindParam(1, $ncharThreshold, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// 㬚㔈♠既                            
// ઔܛ᎓Ե⅜                           
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    

$testNotGreater = "SELECT c_varchar FROM $myTable WHERE c_varchar !> ?";
$stmt = $conn->prepare($testNotGreater);
$stmt->bindParam(1, $charThreshold, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// abcd
// #e@?q&zy+
// 7t

$testNotLess = "SELECT c_nvarchar FROM $myTable WHERE c_nvarchar !< ?";
$stmt = $conn->prepare($testNotLess);
$stmt->bindParam(1, $ncharThreshold, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// 㬚㔈♠既
// 㶋㘚ᐋꗡ

echo "\nTest pattern matching:\n";

$charMatch = "%zy%";
$ncharMatch = "%㔈♠既%";

$testCharMatch = "SELECT c_char FROM $myTable WHERE c_char LIKE ? COLLATE Latin1_General_BIN2";
$stmt = $conn->prepare($testCharMatch);
$stmt->bindParam(1, $charMatch, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// #e@?q&zy+                       
// zyxwv                           

$testNCharMatch = "SELECT c_nchar FROM $myTable WHERE c_nchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = $conn->prepare($testNCharMatch);
$stmt->bindParam(1, $ncharMatch, PDO::PARAM_STR,null,PDO::SQLSRV_ENCODING_UTF8);
getResults($stmt);
// Expect:
// 㬚㔈♠既                            
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    

$testVarcharMatch = "SELECT c_varchar FROM $myTable WHERE c_varchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = $conn->prepare($testVarcharMatch);
$stmt->bindParam(1, $charMatch, PDO::PARAM_STR);
getResults($stmt);
// Expect:
// #e@?q&zy+
// zyxwv

$testNVarcharMatch = "SELECT c_nvarchar FROM $myTable WHERE c_nvarchar LIKE ? COLLATE Latin1_General_BIN2";
$stmt = $conn->prepare($testNVarcharMatch);
$stmt->bindParam(1, $ncharMatch, PDO::PARAM_STR,null,PDO::SQLSRV_ENCODING_UTF8);
getResults($stmt);
// Expect:
// 㬚㔈♠既
// 㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ

function getResults($stmt)
{
    $stmt->execute();
    while($res = $stmt->fetch(PDO::FETCH_NUM)) {
        print_r($res[0]);
        echo "\n";
    }
}
?>
```

Output:

```
Test comparisons:
1
100
2019-12-31 01:00:00.0000000
1753-01-31 14:25:25.2500000
2112-03-15 23:40:10.1594000
abcd                            
zyxwv                           
㬚㔈♠既                            
ઔܛ᎓Ե⅜                           
㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    
abcd
#e@?q&zy+
7t
㬚㔈♠既
㶋㘚ᐋꗡ

Test pattern matching:
#e@?q&zy+                       
zyxwv                           
㬚㔈♠既                            
㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ                    
#e@?q&zy+
zyxwv
㬚㔈♠既
㛜ꆶ㕸㔈♠既ꁺꖁ㓫ޘ갧ᛄ
```

## See Also

[Programming Guide for PHP SQL Driver](programming-guide-for-php-sql-driver.md)  
[SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)  
[PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)  
[Using Always Encrypted with the PHP Drivers for SQL Server](using-always-encrypted-php-drivers.md)
