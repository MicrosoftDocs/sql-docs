---
title: "Profiler Utility"
description: The profiler utility launches the SQL Server Profiler tool. Optional arguments allow you to control how the application starts.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "command prompt utilities [SQL Server], profiler90 utility"
  - "profiler90 utility"
  - "Profiler [SQL Server Profiler], starting"
  - "SQL Server Profiler, starting"
  - "starting SQL Server Profiler"
ms.assetid: e91c30a9-0d29-4f84-bcb8-e8fb62afadda
author: markingmyname
ms.author: maghan
---
# Profiler Utility
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]
  The **profiler** utility launches the [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] tool. The optional arguments listed later in this topic allow you to control how the application starts.  
  
> [!NOTE]  
>  The **profiler** utility is not intended for scripting traces. For more information, see [SQL Server Profiler](../tools/sql-server-profiler/sql-server-profiler.md).  
  
## Syntax  
  
```  
  
profiler  
    [ /? ] |  
[  
{  
{ /U login_id [ /P password ] }  
| /E  
}  
{[ /S sql_server_name ] | [ /A analysis_services_server_name ] }  
[ /D database ]  
[ /T "template_name" ]  
[ /B { "trace_table_name" } ]  
{ [/F "filename" ] | [ /O "filename" ] }  
[ /L locale_ID ]  
[ /M "MM-DD-YY hh:mm:ss" ]  
[ /R ]  
[ /Z file_size ]  
]  
```  
  
## Arguments  
 **/?**  
 Displays the syntax summary of **profiler** arguments.  
  
 **/U** *login_id*  
 Is the user login ID for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. Login IDs are case sensitive.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)].  
  
 **/P** *password*  
 Specifies a user-specified password for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
 **/E**  
 Specifies connecting with Windows Authentication with the current user's credentials.  
  
 **/S**  *sql_server_name*  
 Specifies an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Profiler will automatically connect to the specified server using the authentication information specified in the **/U** and **/P** switches or the **/E** switch. To connect to a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], use **/S** *sql_server_name*\\*instance_name*.  
  
 **/A**  *analysis_services_server_name*  
 Specifies an instance of Analysis Services. Profiler will automatically connect to the specified server using the authentication information specified in the **/U** and **/P** switches or the **/E** switch. To connect to a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] use **/A** *analysis_services_server_name\instance_name*.  
  
 **/D** *database*  
 Specifies the name of the database to be used with the connection. This option will select the default database for the specified user if no database is specified.  
  
 **/B "** *trace_table_name* **"**  
 Specifies a trace table to load when the profiler is launched. You must specify the database, the user or schema, and the table.  
  
 **/T"** *template_name* **"**  
 Specifies the template that will be loaded to configure the trace. The template name must be in quotes. The template name must be in either the system template directory or the user template directory. If two templates with the same name exist in both directories, the template from the system directory will be loaded. If no template with the specified name exists, the standard template will be loaded. Note that the file extension for the template (.tdf) should not be specified as part of the *template_name*. For example:  
  
```  
/T "standard"  
```  
  
 **/F"** *filename* **"**  
 Specifies the path and filename of a trace file to load when profiler is launched. The entire path and filename must be in quotes. This option cannot be used with **/O**.  
  
 **/O "** *filename*  **"**  
 Specifies the path and filename of a file to which trace results should be written. The entire path and filename must be in quotes. This option cannot be used with **/F.**  
  
 **/L** *locale_ID*  
 Not available.  
  
 **/M "** *MM-DD-YY hh:mm:ss* **"**  
 Specifies the date and time for the trace to stop. The stop time must be in quotes. Specify the stop time according to the parameters in the table below:  
  
|Parameter|Definition|  
|---------------|----------------|  
|MM|Two-digit month|  
|DD|Two-digit day|  
|YY|Two-digit year|  
|hh|Two-digit hour on a 24-hour clock|  
|mm|Two-digit minute|  
|ss|Two-digit second|  
  
> [!NOTE]  
>  The "MM-DD-YY hh:mm:ss" format can only be used if the **Use regional settings to display date and time values** option is enabled in [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]. If this option is not enabled, you must use the "YYYY-MM-DD hh:mm:ss" date and time format.  
  
 **/R**  
 Enables trace file rollover.  
  
 **/Z**  *file_size*  
 Specifies the size of the trace file in megabytes (MB). The default size is 5 MB. If rollover is enabled, all rollover files will be limited to the value specified in this argument.  
  
## Remarks  
 To start a trace with a specific template, use the **/S** and **/T** options together. For example, to start a trace using the Standard template on MyServer\MyInstance, enter the following at the command prompt:  
  
```  
profiler /S MyServer\MyInstance /T "Standard"  
```  
  
## See Also  
 [Command Prompt Utility Reference &#40;Database Engine&#41;](../tools/command-prompt-utility-reference-database-engine.md)  
  
  
