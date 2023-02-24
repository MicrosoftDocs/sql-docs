---
title: Configuration checklists
description: Provides checklists for the tasks required to configure Analytics Platform System for your own environment. These configuration tasks are necessary before you can use the appliance.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Appliance configuration checklists for Analytics Platform System
Provides checklists for the tasks required to configure Analytics Platform System for your own environment. These configuration tasks are necessary before you can use the appliance.  
  
> [!WARNING]  
> Using Analytics Platform System**Configuration Manager** is the best way, and the only supported way, to perform the tasks available in the tool.  
  
## <a name="BeforeTasks"></a>Before You Begin  
  
### Prerequisites  
  
1.  The appliance must be installed in the data center and powered on.  
  
2.  Make sure you have the following information, which is provided by your IHV:  
  
    -   External IP address for the PDW Control node (*PDW_region-*CTL01)  
  
    -   Appliance domain name  
  
    -   User name and password for the appliance domain administrator  
  
3.  Obtain a trusted certificate. You will import this in a later step to allow clients to connect to the **Admin Console** with secure connections. Save the certificate to the Control node in a password protected PFX file.  
  
4.  Launch the **Configuration Manager**, using the following steps:  
  
    1.  Use **Remote Desktop** to connect to the PDW Control node (*PDW_region*-CTL01). (You may need to connect with the external IP address for CTL01.)  
  
    2.  Launch the **Configuration Manager** from the **Start** menu of the PDW Control node. The first screen of the Configuration Manager displays the appliance topology, which was created by your IHV. It is a list of the hardware nodes recognized by your SQL Server PDW software as part of your appliance. You should not need to change any settings on the Appliance Topology screen.  
  
## <a name="CMTasks"></a>Perform Configuration Manager Tasks  
The SQL Server PDW**Configuration Manager** (PDWCM) is an appliance administration tool that SQL Server PDW system administrators use to perform appliance-level operations and to change appliance-level settings. For example, use PDWCM to reset passwords, set the time zone, change IP addresses, configure SSL certificates, enable remote access through the firewall, start or stop the appliance, and set Instant File Initialization.  
  
Use **Configuration Manager** to perform the following configuration tasks.  
  
