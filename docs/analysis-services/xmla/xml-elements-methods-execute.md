---
title: "Execute Method (XMLA) | Microsoft Docs"
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
  - "Execute Method"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EXECUTE"
  - "urn:schemas-microsoft-com:xml-analysis#"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#"
  - "microsoft.xml.analysis.execute"
  - "urn:schemas-microsoft-com:xml-analysis#Execute"
helpviewer_keywords: 
  - "Execute method"
ms.assetid: 0fff5221-7164-4bbc-ab58-49cf04c52664
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# XML Elements - Methods - Execute
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
|Child elements|[Command](../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md), [Parameters](../../analysis-services/xmla/xml-elements-properties/parameters-element-xmla.md), [Properties](../../analysis-services/xmla/xml-elements-properties/properties-element-xmla.md)|  
  
## Remarks  
 The **Execute** method executes XMLA commands provided in the **Command** element and returns any resulting data using either the XMLA [Rowset](../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md) data type (for tabular result sets) or the XMLA [MDDataSet](../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md) data type (for multidimensional result sets.)  
  
## Example  
 The following code sample is an example of an **Execute** method call that contains an Multidimensional Expressions (MDX) SELECT statement.  
  
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
 [XML Data Types &#40;XMLA&#41;](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)   
 [Discover Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-discover.md)   
 [Methods &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods.md)   
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)   
 [Analysis Services Schema Rowsets](../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  