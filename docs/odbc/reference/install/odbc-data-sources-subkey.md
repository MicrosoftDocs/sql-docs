---
description: "ODBC Data Sources subkey"
title: "ODBC Data Sources subkey | Microsoft Docs"
ms.custom: ""
ms.date: "09/23/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "subkeys [ODBC], for data sources"
  - "data sources [ODBC], subkeys"
  - "registry entries for data sources [ODBC], subkeys"
ms.assetid: 0a8ccb80-c573-4418-84e5-f04a2b0e2ac1
author: David-Engel
ms.author: v-davidengel
---
# ODBC Data Sources subkey

The values under the `ODBC Data Sources` subkey list the data sources. The format of these values is shown in the following table.

| Name | Data type | Data |
| :--- | :-------- | :--- |
| *data-source-name* | REG_SZ | *driver-description* |

The *data-source-name* value is defined by the administration program (which usually prompts the user for it), and *driver-description* is defined by the driver developer (it is usually the name of the DBMS associated with the driver).

For example, suppose three data sources have been defined: Inventory, which uses SQL Server; Payroll, which uses dBASE; and Personnel, which uses formatted text files. The values under the `ODBC Data Sources` subkey might be as follows:

```console
Inventory : REG_SZ : SQL Server
Payroll : REG_SZ : dBASE
Personnel : REG_SZ : Text
```
