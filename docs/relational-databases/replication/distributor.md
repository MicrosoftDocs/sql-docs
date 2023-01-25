---
description: "Distributor"
title: "Distributor | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.replicationutilities.selectdistributor.f1"
ms.assetid: 787f0e9c-09dd-438a-bc04-5b8f99c127b8
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Distributor
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Distributor** page appears in the Configure Distribution Wizard and in the New Publication Wizard. The Distributor is a server that contains the distribution database and stores metadata and history data for all types of replication. The Distributor also stores transactions for transactional replication. The Distributor can be the same server as the Publisher (a local Distributor) or it can be a separate server from the Publisher (a remote Distributor). The role of the Distributor varies, depending on which type of replication you implement. In general, its role is much greater for transactional replication than it is for merge replication and snapshot replication. Merge and snapshot replication typically use a local Distributor, but transactional replication on a very busy system can benefit from using a remote Distributor.  
  
 The Distributor uses these additional resources on the server where it is located:  
  
-   Additional disk space if the snapshot files for the publication are stored on it (which they typically are).  
  
-   Additional disk space to store the distribution database.  
  
-   Additional processor usage by replication agents for push subscriptions running on the Distributor.  
  
 The server you select as the Distributor should have adequate disk space and processor power to support replication and any other activities on that server.  
  
## Options  
 **'\<ServerName>' will act as its own Distributor; SQL Server will create a distribution database and log**  
 Select this option to configure the server you are connected to as a Distributor.  
  
 **Use the following server as the Distributor (Note: the server you select must already be configured as a Distributor)**  
 Select this option and then click on the name of a server below to configure another server as the Distributor.  
  
 **Add**  
 If the server you want to use as a Distributor is not listed, click **Add** to identify the server and add it to the list.  
  
> [!NOTE]  
>  To use a remote server as the Distributor, the remote server must already be configured as a Distributor. The server against which this wizard is running must be enabled as a Publisher on that Distributor.  
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)  
  
  
