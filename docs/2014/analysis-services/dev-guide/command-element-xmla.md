---
title: "Command Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Command Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.command"
  - "Command"
  - "urn:schemas-microsoft-com:xml-analysis#Command"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Command"
helpviewer_keywords: 
  - "Command element"
ms.assetid: 9abc14df-7cbe-46bc-ba0f-f0691c19afad
caps.latest.revision: 31
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Command Element (XMLA)
  Contains the command to be executed by the [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method.  
  
## Syntax  
  
```xml  
  
<Execute>  
   ...  
   <Command>  
      <Alter>...</Alter>  
      <!-- or -->  
      <Backup>...</Backup>  
      <!-- or -->  
      <Batch>...</Batch>  
      <!-- or -->  
      <BeginTransaction>...</BeginTransaction>  
      <!-- or -->  
      <Cancel>...</Cancel>  
      <!-- or -->  
      <ClearCache>...</ClearCache>  
      <!-- or -->  
      <CommitTransaction>...</CommitTransaction>  
      <!-- or -->  
      <Create>...</Create>  
      <!-- or -->  
      <Delete>...</Delete>  
      <!-- or -->  
      <DesignAggregations>...</DesignAggregations>  
      <!-- or -->  
      <Drop>...</Drop>  
      <!-- or -->  
      <Insert>...</Insert>  
      <!-- or -->  
      <Lock>...</Lock>  
      <!-- or -->  
      <MergePartitions>...</MergePartitions>  
      <!-- or -->  
      <NotifyTableChange>...</NotifyTableChange>  
      <!-- or -->  
      <Process>...</Process>  
      <!-- or -->  
      <Restore>...</Restore>  
      <!-- or -->  
      <RollbackTransaction>...</RollbackTransaction>  
      <!-- or -->  
      <SetPasswordEncryptionKey>...</SetPasswordEncryptionKey>  
      <!-- or -->  
      <Statement>...</Statement>  
      <!-- or -->  
      <Subscribe>...</Subscribe>  
      <!-- or -->  
      <Synchronize>...</Synchronize>  
      <!-- or -->  
      <Unlock>...</Unlock>  
      <!-- or -->  
      <Update>...</Update>  
      <!-- or -->  
      <UpdateCells>...</UpdateCells>  
   </Command>  
   ...  
</Execute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md)|  
|Child elements|[Alter](../../../2014/analysis-services/dev-guide/alter-element-xmla.md), [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md), [BeginTransaction](../../../2014/analysis-services/dev-guide/begintransaction-element-xmla.md), [Cancel](../../../2014/analysis-services/dev-guide/cancel-element-xmla.md), [ClearCache](../../../2014/analysis-services/dev-guide/clearcache-element-xmla.md), [CommitTransaction](../../../2014/analysis-services/dev-guide/committransaction-element-xmla.md), [Create](../../../2014/analysis-services/dev-guide/create-element-xmla.md), [Delete](../../../2014/analysis-services/dev-guide/delete-element-xmla.md), [DesignAggregations](../../../2014/analysis-services/dev-guide/designaggregations-element-xmla.md), [Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md), [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md), [MergePartitions](../../../2014/analysis-services/dev-guide/mergepartitions-element-xmla.md), [NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md), [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md), [RollbackTransaction](../../../2014/analysis-services/dev-guide/rollbacktransaction-element-xmla.md), [SetPasswordEncryptionKey](http://msdn.microsoft.com/en-us/fb262737-f0f4-4441-985e-3b2a94d00a9e), [Statement](../../../2014/analysis-services/dev-guide/statement-element-xmla.md), [Subscribe](../../../2014/analysis-services/dev-guide/subscribe-element-xmla.md), [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md), [Unlock](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md), [UpdateCells](../../../2014/analysis-services/dev-guide/drop-element-xmla.md)|  
  
## Remarks  
 The `Command` element is used by the `Execute` method to relay commands to a data source. While the XML for Analysis (XMLA) 1.1 Specification supports only the `Statement` command, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports many new XMLA commands. For more information about the XMLA command supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], see [Commands &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/commands-xmla.md).  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/xml-data-types-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  