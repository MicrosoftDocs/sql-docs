---
description: "Executing the SSMA Console (SybaseToSQL)"
title: "Executing the SSMA Console (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Sybase Console,Database Connection Commands"
  - "Sybase Console,Manageability Commands"
  - "Sybase Console,Migration Commands"
  - "Sybase Console,Migration Preparation Command"
  - "Sybase Console,Project Commands"
  - "Sybase Console,Report Commands"
  - "Sybase Console,Script File Commands"
  - "Sybase Console,Script Generation Commands"
ms.assetid: ea8950b7-fabc-4aa4-89f8-9573a2617d70
author: cpichuka 
ms.author: cpichuka 
---
# Executing the SSMA Console (SybaseToSQL)
Microsoft provides you with a robust set of script file commands to execute and control SSMA activities. The ensuing sections detail the same.  
  
## Script file commands  
The console application uses certain standard script file commands as enumerated in this section.  
  
## Project commands  
The Project commands handle creating projects, opening, saving, and exiting projects.  
  
### create-new-project  
This command creates a new SSMA project.  
  
-   `project-folder` indicates the folder of the project getting created.  
  
-   `project-name` indicates the name of the project. {string}  
  
-   `overwrite-if-exists`Optional attribute indicates whether an existing project should be overwritten. {boolean}  
  
-   `project-type:`Optional attribute. Indicates the project type, that is "sql-server-2005" project or "sql-server-2008" project or "sql-server-2012" project or "sql-server-2014" project or "sql-azure" project. Default is "sql-server-2008."  
  
**Syntax example:**  
  
```xml  
<create-new-project  
  
  project-folder="<project-folder>"  
  
  project-name="<project-name>"  
  
  overwrite-if-exists="<true/false>" (optional)  
  
   project-type="<sql-server-2008/sql-server-2005/sql-server-2012/sql-server-2014/sql-azure>"  
/>  
```  
Attribute 'overwrite-if-exists' is **false** by default.  
  
Attribute 'project-type' is **sql-server-2008** by default.  
  
### open-project  
This command opens the project.

-   `project-folder` indicates the folder of the project getting created. The command fails if the specified folder does not exist.  {string}  
  
-   `project-name` indicates the name of the project. The command fails if the specified project does not exist.  {string}  
  
**Syntax example:**  
  
```xml  
<open-project  
  
  project-folder="<project-folder>"  
  
  project-name="<project-name>"  
  
/>  
```  
> [!NOTE]  
> The SSMA for SAP ASE Console Application supports backward compatibility. You can use it to open projects created by previous version of SSMA.  
  
### save-project  
This command saves the migration project.  
  
**Syntax example:**  
  
```xml  
<save-project/>  
```  
  
### close-project  
This command closes the migration project.  
  
**Syntax example:**  
  
```xml  
<close-project   
  if-modified="<save/error/ignore>"   (optional)  
/>  
```  
Attribute 'if-modified' is optional, **ignore** by default.  
  
## Database Connection commands  
The Database Connection commands help connect to the database.  
  
