---
# required metadata

title: Configure SLES Cluster for SQL Server Availability Group | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 85180155-6726-4f42-ba57-200bf1e15f4d

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Configure SLES Cluster for SQL Server Availability Group

## Install and configure Pacemaker on each cluster node
 
[!INCLUDE [SLES-Configure-Pacemaker](../includes/ss-linux-cluster-pacemaker-configure.md)]

## Create a SQL Server login for Pacemaker

[!INCLUDE [SLES-Create-SQL-Login](../includes/ss-linux-cluster-pacemaker-create-login.md)]

## Save credentials

## Open Pacemaker firewall ports

## Install Pacemaker packages

## Set password for default user

## Enable ansd start pcsd service and Pacemaker

## Create the Cluster

## Disable STONITH

## Create AG resource

## Enable monitoring on master

## Create virtual IP resource

## Add colocation constraint

## Add ordering constraint

## Manual failover

## Next steps

[Create SQL Server Availability Group](sql-server-linux-availability-group-configure.md)

