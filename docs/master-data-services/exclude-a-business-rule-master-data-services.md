---
description: "Exclude a Business Rule (Master Data Services)"
title: Exclude a Business Rule
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], excluding"
ms.assetid: bdbc9df0-23f7-40b9-8aba-4445c1482580
author: CordeliaGrey
ms.author: jiwang6
---
# Exclude a Business Rule (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], exclude a business rule when you do not want to delete the rule permanently, but you do not want to validate data against it.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
### To exclude a business rule  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rules** page, from the **Model** drop-down list, select a model.  
  
4.  From the **Entity** drop-down list, select an entity.  
  
5.  From the **Member Types** drop-down list, select a type of member.  
  
6.  In the grid, select the row for the business rule you want to exclude and click **Edit**.  
  
7.  Mark the **Excluded** check-box.  
  
8.  Click **Save**.  
  
9. Click **Publish All**.  
  
10. In the confirmation dialog box, click **OK**. The value in the **Business Rule Status** column is **Excluded** and the **Excluded** column is **Yes**.  
  
## See Also  
 [Delete a Business Rule &#40;Master Data Services&#41;](../master-data-services/delete-a-business-rule-master-data-services.md)   
 [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)  
  
  
