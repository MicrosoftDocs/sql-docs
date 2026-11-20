---
title: "How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support"
description: "Learn how to send and retrieve UTF-8 encoded data using UTF-8 support built in to the drivers for PHP."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "retrieving data, UTF-8 encoded data"
  - "converting data types"
  - "updating data"
---
# How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

If you are using the PDO_SQLSRV driver, you can specify the encoding with the PDO::SQLSRV_ATTR_ENCODING attribute. For more information, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).  
  
The rest of this article discusses encoding by using the SQLSRV driver, except for [Send UTF-8 strings as varchar parameters](#send-utf-8-strings-as-varchar-parameters), which covers both drivers.
  
To send or retrieve UTF-8 encoded data to the server:  
  
1.  Ensure that the source or destination column is of type **nchar** or **nvarchar**. For **varchar** columns, see [Send UTF-8 strings as varchar parameters](#send-utf-8-strings-as-varchar-parameters).
  
2.  Specify the PHP type as `SQLSRV_PHPTYPE_STRING('UTF-8')` in the parameters array. Or, specify `"CharacterSet" => "UTF-8"` as a connection option.  
  
    When you specify a character set as part of the connection options, the driver assumes that the other connection option strings use that same character set. The server name and query strings are also assumed to use the same character set.  
  
You can pass UTF-8, `SQLSRV_ENC_UTF8_VARCHAR`, or `SQLSRV_ENC_CHAR` to **CharacterSet**, but you can't pass `SQLSRV_ENC_BINARY`. The default encoding is `SQLSRV_ENC_CHAR`. `SQLSRV_ENC_UTF8_VARCHAR` requires version 5.14.0 or later.
  
## Connection example  
The following example demonstrates how to send and retrieve UTF-8 encoded data by specifying the UTF-8 character set when making the connection. The example updates the Comments column of the Production.ProductReview table for a specified review ID. The example also retrieves the newly updated data and displays it. Note that the Comments column is of type **nvarchar(3850).** Also note that before data is sent to the server it is converted to UTF-8 encoding using the PHP **utf8_encode** function. This is done for demonstration purposes only. In a real application scenario, you would begin with UTF-8 encoded data.  
  
The example assumes that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the browser when the example is run from the browser.  
  
```php  
<?php  
  
// Connect to the local server using Windows Authentication and  
// specify the AdventureWorks database as the database in use.   
//   
$serverName = "<server>";  
$connectionInfo = array( "Database"=>"AdventureWorks", "CharacterSet" => "UTF-8");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Set up the Transact-SQL query.  
//   
$tsql1 = "UPDATE Production.ProductReview  
          SET Comments = ?  
          WHERE ProductReviewID = ?";  
  
// Set the parameter values and put them in an array. Note that  
// $comments is converted to UTF-8 encoding with the PHP function  
// utf8_encode to simulate an application that uses UTF-8 encoded data.   
//   
$reviewID = 3;  
$comments = utf8_encode("testing 1, 2, 3, 4.  Testing.");  
$params1 = array(  
                  array( $comments, null ),  
                  array( $reviewID, null )  
                );  
  
// Execute the query.  
//   
$stmt1 = sqlsrv_query($conn, $tsql1, $params1);  
  
if ( $stmt1 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
else {  
   echo "The update was successfully executed.<br>";  
}  
  
// Retrieve the newly updated data.  
//   
$tsql2 = "SELECT Comments   
          FROM Production.ProductReview   
          WHERE ProductReviewID = ?";  
  
// Set up the parameter array.  
//   
$params2 = array($reviewID);  
  
// Execute the query.  
//   
$stmt2 = sqlsrv_query($conn, $tsql2, $params2);  
if ( $stmt2 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Retrieve and display the data.   
//   
if ( sqlsrv_fetch($stmt2) ) {  
   echo "Comments: ";  
   $data = sqlsrv_get_field( $stmt2, 0 );  
   echo $data."<br>";  
}  
else {  
   echo "Error in fetching data.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Free statement and connection resources.  
//   
sqlsrv_free_stmt( $stmt1 );  
sqlsrv_free_stmt( $stmt2 );  
sqlsrv_close( $conn);  
?>  
```  
  
For information about storing Unicode data, see [Working with Unicode Data](/previous-versions/sql/sql-server-2008-r2/ms175180(v=sql.105)).  
  
## Column example  
The following example is similar to the first sample but instead of specifying the UTF-8 character set on the connection, this sample shows how to specify the UTF-8 character set on the column.  
  
```php  
<?php  
  
// Connect to the local server using Windows Authentication and  
// specify the AdventureWorks database as the database in use.   
//   
$serverName = "<server>";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Set up the Transact-SQL query.  
//   
$tsql1 = "UPDATE Production.ProductReview  
          SET Comments = ?  
          WHERE ProductReviewID = ?";  
  
// Set the parameter values and put them in an array. Note that  
// $comments is converted to UTF-8 encoding with the PHP function  
// utf8_encode to simulate an application that uses UTF-8 encoded data.   
//   
$reviewID = 3;  
$comments = utf8_encode("testing");  
$params1 = array(  
                  array($comments,  
                        SQLSRV_PARAM_IN,  
                        SQLSRV_PHPTYPE_STRING('UTF-8')  
                  ),  
                  array($reviewID)  
                );  
  
// Execute the query.  
//   
$stmt1 = sqlsrv_query($conn, $tsql1, $params1);  
  
if ( $stmt1 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
else {  
   echo "The update was successfully executed.<br>";  
}  
  
// Retrieve the newly updated data.  
//   
$tsql2 = "SELECT Comments   
          FROM Production.ProductReview   
          WHERE ProductReviewID = ?";  
  
// Set up the parameter array.  
//   
$params2 = array($reviewID);  
  
// Execute the query.  
//   
$stmt2 = sqlsrv_query($conn, $tsql2, $params2);  
if ( $stmt2 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Retrieve and display the data.   
//   
if ( sqlsrv_fetch($stmt2) ) {  
   echo "Comments: ";  
   $data = sqlsrv_get_field($stmt2,   
                            0,   
                            SQLSRV_PHPTYPE_STRING('UTF-8')  
                           );  
   echo $data."<br>";  
}  
else {  
   echo "Error in fetching data.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Free statement and connection resources.  
//   
sqlsrv_free_stmt( $stmt1 );  
sqlsrv_free_stmt( $stmt2 );  
sqlsrv_close( $conn);  
?>  
```  

## Send UTF-8 strings as varchar parameters

With UTF-8 encoding, both drivers send a string parameter as **nvarchar**. Starting with version 5.14.0, you can send UTF-8 strings as **varchar** parameters instead. Use this option when your columns are **varchar**. When SQL Server compares a **varchar** column to an **nvarchar** parameter, it converts the column value, which can prevent an index seek.

Each driver has its own constant. Don't use the SQLSRV constant with PDO_SQLSRV, or the PDO_SQLSRV constant with SQLSRV.

| Driver | Setting | Scope |
| --- | --- | --- |
| SQLSRV | `"CharacterSet" => SQLSRV_ENC_UTF8_VARCHAR` in the [connection options](connection-options.md) | Every string parameter on the connection |
| SQLSRV | `SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_UTF8_VARCHAR)` in the parameter array | One parameter |
| PDO_SQLSRV | `PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8_VARCHAR` with [PDO::setAttribute](pdo-setattribute.md) | Every string parameter on the connection |
| PDO_SQLSRV | `PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8_VARCHAR` in the [PDO::prepare](pdo-prepare.md) options | One statement |
| PDO_SQLSRV | `PDO::SQLSRV_ENCODING_UTF8_VARCHAR` as the fifth argument of [PDOStatement::bindParam](pdostatement-bindparam.md) | One parameter |

The setting changes only string parameters that don't have an explicit SQL type. The following rules apply:

- An explicit SQL type wins. For example, `SQLSRV_SQLTYPE_NVARCHAR(20)` in a SQLSRV parameter array, or an **nvarchar** declaration from [Declare input string types and lengths (PDO_SQLSRV)](pdo-input-string-declarations.md), still sends **nvarchar**.
- With [Always Encrypted](using-always-encrypted-php-drivers.md), the parameter type from the server's encryption metadata wins.
- A per-parameter UTF-8 encoding, `SQLSRV_PHPTYPE_STRING('UTF-8')` or `PDO::SQLSRV_ENCODING_UTF8`, sends that parameter as **nvarchar**.
- Strings that you read are returned in UTF-8, including from **nchar** and **nvarchar** columns.

SQL Server converts a **varchar** parameter to the code page of the database's default collation. To send characters outside that code page without loss, the database's default collation must be a UTF-8 collation, such as `Latin1_General_100_CI_AS_SC_UTF8`, and the destination column must be able to store the characters. A UTF-8 collation on the column alone isn't enough. Otherwise, SQL Server replaces characters that the code page can't represent, typically with a question mark (`?`). For more information, see [Collation and Unicode support](../../relational-databases/collations/collation-and-unicode-support.md#utf8).

With PDO emulated prepares (`PDO::ATTR_EMULATE_PREPARES`), PDO_SQLSRV places string values in the query text as literals. By default, UTF-8 varchar encoding produces ordinary character literals, which also depend on the database's default collation. A parameter bound with `PDO::PARAM_STR | PDO::PARAM_STR_NATL`, a connection with `PDO::ATTR_DEFAULT_STR_PARAM` set to `PDO::PARAM_STR_NATL`, or a per-parameter `PDO::SQLSRV_ENCODING_UTF8` produces Unicode `N'...'` literals instead.

### SQLSRV example

The following example sets **CharacterSet** on the connection. It reads connection settings from environment variables and escapes the database name, username, and password as described in [Connecting to the server](connecting-to-the-server.md). It encrypts the connection and validates the server certificate. It uses `SQL_VARIANT_PROPERTY` to show the type that the server receives. The string `Zoë` round-trips in a database that uses a Latin1 or UTF-8 default collation.

```php
<?php
$config = [];
foreach (['SQL_SERVER', 'SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $value = getenv($name);
    if ($value === false || $value === '') {
        throw new RuntimeException("Set $name before running this example.");
    }
    $config[$name] = $value;
}
foreach (['SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $config[$name] = '{' . str_replace('}', '}}', $config[$name]) . '}';
}

$conn = sqlsrv_connect($config['SQL_SERVER'], [
    'Database' => $config['SQL_DATABASE'],
    'UID' => $config['SQL_USER'],
    'PWD' => $config['SQL_PASSWORD'],
    'Encrypt' => true,
    'TrustServerCertificate' => false,
    'CharacterSet' => SQLSRV_ENC_UTF8_VARCHAR,
]);
if ($conn === false) {
    die(print_r(sqlsrv_errors(), true));
}

$sql = "SELECT SQL_VARIANT_PROPERTY(?, 'BaseType'), ?";
$stmt = sqlsrv_query($conn, $sql, ['Zoë', 'Zoë']);
if ($stmt === false || sqlsrv_fetch($stmt) !== true) {
    die(print_r(sqlsrv_errors(), true));
}
$type = sqlsrv_get_field($stmt, 0);
$value = sqlsrv_get_field($stmt, 1);
if ($type !== 'varchar' || $value !== 'Zoë') {
    throw new RuntimeException("Unexpected result: $type, $value");
}
echo "Sent as $type: $value\n";
sqlsrv_close($conn);
```

### PDO_SQLSRV example

The following example assumes an open `PDO` connection in `$conn` that uses `PDO::ERRMODE_EXCEPTION`. To open one, see [Connecting to the server](connecting-to-the-server.md). The first query sends both parameters as **varchar**. The second query overrides the encoding for one parameter, so the server receives **nvarchar**.

```php
$conn->setAttribute(PDO::SQLSRV_ATTR_ENCODING, PDO::SQLSRV_ENCODING_UTF8_VARCHAR);

$stmt = $conn->prepare("SELECT SQL_VARIANT_PROPERTY(?, 'BaseType'), ?");
$stmt->execute(['Zoë', 'Zoë']);
[$type, $value] = $stmt->fetch(PDO::FETCH_NUM);
if ($type !== 'varchar' || $value !== 'Zoë') {
    throw new RuntimeException("Unexpected result: $type, $value");
}

$name = 'Zoë';
$stmt = $conn->prepare("SELECT SQL_VARIANT_PROPERTY(?, 'BaseType')");
$stmt->bindParam(1, $name, PDO::PARAM_STR, 0, PDO::SQLSRV_ENCODING_UTF8);
$stmt->execute();
$type = $stmt->fetchColumn();
if ($type !== 'nvarchar') {
    throw new RuntimeException("Unexpected type: $type");
}
echo "Override sent as $type\n";
```

## Related content

- [Retrieving Data](retrieving-data.md)
- [How to: Send and Retrieve ASCII Data in Linux and macOS](how-to-send-and-retrieve-ascii-data-in-linux-mac.md)
- [Updating data (Microsoft Drivers for PHP for SQL Server)](updating-data-microsoft-drivers-for-php-for-sql-server.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [Constants (Microsoft Drivers for PHP for SQL Server)](constants-microsoft-drivers-for-php-for-sql-server.md)
- [Example Application (SQLSRV Driver)](example-application-sqlsrv-driver.md)
