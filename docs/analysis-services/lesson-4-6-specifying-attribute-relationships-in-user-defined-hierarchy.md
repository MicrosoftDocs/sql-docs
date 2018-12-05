---
title: "4-6-Specifying Attribute Relationships in User-Defined Hierarchy | Microsoft Docs"
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
# 4-6-Specifying Attribute Relationships in User-Defined Hierarchy
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

As you have already learned in this tutorial, you can organize attribute hierarchies into levels within user hierarchies to provide navigation paths for users in a cube. A user hierarchy can represent a natural hierarchy, such as city, state, and country, or can just represent a navigation path, such as employee name, title, and department name. To the user navigating a hierarchy, these two types of user hierarchies are the same.  
  
With a natural hierarchy, if you define attribute relationships between the attributes that make up the levels, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] can use an aggregation from one attribute to obtain the results from a related attribute. If there are no defined relationships between attributes, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] will aggregate all non-key attributes from the key attribute. Therefore, if the underlying data supports it, you should define attribute relationships between attributes. Defining attribute relationships improves dimension, partition, and query processing performance. For more information, see [Define Attribute Relationships](../analysis-services/multidimensional-models/attribute-relationships-define.md) and [Attribute Relationships](../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md).  
  
When you define attribute relationships, you can specify that the relationship is either flexible or rigid. If you define a relationship as rigid, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] retains aggregations when the dimension is updated. If a relationship that is defined as rigid actually changes, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] generates an error during processing unless the dimension is fully processed. Specifying the appropriate relationships and relationship properties increases query and processing performance. For more information, see [Define Attribute Relationships](../analysis-services/multidimensional-models/attribute-relationships-define.md), and [User Hierarchy Properties](../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md).  
  
In the tasks in this topic, you define attribute relationships for the attributes in the natural user hierarchies in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project. These include the **Customer Geography** hierarchy in the **Custome**r dimension, the **Sales Territory** hierarchy in the **Sales Territory** dimension, the **Product Model** Lines hierarchy in the **Product** dimension, and the **Fiscal Date** and **Calendar Date** hierarchies in the **Date** dimension. These user hierarchies are all natural hierarchies.  
  
## Defining Attribute Relationships for Attributes in the Customer Geography Hierarchy  
  
1.  Switch to Dimension Designer for the Customer dimension, and then click the **Dimension Structure** tab.  
  
    In the **Hierarchies** pane, notice the levels in the **Customer Geography** user-defined hierarchy. This hierarchy is currently just a drill-down path for users, as no relationship between levels or attributes have been defined.  
  
2.  Click the **Attribute Relationships** tab.  
  
    Notice the four attribute relationships that link the non-key attributes from the **Geography** table to the key attribute from the **Geography** table. The **Geography** attribute is related to the **Full Name** attribute. The **Postal Code** attribute is indirectly linked to the **Full Name** attribute through the **Geography** attribute, because the **Postal Code** is linked to the **Geography** attribute and the **Geography** attribute is linked to the **Full Name** attribute. Next, we will change the attribute relationships so that they do not use the **Geography** attribute.  
  
3.  In the diagram, right-click the **Full Name** attribute and then select **New Attribute Relationship**.  
  
4.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Full Name**. Set the **Related Attribute** to **Postal Code**. In the **Relationship type** list, leave the relationship type set to **Flexible** because relationships between the members might change over time.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
    A warning icon appears in the diagram because the relationship is redundant. The relationship **Full Name** -> **Geography**-> **Postal Code** already existed, and you just created the relationship **Full Name** -> **Postal Code**. The relationship **Geography**-> **Postal Code** is now redundant, so we will remove it.  
  
6.  In the **Attribute Relationships** pane, right-click **Geography**-> **Postal Code** and then click **Delete**.  
  
7.  When the **Delete Objects** dialog box appears, click **OK**.  
  
8.  In the diagram, right-click the **Postal Code** attribute and then select **New Attribute Relationship**.  
  
9. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Postal Code**. Set the **Related Attribute** to **City**. In the **Relationship type** list, leave the relationship type set to **Flexible**.  
  
10. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
    The relationship **Geography**-> **City** is now redundant so we will delete it.  
  
11. In the Attribute Relationships pane, right-click **Geography**-> **City** and then click **Delete**.  
  
12. When the **Delete Objects** dialog box appears, click **OK**.  
  
13. In the diagram, right-click the **City** attribute and then select **New Attribute Relationship**.  
  
14. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **City**. Set the **Related Attribute** to **State-Province**. In the **Relationship type** list, set the relationship type to **Rigid** because the relationship between a city and a state will not change over time.  
  
15. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
16. Right-click the arrow between **Geography** and **State-Province** and then click **Delete**.  
  
