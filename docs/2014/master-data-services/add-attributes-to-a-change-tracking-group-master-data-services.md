---
title: "Add Attributes to a Change Tracking Group (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "change tracking groups [Master Data Services]"
  - "attributes [Master Data Services], change tracking groups"
  - "change tracking groups [Master Data Services], adding attributes"
ms.assetid: e153eb5f-70ca-4c6f-89d8-1f937ed3917d
author: leolimsft
ms.author: lle
manager: craigg
---
# Add Attributes to a Change Tracking Group (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], add attributes to a change tracking group when you want to track changes to the attribute's values.  
  
> [!NOTE]  
>  After you add an attribute to a change tracking group, when values for the attribute change, the attribute is flagged as changed in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. Create a business rule to take action based on the change.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   Attributes must exist to add to the change tracking group. For more information, see [Create a Text Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-text-attribute-master-data-services.md).  
  
### To add attributes to a change tracking group  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model Explorer** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to track attribute values for.  
  
5.  Click **Edit selected entity**.  
  
6.  On the **Edit Entity** page:  
  
    -   If the attribute is for leaf members, in the **Leaf attributes** pane, select the attribute and click **Edit leaf attribute**.  
  
    -   If the attribute is for consolidated members, in the **Consolidated attributes** pane, select the attribute and click **Edit consolidated attribute**.  
  
    -   If the attribute is for collections, in the **Collection attributes** pane, select the attribute and click **Edit collection attribute**.  
  
7.  Select the **Enable change tracking** check box.  
  
8.  In the **Change tracking group** box, type a number for the group.  
  
9. Click **Save attribute**.  
  
10. On the **Entity Maintenance** page, click **Save entity**.  
  
11. Repeat this procedure for all attributes you want to include in the group. Use the same change tracking group number for each attribute in the group.  
  
## Next Steps  
  
-   [Initiate Actions Based on Attribute Value Changes &#40;Master Data Services&#41;](../../2014/master-data-services/initiate-actions-based-on-attribute-value-changes-master-data-services.md)  
  
## See Also  
 [Create a Text Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-text-attribute-master-data-services.md)   
 [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-domain-based-attribute-master-data-services.md)  
  
  
