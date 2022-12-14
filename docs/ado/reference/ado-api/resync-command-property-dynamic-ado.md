---
title: "Resync Command Property-Dynamic (ADO)"
description: "Resync Command Property-Dynamic (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Resync Command property [ADO]"
apitype: "COM"
---
# Resync Command Property-Dynamic (ADO)
Specifies a user-supplied command string that the [Resync](./resync-method.md) method issues to refresh the data in the table named in the [Unique Table](./unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) dynamic property.  
  
## Settings and Return Values  
 Sets or returns a **String** value which is a command string.  
  
## Remarks  
 The [Recordset](./recordset-object-ado.md) object is the result of a JOIN operation executed on multiple base tables. The rows affected depend on the *AffectRecords* parameter of the [Resync](./resync-method.md) method. The standard **Resync** method is executed if the [Unique Table](./unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) and **Resync Command** properties are not set.  
  
 The command string of the **Resync Command** property is a parameterized command or stored procedure that uniquely identifies the row being refreshed, and returns a single row containing the same number and order of columns as the row to be refreshed. The command string contains a parameter for each primary key column in the **Unique Table**; otherwise, a run-time error is returned. The parameters are automatically filled in with primary key values from the row to be refreshed.  
  
 Here are two examples based on SQL:  
  
 1\) The **Recordset** is defined by a command:  
  
```  
SELECT * FROM Customers JOIN Orders ON   
   Customers.CustomerID = Orders.CustomerID  
   WHERE city = 'Seattle'  
   ORDER BY CustomerID  
```  
  
 The **Resync Command** property is set to:  
  
```  
"SELECT * FROM   
   (SELECT * FROM Customers JOIN Orders   
   ON Customers.CustomerID = Orders.CustomerID  
   city = 'Seattle' ORDER BY CustomerID)  
WHERE Orders.OrderID = ?"  
```  
  
 The **Unique Table** is *Orders* and its primary key, *OrderID*, is parameterized. The sub-select provides a simple way to programmatically ensure that the same number and order of columns are returned as by the original command.  
  
 2\) The **Recordset** is defined by a stored procedure:  
  
```  
CREATE PROC Custorders @CustomerID char(5) AS   
SELECT * FROM Customers JOIN Orders ON   
Customers.CustomerID = Orders.CustomerID   
WHERE Customers.CustomerID = @CustomerID  
```  
  
 The **Resync** method should execute the following stored procedure:  
  
```  
CREATE PROC CustordersResync @ordid int AS   
SELECT * FROM Customers JOIN Orders ON   
Customers.CustomerID = Orders.CustomerID  
WHERE Orders.ordid  = @ordid  
```  
  
 The **Resync Command** property is set to:  
  
```  
"{call CustordersResync (?)}"  
```  
  
 Once again, the **Unique Table** is *Orders* and its primary key, *OrderID*, is parameterized.  
  
 **Resync Command** is a dynamic property appended to the **Recordset** object [Properties](./properties-collection-ado.md) collection when the [CursorLocation](./cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)