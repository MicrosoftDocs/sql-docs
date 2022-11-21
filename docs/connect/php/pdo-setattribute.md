---
title: "PDO::setAttribute"
description: "API reference for the PDO::setAttribute function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDO::setAttribute
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Sets a predefined PDO attribute or a custom driver attribute.  
  
## Syntax  
  
```
bool PDO::setAttribute ( $attribute, $value );  
```  
  
#### Parameters  
*$attribute*: The attribute to set. See the Remarks section for a list of supported attributes.  
  
*$value*: The value (type mixed).  
  
## Return Value  
Returns true on success, otherwise false.  
  
## Remarks  
  
|Attribute|Processed by|Supported values|Description|  
|-------------|----------------|--------------------|---------------|  
|PDO::ATTR_CASE|PDO|PDO::CASE_LOWER<br /><br />PDO::CASE_NATURAL<br /><br />PDO::CASE_UPPER|Specifies the case of column names.<br /><br />PDO::CASE_LOWER causes lower case column names.<br /><br />PDO::CASE_NATURAL (the default) displays column names as returned by the database.<br /><br />PDO::CASE_UPPER causes column names to upper case.<br /><br />This attribute can be set using PDO::setAttribute.|  
|PDO::ATTR_DEFAULT_FETCH_MODE|PDO|See the PDO documentation.|See the PDO documentation.|  
|PDO::ATTR_DEFAULT_STR_PARAM|PDO|PDO::PARAM_STR_CHAR<br /><br />PDO::PARAM_STR_NATL|For more information see the examples in [PDO::quote](../../connect/php/pdo-quote.md).|
|PDO::ATTR_ERRMODE|PDO|PDO::ERRMODE_SILENT<br /><br />PDO::ERRMODE_WARNING<br /><br />PDO::ERRMODE_EXCEPTION|Specifies how the driver reports failures.<br /><br />PDO::ERRMODE_SILENT (the default) sets the error codes and information.<br /><br />PDO::ERRMODE_WARNING raises E_WARNING.<br /><br />PDO::ERRMODE_EXCEPTION causes an exception to be thrown.<br /><br />This attribute can be set using PDO::setAttribute.|  
|PDO::ATTR_ORACLE_NULLS|PDO|See the PDO documentation.|Specifies how nulls should be returned.<br /><br />PDO::NULL_NATURAL does no conversion.<br /><br />PDO::NULL_EMPTY_STRING converts an empty string to null.<br /><br />PDO::NULL_TO_STRING converts null to an empty string.|  
|PDO::ATTR_STATEMENT_CLASS|PDO|See the PDO documentation.|Sets the user-supplied statement class derived from PDOStatement.<br /><br />Requires `array(string classname, array(mixed constructor_args))`.<br /><br />For more information, see the PDO documentation.|  
|PDO::ATTR_STRINGIFY_FETCHES|PDO|true or false|Converts numeric values to strings when retrieving data.|  
|PDO::SQLSRV_ATTR_CLIENT_BUFFER_MAX_KB_SIZE|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|1 to the PHP memory limit.|Sets the size of the buffer that holds the result set when using a client-side cursor.<br /><br />The default is 10240 KB, if not specified in the php.ini file.<br /><br />Zero and negative numbers are not allowed.<br /><br />For more information about queries that create a client-side cursor, see [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md).|  
|PDO::SQLSRV_ATTR_DECIMAL_PLACES|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|Integer between 0 and 4 (inclusive)|Specifies the number of decimal places when formatting fetched money values.<br /><br />Any negative integer or value more than 4 will be ignored.<br /><br />This option works only when PDO::SQLSRV_ATTR_FORMAT_DECIMALS is true.<br /><br />This option may also be set at the statement level. If so, then the statement level option overrides this one.<br /><br />For more information, see [Formatting Decimal Strings and Money Values (PDO_SQLSRV Driver)](../../connect/php/formatting-decimals-pdo-sqlsrv-driver.md).|
|PDO::SQLSRV_ATTR_DIRECT_QUERY|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true or false|Specifies direct or prepared query execution. For more information, see [Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver](../../connect/php/direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md).|  
|PDO::SQLSRV_ATTR_ENCODING|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|PDO::SQLSRV_ENCODING_UTF8<br /><br />PDO::SQLSRV_ENCODING_SYSTEM.|Sets the character set encoding used by the driver to communicate with the server.<br /><br />PDO::SQLSRV_ENCODING_BINARY is not supported.<br /><br />The default is PDO::SQLSRV_ENCODING_UTF8.|  
|PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true or false|Specifies whether to retrieve date and time types as [PHP DateTime](http://php.net/manual/en/class.datetime.php) objects. If left false, the default behavior is to return them as strings.<br /><br />This option may also be set at the statement level. If so, then the statement level option overrides this one.<br /><br />For more information, see [How to: Retrieve Date and Time Types as PHP DateTime Objects Using the PDO_SQLSRV Driver](../../connect/php/how-to-retrieve-datetime-objects-using-pdo-sqlsrv-driver.md).|  
|PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true or false|Handles numeric fetches from columns with numeric SQL types (bit, integer, smallint, tinyint, float, or real).<br /><br />When connection option flag ATTR_STRINGIFY_FETCHES is on, the return value is a string even when SQLSRV_ATTR_FETCHES_NUMERIC_TYPE is on.<br /><br />When the returned PDO type in bind column is PDO_PARAM_INT, the return value from an integer column is an int even if SQLSRV_ATTR_FETCHES_NUMERIC_TYPE is off.|  
|PDO::SQLSRV_ATTR_FORMAT_DECIMALS|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|true or false|Specifies whether to add leading zeroes to decimal strings when appropriate. If set, this option enables the PDO::SQLSRV_ATTR_DECIMAL_PLACES option for formatting money types. If left false, the default behavior of returning exact precision and omitting leading zeroes for values less than 1 is used.<br /><br />This option may also be set at the statement level. If so, then the statement level option overrides this one.<br /><br />For more information, see [Formatting Decimal Strings and Money Values (PDO_SQLSRV Driver)](../../connect/php/formatting-decimals-pdo-sqlsrv-driver.md).| 
|PDO::SQLSRV_ATTR_QUERY_TIMEOUT|[!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]|integer|Sets the query timeout in seconds.<br /><br />The default is 0, which means the driver will wait indefinitely for results.<br /><br />Negative numbers are not allowed.|  
  
PDO processes some of the predefined attributes and requires the driver to process others. All custom attributes and connection options are processed by the driver. An unsupported attribute, connection option, or unsupported value is reported according to the setting of PDO::ATTR_ERRMODE.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This sample shows how to set the PDO::ATTR_ERRMODE attribute.  
  
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
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
