---
title: "Install PolyBase on Windows"
description: Learn to install PolyBase as a single node or PolyBase scale-out group. You can use an installation wizard or a command prompt. Finally, enable PolyBase.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/01/2022
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
ms.custom: intro-installation
helpviewer_keywords:
  - "PolyBase, installation"
monikerRange: ">= sql-server-2016"
---
# Install PolyBase on Windows

[!INCLUDE [SQL Server Windows Only ](../../includes/applies-to-version/sql-windows-only.md)]

To install a trial version of SQL Server, go to [SQL Server evaluations](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016).

## Prerequisites

- 64-bit SQL Server Evaluation edition.

- Microsoft .NET Framework 4.5.

- Minimum memory: 4 GB.

- Minimum hard-disk space: 2 GB.

- Recommended: Minimum of 16-GB RAM.

- PolyBase services require SQL Server service to have TCP/IP network protocol enabled to function correctly. TCP/IP is enabled by default on all editions of SQL Server except for the Developer and Express SQL Server editions. For PolyBase to function correctly on the Developer and Express editions, you must enable TCP/IP connectivity. See [Enable or disable a server network protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md). Additionally, if TCP/IP Protocol configuration setting **Listen All** is set to **No**, you must still have an entry for the correct listener port in either **TCP Dynamic Ports** or **TCP Ports** under **IPAll** in TCP/IP Properties. This is required due to the way PolyBase services resolve the listener port of the SQL Server Engine.

- PolyBase services require Shared Memory protocol to be enabled to function properly.

- PolyBase can be installed on only one SQL Server instance per machine.

- You cannot add features to a failover cluster instance after creation. For example, you cannot add the PolyBase feature to an existing failover cluster instance.

## Single node or PolyBase scale-out group

Before you install PolyBase on your SQL Server instances, decide whether you want a single node installation or a [PolyBase scale-out group](../../relational-databases/polybase/polybase-scale-out-groups.md).

Scale-out group functionality is retired and removed from the product in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. PolyBase data virtualization will continue to be fully supported as a scale-up feature in SQL Server. For more information, see [Big data options on the Microsoft SQL Server platform](../../big-data-cluster/big-data-options.md).

For the PolyBase service account, choose:
- the default virtual service account (VSA) for stand-alone installations of PolyBase.
- a domain account, with a group managed service account (gMSA) preferred, for installations in a PolyBase scale-out group. For more information, see [Group Managed Service Accounts Overview](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview).

For a PolyBase scale-out group in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] - [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], make sure that:

- All the machines are on the same domain.
- You use the same domain service account and password during PolyBase installation.
- Your SQL Server instances can communicate with one another over the network.
- The SQL Server instances are all the same version of SQL Server.

After installation of PolyBase to either standalone or in a scale-out group, you cannot change to a scale-out group or standalone service. If you need to change an existing installation of PolyBase to a standalone instance or a scale-out group, uninstall and reinstall the PolyBase feature.

## Use the installation wizard

1. Run the SQL Server setup.exe.

1. Select **Installation**, and then select **New standalone SQL Server installation or add features**.

1. On the Feature Selection page, select **PolyBase Query Service for External Data**.

   > [!NOTE]  
   > Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], PolyBase includes an additional option **Java connector for HDFS data sources**. See [SQL Server preview features](https://cloudblogs.microsoft.com/sqlserver/2019/04/24/sql-server-2019-community-technology-preview-2-5-is-now-available/) for more information about this feature.

1. On the Server Configuration page, configure the **SQL Server PolyBase Engine Service** and **SQL Server PolyBase Data Movement Service** to run under the same domain account.

   In a PolyBase scale-out group, the PolyBase Engine and PolyBase Data Movement service on all nodes must run under the same domain account. See [PolyBase scale-out groups](#single-node-or-polybase-scale-out-group).

1. On the PolyBase Configuration page, select one of the two options. For more information, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).

   - Use the SQL Server instance as a standalone PolyBase-enabled instance.

     Choose this option to use the SQL Server instance as a standalone head node.

   - Use the SQL Server instance as part of a PolyBase scale-out group. This option opens the firewall to allow incoming connections. Connections are allowed for the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service, and the SQL browser. The firewall also allows incoming connections from other nodes in a PolyBase scale-out group.

     This option also enables Microsoft Distributed Transaction Coordinator (MSDTC) firewall connections and modifies MSDTC registry settings.

1. On the **PolyBase Configuration** page, specify a port range with at least six ports. SQL Setup allocates the first six available ports from the range.

   > [!IMPORTANT]  
   > Only in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], after installation, you must [enable the PolyBase feature](#enable).

## <a id="installing"></a> Use a command prompt

Use the values in this table to create installation scripts. The SQL Server PolyBase Engine and SQL Server PolyBase Data Movement service must run under the same account. In a PolyBase scale-out group, PolyBase services on all nodes must run under the same domain account.

<!--SQL Server 2016/2017-->
::: moniker range="= sql-server-2016 || = sql-server-2017"

|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBase|Selects PolyBase feature.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase Engine: Automatic (default), Disabled, and Manual.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for the data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default), Disabled, and Manual.|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies whether the SQL Server instance is used as a part of a PolyBase scale-out computational group. <br />Supported values: True, False.|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least six ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|

