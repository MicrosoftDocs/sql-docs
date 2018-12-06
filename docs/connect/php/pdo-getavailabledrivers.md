---
title: "PDO::getAvailableDrivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: eab561e6-1229-401a-9482-008c23f9a4e6
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::getAvailableDrivers
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns an array of the PDO drivers in your PHP installation.  
  
## Syntax  
  
```  
  
array PDO::getAvailableDrivers ();  
```  
  
## Return Value  
An array with the list of PDO drivers.  
  
## Remarks  
The name of the PDO driver is used in PDO::__construct, to create a PDO instance.  
  
PDO::getAvailableDrivers is not required to be implemented by PHP drivers. For more information about this method, see the PHP documentation.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
print_r(PDO::getAvailableDrivers());  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
