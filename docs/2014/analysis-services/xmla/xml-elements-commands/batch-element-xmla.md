---
title: "Batch Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# Batch Element (XMLA)
  Performs one or more XML for Analysis (XMLA) commands as a batch operation, either sequentially or in parallel, on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
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
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Bindings](../xml-elements-properties/bindings-element-xmla.md), [DataSource](../xml-elements-properties/source-element-xmla.md), [DataSourceView](../xml-elements-properties/datasourceview-element-xmla.md), [ErrorConfiguration](../../scripting/objects/errorconfiguration-element-assl.md), [Parallel](../xml-elements-properties/parallel-element-xmla.md)<br /><br /> One or more of the following XMLA commands: [Alter](alter-element-xmla.md), [Backup](backup-element-xmla.md), [BeginTransaction](begintransaction-element-xmla.md), [ClearCache](clearcache-element-xmla.md), [CommitTransaction](committransaction-element-xmla.md), [Create](create-element-xmla.md), [Delete](delete-element-xmla.md), [DesignAggregations](designaggregations-element-xmla.md), [Drop](drop-element-xmla.md), [Insert](insert-element-xmla.md), [Lock](lock-element-xmla.md), [MergePartitions](mergepartitions-element-xmla.md), [NotifyTableChange](notifytablechange-element-xmla.md), [Process](process-element-xmla.md), [Restore](restore-element-xmla.md), [RollbackTransaction](rollbacktransaction-element-xmla.md), [SetPasswordEncryptionKey](http://msdn.microsoft.com/fb262737-f0f4-4441-985e-3b2a94d00a9e), [Statement](statement-element-xmla.md), [Subscribe](subscribe-element-xmla.md), [Synchronize](synchronize-element-xmla.md), [Unlock](unlock-element-xmla.md), [Update](update-element-xmla.md), [UpdateCells](drop-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ProcessAffectedObjects|(Optional `Boolean` attribute) Indicates whether all objects that require reprocessing will be processed.<br /><br /> If set to true, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance processes any objects that require reprocessing as a result of processing an object included in the `Batch` command.<br /><br /> If set to `false`, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance processes only those objects included in the `Batch` command.|  
|Transaction|(Optional `Boolean` attribute) Indicates whether the command included in the `Batch` command are treated as a single transaction or individual transactions.<br /><br /> If set to true, all of the commands included in the `Batch` command are considered a single transaction. If any command fails, the commands executed prior to the failed command are rolled back, and the `Batch` command stops without executing subsequent commands.<br /><br /> If set to `false`, the `Batch` command attempts to execute every command, and commits the results of each command that completes successfully.|  
  
## Remarks  
  
> [!WARNING]  
>  Command/Execute/Statement is currently not supported in a Batch operation.  
  
 For more information about performing batch operations in XMLA, see [Performing Batch Operations &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/performing-batch-operations-xmla.md).  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
