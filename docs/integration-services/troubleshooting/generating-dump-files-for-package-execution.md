---
title: "Generating Dump Files for Package Execution | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 61ef1731-cb3a-4afb-b4a4-059b04aeade0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Generating Dump Files for Package Execution
  In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you can create debug dump files that provide information about the execution of a package. The information in these files can help you with troubleshooting package execution issues.  
  
> **NOTE!** Debug dump files might contain sensitive information. To help protect sensitive information, you can use an access control list (ACL) to restrict access to the files, or copy the files to a folder that has restricted access. For example, before you send your debug files to [!INCLUDE[msCoName](../../includes/msconame-md.md)] support services, we recommend that you remove any sensitive or confidential information.  
  
 When you deploy a project to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, you can create dump files that provide information about the execution of the packages contained in the project. When the ISServerExec.exe process ends, the dump files are created. You can specify that a dump file is created when errors occur during the package execution, by selecting the **Dump on errors** option in the **Execute Package** Dialog box. You can also use the following stored procedures:  
  
-   [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md)  
  
     Call this stored procedure to configure a dump file to be created when any error or event occurs, and when specific events occur, during a package execution.  
  
-   [catalog.create_execution_dump](../../integration-services/system-stored-procedures/catalog-create-execution-dump.md)  
  
     Call this stored procedure to cause a running package to pause and create a dump file.  
  
 If you are using the package deployment model, you create the debug dump files by using either the **dtexec** utility or the **dtutil** utility to specify a debug dump option in the command line. For more information, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md) and [dtutil Utility](../../integration-services/dtutil-utility.md). For more information about the package deployment model, see [Deploy Integration Services (SSIS) Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx) and [Legacy Package Deployment &#40;SSIS&#41;](../../integration-services/packages/legacy-package-deployment-ssis.md).   
  
## Debug dump file format  
 When you specify a debug dump option, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates the following debug dump files:  
  
-   A .mdmp debug dump file. This is a binary file.  
  
-   The .tmp debug dump file. This is a text formatted file.  
  
 By default, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] stores these files in the folder, *\<drive>:*\Program Files\Microsoft SQL Server\110\Shared\ErrorDumps.  
  
 The following table describes only certain sections in the .tmp file. The .tmp file includes additional data that is not listed in the table.  
  
|Type of information|Description|Example|  
|-------------------------|-----------------|-------------|  
|Environment|Operating system version, memory usage data, process ID, and process image name. The environment information is at the beginning of the .tmp file.|# SSIS Textual Dump taken at 9/13/2007 1:50:34 PM<br /><br /> #PID 4120<br /><br /> #Image Name [C:\Program Files\Microsoft SQL Server\110\DTS\Binn\DTExec.exe]<br /><br /> # OS major=6 minor=0 build=6000<br /><br /> # Running on 2 amd64 processors under WOW64<br /><br /> # Memory: 58% in use. Physical: 845M/2044M  Paging: 2404M/4095M (avail/total)|  
|Dynamic-link library (DLL) path and version|Path and version number of each DLL that the system loads during the processing of a package.|# Loaded Module: c:\bb\Sql\DTS\src\bin\debug\i386\DTExec.exe (10.0.1069.5)<br /><br /> # Loaded Module: C:\Windows\SysWOW64\ntdll.dll (6.0.6000.16386)<br /><br /> # Loaded Module: C:\Windows\syswow64\kernel32.dll (6.0.6000.16386)|  
|Recent messages|Recent messages issued by the system. Includes the time, type, description, and thread ID of each message.|[M:1]   Ring buffer entry:              (*pRecord)<br /><br /> [D:2]      <<\<CRingBufferLogging::RingBufferLoggingRecord>>> ( \@ 0282F1A8 )<br /><br /> [E:3]         Time Stamp: 2007-09-13 13:50:32.786      (szTimeStamp)<br /><br /> [E:3]         Thread ID: 2368           (ThreadID)<br /><br /> [E:3]         Event Name: OnError                        (EventName)<br /><br /> [E:3]         Source Name:                (SourceName)<br /><br /> [E:3]         Source ID:                        (SourceID)<br /><br /> [E:3]         Execution ID:                 (ExecutionGUID)<br /><br /> [E:3]         Data Code: -1073446879              (DataCode)<br /><br /> [E:3]         Description: The component is missing, not registered, not upgradeable, or missing required interfaces. The contact information for this component is "".|  
  
## Related information  
 [Execute Package Dialog Box](../../integration-services/packages/run-integration-services-ssis-packages.md#execute_package_dialog)  
  
  
