---
title: "Server configuration: default full-text language"
description: Learn about the default full-text language option. See how to configure it to specify the default language that SQL Server uses for full-text indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "languages [full-text search]"
  - "default full-text language option"
---
# Server configuration: default full-text language

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure the `default full-text language` server configuration option in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. The `default full-text language` option specifies a default language value for full-text indexes. Linguistic analysis is performed on all data that is full-text indexed and is dependent on the language of the data. The default value of this option is the language of the server. For a localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the `default full-text language` option to the language of the server if an appropriate match exists. For a non-localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the `default full-text language` option is English.

## Limitations

The value of the `default full-text language` option is used in a full-text index when no language is specified for a column through the `LANGUAGE <language_term>` option in the `CREATE FULLTEXT INDEX` or `ALTER FULLTEXT INDEX` statements. If the default full-text language isn't supported or the linguistic analysis package isn't available, the `CREATE` or `ALTER` operation fails and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message stating that the language specified isn't valid.

## Recommendations

This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] professional.

The `default full-text language` option requires a locale identifier (LCID) value. For a list of supported LCIDs and their related languages, see [sys.fulltext_languages](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md). Other languages might also be available from independent software vendors, for example. If no specific language is found, the Full-Text Engine automatically switches to the primary language.

## Permissions

Execute permissions on `sp_configure` with no parameters or with only the first parameter are granted to all users by default. To execute `sp_configure` with both parameters to change a configuration option or to run the `RECONFIGURE` statement, a user must be granted the `ALTER SETTINGS` server-level permission. The `ALTER SETTINGS` permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, right-click a server and select **Properties**.

1. Select the **Advanced** node.

1. Under Miscellaneous, use **Default Full Text Language** to specify a default language value for full-text indexed columns.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `default full-text` option to Dutch (`1043`).

   ```sql
   USE master;
   GO

   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'default full-text language', 1043;
   GO

   RECONFIGURE;
   GO

   EXECUTE sp_configure 'show advanced options', 0;
   GO

   RECONFIGURE;
   GO
   ```

For more information, see [Server configuration options](server-configuration-options-sql-server.md).

<a id="FollowUp"></a>

## Follow up: After you configure the default full-text language option

The setting takes effect immediately without restarting the server.

## Related content

- [sys.fulltext_languages (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [CREATE FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/create-fulltext-index-transact-sql.md)
- [ALTER FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/alter-fulltext-index-transact-sql.md)