::: moniker-end
<!--SQL Server 2019-->
::: moniker range=">= sql-server-ver15 "

|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBaseCore, PolyBaseJava, PolyBase | PolyBaseCore installs support for all PolyBase features except Hadoop connectivity. PolyBaseJava enables Hadoop connectivity. PolyBase installs both. |  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase Engine: Automatic (default), Disabled, and Manual.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default), Disabled, and Manual.|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies whether the SQL Server instance is used as a part of a PolyBase scale-out computational group. <br />Supported values: True, False.|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least six ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|

::: moniker-end

After installation, you must [enable the PolyBase feature](#enable).


**Example**

This example shows a sample setup script.

```cmd

Setup.exe /Q /ACTION=INSTALL /IACCEPTSQLSERVERLICENSETERMS /FEATURES=SQLEngine,PolyBase  
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="\<fabric-domain>\Administrator"  
/INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /PBSCALEOUT=TRUE  
/PBPORTRANGE=16450-16460 /SECURITYMODE=SQL /SAPWD="<StrongPassword>"  
/PBENGSVCACCOUNT="<DomainName>\<UserName>" /PBENGSVCPASSWORD="<StrongPassword>"  
/PBDMSSVCACCOUNT="<DomainName>\<UserName>" /PBDMSSVCPASSWORD="<StrongPassword>"

```

[!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"

## <a id="enable"></a> Enable PolyBase

After installation, PolyBase must be enabled to access its features. Use the following Transact-SQL command. SQL 2019 instances deployed during Big Data Cluster installation have this setting enabled by default. The `polybase enabled` configuration option was introduced in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)].

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE;
```

::: moniker-end

## Post-installation notes

PolyBase installs three user databases: `DWConfiguration`, `DWDiagnostics`, and `DWQueue`. These databases are for PolyBase use. Don't alter or delete them.

### Avoid split version

Adding PolyBase to an existing installation of SQL Server on Windows will install the feature at the version level of the installation media, which may be behind the version level other features of SQL Server. This may result in unexpected behavior or errors. Always follow up installing the PolyBase feature by bringing the new feature up to the same version level. Install service packs (SPs), cumulative updates (CUs), and/or general distribution releases (GDRs) as needed. To determine the version of PolyBase, see [Determine the version, edition, and update level of SQL Server and its components](/troubleshoot/sql/general/determine-version-edition-update-level#polybase).

This split-version scenario is not possible when adding the feature to SQL Server on Linux.

### <a id="confirminstall"></a> How to confirm installation

Run the following command. If PolyBase is installed, the return is `1`. Otherwise, it's `0`.

```sql
SELECT SERVERPROPERTY ('IsPolyBaseInstalled') AS IsPolyBaseInstalled;
```

### Firewall rules

SQL Server PolyBase setup creates the following firewall rules on the machine:

- SQL Server PolyBase - Database Engine - \<SQLServerInstanceName> (TCP-In)

- SQL Server PolyBase - PolyBase Services - \<SQLServerInstanceName> (TCP-In)

- SQL Server PolyBase - SQL Browser - (UDP-In)

At installation, if you use the SQL Server instance as part of a PolyBase scale-out group, these rules are enabled. The firewall opens to allow incoming connections. They're allowed for the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service, and the SQL browser. If the firewall service on the machine isn't running during installation, SQL Server setup fails to enable these rules. In that case, start the firewall service on the machine and enable these rules post-installation.

#### Enable the firewall rules

1. Open **Control Panel**.

1. Select **System and Security**, and select **Windows Firewall**.

1. Select **Advanced Settings**, and select **Inbound rules**.

1. Right-click the disabled rule, and then select **Enable rule**.

### PolyBase service accounts

To change the service accounts for the PolyBase Engine and PolyBase Data Movement service, uninstall and reinstall the PolyBase feature. If the password for the service account was changed in Active Directory, you can change the service account password with Windows Services Console (services.msc).

## Next steps

See [PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md).

- [Big data options on the Microsoft SQL Server platform](../../big-data-cluster/big-data-options.md)