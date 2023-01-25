---
title: Business Rules Extension
description: You can apply user-defined SQL scripts as an extension of pre-defined business rule conditions and actions in Master Data Services.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 4c18be5f-a3fa-45a8-9be6-0f45f58bbc9e
author: CordeliaGrey
ms.author: jiwang6
---
# Business Rules Extension (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], you can apply user-defined SQL scripts as an extension of  pre-defined conditions and actions.  
  
> [!NOTE]  
>  All scripts  have to be defined under  [usr] schema.  
  
 SQL functions that meet the following criteria can be used as a Business Rule condition.  
  
-   The return value type must be BIT.  
  
-   Only following types are supported for parameter types.  
  
    -   NVARCHAR  
  
    -   DATETIME2  
  
    -   DECIMAL (precision, scale)  
  
         precision must be 38  
  
         scale must be a value from 0 through 7  
  
 SQL stored procedures that use the following syntax can be used as a Business Rule action  
  
```  
CREATE PROCEDURE [usr].[YourAction]  
       (         
            @MemberIdList mdm.[MemberId] READONLY,  
            @ModelName NVARCHAR(MAX),  
            @VersionName NVARCHAR(MAX),  
            @EntityName NVARCHAR(MAX),  
            @BusinessRuleName NVARCHAR(MAX)  
        )    
      AS BEGIN    
       ...     
       END  
  
```  
  
 User-defined scripts will not be added to deployment packages. Make sure the target Master Data Services database contains all scripts that are used in the business rules before deploying a package.  
  
 Script actions will be executed as mds_br_user which has following permissions  
  
|Schema|Permissions|  
|-|-|  
|mdm|SELECT|  
|stg|SELECT, UPDATE, DELETE, EXECUTE, INSERT|  
|usr|FULL|  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the System Administration functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md)  
  
-   User defined scripts had been added to the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
## Create a business rule to take a user-defined script as a condition or as an action  
  
1.  In Master Data Manager, click **System Administration**.  
  
2.  On the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rules** page, select a model from the **Model** drop-down list.  
  
4.  From the **Entity** drop-down list, select an entity.  
  
5.  From the **Member Types** drop-down list, select a type of member for the business rule to apply to.  
  
6.  Click **Add**.  
  
7.  Do the following to create a user-defined script as a condition.  
  
    1.  Under the **If** block, click on **Add** button. A panel will be displayed.  
  
    2.  From the **Operator** drop-down list, select the user-defined function under **User defined script** .  
  
    3.  All parameters of the user defined function are displayed.  
  
    4.  Assign a value to each parameter  
  
    5.  Click **Save**.  
  
8.  Do the following to take a user-defined script as an action.  
  
    1.  Under the **Then** block, click on **Add** button. A panel will be displayed.  
  
    2.  From the **Operator** drop-down list, select user-defined function under **User defined script** .  
  
    3.  Click **Save**.  
  
## See Also  
 [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)   
 [Business Rule Conditions &#40;Master Data Services&#41;](../master-data-services/business-rule-conditions-master-data-services.md)   
 [Business Rule Actions &#40;Master Data Services&#41;](../master-data-services/business-rule-actions-master-data-services.md)  
  
  
