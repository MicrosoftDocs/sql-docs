---
title: Configure replay for Database Experimentation Assistant for SQL Server upgrades
description: Configure replay for Database Experimentation Assistant
ms.custom: ""
ms.date: 10/05/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: douglasl
manager: craigg
---

# Configure replay for Database Experimentation Assistant

Database Experimentation Assistant uses the Distributed Replay tools from the SQL Server installation to replay a captured trace against an upgraded test environment. We recommend doing a test run with a small trace file before doing a full replay to make sure proper replay of queries.

## Distributed Replay requirements

- An additional 78% hard drive space is required to create IRF files on the Distributed Replay controller machine.
- 200 MB or 512 MB is the ideal trace rollover size used for capturing production or performance traces.   
- The minimum CPU and RAM requirements for the Distributed Replay controller and client machines are a Single Core CPU with 3.5-GB RAM.
- The replay time takes approximately 1.55 times longer than the capture time, since one controller and four children are used to replay the production trace.
- If you use our "published" versions of production and performance trace definition files, and the performance trace definition filters out the traces for one database of interest, analysis has shown that the size of the Performance Trace is about 15 times larger than the Production Trace.

## Set up a virtual network or domain

Distributed Replay requires usage of common accounts between machines. For this and security reasons, we recommend running under a virtual network or a domain-controlled network.

- Create controller and client machines in the environment.
- Make sure that controller and client machines can ping each other over the network.
- Distributed Replay client machines must have connectivity to the replay target SQL server.

## Set up controller service
Follow these steps to set up the controller service:

1. Install the Distributed Replay controller using the SQL Server installer. If you didn't see the SQL Server Installer wizard step that configures the Distributed Replay controller, you can configure it through the configuration file. For a typical installation, it's located at `C:\Program Files (x86)\Microsoft SQL Server\<version>\Tools\DReplayController\DReplayController.config`
1. Distributed Replay controller logs are present at `C:\Program Files (x86)\Microsoft SQL Server\<version>\Tools\DReplayController\Log`.
1. Open `Services.msc` and navigate to the `"SQL Server Distributed Replay Controller"` service.
1. Right-click on the service, then select **Properties** to set the service account to an account that is common to the controller and client machines within the network.
1. Select **OK** to close the **Properties** window.
1. Restart the service `SQL Server Distributed Replay Controller` service from `Services.msc` or run the following commands from the command prompt:
   - `NET STOP "SQL Server Distributed Replay Controller"`
   - `NET START "SQL Server Distributed Replay Controller"`
1. For additional configuration options, see [SQL Server Distributed Replay](https://docs.microsoft.com/sql/tools/distributed-replay/configure-distributed-replay).

## DCOM Configuration

This configuration is required only on the controller machine.

1. Open `dcomcnfg.exe`.
1. Expand **Component Services** > **Computers** > **My Computer** > **DCOM Config**
1. Find DReplayController under DCOM Config. Right-click and open its Properties.
1. Choose the Security tab.
1. Under Launch and Activation Permissions, select **Customize** and choose **Edit**.
1. Add the user that will be starting the replay and give the user Local Launch and Local Activation permissions. If the user plans to launch or activate remotely, give the user Remote Launch and Remote Activation permissions.
1. Select **OK** to commit the changes and return to the Security tab on the **Properties** window.
1. Under Access Permissions, select **Customize** and choose **Edit**.
1. Add the user that will be starting the replay and give the user Local Access permission. If the user plans to access the controller service remotely, give the user Remote Access permission.
1. Choose **OK** to commit the changes and return to the Security tab on the **Properties** window.
1. Choose **OK** to commit the changes.
1. Restart the service `SQL Server Distributed Replay Controller` from `Services.msc` or run the following commands from the command prompt:
   - `NET STOP "SQL Server Distributed Replay Controller"`
   - `NET START "SQL Server Distributed Replay Controller"`

## Set up client service

Before setting up the client service, verify that the controller and client machines can see each other using networking tools such as ping.

1. Install the Distributed Replay client using the SQL Server installer.
1. Open Services.msc and navigate to the service `SQL Server Distributed Replay Client`.
1. Right-click on the service, select **Properties**, and set the service account to an account that is common to the controller and client machines within the network.
1. Choose **OK** to close the **Properties** window. If you didn't see the SQL Server Installer wizard step to configure the Distributed Replay client, you can configure it through the configuration file. For a typical installation, it can be found at `C:\Program Files (x86)\Microsoft SQL Server\<version>\Tools\DReplayClient\DReplayClient.config`
1. Ensure that the `DReplayClient.config` contains the name of the controller machine as its controller for registration.
7.  Restart the service `SQL Server Distributed Replay Client` from `Services.msc` or run the following commands from the command prompt:
    - `NET STOP "SQL Server Distributed Replay Client"`
    - `NET START "SQL Server Distributed Replay Client"`
1. Distributed Replay controller logs are present at `C:\Program Files (x86)\Microsoft SQL Server\<version>\Tools\DReplayClient\Log`. The log indicates whether the client is able to register itself with the controller.
1. If the configuration is successful, the log shows the message, "Registered with controller <controller name>".
1. For any additional configuration options, see [SQL Server Distributed Replay](https://docs.microsoft.com/sql/tools/distributed-replay/configure-distributed-replay).

## Set up Distributed Replay administration tools

Distributed Replay administration tools let you quickly test whether Distributed Replay is functioning properly in the environment, especially an environment in which multiple client machines are registered with a controller. SQL Server Management Studio might need to be installed to get the administration tool.

1. Go to the SSMS install location and look for the Distributed Replay administration tools: dreplay.exe and its dependent components.
1. Open a command prompt, and run `dreplay.exe status -f 1`
1. If all the previous steps are successful, the console output will indicate that the controller is able to see its clients in `READY` state.

## Configure firewall for remote Distributed Replay access

Remotely accessing Distributed Replay requires opening ports that are visible within the domain or virtual network.

1. Open the Windows Firewall with Advanced Security.
1. Navigate to Inbound Rules.
1. Create a new inbound firewall rule for program `C:\Program Files (x86)\Microsoft SQL Server\<version>\Tools\DReplayController\DReplayController.exe`.
1. Allow Domain level access to all ports for DReplayController.exe to be able to communicate with the controller service remotely.
1. Save the rule.

## Set up target SQL Servers

Two Replays are required for running an A/B test or an experiment. That is, you might need two separate instances of SQL Server installations for a migration scenario.

Installing the two versions of SQL Server instances on the same machine also works. One caveat though is to make sure that the instances are isolated completely when a replay is in progress.

The following steps must be performed for each replay.

1. Restore the backup of the database.
1. Provide permission to the client service account user to access the databases under the SQL Server instance. This is required for the queries to be executed on the SQL Server instance.
1. Start the replay.

## Next steps

- [Replay trace](database-experimentation-assistant-replay-trace.md) shows you how to replay a captured trace against an upgraded test environment.

- For a 19-minute introduction and demonstration of DEA, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
