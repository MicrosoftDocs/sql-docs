---
title: "RS.exe Utility (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "automatic report server tasks"
  - "rs utility"
  - "command prompt utilities [Reporting Services]"
  - "report servers [Reporting Services], automating tasks"
  - "command prompt utilities [SQL Server], rs"
  - "scripts [Reporting Services], command prompt"
  - "deploying reports [Reporting Services]"
ms.assetid: bd6f958f-cce6-4e79-8a0f-9475da2919ce
author: markingmyname
ms.author: maghan
manager: craigg
---
# RS.exe Utility (SSRS)
  The rs.exe utility processes script that you provide in an input file. Use this utility to automate report server deployment and administration tasks.  
  
> [!NOTE]  
>  Beginning with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], the **rs** utility is supported against report servers that are configured for SharePoint integrated mode as well as servers configured in native mode. Previous versions only supported native mode configurations.  
  
 **In this topic:**  
  
-   [File Location](#bkmk_filelocation)  
  
-   [Arguments](#bkmk_arguments)  
  
-   [Permissions](#bkmk_permissions)  
  
-   [Examples](#bkmk_examples)  
  
## Syntax  
  
```  
  
      rs {-?}  
{-i input_file=}  
{-s serverURL}  
{-u username}  
{-p password}  
{-e endpoint}  
{-l time_out}  
{-b batchmode}  
{-v globalvars=}  
{-t trace}  
```  
  
##  <a name="bkmk_filelocation"></a> File Location  
 **RS.exe** is located at **\Program Files\Microsoft SQL Server\110\Tools\Binn**. You can run the utility from any folder on your file system.  
  
##  <a name="bkmk_arguments"></a> Arguments  
 **-?**  
 (Optional) Displays the syntax of **rs** arguments.  
  
 `-i` *input_file*  
 (Required) Specifies the .rss file to execute. This value can be a relative or fully qualified path to the .rss file.  
  
 `-s` *serverURL*  
 (Required) Specifies the Web server name and report server virtual directory name to execute the file against. An example of a report server URL is `http://examplewebserver/reportserver`. The prefix http:// or https:// at the beginning of the server name is optional. If you omit the prefix, the report server script host tries to use https first, and then uses http if https does not work.  
  
 `-u` [*domain*\\]*username*  
 (Optional) Specifies a user account used to connect to the report server. If `-u` and `-p` are omitted, the current Windows user account is used.  
  
 `-p` *password*  
 (Required if `-u` is specified) Specifies the password to use with the `-u` argument. This value is case-sensitive.  
  
 `-e`  
 (Optional) Specifies the SOAP endpoint against which the script should run. Valid values are the following:  
  
-   Mgmt2010  
  
-   Mgmt2006  
  
-   Mgmt2005  
  
-   Exec2005  
  
 If a value is not specified, the Mgmt2005 endpoint is used. For more information about the SOAP endpoints, see [Report Server Web Service Endpoints](../report-server-web-service/methods/report-server-web-service-endpoints.md).  
  
 `-l` *time_out*  
 (Optional) Specifies the number of seconds that elapse before the connection to the server times out. The default is 60 seconds. If you do not specify a time-out value, the default is used. A value of `0` specifies that the connection never times out.  
  
 **-b**  
 (Optional) Specifies that the commands in the script file run in a batch. If any commands fail, the batch is rolled back. Some commands cannot be batched, and those run as usual. Only exceptions that are thrown and are not handled within the script result in a rollback. If the script handles an exception and returns normally from `Main`, the batch is committed. If you omit this parameter, the commands run without creating a batch. For more information, see [Batching Methods](../report-server-web-service-net-framework-soap-headers/batching-methods.md).  
  
 `-v` *globalvar*  
 (Optional) Specifies global variables that are used in the script. If the script uses global variables, you must specify this argument. The value that you specify must be valid for global variable defined in the .rss file. You must specify one global variable for each **-v** argument.  
  
 The `-v` argument is specified on the command line and is used to set the value for a global variable that is defined in your script at run time. For example, if your script contains a variable named *parentFolder*, you can specify a name for that folder on the command line:  
  
 `rs.exe -i myScriptFile.rss -s http://myServer/reportserver -v parentFolder="Financial Reports"`  
  
 Global variables are created with the names given and set to the values supplied. For example, **-v a=**"`1`" **-v b=**"`2`" results in a variable named `a` with a value of"`1`" and a variable **b** with a value of "`2`".  
  
 Global variables are available to any function in the script. A backslash and quotation mark (**\\"**) is interpreted as a double quotation mark. The quotation marks are required only if the string contains a space. Variable names must be valid for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)]; they must start with alphabetical character or underscore and contain alphabetical characters, digits, or underscores. Reserved words cannot be used as variable names. For more information about using global variables, see [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../report-design/built-in-collections-in-expressions-report-builder.md).  
  
 **-t**  
 (Optional) Outputs error messages to the trace log. This argument does not take a value. For more information, see [Report Server Service Trace Log](../report-server/report-server-service-trace-log.md).  
  
##  <a name="bkmk_permissions"></a> Permissions  
 To run the tool, you must have permission to connect to the report server instance you are running the script against. You can run scripts to make changes to the local computer or a remote computer. To make changes to a report server installed on a remote computer, specify the remote computer in the `-s` argument.  
  
##  <a name="bkmk_examples"></a> Examples  
 The following example illustrates how to specify the script file that contains [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] .NET script and Web service methods that you want to execute.  
  
```  
rs -i c:\scriptfiles\script_copycontent.rss -s http://localhost/reportserver  
```  
  
 For a detailed example, see [Sample Reporting Services rs.exe Script to Migrate Content between Report Servers](sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
 For additional examples, see [Run a Reporting Services Script File](run-a-reporting-services-script-file.md)  
  
## Remarks  
 You can define scripts to set system properties, publish reports, and so forth. The scripts that you create can include any methods of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] API. For more information about the methods and properties available to you, see [Report Server Web Service](../report-server-web-service/report-server-web-service.md).  
  
 The script must be written in [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] .NET code, and stored in a Unicode or UTF-8 text file with an .rss file name extension. You cannot debug scripts with the **rs** utility. To debug a script, run the code within [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
> [!TIP]  
>  For a detailed example, see [Sample Reporting Services rs.exe Script to Migrate Content between Report Servers](sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
## See Also  
 [Run a Reporting Services Script File](run-a-reporting-services-script-file.md)   
 [Script Deployment and Administrative Tasks](script-deployment-and-administrative-tasks.md)   
 [Script with the rs.exe Utility and the Web Service](script-with-the-rs-exe-utility-and-the-web-service.md)   
 [Report Server Command Prompt Utilities &#40;SSRS&#41;](report-server-command-prompt-utilities-ssrs.md)  
  
  
