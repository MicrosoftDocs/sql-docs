---
title: "DropOnlyMode Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "DropOnlyMode element"
ms.assetid: 80960676-7581-4074-889b-80ee665963dd
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# DropOnlyMode Element (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Specifies that Database Engine Tuning Advisor should only consider dropping existing indexes, indexed views, or partitions during the tuning session. No new physical design structures are considered when this tuning option is specified.  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <DropOnlyMode>...</DropOnlyMode>  
```  
  
## Element Characteristics  
 **Data type and length**  
  
 **Default value**  
  
 **Occurrence**: Optional. Can be used only once for each **TuningOptions** element. Cannot be used if the following elements are specified in the **TuningOptions** element:  
  
-   [FeatureSet Element &#40;DTA&#41;](../../tools/dta/featureset-element-dta.md)  
  
-   [Partitioning Element &#40;DTA&#41;](../../tools/dta/partitioning-element-dta.md)  
  
-   [KeepExisting Element &#40;DTA&#41;](../../tools/dta/keepexisting-element-dta.md) is set to **ALL**  
  
## Element Relationships  
 **Parent element**: [TuningOptions Element &#40;DTA&#41;](../../tools/dta/tuningoptions-element-dta.md)  
  
 **Child elements**  
  
## Example  
 The following example shows the **TuningOptions** section of a Database Engine Tuning Advisor XML input file where the **DropOnlyMode** is specified. In this example the tuning time is limited to 24 hours (1440 minutes) and all existing clustered and nonclustered indexes will be considered for dropping:  
  
```xml  
<TuningOptions  
  <TuningTimeInMin>1440</Name>  
  <KeepExisting>ALIGNED</KeepExisting>  
  <DropOnlyMode />  
</TuningOptions>  
```  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
