---
title: "Execute Method (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Execute Method"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EXECUTE"
  - "urn:schemas-microsoft-com:xml-analysis#"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#"
  - "microsoft.xml.analysis.execute"
  - "urn:schemas-microsoft-com:xml-analysis#Execute"
helpviewer_keywords: 
  - "Execute method"
ms.assetid: 0fff5221-7164-4bbc-ab58-49cf04c52664
author: minewiskan
ms.author: owend
manager: craigg
---
# Execute Method (XMLA)
  Sends XML for Analysis (XMLA) commands to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This includes requests involving data transfer, such as retrieving or updating data on the server.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
 **SOAP Action** "urn:schemas-microsoft-com:xml-analysis:Execute"  
  
## Syntax  
  
```  
  
<Execute>  
   <Command>...</Command>  
   <Properties>...</Properties>  
   <Parameters>...</Parameters>  
</Execute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|None|  
|Child elements|[Command](xml-elements-properties/command-element-xmla.md), [Parameters](xml-elements-properties/parameters-element-xmla.md), [Properties](xml-elements-properties/properties-element-xmla.md)|  
  
## Remarks  
 The `Execute` method executes XMLA commands provided in the `Command` element and returns any resulting data using either the XMLA [Rowset](xml-data-types/rowset-data-type-xmla.md) data type (for tabular result sets) or the XMLA [MDDataSet](xml-data-types/mddataset-data-type-xmla.md) data type (for multidimensional result sets.)  
  
## Example  
 The following code sample is an example of an `Execute` method call that contains an Multidimensional Expressions (MDX) SELECT statement.  
  
```  
<Execute xmlns="urn:schemas-microsoft-com:xml-analysis">  
   <Command>  
      <Statement>  
         SELECT [Measures].MEMBERS ON COLUMNS FROM [Adventure Works]  
      </Statement>  
   </Command>  
   <Properties>  
      <PropertyList>  
         <DataSourceInfo>Provider=MSOLAP;Data Source=local;</DataSourceInfo>  
         <Catalog>Adventure Works DW Multidimensional 2012</Catalog>  
         <Format>Multidimensional</Format>  
         <AxisFormat>ClusterFormat</AxisFormat>  
      </PropertyList>  
   </Properties>  
</Execute>  
```  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](xml-data-types/xml-data-types-xmla.md)   
 [Discover Method &#40;XMLA&#41;](xml-elements-methods-discover.md)   
 [Methods &#40;XMLA&#41;](xml-elements-methods.md)   
 [XML Elements &#40;XMLA&#41;](../dev-guide/xml-elements-xmla.md)   
 [Analysis Services Schema Rowsets](../schema-rowsets/analysis-services-schema-rowsets.md)  
  
  
