---
title: "Use Tokens in Job Steps | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "job steps [SQL Server Agent]"
  - "security [SQL Server Agent], enabling alert job step tokens"
  - "SQL Server Agent jobs, job steps"
  - "tokens [SQL Server]"
  - "escape macros [SQL Server Agent]"
ms.assetid: 105bbb66-0ade-4b46-b8e4-f849e5fc4d43
author: stevestein
ms.author: sstein
manager: craigg
---
# Use Tokens in Job Steps
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent allows you to use tokens in [!INCLUDE[tsql](../../includes/tsql-md.md)] job step scripts. Using tokens when you write your job steps gives you the same flexibility that variables provide when you write software programs. After you insert a token in a job step script, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent replaces the token at run time, before the job step is executed by the [!INCLUDE[tsql](../../includes/tsql-md.md)] subsystem.  
  
> [!IMPORTANT]  
>  Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 1, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step token syntax changed. As a result, an escape macro must now accompany all tokens used in job steps, or else those job steps will fail. Using escape macros and updating your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps that use tokens are described in the following sections, "Understanding Using Tokens," "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Tokens and Macros," and "Updating Job Steps to Use Macros." In addition, the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] syntax, which used square brackets to call out [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step tokens (for example, "`[DATE]`") has also changed. You must now enclose token names in parentheses and place a dollar sign (`$`) at the beginning of the token syntax. For example:  
>   
>  `$(ESCAPE_` *macro name* `(DATE))`  
  
## Understanding Using Tokens  
  
> [!IMPORTANT]  
>  Any Windows user with write permissions on the Windows Event Log can access job steps that are activated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts or WMI alerts. To avoid this security risk, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent tokens that can be used in jobs activated by alerts are disabled by default. These tokens are: **A-DBN**, **A-SVR**, **A-ERR**, **A-SEV**, **A-MSG**., and **WMI(*`property`*)**. Note that in this release, use of tokens is extended to all alerting.  
>   
>  If you need to use these tokens, first ensure that only members of trusted Windows security groups, such as the Administrators group, have write permissions on the Event Log of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resides. Then, right-click **SQL Server Agent** in Object Explorer, select **Properties**, and on the **Alert System** page, select **Replace tokens for all job responses to alerts** to enable these tokens.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent token replacement is simple and efficient: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent replaces an exact literal string value for the token. All tokens are case-sensitive. Your job steps must take this into account and correctly quote the tokens you use or convert the replacement string to the correct data type.  
  
 For example, you might use the following statement to print the name of the database in a job step:  
  
 `PRINT N'Current database name is $(ESCAPE_SQUOTE(A-DBN))' ;`  
  
 In this example, the **ESCAPE_SQUOTE** macro is inserted with the **A-DBN** token. At run time, the **A-DBN** token will be replaced with the appropriate database name. The escape macro escapes any single quotation marks that may be inadvertently passed in the token replacement string. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will replace one single quotation mark with two single quotation marks in the final string.  
  
 For example, if the string passed to replace the token is `AdventureWorks2012'SELECT @@VERSION --`, the command executed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step will be:  
  
 `PRINT N'Current database name is AdventureWorks2012''SELECT @@VERSION --' ;`  
  
 In this case, the inserted statement, `SELECT @@VERSION`, does not execute. Instead, the extra single quotation mark causes the server to parse the inserted statement as a string. If the token replacement string does not contain a single quotation mark, no characters are escaped and the job step containing the token executes as intended.  
  
 To debug token usage in your job steps, use print statements such as `PRINT N'$(ESCAPE_SQUOTE(SQLDIR))'` and save job step output to a file or table. Use the **Advanced** page of the **Job Step Properties** dialog box to specify a job step output file or table.  
  
## SQL Server Agent Tokens and Macros  
 The following tables list and describe the tokens and macros that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent supports.  
  
### SQL Server Agent Tokens  
  
|Token|Description|  
|-----------|-----------------|  
|**(A-DBN)**|Database name. If the job is run by an alert, the database name value automatically replaces this token in the job step.|  
|**(A-SVR)**|Server name. If the job is run by an alert, the server name value automatically replaces this token in the job step.|  
|**(A-ERR)**|Error number. If the job is run by an alert, the error number value automatically replaces this token in the job step.|  
|**(A-SEV)**|Error severity. If the job is run by an alert, the error severity value automatically replaces this token in the job step.|  
|**(A-MSG)**|Message text. If the job is run by an alert, the message text value automatically replaces this token in the job step.|  
|**(DATE)**|Current date (in YYYYMMDD format).|  
|**(INST)**|Instance name. For a default instance, this token will have the default instance name: MSSQLSERVER.|  
|**(JOBID)**|Job ID.|  
|**(MACH)**|Computer name.|  
|**(MSSA)**|Master SQLServerAgent service name.|  
|**(OSCMD)**|Prefix for the program used to run **CmdExec** job steps.|  
|**(SQLDIR)**|The directory in which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. By default, this value is C:\Program Files\Microsoft SQL Server\MSSQL.|  
|**(SQLLOGDIR)**|Replacement token for the SQL Server error log folder path - for example, $(ESCAPE_SQUOTE(SQLLOGDIR)).|  
|**(STEPCT)**|A count of the number of times this step has executed (excluding retries). Can be used by the step command to force termination of a multistep loop.|  
|**(STEPID)**|Step ID.|  
|**(SRVR)**|Name of the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is a named instance, this includes the instance name.|  
|**(TIME)**|Current time (in HHMMSS format).|  
|**(STRTTM)**|The time (in HHMMSS format) that the job began executing.|  
|**(STRTDT)**|The date (in YYYYMMDD format) that the job began executing.|  
|**(WMI(** *property* **))**|For jobs that run in response to WMI alerts, the value of the property specified by *property*. For example, `$(WMI(DatabaseName))` provides the value of the **DatabaseName** property for the WMI event that caused the alert to run.|  
  
### SQL Server Agent Escape Macros  
  
|Escape Macros|Description|  
|-------------------|-----------------|  
|**$(ESCAPE_SQUOTE(** *token_name* **))**|Escapes single quotation marks (') in the token replacement string. Replaces one single quotation mark with two single quotation marks.|  
|**$(ESCAPE_DQUOTE(** *token_name* **))**|Escapes double quotation marks (") in the token replacement string. Replaces one double quotation mark with two double quotation marks.|  
|**$(ESCAPE_RBRACKET(** *token_name* **))**|Escapes right brackets (]) in the token replacement string. Replaces one right bracket with two right brackets.|  
|**$(ESCAPE_NONE(** *token_name* **))**|Replaces token without escaping any characters in the string. This macro is provided to support backward compatibility in environments where token replacement strings are only expected from trusted users. For more information, see "Updating Job Steps to Use Macros," later in this topic.|  
  
## Updating Job Steps to Use Macros  
 Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 1, job steps that contain tokens without escape macros will fail and return an error message indicating the job step contains one or more tokens that must be updated with a macro before the job can run.  
  
 A script is provided with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base article 915845: [SQL Server Agent Job Steps That Use Tokens Fail in SQL Server 2005 Service Pack 1](https://support.microsoft.com/kb/915845).You can use this script to update all of your job steps that use tokens with the **ESCAPE_NONE** macro. After using this script, we recommend that you review your job steps that use tokens as soon as possible, and replace the **ESCAPE_NONE** macro with an escape macro that is appropriate for the job step context.  
  
 The following table describes how token replacement is handled by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. To turn alert token replacement on or off, right-click **SQL Server Agent** in Object Explorer, select **Properties**, and on the **Alert System** page, select or clear the **Replace tokens for all job responses to alerts** check box.  
  
|Token syntax|Alert token replacement on|Alert token replacement off|  
|------------------|--------------------------------|---------------------------------|  
|ESCAPE macro used|All tokens in jobs are successfully replaced.|Tokens activated by alerts are not replaced. These tokens are **A-DBN**, **A-SVR**, **A-ERR**, **A-SEV**, **A-MSG**, and **WMI(*`property`*)**. Other static tokens are replaced successfully.|  
|No ESCAPE macro used|Any jobs containing tokens fail.|Any jobs containing tokens fail.|  
  
## Token Syntax Update Examples  
  
### A. Using tokens in non-nested strings  
 The following example shows how to update a simple non-nested script with the appropriate escape macro. Before running the update script, the following job step script uses a job step token to print the appropriate database name:  
  
 `PRINT N'Current database name is $(A-DBN)' ;`  
  
 After running the update script, an `ESCAPE_NONE` macro is inserted before the `A-DBN` token. Because single quotation marks are used to delimit the print string, you must update the job step by inserting the `ESCAPE_SQUOTE` macro as follows:  
  
 `PRINT N'Current database name is $(ESCAPE_SQUOTE(A-DBN))' ;`  
  
### B. Using tokens in nested strings  
 In job step scripts where tokens are used in nested strings or statements, the nested statements should be rewritten as multiple statements before inserting the appropriate escape macros.  
  
 For example, consider the following job step, which uses the `A-MSG` token and has not been updated with an escape macro:  
  
 `PRINT N'Print ''$(A-MSG)''' ;`  
  
 After running the update script, an `ESCAPE_NONE` macro is inserted with the token. However, in this case, you would have to rewrite the script without using nesting as follows and insert the `ESCAPE_SQUOTE` macro to properly escape delimiters that may be passed in the token replacement string:  
  
 `DECLARE @msgString nvarchar(max)`  
  
 `SET @msgString = '$(ESCAPE_SQUOTE(A-MSG))'`  
  
 `SET @msgString = QUOTENAME(@msgString,'''')`  
  
 `PRINT N'Print ' + @msgString ;`  
  
 Note also in this example that the QUOTENAME function sets the quote character.  
  
### C. Using tokens with the ESCAPE_NONE macro  
 The following example is part of a script that retrieves the `job_id` from the `sysjobs` table and uses the `JOBID` token to populate the `@JobID` variable, which was declared earlier in the script as a binary data type. Note that because no delimiters are required for binary data types, the `ESCAPE_NONE` macro is used with the `JOBID` token. You would not need to update this job step after running the update script.  
  
 `SELECT * FROM msdb.dbo.sysjobs`  
  
 `WHERE @JobID = CONVERT(uniqueidentifier, $(ESCAPE_NONE(JOBID))) ;`  
  
## See Also  
 [Implement Jobs](implement-jobs.md)   
 [Manage Job Steps](manage-job-steps.md)  
  
  
