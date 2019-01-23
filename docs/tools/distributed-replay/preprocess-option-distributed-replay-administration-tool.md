---
title: "Preprocess Option (Distributed Replay Administration Tool) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 9b5012fd-233e-4a25-a2e1-585c63b70502
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Preprocess Option (Distributed Replay Administration Tool)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributed Replay administration tool, **DReplay.exe**, is a command-line tool that you can use to communicate with the distributed replay controller. This topic describes the **preprocess** command-line option and corresponding syntax.  
  
 The **preprocess** option initiates the preprocess stage. During this stage, the controller prepares the input trace data for replay against the target server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```  
  
dreplay preprocess [-m controller] -i input_trace_file  
    -d controller_working_dir [-c config_file] [-f status_interval]  
```  
  
#### Parameters  
 **-m** _controller_  
 Specifies the computer name of the controller. You can use "`localhost`" or "`.`" to refer to the local computer.  
  
 If the **-m** parameter is not specified, the local computer is used.  
  
 **-i** _input_trace_file_  
 Specifies the full path of the input trace file on the controller, such as `D:\Mytrace.trc`. The **-i** parameter is required.  
  
 If there are rollover files in the same directory, they will be loaded and used automatically. The files must follow the file rollover naming convention, for example: `Mytrace.trc`, `Mytrace_1.trc`, `Mytrace_2.trc`, `Mytrace_3.trc`, ... `Mytrace_n.trc`.  
  
> [!NOTE]  
>  If you are using the administration tool on a different computer than the controller, you will need to copy the input trace files to the controller so that a local path can be used for this parameter.  
  
 **-d** _controller_working_dir_  
 Specifies the directory on the controller where the intermediate file will be stored. The **-d** parameter is required.  
  
 The following requirements apply:  
  
-   The directory must reside on the controller.  
  
-   You must specify the full path, starting with a drive letter (for example, `c:\WorkingDir`).  
  
-   The path must not end with a backslash "`\`".  
  
-   UNC paths are not supported.  
  
 **-c** _config_file_  
 Is the full path of the preprocess configuration file; used to specify the location of the preprocess configuration file when stored in a different location. This parameter can be a UNC path, or can reside locally on the computer where you run the administration tool.  
  
 The **-c** parameter is not required if no filtering is needed, or if you do not want to modify the maximum idle time.  
  
 Without the **-c** parameter, the default preprocess configuration file, `DReplay.exe.preprocess.config`, is used.  
  
 **-f** _status_interval_  
 Specifies the frequency (in seconds) at which to display status messages.  
  
 If **-f** is not specified, the default interval is 30 seconds.  
  
## Examples  
 In this example, the preprocess stage is initiated with all of the default settings. The value `localhost` indicates that the controller service is running on the same computer as the administration tool. The *input_trace_file* parameter specifies the location of the input trace data, `c:\mytrace.trc`. Because there is no trace file filtering involved, the **-c** parameter does have to be specified.  
  
```  
dreplay preprocess -m localhost -i c:\mytrace.trc -d c:\WorkingDir  
```  
  
 In this example, the preprocess stage is initiated and a modified preprocess configuration file is specified. Unlike the previous example, the **-c** parameter is used to point to the modified configuration file, if you have stored it in a different location. For example:  
  
```  
dreplay preprocess -m localhost -i c:\mytrace.trc -d c:\WorkingDir -c c:\DReplay.exe.preprocess.config  
```  
  
 In the modified preprocess configuration file, a filter condition is added that filters out system sessions during distributed replay. The filter is added by modifying the `<PreprocessModifiers>` element in the preprocess configuration file, `DReplay.exe.preprocess.config`.  
  
 The following shows an example of the modified configuration file:  
  
```  
<?xml version='1.0'?>  
<Options>  
    <PreprocessModifiers>  
        <IncSystemSession>No</IncSystemSession>  
        <MaxIdleTime>-1</MaxIdleTime>  
    </PreprocessModifiers>  
</Options>  
```  
  
## Permissions  
 You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.  
  
 For more information, see [Distributed Replay Security](../../tools/distributed-replay/distributed-replay-security.md).  
  
## See Also  
 [Prepare the Input Trace Data](../../tools/distributed-replay/prepare-the-input-trace-data.md)   
 [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md)   
 [Configure Distributed Replay](../../tools/distributed-replay/configure-distributed-replay.md)  
  
  
