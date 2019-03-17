---
title: "Exclude a Business Rule (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], excluding"
ms.assetid: bdbc9df0-23f7-40b9-8aba-4445c1482580
author: leolimsft
ms.author: lle
manager: craigg
---
# Exclude a Business Rule (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], exclude a business rule when you do not want to delete the rule permanently, but you do not want to validate data against it.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To exclude a business rule  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member.  
  
6.  From the **Attribute** list, select an attribute or leave the default of **All**.  
  
7.  In the grid, in the row for the business rule, select the check box in the **Exclude** column. The value in the **Status** column is **Exclusion pending**.  
  
8.  Click **Publish business rules**.  
  
9. In the confirmation dialog box, click **OK**. The value in the **Status** column is **Excluded**.  
  
## See Also  
 [Delete a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/delete-a-business-rule-master-data-services.md)   
 [Create and Publish a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/create-and-publish-a-business-rule-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/business-rules-master-data-services.md)  
  
  
