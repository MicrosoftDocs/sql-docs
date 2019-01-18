---
title: "Validate a Version against Business Rules (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "validating versions [Master Data Services]"
  - "validating versions [Master Data Services], about validating versions"
  - "versions [Master Data Services], validating"
  - "business rules [Master Data Services], applying to all members"
ms.assetid: 5aee7901-6d05-41d4-8bbb-c6f26791d1df
author: leolimsft
ms.author: lle
manager: craigg
---
# Validate a Version against Business Rules (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], validate a version to apply business rules to all members in the model version.  
  
 This procedure explains how to use the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application to validate data. If you have permission in the MDS database, you can use a stored procedure instead. For more information, see [Validation Stored Procedure &#40;Master Data Services&#41;](../master-data-services/validation-stored-procedure-master-data-services.md).  
  
> [!NOTE]  
>  All members must pass validation before a version can be committed.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Version Management** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   The version's status must be **Open** or **Locked**.  
  
-   On the **Validate Versions** page, members must exist with a status other than **Validation succeeded**.  
  
### To validate a version  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **Version Management**.  
  
2.  On the **Manage Versions** page, from the menu bar, click **Validate Version**.  
  
3.  On the **Validate Versions** page, select the model and version you want to validate.  
  
4.  Click **Validate**.  
  
5.  In the confirmation dialog box, click **OK**.  
  
    > [!NOTE]  
    >  When the progress indicator is no longer displayed, the version has finished validation.  
  
## Next Steps  
  
-   [Lock a Version &#40;Master Data Services&#41;](../master-data-services/lock-a-version-master-data-services.md)  
  
## See Also  
 [Validation Statuses &#40;Master Data Services&#41;](../master-data-services/validation-statuses-master-data-services.md)   
 [Validation Stored Procedure &#40;Master Data Services&#41;](../master-data-services/validation-stored-procedure-master-data-services.md)   
 [Versions &#40;Master Data Services&#41;](../master-data-services/versions-master-data-services.md)   
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
  
