---
title: "Create a domain-independent availability group"
description: "Steps to create an availability group that uses a Workgroup Cluster. This allows SQL Server 2016 (and greater) to deploy an Always On availability group on top of a WSFC that does not require Active Directory Domain Services and therefore does not require each server to be part of the same domain."
author: MashaMSFT
ms.author: mathoma
ms.date: "09/25/2017"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], domain independent"
---
# Create a domain-independent availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Always On availability groups (AGs) require an underlying Windows Server failover cluster (WSFC). Deploying a WSFC through Windows Server 2012 R2 has always required that the servers participating in a WSFC, also known as nodes, are joined to the same domain. For more information on Active Directory Domain Services (AD DS), see [here](/previous-versions/windows/it-pro/windows-server-2003/cc759073(v=ws.10)).

The AD DS and WSFC dependency is more complex than what has been previously deployed with a Database Mirroring (DBM) configuration, since DBM can be deployed across multiple data centers using certificates, without any such dependencies.  A traditional availability group spanning more than one data center requires that all servers must be joined to the same Active Directory domain -- different domains, even trusted domains, do not work. All the servers must be nodes of the same WSFC. The following figure shows this configuration. SQL Server 2016 also has distributed availability groups which can also achieve this goal in a different way.


![WSFC spanning two data centers connected to the same domain][1]

Windows Server 2012 R2 introduced an [Active Directory-Detached Cluster](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/dn265970(v=ws.11)), a specialized form of a Windows Server failover cluster which can be used with availability groups. This type of WSFC still requires the nodes to be domain-joined to the same Active Directory domain, but in this case the WSFC is using DNS, but not using the domain. Since a domain is still involved, an Active Directory-Detached Cluster still does not provide a completely domain-free experience.

Windows Server 2016 introduced a new kind of Windows Server failover cluster based on the foundation of the Active Directory-Detached Cluster -- a Workgroup Cluster. A Workgroup Cluster allows SQL Server 2016 to deploy an availability group on top of a WSFC that does not require AD DS. SQL Server requires the use of certificates for endpoint security, just as the database mirroring scenario requires certificates.  This type of an availability group is called a Domain Independent Availability Group. Deploying an availability group with an underlying Workgroup Cluster supports the following combinations for the nodes that will make up the WSFC:
- No nodes are joined to a domain.
- All nodes are joined to different domains.
- Nodes are mixed, with a combination of domain-joined and non-domain-joined nodes.

The next figure shows an example of a Domain Independent Availability Group where the nodes in Data Center 1 are domain-joined but the ones in Data Center 2 only use DNS. In this case, set the DNS suffix on all servers that will be nodes of the WSFC. Every application and server accessing the availability group must see the same DNS information.


![Workgroup Cluster with two nodes that are joined to a domain][2]

A Domain Independent Availability Group is not just for multi-site or disaster recovery scenarios. It can be deployed in a single data center and even used with a [Basic Availability Group](basic-availability-groups-always-on-availability-groups.md) (also known as a Standard Edition availability group) to provide a similar architecture to what used to be achieved using Database Mirroring with certificates as shown.


![High-level view of an AG in Standard Edition][3]

Deploying a Domain Independent Availability Group has some known caveats:
- The only witness types available for use with quorum are disk and [cloud](/windows-server/failover-clustering/deploy-cloud-witness), which is new in Windows Server 2016. Disk is problematic since there is most likely no use of shared disk by the availability group.
- The underlying Workgroup Cluster variant of a WSFC can only be created using PowerShell, but it can then be administered using the Failover Cluster Manager.
- If Kerberos is required, you must deploy a standard WSFC that is attached to an Active Directory domain, and a Domain Independent Availability Group is probably not an option.
- While a listener can be configured, it must be registered in DNS to be usable. As noted above, there is no Kerberos support for the listener.
- Applications connecting to SQL Server should primarily use SQL Server authentication since domains may not exist, or may not be configured to work together. 
- Certificates will be used in the configuration of the availability group.

## Set and verify the DNS suffix on all replica servers

A common DNS suffix is necessary for a Domain Independent Availability Group's Workgroup Cluster. To set and verify the DNS suffix on each Windows Server that will host a replica for the availability group, follow these instructions:

1. Using the Windows Key + X shortcut, select System.
2. If the computer name and the full computer name are the same, the DNS suffix has not been set. For example, if the computer name is ALLAN, the value for the full computer name should not be just ALLAN. It should be something like ALLAN.SQLHA.LAB. SQLHA.LAB is the DNS suffix. The value for Workgroup should say WORKGROUP. If you need to set the DNS suffix, select Change Settings.
3. On the System Properties dialog, click Change on the Computer Name tab.
4. On the Computer Name/Domain Changes dialog, click More.
5. On the DNS Suffix and NetBIOS Computer Name dialog, enter the common DNS suffix as the Primary DNS suffix. 
6. Click OK to close the DNS Suffix and NetBIOS Computer Name dialog.
7. Click OK to close the Computer Name/Domain Changes dialog.
8. You will be prompted to restart the server for the changes to take effect. Click OK to close the Computer Name/Domain Changes dialog.
9. Click Close to close the System Properties dialog.
10. You will be prompted to restart. If you do not want to restart immediately, click Restart Later, otherwise click Restart Now.
11. After the server has rebooted, verify that the common DNS suffix is configured by looking at System again.

