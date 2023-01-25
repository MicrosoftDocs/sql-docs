---
description: "Add Multiple Conditions to a Business Rule (Master Data Services)"
title: Add Conditions to a Business Rule
ms.custom: "seo-lt-2019"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], multiple conditions"
ms.assetid: 5f0f6958-6cf2-444b-bdcd-05b887637a0b
author: CordeliaGrey
ms.author: jiwang6
---
# Add Multiple Conditions to a Business Rule (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], add multiple **AND** or **OR** conditions to a business rule when you want a more complex rule.  
  
> [!NOTE]  
>  If you create a business rule that uses the **OR** operator, consider creating a separate rule for each conditional statement that can be evaluated independently. You can then exclude rules as needed, providing more flexibility and easier troubleshooting.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   A business rule must exist. For more information, see [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md).  
  
### To add multiple conditions to a business rule  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rules/** page, from the **Model** drop-down list, select a model.  
  
4.  From the **Entity** drop-down list, select an entity.  
  
5.  From the **Member Types** drop-down list, select a type of member.  
  
6.  Click the row for the business rule you want to edit.  
  
7.  Click **Edit**.  
  
8.  Under the **If** block, on the left side from the logical operator drop-down list select **AND/OR/ NOT**.  
  
9. Click **Add**. A panel will be displayed.  
  
10. From the **Attribute** drop-down list, select an attribute.  
  
11. From the **Operator** drop-down list, select a condition.  
  
12. Complete any required fields.  
  
13. Click **Save**. A new row will be added to the **If** grid.  
  
14. Optionally, to add more conditions, complete steps 8-13.  
  
    > [!TIP]  
    >  To delete a condition, select the condition and right-click on it and click **Delete**.  
  
    > [!TIP]  
    >  You can select multiple conditions and right-click to group them inside a logical operator, or to ungroup conditions inside a specific logical operator.  
  
## See Also  
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Change a Business Rule Name &#40;Master Data Services&#41;](../master-data-services/change-a-business-rule-name-master-data-services.md)   
 [Configure Business Rules to Send Notifications &#40;Master Data Services&#41;](../master-data-services/configure-business-rules-to-send-notifications-master-data-services.md)  
  
  
