---
title: "TuningOptions Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "TuningOptions element"
ms.assetid: 58a22ba1-8e03-411f-bd46-85e4540f217a
author: stevestein
ms.author: sstein
manager: craigg
---
# TuningOptions Element (DTA)
  Contains the tuning options for a specific tuning session.  
  
## Syntax  
  
```  
  
<DTAInput>  
    <Server>  
...code removed...  
    <Workload>...</Workload>  
    <TuningOptions>...</TuningOptions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Optional. If used, can only be used once for each `DTAInput` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[DTAInput Element &#40;DTA&#41;](dtainput-element-dta.md)|  
|**Child elements**|`ReportSet` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> `TuningLogTable` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> `NumberOfEvents` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> [TuningTimeInMin Element &#40;DTA&#41;](tuningtimeinmin-element-dta.md)<br /><br /> [StorageBoundInMB Element &#40;DTA&#41;](storageboundinmb-element-dta.md)<br /><br /> `MaxKeyColumnsInIndex` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> `MaxColumnsInIndex` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> `MinPercentageImprovement` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100)<br /><br /> [TestServer Element &#40;DTA&#41;](server-element-dta.md)<br /><br /> [FeatureSet Element &#40;DTA&#41;](featureset-element-dta.md)<br /><br /> [Partitioning Element &#40;DTA&#41;](partitioning-element-dta.md)<br /><br /> [DropOnlyMode Element &#40;DTA&#41;](droponlymode-element-dta.md)<br /><br /> [KeepExisting Element &#40;DTA&#41;](keepexisting-element-dta.md)<br /><br /> [OnlineIndexOperation Element &#40;DTA&#41;](onlineindexoperation-element-dta.md)<br /><br /> [DatabaseToConnect Element &#40;DTA&#41;](databasetoconnect-element-dta.md)<br /><br /> `IgnoreConstantsInWorkload` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).<br /><br /> `RetainShellDB` element. For more information, see the [Database Engine Tuning Advisor XML schema](https://go.microsoft.com/fwlink/?linkid=43100).|  
  
## Example  
 For examples of the `TuningOptions` element, see the [XML Input File Samples &#40;DTA&#41;](xml-input-file-samples-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
