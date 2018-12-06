---
title: "GetRows Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::GetRows"
  - "Recordset15::raw_GetRows"
helpviewer_keywords: 
  - "Getrows method [ADO]"
ms.assetid: 14b92860-4171-47d9-a413-dd60dd6a8880
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetRows Method (ADO)
Retrieves multiple records of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object into an array.  
  
## Syntax  
  
```  
  
array = recordset.GetRows(Rows, Start, Fields )  
```  
  
## Return Value  
 Returns a **Variant** whose value is a two-dimensional array.  
  
#### Parameters  
 *Rows*  
 Optional. A [GetRowsOptionEnum](../../../ado/reference/ado-api/getrowsoptionenum.md) value that indicates the number of records to retrieve. The default is **adGetRowsRest**.  
  
 *Start*  
 Optional. A **String** value or **Variant** that evaluates to the bookmark for the record from which the **GetRows** operation should begin. You can also use a [BookmarkEnum](../../../ado/reference/ado-api/bookmarkenum.md) value.  
  
 *Fields*  
 Optional. A **Variant** that represents a single field name or ordinal position, or an array of field names or ordinal position numbers. ADO returns only the data in these fields.  
  
## Remarks  
 Use the **GetRows** method to copy records from a **Recordset** into a two-dimensional array. The first subscript identifies the field and the second identifies the record number. The *array* variable is automatically dimensioned to the correct size when the **GetRows** method returns the data.  
  
 If you do not specify a value for the *Rows* argument, the **GetRows** method automatically retrieves all the records in the **Recordset** object. If you request more records than are available, **GetRows** returns only the number of available records.  
  
 If the **Recordset** object supports bookmarks, you can specify at which record the **GetRows** method should begin retrieving data by passing the value of that record's [Bookmark](../../../ado/reference/ado-api/bookmark-property-ado.md) property in the *Start* argument.  
  
 If you want to restrict the fields that the **GetRows** call returns, you can pass either a single field name/number or an array of field names/numbers in the *Fields* argument.  
  
 After you call **GetRows**, the next unread record becomes the current record, or the [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) property is set to **True** if there are no more records.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [GetRows Method Example (VB)](../../../ado/reference/ado-api/getrows-method-example-vb.md)   
 [GetRows Method Example (VC++)](../../../ado/reference/ado-api/getrows-method-example-vc.md)   
