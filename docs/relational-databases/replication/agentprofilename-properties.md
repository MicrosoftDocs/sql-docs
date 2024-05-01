---
title: "&lt;AgentProfileName&gt; Properties"
description: "&lt;AgentProfileName&gt; Properties"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rep.profiles.perfprofileprops.f1"
helpviewer_keywords:
  - "Agent Profile Properties dialog box"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# &lt;AgentProfileName&gt; Properties
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Use the **Agent Profiles Properties** dialog box to view the values specified for each agent parameter in a profile and to modify the values for user-defined profiles.  
  
## Options  
 **Name**  
 The name of the profile.  
  
 **Description**  
 A description of the profile.  
  
 **Parameter**  
 The agent parameters included in the profile. Profiles do not necessarily specify a value for each parameter. To see all parameters that are valid for a given agent, clear the **Show only parameters used in this profile** check box. For descriptions of each parameter, see:  
  
-   [Replication Snapshot Agent](../../relational-databases/replication/agents/replication-snapshot-agent.md)  
  
-   [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md)  
  
-   [Replication Distribution Agent](../../relational-databases/replication/agents/replication-distribution-agent.md)  
  
-   [Replication Merge Agent](../../relational-databases/replication/agents/replication-merge-agent.md)  
  
-   [Replication Queue Reader Agent](../../relational-databases/replication/agents/replication-queue-reader-agent.md)  
  
 **Default Value**  
 The default value for each agent parameter.  
  
 **Value**  
 The value specified for the parameter in the profile. This field is editable for user-defined profiles.  
  
 **Show only parameters used in this profile**  
 Clear to show all valid parameters for a given agent.  
  
## See Also  
 [Work with Replication Agent Profiles](../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)   
 [Replication Agents Overview](../../relational-databases/replication/agents/replication-agents-overview.md)   
 [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md)  
  
  
