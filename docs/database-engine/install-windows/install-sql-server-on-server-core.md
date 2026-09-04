---
title: Install SQL Server on Server Core
description: You can install SQL Server on a Server Core installation. The Server Core installation option provides a minimal environment for running specific server roles.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/14/2026
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
  - sfi-ropc-blocked
monikerRange: ">=sql-server-2017"
---
# Install SQL Server on Server Core

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

You can install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a Server Core installation.

The Server Core installation option provides a minimal environment for running specific server roles. This environment helps reduce maintenance and management requirements and lowers the attack surface for those server roles.

For a list of currently supported operating systems, see:

- [Hardware and software requirements for SQL Server 2025](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025.md)
- [Hardware and software requirements for SQL Server 2022](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Hardware and software requirements for SQL Server 2019](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md)
- [Hardware and software requirements for SQL Server 2017](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2017.md)

## Prerequisites

| Requirement | How to install |
| --- | --- |
| [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.7.2 <sup>1</sup> | For all editions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] except [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)], Setup requires the [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.7.2 Server Core Profile. SQL Server Setup automatically installs this version if it's not already installed. Installation requires a restart. To avoid a restart, install [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] before running setup. |
| Windows Installer 4.5 | Shipped with Server Core installation. |
| Windows PowerShell | Shipped with Server Core installation. |
| Java Runtime | To use PolyBase, you need to install the appropriate Java Runtime. For more information, see [Install PolyBase on Windows](../../relational-databases/polybase/polybase-installation.md). |

<sup>1</sup> [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] requires [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6.1. You can upgrade [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] after installing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

<a id="BK_SupportedFeatures"></a>

## Supported features

Use the following table to find which features are supported in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on a Server Core installation.

| Feature | Supported |
| --- | --- |
| [!INCLUDE [ssDE](../../includes/ssde-md.md)] Services | Yes |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Replication | Yes |
| Full Text Search | Yes |
| [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] | Yes |
| [!INCLUDE [rsql_productname_md](../../includes/rsql-productname-md.md)] | Yes |
| [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] | No |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Data Tools (SSDT) | No |
| Client Tools Connectivity | Yes |
| Integration Services Server | Yes |
| Client Tools Backward Compatibility | No |
| Client Tools SDK | No |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Books Online | No |
| Management Tools - Basic | Remote Only <sup>1</sup> |
| Management Tools - Complete | Remote Only <sup>1</sup> |
| Distributed Replay Controller | No |
| Distributed Replay Client | Remote Only <sup>1</sup> |
| SQL Client Connectivity SDK | Yes |
| Microsoft Sync Framework | Yes <sup>2</sup> |
| [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] | No |
| [!INCLUDE [ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] | No |

<sup>1</sup> Installation of these features on Server Core isn't supported. You can install these components on a different server that isn't Server Core and connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] services installed on Server Core.

<sup>2</sup> Microsoft Sync Framework isn't included in the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] installation package. You can download the appropriate version of Sync Framework from this [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=17616) page and install it on a computer that is running Server Core.

## Supported scenarios

The following table shows the supported scenario matrix for installing [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on a Server Core.

| Installation | Valid target |
| --- | --- |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] editions | All [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] 64-bit editions |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] language | All languages |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] language on OS language/locale (combination) | ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on JPN (Japanese) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on GER (German) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on CHS (Chinese-China) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on ARA (Arabic (SA)) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on THA (Thai) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on TRK (Turkish) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on pt-PT (Portuguese Portugal) Windows<br /><br />ENG [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on ENG (English) Windows |
| Windows edition | Windows Server 2025 Datacenter<br /><br />Windows Server 2025 Datacenter: Azure Edition<br /><br />Windows Server 2025 Standard<br /><br />Windows Server 2022 Datacenter<br /><br />Windows Server 2022 Datacenter: Azure Edition<br /><br />Windows Server 2022 Standard<br /><br />Windows Server 2019 Datacenter<br /><br />Windows Server 2019 Standard<br /><br />Windows Server 2016 Datacenter <sup>1</sup><br /><br />Windows Server 2016 Standard <sup>1</sup> |

<sup>1</sup> [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] doesn't support Windows Server 2016.

## Upgrade

On Server Core installations, upgrades between supported versions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] follow the same paths as upgrades on the full Windows Server operating system.

