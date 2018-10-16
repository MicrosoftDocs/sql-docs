---
title: "Discover Method (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Discover Method"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#"
  - "microsoft.xml.analysis.discover"
  - "urn:schemas-microsoft-com:xml-analysis#Discover"
  - "Discover"
helpviewer_keywords: 
  - "Discover method"
ms.assetid: 0eb52d88-c081-416e-a229-610e4373b0b3
author: minewiskan
ms.author: owend
manager: craigg
---
# Discover Method (XMLA)
  Retrieves information, such as the list of available databases or details about a specific object, from an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The data retrieved with the `Discover` method depends on the values of the parameters passed to it.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
 **SOAP Action** "urn:schemas-microsoft-com:xml-analysis:Discover"  
  
## Syntax  
  
```xml  
  
<Discover>  
   <RequestType>...</RequestType>  
   <Restrictions>...</Restrictions>  
   <Properties>...</Properties>  
</Discover>  
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
|Parent element|None|  
|Child elements|[Properties](xml-elements-properties/properties-element-xmla.md), [RequestType](xml-elements-properties/type-element-xmla.md), [Restrictions](xml-elements-properties/restrictions-element-xmla.md)|  
  
## Remarks  
 The `Discover` method requests metadata about [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances and objects. Metadata is returned using the XMLA [Rowset](xml-data-types/rowset-data-type-xmla.md) data type.  
  
## Example  
 In the following code sample, the client sends the `Discover` call to request a list of cubes from the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database:  
  
```  
<Discover xmlns="urn:schemas-microsoft-com:xml-analysis">  
   <RequestType>MDSCHEMA_CUBES</RequestType>  
   <Restrictions>  
      <RestrictionList>  
         <CATALOG_NAME>Adventure Works DW Multidimensional 2012</CATALOG_NAME>  
      </RestrictionList>  
   </Restrictions>  
   <Properties>  
      <PropertyList>  
         <DataSourceInfo>Provider=MSOLAP;Data Source=local;</DataSourceInfo>  
         <Catalog>Adventure Works DW Multidimensional 2012</Catalog>  
         <Format>Tabular</Format>  
      </PropertyList>  
   </Properties>  
</Discover>  
```  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](xml-data-types/xml-data-types-xmla.md)   
 [Execute Method &#40;XMLA&#41;](xml-elements-methods-execute.md)   
 [Methods &#40;XMLA&#41;](xml-elements-methods.md)   
 [XML Elements &#40;XMLA&#41;](../dev-guide/xml-elements-xmla.md)   
 [Analysis Services Schema Rowsets](../schema-rowsets/analysis-services-schema-rowsets.md)  
  
  
