---
title: "Maintenance Plan (Servers)"
description: Maintenance Plan (Servers)
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/27/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.servers.f1"
  - "sql13.swb.maint.maintplanproperties.server.f1"
---
# Maintenance Plan (Servers)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the **Servers** dialog box to select the servers where you want to run the maintenance plan.

A multiserver environment containing one master server and one or more target servers must be configured to create a multiserver maintenance plan. For multiserver maintenance plans, the local server should be configured as a master server. In multiserver environments, this dialog box displays the **(local)** master server and all corresponding target servers. One [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job is created for the local server. It is enabled or disabled depending on whether you select the **(local)** server. If target servers are selected, a multiserver job is created and downloaded to each of the selected target servers. If no target servers are selected, the multiserver job is deleted.

## See also

- [Maintenance Plans](maintenance-plans.md)
- [Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md)
- [Make a Master Server](../../ssms/agent/make-a-master-server.md)
- [Make a Target Server](../../ssms/agent/make-a-target-server.md)
