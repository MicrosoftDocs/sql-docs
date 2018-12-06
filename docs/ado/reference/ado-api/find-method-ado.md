---
title: "Find Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::raw_Find"
  - "Recordset15::Find"
helpviewer_keywords: 
  - "Find method [ADO]"
ms.assetid: 55c9810a-d8ca-46c2-a9dc-80e7ee7aa188
author: MightyPen
ms.author: genemi
manager: craigg
---
# Find Method (ADO)
Searches a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) for the row that satisfies the specified criteria. Optionally, the direction of the search, starting row, and offset from the starting row may be specified. If the criteria is met, the current row position is set on the found record; otherwise, the position is set to the end (or start) of the **Recordset**.  
  
## Syntax  
  
```  
  
Find (Criteria, SkipRows, SearchDirection, Start)  
```  
  
#### Parameters  
 *Criteria*  
 A **String** value that contains a statement specifying the column name, comparison operator, and value to use in the search.  
  
 *SkipRows*  
 Optional*.* A **Long** value, whose default value is zero, that specifies the row offset from the current row or *Start* bookmark to begin the search. By default, the search will start on the current row.  
  
 *SearchDirection*  
 Optional*.* A [SearchDirectionEnum](../../../ado/reference/ado-api/searchdirectionenum.md) value that specifies whether the search should begin on the current row or the next available row in the direction of the search. An unsuccessful search stops at the end of the **Recordset** if the value is **adSearchForward**. An unsuccessful search stops at the start of the **Recordset** if the value is **adSearchBackward**.  
  
 *Start*  
 Optional. A **Variant** bookmark that functions as the starting position for the search.  
  
## Remarks  
 Only a single-column name may be specified in *criteria*. This method does not support multi-column searches.  
  
 The comparison operator in *Criteria* may be "**>**" (greater than), "**\<**" (less than), "=" (equal), ">=" (greater than or equal), "<=" (less than or equal), "<>" (not equal), or "like" (pattern matching).  
  
 The value in *Criteria* may be a string, floating-point number, or date. String values are delimited with single quotes or "#" (number sign) marks (for example, "state = 'WA'" or "state = #WA#"). Date values are delimited with "#" (number sign) marks (for example, "start_date > #7/22/97#"). These values can contain hours, minutes, and seconds to indicate time stamps, but should not contain milliseconds or errors will occur.  
  
 If the comparison operator is "like", the string value may contain an asterisk (*) to find one or more occurrences of any character or substring. For example, "state like 'M\*'" matches Maine and Massachusetts. You can also use leading and trailing asterisks to find a substring contained within the values. For example, "state like '\*as\*'" matches Alaska, Arkansas, and Massachusetts.  
  
 Asterisks can be used only at the end of a criteria string, or at both the beginning and end of a criteria string, as shown above. You cannot use the asterisk as a leading wildcard ('*str'), or as an embedded wildcard ('s\*r'). This will cause an error.  
  
> [!NOTE]
>  An error will occur if a current row position is not set before calling **Find**. Any method that sets row position, such as [MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md), should be called before calling **Find**.  
  
> [!NOTE]
>  If you call the **Find** method on a recordset, and the current position in the recordset is at the last record or end of file (EOF), you will not find anything. You need to call the **MoveFirst** method to set the current position/cursor to the beginning of the recordset.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Find Method Example (VB)](../../../ado/reference/ado-api/find-method-example-vb.md)   
 [Index Property](../../../ado/reference/ado-api/index-property.md)   
 [Optimize Property-Dynamic (ADO)](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md)   
 [Seek Method](../../../ado/reference/ado-api/seek-method.md)
