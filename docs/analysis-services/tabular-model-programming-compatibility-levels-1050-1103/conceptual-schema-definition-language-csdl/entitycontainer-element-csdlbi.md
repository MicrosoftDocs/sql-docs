---
title: "EntityContainer Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
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
ms.assetid: e328558e-16b0-4d4a-a79a-fdd3c9493595
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EntityContainer Element (CSDLBI)
  The EntityContainer element is a complex type, based on the CSDL type, EntityContainer, that defines a collection of entities within a single data model. In a business intelligence application, the data model represented by an EntityContainer might contain multiple tables with column linked by relationships, as well as calculations, measures, and KPIs. It is conceptually similar to a database or data source.  
  
 The EntityContainer must specify each of the entity types that are included in the data model, including tables and relationships. Information about these model entities is specified by listing child entities of the type, Entity element. For more information, see [EntityType Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/entitytype-element-csdlbi.md).  
  
 Properties such as collation and language are defined at the level of the EntityContainer, not on individual objects. However, columns and text attributes within the model can have captions or translations in other languages.  
  
## Elements and Attributes  
 The table below describes the elements and attributes that define the EntityContainer.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Name|Yes|The name of the data model.|  
|Caption|No|A description of the database or data model.|  
|Culture|Yes|A string that contains the LCID of the request.|  
|CompareOptions|Yes|Language-specific sorting and string comparison options for the model.|  
|DirectQueryMode|No|An enumeration that indicates the query mode when the model is using DirectQuery mode.|  
|EntitySet element|Yes|[EntitySet Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/entityset-element-csdlbi.md)|  
|AssociationSet element|No|[AssociationSet Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/associationset-element-csdlbi.md)|  
  
## CompareOptions Element  
 The CompareOptions attribute defines collation properties that are applied to the data model. The properties defined by CompareOptions are derived from settings for sort order, kana sensitivity, and case sensitivity that are set in the Analysis Services database at model design time. The following table describes the values that are included as part of the CompareOptions attribute.  
  
|Value|Description|  
|-----------|-----------------|  
|IgnoreCase|Boolean value that indicates whether string comparisons should ignore case.|  
|IgnoreNonSpace|Boolean value that indicates whether string comparisons should ignore non-spacing combining characters, such as diacritics.|  
|IgnoreKanaType|Boolean value that indicates whether string comparisons should ignore kana type.|  
|IgnoreWidth|Boolean value that indicates whether string comparisons should ignore character width.|  
  
## DirectQueryMode  
 **DirectQueryMode**  
  
 The simple type, DirectQueryMode, defines the type of query that is used by default when the model is enabled to directly retrieve data from a relational data source. This property is applicable only to tabular models running in DirectQuery mode. The following table lists the possible values for the DirectQuery mode enumeration.  
  
|Value|Description|  
|-----------|-----------------|  
|InMemory|Indicates that queries against the model will use data in the cache.|  
|InMemoryWithDirectQuery|Indicates that, by default, queries against the model will use data from the relational data source.|  
|DirectQueryWithInMemory|Indicates that, by default, queries against the model will use data in the cache.|  
|DirectQuery|Indicates that queries against the model will use data in the relational data source only.|  
  
## Example  
 **Tabular**  
  
 The following sample, in CSDLBI version 1.1, represents a portion of the AdventureWorks tabular data model.  
  
```  
  
<EntityContainer   
     Name="Sandbox">  
   <EntitySet   
       Name="DimEmployee"   
       EntityType="Sandbox.DimEmployee">  
   <bi:EntitySet />  
   </EntitySet>  
  
   <EntitySet   
     Name="DimProduct"   
       EntityType="Sandbox.DimProduct">  
    <bi:EntitySet />  
    </EntitySet>  
  
<bi:EntityContainer Caption="AWSimple" Culture="en-US">  
  
```  
  
## Example  
 **Multidimensional**  
  
 The following sample, in CSDLBI version 1.1, is an excerpt from the Contoso Operations cube.  
  
```  
  
<EntityContainer   
     Name="Sandbox">  
   <EntitySet   
      Name="Bike"   
      EntityType="Sandbox.Bike">  
    <bi:EntitySet Hidden="true" />  
   </EntitySet>  
…  
<bi:EntityContainer   
   Caption="CSDLTest"   
   Culture="en-US">  
<bi:CompareOptions   
   IgnoreCase="true" />  
</bi:EntityContainer>  
</EntityContainer>  
  
```  
  
## See Also  
 [EntitySet Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/entityset-element-csdlbi.md)  
  
  