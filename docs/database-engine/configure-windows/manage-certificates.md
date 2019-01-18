---
title: "Certificate Management (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "01/16/2019"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
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
ms.assetid: e1e55519-97ec-4404-81ef-881da3b42006
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Certificate Management (SQL Server Configuration Manager)

This topic describes how to deploy and manage certificates across your SQL Server Failover Cluster or Always On Availability Group topology.

SSL/TLS certificates are widely used to secure access to SQL Server. With earlier versions of SQL Server, organizations with large SQL Server estates had to expend considerable effort to maintain their SQL Server certificate infrastructure, often through developing scripts and running manual commands. With SQL Server 2019, certificate management is integrated into the SQL Server Configuration Manager, simplifying common tasks such as: 

* Viewing and validating certificates installed in a SQL Server instance. 
* Identifying which certificates may be close to expiration. 
* Deploying certificates across Always On Availability Group machines from the node holding the primary replica. 
* Deploying certificates across machines participating in a failover cluster instance from the active node.

> [!NOTE]
> You can use certificate management in SQL Server Configuration Manager with lower versions of SQL Server, starting with SQL Server 2008.

##  <a name="provision-single-server-cert"></a> To install a certificate for a single SQL Server instance  
  
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**.  
  
2. Right-click **Protocols for** *<instance Name>*, and then select **Properties**.  
  
3. Choose the **Certificate** tab, and then select **Import**.  
  
4. Select **Browse** and then select the certificate file.  
  
5. Select **Next** to validate the certificate. If there are no errors, select **Next** to import the certificate to the local instance.  
  
 
##  <a name="provision-failover-cluster-cert"></a> To install a certificate in a Failover Cluster configuration  
  
1. In SQL Server Configuration Manager, in the console pane, expand SQL Server Network Configuration”.
  
2. Right-click **Protocols for** *<instance Name>*, and then choose **Properties**. 

3. Choose the **Certificate** tab, and then select **Import**.

4. Select the certificate type, and whether to import for the current node only, or for each individual cluster node.

5. If installing for a single node, choose **Browse** and select certificate file. Then skip to step 8.

6. If installing a certificate for each node, choose **Next** to list possible owner nodes. Possible owners for the current SQL Server FCI are pre-selected.

7. Choose **Next** to select the certificate to be imported.
**
8. Enter the password when prompted. Look for any warnings or errors after validation.

9. Select **Next** to import the selected certificates.

> [!NOTE]
> Complete these steps in the active node of the SQL Server Failover Cluster instance. User must have administrator permissions on all the cluster nodes.

##  <a name="provision-always-on-cert"></a>To install a certificate in an Always On configuration  
  
1. In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**. 
  
2. Right-click **Protocols for** *<instance Name>*, and then select **Properties**.  
  
3. Choose the **Certificate** tab, and then select **Import**.  
  
4. Choose the certificate type and select **Next** to select from the list of known Availability Groups.  

5. Click **Next** to select certificates for each replica node. Certificates should have a file name that matches the netbios name of the nodes.

6. Click **Next** to import the certificate on each node.


> [!NOTE]
> Complete these steps from the node holding the Availability Group primary replica. User must have administrator permissions on all the cluster nodes.

