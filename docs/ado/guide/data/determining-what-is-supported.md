---
title: "Determining What is Supported"
description: "Determining What is Supported"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "editing data [ADO], Supports method"
  - "Supports method [ADO]"
---
# Determining What is Supported
The **Supports** method is used to determine whether a specified **Recordset** object supports a particular type of functionality. It has the following syntax:  
  
```  
  
boolean = recordset.Supports(CursorOptions )  
```  
  
## Remarks  
 The **Supports** method returns a Boolean value that indicates whether the provider supports all of the features identified by the CursorOptions argument. You can use the **Supports** method to determine what types of functionality a **Recordset** object supports. If the **Recordset** object supports the features whose corresponding constants are in *CursorOptions*, the **Supports** method returns **True**. Otherwise, it returns **False**.  
  
 Using the **Supports** method, you can check for the ability of the **Recordset** object to add new records, use bookmarks, use the **Find** method, use scrolling, use the **Index** property, and to perform batch updates. For a complete list of constants and their meanings, see [CursorOptionEnum](../../reference/ado-api/cursoroptionenum.md).  
  
 Although the **Supports** method may return **True** for a given functionality, it does not guarantee that the provider can make the feature available under all circumstances. The **Supports** method simply returns whether the provider can support the specified functionality, assuming certain conditions are met. For example, the **Supports** method may indicate that a **Recordset** object supports updates, even though the cursor is based on a multiple table join - some columns of which are not updateable.