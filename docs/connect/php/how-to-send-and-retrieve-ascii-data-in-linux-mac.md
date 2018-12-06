---
title: "How to: Send and Retrieve ASCII Data in Linux and macOS (SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/16/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "retrieving data, ASCII data"
  - "sending data"
  - "linux"
  - "macOS"
author: "yitam"
ms.author: "v-yitam"
manager: "mbarwin"
---
# How to: Send and Retrieve ASCII Data in Linux and macOS 
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This article assumes the ASCII (non-UTF-8) locales have been generated or installed in your Linux or macOS systems. 

To send or retrieve ASCII character sets to the server:  

1.  If the desired locale is not the default in your system environment, make sure you invoke `setlocale(LC_ALL, $locale)` before making the first connection. The PHP setlocale() function changes the locale only for the current script, and if invoked after making the first connection, it may be ignored.
 
2.  When using the SQLSRV driver, you may specify `'CharacterSet' => SQLSRV_ENC_CHAR` as a connection option, but this step is optional because it is the default encoding.

3.  When using the PDO_SQLSRV driver, there are two ways. First, when making the connection, set `PDO::SQLSRV_ATTR_ENCODING` to `PDO::SQLSRV_ENCODING_SYSTEM` (for an example of setting a connection option, see [PDO::__construct](../../connect/php/pdo-construct.md)). Alternatively, after successfully connected, add this line `$conn->setAttribute(PDO::SQLSRV_ATTR_ENCODING, PDO::SQLSRV_ENCODING_SYSTEM);` 
  
When you specify the encoding of a connection resource (in SQLSRV) or connection object (PDO_SQLSRV), the driver assumes that the other connection option strings use that same encoding. The server name and query strings are also assumed to use the same character set.  
  
The default encoding for PDO_SQLSRV driver is UTF-8 (PDO::SQLSRV_ENCODING_UTF8), unlike the SQLSRV driver. For more information about these constants, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md). 
  
## Example  
The following examples demonstrate how to send and retrieve ASCII data using the PHP Drivers for SQL Server by specifying a particular locale before making the connection. The locales in various Linux platforms may be named differently from the same locales in macOS. For example, the US ISO-8859-1 (Latin 1) locale is `en_US.ISO-8859-1` in Linux while in macOS the name is `en_US.ISO8859-1`.
  
The examples assume that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a server. All output is written to the browser when the examples are run from the browser.  
  
```  
<?php  
  
// SQLSRV Example
//
// Setting locale for the script is only necessary if Latin 1 is not the default 
// in the environment
$locale = strtoupper(PHP_OS) === 'LINUX' ? 'en_US.ISO-8859-1' : 'en_US.ISO8859-1';
setlocale(LC_ALL, $locale);
        
$serverName = 'MyServer';
$database = 'Test';
$connectionInfo = array('Database'=>'Test', 'UID'=>$uid, 'PWD'=>$pwd);
$conn = sqlsrv_connect($serverName, $connectionInfo);
  
if ($conn === false) {
    echo "Could not connect.<br>";  
    die(print_r(sqlsrv_errors(), true));
}  
  
// Set up the Transact-SQL query to create a test table
//   
$stmt = sqlsrv_query($conn, "CREATE TABLE [Table1] ([c1_int] int, [c2_varchar] varchar(512))");

// Insert data using a parameter array 
//
$tsql = "INSERT INTO [Table1] (c1_int, c2_varchar) VALUES (?, ?)";
  
// Execute the query, $value being some ASCII string
//   
$stmt = sqlsrv_query($conn, $tsql, array(1, array($value, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR))));
  
if ($stmt === false) {
    echo "Error in statement execution.<br>";  
    die(print_r(sqlsrv_errors(), true));  
}  
else {  
    echo "The insertion was successfully executed.<br>";  
}  
  
// Retrieve the newly inserted data
//   
$stmt = sqlsrv_query($conn, "SELECT * FROM Table1");
$outValue = null;  
if ($stmt === false) {  
    echo "Error in statement execution.<br>";  
    die(print_r(sqlsrv_errors(), true));  
}  
  
// Retrieve and display the data
//   
if (sqlsrv_fetch($stmt)) {  
    $outValue = sqlsrv_get_field($stmt, 1, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR));
    echo "Value: " . $outValue . "<br>";
    if ($value !== $outValue) {
        echo "Data retrieved, \'$outValue\', is unexpected!<br>";
    }
}  
else {  
    echo "Error in fetching data.<br>";  
    die(print_r(sqlsrv_errors(), true));  
}  

// Free statement and connection resources
//   
sqlsrv_free_stmt($stmt);  
sqlsrv_close($conn);  
?>  
```  
  
```
<?php  
  
// PDO_SQLSRV Example:
//
// Setting locale for the script is only necessary if Latin 1 is not the default 
// in the environment
$locale = strtoupper(PHP_OS) === 'LINUX' ? 'en_US.ISO-8859-1' : 'en_US.ISO8859-1';
setlocale(LC_ALL, $locale);
        
$serverName = 'MyServer';
$database = 'Test';

try {
    $conn = new PDO("sqlsrv:Server=$serverName;Database=$database;", $uid, $pwd);
    $conn->setAttribute(PDO::SQLSRV_ATTR_ENCODING, PDO::SQLSRV_ENCODING_SYSTEM);
    
    // Set up the Transact-SQL query to create a test table
    //   
    $stmt = $conn->query("CREATE TABLE [Table1] ([c1_int] int, [c2_varchar] varchar(512))");
    
    // Insert data using parameters, $value being some ASCII string
    //
    $stmt = $conn->prepare("INSERT INTO [Table1] (c1_int, c2_varchar) VALUES (:var1, :var2)");
    $stmt->bindValue(1, 1);
    $stmt->bindParam(2, $value);
    $stmt->execute();
    
    // Retrieve and display the data
    //
    $stmt = $conn->query("SELECT * FROM [Table1]");
    $outValue = null;
    if ($row = $stmt->fetch()) {
        $outValue = $row[1];
        echo "Value: " . $outValue . "<br>";
        if ($value !== $outValue) {
            echo "Data retrieved, \'$outValue\', is unexpected!<br>";
        }
    }
} catch (PDOException $e) {
    echo $e->getMessage() . "<br>";
} finally {
    // Free statement and connection resources
    //
    unset($stmt);
    unset($conn);
}

?>  
```  

## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)  
[Working with UTF-8 Data](../../connect/php/how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md)
[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/updating-data-microsoft-drivers-for-php-for-sql-server.md)  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)  
[Example Application &#40;SQLSRV Driver&#41;](../../connect/php/example-application-sqlsrv-driver.md)  
  
