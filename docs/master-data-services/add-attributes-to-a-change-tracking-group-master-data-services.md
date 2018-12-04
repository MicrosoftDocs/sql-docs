---
title: "Add Attributes to a Change Tracking Group (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
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

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], add attributes to a change tracking group when you want to track changes to the attribute's values.  
  
> [!NOTE]  
>  After you add an attribute to a change tracking group, when values for the attribute change, the attribute is flagged as changed in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. Create a business rule to take action based on the change.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   Attributes must exist to add to the change tracking group. For more information, see [Create a Text Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-text-attribute-master-data-services.md).  
  
### To add attributes to a change tracking group  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and then click **Entities**.  
  
3.  On the **Manage Entity** page, select the row for the entity that you want to create an attribute for.  
  
4.  Click **Attributes**.  
  
5.  On the **Manage Attributes** page, do one of the following.  
  
    -   If the attribute is for leaf members, select **Leaf** from the **Member Types** list box.  
  
    -   If the attribute is for consolidated members, select **Consolidated** from the **Member Types** list box.  
  
    -   If the attribute is for collections, select **Collection** from the **Member Types** list box.  
  
6.  Select the row for the attribute you want to edit, and then click **Edit**.  
  
7.  Select the **Enable change tracking** check box.  
  
8.  In the **Change tracking group** box, type a number for the group.  
  
9. Click **Save attribute**.  
  
     For the edited attribute, the **Enable Change Tracking Group** column in the grid is changed to **Yes (Group: entered group number)**.  
  
10. Repeat this procedure for all attributes you want to include in the group. Use the same change tracking group number for each attribute in the group.  
  
## Next Steps  
  
-   [Initiate Actions Based on Attribute Value Changes &#40;Master Data Services&#41;](../master-data-services/initiate-actions-based-on-attribute-value-changes-master-data-services.md)  
  
## See Also  
 [Create a Text Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-text-attribute-master-data-services.md)   
 [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-domain-based-attribute-master-data-services.md)  
  
  
