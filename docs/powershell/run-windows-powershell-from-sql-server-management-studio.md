---
title: Run Windows PowerShell from SQL Server Management Studio
description: Learn how to start a Windows PowerShell session from Object Explorer in SQL Server Management Studio, with the path preset to the location of your choice of objects.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 03/14/2017
---

# Run Windows PowerShell from SQL Server Management Studio

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can start Windows PowerShell sessions from **Object Explorer** in SQL Server Management Studio (SSMS). SSMS launches Windows PowerShell, loads the **SqlServer** module, and sets the path context to the associated node in the **Object Explorer** tree.

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

When you specify running PowerShell for an object in **Object Explorer**, SQL Server Management Studio starts a Windows PowerShell session in which the SQL Server PowerShell snap-ins have been loaded and registered. The path for the session is preset to the location of the object you right-clicked in Object Explorer.

For example, if you right-click the AdventureWorks database object in Object Explorer and select **Start PowerShell**, the Windows PowerShell path is set as seen below:

```powershell
SQLSERVER:\SQL\MyComputer\MyInstance\Databases\AdventureWorks2012>  
```

## Run PowerShell

### To run PowerShell from SQL Server Management Studio

1. Open **Object Explorer**.

2. Navigate to the node for the object to be worked on.

3. Right-click the object and select **Start PowerShell**.

## Permissions

When opened from SQL Server Management Studio, PowerShell does not run with Administrator privileges, which may prevent some activities such as calls to WMI.

## See Also

- [SQL Server PowerShell](sql-server-powershell.md)