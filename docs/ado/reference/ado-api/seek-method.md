---
title: "Seek Method"
description: "Seek Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset21::Seek"
  - "Recordset21::raw_Seek"
helpviewer_keywords:
  - "Seek method [ADO]"
apitype: "COM"
---
# Seek Method
Searches the index of a [Recordset](./recordset-object-ado.md) to quickly locate the row that matches the specified values, and changes the current row position to that row.  
  
## Syntax  
  
```  
  
recordset.Seek KeyValues, SeekOption  
```  
  
#### Parameters  
 *KeyValues*  
 An array of **Variant** values. An index consists of one or more columns and the array contains a value to compare against each corresponding column.  
  
 *SeekOption*  
 A [SeekEnum](./seekenum.md) value that specifies the type of comparison to be made between the columns of the index and the corresponding *KeyValues*.  
  
## Remarks  
 Use the **Seek** method in conjunction with the [Index](./index-property.md) property if the underlying provider supports indexes on the **Recordset** object. Use the [Supports](./supports-method.md)**(adSeek)** method to determine whether the underlying provider supports **Seek**, and the **Supports(adIndex)** method to determine whether the provider supports indexes. (For example, the [OLE DB Provider for Microsoft Jet](../../guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md) supports **Seek** and **Index**.)  
  
 If **Seek** does not find the desired row, no error occurs, and the row is positioned at the end of the **Recordset**. Set the **Index** property to the desired index before executing this method.  
  
 This method is supported only with server-side cursors. Seek is not supported when the **Recordset** object's [CursorLocation](./cursorlocation-property-ado.md) property value is **adUseClient**.  
  
 This method can only be used when the **Recordset** object has been opened with a [CommandTypeEnum](./commandtypeenum.md) value of **adCmdTableDirect**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [Seek Method and Index Property Example (VB)](./seek-method-and-index-property-example-vb.md)   
 [Seek Method and Index Property Example (VC++)](./seek-method-and-index-property-example-vc.md)   
 [Find Method (ADO)](./find-method-ado.md)   
 [Index Property](./index-property.md)