---
title: "StorageBoundInMB Element (DTA)"
description: In the dta utility, the StorageBoundInMB element specifies the maximum space that can be consumed by the Database Engine Tuning Advisor tuning recommendation.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "StorageBoundInMB element"
ms.assetid: a8374910-bf68-4edb-b464-53a3a705e7f4
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# StorageBoundInMB Element (DTA)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the maximum space in megabytes that can be consumed by the Database Engine Tuning Advisor tuning recommendation (index and partitioning set).  
  
## Syntax  
  
```  
  
<DTAInput>  
...code removed...  
    <TuningOptions>  
      <StorageBoundInMB>...</ StorageBoundInMB >  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**unsignedInt**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Optional. Can only be used once for the **TuningOptions** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](../../tools/dta/tuningoptions-element-dta.md)|  
|**Child elements**|None|  
  
## Remarks  
 When multiple databases are tuned, recommendations for all databases are considered for the space calculation. By default, Database Engine Tuning Advisor assumes the smaller of the following storage sizes:  
  
-   Three times the current raw data size, which includes the total size of heaps and clustered indexes on tables.  
  
-   The free space on all attached disk drives plus the raw data size.  
  
 The default storage size does not include nonclustered indexes and indexed views.  
  
 If the value specified for the **StorageBoundInMB** element exceeds the actual disk space, Database Engine Tuning Advisor returns an error, but continues tuning. After tuning is complete, you can add disk space if you decide to implement the recommendation.  
  
## Example  
  
## Description  
 The following code example shows how to set a limit of 1500 megabytes as the maximum disk space that a tuning recommendation can consume:  
  
## Code  
  
```  
<DTAInput>  
  <Server>...</Server>  
  <Workload>...</Workload>  
  <TuningOptions>  
    <StorageBoundInMB>1500</StorageBoundInMB>  
...code removed here...  
</DTAInput>  
```  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
