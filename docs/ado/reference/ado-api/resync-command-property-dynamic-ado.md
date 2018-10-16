---
title: "Resync Command Property-Dynamic (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Resync Command property [ADO]"
ms.assetid: 4e2bb601-0fe8-4d61-b00e-38341d85a6bb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Resync Command Property-Dynamic (ADO)
Specifies a user-supplied command string that the [Resync](../../../ado/reference/ado-api/resync-method.md) method issues to refresh the data in the table named in the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) dynamic property.  
  
## Settings and Return Values  
 Sets or returns a **String** value which is a command string.  
  
## Remarks  
 The [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object is the result of a JOIN operation executed on multiple base tables. The rows affected depend on the *AffectRecords* parameter of the [Resync](../../../ado/reference/ado-api/resync-method.md) method. The standard **Resync** method is executed if the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) and **Resync Command** properties are not set.  
  
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
  
 **Resync Command** is a dynamic property appended to the **Recordset** object [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection when the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
