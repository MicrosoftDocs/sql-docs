---
title: "Ragged Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "ragged hierarchies [Analysis Services]"
ms.assetid: e40a5788-7ede-4b0f-93ab-46ca33d0cace
author: minewiskan
ms.author: owend
manager: craigg
---
# Ragged Hierarchies
  A ragged hierarchy is a user-defined hierarchy that has an uneven number of levels. Common examples include an organizational chart where a high-level manager has both departmental managers and non-managers as direct reports, or geographic hierarchies composed of Country-Region-City, where some cities lack a parent State or Province, such as Washington D.C., Vatican City, or New Delhi.  
  
 For most hierarchies in a dimension, each level has the same number of members above it as any other member at the same level. A ragged hierarchy is different in that the logical parent of at least one member is not in the level immediately above the member. When this occurs, the hierarchy descends to different levels for different drilldown paths. In a client application, this can make drilldown paths unnecessarily complicated.  
  
 Client applications vary in how well they handle a ragged hierarchy. If ragged hierarchies exist in your model, be prepared to do a little extra work to get the rendering behavior you expect.  
  
 As a first step, check the client application to see how it handles the drilldown path. For example, Excel repeats the parent names as placeholders for missing values. To see this behavior yourself, build a PivotTable using the Sales Territory dimension in the Adventure Works multidimensional model. In a PivotTable having the Sales Territory attributes Group, Country, and Region, you will see that countries missing a region value will get a placeholder, in this case a repeat of the parent above it (Country name). This behavior derives from the MDX Compatibility=1 connection string property that is fixed within Excel. If the client does not naturally provide the drill-down behaviors you are looking for, you can set properties in the model to change at least some of those behaviors.  
  
 This topic contains the following sections:  
  
-   [Approaches for modifying drilldown navigation in a ragged hierarchy](#bkmk_approach)  
  
-   [Set HideMemberIf to hide members in a regular hierarchy](#bkmk_Hide)  
  
-   [Set MDX Compatibility to determine how placeholders are represented in client applications](#bkmk_Mdx)  
  
##  <a name="bkmk_approach"></a> Approaches for modifying drilldown navigation in a ragged hierarchy  
 The presence of a ragged hierarchy becomes a problem when drilldown navigation does not return expected values or is perceived as awkward to use. To fix navigation problems that result from ragged hierarchies, consider these options:  
  
-   Use a regular hierarchy but set the `HideMemberIf` property on each level to specify whether a missing level is visualized to the user. When setting `HideMemberIf`, you should also set `MDXCompatibility` on the connection string to override default navigation behaviors. Instructions for setting these properties are in this topic.  
  
-   Create a parent-child hierarchy that explicitly manages the level members. For an illustration of the technique, see [Ragged Hierarchy in SSAS (blog post)](http://dwbi1.wordpress.com/2011/03/30/ragged-hierarchy-in-ssas/). For more information in Books Online, see [Parent-Child Hierarchy](parent-child-dimension.md). Downsides to creating a parent-child hierarchy are that you can only have one per dimension, and you typically incur a performance penalty when calculating aggregations for intermediate members.  
  
 If your dimension contains more than one ragged hierarchy, you should use the first approach, setting `HideMemberIf`. BI Developers with practical experience in working with ragged hierarchies go further in advocating for additional changes in the physical data tables, creating separate tables for each level. See [Martin Mason's the SSAS Financial Cube-Part 1a-Ragged Hierarchies (blog)](http://martinmason.wordpress.com/2012/03/03/the-ssas-financial-cubepart-1aragged-hierarchies-cont/) for details about this technique.  
  
##  <a name="bkmk_Hide"></a> Set HideMemberIf to hide members in a regular hierarchy  
 In a ragged dimension's table, the logically missing members can be represented in different ways. The table cells can contain nulls or empty strings, or they can contain the same value as their parent to serve as a placeholder. The representation of placeholders is determined by the placeholder status of child members, as determined by the `HideMemberIf` property, and the `MDX Compatibility` connection string property for the client application.  
  
 For client applications that support the display of ragged hierarchies, you can use these properties to hide logically missing members.  
  
1.  In SSDT, double-click a dimension to open it in Dimension Designer. The first tab, Dimension Structure, shows attribute hierarchies in the Hierarchies pane.  
  
2.  Right-click a member within the hierarchy and select **Properties**. Set `HideMemberIf` to one of the values described below.  
  
    |HideMemberIf Setting|Description|  
    |--------------------------|-----------------|  
    |`Never`|Level members are never hidden. This is the default value.|  
    |**OnlyChildWithNoName**|A level member is hidden when it is the only child of its parent and its name is null or an empty string.|  
    |**OnlyChildWithParentName**|A level member is hidden when it is the only child of its parent and its name is the same as the name of its parent.|  
    |**NoName**|A level member is hidden when its name is empty.|  
    |**ParentName**|A level member is hidden when its name is identical to that of its parent.|  
  
##  <a name="bkmk_Mdx"></a> Set MDX Compatibility to determine how placeholders are represented in client applications  
 After setting `HideMemberIf` on a hierarchy level, you should also set the `MDX Compatibility` property in the connection string sent from the client application. The `MDX Compatibility` setting determines whether the `HideMemberIf` is used.  
  
|MDX Compatibility Setting|Description|Usage|  
|-------------------------------|-----------------|-----------|  
|**1**|Show a placeholder value.|This is the default used by Excel, SSDT, and SSMS. It instructs the server to return placeholder values when drilling down empty levels in a ragged hierarchy. If you click the placeholder value, you can continue down to get to the child (leaf) nodes.<br /><br /> Excel owns the connection string used to connect to Analysis Services, and it always sets `MDX Compatibility` to 1 on each new connection. This behavior preserves backward compatibility.|  
|**2**|Hide a placeholder value (either a null value or a duplicate of the parent level), but show other levels and nodes having relevant values.|`MDX Compatibility`=2 is typically viewed as the preferred setting in terms of ragged hierarchies. A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report and some third-party client applications can persist this setting.|  
  
## See Also  
 [Create User-Defined Hierarchies](user-defined-hierarchies-create.md)   
 [User Hierarchies](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md)   
 [Parent-Child Hierarchy](parent-child-dimension.md)   
 [Connection String Properties &#40;Analysis Services&#41;](../../analysis-services/instances/connection-string-properties-analysis-services.md)  
  
  
