---
title: Complete the post-installation steps
titleSuffix: SQL Server Distributed Replay
description: After you install Distributed Replay, you must modify the Distributed Replay controller and client services accounts.
ms.service: sql
ms.subservice: distributed-replay
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray
ms.custom: seo-lt-2019
ms.date: 06/20/2022
monikerRange: ">= sql-server-2016 || >= sql-server-linux-2017"
---

# Complete the Post-Installation Steps

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

After you install Distributed Replay, you must modify the Distributed Replay controller and client services accounts.

## To complete the post-installation steps

1. **Create firewall rules**: On the controller and client computers, you must allow inbound traffic through the firewall for the corresponding service. Specify the firewall rules for the service executables, located in the installation folders.

    1. For the controller service, create a rule for **DReplayController.exe**, located in the installation folder. For example, the following command enables this rule, where `%InstallPath%` is the installation folder of the service:

         `netsh advfirewall firewall add rule name="allow dreplay controller" dir=in program="%InstallPath%\DReplayController\DReplayController.exe" action=allow`

    2. For the client service, on each client computer, create a rule for **DReplayClient.exe**, located in the installation folder. For example, the following command enables this rule, where `%InstallPath%` is the installation folder of the service:

         `netsh advfirewall firewall add rule name="allow dreplay client" dir=in program="%InstallPath%\DReplayClient\DReplayClient.exe" action=allow`

2. **Grant each client permissions on the target server**: After you have completed installing the client service on the client computers, you must manually add the client service accounts to the sysadmin role on the target instance of SQL Server.

## .NET Framework Security

You must have administrative permissions in order to install any of the Distributed Replay features. Only a SQL Server log in having sysadmin permissions can add the client service accounts to the sysadmin server role of the test server. For more information about Distributed Replay security considerations, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).
