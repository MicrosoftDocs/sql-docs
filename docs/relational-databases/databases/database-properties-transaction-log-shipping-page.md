---
title: "Database Properties (Transaction Log Shipping Page) | Microsoft Docs"
description: "Use the Transaction Log Shipping tab in the Database Properties dialog box to enable a database as a log shipping primary database and set its options."
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.logshipping.f1"
ms.assetid: 72c4e539-fe11-4d57-b977-65b418d5916f
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Database Properties (Transaction Log Shipping Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use this page to configure and modify the properties of log shipping for a database.  
  
 For an explanation of log shipping concepts, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
## Options  
 **Enable this as a primary database in a log shipping configuration**  
 Enables this database as a log shipping primary database. Select it and then configure the remaining options on this page. If you clear this check box, the log shipping configuration will be dropped for this database.  
  
 **Backup Settings**  
 Click **Backup Settings** to configure backup schedule, location, alert, and archiving parameters.  
  
 **Backup schedule**  
 Shows the currently selected backup schedule for the primary database. Click **Backup Settings** to modify these settings.  
  
 **Last backup created**  
 Indicates the time and date of the last transaction log backup of the primary database.  
  
 **Secondary server instances and databases**  
 Lists the currently configured secondary servers and databases for this primary database. Highlight a database, and then click **...** to modify the parameters associated with that secondary database.  
  
 **Add**  
 Click **Add** to add a secondary database to the log shipping configuration for this primary database.  
  
 **Remove**  
 Removes a selected database from this log shipping configuration. Select the database first and then click **Remove**.  
  
 **Use a monitor server instance**  
 Sets up a monitor server instance for this log shipping configuration. Select the **Use a monitor server instance** check box and then click **Settings** to specify the monitor server instance.  
  
 **Monitor server instance**  
 Indicates the currently configured monitor server instance for this log shipping configuration.  
  
 **Settings**  
 Configures the monitor server instance for a log shipping configuration. Click **Settings** to configure this monitor server instance.  
  
 **Script Configuration**  
 Generates a script for configuring log shipping with the parameters you have selected. Click **Script Configuration**, and then choose an output destination for the script.  
  
> [!IMPORTANT]  
>  Before scripting settings for a secondary database, you must invoke the **Secondary Database Settings** dialog box. Invoking this dialog box connects you to the secondary server and retrieves the current settings of the secondary database that are needed to generate the script.  
  
## See Also  
 [Log Shipping Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/log-shipping-stored-procedures-transact-sql.md)   
 [Log Shipping Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-tables-transact-sql.md)  
  
  
