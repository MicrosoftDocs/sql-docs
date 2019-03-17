---
title: "Tutorial: Replicating Data Between Continuously Connected Servers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "tutorials [SQL Server replication]"
  - "replication [SQL Server], tutorials"
  - "wizards [SQL Server replication]"
ms.assetid: 7b18a04a-2c3d-4efe-a0bc-c3f92be72fd0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Tutorial: Replicating Data Between Continuously Connected Servers
  Replication is a good solution to the problem of moving data between continuously connected servers. Using replication's wizards, you can easily configure and administer a replication topology. This tutorial shows you how to configure a replication topology for continuously connected servers.  
  
## What You Will Learn  
 This tutorial will show you how to publish data from one database to another using transactional replication. The first lesson shows how to use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create a publication. Later lessons show how to create and validate a subscription and how to measure latency.  
  
## Requirements  
 This tutorial is intended for users who are familiar with basic database operations, but who have limited experience with replication. This tutorial requires that you have completed the previous tutorial, [Preparing the Server for Replication](tutorial-preparing-the-server-for-replication.md).  
  
 To use this tutorial, your system must have the following components:  
  
-   At the Publisher server (source):  
  
    -   Any edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], except Express ([!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]) or [!INCLUDE[ssEW](../../includes/ssew-md.md)]. These editions cannot be replication Publishers.  
  
    -   [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] sample database. To enhance security, the sample databases are not installed by default.  
  
-   Subscriber server (destination):  
  
    -   Any edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], except [!INCLUDE[ssEW](../../includes/ssew-md.md)]. [!INCLUDE[ssEW](../../includes/ssew-md.md)] cannot be a Subscriber in transactional replication.  
  
    > [!NOTE]  
    >  Replication is not installed on [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] by default.  
  
> [!NOTE]  
>  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must connect to the Publisher and Subscriber using a login that is a member of the **sysadmin** fixed server role.  
  
 **Estimated time to complete this tutorial: 30 minutes.**  
  
## Lessons in This Tutorial  
  
-   [Lesson 1: Publishing Data Using Transactional Replication](lesson-1-publishing-data-using-transactional-replication.md)  
  
-   [Lesson 2: Creating a Subscription to the Transactional Publication](lesson-2-creating-a-subscription-to-the-transactional-publication.md)  
  
-   [Lesson 3: Validating the Subscription and Measuring Latency](lesson-3-validating-the-subscription-and-measuring-latency.md)  
  
 [Start the Tutorial](transactional/transactional-replication.md)  
  
## See Also  
 [Replication Programming Concepts](concepts/replication-programming-concepts.md)  
  
  
