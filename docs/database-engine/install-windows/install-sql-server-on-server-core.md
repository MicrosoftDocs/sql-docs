---
title: "Install SQL Server 2016 on Server Core | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 1dd294cc-5b69-4d0c-9005-3e307b75678b
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server on Server Core

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a Server Core installation.   
  
The Server Core installation option provides a minimal environment for running specific server roles. This helps to reduce maintenance and management requirements and the attack surface for those server roles. For more information on Server Core, see [Install Server Core](https://docs.microsoft.com/windows-server/get-started/getting-started-with-server-core). For more information on Server Core as implemented on [!INCLUDE[win8srv](../../includes/win8srv-md.md)], see [Server Core for Windows Server 2012](https://msdn.microsoft.com/library/hh846323\(VS.85\).aspx) (https://msdn.microsoft.com/library/hh846323(VS.85).aspx).  
  
 For a list of currently supported operating systems, see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

## Prerequisites  
  
|Requirement|How to install|  
|-----------------|--------------------|  
|[!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6.1 |For all editions of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] except [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], Setup requires the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.6.1 Server Core Profile. SQL Server Setup will automatically install this if it is not already installed. Installation requires a reboot. You can install [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] before you run setup to avoid a reboot.|  
|Windows Installer 4.5|Shipped with Server Core installation.|  
|Windows PowerShell|Shipped with Server Core installation.|  
|Java Runtime |In order to use PolyBase, you need to install the appropriate Java Runtime. For more information, see [PolyBase installation](../../relational-databases/polybase/polybase-installation.md).|
  
##  <a name="BK_SupportedFeatures"></a> Supported Features  
 Use the following table to find which features are supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on a Server Core installation .  
  
|Feature|Supported|Additional Information|  
|-------------|---------------|----------------------------|  
|[!INCLUDE[ssDE](../../includes/ssde-md.md)] Services|Yes||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication|Yes||  
|Full Text Search|Yes||  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|Yes||  
|[!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)]|Yes||  
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|No||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Data Tools (SSDT)|No||  
|Client Tools Connectivity|Yes||  
|Integration Services Server|Yes||  
|Client Tools Backward Compatibility|No||  
|Client Tools SDK|No||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online|No||  
|Management Tools - Basic|Remote Only|Installation of these features on Server Core is not supported. These components can be installed on a different server that is not Server Core and connected to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] services installed on Server Core.|  
|Management Tools - Complete|Remote Only|Installation of these features on Server Core is not supported. These components can be installed on a different server that is not Server Core and connected to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] services installed on Server Core.|  
|Distributed Replay Controller|No||  
|Distributed Replay Client|Remote Only|Installation of these features on Server Core is not supported. These components can be installed on a different server that is not Server Core , and connected to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] services installed on Server Core.|  
|SQL Client Connectivity SDK|Yes||  
|Microsoft Sync Framework|Yes|Microsoft Sync Framework is not included in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation package. You can download the appropriate version of Sync Framework from this [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkId=221788) (https://go.microsoft.com/fwlink/?LinkId=221788) page and install it on a computer that is running Server Core.|  
|[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]|No||  
|[!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)]|No||  
  
## Supported scenarios  
 The following table shows the supported scenario matrix for installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on a Server Core.  
  
|||  
|-|-|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] editions|All [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] 64-bit editions |  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] language|All languages|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] language on OS language/locale (combination)|ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on JPN (Japanese) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on GER (German) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on CHS (Chinese-China) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on ARA (Arabic (SA)) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on THA (Thai) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on TRK (Turkish) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on pt-PT (Portuguese Portugal) Windows<br /><br /> ENG [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on ENG (English) Windows|  
|Windows edition|[!INCLUDE[winserver2016_datacenter_md](../../includes/winserver2016-datacenter-md.md)]<br/><br/>[!INCLUDE[winserver2016_standard_md](../../includes/winserver2016-standard-md.md)]<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] R2 Datacenter<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] R2 Standard<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] R2 Essentials<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] R2 Foundation<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] Datacenter<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] Standard<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] Essentials<br/><br/>[!INCLUDE[win8srv](../../includes/win8srv-md.md)] Foundation|  
  
## Upgrade 
 On Server Core installations, upgrading from [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] is supported.  
  
