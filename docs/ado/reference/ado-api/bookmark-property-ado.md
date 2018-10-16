---
title: "Bookmark Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "03/20/2018"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::Bookmark"
helpviewer_keywords: 
  - "Bookmark property [ADO]"
ms.assetid: 481dcc93-487b-490e-ac58-a1e9b2ebfd43
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bookmark Property (ADO)
Indicates a bookmark that uniquely identifies the current record in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object or sets the current record in a **Recordset** object to the record identified by a valid bookmark.  
  
## Settings and Return Values  
 Sets or returns a **Variant** expression that evaluates to a valid bookmark.  
  
## Remarks  
 Use the **Bookmark** property to save the position of the current record and return to that record at any time. Bookmarks are available only in **Recordset** objects that support bookmark functionality.  
  
 When you open a **Recordset** object, each of its records has a unique bookmark. To save the bookmark for the current record, assign the value of the **Bookmark** property to a variable. To quickly return to that record at any time after moving to a different record, set the **Recordset** object's **Bookmark** property to the value of that variable.  
  
 The user may not be able to view the value of the bookmark. Also, users should not expect bookmarks to be directly comparable, because two bookmarks that refer to the same record may have different values.  
  
 If you use the [Clone](../../../ado/reference/ado-api/clone-method-ado.md) method to create a copy of a **Recordset** object, the **Bookmark** property settings for the original and the duplicate **Recordset** objects are identical and you can use them interchangeably. However, you cannot use bookmarks from different **Recordset** objects interchangeably, even if they were created from the same source or command.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Recordset** object, the **Bookmark** property is always available.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [BOF, EOF, and Bookmark Properties Example (VB)](../../../ado/reference/ado-api/bof-eof-and-bookmark-properties-example-vb.md)   
 [BOF, EOF, and Bookmark Properties Example (VC++)](../../../ado/reference/ado-api/bof-eof-and-bookmark-properties-example-vc.md)   
 [Supports Method](../../../ado/reference/ado-api/supports-method.md)
