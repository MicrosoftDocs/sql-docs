---
title: "Using Pages | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "PageSize property [ADO]"
  - "pages [ADO]"
  - "Recordset object [ADO]"
  - "AbsolutePage property [ADO]"
  - "PageCount property [ADO]"
ms.assetid: 442b08c5-ccc7-4192-a1cc-22f250867782
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Pages
Use the **PageCount** property to determine how many pages of data are in the **Recordset** object. *Pages* are groups of records whose size equals the **PageSize** property setting. Even if the last page is incomplete because there are fewer records than the **PageSize** value, it counts as an additional page in the **PageCount** value. If the **Recordset** object does not support this property, **PageCount** will be -1 to indicate that the **PageCount** is indeterminable.  
  
 Use the **PageSize** property to determine how many records make up a logical page of data. Establishing a page size allows you to use the **AbsolutePage** property to move to the first record of a particular page. This is useful in Web-server scenarios when you want to allow the user to page through data, viewing a certain number of records at a time.  
  
 This property can be set at any time, and its value will be used for calculating the location of the first record of a particular page.  
  
 Use the **AbsolutePage** property to identify the page number on which the current record is located. Again, the provider must support the appropriate functionality for this property to be available.  
  
 **AbsolutePage** is 1-based and equals 1 when the current record is the first record in the **Recordset**. Set this property to move to the first record of a particular page. Obtain the total number of pages from the **PageCount** property.
