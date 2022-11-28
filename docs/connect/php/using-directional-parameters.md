---
title: Using directional parameters
description: Learn how to use directional parameters when working with PHP and the SQLSRV and PDO_SQLSRV drivers for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 01/19/2017
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using directional parameters

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

When using the PDO_SQLSRV driver, you can use [PDOStatement::bindParam](pdostatement-bindparam.md) to specify input and output parameters.

The topics in this section describe how to use directional parameters when calling stored procedures using the SQLSRV driver.

## In this section

|Article|Description|
|---------|---------------|
|[How to: Specify Parameter Direction Using the SQLSRV Driver](how-to-specify-parameter-direction-using-the-sqlsrv-driver.md)|Demonstrates how to specify parameter direction when calling a stored procedure.|
|[How to: Retrieve Output Parameters Using the SQLSRV Driver](how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md)|Demonstrates how to call a stored procedure with an output parameter and how to retrieve its value.|
|[How to: Retrieve Input and Output Parameters Using the SQLSRV Driver](how-to-retrieve-input-and-output-parameters-using-the-sqlsrv-driver.md)|Demonstrates how to call a stored procedure with an input/output parameter and how to retrieve its value.|

## See also

[Retrieving Data](retrieving-data.md)  
[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](updating-data-microsoft-drivers-for-php-for-sql-server.md)  
