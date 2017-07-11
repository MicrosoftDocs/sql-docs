---
title: "Export a Policy-Based Management Policy | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Policy-Based Management, export policy"
ms.assetid: f0001b33-9078-4432-8460-496736fb325a
caps.latest.revision: 7
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Export a Policy-Based Management Policy
  This topic describes how to export a Policy-Based Management policy in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To export a policy, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To export a policy  
  
1.  In Object Explorer, click the plus sign to expand the server that contains the Policy-Based Management policy that you want to export.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Click the plus sign to expand the **Policies** folder.  
  
5.  Right-click the policy that you want to export and select **Export Policy**.  
  
6.  In the **Export Policy** dialog box, type the path and name of the file in the address bar. Alternately, find a suitable location for the file in the dialog box's navigation pane, and then type the name of the XML file in the **File Name** box.  
  
7.  When finished, click **Save**.  
  
  