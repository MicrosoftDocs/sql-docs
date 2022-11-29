---
title: "CursorTypeEnum"
description: "CursorTypeEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CursorTypeEnum"
helpviewer_keywords:
  - "CursorTypeEnum enumeration [ADO]"
apitype: "COM"
---
# CursorTypeEnum
Specifies the type of cursor used in a [Recordset](./recordset-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adOpenDynamic**|2|Uses a dynamic cursor. Additions, changes, and deletions by other users are visible, and all types of movement through the **Recordset** are allowed, except for bookmarks, if the provider doesn't support them.|  
|**adOpenForwardOnly**|0|Default. Uses a forward-only cursor. Identical to a static cursor, except that you can only scroll forward through records. This improves performance when you need to make only one pass through a **Recordset**.|  
|**adOpenKeyset**|1|Uses a keyset cursor. Like a dynamic cursor, except that you can't see records that other users add, although records that other users delete are inaccessible from your **Recordset**. Data changes by other users are still visible.|  
|**adOpenStatic**|3|Uses a static cursor, which is a static copy of a set of records that you can use to find data or generate reports. Additions, changes, or deletions by other users are not visible.|  
|**adOpenUnspecified**|-1|Does not specify the type of cursor.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.CursorType.DYNAMIC|  
|AdoEnums.CursorType.FORWARDONLY|  
|AdoEnums.CursorType.KEYSET|  
|AdoEnums.CursorType.STATIC|  
|AdoEnums.CursorType.UNSPECIFIED|  
  
## Applies To  
 [CursorType Property (ADO)](./cursortype-property-ado.md)