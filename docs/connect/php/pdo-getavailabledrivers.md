---
title: "PDO::getAvailableDrivers"
description: "API reference for the PDO::getAvailableDrivers function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
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
  
