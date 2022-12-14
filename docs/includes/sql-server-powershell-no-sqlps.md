---
title: Using NOSQLPS for SQL Server Agent with PowerShell
description: Message to explain to use the sqlserver powershell cmdlet instead of the sqlps cmdlet with SQL Server Agent
author: markingmyname
ms.author: maghan
ms.reviewer: drskwier
ms.topic: include
---

Starting with SQL Server 2019, you can disable SQLPS. On the first line of a job step of the type PowerShell you can add `#NOSQLPS`, which stops the SQL Agent from auto-loading the SQLPS module. Now your SQL Agent Job runs the version of PowerShell installed on the machine, and then you can use any other PowerShell module you like.

To use the [**SqlServer module**](https://www.powershellgallery.com/packages/Sqlserver/21.1.18235) in your SQL Agent Job step, you can place this code on the first two lines of your script.

```powershell
#NOSQLPS
Import-Module -Name SqlServer
```