17. When the **Delete Objects** dialog box appears, click **OK**.  
  
18. In the diagram, right-click the **State-Province** attribute and then select **New Attribute Relationship**.  
  
19. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **State-Province**. Set the **Related Attribute** to **Country-Region**. In the **Relationship type** list, set the relationship type to **Rigid** because the relationship between a state-province and a country-region will not change over time.  
  
20. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
21. In the Attribute Relationships pane, right-click **Geography**-> **Country-Region** and then click **Delete**.  
  
22. When the **Delete Objects** dialog box appears, click **OK**.  
  
23. Click the **Dimension Structure** tab.  
  
    Notice that when you delete the last attribute relationship between **Geography** and other attributes, that **Geography** itself is deleted. This is because the attribute is no longer used.  
  
24. On the File menu, click **Save All**.  
  
## Defining Attribute Relationships for Attributes in the Sales Territory Hierarchy  
  
1.  Open Dimension Designer for the **Sales Territory** dimension, and then click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **Sales Territory Country** attribute and then select **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Sales Territory Country**. Set the **Related Attribute** to **Sales Territory Group**. In the **Relationship type** list, leave the relationship type set to **Flexible**.  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
    **Sales Territory Group** is now linked to **Sales Territory Country**, and **Sales Territory Country** is now linked to **Sales Territory Region**. The **RelationshipType** property for each of these relationships is set to **Flexible** because the groupings of regions within a country might change over time and because the groupings of countries into groups might change over time.  
  
## Defining Attribute Relationships for Attributes in the Product Model Lines Hierarchy  
  
1.  Open Dimension Designer for the **Product** dimension, and then click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **Model Name** attribute and then select **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Model Name**. Set the **Related Attribute** to **Product Line**. In the **Relationship type** list, leave the relationship type set to **Flexible**.  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## Defining Attribute Relationships for Attributes in the Fiscal Date Hierarchy  
  
1.  Switch to Dimension Designer for the **Date** dimension, and then click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **Month Name** attribute and then select **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Month Name**. Set the **Related Attribute** to **Fiscal Quarter**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
5.  In the diagram, right-click the **Fiscal Quarter** attribute and then select **New Attribute Relationship**.  
  
6.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Fiscal Quarter**. Set the **Related Attribute** to **Fiscal Semester**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
7.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
8.  In the diagram, right-click the **Fiscal Semester** attribute and then select **New Attribute Relationship**.  
  
9. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Fiscal Semester**. Set the **Related Attribute** to **Fiscal Year**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
10. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## Defining Attribute Relationships for Attributes in the Calendar Date Hierarchy  
  
1.  In the diagram, right-click the **Month Name** attribute and then select **New Attribute Relationship**.  
  
2.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Month Name**. Set the **Related Attribute** to **Calendar Quarter**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
3.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
4.  In the diagram, right-click the **Calendar Quarter** attribute and then select **New Attribute Relationship**.  
  
5.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Calendar Quarter**. Set the **Related Attribute** to **Calendar Semester**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
7.  In the diagram, right-click the **Calendar Semester** attribute and then select **New Attribute Relationship**.  
  
8.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Calendar Semester**. Set the **Related Attribute** to **Calendar Year**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
9. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## Defining Attribute Relationships for Attributes in the Geography Hierarchy  
  
1.  Open Dimension Designer for the Geography dimension, and then click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **Postal Code** attribute and then select **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Postal Code**. Set the **Related Attribute** to **City**. In the **Relationship type** list, set the relationship type to **Flexible**.  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
5.  In the diagram, right-click the **City** attribute and then select **New Attribute Relationship**.  
  
6.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **City**. Set the **Related Attribute** to **State-Province**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
7.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
8.  In the diagram, right-click the **State-Province** attribute and then select **New Attribute Relationship**.  
  
9. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **State-Province**. Set the **Related Attribute** to **Country-Region**. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
10. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
11. In the diagram, right-click the **Geography Key** attribute and then select **Properties**.  
  
12. Set the **AttributeHierarchyOptimizedState** property to **NotOptimized**, set the **AttributeHierarchyOrdered** property to **False**, and set the **AttributeHierarchyVisible** property to **False**.  
  
13. On the **File** menu, click **Save All**.  
  
14. On the **Build** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click **Deploy Analysis Services Tutorial**.  
  
## Next Task in Lesson  
[Defining the Unknown Member and Null Processing Properties](../analysis-services/lesson-4-7-defining-the-unknown-member-and-null-processing-properties.md)  
  
## See Also  
[Define Attribute Relationships](../analysis-services/multidimensional-models/attribute-relationships-define.md)  
[User Hierarchy Properties](../analysis-services/multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md)  
  
  
  
