---
description: "Executing the SSMA Console (AccessToSQL)"
title: "Executing the SSMA Console (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: aa1bf665-8dc0-4259-b36f-46ae67197a43
author: cpichuka 
ms.author: cpichuka 
---
# Executing the SSMA Console (AccessToSQL)
Microsoft provides you with a robust set of script file commands and command line options to execute and control SSMA activities. The ensuing sections detail the same.  
  
## Project  Script File Commands  
The Project commands handle creating projects, opening, saving, and exiting projects.  
  
**Command**  
  
create-new-project: Creates a new SSMA project.  
  
**Script**  
  
-   `project-folder` indicates the folder of the project getting created.  
  
-   `project-name` indicates the name of the project. {string}  
  
-   `overwrite-if-exists`Optional attribute indicates if an existing project should be overwritten. {boolean}  
  
-   `project-type` is an optional attribute.  The following options are available for project-type:  
  
    -   sql-server-2005  
  
    -   sql-server-2008  
  
    -   sql-server-2012  
  
    -   sql-server-2014  
  
    -   sql-server-2016  
  
    -   sql-azure  
  
    Default is "sql-server-2008".  
  
**Example:**  
  
```xml  
<create-new-project  
  
  project-folder="<project-folder>"  
  
  project-name="<project-name>"  
  
  overwrite-if-exists="<true/false>"  
  
  project-type="<sql-server-2008 | sql-server-2005 | sql-server-2012 | sql-server-2014 | sql-azure>"  
  
/>  
```  
Attribute 'overwrite-if-exists' is **false** by default.  
  
Attribute 'project-type' is **sql-server-2008** by default.  
  
**Command**  
  
open-project: Opens an existing project.  
  
**Script**  
  
-   `project-folder` indicates the folder of the project getting created. The command fails if the specified folder doesn't exist.  {string}  
  
-   `project-name` indicates the name of the project. The command fails if the specified project doesn't exist.  {string}  
  
**Syntax Example:**  
  
```xml  
<open-project  
  
  project-folder="<project-folder>"  
  
  project-name="<project-name>"  
  
/>  
```  
**Note:** SSMA For Access Console application supports backward compatibility. You can open projects created by previous version of SSMA.  
  
**Command**  
  
save-project: Saves the migration project.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<save-project/>  
```  
**Command**  
  
close-project: Closes the migration project.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<close-project  
  
  if-modified="<save/error/ignore>"   (optional)  
  
/>  
```  
Attribute 'if-modified' is optional, **ignore** by default.  
  
## Database Connection Script File Commands  
The Database Connection commands help connect to the database.  
  
The **Browse** feature of the UI isn't supported in console.  
  
The **windows-authentication** and **port** parameters aren't applicable when connecting to SQL Azure.  
  
