---
title: "Configure general properties of Policy-Based Management"
description: Learn how to configure the properties for Policy-Based Management using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dmf.PolicyManagement.f1"
helpviewer_keywords: 
  - "Policy-Based Management, configure properties"
ms.assetid: 6d1e0e37-29ea-408a-a055-384984d884be
author: VanMSFT
ms.author: vanto
---
# Configure the General Properties of Policy-Based Management
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to configure the properties for Policy-Based Management in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To configure Policy-Based Management, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure Policy-Based Management  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to configure Policy-Based Management properties.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Right-click **Policy Management** and select **Properties**.  
  
     The following options are available in **Policy Management Properties** dialog box.  
  
     **Enabled**  
     Specifies whether Policy-Based Management is enabled.  
  
     **HistoryRetentionInDays**  
     Specifies the number of days that policy evaluation history should be retained. If this value is 0 (the default), the history will not be automatically removed.  
  
     **LogOnSuccess**  
     Specifies whether Policy-Based Management logs successful policy evaluations.  
  
    -   When this value is false (the default), only failed policy evaluations are logged.  
  
    -   When this value is true, both successful and failed policy evaluations are logged.  
  
4.  When finished, click **OK**.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure Policy-Based Management  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- enables Policy-Based Management   
    USE msdb;  
    GO  
    EXEC dbo.sp_syspolicy_configure   
         @name = N'Enabled',   
         @value = 1;  
    GO  
    ```  
  
 For more information, see [sp_syspolicy_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-configure-transact-sql.md).  
  
  
