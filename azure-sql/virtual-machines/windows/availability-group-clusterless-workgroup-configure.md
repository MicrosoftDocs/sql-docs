---
title: Configure a Domain-Independent Workgroup Availability Group
description: Learn how to configure an Active Directory domain-independent workgroup Always On availability group on a SQL Server virtual machine in Azure.
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma
ms.date: 09/15/2025
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
tags: azure-service-management
---
# Configure a workgroup availability group

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article explains the steps necessary to create an Active Directory domain-independent cluster with an Always On availability group; also known as a workgroup cluster. This article focuses on the steps that are relevant to preparing and configuring the workgroup and availability group. It glosses over steps that are covered in other articles, such as how to create the cluster, or deploy the availability group.

## Prerequisites

To configure a workgroup availability group, you need the following prerequisites:

- At least two Windows Server 2019 (or higher) virtual machines running SQL Server 2017 (or higher), deployed to the same availability set, or different availability zones, using static IP addresses.
- A local network with a minimum of four free IP addresses on the subnet.
- An account on each machine in the administrator group that also has sysadmin rights within SQL Server.
- Open ports: TCP 1433, TCP 5022, TCP 59999.

For reference, the following parameters are used in this article, but can be modified as necessary:

| **Name** | **Parameter** |
| :------ | :---------------------------------- |
| **Node1**   | AGNode1 (10.0.0.4) |
| **Node2**   | AGNode2 (10.0.0.5) |
| **Cluster name** | AGWGAG (10.0.0.6) |
| **Listener** | AGListener (10.0.0.7) |
| **DNS suffix** | ag.wgcluster.example.com |
| **Work group name** | AGWorkgroup |

## Set a DNS suffix

In this step, configure the DNS suffix for both servers. For example, `ag.wgcluster.example.com`. This setting allows you to use the name of the object you want to connect to as a fully qualified address within your network, such as `AGNode1.ag.wgcluster.example.com`.

To configure the DNS suffix, follow these steps:

1. Connect to your first node by using [Bastion](/azure/bastion/bastion-connect-vm-rdp-windows) and open Server Manager.
1. Select **Local Server** and then select the name of your virtual machine under **Computer name**.
1. Select **Change...** under **To rename this computer...**.
1. Change the **Workgroup** name to something meaningful, such as `AGWORKGROUP`:

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/1-change-workgroup-name.png" alt-text="Screenshot showing the 'Computer Name/Doman Changes' dialog box with 'Workgroup' selected and the name 'AGWORKGROUP' entered.":::

1. Select **More...** to open the **DNS Suffix and NetBIOS Computer Name** dialog box.
1. Enter the name of your DNS suffix under **Primary DNS suffix of this computer**, such as `ag.wgcluster.example.com` and then select **OK**:

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/2-add-dns-suffix.png" alt-text="Screenshot that shows the D N S Suffix and NetBIOS Computer Name dialog box with the value ag.wgcluster.example.com entered.":::

1. Confirm that the **Full computer name** is now showing the DNS suffix, and then select **OK** to save your changes:

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/3-confirm-full-computer-name.png" alt-text="Screenshot showing the 'Computer Name/Doman Changes' dialog box with the 'Full computer name' highlighted.":::

1. Restart the server when you're prompted to do so.
1. Repeat these steps on any other nodes to be used for the availability group.

## Edit a host file

Since there's no Active Directory in this configuration, there's no way to authenticate Windows connections. As such, assign trust by editing the host file with a text editor.

To edit the host file, follow these steps:

1. Connect to your virtual machine by using [Bastion](/azure/bastion/bastion-connect-vm-rdp-windows).
1. Use **File Explorer** to go to `c:\windows\system32\drivers\etc`.
1. Right-click the **hosts** file and open the file with **Notepad** (or any other text editor).
1. At the end of the file, add an entry for each node, the availability group, and the listener in the form of `IP Address, DNS Suffix #comment` like:

   ```
   10.0.0.4 AGNode1.ag.wgcluster.example.com #Availability group node
   10.0.0.5 AGNode2.ag.wgcluster.example.com #Availability group node
   10.0.0.6 AGWGAG.ag.wgcluster.example.com #Cluster IP
   10.0.0.7 AGListener.ag.wgcluster.example.com #Listener IP
   ```

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/4-host-file.png" alt-text="Screenshot of the hosts file in Notepad. The new entries for the IP address, cluster, and listener of each new node are added at the end of the file.":::

## Set permissions

Since there's no Active Directory to manage permissions, you need to manually allow a non-builtin local administrator account to create the cluster.

To do so, run the following PowerShell cmdlet in an administrative PowerShell session on every node:

```powershell
new-itemproperty -path HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System -Name LocalAccountTokenFilterPolicy -Value 1
```

## Create the failover cluster