> [!NOTE]  
> - The **Browse** feature of the UI is not supported in console.  
> - For more information on 'Creating Script Files', see [Creating Script Files &#40;SybaseToSQL&#41;](../../ssma/sybase/creating-script-files-sybasetosql.md).  
  
### connect-source-database  
This command performs connection to the source database and loads high-level metadata of the source database, but not all of the metadata.
  
If the connection to the source cannot be established, an error is generated and the console application stops further execution.
  
The server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file.  
  
**Syntax example:**  
  
```xml  
<connect-source-database  server="<server-unique-name>"/>  
```  
  
### force-load-source/target-database  
This command loads the source metadata, and it is useful for working on migration project offline.  
  
If the connection to the source/target cannot be established, an error is generated and the console application stops further execution.  
  
This command requires one or several metabase nodes as command-line parameter.  
  
**Syntax example:**  
  
```xml  
<force-load metabase="<source/target>" >  
  
  <metabase-object object-name="<object-name>"/>  
  
</force-load>  
```  
  
### reconnect-source-database  
This command reconnects to the source database but does not load any metadata unlike the connect-source-database command.  
  
If (re)connection with the source cannot be established, an error is generated and the console application stops further execution.  
  
**Syntax example:**  
  
```xml  
<reconnect-source-database  server="<server-unique-name>"/>  
```  
  
### connect-target-database  
This command connects to the target SQL Server database and loads high-level metadata of the target database but not the metadata entirely.  
  
If the connection to the target cannot be established, an error is generated and the console application stops further execution.  
  
The server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file.  
  
**Syntax example:**  
  
```xml  
<connect-target-database  server="<server-unique-name>"/>  
```  
  
### reconnect-target-database  
  
This command reconnects to the target database but does not load any metadata, unlike the connect-target-database command.  
  
If (re)connection to the target cannot be established, an error is generated and the console application stops further execution.  
  
**Syntax example:**  
  
```xml  
<reconnect-target-database  server="<server-unique-name>"/>  
```  
  
## Report commands  
The Report commands generate reports on the performance of various SSMA Console activities.  
  
### generate-assessment-report  
  
This command generates assessment reports on the source database.  
  
If the source database connection is not performed before executing this command, an error is generated and the console application exits.  
  
Failure to connect to the source database server during the command execution, also results in terminating the console application.  
  
-   `conversion-report-folder:` Specifies the folder in which the assessment report can be stored. (optional attribute)  
  
-   `object-name:` Specifies the object(s) considered for assessment report generation (supports individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type will be "category").  
  
-   `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-summary-report-to:` Specifies the path at which the report will be generated.  
  
    If only the folder path is mentioned, then file by name **AssessmentReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax example:**  
  
```xml  
<generate-assessment-report  
  
  object-name="<object-name>"  
  
  object-type="<object-category>"  
  
  write-summary-report-to="<file-name/folder-name>"             (optional)  
  
  verbose="<true/false>"                       (optional)  
  
  report-errors="<true/false>"                 (optional)  
  
  assessment-report-folder="<folder-name>"          (optional)  
  
  conversion-report-overwrite="<true/false>"   (optional)  
  
/>  
```  
or  
  
```xml  
<generate-assessment-report  
  
  assessment-report-folder="<folder-name>"            (optional)  
  
  conversion-report-overwrite="<true/false>"     (optional)  
  
>  
<metabase-object object-name="<object-name>"  
  
        object-type="<object-category>"/>  
  
</generate-assessment-report>  
```  
  
## Migration commands  
The Migration commands convert the target database schema to the source schema and migrate data to the target server.  
  
### convert-schema  
This command performs schema conversion from source to the target schema.  
  
If the source or target database connection is not performed before executing this command or the connection to the source or target database server fails during the command execution, an error is generated and the console application exits.  
  
-   `conversion-report-folder:` Specifies folder in which the assessment report can be stored. (optional attribute)  
  
-   `object-name:` Specifies the source object(s) considered for converting schema (supports individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type will be "category").  
  
-   `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-summary-report-to:` Specifies the path at which the summary report will be generated.  
  
    If only the folder path is mentioned, then file by name **SchemaConversionReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax example:**  
  
```xml  
<convert-schema  
  
  object-name="<object-name>"  
  
  object-type="<object-category>"  
  write-summary-report-to="<file-name/folder-name>"     (optional)  
  
  verbose="<true/false>"                          (optional)  
  
  report-errors="<true/false>"                    (optional)  
  
  conversion-report-folder="<folder-name>"             (optional)  
  
  conversion-report-overwrite="<true/false>"      (optional)  
  
/>  
```  
or  
  
```xml  
<convert-schema  
  
  conversion-report-folder="<folder-name>"         (optional)  
  
  conversion-report-overwrite="<true/false>"> (optional)  
  
  <metabase-object object-name="<object-name>"  
  
    object-type="<object-category>"/>  
  
</convert-schema>  
```  
  
### migrate-data  
This command migrates the source data to the target.  
  
-   `object-name:` Specifies the source object(s) considered for migrating data (supports individual object names or a group object name).  
  
-   `object-type:` specifies the type of the object called out in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `write-summary-report-to:` Specifies the path at which the report will be generated.  
  
    If only the folder path is mentioned, then file by name **DataMigrationReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further subcategories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax example:**  
  
```xml  
<migrate-data  
  
  write-summary-report-to="<file-name/folder-name>"  
  
  report-errors="<true/false>" verbose="<true/false>">  
  
    <metabase-object object-name="<object-name>"/>  
  
    <metabase-object object-name="<object-name>"/>  
  
    <metabase-object object-name="<object-name>"/>  
  
    <data-migration-connection  
  
      source-use-last-used="true"/source-server="<server-unique-name>"  
  
      target-use-last-used="true"/target-server="<server-unique-name>"/>  
  
</migrate-data>  
```  
or  
  
```xml  
<migrate-data  
  
  object-name="<object-name>"  
  
  object-type="<object-category>"  
  
  write-summary-report-to="<file-name/folder-name>"  
  
  report-errors="<true/false>" verbose="<true/false>"/>  
```  
  
## Migration Preparation command  
The Migration Preparation command initiates schema mapping between the source and target databases.  
  
> [!NOTE]  
> The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
### map-schema  
This command provides the schema mapping of the source database to the target schema.  
  
-   `source-schema` Specifies the source schema to migrate.  
  
-   `sql-server-schema` Specifies the target schema to which the source schema will be migrated.  
  
**Syntax example:**  
  
```xml  
<map-schema source-schema="<source-schema>"  
  
sql-server-schema="<target-schema>"/>  
```  
  
## Manageability commands  
The Manageability commands help synchronize the target database objects with the source database.  
  
> [!NOTE]  
> The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
### synchronize-target  
This command synchronizes the target objects with the target database.  
 
If this command is executed against the source database, an error is encountered.  
  
If the target database connection is not performed before executing this command or the connection to the target database server fails during the command execution, an error is generated and the console application exits.  
  
-   `object-name:` Specifies the target object(s) considered for synchronizing with target database (supports individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object called out in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `on-error:` Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:  
  
    -   report-total-as-warning  
  
    -   report-each-as-warning  
  
    -   fail-script  
  
-   `report-errors-to:` Specifies location of error report for the synchronization operation (optional attribute). If only folder path is given, then file by name **TargetSynchronizationReport.XML** is created.  
  
**Syntax example:**  
  
```xml  
<synchronize-target  
  
object-name="<object-name>"  
  
on-error="<report-total-as-warning/  
  
report-each-as-warning/  
  
fail-script>" (optional)  
  
  report-errors-to="<file-name/folder-name>"        (optional)  
  
/>  
```  
or  
  
```xml  
<synchronize-target  
  
  object-name="<object-name>"  
  
  object-type="<object-category>"/>  
```  
or  
  
```xml  
<synchronize-target>  
  
  <metabase-object object-name="<object-name>"/>  
  
  <metabase-object object-name="<object-name>"/>  
  
  <metabase-object object-name="<object-name>"/>  
  
</synchronize-target>  
```  
  
### refresh-from-database  
This command refreshes the source objects from database.  
  
If this command is executed against the target database, an error is generated.  
  
This command requires one or several metabase nodes as command-line parameter.  
  
-   `object-name:` Specifies the source object(s) considered for refreshing from source database (supports individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `on-error:` Specifies whether to call out refresh errors as warnings or errors. Available options for on-error:  
  
    -   report-total-as-warning  
  
    -   report-each-as-warning  
  
    -   fail-script  
  
-   `report-errors-to:` Specifies location of error report for the refresh operation (optional attribute). If only folder path is given, then file by name **SourceDBRefreshReport.XML** is created.  
  
**Syntax example:**  
  
```xml  
<refresh-from-database  
  
  object-name="<object-name>"  
  
  on-error="<report-total-as-warning/  
  
             report-each-as-warning/  
  
             fail-script>"              (optional)  
  
  report-errors-to="<file-name/folder-name>"        (optional)  
  
/>  
```  
or  
  
```xml  
<refresh-from-database  
  
  object-name="<object-name>"  
  
  object-type="<object-category>" />  
```  
or  
  
```xml  
<refresh-from-database>  
  
  <metabase-object object-name="<object-name>"/>  
  
</refresh-from-database>  
```  
  
## Script Generation commands  
The Script Generation commands perform dual tasks: they help save the console output in a script file, and they record the T-SQL output to the console or a file based on the parameter you specify.  
  
### save-as-script  
This command is used to save the Scripts of the objects to a file mentioned when metabase=target. This is an alternative to synchronization command in that we get the scripts and execute the same on the target database.  
  
This command requires one or several metabase nodes as command-line parameter.  
  
-   `object-name:` Specifies the object(s) whose scripts are to be saved (supports individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type will be "category").  
  
-   `metabase:` Specifies whether it is the source or target metabase.  
  
-   `destination:` Specifies the path or the folder in which the script must be saved. If the file name is not given, then a file name in the format (object_name   attribute value).out will be provided.
  
-   `overwrite:` If true, then it overwrites the same filename if it exists. It can have the values (true/false).  
  
**Syntax example:**  
  
```xml  
<save-as-script  
  
  metabase="<source/target>"  
  
  object-name="<object-name>"  
  
  object-type="<object-category>"  
  
  destination="<file-name/folder-name>"  
  
  overwrite="<true/false>"   (optional)  
  
/>  
```  
or  
  
```xml  
<save-as-script  
  
  metabase="<source/target>"  
  
  destination="<file-name/folder-name>"  
  
    <metabase-object object-name="<object-name>"  
  
                     object-type="<object-category>"/>  
  
</save-as-script>  
```  
  
### convert-sql-statement
This command converts the SQL statement.  
  
-   `context` Specifies the schema name.  
  
-   `destination` Specifies whether the output should be stored in a file.  
  
    If this attribute is not specified, then the converted T-SQL statement is displayed on the console. (optional attribute)  
  
-   `conversion-report-folder` Specifies the folder in which the assessment report can be stored. (optional attribute)  
  
-   `conversion-report-overwrite` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-converted-sql-to` specifies the file (or) folder path to which the converted T-SQL should be stored. When a folder path is specified along with the `sql-files` attribute, each source file has a corresponding target T-SQL file created under the specified folder. When a folder path is specified along with the `sql` attribute, the converted T-SQL is written to a file named Result.out under the specified folder.  
  
-   `sql` specifies the Sybase sql statements to be converted, one or more statements can be separated using a ";"  
  
-   `sql-files` specifies the path of the sql files that has to be converted to T-SQL code.  
  
-   `write-summary-report-to` specifies the path where the summary report will be generated. If only the folder path is mentioned, then file by name **ConvertSQLReport.XML** is created. (optional attribute)  
  
    Summary report creation has two further subcategories, namely:  
  
    -   report-errors (="true/false", with default as "false" (optional attributes)).  
  
    -   verbose (="true/false", with default as "false" (optional attributes)).  
  
This command requires one or several metabase nodes as command-line parameter.  
  
**Syntax example:**  
  
```  
<convert-sql-statement  
  
       context="<database-name>.<schema-name>"  
  
        conversion-report-folder="<folder-name>"  
  
        conversion-report-overwrite="<true/false>"  
  
        write-summary-report-to="<file-name/folder-name>"   (optional)  
  
        verbose="<true/false>"   (optional)  
  
        report-errors="<true/false>"   (optional)  
  
        destination="<stdout/file>"   (optional)  
  
        write-converted-sql-to ="<file-name/folder-name>"  
  
        sql="SELECT 1 FROM DUAL;">  
  
    <output-window suppress-messages="<true/false>" />  
  
</convert-sql-statement>  
```  
or  
  
```  
<convert-sql-statement  
  
         context="<database-name>.<schema-name>"  
  
         conversion-report-folder="<folder-name>"  
  
         conversion-report-overwrite="<true/false>"  
  
         write-summary-report-to="<file-name/folder-name>"   (optional)  
  
         verbose="<true/false>"   (optional)  
  
         report-errors="<true/false>"   (optional)  
  
         destination="<stdout/file>"   (optional)  
  
         write-converted-sql-to ="<file-name/folder-name>"  
  
         sql-files="<folder-name>\*.sql"  
  
/>  
```  
or  
  
```  
<convert-sql-statement  
  
         context="<database-name>.<schema-name>"  
  
         conversion-report-folder="<folder-name>"  
  
         conversion-report-overwrite="<true/false>"  
  
         sql-files="<folder-name>\*.sql"  
  
/>  
```  
  
## Next steps  
For information on command-line options, see [Command-line options in the SSMA Console (AccessToSQL)](../access/command-line-options-in-ssma-console-accesstosql.md).  
  
For information on a sample console script file, see [Working with the Sample Console Script Files &#40;SybaseToSQL&#41;](../../ssma/sybase/working-with-the-sample-console-script-files-sybasetosql.md)  
  
The next step depends on your project requirements:  
  
-   For specifying a password or export/ import passwords, see [Managing Passwords &#40;SybaseToSQL&#41;](../../ssma/sybase/managing-passwords-sybasetosql.md).  
  
-   For generating reports, see [Generating Reports &#40;SybaseToSQL&#41;](../../ssma/sybase/generating-reports-sybasetosql.md).  
  
-   For troubleshooting issues in console, see [Troubleshooting &#40;SybaseToSQL&#41;](../../ssma/sybase/troubleshooting-sybasetosql.md).  
  
