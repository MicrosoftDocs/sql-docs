---
title: "Install Distributed Replay (Setup) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 64479cdc-661a-4e32-a381-8f8b5a238337
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Install Distributed Replay (Setup)
  Install the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay features with the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Installation Wizard. When planning where to install the features, consider the following:  
  
-   You can install the administration tool on the same computer as the Distributed Replay controller, or on different computers.  
  
-   There can only be one controller in each Distributed Replay environment.  
  
-   You can install the client service on up to 16 (physical or virtual) computers.  
  
-   Only one instance of the client service can be installed on the Distributed Replay controller computer. If your Distributed Replay environment will have more than one client, we do not recommend installing the client service on the same computer as the controller. Doing so may decrease the overall speed of the distributed replay.  
  
-   For performance testing scenarios, we do not recommend installing the administration tool, Distributed Replay controller service, or client service on the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Installing all of these features on the target server should be limited to functional testing for application compatibility.  
  
-   After installation, the controller service, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay controller, must be running before you start the Distributed Replay client service on the clients.  
  
> [!NOTE]  
>  To remove or change the Distributed Replay features, use the Windows **Programs and Features** window in **Control Panel**. Select [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] in the **Uninstall or change a program** window, and then click **Remove** to open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Installation Wizard. On the **Select Features** page, check the Distributed Replay features you want to remove.  
  
 **Prerequisites:**  
  
-   Make sure that the computers that you want to use meet the requirements that are described in the topic [Distributed Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md).  
  
-   Before you begin this procedure, you create the domain user accounts that the controller and client services will run under. We recommend that these accounts are not members of the Windows Administrators group. For more information, see the User and Service Accounts section in the [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md) topic.  
  
    > [!NOTE]  
    >  You can use local user accounts if you are running the administration tool, controller service, and client service on the same computer.  
  
 **Installation Locations:**  
  
 Assuming you use the default file locations and a standard installation, the base directory is found at C:\Program Files\Microsoft SQL Server. Within that, following are where the binaries and assemblies are installed to:  
  
-   On a 32-bit system:  
  
     [!INCLUDE[ssInstallPath](../../includes/ssinstallpath-md.md)]Tools  
  
     \- OR -  
  
     \<Share Feature Directory>\Tools\\(user-supplied alternative shared feature directory)  
  
-   On a 64-bit system:  
  
     C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (x86) \120\Tools  
  
     \- OR -  
  
     \<Share Feature Directory (x86)>\Tools\\(user-supplied alternative shared feature (x86) directory)  
  
### To install Distributed Replay features  
  
1.  To start the installation of any of the Distributed Replay features, start the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Installation Wizard.  
  
2.  The **Setup Support Rules** page identifies issues that might occur when installing the SQL Server Setup support files. You must correct any Setup support failures before continuing with Setup.  
  
3.  On the **Product Key** page, select an option button to indicate whether you are installing a free edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or a production version of the product that has a PID key. For more information, see [Editions and Components of SQL Server 2014](../editions-and-components-of-sql-server-2016.md).  
  
4.  On the **License Terms** page, read the license agreement, and then select the check box to accept the license terms and conditions. To help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also enable the feature usage option and send reports to [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
5.  On the **Setup Support Files** page, click **Install** to install or update the Setup Support files for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
6.  On the **Setup Role** page, select **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Feature Installation**, and then click **Next** to continue to the **Feature Selection** page.  
  
7.  On the **Feature Selection** page, configure which features you want to install.  
  
    -   To install the administration tool, select **Management Tools - Basic**.  
  
    -   To install the controller service, select **Distributed Replay Controller**.  
  
    -   To install the client service, select **Distributed Replay Client**.  
  
     **Important**: When you configure Distributed Replay controller, you can specify one or more user accounts that will be used to run the Distributed Replay client services. The following is the list of supported accounts:  
  
    -   Domain user account  
  
    -   User created local user account  
  
    -   Administrator  
  
    -   Virtual account and MSA (Managed Service Account)  
  
    -   Network Services, Local Services, and System  
  
     Group accounts (local or domain) and other built-in accounts (like Everyone) are not accepted.  
  
8.  Optionally, click the ellipsis (...) button to change the shared feature directory path.  
  
    1.  On 32-bit computers, the default installation path is **C:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\**  
  
    2.  On 64-bit computers, the default installation path is **C:\Program Files (x86)\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\**  
  
9. When you are finished, click **Next**.  
  
10. On the **Installation Rules** page, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup validates your computer configuration. Once the validation process is completed, click **Next**.  
  
11. The **Disk Space Requirements** page calculates the required disk space for the features that you specify. Then it compares the required space to the available disk space.  
  
12. On the **Error Reporting** page, specify the information that you want to send to [!INCLUDE[msCoName](../../includes/msconame-md.md)] to help improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, option for error reporting is enabled.  
  
13. On the **Installation Configuration Rules** page, the System Configuration Checker will run one more set of rules to validate your computer configuration with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that you have specified.  
  
14. On the **Ready to Install the Program** page, click **Install**.  
  
    > [!IMPORTANT]  
    >  After you install Distributed Replay you must create firewall rules on the controller and client computers, and grant each client computer permissions on the target server. For more information, see [Complete the Post-Installation Steps](../../tools/distributed-replay/complete-the-post-installation-steps.md).  
  
 These additional topics document other ways to install Distributed Replay:  
  
-   [Install Distributed Replay from the Command Prompt](../../tools/distributed-replay/install-distributed-replay-overview.md)  
  
-   [Install Distributed Replay Using a Configuration File](../../../2014/sql-server/install/install-distributed-replay-using-a-configuration-file.md)  
  
## .NET Framework Security  
 You must have administrative permissions in order to install any of the Distributed Replay features. Only a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login having sysadmin permissions can add the client service accounts to the sysadmin server role of the test server. For more information about Distributed Replay security considerations, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)   
 [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)   
 [Distributed Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md)   
 [Administration Tool Command-line Options &#40;Distributed Replay Utility&#41;](../../tools/distributed-replay/administration-tool-command-line-options-distributed-replay-utility.md)   
 [Configure Distributed Replay](../../tools/distributed-replay/configure-distributed-replay.md)  
  
  
