---
title: "Create a domain-independent availability group"
description: "Learn the steps required to create an availability group that uses a workgroup cluster."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "Availability Groups [SQL Server], domain independent"
---
# Create a domain-independent availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Always On availability groups (AGs) require an underlying Windows Server Failover Cluster (WSFC). Deploying a WSFC through Windows Server 2012 R2 required that the servers participating in a WSFC, also known as nodes, were joined to the same domain. For more information on Active Directory Domain Services (AD DS), see [here](/previous-versions/windows/it-pro/windows-server-2003/cc759073(v=ws.10)).

The dependency on AD DS and WSFC is more complex than previously deployed with a database mirroring (DBM) configuration, since DBM can be deployed across multiple data centers using certificates, without any such dependencies. A traditional availability group spanning more than one data center requires that all servers must be joined to the same Active Directory domain. Different domains, even trusted domains, don't work. All the servers must be nodes of the same WSFC. The following figure shows this configuration. [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] and later versions also have distributed AGs, which achieve this goal in a different way.

:::image type="content" source="media/domain-independent-availability-groups/wsfc-two-data-centers-same-domain.png" alt-text="Diagram of WSFC spanning two data centers connected to the same domain.":::

Windows Server 2012 R2 introduced an [Active Directory-detached cluster](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/dn265970(v=ws.11)), a specialized form of a Windows Server failover cluster, which can be used with availability groups. This type of WSFC still requires the nodes to be domain-joined to the same Active Directory domain, but in this case the WSFC is using DNS, but not using the domain. Since a domain is still involved, an Active Directory-detached cluster still doesn't provide a domain-free experience.

Windows Server 2016 introduced a new type of Windows Server failover cluster based on the foundation of the Active Directory-detached cluster: a *workgroup cluster*. A workgroup cluster allows [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to deploy an availability group on top of a WSFC that doesn't require AD DS. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] requires the use of certificates for endpoint security, just as the database mirroring scenario requires certificates. This type of an availability group is called a domain independent availability group. Deploying an availability group with an underlying workgroup cluster supports the following combinations for the nodes that will make up the WSFC:

- No nodes are joined to a domain.
- All nodes are joined to different domains.
- Nodes are mixed, with a combination of domain-joined and non-domain-joined nodes.

The next figure shows an example of a domain independent availability group where the nodes in Data Center 1 are domain-joined but the ones in Data Center 2 only use DNS. In this case, set the DNS suffix on all servers that will be nodes of the WSFC. Every application and server accessing the availability group must see the same DNS information.

:::image type="content" source="media/domain-independent-availability-groups/workgroup-cluster-two-nodes-joined.png" alt-text="Diagram of workgroup cluster with two nodes that are joined to a domain.":::

A domain independent availability group isn't just for multi-site or disaster recovery scenarios. You can deploy it in a single data center, and even use it with a [basic availability group](basic-availability-groups-always-on-availability-groups.md). This configuration provides a similar architecture to what was achievable using database mirroring with certificates as shown.

:::image type="content" source="media/domain-independent-availability-groups/high-level-view-ag-standard-edition.png" alt-text="Diagram of High-level view of an AG in Standard Edition.":::

Deploying a domain independent availability group has some known caveats:

- The only witness types available for use with quorum are disk and [cloud](/windows-server/failover-clustering/deploy-cloud-witness), which is new in Windows Server 2016. Disk is problematic since there's most likely no use of shared disk by the availability group.

- The underlying workgroup cluster variant of a WSFC can only be created using PowerShell, but it can then be administered using the Failover Cluster Manager.

- If Kerberos is required, you must deploy a standard WSFC that is attached to an Active Directory domain, and a domain independent availability group is probably not an option.

- While a listener can be configured, it must be registered in DNS to be usable. As noted previously, there's no Kerberos support for the listener.

- Applications connecting to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] should primarily use [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] authentication since domains might not exist, or might not be configured to work together.

- Certificates are used in the configuration of the availability group.

## Set and verify the DNS suffix on all replica servers

A common DNS suffix is necessary for a domain independent availability group's workgroup cluster. To set and verify the DNS suffix on each Windows Server that will host a replica for the availability group, follow these instructions:

1. Using the Windows Key + X shortcut, select **System**.
1. If the computer name and the full computer name are the same, the DNS suffix isn't set. For example, if the computer name is `SERVER1`, the value for the full computer name shouldn't be just `SERVER1`. It should be something like `SERVER1.CONTOSO.LAB`. `CONTOSO.LAB` is the DNS suffix. The value for Workgroup should say `WORKGROUP`. If you need to set the DNS suffix, select **Change Settings**.
1. On the System Properties dialog, select **Change** on the Computer Name tab.
1. On the Computer Name/Domain Changes dialog, select **More**.
1. On the DNS Suffix and NetBIOS Computer Name dialog, enter the common DNS suffix as the **Primary DNS** suffix.
1. Select **OK** to close the DNS Suffix and NetBIOS Computer Name dialog.
1. Select **OK** to close the Computer Name/Domain Changes dialog.
1. You're prompted to restart the server for the changes to take effect. Select **OK** to close the Computer Name/Domain Changes dialog.
1. Select **Close** to close the System Properties dialog.
1. You're prompted to restart. If you don't want to restart immediately, select **Restart Later**, otherwise select **Restart Now**.
1. After the server restarts, verify that the common DNS suffix is configured by looking at System again.

