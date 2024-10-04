---
title: "RS.exe utility"
description: Learn about the commands and syntax for the rs.exe utility that processes scripts used to automate report server deployment and administration tasks.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "automatic report server tasks"
  - "rs utility"
  - "command prompt utilities [Reporting Services]"
  - "report servers [Reporting Services], automating tasks"
  - "command prompt utilities [SQL Server], rs"
  - "scripts [Reporting Services], command prompt"
  - "deploying reports [Reporting Services]"
---
# RS.exe utility (SSRS)
  The rs.exe utility processes script that you provide in an input file. Use this utility to automate report server deployment and administration tasks.  
  
> [!NOTE]  
>  Beginning with [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)], the **rs** utility is supported against report servers that are configured for SharePoint integrated mode as well as servers configured in native mode. Previous versions only supported native mode configurations.  
  
## Syntax  
  
```  
  
rs {-?}  
{-i input_file}  
{-s serverURL}  
{-u username}  
{-p password}  
{-e endpoint}  
{-l time_out}  
{-b batchmode}  
{-v globalvars=}  
{-t trace}  
```  
  
##  <a name="bkmk_filelocation"></a> File location  
 **RS.exe** is located in the following folders, depending on the report server version. You can run the utility from any folder on your file system.

**[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)]** [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)]

```  
C:\Program Files (x86)\Microsoft SQL Server\130\Tools\Binn  
```

**[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)]** [!INCLUDE[ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)]

```  
C:\Program Files\Microsoft SQL Server Reporting Services\Shared Tools
```  

**[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)]** [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

```  
C:\Program Files\Microsoft Power BI Report Server\Shared Tools
```  
 
  
##  <a name="bkmk_arguments"></a> Arguments  
 **-?**  
 (Optional) Displays the syntax of **rs** arguments.  
  
 **-i** *input_file*  
 (Required) Specifies the .rss file to execute. This value can be a relative or fully qualified path to the .rss file.  
  
 **-s** *serverURL*  
 (Required) Specifies the Web server name and report server virtual directory name to execute the file against. An example of a report server URL is `https://examplewebserver/reportserver`. The prefix http:// or https:// at the beginning of the server name is optional. If you omit the prefix, the report server script host tries to use https first, and then uses http if https doesn't work.  
  
 **-u** [*domain*\\]*username*  
 (Optional) Specifies a user account used to connect to the report server. If **-u** and **-p** are omitted, the current Windows user account is used.  
  
 **-p** *password*  
 (Required if **-u** is specified) Specifies the password to use with the **-u** argument. This value is case-sensitive.  
  
 **-e**  
 (Optional) Specifies the SOAP endpoint against which the script should run. Valid values are:  
  
-   Mgmt2010  
  
-   Mgmt2006  
  
-   Mgmt2005  
  
-   Exec2005  
  
 If a value isn't specified, the Mgmt2005 endpoint is used. For more information about the SOAP endpoints, see [Report server web service endpoints](../../reporting-services/report-server-web-service/methods/report-server-web-service-endpoints.md).  
  
 **-l** *time_out*  
 (Optional) Specifies the number of seconds that elapse before the connection to the server times out. The default is 60 seconds. If you don't specify a time-out value, the default is used. A value of **0** specifies that the connection never times out.  
  
 **-b**  
 (Optional) Specifies that the commands in the script file run in a batch. If any commands fail, the batch is rolled back. Some commands can't be batched, and those commands run as usual. Only exceptions that are thrown and aren't handled within the script result in a rollback. If the script handles an exception and returns normally from **Main**, the batch is committed. If you omit this parameter, the commands run without creating a batch. For more information, see [Batching methods](../../reporting-services/report-server-web-service-net-framework-soap-headers/batching-methods.md).  
  
 **-v** *globalvar*  
 (Optional) Specifies global variables that are used in the script. If the script uses global variables, you must specify this argument. The value that you specify must be valid for global variable defined in the .rss file. You must specify one global variable for each **-v** argument.  
  
 The **-v** argument is specified on the command line and is used to set the value for a global variable that is defined in your script at run time. For example, if your script contains a variable named *parentFolder*, you can specify a name for that folder on the command line:  
  
 `rs.exe -i myScriptFile.rss -s https://myServer/reportserver -v parentFolder="Financial Reports"`  
  
 Global variables are created with the names given and set to the values supplied. For example, **-v a=**"**1**" **-v b=**"**2**" results in a variable named **a** with a value of"**1**" and a variable **b** with a value of "**2**".  
  
 Global variables are available to any function in the script. A backslash and quotation mark (**\\"**) is interpreted as a double quotation mark. The quotation marks are required only if the string contains a space. Variable names must be valid for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]. They must start with alphabetical character or underscore and contain alphabetical characters, digits, or underscores. Reserved words can't be used as variable names. For more information about using global variables, see [Built-in collections in expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md).  
  
 **-t**  
 (Optional) Outputs error messages to the trace log. This argument doesn't take a value. For more information, see [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
##  <a name="bkmk_permissions"></a> Permissions  
 To run the tool, you must have permission to connect to the report server instance you're running the script against. You can run scripts to make changes to the local computer or a remote computer. To make changes to a report server installed on a remote computer, specify the remote computer in the **-s** argument.  
  
##  <a name="bkmk_examples"></a> Examples  
 The following example illustrates how to specify the script file that contains [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET script and Web service methods that you want to execute.  
  
```  
rs -i c:\scriptfiles\script_copycontent.rss -s https://localhost/reportserver  
```  
  
 For a detailed example, see [Sample Reporting Services rs.exe script to copy content between report servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
 For more examples, see [Run a Reporting Services script file](../../reporting-services/tools/run-a-reporting-services-script-file.md)  
  
## Remarks  
 You can define scripts to set system properties, publish reports, and so forth. The scripts that you create can include any methods of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] API. For more information about the methods and properties available to you, see [Report server web service](../../reporting-services/report-server-web-service/report-server-web-service.md).  
  
 The script must be written in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET code, and stored in a Unicode or UTF-8 text file with an .rss file name extension. You can't debug scripts with the **rs** utility. To debug a script, run the code within [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
> [!TIP]  
>  For a detailed example, see [Sample Reporting Services rs.exe script to copy content between report servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
## Related content

- [Run a Reporting Services script file](../../reporting-services/tools/run-a-reporting-services-script-file.md)
- [Script deployment and administrative tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md)
- [Script with the rs.exe utility and the web service](../../reporting-services/tools/script-with-the-rs-exe-utility-and-the-web-service.md)
- [Report server command prompt utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md)