## Install  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] does not support setup by using the installation wizard on the Server Core operating system. When installing on Server Core, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup supports full quiet mode by using the /Q parameter, or Quiet Simple mode by using the /QS parameter. For more information, see [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
 Regardless of the installation method, you are required to confirm acceptance of the software license terms as an individual or on behalf of an entity, unless your use of the software is governed by a separate agreement such as a [!INCLUDE[msCoName](../../includes/msconame-md.md)] volume licensing agreement or a third-party agreement with an ISV or OEM.  
  
 The license terms are displayed for review and acceptance in the Setup user interface. Unattended installations (using the /Q or /QS parameters) must include the /IACCEPTSQLSERVERLICENSETERMS parameter. You can review the license terms separately at [Microsoft Software License Terms](https://go.microsoft.com/fwlink/?LinkId=148209).  
  
> [!NOTE]  
>  Depending on how you received the software (for example, through [!INCLUDE[msCoName](../../includes/msconame-md.md)] volume licensing), your use of the software may be subject to additional terms and conditions.  
  
 To install specific features, use the /FEATURES parameter and specify the parent feature or feature values. For more information about feature parameters and their use, see the following sections.  
  
### Feature parameters  
  
|Feature parameter|Description|  
|-----------------------|-----------------|  
|SQLENGINE|Installs only the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|REPLICATION|Installs the Replication component along with [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|FULLTEXT|Installs the FullText component along with [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|AS|Installs all [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] components.|  
|IS|Installs all [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components.|  
|CONN|Installs the connectivity components.| 
|ADVANCEDANALYTICS |Installs R Services, requires the database engine. Unattended installations require /IACCEPTROPENLICENSETERMS parameter.  |


 See the following examples of the usage of feature parameters:  
  
|Parameter and values|Description|  
|--------------------------|-----------------|  
|/FEATURES=SQLEngine|Installs only the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|/FEATURES=SQLEngine,FullText|Installs the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and full-text.|  
|/FEATURES=SQLEngine,Conn|Installs the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and the connectivity components.|  
|/FEATURES=SQLEngine,AS,IS,Conn|Installs the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], and the connectivity components.|  
|/FEATURES=SQLENGINE,ADVANCEDANALYTICS /IACCEPTROPENLICENSETERMS |Installs the  [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)].|  

  
### Installation options  
 The Setup supports the following installation options while installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on a Server Core operating system:  
  
1.  **Installation from Command Line**  
  
     To install specific features using the command prompt installation option, use the /FEATURES parameter and specify the parent feature or feature values. The following is an example of using the parameters from the command line:  
  
    ```  
    Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,Replication /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<StrongPassword>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="NT AUTHORITY\Network Service" /TCPENABLED=1 /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
2.  **Installation using Configuration File**  
  
     Setup supports the use of the configuration file only through the command prompt. The configuration file is a text file with the basic structure of a parameter (name/value pair) and a descriptive comment. The configuration file specified at the command prompt should have an .INI file name extension. See the following examples of ConfigurationFile.INI:  
  
    - Installing [!INCLUDE[ssDE](../../includes/ssde-md.md)]. 
    
    The following example shows how to install a new stand-alone instance that includes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)]:  
  
        ```  
        ; SQL Server Configuration File  
        [OPTIONS]  
  
        ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.   
  
        ACTION="Install"  
  
        ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.   
  
        FEATURES=SQLENGINE  
  
        ; Specify a default or named instance. MSSQLSERVER is the default instance for non-Express editions and SQLExpress for Express editions. This parameter is required when installing the ssNoVersion Database Engine, and Analysis Services (AS).  
  
        INSTANCENAME="MSSQLSERVER"  
  
        ; Specify the Instance ID for the ssNoVersion features you have specified. ssNoVersion directory structure, registry structure, and service names will incorporate the instance ID of the ssNoVersion instance.   
  
        INSTANCEID="MSSQLSERVER"  
  
        ; Account for ssNoVersion service: Domain\User or system account.   
  
        SQLSVCACCOUNT="NT Service\MSSQLSERVER"  
  
        ; Windows account(s) to provision as ssNoVersion system administrators.   
  
        SQLSYSADMINACCOUNTS="\<DomainName\UserName>"  
  
        ; Accept the License agreement to continue with Installation  
  
        IAcceptSQLServerLicenseTerms="True"  
  
        ```  
  
    -   Installing connectivity components. The following example shows how to install the connectivity components:  
  
        ```  
        ; SQL Server Configuration File  
        [OPTIONS]  
  
        ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.   
  
        ACTION="Install"  
  
        ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.   
  
        FEATURES=Conn  
  
        ; Specifies acceptance of License Terms  
  
        IAcceptSQLServerLicenseTerms="True  
  
        ```  
  
    -   Installing all supported features  
  
        The following example shows how to install all supported features of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on Server Core:  
  
        ```  
        ; SQL Server Configuration File  
        [OPTIONS]  
        ; Specifies a Setup work flow, like INSTALL, UNINSTALL, or UPGRADE. This is a required parameter.   
  
        ACTION="Install"  
  
        ; Specifies features to install, uninstall, or upgrade. The lists of features include SQLEngine, FullText, Replication, AS, IS, and Conn.   
  
        FEATURES=SQLENGINE,FullText,Replication,AS,IS,Conn  
  
        ; Specify a default or named instance. MSSQLSERVER is the default instance for non-Express editions and SQLExpress for Express editions. This parameter is required when installing the ssNoVersion Database Engine (SQL), or Analysis Services (AS).  
  
        INSTANCENAME="MSSQLSERVER"  
  
        ; Specify the Instance ID for the ssNoVersion features you have specified. ssNoVersion directory structure, registry structure, and service names will incorporate the instance ID of the ssNoVersion instance.   
  
        INSTANCEID="MSSQLSERVER"  
  
        ; Account for ssNoVersion service: Domain\User or system account.   
  
        SQLSVCACCOUNT="NT Service\MSSQLSERVER"  
  
        ; Windows account(s) to provision as ssNoVersion system administrators.   
  
        SQLSYSADMINACCOUNTS="\<DomainName\UserName>"  
  
        ; The name of the account that the Analysis Services service runs under.   
  
        ASSVCACCOUNT= "NT Service\MSSQLServerOLAPService"  
  
        ; Specifies the list of administrator accounts that need to be provisioned.   
  
        ASSYSADMINACCOUNTS="\<DomainName\UserName>"  
  
        ; Specifies the server mode of the Analysis Services instance. Valid values are MULTIDIMENSIONAL, POWERPIVOT or TABULAR. ASSERVERMODE is case-sensitive. All values must be expressed in upper case.   
  
        ASSERVERMODE="MULTIDIMENSIONAL"  
  
        ; Optional value, which specifies the state of the TCP protocol for the ssNoVersion service. Supported values are: 0 to disable the TCP protocol, and 1 to enable the TCP protocol.  
  
        TCPENABLED=1  
  
        ;Specifies acceptance of License Terms  
  
        IAcceptSQLServerLicenseTerms="True"  
        ```  
  
     The following shows how you can launch Setup using a custom or default configuration file:  
  
    -   Launch setup using a custom configuration file:  
  
         To specify the configuration file at the command prompt:  
  
        ```  
        Setup.exe /QS /ConfigurationFile=MyConfigurationFile.INI  
        ```  
  
         To specify passwords at the command prompt instead of in the configuration file:  
  
        ```  
        Setup.exe /QS /SQLSVCPASSWORD="************" /ASSVCPASSWORD="************"  /ConfigurationFile=MyConfigurationFile.INI  
        ```  
  
    -   Launch setup using DefaultSetup.ini:  
  
         If you have the DefaultSetup.ini file in the \x86 and \x64 folders at the root level of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source media, open the DefaultSetup.ini file, and then add the *Features* parameter to the file.  
  
         If the DefaultSetup.ini file does not exist, you can create it and copy it to the \x86 and \x64 folders at the root level of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source media.  
  
## Configure remote access of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core  
 Perform the actions described below to configure remote access of a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance that is running on Server Core.  
  
### Enable remote connections on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  

To enable remote connections, use SQLCMD.exe locally and execute the following statements against the Server Core instance:  

   ```Transact-SQL
   EXEC sys.sp_configure N'remote access', N'1'  
   GO
   RECONFIGURE WITH OVERRIDE
   GO
   ```  
  
### Enable and start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] browser service  
 By default, the Browser service is disabled.  If it is disabled on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Server Core, run the following command from the command prompt to enable it:  
  
 `sc config SQLBROWSER start= auto`  
  
 After it is enabled, run the following command from the command prompt to start the service:  
  
 `net start SQLBROWSER`  
  
### Create exceptions in Windows Firewall  
 To create exceptions for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] access in Windows Firewall, follow the steps specified in [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
### Enable TCP/IP on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 The TCP/IP protocol can be enabled through Windows PowerShell for an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Server Core. Follow these steps:  
  
1.  On the server, launch Task Manager.  
  
2.  On the **Applications** tab, click **New Task**.  
  
3.  In the **Create New Task** dialog box, type **sqlps.exe** in the **Open** field and then click **OK**. This opens the **[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Powershell** window.  
  
4.  In the **Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Powershell** window, run the following script to enable the TCP/IP protocol:  
  
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
  
## Uninstall

 After you log on to a computer that is running Server Core, you have a limited desktop environment with an Administrator command prompt. You can use this command prompt to launch the uninstall an of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. To uninstall an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], launch the uninstallation from the command prompt in full quiet mode by using the /Q parameter, or quiet simple mode by using the /QS parameter. The /QS parameter shows progress through the UI, but does not accept any input. /Q runs in a quiet mode without any user interface.  
  
 To uninstall an existing instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
```  
Setup.exe /Q /Action=Uninstall /FEATURES=SQLEngine,AS,IS /INSTANCENAME=MSSQLSERVER  
```  
  
 To remove a named instance, specify the name of the instance instead of `MSSQLSERVER` in the preceding example.  
  
## Start a new command prompt

If you accidentally close the command prompt, you can start a new command prompt by following these steps:  
 
1.  Press Ctrl+Shift+Esc to display Task Manager.  
2.  On the **Applications** tab, click **New Task**.  
3.  In the **Create New Task** dialog box, type **cmd** in the **Open** field and then [!INCLUDE[clickOK](../../includes/clickok-md.md)].  
  
## See also  
 [Install SQL Server Using a Configuration File](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md)   
 [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)   
 [Install Server Core](https://technet.microsoft.com/windows-server-docs/get-started/getting-started-with-server-core)   
 [Configure a Server Core installation of Windows Server 2016 with Sconfig.cmd](https://technet.microsoft.com/windows-server-docs/get-started/sconfig-on-ws2016)   
 [Failover Cluster Cmdlets in Windows PowerShell](/powershell/module/failoverclusters/)

  
  

