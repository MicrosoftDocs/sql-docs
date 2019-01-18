---
title: "Validate Specific Members against Business Rules (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "applying business rules [Master Data Services]"
  - "business rules [Master Data Services], applying to select members"
ms.assetid: 2288ef43-5392-47ea-b651-ec25e5692a14
author: leolimsft
ms.author: lle
manager: craigg
---
# Validate Specific Members against Business Rules (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

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
  
  
