---
title: Azure activity log
description: View Azure Arc-enabled SQL Server and Databases activity log
author: guptasnigdha
ms.author: guptasnigdha
ms.reviewer: mikeray, rajpo
ms.date: 05/24/2023 ##edit
ms.service: sql
ms.topic: conceptual
---

# Azure Arc-enabled SQL Server and Databases Activity logs

The Azure activity logs for Arc-enabled SQL Server provide an insight into [SQL Server - Azure Arc](overview.md) and [SQL Server databases - Azure Arc](view-databases.md) related events. The activity log includes information like when a resource is created or modified. </br>
One can access these activity logs by going to the SQL Server - Azure Arc resource > Activty Log. With the help of activity logs one can identify events like SQL Server instance update, *SqlServerInstance_Update* SQL Server Databases Update, *SqlServerDatabases_Update*, Writing of tags to the resource etc.<br>
This helps in auditing different operations performed on the resource along with other crucial information such as time at which operation was initiated, it's status and party responsible for event creation. 

## View the activity log
You can access the activity log from most menus in the Azure portal. The menu that you open it from determines its initial filter. You can always change the filter to view all other entries. Select **Add Filter** to add more properties to the filter.

