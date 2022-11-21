---
title: "CompareEnum"
description: "CompareEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CompareEnum"
helpviewer_keywords:
  - "CompareEnum enumeration [ADO]"
apitype: "COM"
---
# CompareEnum
Specifies the relative position of two records represented by their bookmarks.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCompareEqual**|1|Indicates that the bookmarks are equal.|  
|**adCompareGreaterThan**|2|Indicates that the first bookmark is after the second.|  
|**adCompareLessThan**|0|Indicates that the first bookmark is before the second.|  
|**adCompareNotComparable**|4|Indicates that the bookmarks cannot be compared.|  
|**adCompareNotEqual**|3|Indicates that the bookmarks are not equal and not ordered.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Compare.EQUAL|  
|AdoEnums.Compare.GREATERTHAN|  
|AdoEnums.Compare.LESSTHAN|  
|AdoEnums.Compare.NOTCOMPARABLE|  
|AdoEnums.Compare.NOTEQUAL|  
  
## Applies To  
 [CompareBookmarks Method (ADO)](./comparebookmarks-method-ado.md)  
  
## See Also  
 [CompareBookmarks Method (ADO)](./comparebookmarks-method-ado.md)