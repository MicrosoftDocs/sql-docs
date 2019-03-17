---
title: "Resync Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset20::raw_Resync"
  - "Fields::Resync"
  - "Recordset20::Resync"
  - "Fields::raw_Resync"
helpviewer_keywords: 
  - "Resync method [ADO]"
ms.assetid: 73b355d4-a4c0-434b-bfc4-039b1c76b32e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Resync Method
Refreshes the data in the current [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, or [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object, from the underlying database.  
  
## Syntax  
  
```  
  
Recordset.Resync AffectRecords, ResyncValues Record.Fields.Resync ResyncValues  
```  
  
#### Parameters  
 *AffectRecords*  
 Optional. An [AffectEnum](../../../ado/reference/ado-api/affectenum.md) value that determines how many records the **Resync** method will affect. The default value is **adAffectAll**. This value is not available with the **Resync** method of the **Fields** collection of a **Record** object.  
  
 *ResyncValues*  
 Optional. A [ResyncEnum](../../../ado/reference/ado-api/resyncenum.md) value that specifies whether underlying values are overwritten. The default value is **adResyncAllValues**.  
  
## Remarks  
  
## Recordset  
 Use the **Resync** method to resynchronize records in the current **Recordset** with the underlying database. This is useful if you are using either a static or forward-only cursor, but you want to see any changes in the underlying database.  
  
 If you set the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**, **Resync** is only available for non-read-only **Recordset** objects.  
  
 Unlike the [Requery](../../../ado/reference/ado-api/requery-method.md) method, the **Resync** method does not re-execute the **Recordset** object's underlying command. New records in the underlying database will not be visible.  
  
 If the attempt to resynchronize fails because of a conflict with the underlying data (for example, a record has been deleted by another user), the provider returns warnings to the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection and a run-time error occurs. Use the [Filter](../../../ado/reference/ado-api/filter-property.md) property (**adFilterConflictingRecords**) and the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property to locate records with conflicts.  
  
 If the [Unique Table](../../../ado/reference/ado-api/unique-table-unique-schema-unique-catalog-properties-dynamic-ado.md) and [Resync Command](../../../ado/reference/ado-api/resync-command-property-dynamic-ado.md) dynamic properties are set, and the **Recordset** is the result of executing a JOIN operation on multiple tables, then the **Resync** method will execute the command given in the **Resync Command** property only on the table named in the **Unique Table** property.  
  
## Fields  
 Use the **Resync** method to resynchronize the values of the **Fields** collection of a **Record** object with the underlying data source. The [Count](../../../ado/reference/ado-api/count-property-ado.md) property is not affected by this method.  
  
 If *ResyncValues* is set to **adResyncAllValues** (the default value), the [UnderlyingValue](../../../ado/reference/ado-api/underlyingvalue-property.md), [Value](../../../ado/reference/ado-api/value-property-ado.md), and [OriginalValue](../../../ado/reference/ado-api/originalvalue-property-ado.md) properties of [Field](../../../ado/reference/ado-api/field-object.md) objects in the collection are synchronized. If *ResyncValues* is set to **adResyncUnderlyingValues**, only the **UnderlyingValue** property is synchronized.  
  
 The value of the **Status** property for each **Field** object at the time of the call also affects the behavior of **Resync**. For **Field** objects that have **Status** values of **adFieldPendingUnknown** or **adFieldPendingInsert**, **Resync** has no effect. For **Status** values of **adFieldPendingChange** or **adFieldPendingDelete**, **Resync** synchronizes data values for fields that still exist at the data source.  
  
 **Resync** will not modify **Status** values of **Field** objects unless an error occurs when **Resync** is called. For example, if the field no longer exists, the provider will return an appropriate **Status** value for the **Field** object, such as **adFieldDoesNotExist**. Returned **Status** values can be logically combined within the value of the **Status** property.  
  
## Applies To  
  
|||  
|-|-|  
|[Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|  
  
## See Also  
 [Resync Method Example (VB)](../../../ado/reference/ado-api/resync-method-example-vb.md)   
 [Resync Method Example (VC++)](../../../ado/reference/ado-api/resync-method-example-vc.md)   
 [Clear Method (ADO)](../../../ado/reference/ado-api/clear-method-ado.md)   
 [UnderlyingValue Property](../../../ado/reference/ado-api/underlyingvalue-property.md)
