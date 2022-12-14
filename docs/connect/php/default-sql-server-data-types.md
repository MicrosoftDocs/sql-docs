---
title: "Default SQL Server data types"
description: "This topic lists all the default SQL Server Data types based on PHP Data Types when using the Microsoft SQLSRV Driver for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "default data types"
  - "converting data types"
---
# Default SQL Server Data Types
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

When sending data to the server, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] converts data from its PHP data type to a SQL Server data type if no SQL Server data type has been specified by the user. The table that follows lists the PHP data type (the data type being sent to the server) and the default SQL Server data type (the data type to which the data is converted). For details about how to specify data types when sending data to the server, see [How to: Specify SQL Server Data Types When Using the SQLSRV Driver](../../connect/php/how-to-specify-sql-server-data-types-when-using-the-sqlsrv-driver.md).  
  
|PHP Data Type|Default SQL Server Type in the SQLSRV Driver|Default SQL Server Type in the PDO_SQLSRV Driver|  
|-----------------|------------------------------------------------|-----------------------------------------------------|  
|NULL|varchar(1)|not supported|  
|Boolean|bit|bit|  
|Integer|int|int|  
|Float|float(24)|not supported|  
|String (length less than 8000 bytes)|varchar(\<string length\>)|varchar(\<string length\>)|  
|String (length greater than 8000 bytes)|varchar(max)|varchar(max)|  
|Resource|Not supported.|Not supported.|  
|Stream (encoding: not binary)|varchar(max)|varchar(max)|  
|Stream (encoding: binary)|varbinary|varbinary|  
|Array|Not supported.|Not supported.|  
|Object|Not supported.|Not supported.|  
|DateTime (1)|datetime|Not supported.|  
  
## See Also  
[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)

[Converting Data Types](../../connect/php/converting-data-types.md)

[sqlsrv_field_metadata](../../connect/php/sqlsrv-field-metadata.md)

[PHP Types](https://php.net/manual/language.types.php)

[Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)  
