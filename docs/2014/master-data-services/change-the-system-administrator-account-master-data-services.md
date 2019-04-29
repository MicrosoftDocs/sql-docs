---
title: "Change the System Administrator Account (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "master-data-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "administrators [Master Data Services], changing"
ms.assetid: cf30312e-4338-49a7-90f0-6e4f7b431ff8
author: leolimsft
ms.author: lle
manager: craigg
---
# Change the System Administrator Account (Master Data Services)
  You can change the user account that is designated as the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] system administrator.  
  
> [!WARNING]  
>  When you complete this procedure, the former system administrator user account is deleted.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must add the new administrator's user name to the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] Users list. For more information, see [Add a User &#40;Master Data Services&#41;](add-a-user-master-data-services.md).  
  
-   You must have permission to view mdm.tblUser and to execute the mdm.udpSecurityMemberProcessRebuildModel stored procedure in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. For more information, see [Database Object Security &#40;Master Data Services&#41;](../../2014/master-data-services/database-object-security-master-data-services.md).  
  
### To change the administrator account  
  
1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] and connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)] instance for your [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
2.  In mdm.tblUser, find the user that will be the new administrator and copy the value in the `SID` column.  
  
3.  Create a new query.  
  
4.  Type the following text, replacing *DOMAIN\user_name* with the new administrator's user name and *SID* with the value you copied in step 2.  
  
    ```  
    EXEC [mdm].[udpSecuritySetAdministrator] @UserName='DOMAIN\user_name', @SID = 'SID', @PromoteNonAdmin = 1  
    ```  
  
5.  Run the query.  
  
## See Also  
 [Administrators &#40;Master Data Services&#41;](../../2014/master-data-services/administrators-master-data-services.md)  
  
  
