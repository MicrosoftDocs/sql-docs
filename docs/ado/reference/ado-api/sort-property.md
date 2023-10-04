---
title: "Sort Property"
description: "Sort Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::get_Sort"
  - "Recordset15::put_Sort"
  - "Recordset15::PutSort"
  - "Recordset15::GetSort"
  - "Recordset15::Sort"
helpviewer_keywords:
  - "DESC [ADO]"
  - "ASC [ADO]"
  - "Sort property [ADO]"
apitype: "COM"
---
# Sort Property
Indicates one or more field names on which the [Recordset](./recordset-object-ado.md) is sorted, and whether each field is sorted in ascending or descending order.  
  
## Settings and Return Values  
 Sets or returns a **String** value that indicates the field names in the **Recordset** on which to sort. Each name is separated by a comma, and is optionally followed by a blank and the keyword, **ASC**, which sorts the field in ascending order, or **DESC**, which sorts the field in descending order. By default, if no keyword is specified, the field is sorted in ascending order.  
  
## Remarks  
 This property requires the [CursorLocation](./cursorlocation-property-ado.md) property to be set to **adUseClient**. A temporary index will be created for each field specified in the **Sort** property if an index does not already exist.  
  
 The sort operation is efficient because data is not physically rearranged, but is simply accessed in the order specified by the index.  
  
 When the value of the **Sort** property is anything other than an empty string, the **Sort** property order takes precedence over the order specified in an **ORDER BY** clause included in the SQL statement used to open the **Recordset**.  
  
 The **Recordset** does not have to be opened before accessing the **Sort** property; it can be set at any time after the **Recordset** object is instantiated.  
  
 Setting the **Sort** property to an empty string will reset the rows to their original order and delete temporary indexes. Existing indexes will not be deleted.  
  
 Suppose a **Recordset** contains three fields named *firstName*, *middleInitial*, and *lastName*. Set the **Sort** property to the string, "`lastName DESC, firstName ASC`", which will order the **Recordset** by last name in descending order, then by first name in ascending order. The middle initial is ignored.  
  
 No field can be named "ASC" or "DESC" because those names conflict with the keywords **ASC** and **DESC**. You can create an alias for a field with a conflicting name by using the **AS** keyword in the query that returns the **Recordset**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [Sort Property Example (VB)](./sort-property-example-vb.md)   
 [Sort Property Example (VC++)](./sort-property-example-vc.md)   
 [Optimize Property-Dynamic (ADO)](./optimize-property-dynamic-ado.md)   
 [SortColumn Property (RDS)](../rds-api/sortcolumn-property-rds.md)   
 [SortDirection Property (RDS)](../rds-api/sortdirection-property-rds.md)