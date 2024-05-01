---
title: Restore master database
description: Restore the master database in Analytics Platform System (APS).
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Restore the master database in Analytics Platform System (APS)
The **Restore Master** page of the SQL Server PDW Configuration Manager enables you to restore the `master` database from a backup.  
  
## Before you begin
  
> [!IMPORTANT]  
> To perform the restore, SQL Server PDW must delete the current `master` database, which contains user security information and the database catalog. We recommend making a backup of the current `master` database before performing the restore.  
  
## <a id="to-restore-the-master-database"></a> Restore the master database
  
1. Launch the Configuration Manager. For more information, see [Launch the Configuration Manager (Analytics Platform System)](launch-the-configuration-manager.md).  
  
1. In the left pane of the Configuration Manager, select **Restore Master**.  
  
1. Select the master backup to restore.  
  
1. Select **Apply**.  
  
1. To perform the restore, SQL Server PDW will shut down all appliance services and disconnect all users. After the restore completes, SQL Server PDW will restart the appliance services.  
  
:::image type="content" source="./media/restore-the-master-database/SQL_Server_PDW_DWConfig_ApplPDWRestore.png" alt-text="A screenshot of the Microsoft Analytics Platform System Configuration Manager, showing the Restore master page.":::
  
## Related content

- [Microsoft Analytics Platform System](home-analytics-platform-system-aps-pdw.md)
- [System databases in Parallel Data Warehouse](system-databases.md)