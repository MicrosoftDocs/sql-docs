---
title: "Resource Governor Workload Group | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Resource Governor, workload group"
  - "workload groups [SQL Server]"
  - "workload groups [SQL Server], overview"
ms.assetid: a84c3c3f-55b6-4a30-9c42-13f082d9281e
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Resource Governor Workload Group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resource Governor, a workload group serves as a container for session requests that have similar classification criteria. A workload allows for aggregate monitoring of the sessions, and defines policies for the sessions. Each workload group is in a resource pool, which represents a subset of the physical resources of an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. When a session is started, the Resource Governor classifier assigns the session to a specific workload group, and the session must run using the policies assigned to the workload group and the resources defined for the resource pool.  
  
## Workload Group Concepts  
 A workload group serves as a container for session requests that are similar according to the classification criteria that are applied to each request. A workload group allows the aggregate monitoring of resource consumption and the application of a uniform policy to all the requests in the group. A group defines the policies for its members.  
  
> [!NOTE]  
>  User-defined workload groups can be moved from one resource pool to another.  
  
 Resource Governor predefines two workload groups: the internal group and the default group. A user cannot change anything classified as an internal group, but can monitor it. Requests are classified into the default group when the following conditions exist:  
  
-   There are no criteria to classify a request.  
  
-   There is an attempt to classify the request into a non-existent group.  
  
-   There is a general classification failure.  
  
 Resource Governor also provides DDL statements for creating, changing, and dropping workload groups.  
  
## Workload Group Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to create a workload group.|[Create a Workload Group](../../relational-databases/resource-governor/create-a-workload-group.md)|  
|Describes how to change workload group settings.|[Change Workload Group Settings](../../relational-databases/resource-governor/change-workload-group-settings.md)|  
|Describes how to delete a workload group.|[Delete a Workload Group](../../relational-databases/resource-governor/delete-a-workload-group.md)|  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [Enable Resource Governor](../../relational-databases/resource-governor/enable-resource-governor.md)   
 [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)   
 [Resource Governor Classifier Function](../../relational-databases/resource-governor/resource-governor-classifier-function.md)   
 [Configure Resource Governor Using a Template](../../relational-databases/resource-governor/configure-resource-governor-using-a-template.md)   
 [View Resource Governor Properties](../../relational-databases/resource-governor/view-resource-governor-properties.md)  
  
  
