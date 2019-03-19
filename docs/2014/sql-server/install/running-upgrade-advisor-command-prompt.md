---
title: "Running Upgrade Advisor (Command Prompt) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor [SQL Server], running"
  - "command prompt [Upgrade Advisor]"
  - "UpgradeAdvisorWizardCmd utility"
  - "XML formats [Upgrade Advisor]"
ms.assetid: 7c83049b-9227-4723-9b7f-66288bc6bd1d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Running Upgrade Advisor (Command Prompt)
  Use the **UpgradeAdvisorWizardCmd** utility to run Upgrade Advisor from the command prompt. You can choose to receive results in XML format or in a file with comma-separated values.  
  
## Syntax  
  
```  
  
      UpgradeAdvisorWizardCmd [ -? ]  |   
    [ -ConfigFilefilename | <server_info> ]  
    [ -SqlUserlogin_id-SqlPasswordpassword ]  
    [ -CSV ]  
  
where <server_info> is any combination of the following:  
        -Serverserver_name-Instanceinstance_name-ASInstanceAS_instance_name-RSInstanceRS_instance_name  
```  
  
## Arguments  
 **-?**  
 Displays the command syntax.  
  
 **-ConfigFile** _filename_  
 Is the path name and file name of an XML file that contains settings to use when you run the **UpgradeAdvisorWizardCmd** utility.  
  
 *<server_info>*  
 Specifies which computer and instance to analyze. Use these options if you are not using a configuration file.  
  
 *<server_info>* can be any combination of the following four arguments:  
  
 **-Server** _server_name_  
 Specifies the name of the computer to analyze. This can be the local computer, which is the default value, or a remote computer.  
  
 **-Instance** _instance_name_  
 Specifies the name of the instance to analyze. There is no default value. If you do not specify this parameter, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is not scanned. The value for a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is MSSQLSERVER. For a named instance, use the instance name.  
  
 **-ASInstance**  _AS_instance_name_  
 Specifies the name of the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to analyze. There is no default value. If you do not specify this value, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is not scanned. The value for a default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is MSSQLServerOLAPService. For a named instance, use the instance name.  
  
 **-RSInstance**  _RS_instance_name_  
 Specifies the name of the instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to analyze. There is no default value. If you do not specify this value, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is not scanned. The value for a default instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is ReportServer. For a named instance, use the instance name.  
  
 **-SqlUser** _login_id_  
 If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, this value is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that Upgrade Advisor will use to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you do not specify a login, Windows Authentication is used to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 **-SqlPassword** _password_  
 If you use the **-SqlUser** argument, use this argument to specify the password for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 **-CSV**  
 Specifies to provide results as comma-separated values to a .csv file in addition to the standard XML results. Results are written to the My Documents\\[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor\110\Reports folder.  
  
## Return Values  
 The following table shows the values that **UpgradeAdvisorWizardCmd** returns.  
  
|Value|Description|  
|-----------|-----------------|  
|0|Analysis succeeded, no upgrade issues found.|  
|positive integer|Analysis succeeded, upgrade issues found.|  
|negative integer|Analysis failed.|  
  
## Remarks  
 All information that is required to run the analysis, other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication user names and passwords, can be provided in an XML configuration file. This XML configuration file is documented in the template. If you do not use a configuration file, you can analyze all installed components in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using default settings by specifying computer names and instance names. See the "Element Descriptions" table later in this topic for a description of the default configuration file settings.  
  
## Configuration File Template  
 Use the following XML as a template for creating your own configuration files. You can modify the template to meet the needs of your organization.  
  
```xml  
<Configuration>  
    <Server> </Server>  
    <Instance></Instance>  
    <Components>  
        <SQLServer>  
            <Databases>  
                <Database></Database>  
            </Databases>  
            <TraceFiles>  
                <TraceFile></TraceFile>  
            </TraceFiles>  
            <BatchFiles>  
                <BatchFile></BatchFile>  
            </BatchFiles>  
            <BatchSeparator></BatchSeparator>  
        </SQLServer>  
        <AnalysisServices>  
            <ASInstance></ASInstance>  
            <Databases>  
                <Database></Database>  
            </Databases>  
        </AnalysisServices>  
        <ReportingServices>  
            <RSInstance></RSInstance>  
        </ReportingServices>  
        <IntegrationServices>  
            <PackagePath></PackagePath>  
        </IntegrationServices>  
    </Components>  
</Configuration>  
```  
  
## Element Descriptions  
  
|Tag|Definition|Occurrence|  
|---------|----------------|----------------|  
|`Configuration`|Parent element for Upgrade Advisor configuration file.|Required once per configuration file.|  
|`Server`|Name of the server to analyze.|Optional once per configuration file. The default value is the local computer.|  
|`Instance`|Name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance to analyze.|Optional once per configuration file. The default value is the default instance.<br /><br /> Required once per configuration file, if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] element or an `IntegrationServices` element is present on the server.|  
|`Components`|Contains elements that specify which components to analyze.|Required once per configuration file.|  
|`SQLServer`|Contains analysis settings for an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|Optional once per configuration file. If not specified, [!INCLUDE[ssDE](../../includes/ssde-md.md)] databases are not analyzed.|  
|`Databases` for `SQLServer` element|Contains a list of databases to analyze.|Optional once per `SQLServer` element. If this element is not present, all databases in the instance are analyzed.|  
|`Database` for `SQLServer` element|Specifies the name of a database to analyze.|Required once or more if the `Databases` element is present. If a `Database` element contains the value "*", all databases in the instance are analyzed. There is no default value.|  
|`TraceFiles`|Contains a list of trace files to analyze.|Optional once per `SQLServer` element.|  
|`TraceFile`|Specifies the path and name of a trace file to analyze.|Required once or more if the `TraceFiles` element is present. There is no default value.|  
|`BatchFiles`|Contains a list of batch files to analyze.|Optional once per `SQLServer` element.|  
|`BatchFile`|Specifies a batch file to analyze. Can be multiple.|Required once or more if the `BatchFiles` element is present. There is no default value.|  
|`BatchSeparator`|Specifies the batch separator used in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] batch files.|Optional once per `SQLServer` element. The default value is GO.|  
|`AnalysisServices`|Contains analysis settings for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|Optional once per configuration file. If not specified, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases are not analyzed.|  
|`ASInstance`|Specifies the name of an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|Required once per `AnalysisServices` element. There is no default value.|  
|`Databases` for `Analysis Services` element|Contains a list of databases to analyze.|Optional once per `AnalysisServices` element. If this element is not present, all databases in the instance are analyzed.|  
|`Database` for `AnalysisServices` element|Specifies the name of a database to analyze.|Required once or more if the `Databases` element is present. If a `Database` element contains the value "*", all databases in the instance are analyzed. There is no default value.|  
|`ReportingServices`|Specifies to run analysis against [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|Optional once per configuration file. If not specified, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is not analyzed.|  
|`RSInstance`|Specifies the name of an instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|Required once per `ReportingServices` element. There is no default value.|  
|`IntegrationServices`|Contains analysis settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].|Optional once per configuration file. If not specified, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is not analyzed.|  
|`PackagePath`|Specifies the path of a set of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.|Optional once per `IntegrationServices` element. If this element is not present, analysis occurs on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and no externally stored packages are analyzed. There is no default value.|  
  
