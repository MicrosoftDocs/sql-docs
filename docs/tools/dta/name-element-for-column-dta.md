---
title: "Name Element for Column (DTA)"
description: In the dta utility, the Name element for Column specifies the name for an index column in a user-specified configuration.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/01/2017
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
helpviewer_keywords:
  - "Name element"
dev_langs:
  - "XML"
---

# Name Element for Column (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the name for an index column in a user-specified configuration.  
  
## Syntax  
  
```  
  
<Index>  
    <Column>  
        <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each **Column** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Column Element for Index &#40;DTA&#41;](../../tools/dta/column-element-for-index-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## Related content

- [XML Input File Reference (Database Engine Tuning Advisor)](xml-input-file-reference-database-engine-tuning-advisor.md)
