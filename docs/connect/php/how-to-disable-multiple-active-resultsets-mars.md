---
title: "How to: Disable Multiple Active Resultsets (MARS) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "multiple active result sets, disabling"
  - "MARS, disabling"
ms.assetid: 1912ad05-d0a4-40ff-8888-0d85bb36a807
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Disable Multiple Active Resultsets (MARS)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

If you need to connect to a SQL Server data source that does not enable Multiple Active Result Sets (MARS), you can use the MultipleActiveResultSets connection option to disable or enable MARS.  
  
## Procedure  
  
#### To disable MARS support  
  
-   Use the following connection option:  
  
    ```  
    'MultipleActiveResultSets'=>false  
    ```  
  
    If your application attempts to execute a query on a connection that has an open active result set, the second query attempt will return the following error information:  
  
    The connection cannot process this operation because there is a statement with pending results.  To make the connection available for other queries either fetch all results, cancel or free the statement. For more information about the MultipleActiveResultSets connection option, see [Connection Options](../../connect/php/connection-options.md).  
  
## Example  
The following example shows how to disable MARS support, using the SQLSRV driver of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
```  
<?php  
/* Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "MyServer";  
$connectionInfo = array( "Database"=>"AdventureWorks", 'MultipleActiveResultSets'=> false);  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
   echo "Could not connect.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
sqlsrv_close( $conn);  
?>  
```  
  
## Example  
The following example shows how to disable MARS support, using the PDO_SQLSRV driver of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
```  
<?php  
// Connect to the local server using Windows Authentication and AdventureWorks database  
$serverName = "(local)";   
$database = "AdventureWorks";  
  
try {  
   $conn = new PDO(" sqlsrv:server=$serverName ; Database=$database ; MultipleActiveResultSets=false ", NULL, NULL);   
   $conn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );   
}  
  
catch( PDOException $e ) {  
   die( "Error connecting to SQL Server" );   
}  
  
$conn = null;   
?>  
```  
  
## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)  
  
