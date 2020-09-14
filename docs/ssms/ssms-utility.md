---
description: "SSMS Utility"
title: SSMS Utility
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], opening"
  - "command prompt utilities [SQL Server], sqlwb"
  - "sqlwb utility"
  - "Management Studio command line"
  - "opening SQL Server Management Studio"
ms.assetid: aafda520-9e2a-4e1e-b936-1b165f1684e8
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 07/24/2020
---

# SSMS Utility

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The **SSMS** utility opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. If specified, **Ssms** also establishes a connection to a server, and opens queries, scripts, files, projects, and solutions.

You can specify files that contain queries, projects, or solutions. Files that contain queries are automatically connected to a server if connection information is provided and the file type is associated with that type of server. For instance, .sql files open a SQL Query Editor window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], and .mdx files open an MDX Query Editor window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. **SQL Server Solutions and Projects** open in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. 

> [!NOTE]
> The **Ssms** utility does not run queries. To run queries from the command line, use the **sqlcmd** utility. 

## Syntax

```syntaxsql
Ssms
[scriptfile] [projectfile] [solutionfile] 
[-S servername] [-d databasename] [-G] [-U username] [-E] [-nosplash] [-log [filename]?] [-?] 
```

## Arguments

*scriptfile*
Specifies one or more script files to open. The parameter must contain the full path to the files. 

*projectfile*
Specifies a script project to open. The parameter must contain the full path to the script project file. 

*solutionfile*
Specifies a solution to open. The parameter must contain the full path to the solution file. 

[**-S** _servername_]
Server name

[**-d** _databasename_]
Database name

[**-G**]
Connect using Active Directory Authentication. The type of connection is determined whether **-U** is included.

> [!Note]
> **Active Directory - Universal with MFA support** is not currently supported.

[**-U** _username_]
User name when connecting with 'SQL Authentication'

[**-P** _password_]
Password when connecting with 'SQL Authentication'

[**-E**]
Connect using Windows Authentication

[**-nosplash**]
Prevents [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from displaying the splash screen graphic while opening. Use this option when connecting to the computer running [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] by means of Terminal Services over a connection with a limited bandwidth. This argument is not case-sensitive and may appear before or after other arguments

[**-log**_[filename]?_]
Logs [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] activity to the specified file for troubleshooting

[**-?**]
Displays command-line help

## Remarks

All of the switches are optional and separated by a space except files, which are separated by commas. If you do not specify any switches, **Ssms** opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] as specified in the **Options** settings on the **Tools** menu. For example, if the **Environment/General** page **At startup** option specifies **Open new query window**, **Ssms** opens with a blank Query Editor.

The **-log** switch must appear at the end of the command line, after all, other switches. The filename argument is optional. If a filename is specified, and the file does not exist, the file is created. If the file cannot be created - for example, due to insufficient write access, the log is written to the nonlocalized APPDATA location instead (See below). If the filename argument is not specified, two files are written to the current user's nonlocalized application data folder. The nonlocalized application data folder for SQL Server can be found from the APPDATA environment variable. For example, for SQL Server 2012, the folder is \<system drive>:\Users\\<username\>\AppData\Roaming\Microsoft\AppEnv\10.0\\. The two files are, by default, named ActivityLog.xml and ActivityLog.xsl. The former contains the activity log data, and the latter is an XML style sheet, which provides a more convenient way to view the XML file. Use the following steps to view the log file in your default XML viewer, like Internet Explorer: Click Start, then click Run...", then type "\<system drive>:\Users\\<username\>\AppData\Roaming\Microsoft\AppEnv\10.0\ActivityLog.xml" into the field provided, and then press Enter.

Files that contain queries prompt to be connected to a server if connection information is provided and the file type is associated with that type of server. For instance, .sql files open a SQL Query Editor window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], and .mdx files open an MDX Query Editor window in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. **SQL Server Solutions and Projects** open in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].

The following table maps server types to file extensions.

| Server type | Extension |
|-------------|-----------|
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]|.sql|
|SQL Server Analysis Services|.mdx<br /><br /> .xmla|

## Examples

The following script opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt with the default settings:

```
  Ssms
```

The following scripts opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt using *Active Directory - Integrated*:

```
Ssms.exe -S servername.database.windows.net -G
```

The following script opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt, with Windows Authentication, with the Code Editor set to the server `ACCTG and the database AdventureWorks2012,` without showing the splash screen:

```
Ssms -E -S ACCTG -d AdventureWorks2012 -nosplash
```

The following script opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt, and opens the MonthEndQuery script.

```
Ssms "C:\Documents and Settings\username\My Documents\SQL Server Management Studio Projects\FinanceScripts\FinanceScripts\MonthEndQuery.sql"
```

The following script opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt, and opens the NewReportsProject project on the computer named `developer`:

```
Ssms "\\developer\fin\ReportProj\ReportProj\NewReportProj.ssmssqlproj"
```

The following script opens [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from a command prompt, and opens the MonthlyReports solution: 

```
Ssms "C:\solutionsfolder\ReportProj\MonthlyReports.ssmssln"
```

## See Also

[Use SQL Server Management Studio](https://msdn.microsoft.com/library/f289e978-14ca-46ef-9e61-e1fe5fd593be)
