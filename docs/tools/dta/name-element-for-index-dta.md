---
title: "Name Element for Index (DTA)"
description: In the dta utility, the Name element for Index specifies a name for an index in the user-specified configuration.
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

# Name Element for Index (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies a name for an index in the user-specified configuration.  
  
## Syntax  
  
```  
  
<Create>  
    <Index>  
        <Name>...</Name>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each **Index** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Index Element &#40;DTA&#41;](../../tools/dta/index-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## Related content

- [XML Input File Reference (Database Engine Tuning Advisor)](xml-input-file-reference-database-engine-tuning-advisor.md)
