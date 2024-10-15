---
title: "Tutorial: Prepare for replication"
description: In this tutorial, learn how to prepare your publisher, distributor, and subscriber for replication by creating Windows accounts, preparing the snapshot folder, and configuring distribution.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 10/11/2024
ms.service: sql
ms.subservice: replication
ms.topic: tutorial
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "replication [SQL Server], tutorials"
---
# Tutorial: Prepare SQL Server for replication (publisher, distributor, subscriber)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

It's important to plan for security before you configure your replication topology. This tutorial shows you how to better secure a replication topology. It also shows you how to configure distribution, which is the first step in replicating data. You must complete this tutorial before any of the others.

> [!NOTE]  
> To replicate data securely between servers, you should implement all of the recommendations in [Replication security best practices](security/replication-security-best-practices.md).

This tutorial teaches you to prepare a server so that replication can run securely with least privileges.

In this tutorial, you learn how to:
> [!div class="checklist"]
> - Create Windows accounts for replication.
> - Prepare the snapshot folder.
> - Configure distribution.

## Prerequisites

This tutorial is for users who are familiar with fundamental database operations, but who have limited exposure to replication.

To complete this tutorial, you need SQL Server, SQL Server Management Studio (SSMS), and an AdventureWorks database:

- At the publisher server (source), install:

  - Any edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], except SQL Server Express or SQL Server Compact. These editions can't be replication publishers.

  - The [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database. To enhance security, the sample databases aren't installed by default.

- At the subscriber server (destination), install any edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], except [!INCLUDE [ssEW](../../includes/ssew-md.md)]. [!INCLUDE [ssEW](../../includes/ssew-md.md)] can't be a subscriber in transactional replication.

