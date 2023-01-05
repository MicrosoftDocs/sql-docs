---
description: "Default Driver Subkey"
title: "Default Driver Subkey | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "default subkey [ODBC]"
  - "registry entries for components [ODBC], default subkey"
  - "subkeys [ODBC], default subkey"
  - "drivers subkey [ODBC]"
ms.assetid: 9e58b24f-ebfc-4286-a272-0843b4d6f2d5
author: David-Engel
ms.author: v-davidengel
---
# Default Driver Subkey
The Default subkey contains a single value that describes the driver used by the default data source. The format of this value is shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|**Driver**|REG_SZ|*default-driver-description*|  
  
 The *default-driver-description* name is the same as the name of the value under the ODBC Drivers subkey that describes the driver.  
  
 For example, if the default data source uses the SQL Server driver, the value under the Default subkey might be:  
  
```  
Driver : REG_SZ : SQL Server  
```  
  
> [!NOTE]  
>  The default driver contained in the Default subkey can refer to either a default user DSN or a default system DSN. If both a default user DSN and a default system DSN have been created, the default driver is determined by the DSN that was created last, so it might not be a valid entry for the DSN that was created first.
