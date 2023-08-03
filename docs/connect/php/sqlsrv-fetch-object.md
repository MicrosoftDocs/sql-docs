---
title: "sqlsrv_fetch_object"
description: "sqlsrv_fetch_object"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_fetch_object"
  - "API Reference, sqlsrv_fetch_object"
  - "retrieving data, as an object"
apiname: "sqlsrv_fetch_object"
apitype: "NA"
---
# sqlsrv_fetch_object
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves the next row of data as a PHP object.  
  
## Syntax  
  
```  
sqlsrv_fetch_object( resource $stmt [, string $className [, array $ctorParams[, row[, ]offset]]])  
```  
  
#### Parameters  
*$stmt*: A statement resource corresponding to an executed statement.  
  
*$className* [OPTIONAL]: A string specifying the name of the class to instantiate. If a value for the *$className* parameter is not specified, an instance of the PHP **stdClass** is instantiated.  
  
*$ctorParams* [OPTIONAL]: An array that contains values passed to the constructor of the class specified with the *$className* parameter. If the constructor of the specified class accepts parameter values, the *$ctorParams* parameter must be used when calling **sqlsrv_fetch_object**.  
  
*row* [OPTIONAL]: One of the following values, specifying the row to access in a result set that uses a scrollable cursor. (If *row* is specified, *$className* and *$ctorParams* must be explicitly specified, even if you must specify null for *$className* and *$ctorParams*.)  
  
-   SQLSRV_SCROLL_NEXT  
  
-   SQLSRV_SCROLL_PRIOR  
  
-   SQLSRV_SCROLL_FIRST  
  
-   SQLSRV_SCROLL_LAST  
  
-   SQLSRV_SCROLL_ABSOLUTE  
  
-   SQLSRV_SCROLL_RELATIVE  
  
For more information about these values, see [Specifying a Cursor Type and Selecting Rows](../../connect/php/specifying-a-cursor-type-and-selecting-rows.md).  
  
*offset* [OPTIONAL]: Used with SQLSRV_SCROLL_ABSOLUTE and SQLSRV_SCROLL_RELATIVE to specify the row to retrieve. The first record in the result set is 0.  
  
## Return Value  
A PHP object with properties that correspond to result set field names. Property values are populated with the corresponding result set field values. If the class specified with the optional *$className* parameter does not exist or if there is no active result set associated with the specified statement, **false** is returned. If there are no more rows to retrieve, **null** is returned.  
  
The data type of a value in the returned object will be the default PHP data type. For information on default PHP data types, see [Default PHP Data Types](../../connect/php/default-php-data-types.md).  
  
## Remarks  
If a class name is specified with the optional *$className* parameter, an object of this class type is instantiated. If the class has properties whose names match the result set field names, the corresponding result set values are applied to the properties. If a result set field name does not match a class property, a property with the result set field name is added to the object and the result set value is applied to the property.  
  
The following rules apply when specifying a class with the *$className* parameter:  
  
-   Matching is case-sensitive. For example, the property name CustomerId does not match the field name CustomerID. In this case, a CustomerID property would be added to the object and the value of the CustomerID field would be given to the CustomerID property.  
  
-   Matching occurs regardless of access modifiers. For example, if the specified class has a private property whose name matches a result set field name, the value from the result set field is applied to the property.  
  
-   Class property data types are ignored. If the "CustomerID" field in the result set is a string but the "CustomerID" property of the class is an integer, the string value from the result set is written to the "CustomerID" property.  
  
-   If the specified class does not exist, the function returns **false** and adds an error to the error collection. For information about retrieving error information, see [sqlsrv_errors](../../connect/php/sqlsrv-errors.md).  
  
If a field with no name is returned, **sqlsrv_fetch_object** will discard the field value and issue a warning. For example, consider this Transact-SQL statement that inserts a value into a database table and retrieves the server-generated primary key:  
  
```sql
INSERT INTO Production.ProductPhoto (LargePhoto) VALUES (?);  
SELECT SCOPE_IDENTITY()
```
  
If the results returned by this query are retrieved with **sqlsrv_fetch_object**, the value returned by `SELECT SCOPE_IDENTITY()` will be discarded and a warning will be issued. To avoid this, you can specify a name for the returned field in the Transact-SQL statement. The following is one way to specify a column name in Transact-SQL:  
  
```sql
SELECT SCOPE_IDENTITY() AS PictureID
```
  
