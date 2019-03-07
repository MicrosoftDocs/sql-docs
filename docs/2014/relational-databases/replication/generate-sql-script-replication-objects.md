---
title: "Generate SQL Script (Replication Objects) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.generatesqlscript.f1"
helpviewer_keywords: 
  - "Generate SQL Script dialog box"
ms.assetid: b7ccc34e-1c22-44b8-8eb5-f6423af3164e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Generate SQL Script (Replication Objects)
  A replication script contains the [!INCLUDE[tsql](../../includes/tsql-md.md)] system stored procedures necessary to implement the replication components scripted, such as a publication or subscription. All replication components in a topology should be scripted as part of a disaster recovery plan, and scripts can also be used to automate repetitive tasks. Replication offers two dialog boxes for scripting replication objects:  
  
-   **Generate SQL Script**, which is available from the context menu of the **Replication** folder and all subfolders in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This dialog box allows you to script all replication objects on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Generate SQL Script \<ObjectName>**, which is available from the context menu for publications and subscriptions. This dialog box allows you to script individual objects.  
  
 These dialog boxes script objects on a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; they do not connect to other instances to script related objects.  
  
## Generate SQL Script Options  
 **Distributor properties**  
 Select to script stored procedures to: enable or disable the Distributor; add or drop Publishers associated with the Distributor; and create or drop the distribution database.  
  
 **Publications in the following data sources**  
 Select to script stored procedures to: enable or disable publishing; and create or drop publications, articles, push subscriptions, and replication jobs.  
  
 **Subscriptions in the following data sources**  
 Select to script stored procedures to create or drop pull subscriptions and replication jobs.  
  
 **To create or enable the components** and **To drop or disable the components**  
 Specify whether the script should include commands for creating or dropping a replication object. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use the dialog box to create a set of scripts for enabling and disabling components.  
  
 **Replication jobs**  
 Select to script replication jobs in addition to stored procedure calls. This option is available only when scripting from a Distributor.  
  
 Replication stored procedures create the necessary jobs when they are executed, so it is not required to select this option. However, it can be useful to have a record of the jobs created in case an individual job must be recreated.  
  
## Generate SQL Script \<ObjectName> Options  
 **To create or enable the components** and **To drop or disable the components**  
 Specify whether the script should include commands for creating or dropping a replication object. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you use the dialog box, creating a set of scripts for enabling and disabling components.  
  
 **Replication jobs**  
 This option is available only from the **Generate SQL Script** dialog box.  
  
## See Also  
 [Scripting Replication](scripting-replication.md)  
  
  