![Successful configuration of DNS suffix][4]

  > [!NOTE]
  > If you are using multiple subnets, and have a static DNS, you will need to have a process in place to update the DNS record associated with the listener before you perform a failover as otherwise the network name will not come online.

## Create a Domain Independent Availability Group

Creating a Domain Independent Availability Group cannot currently be achieved completely using SQL Server Management Studio. While creating the Domain Independent Availability Group is basically the same as the creation of a normal availability group, certain aspects (such as creating the certificates) are only possible with Transact-SQL. The example below assumes an availability group configuration with two replicas: one primary and one secondary. 

1. [Using the instructions at this link](https://techcommunity.microsoft.com/t5/Failover-Clustering/Workgroup-and-Multi-domain-clusters-in-Windows-Server-2016/ba-p/372059), deploy a Workgroup Cluster composed of all servers that will participate in the availability group. Ensure that the common DNS suffix is already configured before configuring the Workgroup Cluster.
2. [Enable the Always On Availability Groups feature](./enable-and-disable-always-on-availability-groups-sql-server.md) on each instance that will be participating in the availability group. This will require a restart of each SQL Server instance.
3. Each instance that will host the primary replica requires a database master key. If a master key does not exist already, run the following command:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Strong Password';
   GO
   ```

4. On the instance that will be the primary replica, create the certificate that will be used both for inbound connections on the secondary replicas and for securing the endpoint on the primary replica.

   ```sql
   CREATE CERTIFICATE InstanceA_Cert 
   WITH SUBJECT = 'InstanceA Certificate';
   GO
   ``` 

5. Back up the certificate. You can also secure it further with a private key if desired. This example does not use a private key.

   ```sql
   BACKUP CERTIFICATE InstanceA_Cert 
   TO FILE = 'Backup_path\InstanceA_Cert.cer';
   GO
   ```

6. Repeat Steps 4 and 5 to create and back up certificates for each secondary replica, using appropriate names for the certificates, such as InstanceB_Cert.
7. On the primary replica, you must create a login for each secondary replica of the availability group. This login will be granted permissions to connect to the endpoint used by the Domain Independent Availability Group. For example, for a replica named InstanceB:

   ```sql
   CREATE LOGIN InstanceB_Login WITH PASSWORD = 'Strong Password';
   GO
   ```

8. On each secondary replica, create a login for the primary replica. This login will be granted permissions to connect to the endpoint. For example, on a replica named InstanceB:

   ```sql
   CREATE LOGIN InstanceA_Login WITH PASSWORD = 'Strong Password';
   GO
   ```

9. On all instances, create a user for each login that was created. This will be used when restoring the certificates. For example, to create a user for the primary replica:

   ```sql
   CREATE USER InstanceA_User FOR LOGIN InstanceA_Login;
   GO
   ```

10. For any replica that may be a primary, create a login and user on all relevant secondary replicas.
11. On each instance, restore the certificates for the other instances that had a login and user created. On the primary replica, restore all secondary replica certificates. On each secondary, restore the certificate of the primary replica, and also on any other replica that could be a primary. For example:

   ```sql
   CREATE CERTIFICATE [InstanceB_Cert]
   AUTHORIZATION InstanceB_User
   FROM FILE = 'Restore_path\InstanceB_Cert.cer'
   ```

12. Create the endpoint that will be used by the availability group on each instance that will be a replica. For availability groups, the endpoint must have a type of DATABASE_MIRRORING. The endpoint uses the certificate created in Step 4 for that instance for authentication. Example syntax is shown below to create an endpoint using a certificate. Use the appropriate encryption method and other options relevant to your environment. For more information on the options available see [CREATE ENDPOINT (Transact-SQL)](../../../t-sql/statements/create-endpoint-transact-sql.md).

   ```sql
   CREATE ENDPOINT DIAG_EP
   STATE = STARTED
   AS TCP (   
	LISTENER_PORT = 5022,
	LISTENER_IP = ALL
         )
   FOR DATABASE_MIRRORING (
	AUTHENTICATION = CERTIFICATE InstanceX_Cert,
	ROLE = ALL
         )
   ```

13. Assign rights to each login created on that instance in Step 8 to be able to connect to the endpoint. 

   ```sql
   GRANT CONNECT ON ENDPOINT::DIAG_EP TO [InstanceX_Login];
   GO
   ```

14. Once the underlying certificates and endpoint security are configured, create the availability group using your preferred method. It is recommended to manually back up, copy, and restore the backup used to initialize the secondary, or use [automatic seeding](automatically-initialize-always-on-availability-group.md). Using the Wizard to initialize the secondary replicas involves the use of Server Message Block (SMB) files, which may not work when using a non-domain-joined Workgroup Cluster.
15. If creating a listener, make sure that both its name and its IP address are registered in DNS.

### Next steps 

- [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)

- [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
 
- [Create an availability group with Transact-SQL](create-an-availability-group-transact-sql.md)

<!--Image references-->
[1]: ./media/diag-wsfc-two-data-centers-same-domain.png
[2]: ./media/diag-workgroup-cluster-two-nodes-joined.png
[3]: ./media/diag-high-level-view-ag-standard-edition.png
[4]: ./media/diag-successful-dns-suffix.png
