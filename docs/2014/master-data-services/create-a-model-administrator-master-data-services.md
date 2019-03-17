---
title: "Create a Model Administrator (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "administrators [Master Data Services], creating"
ms.assetid: dae17afc-3b39-490e-b51f-2d8da26d429e
author: leolimsft
ms.author: lle
manager: craigg
---
# Create a Model Administrator (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create a model administrator when you want a group or user to have **Update** permission to all objects in one or more models.  
  
> [!TIP]  
>  To simplify administration, create a Windows or local group and configure it as a model adminstrator. You can then add and remove users from the group without accessing [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **User and Group Permissions** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
### To create a model administrator  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **User and Group Permissions**.  
  
2.  On the **Users** or **Groups** page, select the row for the user or group that you want to edit.  
  
3.  Click **Edit selected user**.  
  
4.  Click the **Models** tab.  
  
5.  Optionally, select a model from the **Model** list.  
  
6.  Click **Edit**.  
  
7.  Click the model you want to grant permission to.  
  
8.  From the menu, select **Update**.  
  
9. Complete steps 7 and 8 for each model you want the group or user to be an administrator for.  
  
10. Click **Save**.  
  
## Remarks  
 Do not assign any other permissions to model objects or hierarchy members. If you do, the user is no longer an administrator and cannot view the model in any functional area other than **Explorer**.  
  
 There is one exception: if the user has **Update** permission assigned to a hierarchy **Root** on the **Hierarchy Members** tab, the user is still considered a model administrator.  
  
## See Also  
 [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md)   
 [Assign Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/assign-model-object-permissions-master-data-services.md)   
 [Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/assign-hierarchy-member-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/model-object-permissions-master-data-services.md)   
 [Hierarchy Member Permissions &#40;Master Data Services&#41;](../../2014/master-data-services/hierarchy-member-permissions-master-data-services.md)  
  
  
