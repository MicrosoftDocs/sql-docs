---
title: Acquire and Configure Loading Server
description: This article describes how to acquire and configure a loading server as a nonappliance Windows system for submitting data loads to Parallel Data Warehouse (PDW).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Acquire and configure a loading server for Parallel Data Warehouse
This article describes how to acquire and configure a loading server as a nonappliance Windows system for submitting data loads to Parallel Data Warehouse (PDW).  
  
## <a id="Basics"></a> Basics
The loading server:  
  
-   Doesn't have to be a single server. You can load concurrently with multiple loading servers.  
  
-   Your IT team provides and manages the server. You might already have a server or servers that you can use for loading data into PDW.  
  
-   Resides in your own nonappliance rack. You can't place it within the Analytics Platform System appliance.  
  
-   Connects to the appliance through the Appliance InfiniBand network or over Ethernet. For performance, use InfiniBand.  
  
-   Belongs to your own customer domain, not the appliance domain. There's no trust relationship between your customer domain and the appliance domain.  
  
## <a id="Step1"></a> Step 1: Determine capacity requirements
Design the loading system as one or more loading servers that perform concurrent loads. Each loading server doesn't need to be dedicated only to loading, as long as it handles the performance and storage requirements of your workload.  
  
The system requirements for a loading server depend almost completely on your own workload. Use the [Loading Server Capacity Planning Worksheet](loading-server-capacity-planning-worksheet.md) to help determine your capacity requirements.  
  
## <a id="Step2"></a> Step 2: Acquire the server
Now that you better understand your capacity requirements, plan the servers and networking components that you need to purchase or provision. Incorporate the following list of requirements into your purchasing plan, and then purchase your server or provision an existing server.  
  
### <a id="R"></a> Software requirements
Supported operating systems:  
  
-   Windows Server 2012, Windows Server 2012 R2, or later versions. These operating systems require the FDR network adapter.  

  
-   Windows Server 2008 R2. This operating system requires the DDR network adapter.  
  
The server must use the en-US locale to use the dwloader command-line loading tool. dwloader doesn't support other locales.  
  
### Networking requirements for Windows Server 2012 and Windows Server 2012 R2

Although InfiniBand isn't required for loading, it's the recommended connection type for loading servers. For best performance, use Windows Server 2012 or Windows Server 2012 R2, and the FDR InfiniBand network adapter to connect the loading server to the appliance InfiniBand network.  
  
To prepare for a Windows Server 2012 or Windows Server 2012 R2 InfiniBand connection:  
  
1. Plan to rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand switches. For more information from Mellanox Technologies about InfiniBand, see the whitepaper [Introduction to InfiniBand](https://www.mellanox.com/pdf/whitepapers/IB_Intro_WP_190.pdf).  
  
1. Purchase a Mellanox ConnectX-3 FDR InfiniBand single or dual port network adapter. We recommend purchasing the network adapter with two ports for fault tolerance during data transmission. A two port network adapter is required for high availability.  
  
1.  Purchase two FDR InfiniBand cables for a dual port card, or one FDR InfiniBand cable for a single port card. The FDR InfiniBand cables connect the loading server to the appliance InfiniBand network. The cable length depends on the distance between the loading server and the appliance InfiniBand switches, according to your environment.  
  
## <a id="Step3"></a> Step 3: Connect the server to the InfiniBand networks
Use these steps to connect the loading server to the InfiniBand network. If the server doesn't use the InfiniBand network, skip this step.  
  
1. Rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand network.  
  
1. Install the InfiniBand Mellanox ConnectX-3 FDR InfiniBand network adapter into the loading server.  
  
1. Use the FDR cables to connect the InfiniBand network adapter to one of the two InfiniBand switches in the first appliance rack.  
  
1. Install and configure the appropriate Windows driver for the InfiniBand network adapter.  
  
    -   The OpenFabrics Alliance, an industry consortium of InfiniBand vendors, develops InfiniBand drivers for Windows. Your InfiniBand network adapter might include the correct driver. If it doesn't, you can download the driver from [www.openfabrics.org](http://www.openfabrics.org).  
  
1. Configure the InfiniBand and DNS settings for the network adapters. For configuration instructions, see [Configure InfiniBand network adapters](configure-infiniband-network-adapters.md).  
  
## <a id="Step4"></a> Step 4: Install the loading tools
You can download the client tools from the Microsoft Download Center. 

To install `dwloader`, run the `dwloader` installation from the client tools.
  
If you plan to use Integration Services for loading, you need to install Integration Services and the Integration Services destination adapters. The adapters are available in the client tools.

<!-- To install the des[Install Integration Services Destination Adapters](install-integration-services-destination-adapters.md). 
--> 
  
## <a id="Step5"></a> Step 5: Start loading
You're now ready to start loading data. For more information, see:  
  
1. [dwloader Command-Line Loading Tool](dwloader.md)  
  
1. [Load overview](load-overview.md)  
  
## Performance
For the best loading performance on Windows Server 2012 and later versions, turn on Instant File Initialization. When data is overwritten, the operating system doesn't overwrite existing data with zeros. If this setting is a security risk because prior data still exists on the disks, turn off Instant File Initialization.  
  
## <a id="Security"></a> Security notices
Because the data to load isn't stored on the appliance, your IT team is responsible for managing all aspects of the security for your data to load. For example, this responsibility includes managing the security of the data to load, the security of the server used to store loads, and the security of the networking infrastructure that connects the loading server to the SQL Server PDW appliance.  
  
> [!IMPORTANT]  
> It's especially important to secure each loading server that uses the dwloader command-line loading tool. When dwloader loads data, it first authenticates with the Control node. After successful authentication, it moves data from the loading server directly to the Compute nodes over data channels. Certificate validation doesn't occur during the handshake between each loading server and each Compute node. This process leaves the Compute nodes exposed to potential man-in-the-middle attacks on each data channel while loading. These attacks could result in tampered data and information disclosure.  
  
To reduce security risks with your data, follow these recommendations:  
  
-   Designate one Windows account that's used solely for loading data into PDW. Give this account permissions to the load location and no other permissions.  
  
-   Designate one PDW user that has permissions to load data. Depending on your security requirements, you could have one specific user per database.  
  
-   Operations on the loading server can accept a UNC path from which to pull data from outside the trusted internal network. An attacker on the network or with the ability to influence name resolution can intercept or modify data sent to the SQL Server PDW. This vulnerability presents a tampering and information disclosure risk. To mitigate tampering, require signing on the connection. To help mitigate this risk, set the following group policy option in **Security Settings\Local Policies\Security Options** on the loading server:  **Microsoft network client: Digitally sign communications (always): Enabled**  
  
-   Turn off Instant File Initialization on Windows Server 2012 and later versions. This setting is a tradeoff between performance and security, as noted in the Performance section. You need to decide what is best according to your security requirements.  
  
## Related content

- [Backup and restore](backup-and-restore-overview.md)
