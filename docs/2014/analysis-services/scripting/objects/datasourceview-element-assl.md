---
title: "DataSourceView Element (ASSL) | Microsoft Docs"
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
  - "DataSourceView"
helpviewer_keywords: 
  - "DataSourceView element"
ms.assetid: cda26126-8af2-4519-8237-f4a57976a284
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceView Element (ASSL)
  Defines a data source view used by a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] [Database](database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataSourceViews>  
   <DataSourceView>  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Description>...</Description>  
      <Annotations>...</Annotations>  
      <DataSourceID>   </DataSourceID>  
      <Schema>...</Schema>  
   </DataSourceView>  
</DataSourceViews>  
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
|Parent elements|[DataSourceViews](../collections/datasourceviews-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [DataSourceID](../properties/id-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [Name](../properties/name-element-assl.md), [Schema](../properties/schema-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSourceView>.  
  
## See Also  
 [Database Element &#40;ASSL&#41;](database-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
