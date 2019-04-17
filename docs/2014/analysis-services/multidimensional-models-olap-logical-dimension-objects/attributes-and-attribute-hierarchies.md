---
title: "Attributes and Attribute Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "regular attributes [Analysis Services]"
  - "parent attributes [Analysis Services]"
  - "hierarchies [Analysis Services], attribute"
  - "attributes [Analysis Services], about attributes"
  - "account attributes [Analysis Services]"
  - "dimensions [Analysis Services], attributes"
  - "key attributes [Analysis Services]"
  - "OLAP objects [Analysis Services], attributes"
  - "attributes [Analysis Services], relationships"
  - "attributes [Analysis Services]"
  - "relationships [Analysis Services], attributes"
ms.assetid: 59de1ea2-e7a9-4a53-9ee0-14be52e95643
author: minewiskan
ms.author: owend
manager: craigg
---
# Attributes and Attribute Hierarchies
  Dimensions are collections of attributes, which are bound to one or more columns in a table or view in the data source view.  
  
## Key Attribute  
 Each dimension contains a key attribute. Each attribute bound to one or more columns in a dimension table. The key attribute is the attribute in a dimension that identifies the columns in the dimension main table that are used in foreign key relationships to the fact table. Typically, the key attribute represents the primary key column or columns in the dimension table. You can define a logical primary key on a table in a data source view which has no physical primary key in the underlying data source. **For more information**, see [Define Logical Primary Keys in a Data Source View &#40;Analysis Services&#41;](../multidimensional-models/define-logical-primary-keys-in-a-data-source-view-analysis-services.md). When defining key attributes, the Cube Wizard and Dimension Wizard try to use the primary key columns of the dimension table in the data source view. If the dimension table does not have a logical primary key or physical primary key defined, the wizards may not be able to correctly define the key attributes for the dimension.  
  
## Binding an Attribute to Columns in Data Source View Tables or Views  
 An attribute is bound to columns in one or more data source view tables or views. An attribute is always bound to one or more key columns, which determines the members that are contained by the attribute. By default, this is the only column to which an attribute is bound. An attribute can also be bound to one or more additional columns for specific purposes. For example, an attribute's `NameColumn` property determines the name that appears to the user for each attribute member - this property of the attribute can be bound to a particular dimension column through a data source view or can be bound to a calculated column in the data source view. For more information, see [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md).  
  
## Attribute Hierarchies  
 By default, attribute members are organized into two level hierarchies, consisting of a leaf level and an All level. The All level contains the aggregated value of the attribute's members across the measures in each measure group to which the dimension of which the attribute is related is a member. However, if the `IsAggregatable` property is set to False, the All level is not created. For more information, see [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md).  
  
 Attributes can be, and typically are, arranged into user-defined hierarchies that provide the drill-down paths by which users can browse the data in the measure groups to which the attribute is related. In client applications, attributes can be used to provide grouping and constraint information. When attributes are arranged into user-defined hierarchies, you define relationships between hierarchy levels when levels are related in a many-to-one or a one-to-one relationship (called a *natural* relationship). For example, in a Calendar Time hierarchy, a Day level should be related to the Month level, the Month level related to the Quarter level, and so on. Defining relationships between levels in a user-defined hierarchy enables Analysis Services to define more useful aggregations to increase query performance and can also save memory during processing performance, which can be important with large or complex cubes. For more information, see [User Hierarchies](user-hierarchies.md), [Create User-Defined Hierarchies](../multidimensional-models/user-defined-hierarchies-create.md), and [Define Attribute Relationships](../multidimensional-models/attribute-relationships-define.md).  
  
## Attribute Relationships, Star Schemas, and Snowflake Schemas  
 By default, in a star schema, all attributes are directly related to the key attribute, which enables users to browse the facts in the cube based on any attribute hierarchy in the dimension. In a snowflake schema, an attribute is either directly linked to the key attribute if their underlying table is directly linked to the fact table or is indirectly linked by means of the attribute that is bound to the key in the underlying table that links the snowflake table to the directly linked table.  
  
## See Also  
 [Create User-Defined Hierarchies](../multidimensional-models/user-defined-hierarchies-create.md)   
 [Define Attribute Relationships](../multidimensional-models/attribute-relationships-define.md)   
 [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md)  
  
  
