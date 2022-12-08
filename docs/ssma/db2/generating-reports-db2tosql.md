---
description: "Generating Reports (DB2ToSQL)"
title: "Generating Reports (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 69ef5fd9-190d-4c58-8199-b3f77d5e1883
author: cpichuka 
ms.author: cpichuka 
---
# Generating Reports (DB2ToSQL)
The reports of certain activities performed using commands are generated in SSMA Console at object tree level.  
  
Use the following procedure to generate reports:  
  
1.  Specify the **write-summary-report-to** parameter. The related report is stored as the file name (if specified) or in the folder you specify. The file name is system-predefined as mentioned in the table below where, **&lt;n&gt;** is the unique file number that increments with a digit with each execution of the same command.  
  
    The reports vis-à-vis commands are:  
  
    |Sl. No.|Command|Report Title|  
    |-|-|-|  
    |1|generate-assessment-report|AssessmentReport&lt;n&gt;.XML|  
    |2|convert-schema|SchemaConversionReport&lt;n&gt;.XML|  
    |3|migrate-data|DataMigrationReport&lt;n&gt;.XML|  
    |4|convert-sql-statement|ConvertSQLReport&lt;n&gt;.XML|  
    |5|synchronize-target|TargetSynchronizationReport&lt;n&gt;.XML|  
    |6|refresh-from-database|SourceDBRefreshReport&lt;n&gt;.XML|  
  
    > [!IMPORTANT]  
    > An output report is different from Assessment Report. The former is a report on the performance of an executed command while, the latter is an XML report for programmatic consumption.  
  
    For the command options for output reports (from Sl. No. 2-4 above), refer to the [Executing the SSMA Console &#40;DB2ToSQL&#41;](../../ssma/db2/executing-the-ssma-console-db2tosql.md) section.  
  
2.  Indicate the extent of detail you desire in the output report using the Report Verbosity settings:  
  
    |Sl. No.|Command and Parameter|Output Description|  
    |-|-|-|  
    |1|verbose="false"|Generates a summarized report of the activity.|  
    |2|verbose="true"|Generates a summarized and detailed status report for each activity.|  
  
    > [!NOTE]  
    > The Report Verbosity Settings specified above are applicable for generate-assessment-report, convert-schema, migrate-data, convert-sql-statement commands.  
  
3.  Indicate the extent of detail you desire in the error reports using the Error Reporting settings:  
  
    |Sl. No.|Command and Parameter|Output Description|  
    |-|-|-|  
    |1|report-errors="false"|No details on error/ warning/ info messages.|  
    |2|report-errors="true"|Detailed error/ warning/ info messages.|  
  
    > [!NOTE]  
    > The Error Reporting Settings specified above are applicable for generate-assessment-report, convert-schema, migrate-data, convert-sql-statement commands.  
  
**Example:**  
  
```  
<generate-assessment-report  
  
   object-name="<object-name>"  
  
   object-type="<object-type>"  
  
   verbose="<true/false>"  
  
   report-erors="<true/false>"  
  
   write-summary-report-to="<file-name/folder-name>"  
  
   assessment-report-folder="<folder-name>"  
  
   assessment-report-overwrite="<true/false>"/>  
```  
  
### synchronize-target:  
The command **synchronize-target** has **report-errors-to** parameter, which specifies the location of error report for the synchronization operation. Then, a file by name **TargetSynchronizationReport&lt;n&gt;.XML** is created at the specified location, where **&lt;n&gt;** is the unique file number that increments with a digit with each execution of the same command.  
  
**Note:** If the folder path is given, then 'report-errors-to' parameter becomes an optional attribute for the command 'synchronize-target'.  
  
```  
<!-- Example: Synchronize target entire Database with all attributes-->  
  
<synchronize-target  
  
   object-name="<object-name>"  
  
   on-error="report-total-as-warning/report-each-as-warning/fail-script"  
  
   report-errors-to="<file-name/folder-name>"/>  
```  
**object-name:** Specifies the object(s) considered for synchronization (It can also have indivdual object names or a group object name).  
  
**on-error:** Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:  
  
-   report-total-as-warning  
  
-   report-each-as-warning  
  
-   fail-script  
  
### refresh-from-database:  
The command **refresh-from-database** has **report-errors-to** parameter, which specifies the location of error report for the refresh operation. Then, a file by name **SourceDBRefreshReport&lt;n&gt;.XML** is created at the specified location, where **&lt;n&gt;** is the unique file number that increments with a digit with each execution of the same command.  
  
**Note:** If the folder path is given, then 'report-errors-to' parameter becomes an optional attribute for the command 'synchronize-target'.  
  
```  
<!-- Example: Refresh entire Schema (with all attributes)-->  
  
<refresh-from-database  
  
   object-name="<object-name>"  
  
   object-type ="<object-type>"  
  
   on-error="report-total-as-warning/report-each-as-warning/fail-script"  
  
   report-errors-to="<file-name/folder-name>"/>  
```  
**object-name:** Specifies the object(s) considered for refresh (It can also have indivdual object names or a group object name).  
  
**on-error:** Specifies whether to specify refresh errors as warnings or error. Available options for on-error:  
  
-   report-total-as-warning  
  
-   report-each-as-warning  
  
-   fail-script  
  
## See Also  
[Executing the SSMA Console](./executing-the-ssma-console-db2tosql.md)  
