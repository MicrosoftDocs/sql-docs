---
title: "Batch Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Batch Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Batch"
  - "microsoft.xml.analysis.batch"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Batch"
helpviewer_keywords: 
  - "Batch command"
ms.assetid: 818f3212-9605-4e34-8623-1154d9fae1f0
caps.latest.revision: 14
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Batch Element (XMLA)
  Performs one or more XML for Analysis (XMLA) commands as a batch operation, either sequentially or in parallel, on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Command>  
   <Batch Transaction="Boolean" ProcessAffectedObjects="Boolean">  
      <Bindings>...</Bindings>  
      <DataSource>...</DataSource>  
      <DataSourceView>...</DataSourceView>  
      <ErrorConfiguration>...</ErrorConfiguration>  
      <Parallel>...</Parallel>  
      <!-- One or more XMLA commands -->  
   </Batch>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../2014/analysis-services/dev-guide/command-element-xmla.md)|  
|Child elements|[Bindings](../../../2014/analysis-services/dev-guide/bindings-element-xmla.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-xmla.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-xmla.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md), [Parallel](../../../2014/analysis-services/dev-guide/parallel-element-xmla.md)<br /><br /> One or more of the following XMLA commands: [Alter](../../../2014/analysis-services/dev-guide/alter-element-xmla.md), [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [BeginTransaction](../../../2014/analysis-services/dev-guide/begintransaction-element-xmla.md), [ClearCache](../../../2014/analysis-services/dev-guide/clearcache-element-xmla.md), [CommitTransaction](../../../2014/analysis-services/dev-guide/committransaction-element-xmla.md), [Create](../../../2014/analysis-services/dev-guide/create-element-xmla.md), [Delete](../../../2014/analysis-services/dev-guide/delete-element-xmla.md), [DesignAggregations](../../../2014/analysis-services/dev-guide/designaggregations-element-xmla.md), [Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md), [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md), [MergePartitions](../../../2014/analysis-services/dev-guide/mergepartitions-element-xmla.md), [NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md), [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md), [RollbackTransaction](../../../2014/analysis-services/dev-guide/rollbacktransaction-element-xmla.md), [SetPasswordEncryptionKey](http://msdn.microsoft.com/en-us/fb262737-f0f4-4441-985e-3b2a94d00a9e), [Statement](../../../2014/analysis-services/dev-guide/statement-element-xmla.md), [Subscribe](../../../2014/analysis-services/dev-guide/subscribe-element-xmla.md), [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md), [Unlock](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md), [UpdateCells](../../../2014/analysis-services/dev-guide/drop-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ProcessAffectedObjects|(Optional `Boolean` attribute) Indicates whether all objects that require reprocessing will be processed.<br /><br /> If set to true, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance processes any objects that require reprocessing as a result of processing an object included in the `Batch` command.<br /><br /> If set to `false`, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance processes only those objects included in the `Batch` command.|  
|Transaction|(Optional `Boolean` attribute) Indicates whether the command included in the `Batch` command are treated as a single transaction or individual transactions.<br /><br /> If set to true, all of the commands included in the `Batch` command are considered a single transaction. If any command fails, the commands executed prior to the failed command are rolled back, and the `Batch` command stops without executing subsequent commands.<br /><br /> If set to `false`, the `Batch` command attempts to execute every command, and commits the results of each command that completes successfully.|  
  
## Remarks  
  
> [!WARNING]  
>  Command/Execute/Statement is currently not supported in a Batch operation.  
  
 For more information about performing batch operations in XMLA, see [Performing Batch Operations &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/performing-batch-operations-xmla.md).  
  
## See Also  
 [Commands &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/commands-xmla.md)  
  
  