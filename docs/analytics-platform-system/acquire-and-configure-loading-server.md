---
title: Acquire & configure loading server
description: This article describes how to acquire and configure a loading server as a non-appliance Windows system for submitting data loads to Parallel Data Warehouse (PDW).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Acquire and configure a loading server for Parallel Data Warehouse
This article describes how to acquire and configure a loading server as a non-appliance Windows system for submitting data loads to Parallel Data Warehouse (PDW).  
  
## <a name="Basics"></a>Basics  
The loading server:  
  
-   Does not have to be a single server. You can load concurrently with multiple loading servers.  
  
-   Is provided and managed by your own IT team. You might already have a server or servers that can be used for loading data into PDW.  
  
-   Is located in your own non-appliance rack, and cannot be placed within the Analytics Platform System appliance.  
  
-   Is connected to the appliance through the Appliance InfiniBand network, or over Ethernet. For performance, we recommend using InfiniBand.  
  
-   Is in your own customer domain, not the appliance domain. There is no trust relationship between your customer domain and the appliance domain.  
  
## <a name="Step1"></a>Step 1: Determine capacity requirements  
The loading system can be designed as one or more loading servers that perform concurrent loads. Each Loading server does not have to be dedicated only to loading, as long as it will handle the performance and storage requirements of your workload.  
  
The system requirements for a loading server depend almost completely on your own workload. Use the [Loading Server Capacity Planning Worksheet](loading-server-capacity-planning-worksheet.md) to help determine your capacity requirements.  
  
## <a name="Step2"></a>Step 2: Acquire the sServer  
Now that you better understand your capacity requirements, you can plan the servers and networking components that you will need to purchase or provision. Incorporate the following list of requirements into your purchasing plan, and then purchase your server or provision an existing server.  
  
### <a name="R"></a>Software Requirements  
Supported Operating Systems:  
  
-   Windows Server 2012 or Windows Server 2012 R2. These operating systems require the FDR network adapter.  
  
-   Windows Server 2008 R2. This OS requires the DDR network adapter.  
  
The server must use the EN-US locale in order to use the dwloader Command-Line Loading tool. dwloader does not support other locales.  
  
### Networking Requirements for Windows Server 2012 and Windows Server 2012 R2  
Although not required for Loading, InfiniBand is the recommended connection type for loading servers. For best performance, use Windows Server 2012 or Windows Server 2012 R2, and the FDR InfiniBand network adapter to connect the Loading server to the appliance InfiniBand network.  
  
To prepare for a Windows Server 2012 or Windows Server 2012 R2 InfiniBand connection:  
  
1.  Plan to rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand switches. For more information from Mellanox Technologies about InfiniBand, see the whitepaper, [Introduction to InfiniBand](https://www.mellanox.com/pdf/whitepapers/IB_Intro_WP_190.pdf).  
  
2.  Purchase a Mellanox ConnectX-3 FDR InfiniBand single or dual port network adapter. We recommend purchasing the network adapter with two ports for fault tolerance during data transmission. A two port network adapter is required for high availability.  
  
3.  Purchase 2 FDR InfiniBand cables for a dual port card, or 1 FDR InfiniBand cable for a single port card. The FDR InfiniBand cables will connect the loading server to the appliance InfiniBand network. The cable length depends on the distance between the loading server and the appliance InfiniBand switches, according to your environment.  
  
## <a name="Step3"></a>Step 3: Connect the server to the InfiniBand networks  
Use these steps to connect the loading server to the InfiniBand network. If the server is not using the InfiniBand network, skip this step.  
  
1.  Rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand network.  
  
2.  Install the InfiniBand Mellanox ConnectX-3 FDR InfiniBand network adapter into the loading server.  
  
3.  Use the FDR cables to connect the InfiniBand network adapter to one of the two InfiniBand switches in the first appliance rack.  
  
4.  Install and configure the appropriate Windows driver for the InfiniBand network adapter.  
  
    -   InfiniBand drivers for Windows are developed by the OpenFabrics Alliance, an industry consortium of InfiniBand vendors.  The correct driver may have been distributed with your InfiniBand network adapter. If not, the driver can be downloaded from www.openfabrics.org.  
  
5.  Configure the InfiniBand and DNS settings for the network adapters. For configuration instructions, see [Configure InfiniBand network adapters](configure-infiniband-network-adapters.md).  
  
## <a name="Step4"></a>Step 4: Install the loading tools  
The Client Tools are available for download from the Microsoft Download Center. 

To install dwloader, run the dwloader installation from the client tools.
  
If you plan to use Integration Services for loading, you will need to install Integration Services  and the Integration Services destination adapters. The adapters are available in the Client Tools.

<!-- To install the des[Install Integration Services Destination Adapters](install-integration-services-destination-adapters.md). 
--> 
  
## <a name="Step5"></a>Step 5: Start Loading  
You are now ready to start loading data. For more information, see:  
  
1.  [dwloader Command-Line Loading Tool](dwloader.md)  
  
2.  [Load overview](load-overview.md)  
  
## Performance  
For best loading performance on Windows Server 2012 and beyond, turn on Instant File Initialization so that when data is overwritten, the operating system will not overwrite existing data with zeros. If this is a security risk because prior data still exists on the disks, then be sure to turn Instant File Initialization off.  
  
## <a name="Security"></a>Security Notices  
Since the data to load is not stored on the appliance, your IT team is responsible for managing all aspects of the security for your data to load. For example, this includes managing the security of the data to load, the security of the server used to store loads, and the security of the networking infrastructure that connects the loading server to the SQL Server PDW appliance.  
  
> [!IMPORTANT]  
> It is especially important to secure each loading server that will use dwloader Command-line Loading tool. When dwloader loads data, it first authenticates with the Control node, and then after successful authentication it moves data from the loading server directly to the Compute nodes over data channels. Certificate validation does not occur  during the hand-shake between each loading server and each Compute node. This leaves the Compute nodes exposed to potential man-in-the-middle attacks on each data channel while loading. These attacks could result in tampered data and/or information disclosure.  
  
To reduce security risks with your data, we advise the following:  
  
-   Designate one Windows account that is used solely for the purpose of loading data into PDW. Allow this account to have permissions to the load location and nowhere else.  
  
-   Designate one PDW user that has permissions to load data. Depending on your security requirements, you could have one specific user per database.  
  
-   Operations on the loading server can accept an UNC path from which to pull data from outside the trusted internal network. And an attacker on the network or with ability to influence name resolution can intercept or modify data sent to the SQL Server PDW. This presents a tampering and information disclosure risk. Tampering should be mitigated by requiring signing on the connection. To help mitigate this risk, set the following group policy option in **Security Settings\Local Policies\Security Options** on the loading server:  **Microsoft network client: Digitally sign communications (always): Enabled**  
  
-   Turn off Instant File Initialization on Windows Server 2012 and beyond. This is a tradeoff between performance and security, as noted in the Performance section. You need to decide what is best according to your security requirements.  
  
## See Also  
[Backup and restore overview](backup-and-restore-overview.md)  
  
