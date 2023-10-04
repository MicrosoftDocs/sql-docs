---
title: "PageCount Property (ADO)"
description: "PageCount Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::PageCount"
helpviewer_keywords:
  - "PageCount property [ADO]"
apitype: "COM"
---
# PageCount Property (ADO)
Indicates how many pages of data the [Recordset](./recordset-object-ado.md) object contains.  
  
## Return Value  
 Returns a **Long** value that indicates the number of pages in the **Recordset**.  
  
## Remarks  
 Use the **PageCount** property to determine how many pages of data are in the **Recordset** object. *Pages* are groups of records whose size equals the [PageSize](./pagesize-property-ado.md) property setting. Even if the last page is incomplete because there are fewer records than the **PageSize** value, it counts as an additional page in the **PageCount** value. If the **Recordset** object does not support this property, the value will be -1 to indicate that the **PageCount** is indeterminable.  
  
 See the **PageSize** and [AbsolutePage](./absolutepage-property-ado.md) properties for more on page functionality.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [AbsolutePage, PageCount, and PageSize Properties Example (VB)](./absolutepage-pagecount-and-pagesize-properties-example-vb.md)   
 [AbsolutePage, PageCount, and PageSize Properties Example (VC++)](./absolutepage-pagecount-and-pagesize-properties-example-vc.md)   
 [AbsolutePage Property (ADO)](./absolutepage-property-ado.md)   
 [PageSize Property (ADO)](./pagesize-property-ado.md)   
 [RecordCount Property (ADO)](./recordcount-property-ado.md)