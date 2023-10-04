---
title: "Source Property (ADO Recordset)"
description: "Source Property (ADO Recordset)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::putref_Source"
  - "Recordset15::Source"
  - "Recordset15::PutSource"
  - "Recordset15::get_Source"
  - "Recordset15::GetSource"
  - "Recordset15::PutRefSource"
  - "Recordset15::put_Source"
helpviewer_keywords:
  - "Source property [ADO Recordset]"
apitype: "COM"
---
# Source Property (ADO Recordset)
Indicates the data source for a [Recordset](./recordset-object-ado.md) object.  
  
## Settings and Return Values  
 Sets a **String** value or [Command](./command-object-ado.md) object reference; returns only a **String** value that indicates the source of the **Recordset**.  
  
## Remarks  
 Use the **Source** property to specify a data source for a **Recordset** object using one of the following: a **Command** object variable, an SQL statement, a stored procedure, or a table name.  
  
 If you set the **Source** property to a **Command** object, the [ActiveConnection](./activeconnection-property-ado.md) property of the **Recordset** object will inherit the value of the **ActiveConnection** property for the specified **Command** object. However, reading the **Source** property does not return a **Command** object; instead, it returns the [CommandText](./commandtext-property-ado.md) property of the **Command** object to which you set the **Source** property.  
  
 If the **Source** property is an SQL statement, a stored procedure, or a table name, you can optimize performance by passing the appropriate *Options* argument with the [Open](./open-method-ado-recordset.md) method call.  
  
 The **Source** property is read/write for closed **Recordset** objects and read-only for open **Recordset** objects.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [Source Property Example (VB)](./source-property-example-vb.md)   
 [Source Property (ADO Error)](./source-property-ado-error.md)   
 [Source Property (ADO Record)](./source-property-ado-record.md)