---
title: "Tutorial: Preparing the Server for Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: ce30a095-2975-4387-9377-94a461ac78ee
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Tutorial: Preparing the Server for Replication
It is important to plan for security before you configure your replication topology. This tutorial shows you how to better secure a replication topology as well as how to configure distribution, which is the first step in replicating data. You must complete this tutorial before any of the others.  
  
> [!NOTE]  
> To replicate data securely between servers, you should implement all of the recommendations in [Replication Security Best Practices](../../relational-databases/replication/security/replication-security-best-practices.md).  
  
## What You Will Learn  
In this tutorial you will learn how to prepare a server so that replication can run securely with least privileges. The first lesson shows how to create the Windows service accounts used to run replication agents. The second lesson shows how to configure the folder used to generate and store publication snapshots. The third lesson shows how to configure distribution and set permissions.  
  
## Requirements  
This tutorial is intended for users familiar with fundamental database operations, but who have limited exposure to replication.  
  
To use this tutorial, your system must have the following components installed:  
  
-   [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
**Estimated time to complete this tutorial: 30 minutes.**  
  
## Lessons in This Tutorial  
  
-   [Lesson 1: Creating Windows Accounts for Replication](../../relational-databases/replication/lesson-1-creating-windows-accounts-for-replication.md)  
  
-   [Lesson 2: Preparing the Snapshot Folder](../../relational-databases/replication/lesson-2-preparing-the-snapshot-folder.md)  
  
-   [Lesson 3: Configuring Distribution](../../relational-databases/replication/lesson-3-configuring-distribution.md)  
  
[Start the Tutorial](../../relational-databases/replication/lesson-1-creating-windows-accounts-for-replication.md)  
  
## See Also  
[Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
[Security and Protection &#40;Replication&#41;](../../relational-databases/replication/security/security-and-protection-replication.md)  
  
  
  
