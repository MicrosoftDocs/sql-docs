---
title: "Configuration Element (DTA)"
description: In the dta utility, the Configuration element specifies a user-specified configuration consisting of existing and hypothetical physical design structures.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Configuration element"
ms.assetid: 1478e56f-57c4-4441-bac9-1ac91453839b
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Configuration Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies a user-specified configuration consisting of existing and hypothetical physical design structures for the Database Engine Tuning Advisor to analyze when tuning a workload.  
  
## Syntax  
  
```  
  
<DTAInput>  
    <Server>...</Server>  
    <Workload>...</Workload>  
    <TuningOptions>...</TuningOptions  
    <Configuration [SpecificationMode="Relative" | "Absolute"]>  
    ...code removed here...  
    </Configuration>  
</DTAInput>  
```  
  
## Element Attributes  
  
|Configuration Attribute|Description|  
|-----------------------------|-----------------|  
|**SpecificationMode**|Optional. Specifies whether Database Engine Tuning Advisor should analyze the specified configuration in relation to the current existing configuration, or as a completely new, standalone one. Use a **string** data type to specify this attribute with one of the following allowed values:<br /><br /> **Relative**:<br />                  Evaluates the specified configuration in relation to the current existing configuration of physical design structures (indexes, indexed views, partitioning) in the database that is being tuned. For example:<br /><br /> `<Configuration SpecificationMode="Relative">`<br /><br /> **Absolute**:<br />                  Evaluates the specified configuration as a standalone configuration. When Absolute is specified, Database Engine Tuning Advisor does not consider the existing configuration. For example:<br /><br /> `<Configuration SpecificationMode="Absolute">`|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Optional. Can use once for each **DTAInput** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DTAInput Element &#40;DTA&#41;](../../tools/dta/dtainput-element-dta.md)|  
|**Child elements**|[Server Element for Configuration &#40;DTA&#41;](../../tools/dta/server-element-for-configuration-dta.md)|  
  
## Example  
 For a usage example of this element, see the [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
