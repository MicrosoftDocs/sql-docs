---
description: "Manage Policy Categories"
title: "Manage Policy Categories | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dmf.policycategories.f1"
ms.assetid: d188a819-731f-4029-98aa-780d3299a0ce
author: VanMSFT
ms.author: vanto
---
# Manage Policy Categories
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to apply any or all available policies in a category to the whole instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To apply category policies to a SQL Server instance, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   When using s[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], if the **Mandate Database Subscriptions** check box is not selected, the policy category must be individually applied to each relevant portion of the server, such as one or more databases or tables.  
  
-   If you specify a policy category that does not exist, a new policy category is created and the subscription is mandated for all databases when you execute the stored procedure. If you then clear the mandated subscription for the new category, the subscription will only apply for the database that you specified as the *target_object*. For more information about how to change a mandated subscription setting, see [sp_syspolicy_update_policy_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-update-policy-category-transact-sql.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 This stored procedure runs in the context of the current owner of the stored procedure.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To apply category policies to a SQL Server instance  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you will apply category policies.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Right-click **Policy Management** and select **Manage Categories**.  
  
     The following information is available in the **Manage Policy Categories** dialog box:  
  
     **Name**  
     The name of the policy category.  
  
     **Mandate Database Subscriptions**  
     Forces all databases on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to enforce policies in the policy category.  
  
4.  Select or clear any or all check boxes under **Mandate Database Subscriptions** to apply that policy category to the SQL Server instance.  
  
5.  When finished, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To apply category policies to a SQL Server instance  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb;  
    GO  
    -- configures the specified database to subscribe to a policy category that is named 'Table Naming Policies'.  
    EXEC dbo.sp_syspolicy_add_policy_category_subscription @target_type = N'DATABASE'  
    , @target_object = N'AdventureWorks2012'  
    , @policy_category = N'Table Naming Policies';  
    GO  
    ```  
  
 For more information, see [sp_syspolicy_add_policy_category_subscription &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-add-policy-category-subscription-transact-sql.md).  
  
  
