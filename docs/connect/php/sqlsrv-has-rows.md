---
title: "sqlsrv_has_rows | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "API Reference, sqlsrv_has_rows"
  - "sqlsrv_has_rows"
ms.assetid: 4da7f640-cf12-409f-9e00-95b30a8d5e17
author: MightyPen
ms.author: genemi
manager: craigg
---
# sqlsrv_has_rows
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Indicates if the result set has one or more rows.  
  
## Syntax  
  
```  
  
sqlsrv_has_rows( resource $stmt )  
```  
  
#### Parameters  
*$stmt*: The executed statement.  
  
## Return Value  
If there are rows in the result set, the return value will be **true**. If there are no rows, or if the function call fails, the return value will be **false**.  
  
## Example  
  
```  
<?php  
   $server = "server_name";  
   $conn = sqlsrv_connect( $server, array( 'Database' => 'Northwind' ) );  
  
   $stmt = sqlsrv_query( $conn, "select * from orders where CustomerID = 'VINET'" , array());  
  
   if ($stmt !== NULL) {  
      $rows = sqlsrv_has_rows( $stmt );  
  
      if ($rows === true)  
         echo "\nthere are rows\n";  
      else   
         echo "\nno rows\n";  
   }  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
  