In this step, you create the failover cluster. If you're unfamiliar with the steps, see [Create a Windows Server failover cluster](failover-cluster-instance-storage-spaces-direct-manually-configure.md#create-a-windows-server-failover-cluster) in the [Failover cluster tutorial](failover-cluster-instance-storage-spaces-direct-manually-configure.md).

Notable differences between the failover cluster tutorial and what should be done for a workgroup cluster:

- Uncheck **Storage**, and **Storage Spaces Direct** when running the cluster validation.
- When adding the nodes to the cluster, add the fully qualified name, such as:

  - `AGNode1.ag.wgcluster.example.com`
  - `AGNode2.ag.wgcluster.example.com`

- Uncheck **Add all eligible storage to the cluster**.

Once the cluster is created, assign a static Cluster IP address. To do so, follow these steps:

1. On one of the nodes, open **Failover Cluster Manager**, select the cluster, right-click the **Name: \<ClusterName>** under **Cluster Core Resources** and then select **Properties**.

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/5-launch-cluster-name-properties.png" alt-text="Screenshot showing how to edit the cluster properties in the 'FailoverCluster Manger'.":::

1. Select the IP address under **IP Addresses** and select **Edit**.
1. Select **Use Static**, provide the IP address of the cluster, and then select **OK**:

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/6-provide-static-ip-for-cluster.png" alt-text="Screenshot showing how to edit the static IP address for the cluster.":::

1. Verify that your settings look correct, and then select **OK** to save them:

   :::image type="content" source="./media/availability-group-clusterless-workgroup-configure/7-verify-cluster-properties.png" alt-text="Screenshot showing the cluster properties for verification.":::

## Create a cloud witness

In this step, configure a cloud share witness. If you're unfamiliar with the steps, see [Deploy a Cloud Witness for a Failover Cluster](/windows-server/failover-clustering/deploy-cloud-witness).

## Enable the availability group feature

In this step, enable the availability group feature. If you're unfamiliar with the steps, see the [availability group tutorial](availability-group-manually-configure-tutorial-single-subnet.md#enable-availability-groups).

## Create keys and certificates

In this step, create certificates that a SQL login uses on the encrypted endpoint. Create a folder on each node to hold the certificate backups, such as `c:\certs`.

> [!NOTE]  
> If you don't specify an expiration date for the certificate, it's valid for one year from the date of creation. If the certificate expires, the connection between endpoints will fail.

To configure the first node, follow these steps:

1. Open **SQL Server Management Studio** and connect to your first node, such as `AGNode1`.
1. Open a **New Query** window and run the following Transact-SQL (T-SQL) statement. Replace `<password>` with a strong password:

   ```sql
   USE master;
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
   GO

   --create a cert from the master key
   USE master;
   CREATE CERTIFICATE AGNode1Cert
      WITH SUBJECT = 'AGNode1 Certificate';
   GO

   --Backup the cert and transfer it to AGNode2
   BACKUP CERTIFICATE AGNode1Cert TO FILE = 'C:\certs\AGNode1Cert.crt';
   GO
   ```

1. Next, create the HADR endpoint, and use the certificate for authentication by running this Transact-SQL (T-SQL) statement:

   ```sql
   --CREATE or ALTER the mirroring endpoint
   CREATE ENDPOINT hadr_endpoint
      STATE = STARTED
      AS TCP (
         LISTENER_PORT=5022
         , LISTENER_IP = ALL
      )
      FOR DATABASE_MIRRORING (
         AUTHENTICATION = CERTIFICATE AGNode1Cert
         , ENCRYPTION = REQUIRED ALGORITHM AES
         , ROLE = ALL
      );
   GO
   ```

1. Use **File Explorer** to go to the file location where your certificate is, such as `c:\certs`.
1. Manually make a copy of the certificate, such as `AGNode1Cert.crt`, from the first node, and transfer it to the same location on the second node.

To configure the second node, follow these steps:

1. Connect to the second node with **SQL Server Management Studio**, such as `AGNode2`.
1. In a **New Query** window, run the following Transact-SQL (T-SQL) statement. Replace `<password>` with a strong password:

   ```sql
   USE master;
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
   GO

   --create a cert from the master key
   USE master;
   CREATE CERTIFICATE AGNode2Cert
      WITH SUBJECT = 'AGNode2 Certificate';
   GO
   --Backup the cert and transfer it to AGNode1
   BACKUP CERTIFICATE AGNode2Cert TO FILE = 'C:\certs\AGNode2Cert.crt';
   GO
   ```

1. Next, create the HADR endpoint, and use the certificate for authentication by running this Transact-SQL (T-SQL) statement:

   ```sql
   --CREATE or ALTER the mirroring endpoint
   CREATE ENDPOINT hadr_endpoint
      STATE = STARTED
      AS TCP (
         LISTENER_PORT=5022
         , LISTENER_IP = ALL
      )
      FOR DATABASE_MIRRORING (
         AUTHENTICATION = CERTIFICATE AGNode2Cert
         , ENCRYPTION = REQUIRED ALGORITHM AES
         , ROLE = ALL
      );
   GO
   ```

