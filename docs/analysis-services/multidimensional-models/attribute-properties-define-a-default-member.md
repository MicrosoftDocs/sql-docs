---
title: "Define a Default Member | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Attribute Properties - Define a Default Member
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The default member of an attribute hierarchy is used to evaluate expressions when an attribute hierarchy is not included in a query. The default member is ignored whenever a query includes an attribute hierarchy or user hierarchy that contains the attribute that sources the attribute hierarchy. This is because the member specified in the query is used.  
  
 The default member for an attribute hierarchy is set by specifying an attribute member as the **DefaultMember** property value for the attribute hierarchy. You can set this property on the Dimension Structure tab in Dimension Designer, or in the cube's calculation script on the Calculation tab in Cube Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You can also specify the **DefaultMember** property for a security role (overriding the default member set on the dimension) on the Dimension Data tab when defining dimension security. To avoid name resolution problems, define the default member in the cube's MDX script in the following situations: if the cube refers to a database dimension more than once, if the dimension in the cube has a different name than the dimension in the database, or if you want to have different default members in different cubes.  
  
 The default member on an attribute is used to evaluate expressions when an attribute is not included in a query. The default member for an attribute is specified by the **DefaultMember** property on the attribute. Whenever a hierarchy from a dimension is included in a query, all the default members from attributes corresponding to levels in the hierarchy are ignored. If no hierarchy of a dimension is included in a query, default members are used for all the attributes in the dimension.  
  
## Resolving the Default Member When No Default Member is Specified  
 If no default member is specified for an attribute hierarchy, and the attribute hierarchy is aggregatable (the **IsAggregatable** property on the attribute is set to **True**), the (All) member is the default member. If no default member is specified and the attribute hierarchy is not aggregatable (the **IsAggregatable** property on the attribute is set to **False**), a default member is selected from the attribute hierarchy's top level.  
  
## Specifying the Default Member  
 Every attribute in a dimension in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] has a default member, which you can specify by using the **DefaultMember** property for an attribute. This setting is used to evaluate expressions if an attribute is not included in a query. If a query specifies a hierarchy in a dimension, the default members for the attributes in the hierarchy are ignored. If a query does not specify a hierarchy in a dimension, the **DefaultMember** settings for dimension attributes take effect.  
  
 If the **DefaultMember** setting for an attribute is blank and its **IsAggregatable** property is set to **True**, the default member is the All member. If the **IsAggregatable** property is set to **False**, the default member is the first member of the first visible level.  
  
 The **DefaultMember** setting for an attribute applies to every hierarchy in which the attribute participates. You cannot use different settings for different hierarchies in a dimension. For example, if the [1998] member is the default member for a [Year] attribute, this setting applies to every hierarchy in the dimension. The **DefaultMember** setting in this case cannot be [1998] in one hierarchy and [1997] in a different hierarchy.  
  
 If you define a default member for a particular level in a hierarchy that does not aggregate naturally, you must define default members in all levels above that level in the hierarchy. For example, in the hierarchy All-Countries-Climate, you cannot define a default member for Climate unless you define a default member for Countries. Failing to do so creates query-time errors.  
  
 When levels in a hierarchy aggregate naturally, you can define a default member for any attribute in the hierarchy without regard to other attributes in the hierarchy. For example, in the hierarchy Country-Province-City, you can define a default member for City such as [City].[Montreal] without defining the default member for State or for Country.  
  
## See Also  
 [Configure the &#40;All&#41; Level for Attribute Hierarchies](../../analysis-services/multidimensional-models/database-dimensions-configure-the-all-level-for-attribute-hierarchies.md)  
  
  
