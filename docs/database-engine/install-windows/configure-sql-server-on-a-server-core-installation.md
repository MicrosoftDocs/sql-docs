---
title: Configure Server Core Installation
description: This article covers details about configuring SQL Server on a Server Core installation, including troubleshooting tools.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/10/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
helpviewer_keywords:
  - "IsHadrEnabled server property"
  - "Server Core Installation [SQL Server]"
monikerRange: ">=sql-server-2017"
ms.custom: sfi-ropc-blocked
---
# Configure SQL Server on a Server Core installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article covers details about configuring [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a Server Core installation.

<a id="BKMK_ConfigureWindows"></a>

## Configure and manage Server Core on Windows Server

The section provides references to the articles that help configure and manage a Server Core installation.

Not all features of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)] are supported in Server Core mode. Some of these features can be installed on a client computer or a different server that isn't running Server Core, and connected to the Database Engine services installed on Server Core.

For more information about configuring and managing a Server Core installation remotely, see the following articles:

- [Install Server Core](/windows-server/get-started/getting-started-with-server-core)
- [Configure a Server Core installation of Windows Server with the Server Configuration tool (SConfig)](/windows-server/administration/server-core/server-core-sconfig)
- [Install Server Roles and Features on a Server Core Server Windows Server 2012 R2](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/jj574158(v=ws.11))
- [Managing a Server Core installation: Overview](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee441255(v=ws.10))
- [Administering a Server Core installation](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee441258(v=ws.10))

<a id="BKMK_InstallSQLUpdates"></a>

## Install SQL Server updates

This section provides information about installing updates for [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)] on a Windows Server Core machine. We recommend that customers evaluate and install latest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] updates in a timely manner to make sure that systems are up to date with the most recent security updates. For more information about installing [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)] on a Windows Server Core machine, see [Install SQL Server on Server Core](install-sql-server-on-server-core.md).

The following are the two scenarios for installing product updates:

