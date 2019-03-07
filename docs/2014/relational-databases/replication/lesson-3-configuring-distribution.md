---
title: "Lesson 3: Configuring Distribution | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: f248984a-0b59-4c2f-a56d-31f8dafe72b5
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 3: Configuring Distribution
  In this lesson, you will configure distribution at the Publisher and set the required permissions on the publication and distribution databases. If you have already configured the Distributor, you must first disable publishing and distribution before you begin this lesson. Do not do this if you must retain an existing replication topology.  
  
 Configuring a Publisher with a remote Distributor is outside the scope of this tutorial.  
  
### Configuring distribution at the Publisher  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder and click **Configure Distribution**.  
  
    > [!NOTE]  
    >  If you have connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using **localhost** rather than the actual server name you will be prompted with a warning that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to connect to server **'localhost'**. Click **OK** on the warning dialog. In the **Connect to Server** dialog change the **Server name** from **localhost** to the name of your server. Click **Connect**.  
  
     The Distribution Configuration Wizard launches.  
  
3.  On the **Distributor** page, select **'**_\<ServerName>_**' will act as its own Distributor; SQL Server will create a distribution database and log**, and then click **Next**.  
  
4.  If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running, on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**Agent Start** page, select **Yes**, configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically. Click **Next**.  
  
5.  Enter **\\\\**\<_Machine_Name>_**\repldata** in the **Snapshot folder** text box, where \<*Machine_Name>* is the name of the Publisher, and then click **Next**.  
  
6.  Accept the default values on the remaining pages of the wizard.  
  
7.  Click **Finish** to enable distribution.  
  
### Setting database permissions at the Publisher  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand **Security**, right-click **Logins**, and then select **New Login**.  
  
2.  On the **General** page, click **Search**, enter \<_Machine_Name>_**\repl_snapshot** in the **Enter the object name to select** box, where \<*Machine_Name>* is the name of the local Publisher server, click **Check Names**, and then click **OK**.  
  
3.  On the **User Mapping** page, in the **Users mapped to this login** list select both the **distribution** and [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] databases.  
  
     In the **Database role membership** list select the `db_owner` role for the login for both databases.  
  
4.  Click **OK** to create the login.  
  
5.  Repeat steps 1-4 to create a login for the local repl_logreader account. This login must also be mapped to users that are members of the `db_owner` fixed database role in the **distribution** and **AdventureWorks** databases.  
  
6.  Repeat steps 1-4 to create a login for the local repl_distribution account. This login must be mapped to a user that is a member of the `db_owner` fixed database role in the **distribution** database.  
  
7.  Repeat steps 1-4 to create a login for the local repl_merge account. This login must have user mappings in the **distribution** and **AdventureWorks** databases.  
  
## See Also  
 [Configure Distribution](configure-distribution.md)   
 [Replication Agent Security Model](security/replication-agent-security-model.md)  
  
  
