---
title: "Define a Regular Relationship and Regular Relationship Properties | Microsoft Docs"
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
# Define a Regular Relationship and Regular Relationship Properties
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When you define a new cube dimension or a new measure group, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will try to detect if a regular relationship exists and then set the dimension usage setting to **Regular**. You can view or edit a regular dimension relationship on the **Dimension Usage** tab of Cube Designer.  
  
 When you define the relationship of a cube dimension to a measure group, you also specify the granularity attribute for that relationship. The granularity attribute defines the lowest level of detail available in the cube for that dimension, which is generally the key attribute for the dimension. However, sometimes you may want to set the granularity of a particular cube dimension in a particular measure group to a different grain. For example, you may want to set the granularity attribute for the Time dimension to the Month attribute instead of to the Day attribute, if you are using a Sales Quotas or a Budget measure group. When you specify the granularity attribute to be an attribute other than the key attribute, you must guarantee that all other attributes in the dimension are directly or indirectly linked to this other attribute through attribute relationships. If not, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will be unable to aggregate data correctly.  
  
  
