---
title: "sqlsrv_client_info"
description: "API reference for the sqlsrv_client_info function in the Microsoft SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "API Reference, sqlsrv_client_info"
  - "sqlsrv_client_info"
apiname: "sqlsrv_client_info"
apitype: "NA"
---
# sqlsrv_client_info
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns information about the connection and client stack.  
  
## Syntax  
  
```  
  
sqlsrv_client_info( resource $conn)  
```  
  
#### Parameters  
*$conn*: The connection resource by which the client is connected.  
  
## Return Value  
An associative array with keys described in the table below, or **false** if the connection resource is null.  
  
**For PHP for SQL Server versions 3.2 and 3.1**:  
  
|Key|Description|  
|-------|---------------|  
|DriverDllName|MSODBCSQL11.DLL (ODBC Driver 11 for SQL Server)|  
|DriverODBCVer|ODBC version (xx.yy)|  
|DriverVer|ODBC Driver 11 for SQL Server DLL version:<br /><br />xx.yy.zzzz ([!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 3.2 or 3.1)|  
|ExtensionVer|php_sqlsrv.dll version:<br /><br />3.2.xxxx.x (for [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 3.2)<br /><br />3.1.xxxx.x (for [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 3.1)|  
  
**For PHP for SQL Server versions 3.0 and 2.0**:  
  
|Key|Description|  
|-------|---------------|  
|DriverDllName|SQLNCLI10.DLL ([!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 2.0)|  
|DriverODBCVer|ODBC version (xx.yy)|  
|DriverVer|SQL Server Native Client DLL version:<br /><br />10.50.xxx ([!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 2.0)|  
|ExtensionVer|php_sqlsrv.dll version:<br /><br />2.0.xxxx.x ([!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version 2.0)|  
  
## Example  
The following example writes client information to the console when the example is run from the command line. The example assumes that SQL Server is installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/*Connect to the local server using Windows Authentication and   
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$conn = sqlsrv_connect( $serverName);  
  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
if( $client_info = sqlsrv_client_info( $conn))  
{  
       foreach( $client_info as $key => $value)  
      {  
              echo $key.": ".$value."\n";  
      }  
}  
else  
{  
       echo "Client info error.\n";  
}  
  
/* Close connection resources. */  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
  
