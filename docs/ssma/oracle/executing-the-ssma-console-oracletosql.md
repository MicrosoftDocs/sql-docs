---
description: "Executing the SSMA Console (OracleToSQL)"
title: "Executing the SSMA Console (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle SSMA Console"
  - "Script File Commands, Script Generation Commands,Manageability Commands"
  - "Script File Commands,Project Commands"
ms.assetid: 7228ccba-c69f-4b4c-8664-01a2750183c5
author: cpichuka 
ms.author: cpichuka 
---
# Executing the SSMA Console (OracleToSQL)
Microsoft provides you with a robust set of script file commands to execute and control SSMA activities. The console application uses certain standard script file commands as enumerated in this section.  
  
## Project Script File Commands  
The Project commands handle creating projects, opening, saving, and exiting projects.  
  
**Command**  
  
create-new-project  
                  : Creates a new SSMA project.  
  
**Script**  
  
-   `project-folder` indicates the folder of the project getting created.  
  
-   `project-name` indicates the name of the project. {string}  
  
-   `overwrite-if-exists`Optional attribute indicates if an existing project should be overwritten. {boolean}  
  
-   `project-type:`Optional attribute. Indicates the project type i.e. "sql-server-2005" project or "sql-server-2008" project or "sql-server-2012" project or "sql-server-2014" project or "sql-azure". Default is "sql-server-2014".  
  
**Example:**  
  
```xml  
<create-new-project  
  
   project-folder="<project-folder>"  
  
   project-name="<project-name>"  
  
   overwrite-if-exists="<true/false>"   (optional)  
  
   project-type="<sql-server-2008 | sql-server-2005 | sql-server-2012 | sql-server-2014>"   (optional)  
  
/>  
```  
Attribute 'overwrite-if-exists' is **false** by default.  
  
Attribute 'project-type' is **sql-server-2008** by default.  
  
**Command**  
  
open-project: Opens an existing project.  
  
**Script**  
  
-   `project-folder` indicates the folder of the project getting created. The command fails if the specified folder does not exist.  {string}  
  
-   `project-name` indicates the name of the project. The command fails if the specified project does not exist.  {string}  
  
**Syntax Example:**  
  
```xml  
<open-project  
  
   project-folder="<project-folder>"  
  
   project-name="<project-name>"  
  
/>  
```  
SSMA for Oracle Console Application supports backward compatibility. You will be able to open projects created by previous version of SSMA.  
  
**Command**  
  
save-project  
  
Saves the migration project.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<save-project/>  
```  
**Command**  
  
close-project  
  
Closes the migration project.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<close-project  
  
   if-modified="<save/error/ignore>"   (optional)  
  
/>  
```  
  
## Database Connection Script File Commands  
The Database Connection commands help connect to the database.  
  
-   The **Browse** feature of the UI is not supported in console.  
  