1. Use **File Explorer** to go to the file location where your certificate is, such as `c:\certs`.
1. Manually make a copy of the certificate, such as `AGNode2Cert.crt`, from the second node, and transfer it to the same location on the first node.

If there are any other nodes in the cluster, repeat these steps there also, modifying the respective certificate names.

## Create logins

Certificate authentication is used to synchronize data across nodes. To allow synchronization, create a login for the other node, create a user for the login, create a certificate for the login to use the backed-up certificate, and then grant connect on the mirroring endpoint.

To do so, first run the following Transact-SQL (T-SQL) query on the first node, such as `AGNode1`. Replace `<password>` with a strong password:

```sql
--create a login for the AGNode2
USE master;
CREATE LOGIN AGNode2_Login WITH PASSWORD = '<password>';
GO

--create a user from the login
CREATE USER AGNode2_User FOR LOGIN AGNode2_Login;
GO

--create a certificate that the login uses for authentication
CREATE CERTIFICATE AGNode2Cert
   AUTHORIZATION AGNode2_User
   FROM FILE = 'C:\certs\AGNode2Cert.crt'
GO

--grant connect for login
GRANT CONNECT ON ENDPOINT::hadr_endpoint TO [AGNode2_login];
GO
```

Next, run the following Transact-SQL (T-SQL) query on the second node, such as `AGNode2`. Replace `<password>` with a strong password:

```sql
--create a login for the AGNode1
USE master;
CREATE LOGIN AGNode1_Login WITH PASSWORD = '<password>';
GO

--create a user from the login
CREATE USER AGNode1_User FOR LOGIN AGNode1_Login;
GO

--create a certificate that the login uses for authentication
CREATE CERTIFICATE AGNode1Cert
   AUTHORIZATION AGNode1_User
   FROM FILE = 'C:\certs\AGNode1Cert.crt'
GO

--grant connect for login
GRANT CONNECT ON ENDPOINT::hadr_endpoint TO [AGNode1_login];
GO
```

If there are any other nodes in the cluster, repeat these steps there also, modifying the respective certificate and user names.

## Configure an availability group

In this step, configure your availability group, and add your databases to it. Don't create a listener at this time. If you're not familiar with the steps, see the [availability group tutorial](availability-group-manually-configure-tutorial-single-subnet.md#create-the-availability-group). To verify that everything is working correctly, be sure to initiate a failover and failback.

> [!NOTE]  
> If there's a failure during the synchronization process, you might need to temporarily grant the `NT AUTHORITY\SYSTEM` account **sysadmin** rights to create cluster resources on the first node, such as `AGNode1`.

## Configure a load balancer

In this final step, configure the load balancer by using either the [Azure portal](availability-group-load-balancer-portal-configure.md) or [PowerShell](availability-group-listener-powershell-configure.md).

However, there can be some [limitations](https://techcommunity.microsoft.com/t5/core-infrastructure-and-security/sql-server-workgroup-cluster-fcm-errors/ba-p/371387) when using the Windows Cluster GUI. As such, you should use PowerShell to create a client access point or the network name for your listener with the following example script:

```powershell
Add-ClusterResource -Name "IPAddress1" -ResourceType "IP Address" -Group "WGAG"
Get-ClusterResource -Name IPAddress1 | Set-ClusterParameter -Multiple @{"Network" = "Cluster Network 1";"Address" = "10.0.0.4";"SubnetMask" = "255.0.0.0";"EnableDHCP" = 0}
Add-ClusterResource -Name "IPAddress2" -ResourceType "IP Address" -Group "WGAG"
Get-ClusterResource -Name IPAddress2 | Set-ClusterParameter -Multiple @{"Network" = "Cluster Network 2";"Address" = "10.0.0.5";"SubnetMask" = "255.0.0.0";"EnableDHCP" = 0}
Add-ClusterResource -Name "TestName" -Group "WGAG" -ResourceType "Network Name"
Get-ClusterResource -Name "TestName" | Set-ClusterParameter -Multiple @{"DnsName" = "TestName";"RegisterAllProvidersIP" = 1}
Set-ClusterResourceDependency -Resource TestName -Dependency "[IPAddress1] or [IPAddress2]"
Start-ClusterResource -Name TestName -Verbose
```

## Configure probe port

[!INCLUDE [virtual-machines-port-exclusion](../../includes/virtual-machines-port-exclusion.md)]

## Related content

- [HADR configuration best practices (SQL Server on Azure VMs)](hadr-cluster-best-practices.md)
- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability group on SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
