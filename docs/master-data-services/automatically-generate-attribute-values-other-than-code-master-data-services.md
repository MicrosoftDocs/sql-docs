---
description: "Automatically Generate Attribute Values Other Than Code (Master Data Services)"
title: Automatically Generate Attribute Values
titleSuffix: Master Data Services
ms.custom: "seo-lt-2019"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: b82f6f81-6e9c-4918-9ea9-4ab5f5d11b15
author: CordeliaGrey
ms.author: jiwang6
---
# Automatically Generate Attribute Values Other Than Code (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], automatically generate values for an entity's attribute values when you want an integer to be automatically assigned as the value each time business rules are applied.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   A numeric attribute must exist. For more information, see [Create a Numeric Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-numeric-attribute-master-data-services.md).  
  
### To automatically generate an attribute value  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member for the business rule to apply to.  
  
6.  From the **Attribute** list, leave the default of **All**.  
  
7.  Click **Add business rule**.  
  
8.  Click **Edit selected business rule**.  
  
9. In the **Components** pane, expand the **Actions** node.  
  
10. In the Default Value node, click **defaults to a generated value** and drag it to the **THEN** pane's **Actions** label.  
  
11. In the **Attributes** pane, click the attribute with the value you want to generated and drag it to the **Edit Action** pane's **Select attribute** label.  
  
12. Type a value in the **Start with** and **Increment by** boxes. If members already exist, the value will be set based on the highest existing value. For example, if the highest existing value is 299 and you set **Increment by** to **1**, the next member's value will be set to 300.  
  
13. In the **Edit Action** pane, click **Save item**.  
  
14. Click **Back**.  
  
15. Optionally, on the **Business Rules Maintenance** page, for the row that contains your business rule, double-click a cell in the **Name**, **Description**, or **Notification** column to update the value.  
  
16. Click **Publish business rules**.  
  
17. On the confirmation dialog box, click **OK**. The rule's status changes to **Active**.  
  
## Next Steps  
  
-   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
-   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
## See Also  
 [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Validation &#40;Master Data Services&#41;](../master-data-services/validation-master-data-services.md)  
  
  
