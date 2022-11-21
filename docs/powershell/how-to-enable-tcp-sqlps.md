---
title: How to enable the TCP protocol using SQLPS
description: Learn how to enable TCP protocols using SQLPS
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot
ms.custom: ""
ms.date: 08/06/2020
---

# How to enable the TCP protocol

## How to enable the TCP protocol when connected to the console with SQLPS.

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

1. Open a command prompt and type:

    ```console
    C:\> SQLPS.EXE
    ```

    > [!TIP]
    > If SQLPS is not found, you may need to open a new command prompt or just log-off and log back on.

2. At the PowerShell command prompt, type:

    ```powershell
    # Instantiate a ManagedComputer object which exposes primitives to control the
    # installation of SQL Server on this machine.

    $wmi = New-Object 'Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer' localhost

    # Enable the TCP protocol on the default instance. If the instance is named, 
    # replace MSSQLSERVER with the instance name in the following line.

    $tcp = $wmi.ServerInstances['MSSQLSERVER'].ServerProtocols['Tcp']
    $tcp.IsEnabled = $true  
    $tcp.Alter()  

    # You need to restart SQL Server for the change to persist
    # -Force takes care of any dependent services, like SQL Agent.
    # Note: if the instance is named, replace MSSQLSERVER with MSSQL$ followed by
    # the name of the instance (e.g. MSSQL$MYINSTANCE)

    Restart-Service -Name MSSQLSERVER -Force
    ```

## How to enable the TCP protocol when connected to the console not using SQLPS.

1. Open a command prompt and type:

    ```console
    C:\> PowerShell.exe
    ```

2. At the PowerShell command prompt, type:

    ```powershell
    # Get access to SqlWmiManagement DLL on the machine with SQL
    # we are on, which is where SQL Server was installed.
    # Note: this is installed in the GAC by SQL Server Setup.

    [System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SqlWmiManagement')

    # Instantiate a ManagedComputer object which exposes primitives to control the
    # installation of SQL Server on this machine.

    $wmi = New-Object 'Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer' localhost

    # Enable the TCP protocol on the default instance. If the instance is named, 
    # replace MSSQLSERVER with the instance name in the following line.

    $tcp = $wmi.ServerInstances['MSSQLSERVER'].ServerProtocols['Tcp']
    $tcp.IsEnabled = $true  
    $tcp.Alter()  

    # You need to restart SQL Server for the change to persist
    # -Force takes care of any dependent services, like SQL Agent.
    # Note: if the instance is named, replace MSSQLSERVER with MSSQL$ followed by
    # the name of the instance (e.g. MSSQL$MYINSTANCE)

    Restart-Service -Name MSSQLSERVER -Force
    ```

## Next steps

- [Install the SQL Server PowerShell module](download-sql-server-ps-module.md)
- [SQL Server PowerShell](sql-server-powershell.md)
- [Enable or Disable a Server Network Protocol](../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
- [Configure SQL Server on a Server Core Installation](../database-engine/install-windows/configure-sql-server-on-a-server-core-installation.md)
