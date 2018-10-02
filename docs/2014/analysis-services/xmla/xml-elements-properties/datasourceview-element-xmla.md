---
title: "DataSourceView Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSourceView Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DataSourceView"
  - "microsoft.xml.analysis.datasourceview"
  - "urn:schemas-microsoft-com:xml-analysis#DataSourceView"
helpviewer_keywords: 
  - "DataSourceView element"
ms.assetid: c4a4360f-7342-484b-bac1-0a247e8f279d
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceView Element (XMLA)
  Contains an out-of-line data source view binding for the parent [Batch](../xml-elements-commands/batch-element-xmla.md) or [Process](../xml-elements-commands/process-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Batch> <!-- or Process>  
...  
   <DataSourceView>  
      <DatabaseID>...</DatabaseID>  
      <DataSourceViewID>...</DataSourceViewID>  
   </DataSourceView>  
...  
</Batch>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Batch](../xml-elements-commands/batch-element-xmla.md), [Process](../xml-elements-commands/process-element-xmla.md)|  
|Child elements|[DatabaseID](id-element-xmla.md), [DataSourceViewID](../../scripting/properties/id-element-assl.md)|  
  
## Remarks  
 The `DataSourceView` element represents an out-of-line binding to a data source view, used by the `Batch` or `Process` command to temporarily override the data source view binding for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects processed by the command.  
  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
