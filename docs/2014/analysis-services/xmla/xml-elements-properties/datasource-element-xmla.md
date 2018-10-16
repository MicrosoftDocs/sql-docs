---
title: "DataSource Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSource Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#DataSource"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DataSource"
  - "microsoft.xml.analysis.datasource"
helpviewer_keywords: 
  - "DataSource element"
ms.assetid: adc0713a-3927-40f3-8b87-012130908f34
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSource Element (XMLA)
  Contains an out-of-line data source binding for the parent [Batch](../xml-elements-commands/batch-element-xmla.md) or [Process](../xml-elements-commands/process-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Batch> <!-- or Process>  
...  
   <DataSource>  
      <DatabaseID>...</DatabaseID>  
      <DataSourceID>...</DataSourceID>  
   </DataSource>  
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
|Child elements|[DatabaseID](id-element-xmla.md), [DataSourceID](datasourceid-element-xmla.md)|  
  
## Remarks  
 The `DataSource` element represents an out-of-line binding to a data source, used by the `Batch` or `Process` command to temporarily override the data source binding for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects processed by the command.  
  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
