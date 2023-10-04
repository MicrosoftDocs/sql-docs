---
title: "PageSize Property (ADO)"
description: "PageSize Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::PageSize"
helpviewer_keywords:
  - "PageSize property [ADO]"
apitype: "COM"
---
# PageSize Property (ADO)
Indicates how many records constitute one page in the [Recordset](./recordset-object-ado.md).  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates how many records are on a page. The default is **10**.  
  
## Remarks  
 Use the **PageSize** property to determine how many records make up a logical page of data. Establishing a page size allows you to use the [AbsolutePage](./absolutepage-property-ado.md) property to move to the first record of a particular page. This is useful in Web-server scenarios when you want to allow the user to page through data, viewing a certain number of records at a time.  
  
 This property can be set at any time, and its value will be used for calculating the location of the first record of a particular page.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [AbsolutePage, PageCount, and PageSize Properties Example (VB)](./absolutepage-pagecount-and-pagesize-properties-example-vb.md)   
 [AbsolutePage, PageCount, and PageSize Properties Example (VC++)](./absolutepage-pagecount-and-pagesize-properties-example-vc.md)   
 [AbsolutePage Property (ADO)](./absolutepage-property-ado.md)   
 [PageCount Property (ADO)](./pagecount-property-ado.md)