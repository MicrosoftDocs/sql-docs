---
title: Firewall configuration
description: How to configure the firewall for outbound connections from SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 10/17/2018  
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Firewall configuration for SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article lists firewall configuration considerations that the administrator or architect should bear in mind when using [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) .

## Default firewall rules

By default, the SQL Server Setup disables outbound connections by creating firewall rules.

In SQL Server 2016 and 2017, these rules are based on local user accounts, where Setup created one outbound rule for **SQLRUserGroup** that denied network access to its members (each worker account was listed as a local principal subject to the rule. For more information about SQLRUserGroup, see [Security overview for the extensibility framework in SQL Server Machine Learning Services](../../machine-learning/concepts/security.md#sqlrusergroup).

In SQL Server 2019, as part of the move to AppContainers, there are new firewall rules based on AppContainer SIDs: one for each of the 20 AppContainers created by SQL Server Setup. Naming conventions for the firewall rule name are **Block network access for AppContainer-00 in SQL Server instance MSSQLSERVER**, where 00 is the number of the AppContainer (00-20 by default), and MSSQLSERVER is the name of the SQL Server instance.

> [!Note]
> If network calls are required, you can disable the outbound rules in Windows Firewall.

## Restrict network access

In a default installation, a Windows firewall rule is used to block all outbound network access from external runtime processes. Firewall rules should be created to prevent the external runtime processes from downloading packages or from making other network calls that could potentially be malicious.

If you are using a different firewall program, you can also create rules to block outbound network connection for external runtimes, by setting rules for the local user accounts or for the group represented by the user account pool.

We strongly recommend that you turn on Windows Firewall (or another firewall of your choice) to prevent unrestricted network access by the R or Python runtimes.

## Next steps

[Configure Windows firewall for in-bound connections](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)
