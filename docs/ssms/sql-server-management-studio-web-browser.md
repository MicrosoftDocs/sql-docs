---
title: Web Browser
description: "SQL Server Management Studio Web Browser"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 12/21/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
helpviewer_keywords:
  - "hosting Web browsers"
  - "Web browsers [SQL Server Management Studio]"
  - "integrated Web browsers [SQL Server Management Studio]"
---

# SQL Server Management Studio Web Browser

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Management Studio (SSMS) can invoke a web browser through menu items, when selecting a URL in the query editor, or when using Microsoft Entra authentication.  

## Web browser configuration

To control what browser is invoked from SSMS, change the setting, **Use system default web browser**. Within **Tools > Options**, select **Azure Services**, and within **Miscellaneous**, alter the setting.

- The default value for **Use system default web browser** is *False* starting with SSMS 19.1. When set to *False*, SSMS uses the default browser configured for the user's workstation. This value isn't changed when upgrading from an earlier version of SSMS to version 19.1 or higher.  

- For SSMS 19.0.2 and below, the default value is *True*. When set to *True*, SSMS invokes Microsoft Internet Explorer for Microsoft Entra authentication. Internet Explorer was retired in June 2022. If you encounter an error message titled `Unsupported browser`, change **Use system default web browser** to *False* and configure the default browser for the workstation.

## See Also

- [General User Interface Elements](../ssms/general-user-interface-elements.md)
- [Options (Azure Services)](menu-help/options-azure-services.md)
