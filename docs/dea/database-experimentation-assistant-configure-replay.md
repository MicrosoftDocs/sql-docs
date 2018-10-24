---
title: Configure replay in Database Experimentation Assistant for SQL Server upgrades
description: Configure replay in Database Experimentation Assistant
ms.custom: ""
ms.date: 10/22/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: ajaykar
ms.reviewer: douglasl
manager: craigg
---

# Configure replay in Database Experimentation Assistant

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
1. Distributed Replay controller logs are located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayController\Log.
1. Open Services.msc and go to the **SQL Server Distributed Replay Controller** service.
1. Right-click on the service, and then select **Properties**. Set the service account to an account that is common to the controller and client machines in the network.
1. Select **OK** to close the **Properties** window.
1. Restart the **SQL Server Distributed Replay Controller** service from Services.msc. You also can run the following commands at the command line to restart the service:<br/>
   `NET STOP "SQL Server Distributed Replay Controller"`<br/>
   `NET START "SQL Server Distributed Replay Controller"`
1. For more configuration options, see [Configure Distributed Replay](https://docs.microsoft.com/sql/tools/distributed-replay/configure-distributed-replay).

## Configure DCOM

This configuration is required only on the controller machine.

1. Open dcomcnfg.exe.
1. Expand **Component Services** > **Computers** > **My Computer** > **DCOM Config**.
1. Under **DCOM Config**, right-click **DReplayController**, and then select **Properties**.
1. Select the **Security** tab.
1. Under **Launch and Activation Permissions**, select **Customize**, and then select **Edit**.
1. Add the user that will be starting the replay. Give the user Local Launch and Local Activation permissions. If the user plans to launch or activate remotely, give the user Remote Launch and Remote Activation permissions.
1. Select **OK** to commit the changes and return to the **Security** tab.
1. Under **Access Permissions**, select **Customize**, and then select **Edit**.
1. Add the user that will be starting the replay. Give the user Local Access permissions. If the user plans to access the controller service remotely, give the user Remote Access permissions.
1. Select **OK** to commit the changes and return to the **Security** tab.
1. Select **OK** to commit the changes.
1. Restart the SQL Server Distributed Replay Controller service from Services.msc. You also can run the following commands at the command line to restart the service:<br/>
   `NET STOP "SQL Server Distributed Replay Controller"`<br/>
   `NET START "SQL Server Distributed Replay Controller"`

## Set up the client service

Before you set up the client service, use networking tools like ping to verify that the controller and client machines can communicate.

1. Install the Distributed Replay client by using the SQL Server installer.
1. Open Services.msc and go to the SQL Server Distributed Replay Client service.
1. Right-click on the service, and then select **Properties**. Set the service account to an account that is common to both the controller and client machines in the network.
1. Select **OK** to close the **Properties** window. If you skipped the SQL Server Installer wizard step to configure the Distributed Replay client, you can configure it through the configuration file. In a typical installation, the configuration file is located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayClient\DReplayClient.config.
1. Ensure that the DReplayClient.config file contains the name of the controller machine as its controller for registration.
1.  Restart the SQL Server Distributed Replay Client service from Services.msc. You also can run the following commands from the command line to restart the service:<br/>
    `NET STOP "SQL Server Distributed Replay Client"`<br/>
    `NET START "SQL Server Distributed Replay Client"`
1. Distributed Replay controller logs are located at C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayClient\Log. The logs indicate whether the client can register itself with the controller.
1. If the configuration is successful, the log displays the message "Registered with controller <controller name\>".
1. For more configuration options, see [Configure Distributed Replay](https://docs.microsoft.com/sql/tools/distributed-replay/configure-distributed-replay).

## Set up Distributed Replay administration tools

You can use Distributed Replay administration tools to quickly test whether Distributed Replay is functioning properly in the environment. Testing the configuration can be especially helpful in an environment in which multiple client machines are registered with a controller. You might need to install SQL Server Management Studio (SSMS) to get the administration tools.

1. Go to the SSMS install location and look for the Distributed Replay administration tool dreplay.exe and its dependent components.
1. Open a Command Prompt window and run `dreplay.exe status -f 1`.
1. If all the preceding steps are successful, the console output indicates that the controller can see its clients in a `READY` state.

## Configure the firewall for remote Distributed Replay access

Remotely accessing Distributed Replay requires opening ports that are visible within the domain or virtual network.

1. Open **Windows Firewall** with **Advanced Security**.
1. Go to **Inbound Rules**.
1. Create a new inbound firewall rule for program C:\Program Files (x86)\Microsoft SQL Server\<version\>\Tools\DReplayController\DReplayController.exe.
1. Allow domain-level access to all ports for DReplayController.exe to be able to communicate with the controller service remotely.
1. Save the rule.

## Set up target computers

Two replays are required to run an A/B test or an experiment. That is, you might need two separate instances of SQL Server installations for a migration scenario. 

You can also install the two versions of SQL Server instances on the same machine. A caveat is to make sure that the instances are completely isolated when a replay is in progress.

The following steps must be performed for each replay:

1. Restore the backup of the database.
1. Provide permissions for the client service account user to access the databases under the SQL Server instance. Permissions are required for the queries to be executed on the SQL Server instance.
1. Start the replay.

## Next steps

- To learn how to replay a captured trace in an upgraded test environment, see [Replay trace](database-experimentation-assistant-replay-trace.md).

- For a 19-minute introduction to DEA and demonstration, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
