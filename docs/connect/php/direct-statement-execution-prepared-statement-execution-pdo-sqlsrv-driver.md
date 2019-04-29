---
title: "Direct Statement - Prepared Statement Execution PDO_SQLSRV Driver | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 05544ca6-1e07-486c-bf03-e8c2c25b3024
author: MightyPen
ms.author: genemi
manager: craigg
---
# Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic discusses the use of the PDO::SQLSRV_ATTR_DIRECT_QUERY attribute to specify direct statement execution instead of the default, which is prepared statement execution. Using a prepared statement can result in better performance if the statement is executed more than once using parameter binding.  
  
## Remarks  
If you want to send a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement directly to the server without statement preparation by the driver, you can set the PDO::SQLSRV_ATTR_DIRECT_QUERY attribute with [PDO::setAttribute](../../connect/php/pdo-setattribute.md) (or as a driver option passed to [PDO::__construct](../../connect/php/pdo-construct.md)) or when you call [PDO::prepare](../../connect/php/pdo-prepare.md). By default, the value of PDO::SQLSRV_ATTR_DIRECT_QUERY is False (use prepared statement execution).  
  
If you use [PDO::query](../../connect/php/pdo-query.md), you might want direct execution. Before calling [PDO::query](../../connect/php/pdo-query.md), call [PDO::setAttribute](../../connect/php/pdo-setattribute.md) and set PDO::SQLSRV_ATTR_DIRECT_QUERY to True.  Each call to [PDO::query](../../connect/php/pdo-query.md) can be executed with a different setting for PDO::SQLSRV_ATTR_DIRECT_QUERY.  
  
If you use [PDO::prepare](../../connect/php/pdo-prepare.md) and [PDOStatement::execute](../../connect/php/pdostatement-execute.md) to execute a query multiple times using bound parameters, prepared statement execution optimizes execution of the repeated query.  In this situation, call [PDO::prepare](../../connect/php/pdo-prepare.md) with PDO::SQLSRV_ATTR_DIRECT_QUERY set to False in the driver options array parameter. When necessary, you can execute prepared statements with PDO::SQLSRV_ATTR_DIRECT_QUERY set to False.  
  
After you call [PDO::prepare](../../connect/php/pdo-prepare.md), the value of PDO::SQLSRV_ATTR_DIRECT_QUERY cannot change when executing the prepared query.  
  
If a query requires the context that was set in a previous query, then execute your queries with PDO::SQLSRV_ATTR_DIRECT_QUERY set to True. For example, if you use temporary tables in your queries, PDO::SQLSRV_ATTR_DIRECT_QUERY must be set to True.  
  
The following sample shows that when context from a previous statement is required, you need to set PDO::SQLSRV_ATTR_DIRECT_QUERY to True. This sample uses temporary tables, which are only available to subsequent statements in your program when queries are executed directly.  
  
> [!NOTE]
> If the query is to invoke a stored procedure and temporary tables are used in this 
> stored procedure, use [PDO::exec](../../connect/php/pdo-exec.md) instead.

```  
<?php  
   $conn = new PDO('sqlsrv:Server=(local)', '', '');  
   $conn->setAttribute(constant('PDO::SQLSRV_ATTR_DIRECT_QUERY'), true);  
  
   $stmt1 = $conn->query("DROP TABLE #php_test_table");  
  
   $stmt2 = $conn->query("CREATE TABLE #php_test_table ([c1_int] int, [c2_int] int)");  
  
   $v1 = 1;  
   $v2 = 2;  
  
   $stmt3 = $conn->prepare("INSERT INTO #php_test_table (c1_int, c2_int) VALUES (:var1, :var2)");  
  
   if ($stmt3) {  
      $stmt3->bindValue(1, $v1);  
      $stmt3->bindValue(2, $v2);  
  
      if ($stmt3->execute())  
         echo "Execution succeeded\n";       
      else  
         echo "Execution failed\n";  
   }  
   else  
      var_dump($conn->errorInfo());  
  
   $stmt4 = $conn->query("DROP TABLE #php_test_table");  
?>  
```  
  
## See Also  
[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)
  
