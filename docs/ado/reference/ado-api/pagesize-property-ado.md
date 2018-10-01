---
title: "PageSize Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::PageSize"
helpviewer_keywords: 
  - "PageSize property [ADO]"
ms.assetid: e57930a6-46c4-4a17-a3b6-f79e94d5c9c7
author: MightyPen
ms.author: genemi
manager: craigg
---
# PageSize Property (ADO)
Indicates how many records constitute one page in the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates how many records are on a page. The default is **10**.  
  
## Remarks  
 Use the **PageSize** property to determine how many records make up a logical page of data. Establishing a page size allows you to use the [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md) property to move to the first record of a particular page. This is useful in Web-server scenarios when you want to allow the user to page through data, viewing a certain number of records at a time.  
  
 This property can be set at any time, and its value will be used for calculating the location of the first record of a particular page.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [AbsolutePage, PageCount, and PageSize Properties Example (VB)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vb.md)   
 [AbsolutePage, PageCount, and PageSize Properties Example (VC++)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vc.md)   
 [AbsolutePage Property (ADO)](../../../ado/reference/ado-api/absolutepage-property-ado.md)   
 [PageCount Property (ADO)](../../../ado/reference/ado-api/pagecount-property-ado.md)
