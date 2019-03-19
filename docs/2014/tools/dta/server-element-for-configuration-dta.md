---
title: "Server Element for Configuration (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Server element"
ms.assetid: da9ff870-9cfd-42fe-994b-7b9292681f7d
author: stevestein
ms.author: sstein
manager: craigg
---
# Server Element for Configuration (DTA)
  Contains the identifying information for the server where you want Database Engine Tuning Advisor to evaluate the hypothetical configuration (specified by the `Configuration` element).  
  
## Syntax  
  
```  
  
<Configuration>  
    <Server>  
...code removed here...  
    </Server>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once per `Configuration` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Configuration Element &#40;DTA&#41;](configuration-element-dta.md)|  
|**Child elements**|[Name Element for Server &#40;DTA&#41;](name-element-for-server-dta.md)<br /><br /> [Database Element for Configuration &#40;DTA&#41;](database-element-for-configuration-dta.md)|  
  
## Remarks  
 You can specify only one `Server` element for the `Configuration` element. This element is of the **ServerTypecomplexType** name in the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100). Do not confuse this `Server` element with the one that is the child of the `DTAInput` element. For more information, see [Server Element &#40;DTA&#41;](server-element-dta.md).  
  
## Example  
 For a usage example, see the [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
