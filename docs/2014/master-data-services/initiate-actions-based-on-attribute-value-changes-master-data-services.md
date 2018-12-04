---
title: "Initiate Actions Based on Attribute Value Changes (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], tracking attribute changes"
  - "change tracking groups [Master Data Services], initiating actions"
ms.assetid: 5e4402ce-31db-4774-a2a1-552335f87693
author: leolimsft
ms.author: lle
manager: craigg
---
# Initiate Actions Based on Attribute Value Changes (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a business rule to initiate actions based on changes to attribute values. For example, when a specific attribute value changes, you may want to change a value, send a notification, or start an external workflow.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   Your attributes must be in a change tracking group. See [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../../2014/master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md) for more information.  
  
### To create a business rule to initiate actions based on attribute value changes  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member for the business rule to apply to.  
  
6.  From the **Attribute** list, select an attribute or leave the default of **All**.  
  
7.  Click **Add business rule**.  
  
8.  Click **Edit selected business rule**.  
  
9. In the **Components** pane, expand the **Conditions** node.  
  
10. Under the **Value comparison** node, drag **has changed** to the **IF** pane's **Conditions** label.  
  
11. In the **Attributes** pane, click an attribute and drag it to the **Edit Condition** pane's **Select attribute** label. This attribute has no effect on the rule, so select any available attribute.  
  
12. In the **Edit Condition** pane, in the **Change tracking group** box, type the number of the change tracking group that you assigned as part of the prerequisites.  
  
13. In the **Edit Condition** pane, click **Save item**.  
  
14. In the **Components** pane, expand the **Actions** node.  
  
15. Click an action and drag it to the **THEN** pane's **Action** label.  
  
16. In the **Attributes** pane, click an attribute and drag it to the **Edit Action** pane's **Select attribute** label.  
  
17. In the **Edit Action** pane, complete any required fields.  
  
18. In the **Edit Action** pane, click **Save item**.  
  
19. Click **Back**.  
  
20. Optionally, on the **Business Rules Maintenance** page, for the row that contains your business rule, double-click a cell in the **Name**, **Description**, or **Notification** column to update the value.  
  
    > [!NOTE]  
    >  Notifications are sent only for rules that include a validation action.  
  
21. Click **Publish business rules**.  
  
22. On the confirmation dialog box, click **OK**. The rule's status changes to **Active**.  
  
## Next Steps  
  
-   Apply business rules to data by following one of these procedures:  
  
    -   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
    -   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
## See Also  
 [Add Attributes to a Change Tracking Group &#40;Master Data Services&#41;](../../2014/master-data-services/add-attributes-to-a-change-tracking-group-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/business-rules-master-data-services.md)  
  
  
