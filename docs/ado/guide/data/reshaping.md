---
title: "Reshaping | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "reshaping previously shaped Recordset [ADO]"
  - "data shaping [ADO], reshaping"
ms.assetid: b1c965b7-3dad-4de6-9e0e-502ca8785be3
author: MightyPen
ms.author: genemi
manager: craigg
---
# Reshaping
A **Recordset** created by a clause of a shape command may be assigned an *alias* name (typically with the AS keyword). The alias of a shaped **Recordset** can be referenced in a completely different command. That is, you can reuse, or *reshape*, a previously shaped **Recordset** in a new shape command. To support this feature, ADO provides a property, [Reshape Name](../../../ado/reference/ado-api/reshape-name-property-dynamic-ado.md).  
  
 Reshaping has two main functions. The first is to associate an existing **Recordset** with a new parent **Recordset**.  
  
## Example  
  
```  
rs1.Open "SHAPE {select * from Customers} " & _  
         "APPEND ({select * from Orders} AS chapOrders " & _  
         "RELATE CustomerID to CustomerID)", cn  
  
rs2.Open "SHAPE {select * from Employees} " & _  
         "APPEND (chapOrders RELATE EmployeeID to EmployeeID)", cn  
```  
  
 The second function is to enable non-chaptered access to existing child **Recordset** objects, using the syntax "SHAPE \<recordset reshape name>".  
  
> [!NOTE]
>  You cannot append columns to an existing **Recordset**, reshape a parameterized **Recordset** or the **Recordset** objects in any intervening COMPUTE clause, or perform aggregate operations on any **Recordset** descendant from the **Recordset** being reshaped. The **Recordset** being reshaped and the new shape command must both use the same [Connection](../../../ado/reference/ado-api/connection-object-ado.md).  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)
