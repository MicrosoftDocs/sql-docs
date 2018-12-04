---
title: "Automatically Generate Code Attribute Values (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 19b354ee-2906-4cc7-ba2f-32b4543bddcf
author: leolimsft
ms.author: lle
manager: craigg
---
# Automatically Generate Code Attribute Values (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], automatically generate values for an entity's Code attribute when you want an integer to be automatically assigned to the Code value each time a new member is created.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   The entity must exist. For more information, see [Create an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/create-an-entity-master-data-services.md).  
  
### To automatically generate Code values  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model Explorer** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Select the row for the entity that you want to generate codes for.  
  
5.  Click **Edit selected entity**.  
  
6.  Select the **Create Code values automatically** check box.  
  
7.  In the **Start with** box, type a number to begin incrementing. If members already exist, the Code will be set based on the highest existing value. For example, if the highest existing Code value is 299, the next member's Code value will be set to 300.  
  
8.  Click **Save entity**.  
  
## See Also  
 [Automatic Code Creation &#40;Master Data Services&#41;](../../2014/master-data-services/automatic-code-creation-master-data-services.md)   
 [Automatically Generate Attribute Values Other Than Code &#40;Master Data Services&#41;](../../2014/master-data-services/automatically-generate-attribute-values-other-than-code-master-data-services.md)  
  
  
