---
title: "Attributes (Dimension Structure Tab, Dimension Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql.asvs.dimensiondesigner.dbv.attributespane.f1"
ms.assetid: 627eaa08-7638-4edd-bdfa-0d8175a7cde5
author: minewiskan
ms.author: owend
manager: craigg
---
# Attributes (Dimension Structure Tab, Dimension Designer) (Analysis Services - Multidimensional Data)
  Use this pane to manage the attributes associated with the selected dimension. Attributes can be dragged from this pane to the **Hierarchies** pane to create hierarchies and levels. For more information, see [Hierarchies &#40;Dimension Structure Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](hierarchies-dimension-designer-analysis-services-multidimensional-data.md).  
  
 To create an attribute, drag a column from the **Data Source View** pane to the **Attributes** pane while in list, tree, or view mode. To remove an attribute, select **Delete** on the shortcut menu.  
  
 **To display the Attributes pane**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, and then open the dimension that you want.  
  
2.  If not selected, click the **Dimension Structure** tab.  
  
## Options  
 **Attributes**  
 Displays the attributes available to the selected dimension. This option can be viewed in the following modes:  
  
-   List mode  
  
     Use this mode to create and modify hierarchies. To view the attributes in list mode, select **Show Attributes in** on the shortcut menu, and then click **List**.  
  
-   Tree mode  
  
     Use this mode to create and modify hierarchies. To view the attributes in tree mode, select **Show Attributes in** on the shortcut menu, and then click **Tree**.  
  
-   Grid mode  
  
     Use this mode to create dimensions manually, or to modify attribute properties. To view the attributes in grid mode, select **Show Attributes in** on the shortcut menu, and then click **Grid**.  
  
## Grid Mode Options  
 When viewing attributes in grid mode, you have access to the columns listed in the following table.  
  
 **Name**  
 Displays the name of the attribute.  
  
 **Usage**  
 Sets the usage of the selected attribute. Click the down arrow to select from the following choices:  
  
|Value|Description|  
|-----------|-----------------|  
|Regular|Identifies a regular attribute.|  
|Key|Identifies the key attribute for the dimension. This corresponds to the leaf members of the dimension. There can only be one key attribute per dimension. To modify, click the ellipsis button (**...**) next to the **KeyColumns** property in the **Properties** pane.|  
|Parent|Denotes the parent attribute for a parent-child relationship. The child attribute in this relationship must always be the key attribute.|  
|AccountType|Denotes an account type attribute. This is used by the server or engine when the aggregation function for a measure is set to "by account."|  
  
 **Type**  
 Sets the type of the attribute. Click the down arrow to select from the available choices.  
  
 **Key Column**  
 Displays the data type of the underlying columns. When creating a new attribute, click the down arrow to select from the available choices.  
  
 **Name Column**  
 Displays the location of the underlying column. When creating a new attribute, click the down arrow to select between **Same as key** and **Separate column**. If **Separate column** is chosen, the **NameColumn** property in the **Properties** pane sets the column that stores the name to use for the attribute.  
  
## See Also  
 [Dimension Structure &#40;Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](dimension-structure-dimension-designer-analysis-services-multidimensional-data.md)   
 [Hierarchies &#40;Dimension Structure Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](hierarchies-dimension-designer-analysis-services-multidimensional-data.md)   
 [Toolbar &#40;Dimension Structure Tab, Dimension Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-dimension-structure-designer-analysis-services-multidimensional-data.md)  
  
  
