---
title: "Dimension Relationships | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Dimension Relationships
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Dimension usage defines the relationships between a cube dimension and the measure groups in a cube. A cube dimension is an instance of a database dimension that is used in a specific cube. A cube can, and frequently does, have cube dimensions that are not directly related to a measure group, but which might be indirectly related to the measure group through another dimension or measure group. When you add a database dimension or measure group to a cube, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tries to determine dimension usage by examining relationships between the dimension tables and fact tables in the cube's data source view, and by examining the relationships between attributes in dimensions. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] automatically sets the dimension usage settings for the relationships that it can detect.  
  
 A relationship between a dimension and a measure group consists of the dimension and fact tables participating in the relationship and a granularity attribute that specifies the granularity of the dimension in the particular measure group.  
  
## Regular Dimension Relationships  
 A regular dimension relationship between a cube dimension and a measure group exists when the key column for the dimension is joined directly to the fact table. This direct relationship is based on a primary key-foreign key relationship in the underlying relational database, but might also be based on a logical relationship that is defined in the data source view. A regular dimension relationship represents the relationship between dimension tables and a fact table in a traditional star schema design. For more information about regular relationships, see [Define a Regular Relationship and Regular Relationship Properties](../../analysis-services/multidimensional-models/define-a-regular-relationship-and-regular-relationship-properties.md).  
  
## Reference Dimension Relationships  
 A reference dimension relationship between a cube dimension and a measure group exists when the key column for the dimension is joined indirectly to the fact table through a key in another dimension table, as shown in the following illustration.  
  
 ![Logical diagram, referenced dimension relationship](../../analysis-services/multidimensional-models-olap-logical-cube-objects/media/as-refdimension1.gif "Logical diagram, referenced dimension relationship")  
  
 A reference dimension relationship represents the relationship between dimension tables and a fact table in a snowflake schema design. When dimension tables are connected in a snowflake schema, you can define a single dimension using columns from multiple tables, or you can define separate dimensions based on the separate dimension tables and then define a link between them using the reference dimension relationship setting. The following figure shows one fact table named **InternetSales**, and two dimension tables called **Customer** and **Geography**, in a snowflake schema.  
  
 ![Logical schema, referenced dimension relationship](../../analysis-services/multidimensional-models-olap-logical-cube-objects/media/as-refdim-schema1.gif "Logical schema, referenced dimension relationship")  
  
 You can create a dimension with the **Customer** table as the dimension main table and the **Geography** table included as a related table. A regular relationship is then defined between the dimension and the InternetSales measure group.  
  
 Alternatively, you can create two dimensions related to the InternetSales measure group: a dimension based on the **Customer** table, and a dimension based on the **Geography** table. You can then relate the Geography dimension to the InternetSales measure group using a reference dimension relationship using the Customer dimension. In this case, when the facts in the InternetSales measure group are dimensioned by the Geography dimension, the facts are dimensioned by customer and by geography. If the cube contained a second measure group named Reseller Sales, you would be unable to dimension the facts in the Reseller Sales measure group by Geography because no relationship would exist between Reseller Sales and Geography.  
  
 There is no limit to the number of reference dimensions that can be chained together, as shown in the following illustration.  
  
 ![Logical diagram, referenced dimension relationship](../../analysis-services/multidimensional-models-olap-logical-cube-objects/media/as-refdimension2.gif "Logical diagram, referenced dimension relationship")  
  
 For more information about referenced relationships, see [Define a Referenced Relationship and Referenced Relationship Properties](../../analysis-services/multidimensional-models/define-a-referenced-relationship-and-referenced-relationship-properties.md).  
  
## Fact Dimension Relationships  
 Fact dimensions, frequently referred to as degenerate dimensions, are standard dimensions that are constructed from attribute columns in fact tables instead of from attribute columns in dimension tables. Useful dimensional data is sometimes stored in a fact table to reduce duplication. For example, the following diagram displays the **FactResellerSales** fact table, from the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample database.  
  
 ![Columns in fact table can support dimensions](../../analysis-services/multidimensional-models-olap-logical-cube-objects/media/as-factdim.gif "Columns in fact table can support dimensions")  
  
 The table contains attribute information not only for each line of an order issued by a reseller, but about the order itself. The attributes circled in the previous diagram identify the information in the **FactResellerSales** table that could be used as attributes in a dimension. In this case, two additional pieces of information, the carrier tracking number and the purchase order number issued by the reseller, are represented by the CarrierTrackingNumber and CustomerPONumber attribute columns. This information is interesting-for example, users would definitely be interested in seeing aggregated information, such as the total product cost, for all the orders being shipped under a single tracking number. But, without a dimension data for these two attributes cannot be organized or aggregated.  
  
 In theory, you could create a dimension table that uses the same key information as the FactResellerSales table and move the other two attribute columns, CarrierTrackingNumber and CustomerPONumber, to that dimension table. However, you would be duplicating a significant portion of data and adding unnecessary complexity to the data warehouse to represent just two attributes as a separate dimension.  
  
> [!NOTE]  
>  Fact dimensions are frequently used to support drillthrough actions. For more information about actions, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/actions-analysis-services-multidimensional-data.md).  
  
> [!NOTE]  
>  Fact dimensions must be incrementally updated after every update to the measure group that is referenced by the fact relationship. If the fact dimension is a ROLAP dimension, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] processing engine drops any caches and incrementally processes the measure group.  
  
 For more information about fact relationships, see [Define a Fact Relationship and Fact Relationship Properties](../../analysis-services/multidimensional-models/define-a-fact-relationship-and-fact-relationship-properties.md).  
  
## Many to Many Dimension Relationships  
 In most dimensions, each fact joins to one and only one dimension member, and a single dimension member can be associated with multiple facts. In relational database terminology, this is referred to as a one-to-many relationship. However, it is frequently useful to join a single fact to multiple dimension members. For example, a bank customer might have multiple accounts (checking, saving, credit card, and investment accounts), and an account can also have joint or multiple owners. The Customer dimension constructed from such relationships would then have multiple members that relate to a single account transaction.  
  
 ![Logical schema/many-to-many dimension relationship](../../analysis-services/multidimensional-models-olap-logical-cube-objects/media/as-many-dimension1.gif "Logical schema/many-to-many dimension relationship")  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] lets you define a many-to-many relationship between a dimension and a fact table.  
  
> [!NOTE]  
>  To support a many-to-many dimension relationship, the data source view must have established a foreign key relationship between all the tables involved, as shown in the previous diagram. Otherwise, you will be unable to select the correct intermediate measure group when establishing the relationship in the **Dimension Usage** tab of Dimension Designer.  
  
 For more information about many-to-many relationships, see [Define a Many-to-Many Relationship and Many-to-Many Relationship Properties](../../analysis-services/multidimensional-models/define-a-many-to-many-relationship-and-many-to-many-relationship-properties.md).  
  
## See Also  
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)  
  
  
