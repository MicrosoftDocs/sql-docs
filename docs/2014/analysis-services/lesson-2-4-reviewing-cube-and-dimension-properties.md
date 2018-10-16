---
title: "Reviewing Cube and Dimension Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: dda922b8-6d75-4662-b09e-8a317c6a1c70
author: minewiskan
ms.author: owend
manager: craigg
---
# Reviewing Cube and Dimension Properties
  After you have defined a cube, you can review the results by using Cube Designer. In the following task, you review the structure of the cube in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project.  
  
### To review cube and dimension properties in Cube Designer  
  
1.  To open the Cube Designer, double-click the **Analysis Services Tutorial** cube in the **Cubes** node of Solution Explorer.  
  
2.  In the **Measures** pane of the **Cube Structure** tab in Cube Designer, expand the **Internet Sales** measure group to reveal the defined measures.  
  
     You can change the order by dragging the measures into the order that you want. The order you create affects how certain client applications order these measures. The measure group and each measure that it contains have properties that you can edit in the Properties window.  
  
3.  In the **Dimensions** pane of the **Cube Structure** tab in Cube Designer, review the cube dimensions that are in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube.  
  
     Notice that although only three dimensions were created at the database level, as displayed in Solution Explorer, there are five cube dimensions in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube. The cube contains more dimensions than the database because the Date database dimension is used as the basis for three separate date-related cube dimensions, based on different date-related facts in the fact table. These date-related dimensions are also called *role playing dimensions*. The three date-related cube dimensions let users dimension the cube by three separate facts that are related to each product sale: the product order date, the due date for fulfillment of the order, and the ship date for the order. By reusing a single database dimension for multiple cube dimensions, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] simplifies dimension management, uses less disk space, and reduces overall processing time.  
  
4.  In the **Dimensions** pane of the **Cube Structure** tab, expand **Customer**, and then click **Edit Customer** to open the dimension in Dimension Designer.  
  
     Dimension Designer contains these tabs: **Dimension Structure**, **Attribute Relationships**, **Translations**, and **Browser**. Notice that the **Dimension Structure** tab includes three panes: **Attributes**, **Hierarchies**, and **Data Source View**. The attributes that the dimension contains appear in the **Attributes** pane. For more information, see [Dimension Attribute Properties Reference](multidimensional-models/dimension-attribute-properties-reference.md), [Create User-Defined Hierarchies](multidimensional-models/user-defined-hierarchies-create.md).  
  
5.  To switch to Cube Designer, right-click the **Analysis Services Tutorial** cube in the **Cubes** node in Solution Explorer, and then click **View Designer**.  
  
6.  In Cube Designer, click the **Dimension Usage** tab.  
  
     In this view of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, you can see the cube dimensions that are used by the Internet Sales measure group. Also, you can define the type of relationship between each dimension and each measure group in which it is used.  
  
7.  Click the **Partitions** tab.  
  
     The Cube Wizard defines a single partition for the cube, by using the multidimensional online analytical processing (MOLAP) storage mode without aggregations. With MOLAP, all leaf-level data and all aggregations are stored within the cube for maximum performance. Aggregations are precalculated summaries of data that improve query response time by having answers ready before questions are asked. You can define additional partitions, storage settings, and writeback settings on the **Partitions** tab. For more information, see [Partitions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md), [Aggregations and Aggregation Designs](multidimensional-models-olap-logical-cube-objects/aggregations-and-aggregation-designs.md).  
  
8.  Click the **Browser** tab.  
  
     Notice that the cube cannot be browsed because it has not yet been deployed to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. At this point, the cube in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project is just a definition of a cube, which you can deploy to any instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. When you deploy and process a cube, you create the defined objects in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and populate the objects with data from the underlying data sources.  
  
9. In Solution Explorer, right-click **Analysis Services Tutorial** in the **Cubes** node, and then click **View Code**. You might need to wait.  
  
     The XML code for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube is displayed on the **[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial.cube [XML]** tab. This is the actual code that is used to create the cube in an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] during deployment. For more information, see [View the XML for an Analysis Services Project &#40;SSDT&#41;](multidimensional-models/view-the-xml-for-an-analysis-services-project-ssdt.md).  
  
10. Close the XML code tab.  
  
## Next Task in Lesson  
 [Deploying an Analysis Services Project](lesson-2-5-deploying-an-analysis-services-project.md)  
  
## See Also  
 [Browse Dimension Data in Dimension Designer](multidimensional-models/database-dimensions-browse-dimension-data-in-dimension-designer.md)  
  
  
