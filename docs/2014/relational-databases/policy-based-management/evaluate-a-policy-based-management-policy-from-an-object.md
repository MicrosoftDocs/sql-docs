---
title: "Evaluate a Policy-Based Management Policy from an Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, evaluate policy"
ms.assetid: b9e9d646-4894-4dee-a02a-0c71a8dc020e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Evaluate a Policy-Based Management Policy from an Object
  This topic describes how to evaluate a policy from a server instance, database, or database object in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
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
  
2.  In the **Evaluate Policies** dialog box, select one or more policies and click **Evaluate** to run the policy in evaluation mode. This generates a compliance report for the target set but does not reconfigure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or enforce future compliance. For targets that do not comply with the selected policies and have properties that can be reconfigured by Policy-Based Management, you can enforce policy compliance by clicking **Apply**. For more information on the available options in the **Evaluate Policies** dialog box, see [Evaluate Policies Dialog Box, Policy Selection Page](evaluate-policies-dialog-box-policy-selection-page.md), [Evaluate Policies Dialog Box, Evaluation Results Page](evaluate-policies-dialog-box-evaluation-results-page.md), and [Results Detailed View Dialog Box](results-detailed-view-dialog-box.md).  
  
3.  When finished, click **Close**.  
  
  
