---
title: "Create a Database Master Key | Microsoft Docs"
description: Create a database master key in SQL Server by using Transact-SQL. Be sure you have the required permissions.
ms.custom: ""
ms.date: "09/12/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "database master key [SQL Server], creating"
ms.assetid: 8cb24263-e97d-4e4d-9429-6cf494a4d5eb
author: jaszymas
ms.author: jaszymas
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a Database Master Key

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
This topic describes how to create a database master key in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)].

## Security

### Permissions

Requires CONTROL permission on the database.

## Using Transact-SQL

### To create a database master key

1. Choose a password for encrypting the copy of the master key that will be stored in the database.
2. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].
3. Expand **System Databases**, right-click `master` and then click **New Query**.
4. Copy and paste the following example into the query window and click **Execute**.

   ```sql
     -- Creates the master key.
     -- The key is encrypted using the password "23987hxJ#KL95234nl0zBe".  
     CREATE MASTER KEY ENCRYPTION BY PASSWORD = '23987hxJ#KL95234nl0zBe';  

   ```

For more information, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-master-key-transact-sql.md).
