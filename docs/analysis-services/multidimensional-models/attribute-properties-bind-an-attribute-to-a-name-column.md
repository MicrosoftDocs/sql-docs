---
title: "Bind an Attribute to a Name Column | Microsoft Docs"
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
# Attribute Properties - Bind an Attribute to a Name Column
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This procedure describes how to bind an attribute to a name column manually by using the **Attributes** pane in the Dimension Designer and by using the **Object Binding** dialog box.  
  
### To bind an attribute to a name column  
  
1.  In Dimension Designer, open the dimension in which you want to create the attribute.  
  
2.  On the **Dimension Structure** tab, in the **Attributes** pane, right-click the attribute that you want to configure, and then click **Properties**.  
  
3.  In the **Properties** window, locate the **NameColumn** property, and then select **(new)**.  
  
4.  In the **Object Binding** dialog box, for **Binding type**, select **Column binding**.  
  
5.  In the **Source column** list, select the column to which the attribute will be bound, and then click **OK**.  
  
  