-   For more information on 'Creating Script Files', see [Creating Script Files &#40;OracleToSQL&#41;](../../ssma/oracle/creating-script-files-oracletosql.md).  
  
**Command**  
  
connect-source-database  
  
-   Performs connection to the source database and loads high level metadata of the source database but not all of the metadata.  
  
-   If the connection to the source cannot be established, an error is generated and the console application stops further execution  
  
**Script**  
  
Server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file.  
  
**Syntax Example:**  
  
```xml  
<connect-source-database  server="<server-unique-name>"/>  
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
<force-load object-name="<object-name>"  
  
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
  
-   Reconnects to the source database but does not load any metadata unlike the connect-source-database command.  
  
-   If (re)connection with the source cannot be established, an error is generated and the console application stops further execution.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<reconnect-source-database  server="<server-unique-name>"/>  
```  
**Command**  
  
connect-target-database  
  
-   Connects to the target SQL Server database and loads high level metadata of the target database but not the metadata entirely.  
  
-   If the connection to the target cannot be established, an error is generated and the console application stops further execution.  
  
**Script**  
  
Server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file  
  
**Syntax Example:**  
  
```xml  
<connect-target-database  server="<server-unique-name>"/>  
```  
**Command**  
  
reconnect-target-database  
  
-   Reconnects to the target database but does not load any metadata, unlike the connect-target-database command.  
  
-   If (re)connection to the target cannot be established, an error is generated and the console application stops further execution.  
  
**Script**  
  
**Syntax Example:**  
  
```xml  
<reconnect-target-database  server="<server-unique-name>"/>  
```  
  
## Report Script File  Commands  
The Report commands generate reports on the performance of various SSMA Console activities.  
  
**Command**  
  
generate-assessment-report  
  
-   Generates assessment reports on the source database.  
  
-   If the source database connection is not performed before executing this command, an error is generated and the console application exits.  
  
-   Failure to connect to the source database server during the command execution, also results in terminating the console application.  
  
**Script**  
  
-   `conversion-report-folder:` Specifies folder where the assessment report can to be stored.(optional attribute)  
  
-   `object-name:` Specifies the object(s) considered for assessment report generation (It can have indivdual object names or a group object name).  
  
-   `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-summary-report-to:` Specifies the path where the summary report will be generated.  
  
    If only the folder path is mentioned, then file by name **AssessmentReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further sub-categories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<generate-assessment-report  
  
   object-name="<object-name>"  
  
   object-type="<object-category>"  
  
   write-summary-report-to="<file>"   (optional)  
  
   verbose="<true/false>"   (optional)  
  
   report-errors="<true/false>"   (optional)  
  
   assessment-report-folder="<folder-name>"   (optional)  
  
   conversion-report-overwrite="<true/false>"   (optional)  
  
/>  
```  
or  
  
```xml  
<generate-assessment-report  
  
   conversion-report-folder="<folder-name>"   (optional)  
  
   conversion-report-overwrite="<true/false>"   (optional)  
  
>  
  
      <metabase-object object-name="<object-name>"  
  
         object-type="<object-category>"/>  
  
</generate-assessment-report>  
```  
  
## Migration Script File Commands  
The Migration commands convert the target database schema to the source schema and migrates data to the target server.  
  
The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
**Command**  
  
convert-schema  
  
-   Performs schema conversion from source to the target schema.  
  
-   If the source or target database connection is not performed before executing this command or the connection to the source or target database server fails during the command execution, an error is generated and the console application exits.  
  
**Script**  
  
-   `conversion-report-folder:` Specifies folder where the assessment report can to be stored.(optional attribute)  
  
-   `object-name:` Specifies the source object(s) considered for converting schema (It can have indivdual object names or a group object name).  
  
-   `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-summary-report-to:` Specifies the path where the summary report will be generated.  
  
    If only the folder path is mentioned, then file by name **SchemaConversionReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further sub-categories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<convert-schema  
  
   object-name="<object-name>"  
  
   object-type="<object-category>"  
  
   write-summary-report-to="<file-name/folder-name>"   (optional)  
  
   verbose="<true/false>"   (optional)  
  
   report-errors="<true/false>"   (optional)  
  
   conversion-report-folder="<folder-name>"   (optional)  
  
   conversion-report-overwrite="<true/false>"   (optional)  
  
/>  
```  
or  
  
```xml  
<convert-schema  
  
   conversion-report-folder="<folder-name>"   (optional)  
  
   conversion-report-overwrite="<true/false>"   (optional)  
  
      <metabase-object object-name="<object-name>"  
  
         object-type="<object-category>"/>  
  
</convert-schema>  
```  
**Command**  
  
migrate-data  
  
Migrates the source data to the target.  
  
**Script**  
  
-   `conversion-report-folder:` Specifies folder where the assessment report can to be stored.(optional attribute)  
  
-   `object-name:` Specifies the source object(s) considered for migrating data (It can have indivdual object names or a group object name).  
  
-   `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `conversion-report-overwrite:` Specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-summary-report-to:` Specifies the path where the summary report will be generated.  
  
    If only the folder path is mentioned,then file by name **DataMigrationReport&lt;n&gt;.XML** is created. (optional attribute)  
  
    Report creation has two further sub-categories:  
  
    -   `report-errors` (="true/false", with default as "false" (optional attributes))  
  
    -   `verbose` (="true/false", with default as "false" (optional attributes))  
  
**Syntax Example:**  
  
```xml  
<migrate-data  
  
   write-summary-report-to="<file-name/folder-name>"  
  
   report-errors="<true/false>"  
  
   verbose="<true/false>">  
  
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
  
   report-errors="<true/false>"  
  
   verbose="<true/false>"/>  
```  
  
## Migration Preparation Script File Commands  
The Migration Preparation command initiates schema mapping between the source and target databases.  
  
**Command**  
  
map-schema  
  
Schema mapping of source database to the target schema.  
  
Migrates the source data to the target.  
  
**Script**  
  
-   `source-schema` specifies the source schema we intend to migrate.  
  
-   `sql-server-schema` specifies the target schema where we want it to be migrated.  
  
**Syntax Example:**  
  
```xml  
<map-schema  
  
   source-schema="<source-schema>"  
  
   sql-server-schema="<target-schema>"/>  
```  
  
## Manageability Script File Commands  
The Manageability commands help synchronize the target database objects with the source database. The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.  
  
**Command**  
  
synchronize-target  
  
-   Synchronizes the target objects with the target database.  
  
-   If this command is executed against the source database, an error is encountered.  
  
-   If the target database connection is not performed before executing this command or the connection to the target database server fails during the command execution, an error is generated and the console application exits.  
  
**Script**  
  
-   `object-name:` Specifies the target object(s) considered for synchronizing with target database (It can have indivdual object names or a group object name).  
  
-   `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `on-error:` Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:  
  
    -   report-total-as-warning  
  
    -   report-each-as-warning  
  
    -   fail-script  
  
-   `report-errors-to:` Specifies location of error report for the synchronization operation (optional attribute)           if only folder path is given, then file by name **TargetSynchronizationReport.XML** is created.  
  
**Syntax Example:**  
  
```xml  
<synchronize-target  
  
   object-name="<object-name>"  
  
   on-error="<report-total-as-warning/  
  
               report-each-as-warning/  
  
               fail-script>"   (optional)  
  
   report-errors-to="<file-name/folder-name>"   (optional)  
  
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
**Command**  
  
refresh-from-database  
  
-   Refreshes the source objects from database.  
  
-   If this command is executed against the target database, an error is generated.  
  
**Script**  
  
Requires one or several metabase nodes as command line parameter.  
  
-   `object-name:` Specifies the source object(s) considered for refreshing from source database (It can have individual object names or a group object name).  
  
-   `object-type:` Specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `on-error:` Specifies whether to specify refresh errors as warnings or error. Available options for on-error:  
  
    -   report-total-as-warning  
  
    -   report-each-as-warning  
  
    -   fail-script  
  
-   `report-errors-to:` Specifies location of error report for the refresh operation (optional attribute) if only folder path is given, then file by name **SourceDBRefreshReport.XML** is created.  
  
**Syntax Example:**  
  
```xml  
<refresh-from-database  
  
   object-name="<object-name>"  
  
   on-error="<report-total-as-warning/  
  
               report-each-as-warning/  
  
               fail-script>"   (optional)  
  
   report-errors-to="<file-name/folder-name>"   (optional)  
  
/>  
```  
or  
  
```xml  
<refresh-from-database  
  
   object-name="<object-name>"  
  
   object-type="<object-category>"/>  
```  
or  
  
```xml  
<refresh-from-database>  
  
   <metabase-object object-name="<object-name>"/>  
  
</refresh-from-database>  
```  
  
## Script Generation Script File Commands  
The Script Generation commands perform dual tasks: They help save the console output in a script file; and record the T-SQL output to the console or a file based on the parameter you specify.  
  
**Command**  
  
save-as-script  
  
Used to save the scripts of the objects to a file mentioned when metabase=target ,this is an alternative to synchronization command where in we get the scripts and execute the same on the target database.  
  
**Script**  
  
Requires one or several metabase nodes as command line parameter.  
  
-   `object-name:` Specifies the object(s) whose scripts are to be saved . (It can have individual object names or a group object name)  
  
-   `object-type:` specifies the type of the object specified in the object-name attribute (if object category is specified then object type will be "category").  
  
-   `metabase:` Specifies whether it ithe source or target metabase.  
  
-   `destination:` Specifies the path or the folder where the script has to be saved, if the file name is not given then a file name in the format (object_name   attribute value).out  
  
-   `overwrite:` if true then it overwrites if same filename exist. It can have the values (true/false).  
  
**Syntax Example:**  
  
```xml  
<save-as-script  
  
   metabase="<source/target>"  
  
   object-name="<object-name>"  
  
   object-type="<object-category>"  
  
   destination="<file/folder>"  
  
   overwrite="<true/false>"   (optional)  
  
/>  
```  
or  
  
```xml  
<save-as-script  
  
   metabase="<source/target>"  
  
   destination="<file/folder>"  
  
      <metabase-object object-name="<object-name>"  
  
         object-type="<object-category>"/>  
  
</save-as-script>  
```  
**Command**  
  
convert-sql-statement  
  
-   `context` specifies the schema name.  
  
-   `destination` specifies whether the output should be stored in a file.  
  
    If this attribute is not specified, then the converted T-SQL statement is displayed on the console. (optional attribute)  
  
-   `conversion-report-folder` specifies folder where the assessment report can to be stored.(optional attribute)  
  
-   `conversion-report-overwrite` specifies whether to overwrite the assessment report folder if it already exists.  
  
    **Default value:** false. (optional attribute)  
  
-   `write-converted-sql-to` specifies the file (or) folder path where the converted T-SQL is to be stored. When a folder path is specified along with the `sql-files` attribute, each source file will have a corresponding target T-SQL file created under the specified folder. When a folder path is specified along with the `sql` attribute, the converted T-SQL is written to a file named **Result.out** under the specified folder.  
  
-   `sql` specifies the Oracle sql statements to be converted, one or more statements can be separated using a ";"  
  
-   `sql-files` specifies the path of the sql files which has to be converted to T-SQL code.  
  
-   `write-summary-report-to` specifies the path where the report will be generated. If only the folder path is mentioned, then file by name **ConvertSQLReport.XML** is created. (optional attribute)  
  
    Report creation has 2 further sub-categories, viz.:  
  
    -   report-errors (="true/false", with default as "false" (optional attributes)).  
  
    -   verbose (="true/false", with default as "false" (optional attributes)).  
  
**Script**  
  
Requires one or several metabase nodes as command line parameter.  
  
**Syntax Example:**  
  
```  
<convert-sql-statement  
  
   context="<schema-name>"  
  
   conversion-report-folder="<folder-name>"  
  
   conversion-report-overwrite="<true/false>"  
  
   write-summary-report-to="<file-name/folder-name>"   (optional)  
  
   verbose="<true/false>"   (optional)  
  
   report-errors="<true/false>"   (optional)  
  
   destination="<stdout/file>"   (optional)  
  
   file-name="<file-name>"  
  
   sql="SELECT 1 FROM DUAL;">  
  
   <output-window suppress-messages="<true/false>" />  
  
</convert-sql-statement>  
```  
or  
  
```  
<convert-sql-statement  
  
   context="<schema-name>"  
  
   conversion-report-folder="<folder-name>"  
  
   conversion-report-overwrite="<true/false>"  
  
   write-summary-report-to="<file-name/folder-name>" (optional)  
  
   verbose="<true/false>" (optional)  
  
   report-errors="<true/false>"  
  
   destination="<stdout/file>"   (optional)  
  
   write-converted-sql-to="<file-name/folder-name>"  
  
   sql-files="<folder-name>\*.sql" />  
```  
or  
  
```  
<convert-sql-statement  
  
   context="<schema-name>"  
  
   conversion-report-folder="<folder-name>"  
  
   conversion-report-overwrite="<true/false>"  
  
   sql-files="<folder-name>\*.sql" />  
```  
  
## Next Step  
For information on command line options, see [Command Line Options in SSMA Console &#40;OracleToSQL&#41;](../../ssma/oracle/command-line-options-in-ssma-console-oracletosql.md) .  
  
For information on Sample console script files, see [Working with the Sample Console Script Files &#40;OracleToSQL&#41;](../../ssma/oracle/working-with-the-sample-console-script-files-oracletosql.md)  
  
The next step depends on your project requirements:  
  
-   For specifying a password or export/ import passwords, see [Managing Passwords &#40;OracleToSQL&#41;](../../ssma/oracle/managing-passwords-oracletosql.md).  
  
-   For generating reports, see [Generating Reports &#40;OracleToSQL&#41;](../../ssma/oracle/generating-reports-oracletosql.md).  
  
-   For troubleshooting issues in console, see [Troubleshooting &#40;OracleToSQL&#41;](../../ssma/oracle/troubleshooting-oracletosql.md).  
  