For more information on 'Creating Script Files', see [Creating Script Files &#40;AccessToSQL&#41;](../../ssma/access/creating-script-files-accesstosql.md).  
  
**Command**
  
connect-source-database  
  
- Performs connection to the source database and loads high-level metadata of the source database but not all of the metadata.  
  
- If the connection to the source can't be established, an error is generated and the console application stops further execution  
  
**Script**
  
Server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file.  
  
**Syntax Example:**  
  
```xml  
<connect-source-database  server="<server-unique-name>"/>  
```  
**Command**  
  
load-access-database: Used to load access database files  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<load-access-database  database-file="<Access-database>"/>  
```  
or  
  
```xml  
<load-access-database>  
  
  <access-database database-file="<Access-database1>"/>  
  
  <access-database database-file="<Access-database2>"/>  
  
</load-access-database>  
```  
**Command**  
  
force-load-source/target-database  
  
-   Loads the source metadata.  
  
-   Useful for working on migration project offline.  
  
-   If the connection to the source/target cannot be established, an error is generated and the console application stops further execution  
  
**Script**  
  
Requires one or several metabase nodes as command line parameter.  
  
**Syntax Example:**  
  
```xml  
<force-load  
  
  object-name="<object-name>"  
  
  metabase="<source/target>"/>  
```  
or  
  
```xml  
<force-load>  
  
  <metabase-object object-name="<object-name>"/>  
  
</force-load>  
```  
**Command**  
  
reconnect-source-database  
  
- Reconnects to the source database but doesn't load any metadata unlike the connect-source-database command.  
  
- If (re)connection with the source cannot be established, an error is generated and the console application stops further execution.  
  
**Script**
  
**Syntax Example:**  
  
```xml  
<reconnect-source-database  server="<server-unique-name>"/>  
```

**Command**
  
connect-target-database  
  
- Connects to the target SQL Server or SQL Azure Database and loads high-level metadata of the target database but not the metadata entirely.  
  
- If the connection to the target can't be established, an error is generated and the console application stops further execution.  
  
**Script**
  
Server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file  
  
**Syntax Example:**  
  
```xml  
<connect-target-database  server="<server-unique-name>"/>  
```  

**Command**
  
reconnect-target-database  
  
- Reconnects to the target database but doesn't load any metadata, unlike the connect-target-database command.  
  
- If (re)connection to the target cannot be established, an error is generated and the console application stops further execution.  
  
**Script**
  
**Syntax Example:**  
  
```xml  
<reconnect-target-database  server="<server-unique-name>"/>  
```  

## Report Script File Commands

The Report commands generate reports on the performance of various SSMA Console activities

**Command**
  
generate-assessment-report  
  
- Generates assessment reports on the source database.  
  
- If the source database connection is not performed before executing this command, an error is generated and the console application exits.  
  
- Failure to connect to the source database server during the command execution, also results in ending the console application.  
  
**Script**

- `assessment-report-folder:` Specifies folder where the assessment report can be stored.(optional attribute)  
  
- `object-name:` Specifies the object(s) considered for assessment report generation (It can have individual object names or a group object name).  
  
- `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `assessment-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
- `write-summary-report-to:` Specifies the path where the report will be generated.  
  
    If only the folder path is mentioned, then file by name **AssessmentReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    - `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    - `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<generate-assessment-report  
  
  object-name="ssma.Procedures"  
  
  object-type="category"  
  
  write-summary-report-to="<file>"             (optional)  
  
  verbose="<true/false>"                       (optional)  
  
  report-errors="<true/false>"                 (optional)  
  
  conversion-report-folder="<folder>"          (optional)  
  
  conversion-report-overwrite="<true/false>"   (optional)  
  
/>  
```

or
  
```xml  
<generate-assessment-report  
  
  conversion-report-folder="<folder>"            (optional)  
  
  conversion-report-overwrite="<true/false>"     (optional)  
  
>  
    <metabase-object object-name="ssma.Procedures"  
  
        object-type="category"/>  
  
</generate-assessment-report>  
```  
  
## Migration Script File Commands

The Migration commands convert the target database schema to the source schema and migrates data to the target server.  
  
The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
**Command**

convert-schema  
  
- Performs schema conversion from source to the target schema.  
  
- If the source or target database connection isn't performed before executing this command or the connection to the source or target database server fails during the command execution, an error is generated and the console application exits.  
  
**Script**

- `conversion-report-folder:` Specifies folder where the assessment report can be stored.(optional attribute)  
  
- `object-name:` Specifies the source object(s) considered for converting schema (It can have individual object names or a group object name).  
  
- `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
- `write-summary-report-to:` Specifies the path where the report will be generated.  
  
    If only the folder path is mentioned, then file by name **SchemaConversionReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    - `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    - `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<convert-schema  
  
  object-name="ssma.Procedures"  
  
  object-type="category"  
  write-summary-report-to="<filepath/folder>"     (optional)  
  
  verbose="<true/false>"                          (optional)  
  
  report-errors="<true/false>"                    (optional)  
  
  conversion-report-folder="<folder>"             (optional)  
  
  conversion-report-overwrite="<true/false>"      (optional)  
  
/>  
```

or  
  
```xml  
<convert-schema  
  
  conversion-report-folder="<folder>"         (optional)  
  
  conversion-report-overwrite="<true/false>"  (optional)  
  
  <metabase-object object-name="ssma.Procedures"  
  
    object-type="category"/>  
  
</convert-schema>  
```

**Command**
  
migrate-data  
  
- Migrates the source data to the target.  
  
**Script**
  
- `object-name:` Specifies the source object(s) considered for migrating data (It can have individual object names or a group object name).  
  
- `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `write-summary-report-to:` Specifies the path where the report will be generated.  
  
    If only the folder path is mentioned, then file by name **DataMigrationReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    - `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    - `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<migrate-data  
  
  write-summary-report-to="<filepath/folder>"  
  
  report-errors="true" verbose="true">  
  
    <metabase-object object-name="ssma.TT1"/>  
  
    <metabase-object object-name="ssma.TT2"/>  
  
    <metabase-object object-name="ssma.TT3"/>  
  
    <data-migration-connection  
  
      source-use-last-used="true"/source-server="<server-unique-name>"  
  
      target-use-last-used="true"/target-server="<server-unique-name>"/>  
  
</migrate-data>  
```

or  
  
```xml  
<migrate-data  
  
  object-name="ssma.Tables"  
  
  object-type="category"  
  
  write-summary-report-to="<filepath/folder>"  
  
  report-errors="true" verbose="true"/>  
```

**Command**
  
link-tables: This command links the source (Access) table to the target table.  
  
**Script**

**Syntax Example:**  
  
```xml  
<link-tables>  
  
  <metabase-object object-name="AccessDatabase.table1" object-type="Tables"/>  
  
  <metabase-object object-name="AccessDatabase.table2" object-type="Tables"/>  
  
</link-tables>  
```

or  
  
```xml  
<link-tables>  
  
  <metabase-object object-name="AccessDatabase.Tables" object-type="category"/>  
  
</link-tables>  
```

**Command**
  
unlink-tables: This command unlinks the source (Access) table from the target table.  
  
**Script**
  
**Syntax Examples:**  
  
```xml  
<unlink-tables>  
  
  <metabase-object object-name="AccessDatabase.table1" object-type="Tables"/>  
  
  <metabase-object object-name="AccessDatabase.table2" object-type="Tables"/>  
  
</unlink-tables>  
```

or  
  
```xml  
<unlink-tables>  
  
  <metabase-object object-name="AccessDatabase.Tables" object-type="category"/>  
  
</unlink-tables>  
```  
  
## Migration Preparation Script File Commands

The Migration Preparation command starts schema mapping between the source and target databases.  
  
**Command**
  
map-schema: Schema mapping of source database to the target schema.  
  
**Script**
  
- `source-schema` specifies the source schema we intend to migrate.  
  
- `sql-server-schema` specifies the target schema where we want it to be migrated.  
  
**Syntax Example:**  
  
```xml  
<map-schema source-schema="source-schema"  
  
            sql-server-schema="target-schema"/>  
```  
  
## Manageability Commands

The Manageability commands help synchronize the target database objects with the source database.  
  
The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
**Command**

synchronize-target  
  
- Synchronizes the target objects with the target database.  
  
- If this command is executed against the source database, you get an error.  
  
- If the target database connection is not performed before executing this command or the connection to the target database server fails during the command execution, an error is generated and the console application exits.  
  
**Script**
  
- `object-name:` Specifies the target object(s) considered for synchronizing with target database (It can have individual object names or a group object name).  
  
- `object-type:` Specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `on-error:` Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:  
  
    - report-total-as-warning  
  
    - report-each-as-warning  
  
    - fail-script  
  
- `report-errors-to:` Specifies location of error report for the synchronization operation (optional attribute)           if only folder path is given, then file by name **TargetSynchronizationReport.XML** is created.  
  
**Syntax Example:**  
  
```xml  
<synchronize-target  
  
  object-name="ots_triggers.dbo"  
  
  on-error="<report-total-as-warning|  
  
             report-each-as-warning|  
  
             fail-script>"              (optional)  
  
  report-errors-to="<file-name>"        (optional)  
  
/>  
```

or  
  
```xml  
<synchronize-target  
  
  object-name="ssma.dbo.Procedures"  
  
  object-type="category"/>  
```

or  
  
```xml  
<synchronize-target>  
  
  <metabase-object object-name="ssma.dbo.TT1"/>  
  
  <metabase-object object-name="ssma.dbo.TT2"/>  
  
  <metabase-object object-name="ssma.dbo.TT3"/>  
  
</synchronize-target>  
```

**Command**
  
refresh-from-database  
  
- Refreshes the source objects from database.  
  
- If this command is executed against the target database, an error is generated.  
  
**Script**
  
Requires one or several metabase nodes as command line parameter.  
  
- `object-name:` Specifies the source object(s) considered for refreshing from source database (It can have individual object names or a group object name).  
  
- `object-type:` Specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `on-error:` Specifies whether to specify refresh errors as warnings or error. Available options for on-error:  
  
    - report-total-as-warning  
  
    - report-each-as-warning  
  
    - fail-script  
  
- `report-errors-to:` Specifies location of error report for the refresh operation (optional attribute) if only folder path is given, then file by name **SourceDBRefreshReport.XML** is created.  
  
**Syntax Example:**  
  
```xml  
<refresh-from-database  
  
  object-name="ssma.TT1"  
  
  on-error="<report-total-as-warning|  
  
             report-each-as-warning|  
  
             fail-script>"              (optional)  
  
  report-errors-to="<file-name>"        (optional)  
  
/>  
```

or  
  
```xml  
<refresh-from-database  
  
  object-name="ssma.Procedures"  
  
  object-type="category"/>  
```

or  
  
```xml  
<refresh-from-database>  
  
  <metabase-object object-name="ssma.TT_1"/>  
  
</refresh-from-database>  
```  
  
## Script Generation Script File Commands

The Script Generation commands help save the console output in a script file.  
  
**Command**
  
save-as-script  
  
Used to save the Scripts of the objects to a file mentioned when metabase=target, this is an alternative to synchronization command where in we get the scripts and execute the same on the target database.  
  
**Script**
  
Requires one or several metabase nodes as command line parameter.  
  
- `object-name:` Specifies the object(s) whose scripts are to be saved. (It can have individual object names or a group object name)  
  
- `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
- `metabase:` Specifies whether it's the source or target metabase.  
  
- `destination:` Specifies the path or the folder where the script has to be saved; if the file name is not given then a file name in the format (object_name   attribute value).out  
  
- `overwrite:` if true then it overwrites if same filenames exist. It can have the values (true/false).  
  
**Syntax Example:**  
  
```xml  
<save-as-script  
  
  metabase="<source/target>"  
  
  object-name="ssma.dbo.Procedures"  
  
  object-type="category"  
  
  destination="<file/folder>"  
  
  overwrite="<true/false>"             (optional)  
  
/>  
```

or  
  
```xml  
<save-as-script  
  
  metabase="<source/target>"  
  
  destination="<file/folder>"  
  
    <metabase-object object-name="ssma.dbo.Procedures"  
  
                     object-type="category"/>  
  
</save-as-script>  
```  
  
## Next steps

For information on command line options, see [Command Line Options in SSMA Console &#40;AccessToSQL&#41;](../../ssma/access/command-line-options-in-ssma-console-accesstosql.md) .  
  
For information on Sample console script files, see [Working with the Sample Console Script FilesExecuting the SSMA Console &#40;AccessToSQL&#41;](../../ssma/access/working-sample-console-script-filesexecuting-ssma-console-accesstosql.md)  
  
The next step depends on your project requirements:  
  
- For specifying a password or export/ import passwords, see [Managing Passwords &#40;AccessToSQL&#41;](../../ssma/access/managing-passwords-accesstosql.md).  
  
- For generating reports, see [Generating Reports &#40;AccessToSQL&#41;](../../ssma/access/generating-reports-accesstosql.md).  
  
- For troubleshooting issues in console, see [Troubleshooting &#40;AccessToSQL&#41;](../../ssma/access/troubleshooting-accesstosql.md).  
