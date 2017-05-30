---
title: "Invoke-ASCmd cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 2896b74a-3911-4b3f-89ab-bb375bdb34d8
caps.latest.revision: 15
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Invoke-ASCmd cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  Enables a database administrator to execute an XMLA script, Multidimensional Expressions (MDX), Data Mining Extensions (DMX) statements, or Tabular Model Scripting Language (TMSL) script.  
  
 TMSL is supported only for Tabular server mode on a SQL Server 2016 Analysis Services instance.  
  
 If you want to create databases or other objects, this is the cmdlet you would use, with a script input file.  
  
## Syntax  
 `Invoke-ASCmd –Query <string> [-Server <string>] [-Database <string>] [-Credential <PSCredential>] [-ConnectionTimeout <int>] [-QueryTimeout <int>] [-Variable <string[]>] [-TraceFile <string>] [-TraceFileFormat <TraceFileFormatOption>] [-TraceFileDelimiter <string>] [-TraceTimeout <int>] [-TraceLevel <TraceLevelOption>] [<CommonParameters>]`  
  
 `Invoke-ASCmd –InputFile <string> [-Server <string>] [-Database <string>] [-Credential <PSCredential>] [-ConnectionTimeout <int>] [-QueryTimeout <int>] [-Variable <string[]>] [-TraceFile <string>] [-TraceFileFormat <TraceFileFormatOption>] [-TraceFileDelimiter <string>] [-TraceTimeout <int>] [-TraceLevel <TraceLevelOption>] [<CommonParameters>]`  
  
## Description  
 The Invoke-ASCmd cmdlet can execute queries or scripts that are contained in input files.  
  
 For XMLA, the following commands are supported: Alter, Backup, Batch, BeginTransaction, Cancel, ClearCache, CommitTransaction, Create, Delete, DesignAggregations, Drop, Insert, Lock, MergePartitions, NotifyTableChange, Process, Restore, RollbackTransaction, Statement (used to execute MDX queries and DMX statements), Subscribe, Synchronize, Unlock, Update, UpdateCells.  
  
 For TMSL: Alter, Create, Delete, MergePartitions, Process, Update.  
  
 This cmdlet supports the –Credential parameter, which can be used if you configured the Analysis Services instance for HTTP access. The –Credential parameter takes a PSCredential object that provides a Windows user identity. IIS will then impersonate this user when connecting to Analysis Services. The identity must have system administrator permissions on the Analysis Services instance to run the script.  
  
## Parameters  
  
### -Query \<string>  
 Specifies the actual script, query, or statement directly on the command line instead of in a file. You can also specify a query as pipeline input. You must specify a value for either the **–InputFile** or the **–Query** parameter when using **Invoke-AsCmd**.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|True (ByValue)|  
|Accept wildcard characters?|false|  
  
### -InputFile \<string>  
 Identifies the file that contains the XMLA script, MDX query, DMX statement, or TMSL script (in JSON). You must specify a value for either the **–InputFile** or the **–Query** parameter when using **Invoke-AsCmd**.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Server \<string>  
 Specifies the Analysis Services instance to which the cmdlet will connect and execute. If no server name is provided, a connection will be made to localhost. For default instances, specify just the server name. For named instances, use the format servername\instancename. For HTTP connections, use the format http[s]://server[:port]/virtualdirectory/msmdpump.dll.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|localhost|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Database \<string>  
 Specifies the database against which an MDX query or DMX statement will execute. The database parameter is ignored when the cmdlet executes an XMLA script, because the database name is embedded in the XMLA script.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Credential \<PSCredential>  
 Specifies a PSCredential object that provides a Windows user name and password. Specify this parameter only if the Analysis Services instance is configured for HTTP access, using Basic authentication. For native connections that use integrated security, this parameter is ignored.  
  
 If this parameter is present, the credentials that it provides are appended to the connection string. IIS will impersonate this user identity when connecting to Analysis Services. If no credentials are specified, the default windows account of the user who is running the tool will be used.  
  
 To use this parameter, first create a PSCredential object using Get-Credential to specify the username and password (for example, `$Cred=Get-Credential “adventure-works\admin”`. You can then pipe this object to the –Credential parameter `(-Credential:$Cred`).  
  
 For more information about authentication and credential usage, see [PowerShell scripting in Analysis Services](../../analysis-services/instances/powershell-scripting-in-analysis-services.md). For more information about HTTP access, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|True (ByValue)|  
