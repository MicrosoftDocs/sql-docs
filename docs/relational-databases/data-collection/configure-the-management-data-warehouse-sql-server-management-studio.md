---
title: "Configure the management data warehouse (SSMS)"
description: Learn how to configure the management data warehouse to support data storage on one or more instances of SQL Server that are using the data collector.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.dc.datacollection.wizard_completecfg.f1"
  - "sql13.swb.dc.datacollection.wizard_config.f1"
  - "sql13.swb.dc.datacollection.wizard_finish.f1"
  - "sql13.swb.dc.datacollection.wizard_maploginuser.f1"
  - "sql13.swb.dc.datacollection.wizard_choosemdw.f1"
  - "sql13.swb.dc.datacollection.wizard_welcome.f1"
  - "sql13.swb.dc.datacollection.wizard_createmdw.f1"
helpviewer_keywords:
  - "data warehouse [SQL Server], multiple instances"
  - "data warehouse [SQL Server], configuring"
  - "Configure Management Data Warehouse Wizard"
  - "management data warehouse, configuring"
---
# Configure the management data warehouse (SQL Server Management Studio)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the management data warehouse to support data storage on a single instance, or multiple instances, of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that use the data collector. These instances can be on the same server or on different servers. This article also provides descriptions of the user interface for the [Configure Data Management Warehouse Wizard](#Wizard) dialog box. For information about configuring a data collector, see [Configure properties of a data collector](configure-properties-of-a-data-collector.md).

> [!NOTE]  
> If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent is configured to run using one of the System service accounts (Local System, Network Service, or Local Service), and the management data warehouse is created on a different instance from the data collector, you must configure collection sets to use a proxy for uploading data to the management data warehouse.

### Configure the management data warehouse on a single instance or multiple instances of SQL Server

1. Ensure that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running.

1. In Object Explorer, expand the **Management** node.

1. Right-click **Data Collection**, expand **Tasks**, and then select **Configure Management Data Warehouse**.

1. Use the [Configure Management Data Warehouse Wizard](#Wizard) to create a management data warehouse, configure logins, enable data collection, and start the **System Data Collection Sets**.

     To configure multiple instances, continue with step 5.

    > [!NOTE]  
    > It's considered best practice to use proxies in deployments where the data collector is installed on multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are using the same management data warehouse.

1. Open [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on another instance and do either of the following actions:

    - Use the Configure Management Data Warehouse wizard to configure data collection for the existing management data warehouse.

    - Right-click **Data Collection**, and then select **Properties**. On the **General** tab, specify the existing management data warehouse and the server it's installed on.

1. Repeat step 5 until all the database instances that use the data collector are configured to upload data to the shared management data warehouse.

#### <a id="Wizard"></a> Configure Management Data Warehouse Wizard

**Welcome page**

The Welcome page is the starting page of the Configure Data Collection Wizard. Displaying this page is optional.

**Don't show this starting page again.**

Select to suppress this page the next time you launch the Configure Data Collection Wizard.

**Configure Management Data Warehouse Storage page**

Use this page to select a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database server and management data warehouse. The management data warehouse is a relational database that will store collected data.

> [!NOTE]  
> You must have the appropriate level of permissions in order to create the management data warehouse on the server. For more information, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md). You also must have the appropriate level of permissions to create logins for management data warehouse roles.

**Server name**

Specifies the name of the server that will host the management data warehouse.

When configuring a management data warehouse, **Server name** is always the name of the local server and can't be modified.

**Database name**

Specifies the relational database that will store collected data. Use the list to select an existing database or select **New** to create a new database using the **New Database** dialog.

The **New** option is available only when configuring a data collection set.

**Map Logins and Users page**

Use this page to map logins to database user roles for the management data warehouse.

**Users mapped to this login**

Displays the existing logins on the server that will host the management data warehouse. Each row contains an editable **Map** check box, a **Login** name, and a **Type** of login.

Specify a login by selecting the **Map** checkbox for the login.

**Database role membership for:** `<data warehouse name>`

Select the management data warehouse role that the login is mapped to by selecting the checkbox by one or more of the following options:

- **mdw_admin**
- **mdw_reader**
- **mdw_writer**

**New Login**

Open the **Login - New** dialog box and create a new login for the management data warehouse.

**Complete the Wizard page**

Use this page to verify and complete data collection configuration. The tree displayed in the viewing window shows what configurations will be applied, and which actions take place, when you select **Finish**.

**Configure Data Collection Wizard Progress page**

Use this page to view the results of each configuration step.

**Details**

Displays each configuration step as a row in the **Details** grid. Each row contains an **Action** column that describes the step, and a **Status** column that indicates the success or failure of the step. If there's an error, a message appears in the **Message** column.

**Stop**

Stop wizard processing.

**Report**

View a report of the data collection configuration. The following report options are provided:

- View Report
- Save Report to File
- Copy Report to Clipboard
- Send Report as E-mail

**Close**

Close the wizard.

## Related content

- [sp_syscollector_enable_collector (Transact-SQL)](../system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md)
- [sp_syscollector_disable_collector (Transact-SQL)](../system-stored-procedures/sp-syscollector-disable-collector-transact-sql.md)
- [Data collection](data-collection.md)
- [Manage data collection](manage-data-collection.md)
