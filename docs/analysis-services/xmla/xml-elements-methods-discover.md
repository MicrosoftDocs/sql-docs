---
title: "Discover Method (XMLA) | Microsoft Docs"
ms.date: 05/30/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# XML Elements - Methods - Discover
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Retrieves information, such as the list of available databases or details about a specific object, from an instance of Analysis Services. The data retrieved with the **Discover** method depends on the values of the parameters passed to it.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|None|  
|Child elements|[Properties](../../analysis-services/xmla/xml-elements-properties/properties-element-xmla.md), [RequestType](../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md), [Restrictions](../../analysis-services/xmla/xml-elements-properties/restrictions-element-xmla.md)|  
  
## Remarks  
 The **Discover** method requests metadata about instances and objects. Metadata is returned using the XMLA [Rowset](../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md) data type.  
 
> [!TIP] 
> If you are unfamiliar with XML commands, click the XMLA query template on the **Query** toolbar in Management Studio, to build the query and add parameters. For more information, see [Use Analysis Services Templates in SQL Server Management Studio](../../analysis-services/instances/use-analysis-services-templates-in-sql-server-management-studio.md). 
  
## Example  
 In the following code sample, the client sends the **Discover** call to request a list of cubes from the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample Analysis Services database:  
  
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
  
## See also
 [XML Data Types &#40;XMLA&#41;](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)   
 [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md)   
 [Methods &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods.md)   
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)   
 [Analysis Services Schema Rowsets](../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  