## Examples  
  
### A. Run Upgrade Advisor Using a Configuration File  
 The following example shows how to run Upgrade Advisor from the command prompt by using a configuration file that specifies what to analyze. This example uses Windows Authentication to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
UpgradeAdvisorWizardCmd -ConfigFile "C:\My Documents\UpgradeConfig1.xml"  
```  
  
### B. Run Upgrade Advisor Using Default Configuration Settings  
 The following example shows how to run Upgrade Advisor from the command prompt by using default configuration settings and Windows Authentication.  
  
```  
UpgradeAdvisorWizardCmd -Server MyServer -Instance MyInst   
```  
  
### C. Run Upgrade Advisor Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication  
 The following example shows how to run Upgrade Advisor from the command prompt by using a configuration file. This example specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user name and password to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
UpgradeAdvisorWizardCmd -ConfigFile "C:\My Documents\UpgradeConfig1.xml"   
    -SqlUser "MyUserName" -SqlPassword "QweRTy-55"  
```  
  
## See Also  
 [Resolving Upgrade Issues](../../../2014/sql-server/install/resolving-upgrade-issues.md)   
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)   
 [Running Upgrade Advisor &#40;User Interface&#41;](../../../2014/sql-server/install/running-upgrade-advisor-user-interface.md)  
  
  
