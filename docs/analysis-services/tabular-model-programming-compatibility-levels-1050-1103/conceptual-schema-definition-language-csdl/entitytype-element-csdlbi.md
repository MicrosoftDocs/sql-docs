---
title: "EntityType Element (CSDLBI) | Microsoft Docs"
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
ms.assetid: 372e2c13-ec38-4bb1-981c-50758d59a1da
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EntityType Element (CSDLBI)
  The **EntityType** element is a complex type that represents the structure of a high-level entity, such as a customer or order, in a data model. The **bi:EntityType** element extends the definition of [EntityType](http://msdn.microsoft.com/library/bb399206.aspx) used in the [Entity Data Framework](http://msdn.microsoft.com/library/bb399567.aspx).  
  
 An EntityType element must be specified for each of the entities that are included in the data model. The subelements of the EntityType describe the columns and measures in the table. Relationships among tables are included in the **EntityContainer**.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the **EntityType** element. Also see the attributes applicable to the [EntityType](http://msdn.microsoft.com/library/bb399206.aspx) element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Contents|No|A string that contains the possible types of data in a column. The value is derived from the value of  DimensionAttributeTypeEnumType in the data model.<br /><br /> If the value of DimensionAttributeTypeEnumType is "ExtendedType", the value of Contents is derived from the ExtendedType element of DimensionAttribute. The client is not required to respond to these values.|  
|DefaultDetails|No|A list of property references that represent the set of columns in the table.<br /><br /> See [DefaultDetails Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/defaultdetails-element-csdlbi.md).|  
|DefaultImage|No|A reference to a column that contains the image that illustrates the entity.<br /><br /> In multidimensional models, this element corresponds to a binary attribute on the dimension attribute. If this attribute is present, the element must contain exactly one MemberRef element.<br /><br /> See [MemberRef Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/memberref-element-csdlbi.md).|  
|DefaultMeasure|No|A reference to a measure in the entity that should be used as the default when making calculations over the entity. If not specified, the default is SUM.<br /><br /> See [MemberRef Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/memberref-element-csdlbi.md).|  
|DisplayKey|No|A list of references to columns or to role ends that constitutes a strong identifier that uniquely identifies an entity instance.<br /><br /> See [DisplayKey Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/displaykey-element-csdlbi.md).|  
|Hierarchy|No|A list of hierarchies in the model.<br /><br /> See [Hierarchy Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/hierarchy-element-csdlbi.md).|  
|ReferenceName|Yes|An identifier that can be used to reference this entity in a Data Analysis Expressions (DAX) query.<br /><br /> If this attribute is not present, the fully qualified field name of the entity is used.|  
|SortMembers|No|A list of properties on which to sort. The SortDirection attribute indicates whether the order is ascending or descending.|  
  
## Contents Element  
 The **Contents** element is a simple type that describes the type of data in the entity.  
  
 The contents of the entity (column) can be any of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|Regular|Not otherwise defined.|  
|Time|Attributes represent time periods, such as years, semesters, quarters, months, or days.|  
|Geography|Attributes represent geographic information, such as cities or postal codes.|  
|Organization|Attributes represent organizational information, such as employees or subsidiaries.|  
|BillOfMaterials|Attributes represent inventory or manufacturing information, such as a parts lists for products.|  
|Accounts|Attributes represent a chart of accounts for financial reporting.|  
|Customers|Attributes represent customer or contact information.|  
|Products|Attributes represent product information.|  
|Scenario|Attributes represent planning or strategic analysis information.|  
|Quantitative|Attributes represent quantitative information.|  
|Utility|Attributes represent miscellaneous information.|  
|Currency|Contains currency data and metadata.|  
|Rates|Attributes represent currency rate information.|  
|Channel|Attributes represent channel information.|  
|Promotion|Attributes represent marketing promotion information.|  
  
## Example  
 **Tabular**  
  
 The following sample shows part of the CSDLBI version 1.1 representation of the Geography table used in the AdventureWorks tabular model. The RowNumber column is a hidden column that is generated automatically as a row identifier in tabular models, and hence has the Contents attribute, **RowNumber**.  
  
```  
  
<EntityType   
     Name="DimGeography">  
     <Key>  
        <PropertyRef Name="RowNumber" />  
     </Key>  
     <Property   
        Name="RowNumber"   
        Type="Int64" Nullable="false">  
     <bi:Property   
        Hidden="true"   
        Contents="RowNumber"   
        Stability="RowNumber" />  
     </Property>  
....  
  
```  
  
## Example  
 **Multidimensional**  
  
 The following sample shows the EntityType elements in CSDLBI version 1.1 that represent a portion of a time dimension from the Contoso Operations cube.  
  
```  
<EntityType   
       Name="CalendarQuarter">  
    <Key>  
       <PropertyRef Name="RowNumber" />  
    </Key>  
  
    <Property Name="RowNumber"   
       Type="Int64"   
       Nullable="false">  
    <bi:Property   
       Hidden="true"   
       Contents="RowNumber"   
       Stability="RowNumber"   
    />  
    </Property>  
  
    <Property Name="CalendarQuarter2"   
       Type="String"   
       MaxLength="Max"   
       Unicode="true"   
       FixedLength="false"   
       Nullable="false">  
    <bi:Property   
       Caption="CalendarQuarter"   
       ReferenceName="CalendarQuarter"   
    />  
    </Property>  
   <bi:EntityType />  
</EntityType>  
```  
  
## See Also  
 [Technical Reference for BI Annotations to CSDL](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/technical-reference-for-bi-annotations-to-csdl.md)  
  
  