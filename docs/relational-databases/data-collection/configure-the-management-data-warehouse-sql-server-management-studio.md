---
title: "Configure the Management Data Warehouse (SSMS)"
description: Learn how to configure the management data warehouse to support data storage on one or more instances of SQL Server that are using the data collector. 
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
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
ms.assetid: 23a584f3-c5e1-414c-9afe-73cd7efbda4b
author: MashaMSFT
ms.author: mathoma
---
# Configure the Management Data Warehouse (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to configure the management data warehouse to support data storage on a single instance or multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are using the data collector. These instances can be on the same server or on different servers. This topic also provides descriptions of the user interface for the [Configure Data Management Warehouse Wizard](#Wizard) dialog box. For information about configuring a data collector, see [Configure Properties of a Data Collector](../../relational-databases/data-collection/configure-properties-of-a-data-collector.md).  
  
> [!NOTE]  
>  If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is configured to run using one of the System service accounts (Local System, Network Service, or Local Service), and the management data warehouse is created on a different instance from the data collector, you must configure collection sets to use a proxy for uploading data to the management data warehouse.  
  
### Configure the management data warehouse on a single instance or multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
1.  Ensure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running.  
  
2.  In Object Explorer, expand the **Management** node.  
  
3.  Right-click **Data Collection**, expand **Tasks**, and then click **Configure Management Data Warehouse**.  
  
4.  Use the [Configure Management Data Warehouse Wizard](#Wizard) to create a management data warehouse, configure logins, enable data collection, and start the **System Data Collection Sets**.  
  
     To configure multiple instances, continue with step 5.  
  
    > [!NOTE]  
    >  It is considered best practice to use proxies in deployments where the data collector is installed on multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are using the same management data warehouse.  
  
5.  Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on another instance and do either of the following:  
  
    -   Use the Configure Management Data Warehouse wizard to configure data collection for the existing management data warehouse.  
  
    -   Right-click **Data Collection**, and then click **Properties**. On the **General** tab, specify the existing management data warehouse and the server that it is installed on.  
  
6.  Repeat step 5 until all the database instances that use the data collector are configured to upload data to the shared management data warehouse.  

####  <a name="Wizard"></a> Configure Management Data Warehouse Wizard  
 **Welcome Page**  
  
 The Welcome page is the starting page of the Configure Data Collection Wizard. Displaying this page is optional.  
  
 **Do not show this starting page again.**  
 Select to suppress this page the next time you launch the Configure Data Collection Wizard.  
  
 **Configure Management Data Warehouse Storage Page**  
  
 Use this page to select a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database server and management data warehouse. The management data warehouse is a relational database that will store collected data.  
  
> [!NOTE]  
>  You must have the appropriate level of permissions in order to create the management data warehouse on the server. For more information, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md). You also must have the appropriate level of permissions to create logins for management data warehouse roles.  
  
 **Server name**  
 Specifies the name of the server that will host the management data warehouse.  
  
 When configuring a management data warehouse, **Server name** is always the name of the local server and cannot be modified.  
  
 **Database name**  
 Specifies the relational database that will store collected data. Use the list to select an existing database or click **New** to create a new database using the **New Database** dialog.  
  
 The **New** option is available only when configuring a data collection set  
  
 **Map Logins and Users Page**  
  
 Use this page to map logins to database user roles for the management data warehouse.  
  
 **Users mapped to this login**  
 Displays the existing logins on the server that will host the management data warehouse. Each row contains an editable **Map** check box, a **Login** name, and a **Type** of login.  
  
 Specify a login by selecting the **Map** checkbox for the login.  
  
 **Database role membership for:**  *\<data warehouse name>*  
 Select the management data warehouse role that the login is mapped to by clicking the checkbox by one or more of the following options:  
  
-   **mdw_admin**  
  
-   **mdw_reader**  
  
-   **mdw_writer**  
  
 **New Login**  
 Open the **Login - New** dialog box and create a new login for the management data warehouse.  
  
 **Complete the Wizard Page**  
  
 Use this page to verify and complete data collection configuration. The tree displayed in the viewing window shows what configurations will applied as well as what actions will take place when you click **Finish**.  
  
 **Configure Data Collection Wizard Progress Page**  
  
 Use this page to view the results of each configuration step.  
  
 **Details**  
 Displays each configuration step as a row in the **Details** grid. Each row contains an **Action** column that describes the step, and a **Status** column that indicates the success or failure of the step. If there is an error, a message appears in the **Message** column.  
  
 **Stop**  
 Stop wizard processing.  
  
 **Report**  
 View a report of the data collection configuration. The following report options are provided:  
  
-   View Report  
  
-   Save Report to File  
  
-   Copy Report to Clipboard  
  
-   Send Report as E-mail  
  
 **Close**  
 Close the wizard.  
  
## See Also  
 [sp_syscollector_enable_collector &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md)   
 [sp_syscollector_disable_collector &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-disable-collector-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [Manage Data Collection](../../relational-databases/data-collection/manage-data-collection.md)  
  
