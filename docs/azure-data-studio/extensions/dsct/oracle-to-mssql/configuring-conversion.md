---
title: "Configuring conversion"
description: "Learn how to adjust the conversion settings when using Database Schema Conversion Toolkit (Oracle to MS SQL)."
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: "nahk-ivanov"
ms.author: "alexiva"
ms.reviewer: "maghan"
ms.topic: conceptual
ms.custom:
ms.date: "10/4/2021"
---

# Configuring conversion

The Database Schema Conversion Toolkit (Oracle to MS SQL) is aiming to provide a solution to database schema migrations. While default conversion is fairly reasonable, there are cases when users may want to adjust some of the conversion settings to better suit their needs.

## Configuraton user interface

When [performing a database schema conversion](./converting-oracle-database-objects-to-mssql.md) through the Database Schema Conversion Toolkit conversion wizard UI, the **Conversion settings** step will allow you to tweak basic conversion settings.

In more complex scenarios custom JSON configuration file with advanced options can be provided, as explained in the next section.

## Advanced conversion configuration

There are some conversion settings that are not currently exposed through the user interface. These settings can be adjusted through the JSON configuration file.

Basic structure of the configuration file looks as following:

```json
{
  "options" : <simple-conversion-options>,
  "nameMappings": [
    <object-name-mapping>,
    ...
  ]
}
```

Following sections will cover each configuration region in more details.

### Simple conversion options

The `options` configuration region has the following schema:

```json
{
  "msSqlDialect":
    "AzureSynapse"
    | "AzureSqlDatabase"
    | "AzureSqlManagedInstance"
    | "SqlServer2012"
    | "SqlServer2014"
    | "SqlServer2016"
    | "SqlServer2017"
    | "SqlServer2019",
  "quoteIdentifiers": true | false,
  "isMsSqlCaseSensitive": true | false,
}
```

Following table describes all possible configuration options in this region:

| Option name | Description |
| ----------- | ----------- |
| `msSqlDialect` | Determines which Microsoft SQL platform dialect to use when converting the source object definitions. This option will be derived from the target SQL Database project and you should not need to set it explicitly. |
| `quoteIdentifiers` | Determines whether all identifiers should be quoted in converted SQL scripts. Default is `true`. It is recommended to set it to `true`, as quotation might be required when special characters are used in identifier names. |
| `isMsSqlCaseSensitive` | Controls whether [DSCT01000](../conversion-messages/dsct01000.md) conversion message will be produced during conversion. This option will be derived from the default collation of the target SQL Database project and you should not need to set it explicitly. |

### Object name mappings

The `nameMappings` configuration region consists of multiple name mapping records. Each name mapping record has the following schema:

```json
{
  "source": [
    {
      "type":
         "constraint"
         | "index"
         | "materializedview"
         | "sequence"
         | "synonym"
         | "table"
         | "tablecolumn"
         | "tabletrigger"
         | "user"
         | "view",
      "name": "<source-object-name>"
    },
    ...
  ],
  "target": "<target-object-name>"
}
```

The `source` collection defines name parts of the source identifier that is being mapped. For example, to define the target name for a source schema, the following source collection may be used:

```json
[
  { "type" : "user", "name": "MySchema" }
]
```

If you want to define target name for the source table, then two-part identifier name should be specified in the `source` collection as following:

```json
[
  { "type" : "user", "name": "MySchema" },
  { "type" : "table", "name": "MyTable" }
]
```

This will match a table with multi-part name `"MySchema"."MyTable"`. Most database objects will require multi-part names to be specified in the `source` collection.

Following table describes supported source object types:

| Type | Description |
| ----------- | ----------- |
| `constraint` | Name of the constraint object |
| `index` | Name of the index object |
| `materializedview` | Name of the materialized view object |
| `sequence` | Name of the sequence object |
| `synonym` | Name of the synonym object |
| `table` | Name of the table object |
| `tablecolumn` | Name of the table column |
| `tabletrigger` | Name of the table trigger object |
| `user` | Name of the database schema |
| `view` | Name of the view object |

The `target` property is always a simple string that defines new name for the source object that matches `source` multi-part identifier.

> [!NOTE]
> It is not possible to change the schema for just one object. The `target` property only specifies a one-part name for the matching source object, not a multi-part name.

### Examples

Following example demonstrates entire configuration file that maps `HR` Oracle schema to `dbo` in the target database:

```json
{
  "nameMappings": [
    {
      "source": [
        { "type": "user", "name": "HR" }
      ],
      "target": "dbo"
    }
  ]
}
```

When this configuration is used all converted objects from `HR` schema will be defined under the `dbo` schema.
