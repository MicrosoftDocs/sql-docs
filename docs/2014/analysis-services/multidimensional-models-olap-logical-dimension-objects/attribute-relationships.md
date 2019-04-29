---
title: "Attribute Relationships | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "member properties [Analysis Services], attribute relationships"
  - "security [Analysis Services], properties"
  - "PROPERTIES keyword"
  - "storage [Analysis Services], attribute relationships"
  - "natural hierarchies [Analysis Services]"
  - "dimensions [Analysis Services], member properties"
  - "queries [MDX], attribute relationships"
  - "user-defined hierarchies [Analysis Services]"
  - "attributes [Analysis Services], relationships"
  - "member properties [Analysis Services]"
  - "members [Analysis Services], attribute relationships"
  - "storing data [Analysis Services], attribute relationships"
  - "relationships [Analysis Services], attributes"
ms.assetid: 2491422a-4cf5-4b23-b6ab-289222b22ce8
author: minewiskan
ms.author: owend
manager: craigg
---
# Attribute Relationships
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], attributes within a dimension are always related either directly or indirectly to the key attribute. When you define a dimension based on a star schema, which is where all dimension attributes are derived from the same relational table, an attribute relationship is automatically defined between the key attribute and each non-key attribute of the dimension. When you define a dimension based on a snowflake schema, which is where dimension attributes are derived from multiple related tables, an attribute relationship is automatically defined as follows:  
  
-   Between the key attribute and each non-key attribute bound to columns in the main dimension table.  
  
-   Between the key attribute and the attribute bound to the foreign key in the secondary table that links the underlying dimension tables.  
  
-   Between the attribute bound to foreign key in the secondary table and each non-key attribute bound to columns from the secondary table.  
  
 However, there are a number of reasons why you might want to change these default attribute relationships. For example, you might want to define a natural hierarchy, a custom sort order, or dimension granularity based on a non-key attribute. For more information, see [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md).  
  
> [!NOTE]  
>  Attribute relationships are known in Multidimensional Expressions (MDX) as member properties.  
  
## Natural Hierarchy Relationships  
 A hierarchy is a natural hierarchy when each attribute included in the user-defined hierarchy has a one to many relationship with the attribute immediately below it. For example, consider a Customer dimension based on a relational source table with eight columns:  
  
-   CustomerKey  
  
-   CustomerName  
  
-   Age  
  
-   Gender  
  
-   Email  
  
-   City  
  
-   Country  
  
-   Region  
  
 The corresponding Analysis Services dimension has seven attributes:  
  
-   Customer (based on CustomerKey, with CustomerName supplying member names)  
  
-   Age, Gender, Email, City, Region, Country  
  
 Relationships representing natural hierarchies are enforced by creating an attribute relationship between the attribute for a level and the attribute for the level below it. For [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], this specifies a natural relationship and potential aggregation. In the Customer dimension, a natural hierarchy exists for the Country, Region, City, and Customer attributes. The natural hierarchy for `{Country, Region, City, Customer}` is described by adding the following attribute relationships:  
  
-   The Country attribute as an attribute relationship to the Region attribute.  
  
-   The Region attribute as an attribute relationship to the City attribute.  
  
-   The City attribute as an attribute relationship to the Customer attribute.  
  
 For navigating data in the cube, you can also create a user-defined hierarchy that does not represent a natural hierarchy in the data (which is called an *ad hoc* or *reporting* hierarchy). For example, you could create a user-defined hierarchy based on `{Age, Gender}`. Users do not see any difference in how the two hierarchies behave, although the natural hierarchy benefits from aggregating and indexing structures - hidden from the user - that account for the natural relationships in the source data.  
  
 The `SourceAttribute` property of a level determines which attribute is used to describe the level. The `KeyColumns` property on the attribute specifies the column in the data source view that supplies the members. The `NameColumn` property on the attribute can specify a different name column for the members.  
  
 To define a level in a user-defined hierarchy using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the **Dimension Designer** allows you to select a dimension attribute, a column in a dimension table, or a column from a related table included in the data source view for the cube. For more information about creating user-defined hierarchies, see [Create User-Defined Hierarchies](../multidimensional-models/user-defined-hierarchies-create.md).  
  
 In Analysis Services, an assumption is usually made about the content of members. Leaf members have no descendents and contain data derived from underlying data sources. Nonleaf members have descendents and contain data derived from aggregations performed on child members. In aggregated levels, members are based on aggregations of subordinate levels. Therefore, when the `IsAggregatable` property is set to `False` on a source attribute for a level, no aggregatable attributes should be added as levels above it.  
  
## Defining an Attribute Relationship  
 The main constraint when you create an attribute relationship is to make sure that the attribute referred to by the attribute relationship has no more than one value for any member in the attribute to which the attribute relationship belongs. For example, if you define a relationship between a City attribute and a State attribute, each city can only relate to a single state.  
  
## Attribute Relationship Queries  
 You can use MDX queries to retrieve data from attribute relationships, in the form of member properties, with the `PROPERTIES` keyword of the MDX `SELECT` statement. For more information about how to use MDX to retrieve member properties, see [Using Member Properties &#40;MDX&#41;](../multidimensional-models/mdx/mdx-member-properties.md).  
  
## See Also  
 [Attributes and Attribute Hierarchies](attributes-and-attribute-hierarchies.md)   
 [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md)   
 [User Hierarchies](user-hierarchies.md)   
 [User Hierarchy Properties](user-hierarchies-properties.md)  
  
  