For supported upgrade paths, see:

- [Supported version and edition upgrades (SQL Server 2025)](supported-version-and-edition-upgrades-2025.md)
- [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md)
- [Supported version and edition upgrades (SQL Server 2019)](supported-version-and-edition-upgrades-2019.md)
- [Supported version and edition upgrades (SQL Server 2017)](supported-version-and-edition-upgrades-2017.md)

## Install

[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] doesn't support setup by using the installation wizard on the Server Core operating system. When installing on Server Core, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup supports full quiet mode by using the `/Q` parameter, or Quiet Simple mode by using the `/QS` parameter. For more information, see [Install, configure, or uninstall SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md).

Regardless of the installation method, you must confirm acceptance of the software license terms as an individual or on behalf of an entity, unless your use of the software is governed by a separate agreement such as a [!INCLUDE [msCoName](../../includes/msconame-md.md)] volume licensing agreement or a third-party agreement with an ISV or OEM.

Review and accept the license terms in the Setup user interface. Unattended installations (using the `/Q` or `/QS` parameters) must include the `/IACCEPTSQLSERVERLICENSETERMS` parameter. You can review the license terms separately at [Microsoft Software License Terms](https://go.microsoft.com/fwlink/?LinkId=148209).

[!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

> [!NOTE]  
> Depending on how you received the software (for example, through [!INCLUDE [msCoName](../../includes/msconame-md.md)] volume licensing), your use of the software might be subject to additional terms and conditions.

To install specific features, use the `/FEATURES` parameter and specify the parent feature or feature values. For more information about feature parameters and their use, see the following sections.

### Feature parameters

| Feature parameter | Description |
| --- | --- |
| `SQLENGINE` | Installs only the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. |
| `REPLICATION` | Installs the Replication component along with [!INCLUDE [ssDE](../../includes/ssde-md.md)]. |
| `FULLTEXT` | Installs the FullText component along with [!INCLUDE [ssDE](../../includes/ssde-md.md)]. |
| `AS` | Installs all [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] components. |
| `IS` | Installs all [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] components. |
| `CONN` | Installs the connectivity components. |
| `ADVANCEDANALYTICS` | Installs R Services, requires the database engine. Unattended installations require `/IACCEPTROPENLICENSETERMS` parameter. |

See the following examples of the usage of feature parameters:

| Parameter and values | Description |
| --- | --- |
| `/FEATURES=SQLEngine` | Installs only the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. |
| `/FEATURES=SQLEngine,FullText` | Installs the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and full-text. |
| `/FEATURES=SQLEngine,Conn` | Installs the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and the connectivity components. |
| `/FEATURES=SQLEngine,AS,IS,Conn` | Installs the [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)], and the connectivity components. |
| `/FEATURES=SQLENGINE,ADVANCEDANALYTICS /IACCEPTROPENLICENSETERMS` | Installs the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [rsql_productname_md](../../includes/rsql-productname-md.md)]. |

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

### Installation options

