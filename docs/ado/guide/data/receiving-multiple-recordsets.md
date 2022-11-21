---
title: "Receiving Multiple Recordsets"
description: "Receiving Multiple Recordsets"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "receiving multiple Recordsets [ADO]"
  - "Recordset object [ADO], receiving multiple Recordsets"
---
# Receiving Multiple Recordsets
The [Microsoft OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md) supports returning multiple **Recordset** objects for a single command containing multiple SQL statements, one **Recordset** per SQL statement. The order in which the **Recordset**s are returned follows the order in which the SQL statements are placed in the command text.  
  
 The Microsoft OLE DB Provider for SQL Server also returns multiple resultsets to ADO when the command contains a COMPUTE clause. For example, a command containing the following SQL statement will return the results in two **Recordset** objects: one for the rowset of (*ProductID*, *ProductName*, *UnitPrice*), and the other for the average price of all products in the table.  
  
```  
SELECT ProductID, ProductName, UnitPrice   
  FROM PRODUCTS   
  COMPUTE AVG(UnitPrice)  
```  
  
 You can use the **Recordset.NextRecordset** method to enumerate the two objects.  
  
 For more information, see [NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md).
