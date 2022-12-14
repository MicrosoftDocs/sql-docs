---
description: "Evaluate a Policy-Based Management Policy on a Schedule"
title: "Evaluate a Policy-Based Management Policy on a Schedule | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, evaluate policy"
ms.assetid: bea09522-ff47-4037-ab55-a98ea7c10099
author: VanMSFT
ms.author: vanto
---
# Evaluate a Policy-Based Management Policy on a Schedule
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to evaluate a Policy-Based Management policy on a schedule in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To evaluate a policy on a schedule, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To evaluate a policy on a schedule  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the policy schedule that you want to evaluate.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Click the plus sign to expand the **Policies** folder.  
  
5.  Right-click the policy whose schedule you what to evaluate and select **Properties**.  
  
6.  On the **Open Policy -**_policy_name_ dialog box, in the **Evaluation Mode** list, select **On schedule**.  
  
7.  Under **Schedule**, click either **Pick** to specify an existing schedule or **New** to create a new schedule.  
  
8.  When finished, click **OK**.  
  
  