The Setup supports the following installation options while installing [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on a Server Core operating system:

1. **Installation from command line**

   To install specific features, use the command prompt installation option. Use the `/FEATURES` parameter and specify the parent feature or feature values. The following example shows how to use the parameters from the command line:

   ```console
   setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,Replication /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<password>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="NT AUTHORITY\Network Service" /TCPENABLED=1 /IACCEPTSQLSERVERLICENSETERMS
   ```

1. **Installation using configuration file**

   Setup supports the use of the configuration file only through the command prompt. The configuration file is a text file with the basic structure of a parameter (name/value pair) and a descriptive comment. The configuration file specified at the command prompt should have an `.ini` file name extension. See the following examples of `ConfigurationFile.ini`:

   - Installing [!INCLUDE [ssDE](../../includes/ssde-md.md)]:

     The following example shows how to install a new stand-alone instance that includes the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]:

     ```ini
     ; SQL Server Configuration File
     [OPTIONS]

     ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.
     ACTION="Install"

     ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.
     FEATURES=SQLENGINE

     ; Specify a default or named instance. MSSQLSERVER is the default instance for non-Express editions and SQLExpress for Express editions. This parameter is   required when installing the ssNoVersion Database Engine, and Analysis Services (AS).
     INSTANCENAME="MSSQLSERVER"

     ; Specify the Instance ID for the ssNoVersion features you have specified. ssNoVersion directory structure, registry structure, and service names will   incorporate the instance ID of the ssNoVersion instance.
     INSTANCEID="MSSQLSERVER"

     ; Account for ssNoVersion service: Domain\User or system account.
     SQLSVCACCOUNT="NT Service\MSSQLSERVER"

     ; Windows account(s) to provision as ssNoVersion system administrators.
     SQLSYSADMINACCOUNTS="\<DomainName\UserName>"

     ; Accept the License agreement to continue with Installation
     IAcceptSQLServerLicenseTerms="True"
     ```

   - Installing connectivity components. The following example shows how to install the connectivity components:

     ```ini
     ; SQL Server Configuration File
     [OPTIONS]

     ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.
     ACTION="Install"

     ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.
     FEATURES=Conn

     ; Specifies acceptance of License Terms
     IAcceptSQLServerLicenseTerms="True
     ```

   - Installing all supported features:

     The following example shows how to install all supported features of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on Server Core:

     ```ini
     ; SQL Server Configuration File
     [OPTIONS]
     ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.
     ACTION="Install"

     ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.
     FEATURES=SQLENGINE,FullText,Replication,AS,IS,Conn

     ; Specify a default or named instance. MSSQLSERVER is the default instance for non-Express editions and SQLExpress for Express editions. This parameter is  required when installing the ssNoVersion Database Engine (SQL), or Analysis Services (AS).
     INSTANCENAME="MSSQLSERVER"

     ; Specify the Instance ID for the ssNoVersion features you have specified. ssNoVersion directory structure, registry structure, and service names will  incorporate the instance ID of the ssNoVersion instance.
     INSTANCEID="MSSQLSERVER"

     ; Account for ssNoVersion service: Domain\User or system account.
     SQLSVCACCOUNT="NT Service\MSSQLSERVER"

     ; Windows account(s) to provision as ssNoVersion system administrators.
     SQLSYSADMINACCOUNTS="\<DomainName\UserName>"

     ; The name of the account that the Analysis Services service runs under.
     ASSVCACCOUNT= "NT Service\MSSQLServerOLAPService"

     ; Specifies the list of administrator accounts that need to be provisioned.
     ASSYSADMINACCOUNTS="\<DomainName\UserName>"

     ; Specifies the server mode of the Analysis Services instance. Valid values are MULTIDIMENSIONAL, POWERPIVOT or TABULAR. ASSERVERMODE is case-sensitive.  All values must be expressed in upper case.
     ASSERVERMODE="MULTIDIMENSIONAL"

     ; Optional value, which specifies the state of the TCP protocol for the ssNoVersion service. Supported values are: 0 to disable the TCP protocol, and 1 to  enable the TCP protocol.
     TCPENABLED=1

     ; Specifies acceptance of License Terms
     IAcceptSQLServerLicenseTerms="True"
     ```

   The following example shows how you can launch Setup using a custom or default configuration file:

   - Launch setup using a custom configuration file:

     To specify the configuration file at the command prompt:

     ```console
     setup.exe /QS /ConfigurationFile=MyConfigurationFile.INI
     ```

   To specify passwords at the command prompt instead of in the configuration file:

   ```console
   setup.exe /QS /SQLSVCPASSWORD="************" /ASSVCPASSWORD="************"  /ConfigurationFile=MyConfigurationFile.INI
   ```

   - Launch setup using `DefaultSetup.ini`:

     If you have the `DefaultSetup.ini` file in the \x86 and \x64 folders at the root level of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] source media, open the `DefaultSetup.ini` file, and then add the *Features* parameter to the file.

     If the `DefaultSetup.ini` file doesn't exist, you can create it and copy it to the \x86 and \x64 folders at the root level of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] source media.

