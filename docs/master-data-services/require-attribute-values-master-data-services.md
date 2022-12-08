---
description: "Require Attribute Values (Master Data Services)"
title: Require Attribute Values
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], requiring attribute values"
  - "attributes [Master Data Services], requiring values"
ms.assetid: a360ef13-0c34-43b8-a87e-2f5d8732d30e
author: CordeliaGrey
ms.author: jiwang6
---
# Require Attribute Values (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], require attribute values when you want to ensure your master data is complete.  
  
> [!NOTE]  
>  Members that are missing domain-based attribute values are not displayed in derived hierarchies that are based on those relationships.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To require attribute values  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rules** page, from the **Model** drop-down list, select a model.  
  
4.  From the **Entity** drop-down list, select an entity.  
  
5.  From the **Member Types** drop-down list, select a type of member for the business rule to apply to.  
  
6.  Click **Add**.  
  
7.  In the **Name** box, type a name for the business rule.  
  
8.  Optionally, in the **Description** field, type the business rule description.  
  
9. Under the **Then** block, click **Add**. A panel will be displayed.  
  
10. From the **Operator** drop-down list, select **required action**.  
  
11. From the **Attribute** drop-down list, select an attribute.  
  
12. Click **Save**. A new row will be added to the **Then** grid.  
  
13. Click **Save**.  
  
14. Click **Publish All**.  
  
15. On the confirmation dialog box, click **OK**. The value in the **Business Rule State** column is **Active**.  
  
## Next Steps  
  
-   Apply business rules to data by following one of these procedures:  
  
    -   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
    -   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
## See Also  
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md)  
  
  
