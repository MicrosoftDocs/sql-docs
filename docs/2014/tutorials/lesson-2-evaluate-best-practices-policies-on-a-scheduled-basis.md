---
title: "Lesson 2: Evaluate Best Practices Policies on a Scheduled Basis | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
ms.assetid: 37ffad63-d6db-4609-8deb-786200659554
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Lesson 2: Evaluate Best Practices Policies on a Scheduled Basis
  You can configure scheduled evaluations of best practices policies on one or more instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. To configure best practices policies to run on a scheduled basis, you must import the policies into the target instance.  
  
 To deploy scheduled policies to multiple servers, you can import the policies to one instance, configure the schedules for each policy, export the scheduled policies to a folder, and then deploy the scheduled policies to target instances through Registered Servers.  
  
> [!IMPORTANT]  
>  The target instances must be running [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] or a later version. Automation requires the policies to be stored locally on the instance, which is not supported by versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that are earlier than [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
 In this lesson, you will do the following:  
  
-   Import the best practices policies into an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
-   Configure the policies to run on a schedule.  
  
-   Deploy the scheduled best practices policies to multiple instances through Registered Servers.  
  
 This lesson contains the following topics:  
  
-   [Import the Policies to a Single Instance](../../2014/tutorials/import-the-policies-to-a-single-instance.md)  
  
-   [Schedule the Policies](../../2014/tutorials/schedule-the-policies.md)  
  
-   [Deploy Scheduled Policies to Multiple Instances](../../2014/tutorials/deploy-scheduled-policies-to-multiple-instances.md)  
  
## See Also  
 [Administer Multiple Servers Using Central Management Servers](../relational-databases/administer-multiple-servers-using-central-management-servers.md)  
  
  
