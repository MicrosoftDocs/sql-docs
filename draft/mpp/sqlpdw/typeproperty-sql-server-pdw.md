---
title: "TYPEPROPERTY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: af91c17f-77ba-4e98-a1ce-f4e88b224615
caps.latest.revision: 8
author: BarbKess
---
# TYPEPROPERTY (SQL Server PDW)
Returns information about a data type.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TYPEPROPERTY (type ,property)  
```  
  
## Arguments  
*type*  
The name of the data type.  
  
*property*  
The type of information to be returned for the data type. *property* can be one of the following values.  
  
|Property|Description|Value returned|  
|------------|---------------|------------------|  
|**AllowsNull**|Data type allows for null values.|1 = True<br /><br />0 = False<br /><br />NULL = Data type not found.|  
|**OwnerId**|Owner of the type.<br /><br />**Note:** The schema owner is not necessarily the type owner.|Nonnull = The database user ID of the type owner.<br /><br />NULL = Unsupported type, or type ID is not valid.|  
|**Precision**|Precision for the data type.|The number of digits or characters.<br /><br />-1 = **xml** or large value data type<br /><br />NULL = Data type not found.|  
|**Scale**|Scale for the data type.|The number of decimal places for the data type.<br /><br />NULL = Data type is not **numeric** or not found.|  
|**UsesAnsiTrim**|ANSI padding setting was ON when the data type was created.|1 = True<br /><br />0 = False<br /><br />NULL = Data type not found, or it is not a binary or string data type.|  
  
## Return Types  
**int**  
  
## Error Handling  
Returns NULL on error or if a caller does not have permission to view the object.  
  
In SQL Server PDW, a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as TYPEPROPERTY may return NULL if the user does not have any permission on the object.  
  
## Examples  
The following example returns the precision or number of digits for the `tinyint` data type.  
  
```  
SELECT TYPEPROPERTY( 'tinyint', 'PRECISION');  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
