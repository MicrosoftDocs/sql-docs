---
title: "Cursor Types (PDO_SQLSRV Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 49ea6a6e-78d4-40f8-85eb-180b527f0537
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cursor Types (PDO_SQLSRV Driver)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The PDO_SQLSRV driver lets you create scrollable result sets with one of several cursors.  
  
For information on how to specify a cursor using the PDO_SQLSRV driver, and for code samples, see [PDO::prepare](../../connect/php/pdo-prepare.md).  
  
## PDO_SQLSRV and Server-Side Cursors  
Prior to version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], the PDO_SQLSRV driver allowed you to create a result set with a server-side forward-only or static cursor. Beginning in version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], keyset and dynamic cursors are also available.  
  
You can indicate the type of server-side cursor by using [PDO::prepare](../../connect/php/pdo-prepare.md) to select one of the following cursor types:  
  
-   PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY  
  
-   PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL  
  
You can request a dynamic, static, or keyset cursor by specifying PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL and then passing the appropriate value to PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE. Possible values that you can pass to PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE for server-side cursors are:
  
-   PDO::SQLSRV_CURSOR_DYNAMIC  
  
-   PDO::SQLSRV_CURSOR_KEYSET_DRIVEN  
  
-   PDO::SQLSRV_CURSOR_STATIC  
  
## PDO_SQLSRV and Client-Side Cursors  
Client-side cursors were added in version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] and allow you to cache an entire result set in memory. One advantage is that the row count is available after a query is executed.  
  
Client-side cursors should be used for small- to medium-sized result sets. Large result sets should use server-side cursors.

A query will return false if the buffer is not large enough to hold an entire result set when using a client-side cursor. You can increase the buffer size up to the PHP memory limit.  
  
You can configure the size of the buffer that holds the result set with the PDO::SQLSRV_ATTR_CLIENT_BUFFER_MAX_KB_SIZE attribute of [PDO::setAttribute](../../connect/php/pdo-setattribute.md) or [PDOStatement::setAttribute](../../connect/php/pdostatement-setattribute.md). You can also set the maximum buffer size in the php.ini file with pdo_sqlsrv.client_buffer_max_kb_size (for example, pdo_sqlsrv.client_buffer_max_kb_size = 1024).

You can request a client-side cursor by using [PDO::prepare](../../connect/php/pdo-prepare.md), specifying the PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL cursor type, and then specifying PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_BUFFERED.

## Example
The following example shows how to specify a buffered cursor.
```  
<?php  
$database = "AdventureWorks";  
$server = "(local)";  
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
$query = "select * from Person.ContactType";  
$stmt = $conn->prepare( $query, array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL, PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_BUFFERED));  
$stmt->execute();  
print $stmt->rowCount();  
  
echo "\n";  
  
while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){  
   print "$row[Name]\n";  
}  
echo "\n..\n";  
  
$row = $stmt->fetch( PDO::FETCH_BOTH, PDO::FETCH_ORI_FIRST );  
print_r($row);  
  
$row = $stmt->fetch( PDO::FETCH_ASSOC, PDO::FETCH_ORI_REL, 1 );  
print "$row[Name]\n";  
  
$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_NEXT );  
print "$row[1]\n";  
  
$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_PRIOR );  
print "$row[1]..\n";  
  
$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_ABS, 0 );  
print_r($row);  
  
$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_LAST );  
print_r($row);  
?>  
```  
  
## See Also  
[Specifying a Cursor Type and Selecting Rows](../../connect/php/specifying-a-cursor-type-and-selecting-rows.md)  
  
