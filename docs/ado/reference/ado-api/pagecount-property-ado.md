---
title: "PageCount Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::PageCount"
helpviewer_keywords: 
  - "PageCount property [ADO]"
ms.assetid: b601b56c-0ac4-44ee-bc91-c3d2d104f00a
author: MightyPen
ms.author: genemi
manager: craigg
---
# PageCount Property (ADO)
Indicates how many pages of data the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object contains.  
  
## Return Value  
 Returns a **Long** value that indicates the number of pages in the **Recordset**.  
  
## Remarks  
 Use the **PageCount** property to determine how many pages of data are in the **Recordset** object. *Pages* are groups of records whose size equals the [PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md) property setting. Even if the last page is incomplete because there are fewer records than the **PageSize** value, it counts as an additional page in the **PageCount** value. If the **Recordset** object does not support this property, the value will be -1 to indicate that the **PageCount** is indeterminable.  
  
 See the **PageSize** and [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md) properties for more on page functionality.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [AbsolutePage, PageCount, and PageSize Properties Example (VB)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vb.md)   
 [AbsolutePage, PageCount, and PageSize Properties Example (VC++)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vc.md)   
 [AbsolutePage Property (ADO)](../../../ado/reference/ado-api/absolutepage-property-ado.md)   
 [PageSize Property (ADO)](../../../ado/reference/ado-api/pagesize-property-ado.md)   
 [RecordCount Property (ADO)](../../../ado/reference/ado-api/recordcount-property-ado.md)
