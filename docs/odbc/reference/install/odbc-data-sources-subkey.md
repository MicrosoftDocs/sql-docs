---
title: "ODBC Data Sources subkey"
description: "ODBC Data Sources subkey"
author: David-Engel
ms.author: davidengel
ms.date: "09/23/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "subkeys [ODBC], for data sources"
  - "data sources [ODBC], subkeys"
  - "registry entries for data sources [ODBC], subkeys"
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
