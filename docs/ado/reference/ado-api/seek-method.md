---
title: "Seek Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset21::Seek"
  - "Recordset21::raw_Seek"
helpviewer_keywords: 
  - "Seek method [ADO]"
ms.assetid: 129293d2-19d3-4940-bf64-483ee72fb4a1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Seek Method
Searches the index of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) to quickly locate the row that matches the specified values, and changes the current row position to that row.  
  
## Syntax  
  
```  
  
recordset.Seek KeyValues, SeekOption  
```  
  
#### Parameters  
 *KeyValues*  
 An array of **Variant** values. An index consists of one or more columns and the array contains a value to compare against each corresponding column.  
  
 *SeekOption*  
 A [SeekEnum](../../../ado/reference/ado-api/seekenum.md) value that specifies the type of comparison to be made between the columns of the index and the corresponding *KeyValues*.  
  
## Remarks  
 Use the **Seek** method in conjunction with the [Index](../../../ado/reference/ado-api/index-property.md) property if the underlying provider supports indexes on the **Recordset** object. Use the [Supports](../../../ado/reference/ado-api/supports-method.md)**(adSeek)** method to determine whether the underlying provider supports **Seek**, and the **Supports(adIndex)** method to determine whether the provider supports indexes. (For example, the [OLE DB Provider for Microsoft Jet](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-jet.md) supports **Seek** and **Index**.)  
  
 If **Seek** does not find the desired row, no error occurs, and the row is positioned at the end of the **Recordset**. Set the **Index** property to the desired index before executing this method.  
  
 This method is supported only with server-side cursors. Seek is not supported when the **Recordset** object's [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property value is **adUseClient**.  
  
 This method can only be used when the **Recordset** object has been opened with a [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) value of **adCmdTableDirect**.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Seek Method and Index Property Example (VB)](../../../ado/reference/ado-api/seek-method-and-index-property-example-vb.md)   
 [Seek Method and Index Property Example (VC++)](../../../ado/reference/ado-api/seek-method-and-index-property-example-vc.md)   
 [Find Method (ADO)](../../../ado/reference/ado-api/find-method-ado.md)   
 [Index Property](../../../ado/reference/ado-api/index-property.md)
