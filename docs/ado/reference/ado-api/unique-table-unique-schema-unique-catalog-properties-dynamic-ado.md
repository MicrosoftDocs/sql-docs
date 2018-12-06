---
title: "Control Changes to Recordset Base Table (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Unique Table property [ADO]"
  - "Unique Schema property [ADO]"
  - "Unique Catalog property [ADO]"
ms.assetid: d0e775d8-e353-46a1-ad10-ed4cc240dfaa
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unique Table, Unique Schema, Unique Catalog Properties-Dynamic (ADO)
Enables you to closely control modifications to a particular base table in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) that was formed by a JOIN operation on multiple base tables.  
  
-   **Unique Table** specifies the name of the base table upon which updates, insertions, and deletions are allowed.  
  
-   **Unique Schema** specifies the *schema*, or name of the owner of the table.  
  
-   **Unique Catalog** specifies the *catalog*, or name of the database containing the table.  
  
## Settings and Return Values  
 Sets or returns a **String** value that is the name of a table, schema, or catalog.  
  
## Remarks  
 The desired base table is uniquely identified by its catalog, schema, and table names. When the **Unique Table** property is set, the values of the **Unique Schema** or **Unique Catalog** properties are used to find the base table. It is intended, but not required, that either or both the **Unique Schema** and **Unique Catalog** properties be set before the **Unique Table** property is set.  
  
 The primary key of the **Unique Table** is treated as the primary key of the entire **Recordset**. This is the key that is used for any method requiring a primary key.  
  
 While **Unique Table** is set, the [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) method affects only the named table. The [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md), [Resync](../../../ado/reference/ado-api/resync-method.md), [Update](../../../ado/reference/ado-api/update-method.md), and [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) methods affect any appropriate underlying base tables of the **Recordset**.  
  
 **Unique Table** must be specified before doing any custom resynchronizations. If **Unique Table** has not been specified, the [Resync Command](../../../ado/reference/ado-api/resync-command-property-dynamic-ado.md) property will have no effect.  
  
 A run-time error results if a unique base table cannot be found.  
  
 These dynamic properties are all appended to the **Recordset** object [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection when the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
