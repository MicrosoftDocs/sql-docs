---
title: Appliance Network Configuration
description: Appliance network configuration for Analytics Platform System ships with factory-set IP addresses. Learn how to update Ethernet IPs to match your data center.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
  - sfi-ropc-nochange
---

# Appliance network configuration for Analytics Platform System
The IHV factory configures the Analytics Platform System (APS) appliance with a fixed set of IP addresses for all servers and applicable devices. When you receive the appliance, reconfigure the external (Ethernet) IP addresses to match your specific data center requirements.  
  
> [!NOTE]  
> PDW V1 requires eight external (customer-facing) IP addresses to provide external connectivity to each of the control rack nodes. PDW 2012 (V2) enhances network communications by exposing every component of the appliance externally through IP addresses. This approach provides a more robust design which reduces costs, increases flexibility, and enhances data movement, data loading, and Hadoop integration. The number of IP addresses you need depends on the number of nodes in the appliance. To accommodate this larger block of IP addresses, set up a separate subnet for PDW. Within this subnet, there's sufficient IP address space (up to 250 addresses) to accommodate the components of up to five PDW racks.  
  
The **Network Configuration** page enables you to view the externally facing network settings for the nodes on your Analytics Platform System appliance. This page is read-only.  
  
:::image type="content" source="./media/appliance-network-configuration/SQL_Server_PDW_DWConfig_ApplTopNetwork.png" alt-text="A screenshot from the Microsoft Analytics Platform System Configuration Manager, showing the Appliance Network page.":::
  
## <a id="to-update-the-network-configuration-on-your-appliance"></a> Update the network configuration on your appliance
Change the IP addresses of the Microsoft Fabric domain and workload domain by editing the **AplianceInfo.xml** file and then running setup. This operation is offline. The PDW regions automatically stop during the IP address change.  
  
> [!NOTE]  
> Provide domain names during setup. Specify up to six alphanumeric characters, starting with a letter. A frequent naming system creates a Fabric domain starting with F and a PDW workload domain starting with P. This format is presumed throughout the help file topics but isn't required. <!-- MISSING LINKS For more information about the domain structure, see [PDW Domain Security (SQL Server PDW)](../sqlpdw/pdw-domain-security-sql-server-pdw.md) and [Understanding the Security Model of the HDInsight Region (Analytics Platform System)](../hdinsight/understanding-the-security-model-of-the-hdinsight-region.md)  -->    
  
#### <a id="to-change-the-ip-addresses-of-the-analytics-platform-system"></a> Change the IP addresses of the Analytics Platform System
  
1. Using the **Remote Desktop** application, connect to **HST01** by using the workload domain administrator account.  
  
1. On the HST01 node, open the appliance info file at `c:\pdwinst\media\AplianceInfo.xml`.  
  
    > [!NOTE]  
    > If the file isn't present, create a new file.  
  
1. Update the Ethernet IP values as needed, and save the file.  
  
1. In a command prompt window, run the following setup command to update the IP addresses for the PDW region, using the P/F/H domain names and the administrator passwords.  
  
    ```console
    c:\pdwinst\media\setup.exe /action="ConfigureEthernet" /DomainAdminPassword="<password>" /ApplianceInfoFile="C:\PDWINST\media\ApplianceInfo.xml"  
    ```  
  
## Manufacturer references

For more information about Dell appliances, see:  

-   PowerConnect Switch Instructions: [Dell PowerConnect 6200 Series System CLI Reference Guide](https://downloads.dell.com/Manuals/all-products/esuprt_ser_stor_net/esuprt_powerconnect/powerconnect-6224f_Reference%20Guide_en-us.pdf)  
  
-   iDRAC/BMC: [Integrated Dell Remote Access Controller 7 (iDRAC7) Version 1.30.30 User's Guide](https://downloads.dell.com/Manuals/all-products/esuprt_electronics/esuprt_software/esuprt_remote_ent_sys_mgmt/integrated-dell-remote-access-cntrllr-7-v1.30.30_User%27s%20Guide_en-us.pdf?c=us&l=en&cs=555&s=biz)
  
<!-- PDU's **Dell Metered Rack PDU**`ftp://ftp.dell.com/Manuals/all-products/esuprt_ser_stor_net/esuprt_rack_infrastructure/dell-metered-pdu-led_User's%20Guide_en-us.pdf`   -->

## Related content

- [Launch the Configuration Manager in Analytics Platform System](launch-the-configuration-manager.md)
