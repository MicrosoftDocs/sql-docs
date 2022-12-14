---
title: "CompareBookmarks Method (ADO)"
description: "CompareBookmarks Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CompareBookmarks"
  - "Recordset20::CompareBookmarks"
  - "Recordset20::raw_CompareBookmarks"
helpviewer_keywords:
  - "CompareBookmarks method [ADO]"
apitype: "COM"
---
# CompareBookmarks Method (ADO)
Compares two bookmarks and returns an indication of their relative values.  
  
## Syntax  
  
```  
  
result = recordset.CompareBookmarks(Bookmark1, Bookmark2)  
```  
  
## Return Value  
 Returns a [CompareEnum](./compareenum.md) value that indicates the relative row position of two records represented by their bookmarks.  
  
#### Parameters  
 *Bookmark1*  
 The bookmark of the first row.  
  
 *Bookmark2*  
 The bookmark of the second row.  
  
## Remarks  
 The bookmarks must apply to the same [Recordset](./recordset-object-ado.md) object, or a **Recordset** object and its [clone](./clone-method-ado.md). You cannot reliably compare bookmarks from different **Recordset** objects, even if they were created from the same source or command. Nor can you compare bookmarks for a **Recordset** object whose underlying provider does not support comparisons.  
  
 A bookmark uniquely identifies a row in a **Recordset** object. Use the [Bookmark](./bookmark-property-ado.md) property of the current row to obtain its bookmark.  
  
 Because the data type of a bookmark is specific to each provider, ADO exposes it as a **Variant**. For example, SQL Server bookmarks are of type DBTYPE_R8 (**Double**). ADO would expose this type as a **Variant** with a subtype of **Double**.  
  
 When comparing bookmarks, ADO does not attempt any type of coercion. The values are simply passed to the provider where the comparison occurs. If the bookmarks passed to the **CompareBookmarks** method are stored in variables of differing types, it can generate the following type mismatch error: "Arguments are of the wrong type, are out of the acceptable range, or are in conflict with each other."  
  
 A bookmark that is not valid or incorrectly formed will cause an error.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [CompareBookmarks Method Example (VB)](./comparebookmarks-method-example-vb.md)   
 [CompareBookmarks Method Example (VC++)](./comparebookmarks-method-example-vc.md)   
 [Bookmark Property (ADO)](./bookmark-property-ado.md)