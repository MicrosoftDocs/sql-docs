---
title: "Tutorial: Replicating Data with Mobile Clients | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: af673514-30c7-403a-9d18-d01e1a095115
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Tutorial: Replicating Data with Mobile Clients
  Replication is a good solution to the problem of moving data between a central server and mobile clients that are only occasionally connected. Using replication's wizards, you can easily configure and administer a replication topology. This tutorial shows you how to configure a replication topology for mobile clients.  
  
## What You Will Learn  
 In this tutorial you will use merge replication to publish data from a central database to one or more mobile users so that each user gets a uniquely filtered subset of the data. The first lesson shows how to use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create a publication. Later lessons show how to create and synchronize a subscription.  
  
## Requirements  
 This tutorial is intended for users familiar with fundamental database operations, but who have limited experience with replication. Before you start this tutorial, you must complete [Tutorial: Preparing the Server for Replication](tutorial-preparing-the-server-for-replication.md).  
  
 To use this tutorial, your system must have the following components installed:  
  
-   At the Publisher server (source):  
  
    -   Any edition of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], except for Express ([!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]) or [!INCLUDE[ssEW](../../includes/ssew-md.md)]. These editions cannot be a replication Publisher.  
  
    -   The [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. To enhance security, the sample databases are not installed by default.  
  
-   Subscriber server (destination):  
  
    -   Any edition of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], except for [!INCLUDE[ssEW](../../includes/ssew-md.md)]. [!INCLUDE[ssEW](../../includes/ssew-md.md)] is not supported by the publication created in this tutorial.  
  
    > [!NOTE]  
    >  Replication is not installed by default on [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)].  
  
> [!NOTE]  
>  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must connect to the Publisher and Subscriber using a login that is a member of the sysadmin fixed server role.  
  
 **Estimated time to complete this tutorial: 30 minutes.**  
  
## Lessons in This Tutorial  
  
-   [Lesson 1: Publishing Data Using Merge Replication](lesson-1-publishing-data-using-merge-replication.md)  
  
-   [Lesson 2: Creating a Subscription to the Merge Publication](lesson-2-creating-a-subscription-to-the-merge-publication.md)  
  
 [Start the Tutorial](merge/merge-replication.md)  
  
## See Also  
 [Replication Programming Concepts](concepts/replication-programming-concepts.md)  
  
  
