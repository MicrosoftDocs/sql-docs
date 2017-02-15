---
title: "RecordCount Property (ADO) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "Recordset15::RecordCount"
  - "Recordset15::GetRecordCount"
  - "Recordset15::get_RecordCount"
helpviewer_keywords: 
  - "RecordCount property [ADO]"
ms.assetid: 834f0121-394a-44d4-ad7d-999b43a6fe63
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# RecordCount Property (ADO)
Indicates the number of records in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
## Return Value  
 Returns a **Long** value that indicates the number of records in the **Recordset**.  
  
## Remarks  
 Use the **RecordCount** property to find out how many records are in a **Recordset** object. The property returns -1 when ADO cannot determine the number of records or if the provider or cursor type does not support **RecordCount**. Reading the **RecordCount** property on a closed **Recordset** causes an error.  
  
 If the **Recordset** object supports approximate positioning or bookmarks ??? that is, **Supports (adApproxPosition)** or **Supports (adBookmark)**, respectively, return **True**??? this value will be the exact number of records in the **Recordset**, regardless of whether it has been fully populated. If the **Recordset** object does not support approximate positioning, this property may be a significant drain on resources because all records will have to be retrieved and counted to return an accurate **RecordCount** value.  
  
> [!NOTE]
>  In ADO versions 2.8 and earlier, the SQLOLEDB provider fetches all records when a server-side cursor is used, despite the fact that it returns **True** for both **Supports (adApproxPosition)** and **Supports (adBookmark)**.  
  
 The cursor type of the **Recordset** object affects whether the number of records can be determined. The **RecordCount** property will return -1 for a forward-only cursor; the actual count for a static or keyset cursor; and either -1 or the actual count for a dynamic cursor, depending on the data source.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Filter and RecordCount Properties Example (VB)](../../../ado/reference/ado-api/filter-and-recordcount-properties-example-vb.md)   
 [Filter and RecordCount Properties Example (VC++)](../../../ado/reference/ado-api/filter-and-recordcount-properties-example-vc.md)   
 [AbsolutePosition Property (ADO)](../../../ado/reference/ado-api/absoluteposition-property-ado.md)   
 [PageCount Property (ADO)](../../../ado/reference/ado-api/pagecount-property-ado.md)
