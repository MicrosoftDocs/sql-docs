---
title: "RecordCount Property (ADO)"
description: "RecordCount Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "03/20/2018"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::RecordCount"
  - "Recordset15::GetRecordCount"
  - "Recordset15::get_RecordCount"
helpviewer_keywords:
  - "RecordCount property [ADO]"
apitype: "COM"
---
# RecordCount Property (ADO)

Indicates the number of records in a [Recordset](./recordset-object-ado.md) object.
  
## Return Value

Returns a **Long** value that indicates the number of records in the **Recordset**.
  
## Remarks

Use the **RecordCount** property to find out how many records are in a **Recordset** object. The property returns -1 when ADO cannot determine the number of records or if the provider or cursor type does not support **RecordCount**. Reading the **RecordCount** property on a closed **Recordset** causes an error.

#### Bookmarks or approximate positioning

If the Recordset object *does* support either bookmarks or approximate positioning, this property returns the exact number of records in the Recordset. This property returns the exact number regardless of whether the Recordset has been fully populated.

In contrast, if the Recordset object does *not* support either bookmarks or approximate positioning, accessing this property might be a significant drain on resources. The drain occurs because all records must retrieved and counted to return an accurate RecordCount value.

- **adBookmark** related to bookmarks.
- **adApproxPosition** relates to approximate positioning.

> [!NOTE]
> In ADO versions 2.8 and earlier, the SQLOLEDB provider fetches all records when a server-side cursor is used, despite the fact that it returns **True** for both **Supports (adApproxPosition)** and **Supports (adBookmark)**.
  
The cursor type of the **Recordset** object affects whether the number of records can be determined. The **RecordCount** property will return -1 for a forward-only cursor; the actual count for a static or keyset cursor; and either -1 or the actual count for a dynamic cursor, depending on the data source.
  
## Applies To

[Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also

[Filter and RecordCount Properties Example (VB)](./filter-and-recordcount-properties-example-vb.md)   
[Filter and RecordCount Properties Example (VC++)](./filter-and-recordcount-properties-example-vc.md)   
[AbsolutePosition Property (ADO)](./absoluteposition-property-ado.md)   
[PageCount Property (ADO)](./pagecount-property-ado.md)