:::image type="content" source="media/domain-independent-availability-groups/successful-dns-suffix.png" alt-text="Screenshot of successful configuration of DNS suffix.":::

  > [!NOTE]  
  > If you're using multiple subnets, and have a static DNS, you'll need to have a process in place to update the DNS record associated with the listener before you perform a failover as otherwise the network name will not come online.

## Create a domain independent availability group

Creating a domain independent availability group can't currently be achieved completely using SQL Server Management Studio. While creating the domain independent availability group is basically the same as the creation of a normal availability group, certain aspects (such as creating the certificates) are only possible with Transact-SQL. The following example assumes an availability group configuration with two replicas: one primary and one secondary.

1. Using the instructions from [Workgroup and Multi-domain clusters in Windows Server 2016](https://techcommunity.microsoft.com/t5/Failover-Clustering/Workgroup-and-Multi-domain-clusters-in-Windows-Server-2016/ba-p/372059), deploy a workgroup cluster composed of all servers that will participate in the availability group. Ensure that the common DNS suffix is already configured before configuring the workgroup cluster.

1. Enable or disable the [Always On availability group feature](enable-and-disable-always-on-availability-groups-sql-server.md) on each instance that will be participating in the availability group. This requires a restart of each [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance.

1. Each instance that hosts the primary replica requires a database master key (DMK). If a DMK doesn't exist already, run the following command:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Strong Password';
   GO
   ```

1. On the instance that will be the primary replica, create the certificate that will be used both for inbound connections on the secondary replicas and for securing the endpoint on the primary replica.

   ```sql
   CREATE CERTIFICATE InstanceA_Cert
   WITH SUBJECT = 'InstanceA Certificate';
   GO
   ```

1. Back up the certificate. You can also secure it further with a private key if desired. This example doesn't use a private key.

   ```sql
   BACKUP CERTIFICATE InstanceA_Cert
   TO FILE = 'Backup_path\InstanceA_Cert.cer';
   GO
   ```

1. Repeat Steps 4 and 5 to create and back up certificates for each secondary replica, using appropriate names for the certificates, such as `InstanceB_Cert`.

1. On the primary replica, you must create a login for each secondary replica of the availability group. This login is granted permissions to connect to the endpoint used by the domain independent availability group. For example, for a replica named `InstanceB`:

   ```sql
   CREATE LOGIN InstanceB_Login WITH PASSWORD = 'Strong Password';
   GO
   ```

1. On each secondary replica, create a login for the primary replica. This login is granted permissions to connect to the endpoint. For example, on a replica named `InstanceB`:

   ```sql
   CREATE LOGIN InstanceA_Login WITH PASSWORD = 'Strong Password';
   GO
   ```

1. On all instances, create a user for each login that was created. This user is used when restoring the certificates. For example, to create a user for the primary replica:

   ```sql
   CREATE USER InstanceA_User FOR LOGIN InstanceA_Login;
   GO
   ```

1. For any replica that might be a primary, create a login and user on all relevant secondary replicas.

1. On each instance, restore the certificates for the other instances that had a login and user created. On the primary replica, restore all secondary replica certificates. On each secondary, restore the certificate of the primary replica, and also on any other replica that could be a primary. For example:

   ```sql
   CREATE CERTIFICATE [InstanceB_Cert]
   AUTHORIZATION InstanceB_User
   FROM FILE = 'Restore_path\InstanceB_Cert.cer';
   ```

1. Create the endpoint that will be used by the availability group on each instance that will be a replica. For availability groups, the endpoint must have a type of `DATABASE_MIRRORING`. The endpoint uses the certificate created in Step 4 for that instance for authentication. Syntax is shown in the following example to create an endpoint using a certificate. Use the appropriate encryption method and other options relevant to your environment. For more information on the options available, see [CREATE ENDPOINT](../../../t-sql/statements/create-endpoint-transact-sql.md).

   ```sql
   CREATE ENDPOINT DIAG_EP STATE = STARTED AS TCP (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING (
       AUTHENTICATION = CERTIFICATE InstanceX_Cert, ROLE = ALL
   );
   ```

1. Assign rights to each login created on that instance in Step 8 to be able to connect to the endpoint.

   ```sql
   GRANT CONNECT ON ENDPOINT::DIAG_EP TO [InstanceX_Login];
   GO
   ```

1. Once the underlying certificates and endpoint security are configured, create the availability group using your preferred method. You should manually back up, copy, and restore the backup used to initialize the secondary, or use [automatic seeding](automatically-initialize-always-on-availability-group.md). Using the Wizard to initialize the secondary replicas involves the use of Server Message Block (SMB) files, which might not work when using a non-domain-joined workgroup cluster.

1. If creating a listener, make sure that both its name and its IP address are registered in DNS.

### Related content

- [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)
- [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
- [Create an Always On availability group using Transact-SQL (T-SQL)](create-an-availability-group-transact-sql.md)
