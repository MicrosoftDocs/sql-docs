---
title: "Maintenance Plan (Servers)"
description: Maintenance Plan (Servers)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.servers.f1"
  - "sql13.swb.maint.maintplanproperties.server.f1"
ms.assetid: ac24d1a8-dd2f-4162-b804-c0df1fc1e61d
---
# Maintenance Plan (Servers)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Servers** dialog box to select the servers where you want to run the maintenance plan.  
  
 A multiserver environment containing one master server and one or more target servers must be configured to create a multiserver maintenance plan. For multiserver maintenance plans, the local server should be configured as a master server. In multiserver environments, this dialog box displays the **(local)** master server and all corresponding target servers. One [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job is created for the local server. It is enabled or disabled depending on whether you select the **(local)** server. If target servers are selected, a multiserver job is created and downloaded to each of the selected target servers. If no target servers are selected, the multiserver job is deleted.  
  
## See Also  
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md)   
 [Make a Master Server](../../ssms/agent/make-a-master-server.md)   
 [Make a Target Server](../../ssms/agent/make-a-target-server.md)  
  
  
