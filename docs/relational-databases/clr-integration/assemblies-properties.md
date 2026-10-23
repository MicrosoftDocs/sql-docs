---
title: "Assembly Properties (General Page)"
description: You can view or modify properties for an assembly hosted on SQL Server. These include assembly name and owner, permission set, and other properties.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
f1_keywords:
  - "sql13.swb.assemblies.general.f1"
---
# Assembly properties

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this page to view or modify properties for the assembly in [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)].

## Options

#### Assembly name

Displays the assembly name, which always matches the name of the CLR assembly.

#### Assembly owner

Type the owner name or schema name or select from the list.

#### Permission set

Set the security level for the assembly. Three levels of security are provided: **Safe**, **External access**, and **Unsafe** access.

#### Path to assembly

Type the path to the assembly file.

#### Browse

Navigate to the assembly you want to add. Select **Browse** if you don't want to type the path to the assembly file.

## Additional properties grid

#### Creation Date

Displays the date the assembly was created/registered.

#### Strong Name

Displays **True** if the assembly has been digitally signed, **False** if it hasn't been digitally signed.

#### Version

Displays the version number of the assembly.

## Related content

- [CREATE ASSEMBLY (Transact-SQL)](../../t-sql/statements/create-assembly-transact-sql.md)
