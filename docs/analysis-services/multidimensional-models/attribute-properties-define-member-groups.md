---
title: "Define Member Groups | Microsoft Docs"
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
# Attribute Properties - Define Member Groups
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  If an attribute has a large number of members, you can choose to group those members into buckets, reducing the number of members that users see when they browse the data in a hierarchy. You can also determine the number of buckets in which the members are groups and set a naming scheme for the buckets. For more information, see [Group Attribute Members &#40;Discretization&#41;](../../analysis-services/multidimensional-models/attribute-properties-group-attribute-members.md).  
  
 You group members by setting the **DiscretizationMethod** property, which is accessed through the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you create member groups, your changes are not available to users until you process the dimension. For more information, see [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
### To create member groups  
  
1.  Open the dimension that you want to work with. The **Dimension Structure** tab opens by default.  
  
2.  In **Attributes**, right-click the attribute whose members you want to group, and then click **Properties**.  
  
3.  From the drop-down list next to **DiscretizationMethod**, select a method by which to group the members. For more information about **DiscretizationMethod** settings, see [Group Attribute Members &#40;Discretization&#41;](../../analysis-services/multidimensional-models/attribute-properties-group-attribute-members.md).  
  
  