|Configuration Task|Description|  
|----------------------|---------------|  
|Become familiar with the physical component names|[PDW and Appliance Fabric Physical Components &#40;Analytics Platform System&#41;](pdw-and-appliance-fabric-physical-components.md)|  
|Launch SQL Server PDW Configuration Manager|[Launch the Configuration Manager &#40;Analytics Platform System&#41;](launch-the-configuration-manager.md)|  
|Change the domain administrator password|The appliance has a private Windows Active Directory Domain Services that is used to authenticate nodes within the appliance.<br /><br />Your IHV set up the appliance with a default domain administrator password. This needs to be changed to a password that is secure.<br /><br />The **Configuration Manager** is the only supported way to change the domain administrator password.<br /><br />For more information, see [Password Reset &#40;Analytics Platform System&#41;](password-reset.md).|  
|Change the password for the **sa** logon|SQL Server PDW has a system administrator logon named **sa**. The **sa** logon has all privileges. It can grant, deny, or revoke any permission. It can also see all system views.<br /><br />For more information, see [Password Reset &#40;Analytics Platform System&#41;](password-reset.md).|  
|Set the appliance time zone|Set the time (local or other desired time) for all appliance nodes.<br /><br />For more information, see [Appliance Time Zone Configuration &#40;Analytics Platform System&#41;](appliance-time-zone-configuration.md).|  
|Specify externally facing network settings for the SQL Server PDW appliance|[Appliance Network Configuration &#40;Analytics Platform System&#41;](appliance-network-configuration.md)|  
|Import a security certificate for the Admin Console|A certificate can provide Secure Sockets Layer (SSL) connections over HTTPS to the [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](monitor-the-appliance-by-using-the-admin-console.md). By default, the **Admin Console** includes a self-signed certificate that provides privacy, but not server authentication. This certificate also returns an error in Internet Explorer stating: "There is a problem with this website's security certificate" when the user connects. Although this connection encrypts in-flight data between the client and the server, the connection is still at risk from attackers.<br /><br />SQL Server PDW administrators should immediately acquire a certificate that chains to a trusted certificate authority recognized by clients, in order to have a secure connection and remove the error that Internet Explorer reports. This requires a fully qualified domain name that maps the Control node's virtual IP Address (recommended) or a certificate name that matches the value that users type into their browser address bars to access the Admin Console.<br /><br />Use the **Configuration Manager** to add or remove trusted certificates. Directly using the Microsoft Windows HTTP Services Certificate Configuration Tool (`winHttpCertCfg.exe`) to manage certificates is unsupported.<br /><br />For more information, see [PDW Certificate Provisioning &#40;Analytics Platform System&#41;](pdw-certificate-provisioning.md).|
|Feature Switch|Displays information about feature switches that are introduced in Analytics Platform System AU7. Use this page to update or enable/disable features and settings in Analytics Platform System. Changing feature switch values requires a service restart.<br /><br />For more information, see [PDW feature switch &#40;Analytics Platform System&#41;](appliance-feature-switch.md).|
|Enable or disable Windows Firewall rules that allow or prevent access to specific ports on the SQL Server PDW appliance.|Your IHV will configure and enable the firewall rules that are necessary for the appliance to operate properly. In most cases you will not enable or disable firewall rules.<br /><br />For more information, see [PDW Firewall Configuration &#40;Analytics Platform System&#41;](pdw-firewall-configuration.md).|  
|Start and stop the SQL Server PDW appliance|Stops or starts the SQL Server PDW appliance. For more information, see [PDW Services Status &#40;Analytics Platform System&#41;](pdw-services-status.md).|  
|Review Instant File Initialization options using the **Privileges** dialog box|Instant File Initialization is a SQL Server feature that allows data file operations to run more quickly. It is enabled on SQL Server PDW only if the Network Service account has been granted the SE_MANAGE_VOLUME_NAME privilege. It is turned off by default.<br /><br />For more information, see [Instant File Initialization Configuration &#40;Analytics Platform System&#41;](instant-file-initialization-configuration.md).|  
|Restore the master database from a backup|Deletes the current **master** database and replaces it with a backup. For more information, see [Restore the Master Database &#40;Analytics Platform System&#41;](restore-the-master-database.md).|  
  
## <a name="AddTasks"></a>Perform Additional Configuration Tasks  
After performing the **Configuration Manager** tasks, perform the following list of additional configuration tasks. Some of these tasks are optional.  
  
|Configuration Task|Description|  
|----------------------|---------------|  
|Third-party antivirus software can be installed and configured on the SQL Server PDW appliance for externally facing nodes.<br /><br />(Optional)|For more information, see [Antivirus Software &#40;Analytics Platform System&#41;](antivirus-software.md).|  
|The password for DSRM can be changed.<br /><br />(Optional)|For more information, see [Set Admin Password for Logging on to AD Nodes in Directory Services Restore Mode &#40;DSRM&#41; &#40;Analytics Platform System&#41;](set-admin-password-for-logging-on-to-ad-nodes-in-directory-services-restore-mode.md).|  
|Configure the appliance to receive software updates<br /><br />(Recommended)|The appliance needs to be configured to receive updates to the SQL Server PDW and underlying software.<br /><br />For more information, see [Configure Windows Server Update Services &#40;WSUS&#41; &#40;Analytics Platform System&#41;](configure-windows-server-update-services-wsus.md). For information about WSUS, see [Software Servicing &#40;Analytics Platform System&#41;](software-servicing.md).|  
|Configure connectivity to external data such as Hadoop or Azure Blob Storage.<br /><br />(Optional)|For more information, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](configure-polybase-connectivity-to-external-data.md).|  
|Configure Antivirus Software<br /><br />(Optional)|Third-party antivirus solutions can be used to protect externally facing nodes but is not required. Follow the guidelines in.|  
|Configure InfiniBand Network Adapters on Backup and Loading Servers<br /><br />(Optional)|To configure Backup and Loading servers to connect to SQL Server PDW by using the InfiniBand network, you need to configure the network adapters to allow the appliance DNS to resolve the InfiniBand connection to the currently active InfiniBand network.|  
|Configure to send telemetry data to Microsoft<br /><br />(Optional)|To configure Analytics Platform System to send telemetry data to Microsoft, you need to run a PowerShell script on the Control node. For specific instructions, see [Send Telemetry Feedback to Microsoft &#40;SQL Server PDW&#41;](send-telemetry-feedback-to-microsoft-sql-server-pdw.md).|  
  
## See Also  
[Antivirus Software &#40;Analytics Platform System&#41;](antivirus-software.md)  
[Configure InfiniBand Network Adapters &#40;SQL Server PDW&#41;](configure-infiniband-network-adapters.md)  
  
