---
title: "PDO::query | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f6f5e6d4-8ca9-4f06-89ed-de65ad3952a2
caps.latest.revision: 19
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PDO::query
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Executes an SQL query and returns a result set as a PDOStatement object.  
  
## Syntax  
  
```  
  
PDOStatement PDO::query ($statement[, $fetch_style);  
```  
  
#### Parameters  
*$statement*: The SQL statement you want to execute.  
  
*$fetch_style*: The optional instructions on how to perform the query. See the Remarks section for more details. $*fetch_style* in PDO::query can be overridden with $*fetch_style* in PDO::fetch.  
  
## Return Value  
If the call succeeds, PDO::query returns a PDOStatement object. If the call fails, PDO::query throws a PDOException object or returns false, depending on the setting of PDO::ATTR_ERRMODE.  
  
## Exceptions  
PDOException.  
  
## Remarks  
A query executed with PDO::query can execute either a prepared statement or directly, depending on the setting of PDO::SQLSRV_ATTR_DIRECT_QUERY; see [Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver](../../connect/php/direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md) for more information.  
  
PDO::SQLSRV_ATTR_QUERY_TIMEOUT also affects the behavior of PDO::exec; see [PDO::setAttribute](../../connect/php/pdo-setattribute.md) for more information.  
  
You can specify the following options for $*fetch_style*.  
  
|Style|Description|  
|---------|---------------|  
|PDO::FETCH_COLUMN, *num*|Queries for data in the specified column. The first column in the table is column 0.|  
|PDO::FETCH_CLASS, '*classname*', array( *arglist* )|Creates an instance of a class and assigns column names to properties in the class. If the class constructor takes one or more parameters, you can also pass an *arglist*.|  
|PDO::FETCH_CLASS, '*classname*'|Assigns column names to properties in an existing class.|  
  
Call PDOStatement::closeCursor to release database resources associated with the PDOStatement object before calling PDO::query again.  
  
You can close a PDOStatement object by setting it to null.  
  
If all the data in a result set is not fetched, the next PDO::query call will not fail.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This example shows several queries.  
  
```  
<?php  
$database = "AdventureWorks";  
$conn = new PDO( "sqlsrv:server=(local) ; Database = $database", "", "");  
$conn->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );  
$conn->setAttribute( PDO::SQLSRV_ATTR_QUERY_TIMEOUT, 1 );  
  
$query = 'select * from Person.ContactType';  
  
// simple query  
$stmt = $conn->query( $query );  
while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){  
   print_r( $row['Name'] ."\n" );  
}  
  
echo "\n........ query for a column ............\n";  
  
// query for one column  
$stmt = $conn->query( $query, PDO::FETCH_COLUMN, 1 );  
while ( $row = $stmt->fetch() ){  
   echo "$row\n";  
}  
  
echo "\n........ query with a new class ............\n";  
$query = 'select * from HumanResources.Department order by GroupName';  
// query with a class  
class cc {  
   function __construct( $arg ) {  
      echo "$arg";  
   }  
  
   function __toString() {  
      return $this->DepartmentID . "; " . $this->Name . "; " . $this->GroupName;  
   }  
}  
  
$stmt = $conn->query( $query, PDO::FETCH_CLASS, 'cc', array( "arg1 " ));  
  
while ( $row = $stmt->fetch() ){  
   echo "$row\n";  
}  
  
echo "\n........ query into an existing class ............\n";  
$c_obj = new cc( '' );  
$stmt = $conn->query( $query, PDO::FETCH_INTO, $c_obj );  
while ( $stmt->fetch() ){  
   echo "$c_obj\n";  
}  
  
$stmt = null;  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)  
[PDO](http://go.microsoft.com/fwlink/?LinkID=187441)  
  
