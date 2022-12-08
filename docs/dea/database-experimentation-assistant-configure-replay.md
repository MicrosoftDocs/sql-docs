---
title: Configure replay for SQL Server upgrades
description: Use Database Experimentation Assistant (DEA) to access the Distributed Replay tools. Use the tools to replay a captured trace against an upgraded test environment.
author: pochiraju
ms.author: rajpo
ms.reviewer: mathoma
ms.date: 01/24/2020
ms.service: sql
ms.subservice: dea
ms.topic: conceptual
ms.custom: seo-lt-2019
---

# Configure Distributed Replay for Database Experimentation Assistant

Database Experimentation Assistant (DEA) uses the Distributed Replay tools from the SQL Server installation to replay a captured trace against an upgraded test environment. We recommend doing a test run by using a small trace file before doing a full replay to ensure proper replay of queries.

## Distributed Replay requirements

- An additional 78% of hard-drive space is required to create IRF files on the Distributed Replay controller machine.
- 200 MB or 512 MB is the ideal trace rollover size to use to capture production or performance traces.
- The minimum CPU and RAM requirements for the Distributed Replay controller and client machines are a single-core CPU with 3.5 GB of RAM.
- The replay time takes approximately 1.55 times longer than the capture time because one controller and four child machines are used to replay the production trace.
- If you use our "published" versions of production and performance trace definition files and the performance trace definition filters out the traces for one database of interest, analysis shows that the **Performance Trace** size is about 15 times larger than the **Production Trace** size.

## Set up a virtual network or domain

Distributed Replay requires you to use common accounts between machines. Because of this requirement, and for security reasons, we recommend running Distributed Replay on a virtual network or on a domain-controlled network:

- Create the controller and client machines in the environment.
- Make sure that the controller and client machines can ping each other over the network.
- Distributed Replay client machines must have connectivity to the replay target computer running SQL Server.

## Set up the controller service

To set up the controller service:

1. Install the Distributed Replay controller by using the SQL Server installer. If you skipped the SQL Server Installer wizard step that configures the Distributed Replay controller, you can configure the controller through the configuration file. In a typical installation, the configuration file is located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayController\DReplayController.config.
2. Distributed Replay controller logs are located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayController\Log.
3. Open Services.msc and go to the **SQL Server Distributed Replay Controller** service.
4. Right-click on the service, and then select **Properties**. Set the service account to an account that is common to the controller and client machines in the network.
5. Select **OK** to close the **Properties** window.
6. Restart the **SQL Server Distributed Replay Controller** service from Services.msc. You also can run the following commands at the command line to restart the service:

   `NET STOP "SQL Server Distributed Replay Controller"`</br>
   `NET START "SQL Server Distributed Replay Controller"`

For more configuration options, see [Configure Distributed Replay](../tools/distributed-replay/configure-distributed-replay.md).

## Configure DCOM

This configuration is only required on the controller computer.

1. Open dcomcnfg.exe.
2. Expand **Component Services** > **Computers** > **My Computer** > **DCOM Config**.
3. Under **DCOM Config**, right-click **DReplayController**, and then select **Properties**.
4. Select the **Security** tab.
5. Under **Launch and Activation Permissions**, select **Customize**, and then select **Edit**.
6. Add the user that will be starting the replay. Give the user Local Launch and Local Activation permissions. If the user plans to launch or activate remotely, give the user Remote Launch and Remote Activation permissions.
7. Select **OK** to commit the changes and return to the **Security** tab.
8. Under **Access Permissions**, select **Customize**, and then select **Edit**.
9. Add the user that will be starting the replay. Give the user Local Access permissions. If the user plans to access the controller service remotely, give the user Remote Access permissions.
10. Select **OK** to commit the changes and return to the **Security** tab.
11. Select **OK** to commit the changes.
12. Restart the SQL Server Distributed Replay Controller service from Services.msc. You also can run the following commands at the command line to restart the service:

    `NET STOP "SQL Server Distributed Replay Controller"`</br>
    `NET START "SQL Server Distributed Replay Controller"`

## Set up the client service

Before you set up the client service, use networking tools like ping to verify that the controller and client computers can communicate.

1. Install the Distributed Replay client by using the SQL Server installer.
2. Open Services.msc and go to the SQL Server Distributed Replay Client service.
3. Right-click on the service, and then select **Properties**. Set the service account to an account that is common to both the controller and client machines in the network.
4. Select **OK** to close the **Properties** window. If you skipped the SQL Server Installer wizard step to configure the Distributed Replay client, you can configure it through the configuration file. In a typical installation, the configuration file is located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayClient\DReplayClient.config.
5. Ensure that the DReplayClient.config file contains the name of the controller machine as its controller for registration.
6. Restart the SQL Server Distributed Replay Client service from Services.msc. You also can run the following commands from the command line to restart the service:

    `NET STOP "SQL Server Distributed Replay Client"`</br>
    `NET START "SQL Server Distributed Replay Client"`

    Distributed Replay controller logs are located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayClient\Log. The logs indicate whether the client can register itself with the controller.

    If the configuration is successful, the log displays the message **Registered with controller <controller name\>**.

For more configuration options, see [Configure Distributed Replay](../tools/distributed-replay/configure-distributed-replay.md).

## Set up Distributed Replay administration tools

You can use Distributed Replay administration tools to quickly test whether Distributed Replay is functioning properly in the environment. Testing the configuration can be especially helpful in an environment in which multiple client machines are registered with a controller. You might need to install SQL Server Management Studio (SSMS) to get the administration tools.

1. Go to the SSMS install location and look for the Distributed Replay administration tool dreplay.exe and its dependent components. Currently, [SSMS 17](../ssms/release-notes-ssms.md#1791) is the latest release of SSMS to include dreplay.exe.
2. At a Command Prompt, run `dreplay.exe status -f 1`.

If the preceding steps were successful, the console output indicates that the controller can see its clients in a `READY` state.

## Configure the firewall for remote Distributed Replay access

Remotely accessing Distributed Replay requires opening ports that are visible within the domain or virtual network.

1. Open **Windows Firewall** with **Advanced Security**.
2. Go to **Inbound Rules**.
3. Create a new inbound firewall rule for program C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayController\DReplayController.exe.
4. Allow domain-level access to all ports for DReplayController.exe to be able to communicate with the controller service remotely.
5. Save the rule.

## Set up target computers

Two replays are required to run an A/B test or an experiment. That is, you might need two separate instances of SQL Server installations for a migration scenario.

You can also install the two versions of SQL Server instances on the same machine. A caveat is to make sure that the instances are isolated when a replay is in progress.

The following steps must be performed for each replay:

1. Restore the backup of the database.
2. Provide permissions for the client service account user to access the databases under the SQL Server instance. Permissions are required for the queries to be executed on the SQL Server instance.
3. Start the replay.

## See also

- To learn how to replay a captured trace in an upgraded test environment, see [Replay a trace in Database Experimentation Assistant](database-experimentation-assistant-replay-trace.md).
