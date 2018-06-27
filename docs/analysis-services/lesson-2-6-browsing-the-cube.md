---
title: "Browsing the Cube | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 2-6 - Browsing the Cube
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

After you deploy a cube, the cube data is viewable on the **Browser** tab in Cube Designer, and the dimension data is viewable on the **Browser** tab in Dimension Designer. Browsing cube and dimension data is way to check your work incrementally. You can verify that small changes to properties, relationships, and other objects have the desired effect once the object is processed. While the Browser tab is used to view both cube and dimension data, the tab provides different capabilities based on the object you are browsing.  
  
For dimensions, the Browser tab provides a way to view members or navigate a hierarchy all the way down to the leaf node. You can browse dimension data in different languages, assuming you have added the translations to your model.  
  
For cubes, the Browser tab provides two approaches for exploring data. You can use the built-in MDX Query Designer to build queries that return a flattened rowset from a multidimensional database. Alternatively, you can use an Excel shortcut. When you start Excel from within [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], Excel opens with a PivotTable already in the worksheet and a predefined connection to the model workspace database.  
  
Excel generally offers a better browsing experience because you can explore cube data interactively, using horizontal and vertical axes to analyze the relationships in your data. In contrast, the MDX Query Designer is limited to a single axis. Moreover, because the rowset is flattened, you do not get the drilldown that an Excel PivotTable provides. As you add more dimensions and hierarchies to your cube, which you will do in subsequent lessons, Excel will be the preferred solution for browsing data.  
  
### To browse the deployed cube  
  
1.  Switch to **Dimension Designer** for the Product dimension in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. To do this, double-click the **Product** dimension in the **Dimensions** node of Solution Explorer.  
  
2.  Click the **Browser** tab to display the **All** member of the **Product Key** attribute hierarchy. In lesson three, you will define a user hierarchy for the Product dimension that will let you browse the dimension.  
  
3.  Switch to **Cube Designer** in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. To do this, double-click the **Analysis Services Tutorial** cube in the **Cubes** node of Solution Explorer.  
  
4.  Select the **Browser** tab, and then click the **Reconnect** icon on the toolbar of the designer.  
  
    The left pane of the designer shows the objects in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube. On the right side of the **Browser** tab, there are two panes: the upper pane is the **Filter** pane, and the lower pane is the **Data** pane. In an upcoming lesson, you will use the cube browser to do analysis.  
  
## Next Lesson  
[Lesson 3: Modifying Measures, Attributes and Hierarchies](../analysis-services/lesson-3-modifying-measures-attributes-and-hierarchies.md)  
  
## See Also  
[MDX Query Editor &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/777f2c23-1c1c-4b72-9d19-48a4866551f8)  
  
  
  