## Object example  
The following example retrieves each row of a result set as a PHP object. The example assumes that the SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
``` 
<?php  
/* Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Set up and execute the query. */  
$tsql = "SELECT FirstName, LastName  
         FROM Person.Contact  
         WHERE LastName='Alan'";  
$stmt = sqlsrv_query( $conn, $tsql);  
if( $stmt === false )  
{  
     echo "Error in query preparation/execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve each row as a PHP object and display the results.*/  
while( $obj = sqlsrv_fetch_object( $stmt))  
{  
      echo $obj->LastName.", ".$obj->FirstName."\n";  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## Class example  
The following example retrieves each row of a result set as an instance of the *Product* class defined in the script. The example retrieves product information from the *Purchasing.PurchaseOrderDetail* and *Production.Product* tables of the AdventureWorks database for products that have a specified due date (*DueDate*), and a stocked quantity (*StockQty*) less than a specified value. The example highlights some of the rules that apply when specifying a class in a call to **sqlsrv_fetch_object**:  
  
-   The *$product* variable is an instance of the *Product* class, because "Product" was specified with the *$className* parameter and the *Product* class exists.  
  
-   The *Name* property is added to the *$product* instance because the existing *name* property does not match.  
  
-   The *Color* property is added to the *$product* instance because there is no matching property.  
  
-   The private property *UnitPrice* is populated with the value of the *UnitPrice* field.  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/* Define the Product class. */  
class Product  
{  
     /* Constructor */  
     public function Product($ID)  
     {  
          $this->objID = $ID;  
     }  
     public $objID;  
     public $name;  
     public $StockedQty;  
     public $SafetyStockLevel;  
     private $UnitPrice;  
     function getPrice()  
     {  
          return $this->UnitPrice;  
     }  
}  
  
/* Connect to the local server using Windows Authentication, and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Define the query. */  
$tsql = "SELECT Name,  
                SafetyStockLevel,  
                StockedQty,  
                UnitPrice,  
                Color  
         FROM Purchasing.PurchaseOrderDetail AS pdo  
         JOIN Production.Product AS p  
         ON pdo.ProductID = p.ProductID  
         WHERE pdo.StockedQty < ?  
         AND pdo.DueDate= ?";  
  
/* Set the parameter values. */  
$params = array(3, '2002-01-29');  
  
/* Execute the query. */  
$stmt = sqlsrv_query( $conn, $tsql, $params);  
if ( $stmt )  
{  
     echo "Statement executed.\n";  
}   
else   
{  
     echo "Error in statement execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Iterate through the result set, printing a row of data upon each  
 iteration. Note the following:  
     1) $product is an instance of the Product class.  
     2) The $ctorParams parameter is required in the call to  
        sqlsrv_fetch_object, because the Product class constructor is  
        explicity defined and requires parameter values.  
     3) The "Name" property is added to the $product instance because  
        the existing "name" property does not match.  
     4) The "Color" property is added to the $product instance  
        because there is no matching property.  
     5) The private property "UnitPrice" is populated with the value  
        of the "UnitPrice" field.*/  
$i=0; //Used as the $objID in the Product class constructor.  
while( $product = sqlsrv_fetch_object( $stmt, "Product", array($i)))  
{  
     echo "Object ID: ".$product->objID."\n";  
     echo "Product Name: ".$product->Name."\n";  
     echo "Stocked Qty: ".$product->StockedQty."\n";  
     echo "Safety Stock Level: ".$product->SafetyStockLevel."\n";  
     echo "Product Color: ".$product->Color."\n";  
     echo "Unit Price: ".$product->getPrice()."\n";  
     echo "-----------------\n";  
     $i++;  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
The **sqlsrv_fetch_object** function always returns data according to the [Default PHP Data Types](../../connect/php/default-php-data-types.md). For information about how to specify the PHP data type, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
If a field with no name is returned, **sqlsrv_fetch_object** will discard the field value and issue a warning. For example, consider this Transact-SQL statement that inserts a value into a database table and retrieves the server-generated primary key:  
  
```sql
INSERT INTO Production.ProductPhoto (LargePhoto) VALUES (?);  
SELECT SCOPE_IDENTITY()
```
  
If the results returned by this query are retrieved with **sqlsrv_fetch_object**, the value returned by `SELECT SCOPE_IDENTITY()` will be discarded and a warning will be issued. To avoid this, you can specify a name for the returned field in the Transact-SQL statement. The following is one way to specify a column name in Transact-SQL:  
  
```sql
SELECT SCOPE_IDENTITY() AS PictureID
```
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
  
