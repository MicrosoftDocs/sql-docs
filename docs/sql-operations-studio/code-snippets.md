---
title: Code snippets in SQL Operations Studio | Microsoft Docs
description: Learn how to add code snippets to SQL Operations Studio, for your own use or to share.
keywords: 
ms.custom: "tools|sos"
ms.date: "11/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sanagama; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: craigg
ms.workload: "Inactive"
---
# Create code snippets to easily reuse in [!INCLUDE[name-sos](../includes/name-sos-short.md)]

Code snippets are templates that make it easier to enter repeating code patterns, such as loops or conditional-statements.

Snippets show in IntelliSense (**Ctrl+Space**) mixed with other suggestions as well as in a dedicated snippet picker (**Insert Snippet** in the Command Palette). There is also support for tab-completion: Enable it with `"editor.tabCompletion": true`, type a *snippet prefix*, and press **Tab** to insert a snippet.

The snippet syntax follows the [TextMate snippet syntax](https://manual.macromates.com/en/snippets) with the exception of 'interpolated shell code', which is not supported.

![ajax snippet](media/code-snippets/ajax-snippet.gif)

## Creating your Own Snippets

You can define your own snippets for specific languages.  To open up a snippet file for editing, open **User Snippets** under **File** > **Preferences** (**Code** > **Preferences** on Mac) and select the language for which the snippets should appear.

Snippets are defined in a JSON format and stored in a per user `(languageId).json` file. For example, Markdown snippets go in a `markdown.json` file.

The following example is a `For Loop` snippet for `JavaScript`.

```json
    "For Loop": {
        "prefix": "for",
        "body": [
            "for (var ${1:index} = 0; ${1:index} < ${2:array}.length; ${1:index}++) {",
            "\tvar ${3:element} = ${2:array}[${1:index}];",
            "\t$0",
            "}"
        ],
        "description": "For Loop"
    },
```

In the previous example:

* `For Loop` is the snippet name.
* `prefix` defines how this snippet is selected from IntelliSense and tab completion. In this case `for`.
* `body` is the content and either a single string or an array of strings of which each element will be inserted as separate line.
* `description` is the description used in the IntelliSense drop down.

The example above has three placeholders, `${1:index}`, `${2:array}`, and `${3:element}`. You can quickly traverse them in the order of their number. The string after the number and colon is filled in as default.

Once you have added a new snippet, you can try it out right away, no restart needed.

## Snippet Syntax

The `body` of a snippet can use special constructs to control cursors and the text being inserted. The following are supported features and their syntaxes:

### Tabstops

With tabstops, you can make the editor cursor move inside a snippet. Use `$1`, `$2` to specify cursor locations. The number is the order in which tabstops are visited, whereas `$0` denotes the final cursor position. Multiple tabstops are linked and updated in sync.

### Placeholders

Placeholders are tabstops with values, like `${1:foo}`. The placeholder text is inserted such that it can be easily changed. Placeholders can be nested, like `${1:another ${2:placeholder}}`.

### Choice

Placeholders can have choices as values. The syntax is a comma-separated enumeration of values, enclosed with the pipe-character, for example `${1|one,two,three|}`. When the snippet is inserted and the placeholder selected, choices prompt the user to pick one of the values.

### Variables

With `$name` or `${name:default}` you can insert the value of a variable. When a variable isn’t set, its *default* or the empty string is inserted. When a variable is unknown (that is, its name isn’t defined) the name of the variable is inserted and it is transformed into a placeholder.

The following variables can be used:

* `TM_SELECTED_TEXT` The currently selected text or the empty string
* `TM_CURRENT_LINE` The contents of the current line
* `TM_CURRENT_WORD` The contents of the word under cursor or the empty string
* `TM_LINE_INDEX` The zero-index-based line number
* `TM_LINE_NUMBER` The one-index-based line number
* `TM_FILENAME` The filename of the current document
* `TM_FILENAME_BASE` The filename of the current document without its extensions
* `TM_DIRECTORY` The directory of the current document
* `TM_FILEPATH` The full file path of the current document

### Variable Transforms

Transformations allow you to modify the value of a variable before it is inserted. The definition of a transformation consists of three parts:

1. A regular expression that is matched against the value of a variable, or the empty string when the variable cannot be resolved.
2. A "format string" that allows to reference matching groups from the regular expression. The format string allows for conditional inserts and simple modifications.
3. Options that are passed to the regular expression.

The following example inserts the name of the current file without its ending, so from `foo.txt` it makes `foo`.

```
${TM_FILENAME/(.*)\..+$/$1/}
  |           |         | |
  |           |         | |-> no options
  |           |         |
  |           |         |-> references the contents of the first
  |           |             capture group
  |           |
  |           |-> regex to capture everything before
  |               the final `.suffix`
  |
  |-> resolves to the filename
```

### Grammar

Below is the EBNF ([extended Backus-Naur form](https://en.wikipedia.org/wiki/Extended_Backus-Naur_form)) for snippets. With `\` (backslash), you can escape `$`, ༖༗, and `\`. Within choice elements, the backslash also escapes comma and pipe characters.

```
any         ::= tabstop | placeholder | choice | variable | text
tabstop     ::= '$' int | '${' int '}'
placeholder ::= '${' int ':' any '}'
choice      ::= '${' int '|' text (',' text)* '|}'
variable    ::= '$' var | '${' var }'
                | '${' var ':' any '}'
                | '${' var '/' regex '/' (format | text)+ '/' options '}'
format      ::= '$' int | '${' int '}'
                | '${' int ':' '/upcase' | '/downcase' | '/capitalize' '}'
                | '${' int ':+' if '}'
                | '${' int ':?' if ':' else '}'
                | '${' int ':-' else '}' | '${' int ':' else '}'
regex       ::= JavaScript Regular Expression value (ctor-string)
options     ::= JavaScript Regular Expression option (ctor-options)
var         ::= [_a-zA-Z] [_a-zA-Z0-9]*
int         ::= [0-9]+
text        ::= .*
```

## Using TextMate snippets

You can also use existing TextMate snippets (.tmSnippets) with [!INCLUDE[name-sos](../includes/name-sos-short.md)]. See the [Using TextMate Snippets](/docs/extensions/themes-snippets-colorizers.md#using-textmate-snippets) article in our Extension Authoring section to learn more.

## Assign keybindings to snippets

You already know that snippets can be inserted via IntelliSense, the 'Insert Snippet'-action, or via tab-completion. That's not all. You can create custom keybindings to insert specific snippets. Open `keybindings.json`, which defines all your keybindings, and something add this:

```json
{
  "key": "cmd+k 1",
  "command": "editor.action.insertSnippet",
  "when": "editorTextFocus",
  "args": {
    "snippet": "console.log($1)$0"
  }
}
```

It invokes the 'Insert Snippet'-action but instead of letting you select a snippet it runs on the provided snippet. Also, instead of `snippet` you can have `langId` and `name` arguments to reference an existing snippet: `{ "langId": "csharp", "name": "myFavSnippet" }`

## Sample snippets

Now that you know how to create your own snippets, try out some of these other snippets:

```json
{
	"Get extension help": {
		"prefix": "sqlExtensionHelp",
		"body": [
			"/*",
			"mssql getting started:",
			"-----------------------------",
			"1. Change language mode to SQL: Open a .sql file or press Ctrl+K M (Cmd+K M on Mac) and choose 'SQL'.",
			"2. Connect to a database: Press F1 to show the command palette, type 'sqlcon' or 'sql' then click 'Connect'.",
			"3. Use the T-SQL editor: Type T-SQL statements in the editor using T-SQL IntelliSense or type 'sql' to see a list of code snippets you can tweak & reuse.",
			"4. Run T-SQL statements: Press F1 and type 'sqlex' or press Ctrl+Shift+e (Cmd+Shift+e on Mac) to execute all the T-SQL code in the editor.",
			"",
			"Tip #1: Put GO on a line by itself to separate T-SQL batches.",
			"Tip #2: Select some T-SQL text in the editor and press `Ctrl+Shift+e` (`Cmd+Shift+e` on Mac) to execute the selection",
			"*/"
		],
		"description": "Get extension help"
	},

	"Create a new Database": {
		"prefix": "sqlCreateDatabase",
		"body": [
			"-- Create a new database called '${1:DatabaseName}'",
			"-- Connect to the 'master' database to run this snippet",
			"USE master",
			"GO",
			"-- Create the new database if it does not exist already",
			"IF NOT EXISTS (",
			"\tSELECT name",
			"\t\tFROM sys.databases",
			"\t\tWHERE name = N'${1:DatabaseName}'",
			")",
			"CREATE DATABASE ${1:DatabaseName}",
			"GO"
		],
		"description": "Create a new Database"
	},

	"Drop a Database": {
		"prefix": "sqlDropDatabase",
		"body": [
			"-- Drop the database '${1:DatabaseName}'",
			"-- Connect to the 'master' database to run this snippet",
			"USE master",
			"GO",
			"-- Uncomment the ALTER DATABASE statement below to set the database to SINGLE_USER mode if the drop database command fails because the database is in use.",
			"-- ALTER DATABASE ${1:DatabaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;",
			"-- Drop the database if it exists",
			"IF EXISTS (",
			"  SELECT name",
			"   FROM sys.databases",
			"   WHERE name = N'${1:DatabaseName}'",
			")",
			"DROP DATABASE ${1:DatabaseName}",
			"GO"
		],
		"description": "Drop a Database"
	},

	"Create a new Table": {
		"prefix": "sqlCreateTable",
		"body": [
			"-- Create a new table called '${1:TableName}' in schema '${2:SchemaName}'",
			"-- Drop the table if it already exists",
			"IF OBJECT_ID('${2:SchemaName}.${1:TableName}', 'U') IS NOT NULL",
			"DROP TABLE ${2:SchemaName}.${1:TableName}",
			"GO",
			"-- Create the table in the specified schema",
			"CREATE TABLE ${2:SchemaName}.${1:TableName}",
			"(",
			"\t${1:TableName}Id INT NOT NULL PRIMARY KEY, -- primary key column",
			"\t$3Column1 [NVARCHAR](50) NOT NULL,",
			"\t$4Column2 [NVARCHAR](50) NOT NULL",
			"\t-- specify more columns here",
			");",
			"GO"
		],
		"description": "Create a new Table"
	},

	"Drop a Table": {
		"prefix": "sqlDropTable",
		"body": [
			"-- Drop the table '${1:TableName}' in schema '${2:SchemaName}'",
			"IF EXISTS (",
			"\tSELECT *",
			"\t\tFROM sys.tables",
			"\t\tJOIN sys.schemas",
			"\t\t\tON sys.tables.schema_id = sys.schemas.schema_id",
			"\tWHERE sys.schemas.name = N'${2:SchemaName}'",
			"\t\tAND sys.tables.name = N'${1:TableName}'",
			")",
			"\tDROP TABLE ${2:SchemaName}.${1:TableName}",
			"GO"
		],
		"description": "Drop a Table"
	},

	"Add a new column to a Table": {
		"prefix": "sqlAddColumn",
		"body": [
			"-- Add a new column '${1:NewColumnName}' to table '${2:TableName}' in schema '${3:SchemaName}'",
			"ALTER TABLE ${3:SchemaName}.${2:TableName}",
			"\tADD ${1:NewColumnName} /*new_column_name*/ int /*new_column_datatype*/ NULL /*new_column_nullability*/",
			"GO"
		],
		"description": "Add a new column to a Table"
	},

	"Drop a column from a Table": {
		"prefix": "sqlDropColumn",
		"body": [
			"-- Drop '${1:ColumnName}' from table '${2:TableName}' in schema '${3:SchemaName}'",
			"ALTER TABLE ${3:SchemaName}.${2:TableName}",
			"\tDROP COLUMN ${1:ColumnName}",
			"GO"
		],
		"description": "Add a new column to a Table"
	},

	"Select rows from a Table or a View": {
		"prefix": "sqlSelect",
		"body": [
			"-- Select rows from a Table or View '${1:TableOrViewName}' in schema '${2:SchemaName}'",
			"SELECT * FROM ${2:SchemaName}.${1:TableOrViewName}",
			"WHERE $3\t/* add search conditions here */",
			"GO"
		],
		"description": "Select rows from a Table or a View"
	},

	"Insert rows into a Table": {
		"prefix": "sqlInsertRows",
		"body": [
			"-- Insert rows into table '${1:TableName}'",
			"INSERT INTO ${1:TableName}",
			"( -- columns to insert data into",
			" $2[Column1], [Column2], [Column3]",
			")",
			"VALUES",
			"( -- first row: values for the columns in the list above",
			" $3Column1_Value, Column2_Value, Column3_Value",
			"),",
			"( -- second row: values for the columns in the list above",
			" $4Column1_Value, Column2_Value, Column3_Value",
			")",
			"-- add more rows here",
			"GO"
		],
		"description": "Insert rows into a Table"
	},

	"Delete rows from a Table": {
		"prefix": "sqlDeleteRows",
		"body": [
			"-- Delete rows from table '${1:TableName}'",
			"DELETE FROM ${1:TableName}",
			"WHERE $2\t/* add search conditions here */",
			"GO"
		],
		"description": "Delete rows from a Table"
	},

	"Update rows in a Table": {
		"prefix": "sqlUpdateRows",
		"body": [
			"-- Update rows in table '${1:TableName}'",
			"UPDATE ${1:TableName}",
			"SET",
			"\t$2[Colum1] = Colum1_Value,",
			"\t$3[Colum2] = Colum2_Value",
			"\t-- add more columns and values here",
			"WHERE $4\t/* add search conditions here */",
			"GO"
		],
		"description": "Update rows in a Table"
	},

	"Create a stored procedure": {
		"prefix": "sqlCreateStoredProc",
		"body": [
			"-- Create a new stored procedure called '${1:StoredProcedureName}' in schema '${2:SchemaName}'",
			"-- Drop the stored procedure if it already exists",
			"IF EXISTS (",
			"SELECT *",
			"\tFROM INFORMATION_SCHEMA.ROUTINES",
			"WHERE SPECIFIC_SCHEMA = N'${2:SchemaName}'",
			"\tAND SPECIFIC_NAME = N'${1:StoredProcedureName}'",
			")",
			"DROP PROCEDURE ${2:SchemaName}.${1:StoredProcedureName}",
			"GO",
			"-- Create the stored procedure in the specified schema",
			"CREATE PROCEDURE ${2:SchemaName}.${1:StoredProcedureName}",
			"\t$3@param1 /*parameter name*/ int /*datatype_for_param1*/ = 0, /*default_value_for_param1*/",
			"\t$4@param2 /*parameter name*/ int /*datatype_for_param1*/ = 0 /*default_value_for_param2*/",
			"-- add more stored procedure parameters here",
			"AS",
			"\t-- body of the stored procedure",
			"\tSELECT @param1, @param2",
			"GO",
			"-- example to execute the stored procedure we just created",
			"EXECUTE ${2:SchemaName}.${1:StoredProcedureName} 1 /*value_for_param1*/, 2 /*value_for_param2*/",
			"GO"
		],
		"description": "Create a stored procedure"
	},

	"Drop a stored procedure": {
		"prefix": "sqlDropStoredProc",
		"body": [
			"-- Drop the stored procedure called '${1:StoredProcedureName}' in schema '${2:SchemaName}'",
			"IF EXISTS (",
			"SELECT *",
			"\tFROM INFORMATION_SCHEMA.ROUTINES",
			"WHERE SPECIFIC_SCHEMA = N'${2:SchemaName}'",
			"\tAND SPECIFIC_NAME = N'${1:StoredProcedureName}'",
			")",
			"DROP PROCEDURE ${2:SchemaName}.${1:StoredProcedureName}",
			"GO"
		],
		"description": "Drop a stored procedure"
	},

	"List tables": {
		"prefix": "sqlListTablesAndViews",
		"body": [
			"-- Get a list of tables and views in the current database",
			"SELECT table_catalog [database], table_schema [schema], table_name name, table_type type",
			"FROM information_schema.tables",
			"GO"
		],
		"description": "List tables and vies in the current database"
	},

	"List databases": {
		"prefix": "sqlListDatabases",
		"body": [
			"-- Get a list of databases",
			"SELECT name FROM sys.databases",
			"GO"
		],
		"description": "List databases"
	},

	"List columns": {
		"prefix": "sqlListColumns",
		"body": [
			"-- List columns in all tables whose name is like '${1:TableName}'",
			"SELECT ",
			"\tTableName = tbl.table_schema + '.' + tbl.table_name, ",
			"\tColumnName = col.column_name, ",
			"\tColumnDataType = col.data_type",
			"FROM information_schema.tables tbl",
			"INNER JOIN information_schema.columns col ",
			"\tON col.table_name = tbl.table_name",
			"\tAND col.table_schema = tbl.table_schema",
			"",
			"WHERE tbl.table_type = 'base table' and tbl.table_name like '%${1:TableName}%'",
			"GO"
		],
		"description": "Lists all the columns and their types for tables matching a LIKE statement"
	},

	"Show space used by tables": {
		"prefix": "sqlGetSpaceUsed",
		"body": [
			"-- Get the space used by table ${1:TableName}",
			"SELECT TABL.name AS table_name,",
			"INDX.name AS index_name,",
			"SUM(PART.rows) AS rows_count,",
			"SUM(ALOC.total_pages) AS total_pages,",
			"SUM(ALOC.used_pages) AS used_pages,",
			"SUM(ALOC.data_pages) AS data_pages,",
			"(SUM(ALOC.total_pages)*8/1024) AS total_space_MB,",
			"(SUM(ALOC.used_pages)*8/1024) AS used_space_MB,",
			"(SUM(ALOC.data_pages)*8/1024) AS data_space_MB",
			"FROM sys.Tables AS TABL",
			"INNER JOIN sys.Indexes AS INDX",
			"ON TABL.object_id = INDX.object_id",
			"INNER JOIN sys.Partitions AS PART",
			"ON INDX.object_id = PART.object_id",
			"AND INDX.index_id = PART.index_id",
			"INNER JOIN sys.Allocation_Units AS ALOC",
			"ON PART.partition_id = ALOC.container_id",
			"WHERE TABL.name LIKE '%${1:TableName}%'",
			"AND INDX.object_id > 255",
			"AND INDX.index_id <= 1",
			"GROUP BY TABL.name, ",
			"INDX.object_id,",
			"INDX.index_id,",
			"INDX.name",
			"ORDER BY Object_Name(INDX.object_id),",
			"(SUM(ALOC.total_pages)*8/1024) DESC",
			"GO"
		],
		"description": "Get Space Used by Tables"
	}
}
```
## Next Steps

The basics of creating code snippets have been covered in this document, read on to find out more about:

* [Dashboards and Insight Widgets](dashboards.md) - Customize your dashboard with insight widgets.
