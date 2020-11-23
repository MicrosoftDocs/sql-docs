---
description: "Update Resync Property-Dynamic (ADO)"
title: "Update Resync Property-Dynamic (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Update Resync property [ADO]"
ms.assetid: 8a3bb608-66d7-4128-a3ef-84cb0556de0d
author: rothja
ms.author: jroth
---
# Update Resync Property-Dynamic (ADO)
Specifies whether the [UpdateBatch](./updatebatch-method.md) method is followed by an implicit [Resync](./resync-method.md) method operation, and if so, the scope of that operation.  
  
## Settings and Return Values  
 Sets or returns one or more of the [ADCPROP_UPDATERESYNC_ENUM](./adcprop-updateresync-enum.md) values.  
  
## Remarks  
 The values of ADCPROP_UPDATERESYNC_ENUM may be combined, except for adResyncAll which already represents the combination of the rest of the values.  
  
 The constant **adResyncConflicts** stores the resync values as underlying values, but does not override pending changes.  
  
 **Update Resync** is a dynamic property appended to the [Recordset](./recordset-object-ado.md) object [Properties](./properties-collection-ado.md) collection when the [CursorLocation](./cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)