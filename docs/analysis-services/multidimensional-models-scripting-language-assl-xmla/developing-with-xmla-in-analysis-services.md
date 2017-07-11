---
title: "Developing with XMLA in Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "XML for Analysis, data mining"
  - "commands [XML for Analysis]"
  - "data mining [XML for Analysis]"
  - "XMLA, data mining"
  - "XML for Analysis, Analysis Services tasks"
  - "XMLA, Analysis Services tasks"
ms.assetid: 54445ee7-720c-4683-99a6-e75b3dcca904
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Developing with XMLA in Analysis Services
  XML for Analysis (XMLA) is a SOAP-based XML protocol, designed specifically for universal data access to any standard multidimensional data source that can be accessed over an HTTP connection. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses XMLA as its only protocol when communicating with client applications. Fundamentally, all client libraries supported by Analysis Services formulate requests and responses in XMLA.  
  
 As a developer, you can use XMLA to integrate a client application with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], without any dependencies on the .NET Framework or COM interfaces. Application requirements that include hosting on a wide range of platforms can be satisfied by using XMLA and an HTTP connection to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is fully compliant with the 1.1 specification of XMLA, but also extends it to enable data definition, data manipulation, and data control support. Analysis Services extensions are referred to as the Analysis Services Scripting Language (ASSL). Using XMLA and ASSL together enables a broader set of functionality than what XMLA alone provides. For more information about ASSL, see [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Managing Connections and Sessions &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)|Describes how to connect to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, and how to manage sessions and statefulness in XMLA.|  
|[Handling Errors and Warnings &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/handling-errors-and-warnings-xmla.md)|Describes how [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] returns error and warning information for methods and commands in XMLA.|  
|[Defining and Identifying Objects &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/defining-and-identifying-objects-xmla.md)|Describes object identifiers and object references, and how to use identifiers and references within XMLA commands.|  
|[Managing Transactions &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-transactions-xmla.md)|Details how to use the [BeginTransaction](../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md), [CommitTransaction](../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md), and [RollbackTransaction](../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md) commands to explicitly define and manage a transaction on the current XMLA session.|  
|[Canceling Commands &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/canceling-commands-xmla.md)|Describes how to use the [Cancel](../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)command to cancel commands, sessions, and connections in XMLA.|  
|[Performing Batch Operations &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/performing-batch-operations-xmla.md)|Describes how to use the [Batch](../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command to run multiple XMLA commands, in serial or in parallel, either within the same transaction or as separate transactions, using a single XMLA [Execute](../../analysis-services/xmla/xml-elements-methods-execute.md) method.|  
|[Creating and Altering Objects &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/creating-and-altering-objects-xmla.md)|Describes how to use the [Create](../../analysis-services/xmla/xml-elements-commands/create-element-xmla.md), [Alter](../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md), and [Delete](../../analysis-services/xmla/xml-elements-commands/delete-element-xmla.md) commands, along with Analysis Services Scripting Language (ASSL) elements, to define, change, or remove objects from an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.|  
|[Locking and Unlocking Databases &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/locking-and-unlocking-databases-xmla.md)|Details how to use the [Lock](../../analysis-services/xmla/xml-elements-commands/lock-element-xmla.md) and [Unlock](../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md) commands to lock and unlock an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.|  
|[Processing Objects &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md)|Describes how to use the [Process](../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) command to process an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object.|  
|[Merging Partitions &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/merging-partitions-xmla.md)|Describes how to use the [MergePartitions](../../analysis-services/xmla/xml-elements-commands/mergepartitions-element-xmla.md) command to merge partitions on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.|  
|[Designing Aggregations &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/designing-aggregations-xmla.md)|Describes how to use the [DesignAggregations](../../analysis-services/xmla/xml-elements-commands/designaggregations-element-xmla.md) command, either in iterative or batch mode, to design aggregations for an aggregation design in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|[Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md)|Describes how to use the [Backup](../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md) and [Restore](../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) commands to back up and restore an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from a backup file.<br /><br /> Also describes how to use the [Synchronize](../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command to synchronize an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database with an existing database on the same instance or on a different instance.|  
|[Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md)|Describes how to use the [Insert](../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md), [Update](../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md), and [Drop](../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md) commands to add, change, or delete members from a write-enabled dimension.|  
|[Updating Cells &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/updating-cells-xmla.md)|Describes how to use the [UpdateCells](../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md) command to change the values of cells in a write-enabled partition.|  
|[Managing Caches &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-caches-xmla.md)|Details how to use the [ClearCache](../../analysis-services/xmla/xml-elements-commands/clearcache-element-xmla.md) command to clear the caches of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.|  
|[Monitoring Traces &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/monitoring-traces-xmla.md)|Describes how to use the [Subscribe](../../analysis-services/xmla/xml-elements-commands/subscribe-element-xmla.md) command to subscribe to and monitor an existing trace on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.|  
  
## Data Mining with XMLA  
 XML for Analysis fully supports data mining schema rowsets. These rowsets provide information for querying data mining models using the [Discover](../../analysis-services/xmla/xml-elements-methods-discover.md) method. For more information about data mining schema rowsets, see [Data Mining Schema Rowsets](../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
 For more information about DMX, see [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md).  
  
## Namespace and Schema  
  
### Namespace  
 The schema defined in this specification uses the XML namespace `http://schemas.microsoft.com/AnalysisServices/2003/Engine` and the standard abbreviation “DDL.”  
  
### Schema  
 The definition of an XML Schema definition language (XSD) schema for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object definition language is based on the definition of the schema elements and hierarchy in this section.  
  
## Extensibility  
 Extensibility of the object definition language schema is provided by means of an **Annotation** element that is included on all objects. This element can contain any valid XML from any XML namespace (other than the target namespace that defines the DDL), subject to the following rules:  
  
-   The XML can contain only elements.  
  
-   Each element must have a unique name. It is recommended that the value of **Name** reference the target namespace.  
  
 These rules are imposed so that the contents of the **Annotation** tag can be exposed as a set of Name/Value pairs through Decision Support Objects (DSO) 9.0.  
  
 Comments and white space within the **Annotation** tag that are not enclosed with a child element may not be preserved. In addition, all elements must be read-write; read-only elements are ignored.  
  
 The object definition language schema is closed, in that the server does not allow substitution of derived types for elements defined in the schema. Therefore, the server accepts only the set of elements defined here, and no other elements or attributes. Unknown elements cause the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] engine to raise an error.  
  
## See Also  
 [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md)   
 [Understanding Microsoft OLAP Architecture](../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-architecture.md)  
  
  