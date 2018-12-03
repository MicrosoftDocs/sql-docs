---
title: "Automatically Generate Code Attribute Values (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 19b354ee-2906-4cc7-ba2f-32b4543bddcf
author: leolimsft
ms.author: lle
manager: craigg
---
# Automatically Generate Code Attribute Values (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], automatically generate values for an entity's Code attribute when you want an integer to be automatically assigned to the Code value each time a new member is created.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   The entity must exist. For more information, see [Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md).  
  
### To automatically generate Code values  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Manage Model** page, select the row for the model that contains the entity that you want to edit and then click **Entities**.  
  
3.  On the **Manage Entity** page, select the row for the entity that you want to generate codes for and then click **Edit**.  
  
4.  Select the **Create Code values automatically** check box.  
  
5.  In the **Start with** box, type a number to begin incrementing. If members already exist, the Code will be set based on the highest existing value. For example, if the highest existing Code value is 299, the next member's Code value will be set to 300.  
  
6.  Click **Save**.  
  
## See Also  
 [Automatic Code Creation &#40;Master Data Services&#41;](../master-data-services/automatic-code-creation-master-data-services.md)   
 [Automatically Generate Attribute Values Other Than Code &#40;Master Data Services&#41;](../master-data-services/automatically-generate-attribute-values-other-than-code-master-data-services.md)  
  
  
