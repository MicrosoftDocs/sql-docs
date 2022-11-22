---
title: "PDOStatement::fetch"
description: "API reference for the PDOStatement::fetch function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::fetch
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves a row from a result set.  
  
## Syntax  
  
```  
  
mixed PDOStatement::fetch ([ $fetch_style[, $cursor_orientation[, $cursor_offset]]] );  
```  
  
#### Parameters  
$*fetch_style*: An optional (integer) symbol specifying the format of the row data. See the Remarks section for the list of possible values for $*fetch_style*. Default is PDO::FETCH_BOTH. $*fetch_style* in the fetch method will override the $*fetch_style* specified in the PDO::query method.  
  
$*cursor_orientation*: An optional (integer) symbol indicating the row to retrieve when the prepare statement specifies `PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL`. See the Remarks section for the list of possible values for $*cursor_orientation*. See [PDO::prepare](../../connect/php/pdo-prepare.md) for a sample using a scrollable cursor.  
  
$*cursor_offset*: An optional (integer) symbol specifying the row to fetch when $*cursor_orientation* is either PDO::FETCH_ORI_ABS or PDO::FETCH_ORI_REL and PDO::ATTR_CURSOR is PDO::CURSOR_SCROLL.  
  
## Return Value  
A mixed value that returns a row or false.  
  
## Remarks  
The cursor is automatically advanced when fetch is called. The following table contains the list of possible $*fetch_style* values.  
  
|$*fetch_style*|Description|  
|-------------------|---------------|  
|PDO::FETCH_ASSOC|Specifies an array indexed by column name.|  
|PDO::FETCH_BOTH|Specifies an array indexed by column name and 0-based order. This is the default.|  
|PDO::FETCH_BOUND|Returns true and assigns the values as specified by [PDOStatement::bindColumn](../../connect/php/pdostatement-bindcolumn.md).|  
|PDO::FETCH_CLASS|Creates an instance and maps columns to named properties.<br /><br />Call [PDOStatement::setFetchMode](../../connect/php/pdostatement-setfetchmode.md) before calling fetch.|  
|PDO::FETCH_INTO|Refreshes an instance of the requested class.<br /><br />Call [PDOStatement::setFetchMode](../../connect/php/pdostatement-setfetchmode.md) before calling fetch.|  
|PDO::FETCH_LAZY|Creates variable names during access and creates an unnamed object.|  
|PDO::FETCH_NUM|Specifies an array indexed by zero-based column order.|  
|PDO::FETCH_OBJ|Specifies an unnamed object with property names that map to column names.|  
  
If the cursor is at the end of the result set (the last row has been retrieved and the cursor has advanced past the result set boundary) and if the cursor is forward-only (PDO::ATTR_CURSOR = PDO::CURSOR_FWDONLY), subsequent fetch calls will fail.  
  
If the cursor is scrollable (PDO::ATTR_CURSOR = PDO::CURSOR_SCROLL), fetch will move the cursor within the result set boundary. The following table contains the list of possible $*cursor_orientation* values.  
  
|$*cursor_orientation*|Description|  
|--------------------------|---------------|  
|PDO::FETCH_ORI_NEXT|Retrieves the next row. This is the default.|  
|PDO::FETCH_ORI_PRIOR|Retrieves the previous row.|  
|PDO::FETCH_ORI_FIRST|Retrieves the first row.|  
|PDO::FETCH_ORI_LAST|Retrieves the last row.|  
|PDO::FETCH_ORI_ABS, *num*|Retrieves the row requested in $*cursor_offset* by row number.|  
|PDO::FETCH_ORI_REL, *num*|Retrieves the row requested in $*cursor_offset* by relative position from the current position.|  
  
If the value specified for $*cursor_offset* or $*cursor_orientation* results in a position outside result set boundary, fetch will fail.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
  
```  
<?php  
   $server = "(local)";  
   $database = "AdventureWorks";  
   $conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");  
  
   print( "\n---------- PDO::FETCH_CLASS -------------\n" );  
   $stmt = $conn->query( "select * from HumanResources.Department order by GroupName" );  
  
   class cc {  
      function __construct( $arg ) {  
         echo "$arg";  
      }  
  
      function __toString() {  
         return $this->DepartmentID . "; " . $this->Name . "; " . $this->GroupName;  
      }  
   }  
  
   $stmt->setFetchMode(PDO::FETCH_CLASS, 'cc', array( "arg1 " ));  
   while ( $row = $stmt->fetch(PDO::FETCH_CLASS)) {   
      print($row . "\n");   
   }  
  
   print( "\n---------- PDO::FETCH_INTO -------------\n" );  
   $stmt = $conn->query( "select * from HumanResources.Department order by GroupName" );  
   $c_obj = new cc( '' );  
  
   $stmt->setFetchMode(PDO::FETCH_INTO, $c_obj);  
   while ( $row = $stmt->fetch(PDO::FETCH_INTO)) {   
      echo "$c_obj\n";  
   }  
  
   print( "\n---------- PDO::FETCH_ASSOC -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetch( PDO::FETCH_ASSOC );  
   print_r( $result );  
  
   print( "\n---------- PDO::FETCH_NUM -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetch( PDO::FETCH_NUM );  
   print_r ($result );  
  
   print( "\n---------- PDO::FETCH_BOTH -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetch( PDO::FETCH_BOTH );  
   print_r( $result );  
  
   print( "\n---------- PDO::FETCH_LAZY -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetch( PDO::FETCH_LAZY );  
   print_r( $result );  
  
   print( "\n---------- PDO::FETCH_OBJ -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $result = $stmt->fetch( PDO::FETCH_OBJ );  
   print $result->Name;  
   print( "\n \n" );  
  
   print( "\n---------- PDO::FETCH_BOUND -------------\n" );  
   $stmt = $conn->query( "select * from Person.ContactType where ContactTypeID < 5 " );  
   $stmt->bindColumn('Name', $name);  
   $result = $stmt->fetch( PDO::FETCH_BOUND );  
   print $name;  
   print( "\n \n" );  
?>  
```  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
