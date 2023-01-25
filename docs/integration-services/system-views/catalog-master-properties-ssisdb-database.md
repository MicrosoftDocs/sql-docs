---
description: "catalog.master_properties (SSISDB Database)"
title: "catalog.master_properties (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 00bfa716-5390-48e3-b30c-d954d5e0be47
author: chugugrace
ms.author: chugu
---
# catalog.master_properties (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

Displays the properties of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Master.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|property_name|**nvarchar(256)**|The name of the scale out master property.|  
|property_value|**nvarchar(max)**|The value of the scale out master property.|

## Remarks
This view displays a row for each scale out master property. The properties displayed by this view include the following:

|Property Name|Description|  
|-------------------|-----------------| 
|**CLUSTER_LOGDB_SERVER**|The SQL Server that log database locates in.|
|**LAST_ONLINE_TIME**|The last time when Scale Out Master is online.|
|**MACHINE_IP**|The IP of the machine.|
|**MACHINE_NAME**|The name of the machine.|
|**MASTER_ADDRESS**|The endpoint of Scale Out Master.|
|**MASTER_SERVICE_PORT**|The port in the endpoint of Scale Out Master.|
|**SSLCERT_THUMBPRINT**|The thumbprint of Scale Out Master certificate.|

## Permissions
All members of public database role have read permission for this view. 
