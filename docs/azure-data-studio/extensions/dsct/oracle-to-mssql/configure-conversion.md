---
title: "Configure conversion"
description: "Learn how to adjust the conversion settings when using Database Schema Conversion Toolkit (Oracle to Microsoft SQL)."
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: "10/4/2021"
ms.service: azure-data-studio
ms.topic: conceptual
---

# Configure conversion

The Database Schema Conversion Toolkit (Oracle to Microsoft SQL) is aiming to provide a solution to database schema migrations. While default conversion is fairly reasonable, there are cases when users may want to adjust some of the conversion settings to better suit their needs.

## Configuration user interface

When [performing a database schema conversion](./convert-oracle-database-objects-to-mssql.md) through the Database Schema Conversion Toolkit conversion wizard UI, the **Conversion settings** step will allow you to tweak basic conversion settings.

In more complex scenarios custom JSON configuration file with advanced options can be provided, as explained in the next section.

## Advanced conversion configuration

There are some conversion settings that are not currently exposed through the user interface. These settings can be adjusted through the JSON configuration file.

Basic structure of the configuration file looks as following:

```json
{
  "options" : <simple-conversion-options>,
  "dataTypeMappings": [
    <data-type-mapping>,
    ...
  ],
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

### Data type mappings

The `dataTypeMappings` configuration region consists of multiple data type mapping records. Each data type mapping record has the following schema:

```json
{
  "source": {
    "type": "<oracle-data-type-name>",
    "arguments": [
      <argument-value-matching-expression>,
      ...
    ]
  },
  "target": {
    "type": "<ms-sql-data-type-name>",
    "arguments": [
      <argument-value-expression>,
      ...
    ]
  }
}
```

The `source` section defines the source data type that is being mapped and consists of two parts:
- `type` is the name of the Oracle data type to map;
- `arguments` is the collection of matching expressions that will further filter a data type based on its arguments values.

The source `arguments` collection defines matching expressions for data type arguments based on their position. The collection should contain one string expression for each data type argument. Supported expressions are:

| Expression | Meaning |
| ---------- | ------- |
| `"<number>"` | Matches the exact value of an argument. |
| `"*"` | Matches special `*` argument value. For example, the first argument in the `NUMBER(*, 5)` data type definition. |
| `"X..Y"` | Matches an argument value in the `[X, Y]` range, where `X` and `Y` can be either `<number>` or `*`. The `*` in the range expression means _unbound_. To match any argument value the `"*..*"` range expression can be used. |

Some data types may have their arguments specified within the type name, for example `INTERVAL DAY (2) TO SECOND (6)`. In such cases, the type name would be `INTERVAL DAY TO SECOND`, while `2` and `6` are considered first and second arguments respectively.

The `target` section of the data type mapping record defines the Microsoft SQL data type that should be used in the target database and consists of two parts:
- `type` is the name of the Microsoft SQL data type to map to;
- `arguments` is the collection of expressions that define values for the target data type arguments.

The `arguments` collection defines data type arguments value expressions based on the arguments position. The collection should contain one string expression for each data type argument. Supported expressions are:

| Expression | Meaning |
| ---------- | ------- |
| `"<number>"` | Specifies an exact value of an argument. |
| `"$<number>"` | Specifies that a value of the `<number>` source argument should be used. The index is 1-based. For example, `$2` will be replaced with the value of the second argument of the matched source data type. |

The following example demonstrates how to map the `VARCHAR2` Oracle data type that holds 4000 characters or less, to the `NVARCHAR` Microsoft SQL data type of the same length as the source data type:

```json
{
  "source": {
    "type": "VARCHAR2",
    "arguments": [
      "*..4000"
    ]
  },
  "target": {
    "type": "NVARCHAR",
    "arguments": [
      "$1"
    ]
  }
}
```

> [!IMPORTANT]
> Data type mappings should be defined from the least specific to the more specific, as they will be applied in reverse order. In other words, every subsequent data type mapping overrides (entirely or in part) previously defined mappings.

> [!NOTE]
> Database Schema Conversion Toolkit comes with the built-in data type mappings that cover common scenarios, thus custom data type mappings are not required in most cases.

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

Following example demonstrates entire configuration file that maps the `VARCHAR2` Oracle data type that holds 4000 characters or less, to the `NVARCHAR` Microsoft SQL data type of the same length as the source data type, while also replacing `HR` Oracle schema with `dbo` in the target database:

```json
{
  "dataTypeMappings": [
    {
      "source": {
        "type": "VARCHAR2",
        "arguments": [
          "*..4000"
        ]
      },
      "target": {
        "type": "NVARCHAR",
        "arguments": [
          "$1"
        ]
      }
    }
  ],
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

When this configuration is used all converted objects from `HR` schema will be defined under the `dbo` schema and all matching references to the `VARCHAR2` data type will be replaced with the `NVARCHAR`.
