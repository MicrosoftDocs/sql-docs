---
title: Install Distributed Replay
titleSuffix: SQL Server Distributed Replay
description: "This article describes the ways you can install Distributed Replay: using the Installation Wizard, the Command Prompt window, or a configuration file."
ms.service: sql
ms.subservice: distributed-replay
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray
ms.custom: seo-lt-2019, intro-installation
ms.date: 06/20/2022
monikerRange: ">= sql-server-2016 || >= sql-server-linux-2017"
---

# Install Distributed Replay

[!INCLUDE [sqlserver2016-2019-only](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

[!INCLUDE [distributed-replay-sql-server-2022](../../includes/distributed-replay-sql-server-2022.md)]

You can install Distributed Replay in one of three ways:

- [Install Distributed Replay from the Installation Wizard](#bkmk_wizard)

- [Install Distributed Replay from the Command Prompt](#bkmk_command_prompt)

- [Install Distributed Replay Using a Configuration File](#bkmk_configuration_file)

## <a name="bkmk_wizard"></a> Install Distributed Replay from the Installation Wizard

Install the Microsoft[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay features with the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Installation Wizard. When planning where to install the features, consider the following:

- You can install the administration tool on the same computer as the Distributed Replay controller, or on different computers.

- There can only be one controller in each Distributed Replay environment.

- You can install the client service on up to 16 (physical or virtual) computers.

- Only one instance of the client service can be installed on the Distributed Replay controller computer. If your Distributed Replay environment will have more than one client, we don't recommend installing the client service on the same computer as the controller. Doing so may decrease the overall speed of the distributed replay.

- For performance testing scenarios, we don't recommend installing the administration tool, Distributed Replay controller service, or client service on the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Installing all of these features on the target server should be limited to functional testing for application compatibility.

- After installation, the controller service, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay controller, must be running before you start the Distributed Replay client service on the clients.

> [!NOTE]  
> To remove or change the Distributed Replay features, use the Windows **Programs and Features** window in **Control Panel**. Select [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] in the **Uninstall or change a program** window, and then select **Remove** to open the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Installation Wizard. On the **Select Features** page, check the Distributed Replay features you want to remove.

**Prerequisites:**

- Make sure that the computers that you want to use meet the requirements that are described in the topic [Distributed Replay Requirements](./sql-server-distributed-replay.md).

- Before you begin this procedure, you create the domain user accounts that the controller and client services will run under. We recommend that these accounts aren't members of the Windows Administrators group. For more information, see the User and Service Accounts section in the [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md) topic.

    > [!NOTE]  
    > You can use local user accounts if you are running the administration tool, controller service, and client service on the same computer.

**Installation Locations:**

Assuming you use the default file locations and a standard installation, the base directory is found at C:\Program Files\Microsoft SQL Server. Within that, following are where the binaries and assemblies are installed to:

- On a 32-bit system:

    [!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]Tools

    \- OR -

    \<Share Feature Directory>\Tools\\(user-supplied alternative shared feature directory)

- On a 64-bit system:

     C:\Program Files\\Microsoft[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (x86)\130\Tools

     \- OR -

     \<Share Feature Directory (x86)>\Tools\\(user-supplied alternative shared feature (x86) directory)

#### To install Distributed Replay features

1. To start the installation of any of the Distributed Replay features, start the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Installation Wizard.

2. The **Setup Support Rules** page identifies issues that might occur when installing the SQL Server Setup support files. You must correct any Setup support failures before continuing with Setup.

3. On the **Product Key** page, select an option button to indicate whether you're installing a free edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or a production version of the product that has a PID key. For more information, see [Editions and Components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).

4. On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to Microsoft.

5. On the **Setup Support Files** page, select **Install** to install or update the Setup Support files for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].

6. On the **Setup Role** page, select **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Feature Installation**, and then select **Next** to continue to the **Feature Selection** page.

7. On the **Feature Selection** page, configure which features you want to install.

    - To install the administration tool, select **Management Tools - Basic**.

    - To install the controller service, select **Distributed Replay Controller**.

    - To install the client service, select **Distributed Replay Client**.

      > [!IMPORTANT]  
      > When you configure Distributed Replay controller, you can specify one or more user accounts that will be used to run the Distributed Replay client services. The following is the list of supported accounts:

    - Domain user account

    - User created local user account

    - Administrator

    - Virtual account and MSA (Managed Service Account)

    - Network Services, Local Services, and System

     Group accounts (local or domain) and other built-in accounts (like Everyone) aren't accepted.

8. Optionally, select the ellipsis (...) button to change the shared feature directory path.

    1. On 32-bit computers, the default installation path is **C:\Program Files\\Microsoft[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\**

    2. On 64-bit computers, the default installation path is **C:\Program Files (x86)\\Microsoft[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\**

9. When you're finished, select **Next**.

10. On the **Installation Rules** page, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup validates your computer configuration. Once the validation process is completed, select **Next**.

11. The **Disk Space Requirements** page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.

12. On the **Error Reporting** page, specify the information that you want to send to Microsoft to help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, option for error reporting is enabled.

13. On the **Installation Configuration Rules** page, the System Configuration Checker will run one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that you've specified.

14. On the **Ready to Install the Program** page, select **Install**.

    > [!IMPORTANT]  
    > After you install Distributed Replay you must create firewall rules on the controller and client computers, and grant each client computer permissions on the target server. For more information, see [Complete the Post-Installation Steps](../../tools/distributed-replay/complete-the-post-installation-steps.md).

### .NET Framework Security

You must have administrative permissions in order to install any of the Distributed Replay features. Only a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login having sysadmin permissions can add the client service accounts to the sysadmin server role of the test server. For more information about Distributed Replay security considerations, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).

## <a name="bkmk_command_prompt"></a> Install Distributed Replay from the Command Prompt

Installing a new instance of Distributed Replay at the command prompt enables you to specify the features to install and how they should be configured. The command prompt installation supports installing, repairing, upgrading, and uninstalling of the Distributed Replay components. When installing through the command prompt, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the /Q parameter.

> [!NOTE]  
> For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

### Installation Parameters

The list of top-level features include [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], and Tools. The Tools feature will install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Tools, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and other shared components. To install the Distributed Replay components, specify the following parameters:

|Component|Parameter|  
|---------------|---------------|  
|Distributed Replay controller|**DREPLAY_CTLR**|  
|Distributed Replay client|**DREPLAY_CLT**|  
|Administration Tool|**Tools**|

> [!IMPORTANT]  
> After you install Distributed Replay you must create firewall rules on the controller and client computers, and grant each client computer permissions on the target server. For more information, see [Complete the Post-Installation Steps](../../tools/distributed-replay/complete-the-post-installation-steps.md).

Use the parameters in the following table to develop command line scripts for installation.

|Parameter|Description|Supported Values|  
|---------------|-----------------|----------------------|  
|/CTLRSVCACCOUNT<br /><br /> **Optional**|Service account for the Distributed Replay controller service.|Checks account and password|  
|/CTLRSVCPASSWORD<br /><br /> **Optional**|Password for the Distributed Replay controller service account.|Checks account and password|  
|/CTLRSTARTUPTYPE<br /><br /> **Optional**|Start up type for the Distributed Replay controller service.|Automatic<br /><br /> Disabled<br /><br /> Manual|  
|/CTLRUSERS<br /><br /> **Optional**|Specify which users have permissions for the Distributed Replay controller service.|Set of user account strings using " " (space) for delimiter<br /><br /> **Important**: When you configure the Distributed Replay controller service, you can specify one or more user accounts that will be used to run the Distributed Replay client services. The following is the list of supported accounts:<br /><br /> Domain user account<br /><br /> User created local user account<br /><br /> Administrator<br /><br /> Administrator<br /><br /> Virtual account and MSA (Managed Service Account)<br /><br /> Network Services, Local Services, and System<br /><br /> <br /><br /> Note: Group accounts (local or domain) and other built-in accounts (like Everyone) aren't accepted.|  
|/CLTSVCACCOUNT<br /><br /> **Optional**|Service account for the Distributed Replay client service.|Checks account and password|  
|/CLTSVCPASSWORD<br /><br /> **Optional**|Password for the Distributed Replay client service account.|Checks account and password|  
|/CLTSTARTUPTYPE<br /><br /> **Optional**|Start up type for the Distributed Replay client service.|Automatic<br /><br /> Disabled<br /><br /> Manual|  
|/CLTCTLRNAME<br /><br /> **Optional**|The computer name that the client communicates with for the Distributed Replay Controller service.||  
|/CLTWORKINGDIR<br /><br /> **Optional**|The working directory for the Distributed Replay Client service.|Valid path|  
|/CLTRESULTDIR<br /><br /> **Optional**|The result directory for the Distributed Replay Client service.|Valid path|

### Sample Syntax:

**To install the Distributed Replay controller component**

```dos
setup /q /ACTION=Install /FEATURES=DREPLAY_CTLR /IAcceptSQLServerLicenseTerms /CTLRUSERS="domain\user1" "domain\user2" /CTLRSVCACCOUNT="domain\svcuser" /CTLRSVCPASSWORD="password" /CTLRSTARTUPTYPE=Automatic  
```

**To install the Distributed Replay client component**

```dos
setup /q /ACTION=Install /FEATURES=DREPLAY_CLT /IAcceptSQLServerLicenseTerms /CLTSVCACCOUNT="domain\svcuser" /CLTSVCPASSWORD="password" /CLTSTARTUPTYPE=Automatic /CLTCTLRNAME=ControllerMachineName /CLTWORKINGDIR="C:\WorkingDir" /CLTRESULTDIR="C:\ResultDir  
```

## <a name="bkmk_configuration_file"></a> Install Distributed Replay Using a Configuration File

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup provides the ability to generate a configuration file based on user input and system defaults. If you specify that you want the Management tools installed, you can use the configuration file to deploy the three Distributed Replay components (administration tool, Distributed Replay controller, and the Distributed Replay client). It supports Installing, repairing, and uninstalling of the Distributed Replay components.

Setup supports the use of the configuration file only through the command-line. The processing order of the parameters while using the configuration file is outlined below:

- The configuration file overwrites the defaults in a package

- Command-line values overwrite the values in the configuration file

For more information about how to use a configuration file, see [Install SQL Server 2016 Using a Configuration File](../../database-engine/install-windows/install-sql-server-using-a-configuration-file.md).

> [!IMPORTANT]  
> After you install Distributed Replay you must create firewall rules on the controller and client computers, and grant each client computer permissions on the target server. For more information, see [Complete the Post-Installation Steps](../../tools/distributed-replay/complete-the-post-installation-steps.md).

#### To generate a configuration file

1. Follow the Setup wizard through to the **Ready to Install** page. The path to the configuration file is specified in the **Ready to Install** page in the configuration file path section.

2. Cancel the setup without actually completing the installation, to generate the INI file.

#### To Install Distributed Replay Using the Configuration File

- Run the installation through the command prompt and supply the ConfigurationFile.ini using the ConfigurationFile parameter.

**Sample Syntax**

Following is an example of how to specify the configuration file at the command prompt:

```dos
Setup.exe /CTLRSVCPASSWORD="ctlrsvcpswd" /CLTSVCPASSWORD="cltsvcpswd" / ConfigurationFile=ConfigurationFile.INI\  
```

> [!NOTE]
> You must specify both passwords in the command line because you cannot configure them in the configuration file.

## See also

- [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md)
- [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)
- [Distributed Replay Requirements](./sql-server-distributed-replay.md)
- [Administration Tool Command-line Options &#40;Distributed Replay Utility&#41;](../../tools/distributed-replay/administration-tool-command-line-options-distributed-replay-utility.md)
- [Configure Distributed Replay](../../tools/distributed-replay/configure-distributed-replay.md)