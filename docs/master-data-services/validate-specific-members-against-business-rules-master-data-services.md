---
title: "Validate Specific Members against Business Rules (Master Data Services) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "master-data-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "applying business rules [Master Data Services]"
  - "business rules [Master Data Services], applying to select members"
ms.assetid: 2288ef43-5392-47ea-b651-ec25e5692a14
caps.latest.revision: 7
author: "smartysanthosh"
ms.author: "nagavo"
manager: "craigg"
---
# Validate Specific Members against Business Rules (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], apply business rules selectively when you want to update or validate subsets of members against business rules.  
  
> [!NOTE]  
>  If you want to apply business rules to all members in a version of a model, see [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md).  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
-   You must have a minimum of **Update** permission to the model object you are applying business rules to.  
  
### To apply business rules selectively  
  
1.  On the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] home page, from the **Model** drop-down list, select a model.  
  
2.  From the **Version** drop-down list, select a version.  
  
3.  Click **Explorer** tab.  
  
4.  From the menu bar, point to **Entities** and click the name of the entity that contains members you want to apply rules to.  
  
5.  Click **Apply Rules**. Business rules are applied only to the members displayed in the grid.  
  
## See Also  
 [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)  
  
  