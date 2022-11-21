---
title: Certificate Management (SQL Server Configuration Manager)
description: Learn how to install certificates in various SQL Server configurations. Examples include single instances, failover clusters, and Always On availability groups.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/12/2021"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "connections [SQL Server], encrypted"
  - "SSL [SQL Server]"
  - "Secure Sockets Layer (SSL)"
  - "encryption [SQL Server], connections"
  - "cryptography [SQL Server], connections"
  - "certificates [SQL Server], installing"
  - "requesting encrypted connections"
  - "installing certificates"
  - "security [SQL Server], encryption"
---

# Certificate Management (SQL Server Configuration Manager)

[!INCLUDE [sql-windows-only](../../includes/applies-to-version/sql-windows-only.md)]

This topic describes how to deploy and manage certificates across your SQL Server Always On Failover Cluster or Availability Group topology.

SSL/TLS certificates are widely used to secure access to SQL Server. With earlier versions of SQL Server, organizations with large SQL Server estates had to spend considerable effort to maintain their SQL Server certificate infrastructure, often through developing scripts and running manual commands. With SQL Server 2019, certificate management is integrated into the SQL Server Configuration Manager, simplifying common tasks such as: 

* Viewing and validating certificates installed in a SQL Server instance. 
* Identifying which certificates may be close to expiring. 
* Deploying certificates across Always On Availability Group machines from the node holding the primary replica. 
* Deploying certificates across machines participating in an Always On failover cluster instance from the active node.

> [!NOTE]
> You can use certificate management in SQL Server Configuration Manager with lower versions of SQL Server, starting with SQL Server 2008.

##  <a name="provision-single-server-cert"></a> To install a certificate for a single SQL Server instance  

::: moniker range=">=sql-server-ver15"
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.  

2. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.  

3. Choose the **Certificate** tab, and then select **Import**.  

4. Select **Browse** and then select the certificate file.  

5. Select **Next** to validate the certificate. If there are no errors, select **Next** to import the certificate to the local instance.  
::: moniker-end

::: moniker range="<= sql-server-2017"
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.  

2. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.  

3. Select a certificate from the  **Certificate** drop-down menu, and then select **Apply**.  

4. Select **OK**. 
::: moniker-end

##  <a name="provision-failover-cluster-cert"></a> To install a certificate in a failover cluster instance configuration  
  
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.
  
2. Right-click **Protocols for** *&lt;instance Name&gt;*, and then choose **Properties**. 

3. Choose the **Certificate** tab, and then select **Import**.

4. Select the certificate type, and whether to import for the current node only, or for each individual cluster node.

5. If installing for a single node, choose **Browse** and select certificate file. Then skip to step 8.

6. If installing a certificate for each node, select **Next** to list possible owner nodes. Possible owners for the current failover cluster instance are pre-selected.

7. Choose **Next** to select the certificate to be imported.

8. Enter the password when prompted. Look for any warnings or errors after validation.

9. Select **Next** to import the selected certificates.

> [!NOTE]
> Complete these steps in the active node of the Always On failover cluster instance. User must have administrator permissions on all the cluster nodes.

##  <a name="provision-availability-group-cert"></a>To install a certificate in an Always On Availability Group configuration  
  
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.
  
2. Right-click **Protocols for** *&lt;instance Name&gt;*, and then select **Properties**.  
  
3. Choose the **Certificate** tab, and then select **Import**.  
  
4. Choose the certificate type and select **Next** to select from the list of known Availability Groups.  

5. Select **Next** to choose certificates for each replica node. Certificates should have a file name that matches the netbios name of the nodes.

6. Select **Next** to import the certificate on each node.


> [!NOTE]
> Complete these steps from the node holding the Availability Group primary replica. User must have administrator permissions on all the cluster nodes.

