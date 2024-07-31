---
title: "Create variable value files (Db2ToSQL)"
description: A variable value file is an XML file comprising the parameter values of commands.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Create variable value files (Db2ToSQL)

A variable value file is an XML file comprising the parameter values of commands, such as the source or destination server name that frequently change from one server migration to another. When a large number of database migrations occur, multiple variable files for storing the value of each source server are created and referenced in a main script file with the `-v` switch at command line. This helps in maintaining static values in a few script files with the variable values in multiple variable files.

## Remarks

Variable names are prefixed and suffixed with a $ (dollar) symbol. If the variables aren't assigned a value in the variable value file, you can encounter an error during the parsing of the script file, which results in stalling the console execution process.

The escape character for `$` is `$$`. If the value of a variable or static value of a parameter contains `$` (dollar) symbol, then `$$` must be specified to treat it as a character instead of a variable.

For maintainability purposes, variables can be declared inside `variable-group` elements for logical separation of user defined variables. Usage of this element isn't mandatory.

## Examples

### A. Sample of variable value file commands for project

```xml
<variables>
  <variable-group name="ProjectSpecs">
    <variable name="$project_folder$" value="<project-folder>"/>
    <variable name="$project_name$" value="<project-name>"/>
    <variable name="$project_overwrite$" value="<true/false>"/>
    <variable name="$project_type$" value="<project-type>"/>
  </variable-group>
</variables>
```

### B. Sample of variable value file commands for server

```xml
<variables>
  <variable-group name="SQLServerParams">
    <variable-group name="SqlServerConnectionParams">
      <variable name="$TargetServerName$" value="<server-name>"/>
      <variable name="$TargetDB$" value="<database-name>"/>
      <variable name="$TargetUserName$" value="<user-name>"/>
      <variable name="$TargetPassword$" value="<password>"/>
      <variable name="$TrustedConnection$" value="<true/false>"/>
    </variable-group>
    <variable-group name="SqlServerObjectParams">
      <variable name="$ObjectName1$" value="<object-name>"/>
      <variable name="$ObjectName2$" value="<object-name>"/>
    </variable-group>
  </variable-group>
</variables>
```

## Next step

> [!div class="nextstepaction"]
> [Create the Server Connection Files (Db2ToSQL)](creating-the-server-connection-files-db2tosql.md)
