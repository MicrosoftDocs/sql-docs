---
title: "PDO::getAttribute | Microsoft Docs"
ms.custom: ""
ms.date: "07/13/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: c81833ea-8b8a-459d-8f24-920098da994d
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::getAttribute
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves the value of a predefined PDO or driver attribute.  
  
## Syntax  
  
```  
  
mixed PDO::getAttribute ( $attribute )  
```  
  
#### Parameters  
*$attribute*: One of the supported attributes. See the Remarks section for the list of supported attributes.  
  
## Return Value  
On success, returns the value of a connection option, predefined PDO attribute, or custom driver attribute. On failure, returns null.  
  
## Remarks  
The following table contains the list of supported attributes.  
  
|Attribute|Processed by|Supported Values|Description|  
|-------------|----------------|--------------------|---------------|  
|PDO::ATTR_CASE|PDO|PDO::CASE_LOWER<br /><br />PDO::CASE_NATURAL<br /><br />PDO::CASE_UPPER|Specifies whether the column names should be in a specific case. PDO::CASE_LOWER forces lower case column names, PDO::CASE_NATURAL leaves the column name as returned by the database, and PDO::CASE_UPPER forces column names to upper case.<br /><br />The default is PDO::CASE_NATURAL.<br /><br />This attribute can also be set using PDO::setAttribute.|  
|PDO::ATTR_CLIENT_VERSION|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|Array of strings|Describes the versions of the driver and related libraries. Returns an array with the following elements: ODBC version (*MajorVer*.*MinorVer*), [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client DLL name and version, [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version (*MajorVer*.*MinorVer*.*BuildNumber*.*Revision*)|  
|PDO::ATTR_DRIVER_NAME|PDO|String|Always returns "sqlsrv".|  
|PDO::ATTR_DRIVER_VERSION|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|String|Indicates the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] version (*MajorVer*.*MinorVer*.*BuildNumber*.*Revision*)|  
|PDO::ATTR_ERRMODE|PDO|PDO::ERRMODE_SILENT<br /><br />PDO::ERRMODE_WARNING<br /><br />PDO::ERRMODE_EXCEPTION|Specifies how failures should be handled by the driver.<br /><br />PDO::ERRMODE_SILENT (the default) sets the error codes and information.<br /><br />PDO::ERRMODE_WARNING raises an E_WARNING.<br /><br />PDO::ERRMODE_EXCEPTION raises an exception.<br /><br />This attribute can also be set using PDO::setAttribute.|  
|PDO::ATTR_ORACLE_NULLS|PDO|See the PDO documentation.|See the PDO documentation.|  
|PDO::ATTR_SERVER_INFO|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|Array of 3 elements|Returns the current database, SQL Server version, and SQL Server instance.|  
|PDO::ATTR_SERVER_VERSION|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|String|Indicates the SQL Server version (*Major*.*Minor*.*BuildNumber*)|  
|PDO::ATTR_STRINGIFY_FETCHES|PDO|See PDO documentation|See the PDO documentation.|  
|PDO::SQLSRV_ATTR_CLIENT_BUFFER_MAX_KB_SIZE|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|1 to the PHP memory limit.|Configures the size of the buffer that holds the result set for a client-side cursor.<br /><br />The default is 10,240 KB (10 MB).<br /><br />For more information about client-side cursors, see [Cursor Types &#40;SQLSRV Driver&#41;](../../connect/php/cursor-types-sqlsrv-driver.md).|  
|PDO::SQLSRV_ATTR_DIRECT_QUERY|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true<br /><br />false|Specifies direct or prepared query execution. For more information, see [Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver](../../connect/php/direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md).|  
|PDO::SQLSRV_ATTR_ENCODING|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|PDO::SQLSRV_ENCODING_UTF8<br /><br />PDO::SQLSRV_ENCODING_SYSTEM|Specifies the character set encoding used by the driver to communicate with the server.<br /><br />The default is PDO::SQLSRV_ENCODING_UTF8.|  
|PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true or false|Handles numeric fetches from columns with numeric SQL types (bit, integer, smallint, tinyint, float, or real).<br /><br />When connection option flag ATTR_STRINGIFY_FETCHES is on, even when SQLSRV_ATTR_FETCHES_NUMERIC_TYPE is on, the return value is a string.<br /><br />When the returned PDO type in bind column is PDO_PARAM_INT, the return value from an integer column is an int even if SQLSRV_ATTR_FETCHES_NUMERIC_TYPE is off.|  
|PDO::SQLSRV_ATTR_QUERY_TIMEOUT|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|integer|Sets the query timeout in seconds.<br /><br />The default is 0, which means the driver will wait indefinitely for results.<br /><br />Negative numbers are not allowed.|  

  
PDO processes some of the predefined attributes while it requires the driver to handle others. All custom attributes and connection options are handled by the driver, an unsupported attribute or connection option returns null.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This example shows the value of the PDO::ATTR_ERRMODE attribute, before and after changing its value.  
  
```  
<?php  
$database = "AdventureWorks";  
$conn = new PDO( "sqlsrv:server=(local) ; Database = $database", "", "");  
  
$attributes1 = array( "ERRMODE" );  
foreach ( $attributes1 as $val ) {  
     echo "PDO::ATTR_$val: ";  
     var_dump ($conn->getAttribute( constant( "PDO::ATTR_$val" ) ));  
}  
  
$conn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );  
  
$attributes1 = array( "ERRMODE" );  
foreach ( $attributes1 as $val ) {  
     echo "PDO::ATTR_$val: ";  
     var_dump ($conn->getAttribute( constant( "PDO::ATTR_$val" ) ));  
}  
  
// An example using PDO::ATTR_CLIENT_VERSION  
print_r($conn->getAttribute( PDO::ATTR_CLIENT_VERSION ));  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