- Install [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md).
- Install [SQL Server Developer edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download the [AdventureWorks sample database](https://github.com/Microsoft/sql-server-samples/releases). For instructions on restoring a database in SSMS, see [Restore a Database Backup Using SSMS](../backup-restore/restore-a-database-backup-using-ssms.md).

> [!NOTE]  
> - Replication isn't supported on SQL Server instances that are more than two versions apart. For more information, see [Replication backward compatibility](replication-backward-compatibility.md).
>
> - In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must connect to the publisher and subscriber by using a login that is a member of the **sysadmin** fixed server role. For more information on this role, see [Server-level roles](../security/authentication-access/server-level-roles.md).

**Estimated time to complete this tutorial: 30 minutes**

## Create Windows accounts for replication

In this section, you create Windows accounts to run replication agents. You create a separate Windows account on the local server for the following agents:

| Agent | Location | Account name |
| --- | --- | --- |
| Snapshot Agent | Publisher | <*machine_name*>\repl_snapshot |
| Log Reader Agent | Publisher | <*machine_name*>\repl_logreader |
| Distribution Agent | Publisher and subscriber | <*machine_name*>\repl_distribution |
| Merge Agent | Publisher and subscriber | <*machine_name*>\repl_merge |

> [!NOTE]  
> In the replication tutorials, the publisher and distributor share the same instance (NODE1\SQL2016) of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The subscriber instance (NODE2\SQL2016) is remote. The publisher and subscriber might share the same instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], but it isn't a requirement. If the publisher and subscriber share the same instance, the steps that are used to create accounts at the subscriber aren't required.

### Create local Windows accounts for replication agents at the publisher

1. At the publisher, open **Computer Management** from **Administrative Tools** in Control Panel.

1. In **System Tools**, expand **Local Users and Groups**.

1. Right-click **Users** and then select **New User**.

1. Enter **repl_snapshot** in the **User name** box, provide the password and other relevant information, and then select **Create** to create the repl_snapshot account:

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/newuser.png" alt-text="Screenshot of 'New User' dialog box." lightbox="media/tutorial-preparing-the-server-for-replication/newuser.png":::

1. Repeat the previous step to create the repl_logreader, repl_distribution, and repl_merge accounts:

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/replusers.png" alt-text="Screenshot of list of replication users.":::

1. Select **Close**.

### Create local Windows accounts for replication agents at the subscriber

1. At the subscriber, open **Computer Management** from **Administrative Tools** in Control Panel.

1. In **System Tools**, expand **Local Users and Groups**.

1. Right-click **Users** and then select **New User**.

1. Enter **repl_distribution** in the **User name** box, provide the password and other relevant information, and then select **Create** to create the repl_distribution account.

1. Repeat the previous step to create the repl_merge account.

1. Select **Close**.

For more information, see [Replication Agents overview](agents/replication-agents-overview.md).

## Prepare the snapshot folder

In this section, you configure the snapshot folder that's used to create and store the publication snapshot.

### Create a share for the snapshot folder and assign permissions

1. In File Explorer, browse to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data folder. The default location is C:\Program Files\Microsoft SQL Server\MSSQL.X\MSSQL\Data.

1. Create a new folder named **repldata**.

1. Right-click this folder and select **Properties**.

   1. On the **Sharing** tab in the **repldata Properties** dialog box, select **Advanced Sharing**.

   1. In the **Advanced Sharing** dialog box, select **Share this Folder**, and then select **Permissions**.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/repldata.png" alt-text="Screenshot of selections for sharing the repldata folder." lightbox="media/tutorial-preparing-the-server-for-replication/repldata.png":::

1. In the **Permissions for repldata** dialog box, select **Add**. In the **Select User, Computers, Service Account, or Groups** box, type the name of the Snapshot Agent account that you created previously, as <*Publisher_Machine_Name*>**\repl_snapshot**. Select **Check Names**, and then select **OK**.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/addshareperms.png" alt-text="Screenshot of selections to add sharing permissions.":::

1. Repeat step 6 to add the other two accounts that you created previously: <*Publisher_Machine_Name*>**\repl_merge** and <*Publisher_Machine_Name*>**\repl_distribution**.

1. After you add the three accounts, assign the following permissions:

   - repl_distribution: **Read**
   - repl_merge: **Read**
   - repl_snapshot: **Full Control**

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/sharedpermissions.png" alt-text="Screenshot of shared permissions for each account." lightbox="media/tutorial-preparing-the-server-for-replication/sharedpermissions.png":::

1. After your share permissions are configured correctly, select **OK** to close the **Permissions for repldata** dialog box. Select **OK** to close the **Advanced Sharing** dialog box.

1. In the **repldata Properties** dialog box, select the **Security** tab and select **Edit**:

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/editsecurity.png" alt-text="Screenshot of 'Edit' button on the 'Security' tab.":::

1. In the **Permissions for repldata** dialog box, select **Add**. In the **Select Users, Computers, Service Accounts, or Groups** box, type the name of the Snapshot Agent account that you created previously, as <*Publisher_Machine_Name*>**\repl_snapshot**. Select **Check Names**, and then select **OK**.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/addsecuritypermissions.png" alt-text="Screenshot of selections to add security permissions." lightbox="media/tutorial-preparing-the-server-for-replication/addsecuritypermissions.png":::

1. Repeat the previous step to add permissions for the Distribution Agent as <*Publisher_Machine_Name*>**\repl_distribution**, and for the Merge Agent as <*Publisher_Machine_Name*>**\repl_merge**.

1. Verify that the following permissions are allowed:

   - repl_distribution: **Read**
   - repl_merge: **Read**
   - repl_snapshot: **Full Control**

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/replpermissions.png" alt-text="Screenshot of User permissions for replication data." lightbox="media/tutorial-preparing-the-server-for-replication/replpermissions.png":::

1. Select the **Sharing** tab again and note the **Network Path** for the share. You need this path later when you're configuring your snapshot folder.

   :::image type="content" source="media/tutorial-replicating-data-between-continuously-connected-servers/networkpath.png" alt-text="Screenshot of network path on the 'Sharing' tab.":::

1. Select **OK** to close the **repldata Properties** dialog box.

For more information, see [Secure the Snapshot Folder](security/secure-the-snapshot-folder.md).

## Configure distribution

In this section, you configure distribution at the publisher and set the required permissions on the publication and distribution databases. If you already configured the distributor, you must disable publishing and distribution before you begin this section. Don't disable publishing and distribution if you must keep an existing replication topology, especially in production.

Configuring a publisher with a remote distributor is outside the scope of this tutorial.

### Configure distribution at the publisher

1. Connect to the publisher in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.

1. Right-click the **Replication** folder and select **Configure Distribution**:

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/configuredistribution.png" alt-text="Screenshot of 'Configure Distribution' command on the shortcut menu.":::

   - If you connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using **localhost** rather than the actual server name, you're prompted with a warning that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't connect to **localhost or IP Address**. Select **OK** in the warning dialog box. In the **Connect to Server** dialog box, change **Server name** from **localhost or IP Address** to the name of your server. Then select **Connect**.

   - There's currently a known issue with SQL Server Management Studio (SSMS) 18.0 (and later) where a warning message *isn't* displayed when connecting to the Distributor with the IP address, but is still invalid. The actual server name should be used when connecting to the Distributor.

   [!INCLUDE [custom-port](../system-stored-procedures/includes/custom-port.md)]

   The Distribution Configuration Wizard starts.

1. On the **Distributor** page, select <*'ServerName'*> **will act as its own Distributor; SQL Server will create a distribution database and log**. Then select **Next**.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/serverdistributor.png" alt-text="Screenshot of option to make the server act as its own distributor.":::

1. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent isn't running, on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **Agent Start** page, select **Yes, configure the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically**. Select **Next**.

1. Enter the path \\\\<*Publisher_Machine_Name*>**\repldata** in the **Snapshot folder** box, and then select **Next**. This path should match what you saw previously under **Network Path** for your repldata properties folder after configuring your share properties.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/repldatasnapshot.png" alt-text="Screenshot of comparison of network paths in the 'repldata Properties' dialog box and in the Configure Distribution Wizard." lightbox="media/tutorial-preparing-the-server-for-replication/repldatasnapshot.png":::

1. Accept the default values on the remaining pages of the wizard.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/distributionwizarddefaults.png" alt-text="Screenshot of last page of the wizard.":::

1. Select **Finish** to enable distribution.

You might see the following error when configuring the distributor. It's an indication that the account that was used to start the SQL Server Agent account isn't an administrator on the system. You either need to start the SQL Server Agent manually, grant those permissions to the existing account, or modify which account the SQL Server Agent is using.

:::image type="content" source="media/tutorial-preparing-the-server-for-replication/startingagenterror.png" alt-text="Screenshot of error message for configuring the SQL Server Agent.":::

If your SQL Server Management Studio instance is running with administrative rights, you can start the SQL Agent manually from within SSMS:

:::image type="content" source="media/tutorial-preparing-the-server-for-replication/ssmsstartagent.png" alt-text="Screenshot of selecting 'Start' on the shortcut menu for the agent in SSMS.":::

> [!NOTE]  
> If the SQL Agent doesn't visibly start, right-click the SQL Server Agent in SSMS and select **Refresh**. If it's still in the stopped state, start it manually from SQL Server Configuration Manager.

## Set database permissions

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand **Security**, right-click **Logins**, and then select **New Login**:

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/newlogin.png" alt-text="Screenshot of 'New Login' command on the shortcut menu.":::

1. On the **General** page, select **Search**. Enter <*Publisher_Machine_Name*>**\repl_snapshot** in the **Enter the object name to select** box, select **Check Names**, and then select **OK**.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/addsnapshotlogin.png" alt-text="Screenshot of selections for entering the object name.":::

1. On the **User Mapping** page, in the **Users mapped to this login** list, select both the **distribution** and [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] databases.

   In the database role membership list, select the **db_owner** role for the login for both databases.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/dbowner.png" alt-text="Screenshot of selecting the databases and their role.":::

1. Select **OK** to create the login.

1. Repeat steps 1-4 to create a login for the other local accounts (repl_distribution, repl_logreader, and repl_merge). These logins must also be mapped to users who are members of the **db_owner** fixed database role in the **distribution** and **AdventureWorks** databases.

   :::image type="content" source="media/tutorial-preparing-the-server-for-replication/usersinssms.png" alt-text="Screenshot of all four accounts in Object Explorer.":::

For more information, see [Configure distribution](configure-distribution.md) and [Replication Agent security model](security/replication-agent-security-model.md).

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Configure replication between two fully connected servers (transactional)](tutorial-replicating-data-between-continuously-connected-servers.md)