## Configure remote access for SQL Server on Server Core

To configure remote access for a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] instance running on Server Core, complete the following steps.

### Enable remote connections on the instance of SQL Server

To enable remote connections, use SQLCMD.exe locally and execute the following statements against the Server Core instance:

```sql
EXECUTE sys.sp_configure N'remote access', N'1';
GO
RECONFIGURE WITH OVERRIDE;
GO
```

### Enable and start the SQL Server Browser service

By default, the Browser service is disabled. If it's disabled on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on Server Core, run the following command from the command prompt to enable it:

```powershell
Set-Service sqlbrowser -StartupType Auto
```

After you enable it, run the following command from the command prompt to start the service:

```powershell
Start-Service sqlbrowser
```

### Create exceptions in Windows Firewall

To create exceptions for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] access in Windows Firewall, follow the steps specified in [Configure the Windows Firewall to allow SQL Server access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

### Enable TCP/IP on the instance of SQL Server

You can enable the TCP/IP protocol through Windows PowerShell for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core. Follow these steps:

1. In PowerShell, run: `Import-Module SQLPS`.

1. In the **Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell** window, run the following script to enable the TCP/IP protocol:

   ```powershell
   $smo = 'Microsoft.SqlServer.Management.Smo.'
   $wmi = new-object ($smo + 'Wmi.ManagedComputer')
   # Enable the TCP protocol on the default instance. If the instance is named, replace MSSQLSERVER with the instance name in the following line.
   $uri = "ManagedComputer[@Name='" + (get-item env:\computername).Value + "']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"
   $Tcp = $wmi.GetSmoObject($uri)
   $Tcp.IsEnabled = $true
   $Tcp.Alter()
   $Tcp
   ```

## Uninstall

After you sign in to a computer that is running Server Core, you have a limited desktop environment with an Administrator command prompt. Use this command prompt to launch the uninstall of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. To uninstall an instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], launch the uninstallation from the command prompt in full quiet mode by using the `/Q` parameter, or Quiet Simple mode by using the `/QS` parameter. The `/QS` parameter shows progress through the UI, but doesn't accept any input. `/Q` runs in a quiet mode without any user interface.

To uninstall an existing instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

```console
setup.exe /Q /Action=Uninstall /FEATURES=SQLEngine,AS,IS /INSTANCENAME=MSSQLSERVER
```

To remove a named instance, specify the name of the instance instead of `MSSQLSERVER` in the preceding example.

## Start a new command prompt

If you accidentally close the command prompt, you can start a new command prompt by following these steps:

1. Press <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Esc</kbd> to display Task Manager.
1. On the **Applications** tab, select **New Task**.
1. In the **Create New Task** dialog box, type **cmd** in the **Open** field and then select **OK**.

## Related content

- [Install SQL Server using a configuration file](install-sql-server-using-a-configuration-file.md)
- [Install, configure, or uninstall SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md)
- [Install Server Core](/windows-server/get-started/getting-started-with-server-core)
- [Configure a Server Core installation of Windows Server 2016 with Sconfig.cmd](/windows-server/get-started/sconfig-on-ws2016)
- [Failover Cluster Cmdlets in Windows PowerShell](/powershell/module/failoverclusters/)
