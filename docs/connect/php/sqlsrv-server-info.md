---
title: "sqlsrv_server_info"
description: "sqlsrv_server_info"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "API Reference, sqlsrv_server_info"
  - "sqlsrv_server_info"
apiname: "sqlsrv_server_info"
apitype: "NA"
---
# sqlsrv_server_info
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns information about the server. A connection must be established before calling this function.  
  
## Syntax  
  
```  
  
sqlsrv_server_info( resource $conn)  
```  
  
#### Parameters  
*$conn*: The connection resource by which the client and server are connected.  
  
## Return Value  
An associative array with the following keys:  
  
|Key|Description|  
|-------|---------------|  
|CurrentDatabase|The database currently being targeted.|  
|SQLServerVersion|The version of SQL Server.|  
|SQLServerName|The name of the server.|  
  
## Example  
The following example writes server information to the console when the example is run from the command line.  
  
```  
<?php  
/* Connect to the local server using Windows Authentication. */  
$serverName = "(local)";  
$conn = sqlsrv_connect( $serverName);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
$server_info = sqlsrv_server_info( $conn);  
if( $server_info )  
{  
      foreach( $server_info as $key => $value)  
      {  
             echo $key.": ".$value."\n";  
      }  
}  
else  
{  
      echo "Error in retrieving server info.\n";  
      die( print_r( sqlsrv_errors(), true));  
}  
  
/* Free connection resources. */  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
  
