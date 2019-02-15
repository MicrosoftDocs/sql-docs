---
title: "Create and Publish a Business Rule (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], creating"
  - "creating business rules [Master Data Services]"
ms.assetid: 6961d636-4d69-468e-81f7-8d0be6a4a039
author: leolimsft
ms.author: lle
manager: craigg
---
# Create and Publish a Business Rule (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a business rule to ensure the accuracy of your master data. After you create a rule, you must publish it before you can apply it to data.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To create and publish a business rule  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member for the business rule to apply to.  
  
6.  From the **Attribute** list, select an attribute or leave the default of **All**.  
  
7.  Click **Add business rule**.  
  
8.  Click **Edit selected business rule**.  
  
9. In the **Components** pane, expand the **Conditions** node.  
  
10. Click a condition and drag it to the **IF** pane's **Conditions** label.  
  
    > [!TIP]  
    >  You can delete items from your business rule by right-clicking and choosing **Delete**.  
  
11. In the **Attributes** pane, click an attribute and drag it to the **Edit Condition** pane's **Select attribute** label.  
  
12. In the **Edit Condition** pane, complete any required fields.  
  
13. In the **Edit Condition** pane, click **Save item**.  
  
14. In the **Components** pane, expand the **Actions** node.  
  
15. Click an action and drag it to the **THEN** pane's **Action** label.  
  
16. In the **Attributes** pane, click an attribute and drag it to the **Edit Action** pane's **Select attribute** label.  
  
17. In the **Edit Action** pane, complete any required fields.  
  
18. In the **Edit Action** pane, click **Save item**.  
  
19. Optionally, add multiple conditions to the rule. For more information, see [Add Multiple Conditions to a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/add-multiple-conditions-to-a-business-rule-master-data-services.md).  
  
20. Click **Back**.  
  
21. Optionally, on the **Business Rules Maintenance** page, for the row that contains your business rule, double-click a cell in the **Name**, **Description**, or **Notification** column to update the value.  
  
    > [!NOTE]  
    >  Notifications are sent only for rules that include a validation action.  
  
22. Click **Publish business rules**.  
  
23. On the confirmation dialog box, click **OK**. The rule's status changes to **Active**.  
  
## Next Steps  
  
-   Apply business rules to data by following one of these procedures:  
  
    -   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
    -   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
## See Also  
 [Configure Business Rules to Send Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/configure-business-rules-to-send-notifications-master-data-services.md)   
 [Change a Business Rule Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-a-business-rule-name-master-data-services.md)   
 [Add Multiple Conditions to a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/add-multiple-conditions-to-a-business-rule-master-data-services.md)  
  
  
