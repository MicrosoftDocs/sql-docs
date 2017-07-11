---
title: "Batch Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Batch Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Batch"
  - "microsoft.xml.analysis.batch"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Batch"
helpviewer_keywords: 
  - "Batch command"
ms.assetid: 818f3212-9605-4e34-8623-1154d9fae1f0
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
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
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Bindings](../../../analysis-services/xmla/xml-elements-properties/bindings-element-xmla.md), [DataSource](../../../analysis-services/xmla/xml-elements-properties/datasource-element-xmla.md), [DataSourceView](../../../analysis-services/xmla/xml-elements-properties/datasourceview-element-xmla.md), [ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md), [Parallel](../../../analysis-services/xmla/xml-elements-properties/parallel-element-xmla.md)<br /><br /> One or more of the following XMLA commands: [Alter](../../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md), [Backup](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md), [BeginTransaction](../../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md), [ClearCache](../../../analysis-services/xmla/xml-elements-commands/clearcache-element-xmla.md), [CommitTransaction](../../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md), [Create](../../../analysis-services/xmla/xml-elements-commands/create-element-xmla.md), [Delete](../../../analysis-services/xmla/xml-elements-commands/delete-element-xmla.md), [DesignAggregations](../../../analysis-services/xmla/xml-elements-commands/designaggregations-element-xmla.md), [Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md), [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md), [Lock](../../../analysis-services/xmla/xml-elements-commands/lock-element-xmla.md), [MergePartitions](../../../analysis-services/xmla/xml-elements-commands/mergepartitions-element-xmla.md), [NotifyTableChange](../../../analysis-services/xmla/xml-elements-commands/notifytablechange-element-xmla.md), [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md), [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md), [RollbackTransaction](../../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md), [SetPasswordEncryptionKey](http://msdn.microsoft.com/en-us/fb262737-f0f4-4441-985e-3b2a94d00a9e), [Statement](../../../analysis-services/xmla/xml-elements-commands/statement-element-xmla.md), [Subscribe](../../../analysis-services/xmla/xml-elements-commands/subscribe-element-xmla.md), [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md), [Unlock](../../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md), [UpdateCells](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ProcessAffectedObjects|(Optional **Boolean** attribute) Indicates whether all objects that require reprocessing will be processed.<br /><br /> If set to true, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance processes any objects that require reprocessing as a result of processing an object included in the **Batch** command.<br /><br /> If set to **false**, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance processes only those objects included in the **Batch** command.|  
|Transaction|(Optional **Boolean** attribute) Indicates whether the command included in the **Batch** command are treated as a single transaction or individual transactions.<br /><br /> If set to true, all of the commands included in the **Batch** command are considered a single transaction. If any command fails, the commands executed prior to the failed command are rolled back, and the **Batch** command stops without executing subsequent commands.<br /><br /> If set to **false**, the **Batch** command attempts to execute every command, and commits the results of each command that completes successfully.|  
  
## Remarks  
  
> [!WARNING]  
>  Command/Execute/Statement is currently not supported in a Batch operation.  
  
 For more information about performing batch operations in XMLA, see [Performing Batch Operations &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/performing-batch-operations-xmla.md).  
  
## See Also  
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  