|Accept wildcard characters?|false|  
  
### -ConnectionTimeout \<int>  
 Specifies the number of seconds before the connection to the Analysis Services instance times out. The timeout value must be an integer between 0 and 65534. If 0 is specified, connection attempts do not time out.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|30|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -QueryTimeout \<int>  
 Specifies the number of seconds before queries time out. If a timeout value is not specified, the queries do not time out. The timeout must be an integer between 1 and 65535.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|30|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Variable \<string[]>  
 Specifies additional scripting variables. Each variable is a name- value pair. If the value contains embedded spaces or control characters, it must be enclosed in double-quotation marks. Use a PowerShell array to specify multiple variables and their values.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -TraceFile \<string>  
 Identifies a file that receives Analysis Services trace events while executing XMLA script, MDX query, or DMX statement. If the file already exists, it is automatically overwritten (except for trace files that are created by using the -TraceLevel:Duration and –TraceLevel:DurationResult parameter settings). File names that contain spaces must be enclosed in quotation marks (" "). If the file name is not valid, an error message is generated.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -TraceFileFormat \<string>  
 Specifies the file format for the –TraceFile parameter (if this parameter is specified). The available options are text or csv. The default value is “csv".  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|csv|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -TraceFileDelimiter \<string>  
 Specifies which character to use as the trace file delimiter when you specify csv as the trace file format. Default is | (pipe, or vertical bar).  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -TraceTimeout \<int>  
 Specifies the number of seconds the Analysis Services engine waits before ending the trace (if you specify the –TraceFile parameter). The trace is considered finished if no trace messages have been recorded during the specified time period. The default trace time-out value is 5 seconds.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|5|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -TraceLevel \<TraceLevelOption>  
 Specifies what data is collected and recorded in the trace file. Possible values are High, Medium, Low, Duration, DurationResult.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|High|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: -Verbose, -Debug, -ErrorAction, -ErrorVariable, -OutBuffer, and -OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|PSObject|  
|Outputs|String|  
  
## Example 1 (XMLA input file)  
  
```  
Invoke-ASCmd –InputFile:"C:\MyFolder\DiscoverConnections.xmla"  
```  
  
 This command runs an XMLA script that returns the list of active connections on the server. The file DiscoverConnections.xmla contains the following XMLA script:  
  
```  
<Discover xmlns="urn:schemas-microsoft-com:xml-analysis">  
<RequestType>DISCOVER_CONNECTIONS</RequestType>  
<Restrictions />  
   <Properties>  
      <PropertyList>  
         <Content>Data</Content>  
      </PropertyList>  
   </Properties>  
</Discover>  
```  
  
## Example 2 (TMSL input file)  
 This example is identical to the first, except the script is TMSL (JSON) and requires a Tabular instance of SQL Server 2016. You can generate a TMSL script in SQL Server Management Studio.  
  
 If have multiple instances and your Tabular instance is a named instance, remember to set the server name:  
  
```  
Invoke-ASCmd –InputFile "C:\folder-name\T1200-NewDB.json" -Server "server-name\instance-name"  
```  
  
## Example 3 (Query)  
  
```  
Invoke-ASCmd -Database:"Adventure Works DW" -Query:"<Discover xmlns='urn:schemas-microsoft-com:xml analysis'><RequestType>DISCOVER_DATASOURCES</RequestType><Restrictions></Restrictions><Properties></Properties></Discover>"  
```  
  
 The Discover XMLA query returns available data sources for the Analysis server and the information required to connect to them. The results are in XML. For improved readability, you can pipe the output to an XML file (for example, append `| Out-file C:\Results\XMLAQueryOutput.xml` to the command) and view the results in a browser or other application that supports structured XML.  
  
## See Also  
 [PowerShell scripting in Analysis Services](../../analysis-services/instances/powershell-scripting-in-analysis-services.md)   
 [Manage Tabular Models Using PowerShell](http://go.microsoft.com/fwlink/?linkID=227685)  
  
  