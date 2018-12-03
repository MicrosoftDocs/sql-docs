---
title: "New Agent Profile | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.profiles.newperfprofile.f1"
helpviewer_keywords: 
  - "New Agent Profile dialog box"
ms.assetid: ebf59330-a421-45a5-9020-0484a96852bc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# New Agent Profile
  Use the **New Agent Profile** dialog box to create a new profile. New profiles are always based on existing profiles, but they can be modified to meet application requirements. After a profile has been created, it can be applied to existing and future agent jobs in the **Agent Profiles** dialog box. Agent parameter values can be edited in the \<**AgentProfileName> Properties** dialog box.  
  
## Options  
 **Name**  
 Enter a name for the profile.  
  
 **Description**  
 Enter a description for the profile.  
  
 **Parameter**  
 The agent parameters included in the profile. The profile on which the new profile is based does not necessarily specify a value for each parameter. To see all parameters that are valid for a given agent, clear the **Show only parameters used in this profile** check box. For descriptions of each parameter, see:  
  
-   [Replication Snapshot Agent](agents/replication-snapshot-agent.md)  
  
-   [Replication Log Reader Agent](agents/replication-log-reader-agent.md)  
  
-   [Replication Distribution Agent](agents/replication-distribution-agent.md)  
  
-   [Replication Merge Agent](agents/replication-merge-agent.md)  
  
-   [Replication Queue Reader Agent](agents/replication-queue-reader-agent.md)  
  
 **Default Value**  
 The default value for each agent parameter.  
  
 **Value**  
 The value specified for the parameter in the profile on which the new profile is based. Edit this field for any parameter values you want to change.  
  
 **Show only parameters used in this profile**  
 Clear to show all valid parameters for a given agent.  
  
## See Also  
 [Work with Replication Agent Profiles](agents/work-with-replication-agent-profiles.md)   
 [Replication Agents Overview](agents/replication-agents-overview.md)   
 [Replication Agent Profiles](agents/replication-agent-profiles.md)  
  
  
