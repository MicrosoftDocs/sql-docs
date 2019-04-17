---
title: "Index Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset21::Index"
helpviewer_keywords: 
  - "Index property"
ms.assetid: 1c79e271-21ec-41a8-8163-c5e89f0001a7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Index Property
Indicates the name of the index currently in effect for a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a **String** value, which is the name of the index.  
  
## Remarks  
 The index named by the **Index** property must have previously been declared on the base table underlying the **Recordset** object. That is, the index must have been declared programmatically either as an ADOX [Index](../../../ado/reference/adox-api/index-object-adox.md) object, or when the base table was created.  
  
 A run-time error will occur if the index cannot be set. The **Index** property cannot be set under the following conditions:  
  
-   Within a [WillChangeRecordset](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md) or **RecordsetChangeComplete** event handler.  
  
-   If the **Recordset** is still executing an operation (which can be determined by the [State](../../../ado/reference/ado-api/state-property-ado.md) property).  
  
-   If a filter has been set on the **Recordset** with the [Filter](../../../ado/reference/ado-api/filter-property.md) property.  
  
 The **Index** property can always be set successfully if the **Recordset** is closed, but the **Recordset** will not open successfully, or the index will not be usable, if the underlying provider does not support indexes.  
  
 If the index can be set, the current row position may change. This will cause an update to the [AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md) property, and will fire the **WillChangeRecordset**, **RecordsetChangeComplete**, [WillMove](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md), and [MoveComplete](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md) events.  
  
 If the index can be set and the [LockType](../../../ado/reference/ado-api/locktype-property-ado.md) property is **adLockPessimistic** or **adLockOptimistic**, then an implicit [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) operation is performed. This releases the current and affected groups. Any existing filter is released, and the current row position is changed to the first row of the reordered **Recordset**.  
  
 The **Index** property is used in conjunction with the [Seek](../../../ado/reference/ado-api/seek-method.md) method. If the underlying provider does not support the **Index** property, and thus the **Seek** method, consider using the [Find](../../../ado/reference/ado-api/find-method-ado.md) method instead. Determine whether the **Recordset** object supports indexes with the [Supports](../../../ado/reference/ado-api/supports-method.md)**(adIndex)** method.  
  
 The built-in **Index** property is not related to the dynamic [Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md) property, although they both deal with indexes.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Seek Method and Index Property Example (VB)](../../../ado/reference/ado-api/seek-method-and-index-property-example-vb.md)   
 [Index Object (ADOX)](../../../ado/reference/adox-api/index-object-adox.md)   
 [Seek Method](../../../ado/reference/ado-api/seek-method.md)