- [Install updates for SQL Server during a new installation](#bkmk_NewInstall)
- [Install updates for SQL Server after installation](#bkmk_alreadyInstall)

<a id="bkmk_NewInstall"></a>

### Install updates for SQL Server during a new installation

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup supports only command prompt installations on Server Core operating system. For more information, see [Install and configure SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup integrates the latest product updates with the main product installation so that the main product and its applicable updates are installed at the same time.

After Setup finds the latest versions of the applicable updates, it downloads and integrates them with the current [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup process. Product Update can pull in a cumulative update, service pack, or service pack plus cumulative update.

Specify the UpdateEnabled, and UpdateSource parameters to include the latest product updates with the main product installation. Refer the following example to enable product updates during the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup:

```cmd
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<password>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="NT AUTHORITY\Network Service" /UpdateEnabled=True /UpdateSource="<SourcePath>" /IACCEPTSQLSERVERLICENSETERMS
```

[!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

<a id="bkmk_alreadyInstall"></a>

### Install updates for SQL Server after installation

On an installed instance of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)], we recommend that you apply the latest security updates and critical updates including General Distribution Releases (GDRs), and Service Packs (SPs). Individual Cumulative updates and security updates should be adopted on a case-by-case, "as-needed" basis. Evaluate the update. If it's needed, then apply it.

Apply an update at a command prompt, replacing <package_name> with the name of your update package:

- Update a single instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and all shared components. You can specify the instance either by using the InstanceName parameter or the InstanceID parameter.

  ```cmd
  <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /InstanceName=MyInstance
  ```

- Update [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] shared components only:

  ```cmd
  <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch
  ```

- Update all instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the computer and all shared components:

  ```cmd
  <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /AllInstances
  ```

<a id="BKMK_StartStopServices"></a>

## Start and stop SQL Server service

The [sqlservr Application](../../tools/sqlservr-application.md) application starts, stops, pauses, and continues an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a command prompt.

You can also use Net services to start and stop the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services.

<a id="BKMK_EnableAlwaysON"></a>

## Enable Always On availability groups

Being enabled for Always On Availability Groups is a prerequisite for a server instance to use availability groups as a high availability and disaster recovery solution. For more information about managing the Always On availability groups, see [Enable or disable Always On availability group feature](../availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).

### Use SQL Server Configuration Manager remotely

These steps are meant to be performed on a PC running the client edition of Windows, or Windows Server that has the Server Graphical Shell installed.

1. Open **Computer Management**. To open **Computer Management**, select **Start**, type `compmgmt.msc`, and then select **OK**.

1. In the console tree, right-click **Computer Management**, and then select **Connect to another computer...**.

1. In the **Select Computer** dialog box, type the name of the Server Core machine that you want to manage, or select **Browse** to find it, and then select **OK**.

1. In the console tree, under **Computer Management** of the Server Core machine, select **Services and Applications**.

1. Double-click **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager**.

1. In **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager**, select **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Services**, right-click **[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]** (\<instance name>), where \<instance name> is the name of a local server instance for which you want to enable Always On Availability Groups, and select **Properties**.

1. Select the **Always On High Availability** tab.

1. Verify that Windows failover cluster name field contains the name of the local failover cluster node. If this field is blank, this server instance currently doesn't support Always On Availability Groups. Either the local computer isn't a cluster node, the WSFC cluster is shut down, or this edition of [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)] doesn't support Always On Availability Groups.

1. Select the **Enable Always On Availability Groups** check box, and select **OK**.

1. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager saves your change. Then, you must manually restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. This enables you to choose a restart time that is best for your business requirements. When the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service restarts, availability groups are enabled, and the `IsHadrEnabled` server property is set to `1`.

> [!NOTE]  
>  
> - You must have the appropriate user rights or you must have been delegated the appropriate authority on the target computer to connect to that computer.  
> - The name of the computer that you're managing appears in parentheses next to Computer Management in the console tree.

### Use PowerShell cmdlets to enable availability groups

The PowerShell cmdlet `Enable-SqlAlwaysOn` is used to enable Always On Availability Group on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If the Always On Availability Groups feature is enabled while the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service is running, the Database Engine service must be restarted for the change to complete. Unless you specify the `-Force` parameter, the cmdlet prompts you to ask whether you wish to restart the service; if canceled, no operation occurs.

You must have Administrator permissions to execute this cmdlet.

You can use one of the following syntaxes to enable Always On Availability Groups for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

```powershell
Enable-SqlAlwaysOn [-Path <string>] [-Credential <PSCredential>] [-Force] [-NoServiceRestart] [-Confirm] [-WhatIf] [<Common Parameters>]
```

```powershell
Enable-SqlAlwaysOn -InputObject <Server> [-Credential <PSCredential>] [-Force] [-NoServiceRestart] [-Confirm] [-WhatIf] [<Common Parameters>]
```

```powershell
Enable-SqlAlwaysOn [-ServerInstance <string>] [-Credential <PSCredential>] [-Force] [-NoServiceRestart] [-Confirm] [-WhatIf] [<Common Parameters>]
```

The following PowerShell command enables Always On Availability Groups on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Machine\Instance):

```powershell
Enable-SqlAlwaysOn -Path SQLSERVER:\SQL\Machine\Instance
```

<a id="BKMK_ConfigureRemoteAccess"></a>

## Configure remote access of SQL Server running on Server Core

Perform the following actions to configure remote access of a [!INCLUDE [ssNoVersion](../../includes/ssNoVersion-md.md)] instance that is running on Windows Server Core.

### Enable remote connections on the SQL Server instance

To enable remote connections, use **sqlcmd** locally and execute the following statements against the Server Core instance:

```sql
EXEC sys.sp_configure N'remote access', N'1';
GO

RECONFIGURE WITH OVERRIDE
GO
```

### Enable and start the SQL Server Browser service

By default, the Browser service is disabled. If it's disabled on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on Server Core, run the following command from the command prompt to enable it:

`sc config SQLBROWSER start=auto`

After the service is enabled, run the following command from the command prompt to start the service:

`net start SQLBROWSER`

### Create exceptions in Windows Firewall

To create exceptions for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] access in Windows Firewall, follow the steps specified in [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

### Enable TCP/IP on the SQL Server instance

The TCP/IP protocol can be enabled through Windows PowerShell for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core. Follow these steps:

1. On the computer that is running Windows Server Core, launch **Task Manager**.

1. On the **Applications** tab, select **New Task**.

1. In the **Create New Task** dialog box, type **sqlps.exe** in the **Open** field and then select **OK**. This opens the **Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Powershell** window.

1. In the **Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Powershell** window, run the following script to enable the TCP/IP protocol:

```powershell
$smo = 'Microsoft.SqlServer.Management.Smo.'
$wmi = new-object ($smo + 'Wmi.ManagedComputer')
# Enable the TCP protocol on the default instance.  If the instance is named, replace MSSQLSERVER with the instance name in the following line.
$uri = "ManagedComputer[@Name='" + (get-item env:\computername).Value + "']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"
$Tcp = $wmi.GetSmoObject($uri)
$Tcp.IsEnabled = $true
$Tcp.Alter()
$Tcp
```

<a id="BKMK_Profiler"></a>

## SQL Server Profiler

On a remote machine, start [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and select **New Trace** from the **File** menu. The application displays a **Connect to Server** dialog box, where you can specify the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance residing on the Server Core machine, to which you want to connect. For more information, see [Run SQL Server Profiler](../../tools/sql-server-profiler/start-sql-server-profiler.md).

For more information on the permissions required to run [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], see [Permissions required to run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md).

For more information about [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], see [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md).

<a id="BKMK_Auditing"></a>

## SQL Server auditing

You can use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)] remotely to define an audit. After the audit is created and enabled, the target will receive entries. For more information about creating and managing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] audits, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

