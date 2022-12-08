---
title: "Change IP Address of a Failover Cluster Instance"
description: Learn how to change the IP address of a SQL Server failover cluster instance using the Failover Cluster Manager. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords: 
  - "modifying IP addresses"
  - "failover clustering [SQL Server], IP addresses"
  - "IP addresses [SQL Server]"
  - "clusters [SQL Server], IP addresses"
ms.assetid: b685f400-cbfe-4c5d-a070-227a1123dae4
author: MashaMSFT
ms.author: mathoma
---
# Change the IP Address of a Failover Cluster Instance
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to change the IP address resource in an Always On Failover Cluster Instance (FCI) by using the Failover Cluster Manager snap-in. The Failover Cluster Manager snap-in is the cluster management application for the Windows Server Failover Clustering (WSFC) service.  
  
-   **Before you begin:**  [Security](#Security)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Before you begin, review the following [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online topic: [Before Installing Failover Clustering](../../../sql-server/failover-clusters/install/before-installing-failover-clustering.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To maintain or update an FCI, you must be a local administrator with permission to logon as a service on all nodes of the FCI.  
  
##  <a name="WSFC"></a> Using the Failover Cluster Manager Snap-in  
 **To change the IP address resource for an FCI**  
  
1.  Open the Failover Cluster Manager snap-in.  
  
2.  Expand the **Services and applications** node, in the left pane and click on the FCI.  
  
3.  On the right pane, under the **Server Name** category, right-click the SQL Server Instance, and select **Properties** option to open the **Properties** dialog box.  
  
4.  On the **General** tab, change the IP address resource.  
  
5.  Click **OK** to close the dialog box.  
  
6.  In the right-hand pane, right-click the SQL IP Address1(failover cluster instance name) and select **Take Offline**. You will see the SQL IP Address1(failover cluster instance name), SQL Network Name(failover cluster instance name), and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] status change from Online to Offline Pending, and then to Offline.  
  
7.  In the right-hand pane, right-click [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and then select **Bring Online**. You will see the SQL IP Address1(failover cluster instance name), SQL Network Name(failover cluster instance name), and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] status change from Offline to Online Pending, and then to Online.  
  
8.  Close the Failover Cluster Manager snap-in.  
  
  
