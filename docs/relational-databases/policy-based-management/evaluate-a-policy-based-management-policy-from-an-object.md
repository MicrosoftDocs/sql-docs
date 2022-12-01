---
title: "Evaluate Policy-Based Management policy from an object"
description: Learn how to evaluate a policy from a SQL Server instance, database, or database object using SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, evaluate policy"
ms.assetid: b9e9d646-4894-4dee-a02a-0c71a8dc020e
author: VanMSFT
ms.author: vanto
---
# Evaluate a Policy-Based Management Policy from an Object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to evaluate a policy from a server instance, database, or database object in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To evaluate a policy from an object, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The execution mode is defined as part of the policy and cannot be changed in the **Evaluate Policies** dialog box.  
  
-   The **Evaluate Policies** dialog box only shows policies appropriate for the database object.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To evaluate a policy from an object  
  
1.  In Object Explorer, right-click a server instance, a database, or a database object, point to **Policies**, and select **Evaluate**.  
  
2.  In the **Evaluate Policies** dialog box, select one or more policies and click **Evaluate** to run the policy in evaluation mode. This generates a compliance report for the target set but does not reconfigure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or enforce future compliance. For targets that do not comply with the selected policies and have properties that can be reconfigured by Policy-Based Management, you can enforce policy compliance by clicking **Apply**. For more information on the available options in the **Evaluate Policies** dialog box, see [Evaluate Policies Dialog Box, Policy Selection Page](../../relational-databases/policy-based-management/evaluate-policies-dialog-box-policy-selection-page.md), [Evaluate Policies Dialog Box, Evaluation Results Page](../../relational-databases/policy-based-management/evaluate-policies-dialog-box-evaluation-results-page.md), and [Results Detailed View Dialog Box](../../relational-databases/policy-based-management/results-detailed-view-dialog-box.md).  
  
3.  When finished, click **Close**.  
  
  