<a id="BKMK_CMD"></a>

## Command-line utilities

You can use the following command prompt utilities that enable you to script [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] operations on a Server Core machine. The following table contains a list of command prompt utilities that ship with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for Server Core:

| Utility | Description | Installed in |
| --- | --- | --- |
| [bcp](../../tools/bcp-utility.md) | Used to copy data between an instance of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a data file in a user-specified format. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]Tools\Binn |
| [dtexec](../../integration-services/packages/dtexec-utility.md) | Used to configure and execute an [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] package. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]DTS\Binn |
| [dtutil](../../integration-services/dtutil-utility.md) | Used to manage SSIS packages. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]DTS\Binn |
| [osql](../../tools/osql-utility.md) | Allows you to enter [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, system procedures, and script files at the command prompt. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]Tools\Binn |
| [sqlagent](../../tools/sqlagent-application.md) | Used to start [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent from a command prompt. | \<drive>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\\<*instance_name*>\MSSQL\Binn |
| [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md) | Allows you to enter [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, system procedures, and script files at the command prompt. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]Tools\Binn |
| [SQLdiag](../../tools/sqldiag-utility.md) | Used to collect diagnostic information for [!INCLUDE [msCoName](../../includes/msconame-md.md)] Customer Service and Support. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]Tools\Binn |
| [sqlmaint](../../tools/sqlmaint-utility.md) | Used to execute database maintenance plans created in previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. | \<drive>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.MSSQLSERVER\MSSQL\Binn |
| [sqlps](../../tools/sqlps-utility.md) | Used to run PowerShell commands and scripts. Loads and registers the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell provider and cmdlets. | [!INCLUDE [ssinstallpath-md](../../includes/ssinstallpath-md.md)]Tools\Binn |
| [sqlservr](../../tools/sqlservr-application.md) | Used to start and stop an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] from the command prompt for troubleshooting. | \<drive>:\Program Files\\[!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]\MSSQL14.MSSQLSERVER\MSSQL\Binn |

<a id="BKMK_troubleshoot"></a>

## Use troubleshooting tools

You can use [SQLdiag](../../tools/sqldiag-utility.md) to collect logs and data files from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and other types of servers. Use this information to monitor your servers over time or troubleshoot specific problems with your servers. SQLdiag is intended to expedite and simplify diagnostic information gathering for Microsoft Customer Support Services.

You can launch the utility on the administrator command prompt on the Server Core, using the syntax specified in [SQLdiag utility](../../tools/sqldiag-utility.md).

## Related content

- [Install SQL Server on Server Core](install-sql-server-on-server-core.md)
- [Install SQL Server with SysPrep](install-sql-server-using-sysprep.md)
- [Install SQL Server Database Engine](install-sql-server-database-engine.md)
