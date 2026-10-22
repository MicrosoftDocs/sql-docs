---
title: "Administer Multiple Servers Using Central Management Servers"
description: Learn to administer multiple servers in SQL Server by designating Central Management Servers and creating server groups.
author: adbadram
ms.author: adbadram
ms.reviewer: wiassaf
ms.date: 01/28/2026
ms.service: sql
ms.subservice: supportability
ms.topic: concept-article
helpviewer_keywords:
  - "multiserver queries"
  - "central management server"
  - "multiserver administration [SQL Server]"
  - "central management servers"
  - "target configuration [SQL Server]"
  - "server configuration [SQL Server]"
---
# Administer multiple servers using Central Management Servers

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

You can administer multiple servers by designating Central Management Servers and creating server groups.

## What are a Central Management Server and server groups?

An instance of SQL Server that you designate as a Central Management Server (CMS) maintains server groups that contain the connection information for one or more instances. You can execute [!INCLUDE [tsql](../includes/tsql-md.md)] statements and Policy-Based Management policies at the same time against server groups. You can also view the log files on instances managed through a Central Management Server.

Think of a Central Management Server as a central repository containing a list of your managed servers.

You can also execute [!INCLUDE [tsql](../includes/tsql-md.md)] statements against local server groups in Registered Servers.

## Create Central Management Server and server groups

To create a Central Management Server and server groups, use the **Registered Servers** window in [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)]. The Central Management Server shouldn't be a member of a group that it maintains, to prevent accidental audit policy application that could interfere with the CMS itself. SSMS enforces this restriction by preventing a server of the same name as the Central Management Server from being added to a group.

For more information about creating Central Management Servers and server groups, see [Create a Central Management Server and Server Group (SQL Server Management Studio)](/ssms/register-servers/create-a-central-management-server-and-server-group).

## Related content

- [Administer Servers by Using Policy-Based Management](policy-based-management/administer-servers-by-using-policy-based-management.md)
- [Create a Central Management Server and Server Group](/ssms/register-servers/create-a-central-management-server-and-server-group)
