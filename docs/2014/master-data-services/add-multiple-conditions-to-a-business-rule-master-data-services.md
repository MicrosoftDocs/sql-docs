---
title: "Add Multiple Conditions to a Business Rule (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], multiple conditions"
ms.assetid: 5f0f6958-6cf2-444b-bdcd-05b887637a0b
author: leolimsft
ms.author: lle
manager: craigg
---
# Add Multiple Conditions to a Business Rule (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], add multiple **AND** or **OR** conditions to a business rule when you want a more complex rule.  
  
> [!NOTE]  
>  If you create a business rule that uses the **OR** operator, consider creating a separate rule for each conditional statement that can be evaluated independently. You can then exclude rules as needed, providing more flexibility and easier troubleshooting.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   A business rule must exist. For more information, see [Create and Publish a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/create-and-publish-a-business-rule-master-data-services.md).  
  
### To add multiple conditions to a business rule  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member.  
  
6.  From the **Attribute** list, select an attribute or leave the default of **All**.  
  
7.  Click the row for the business rule you want to edit.  
  
8.  Click **Edit selected business rule**.  
  
9. In the **Components** pane, expand the **Logical Operators** node.  
  
10. Click **AND** or **OR** and drag it to the **IF** pane's **AND** label.  
  
11. In the **Components** pane, expand the **Conditions** node.  
  
12. Click a condition and drag it to **IF** pane, to the **AND** or **OR** label from step 10.  
  
13. In the **Attributes** pane, click an attribute and drag it to the **Edit Condition** pane's **Select attribute** label.  
  
14. In the **Edit Condition** pane, complete any required fields.  
  
15. In the **Edit Condition** pane, click **Save item**.  
  
16. Optionally, to add more conditions, from the **Components** pane, drag **AND** or **OR** to any **AND** or **OR** in the **IF** pane. Then follow steps 13-15.  
  
    > [!TIP]  
    >  To delete a condition, click the name of the condition and in the **Edit Condition** pane, click **Delete item**.  
  
## See Also  
 [Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/business-rules-master-data-services.md)   
 [Change a Business Rule Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-a-business-rule-name-master-data-services.md)   
 [Configure Business Rules to Send Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/configure-business-rules-to-send-notifications-master-data-services.md)  
  
  
