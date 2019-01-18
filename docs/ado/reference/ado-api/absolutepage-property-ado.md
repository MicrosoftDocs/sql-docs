---
title: "AbsolutePage Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::AbsolutePage"
helpviewer_keywords: 
  - "AbsolutePage property [ADO]"
ms.assetid: ddb58a35-ec3a-423c-a504-3c65e62c23d4
author: MightyPen
ms.author: genemi
manager: craigg
---
# AbsolutePage Property (ADO)
Indicates on which page the current record resides.  
  
## Settings and Return Values  
 For 32-bit code, sets or returns a **Long** value from 1 to the number of pages in the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object ([PageCount](../../../ado/reference/ado-api/pagecount-property-ado.md)), or returns one of the [PositionEnum](../../../ado/reference/ado-api/positionenum.md) values.  
  
 For 64-bit code, use a data type that provides for storage of a 64-bit value. For example, you can use either **Long** or another value that can be 64-bit length such as DBORDINAL. Do not use **PositionEnum** values because they are limited to 32-bit length.  
  
## Remarks  
 This property can be used to identify the page number on which the current record is located. It uses the [PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md) property to logically divide the total rowset count of the **Recordset** object into a series of pages, each of which has the number of records equal to **PageSize** (except for the last page, which may have fewer records). The provider must support the appropriate functionality for this property to be available.  
  
-   When getting or setting the **AbsolutePage** property, ADO uses the [AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md) property and the [PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md) property together as follows:  
  
-   To get the **AbsolutePage**, ADO first retrieves the **AbsolutePosition**, and then divides it by the **PageSize**.  
  
-   To set the **AbsolutePage**, ADO moves the **AbsolutePosition** as follows: it multiplies the **PageSize** by the new **AbsolutePage** value and then adds 1 to the value. As a result, the current position in the **Recordset** after successfully setting **AbsolutePage** is the first record in that page.  
  
 Like the **AbsolutePosition** property, **AbsolutePage** is 1-based and equals 1 when the current record is the first record in the **Recordset**. Set this property to move to the first record of a particular page. Obtain the total number of pages from the **PageCount** property.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [AbsolutePage, PageCount, and PageSize Properties Example (VB)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vb.md)   
 [AbsolutePage, PageCount, and PageSize Properties Example (VC++)](../../../ado/reference/ado-api/absolutepage-pagecount-and-pagesize-properties-example-vc.md)   
 [AbsolutePosition Property (ADO)](../../../ado/reference/ado-api/absoluteposition-property-ado.md)   
 [PageCount Property (ADO)](../../../ado/reference/ado-api/pagecount-property-ado.md)   
 [PageSize Property (ADO)](../../../ado/reference/ado-api/pagesize-property-ado.md)
