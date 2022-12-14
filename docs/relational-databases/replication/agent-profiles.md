---
description: "Agent Profiles"
title: "Agent Profiles | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.profiles.perfprofiles.f1"
helpviewer_keywords: 
  - "Agent Profiles dialog box"
ms.assetid: 0422e99c-e688-419b-bb4c-c7bebeef1d8d
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Agent Profiles
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Use the **Agent Profiles** dialog box to manage agent profiles. Agent profiles provide a convenient way to manage the runtime parameters for each agent. Each agent has a default profile, and some agents have additional predefined profiles. For example, the Merge Agent has a "slow link" profile designed for low bandwidth connections. Predefined profiles are sufficient for most applications, but you can also create user-defined profiles, allowing you to customize agent behavior.  
  
## Options  
 **Select a page**  
 Select an agent in the left pane, and the profiles for the agent are displayed in the right pane.  
  
 **Default for New**  
 Select the profile that will be used when jobs are created for an agent of a given type. For example, if you create a number of subscriptions to a merge publication, the Merge Agent job for each subscription will use the profile selected. If you want to change the profile for existing jobs, select a profile, and then click **Change Existing Agents**.  
  
 **Name**  
 The name of the profile.  
  
 **Type**  
 The type of profile: **User** (user-defined) or **System** (predefined).  
  
 **Properties (...)**  
 Click to view the values used for each parameter in the agent profile.  
  
 **New**  
 Click to create a new profile.  
  
 **Delete**  
 Select a user-defined profile, and then click **Delete** to delete that profile. Predefined profiles cannot be deleted.  
  
 **Change Existing Agents**  
 Select a profile, and then click **Change Existing Agents** to specify that all existing jobs for an agent of a given type should use the selected profile. For example, if you have created a number of subscriptions to a merge publication, and you want to change the profile to specify that the Merge Agent job for each of these subscriptions should use the **Slow link agent profile**, select that profile, and then click **Change Existing Agents**.  
  
## See Also  
 [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md)  
  
  
