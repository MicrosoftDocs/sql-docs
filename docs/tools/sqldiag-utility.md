---
title: "SQLdiag Utility"
description: Use the SQLdiag utility to collect logs and data files from SQL Server and other types of servers, and monitor servers over time or troubleshoot problems.
author: cwebb-msft
ms.author: chweb
ms.reviewer: jopilov
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: concept-article
ms.collection:
  - data-tools
helpviewer_keywords:
  - "command prompt utilities [SQL Server], SQLdiag"
  - "stopping diagnostic collection"
  - "storing diagnostic information"
  - "performance [SQL Server], diagnostic collection"
  - "diagnostic records [SQL Server]"
  - "scripts [SQL Server], diagnostic collection"
  - "logs [SQL Server], diagnostic collection"
  - "starting diagnostic collection"
  - "clustered instance of SQL Server"
  - "monitoring performance [SQL Server], diagnostic collection"
  - "security [SQL Server], diagnostic collection"
  - "SQLDIAG service"
  - "space [SQL Server], diagnostic collection"
  - "SQLdiag utility"
  - "disk space [SQL Server], diagnostic collection"
  - "configuration files [SQL Server]"
  - "automatic diagnostic collection"
  - "clusters [SQL Server], diagnostic collection"
---
# SQLdiag utility

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

The **SQLdiag** utility is a general purpose diagnostics collection utility that can be run as a console application or as a service. You can use **SQLdiag** to collect logs and data files from [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and other types of servers, and use it to monitor your servers over time or troubleshoot specific problems with your servers. **SQLdiag** is intended to expedite and simplify diagnostic information gathering for [!INCLUDE [msCoName](../includes/msconame-md.md)] Customer Support Services.

> [!NOTE]  
> This utility might be changed, and applications or scripts that rely on its command line arguments or behavior might not work correctly in future releases.

**SQLdiag** can collect the following types of diagnostic information:

- Windows performance logs
- Windows event logs
- [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)] traces
- [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] blocking information
- [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] configuration information

You can specify what types of information you want **SQLdiag** to collect by editing the configuration file `SQLdiag.xml`, which is described in a following section.

## Syntax

```console
sqldiag
    { [ /? ] }
    |
    {
      [ /I configuration_file ]
      [ /O output_folder_path ]
      [ /P support_folder_path ]
      [ /N output_folder_management_option ]
      [ /M machine1 [ machine2 machineN ] | @machinelistfile ]
      [ /C file_compression_type ]
      [ /B [+]start_time ]
      [ /E [+]stop_time ]
      [ /A SQLdiag_application_name ]
      [ /T { tcp [ ,port ] | np | lpc } ]
      [ /Q ] [ /G ] [ /R ] [ /U ] [ /L ] [ /X ]
    }
    |
    { [ START | STOP | STOP_ABORT ] }
    |
    { [ START | STOP | STOP_ABORT ] /A SQLdiag_application_name }
```

## Arguments

#### /?

Displays usage information.

#### /I *configuration_file*

Sets the configuration file for **SQLdiag** to use. By default, `/I` is set to `SQLdiag.xml`.

#### /O *output_folder_path*

Redirects **SQLdiag** output to the specified folder. If the `/O` option isn't specified, **SQLdiag** output is written to a subfolder named `SQLDIAG` under the **SQLdiag** startup folder. If the `SQLDIAG` folder doesn't exist, **SQLdiag** attempts to create it.

> [!NOTE]  
> The output folder location is relative to the support folder location that can be specified with `/P`. To set an entirely different location for the output folder, specify the full directory path for `/O`.

#### /P *support_folder_path*

Sets the support folder path. By default, `/P` is set to the folder where the **SQLdiag** executable resides. The support folder contains **SQLdiag** support files, such as the XML configuration file, Transact-SQL scripts, and other files that the utility uses during diagnostics collection. If you use this option to specify an alternate support files path, **SQLdiag** automatically copies the support files it requires to the specified folder if they don't already exist.

To set your current folder as the support path, specify `%cd%` on the command line as follows:

```console
sqldiag /P %cd%
```

#### /N *output_folder_management_option*

Sets whether **SQLdiag** overwrites or renames the output folder when it starts up. Available options:

- 1 = Overwrites the output folder (default)
- 2 = When **SQLdiag** starts up, it renames the output folder to `SQLDIAG_00001`, `SQLDIAG_00002`, and so on. After renaming the current output folder, **SQLdiag** writes output to the default output folder `SQLDIAG`.

**SQLdiag** doesn't append output to the current output folder when it starts up. It can only overwrite the default output folder (option 1) or rename the folder (option 2), and then it writes output to the new default output folder named `SQLDIAG`.

#### /M *machine1* [ *machine2* *machineN* ] | *@machinelistfile*

Overrides the machines specified in the configuration file. By default the configuration file is `SQLdiag.xml`, or is set with the `/I` parameter. When specifying more than one machine, separate each machine name with a space.

The *@machinelistfile* option specifies a machine list file name to be stored in the configuration file.

#### /C *file_compression_type*

Sets the type of file compression used on the **SQLdiag** output folder files. Available options:

- 0 = none (default)
- 1 = uses NTFS compression

#### /B [+]*start_time*

Specifies the date and time to start collecting diagnostic data in the following format: `yyyyMMdd_HH:mm:ss`

The time is specified using 24-hour notation. For example, 2:00 P.M. should be specified as `14:00:00`.

Use `+` without the date (HH:mm:ss only) to specify a time that is relative to the current date and time. For example, if you specify `/B +02:00:00`, **SQLdiag** waits 2 hours before it starts collecting information.

Don't insert a space between `+` and the specified *start_time*.

If you specify a start time that is in the past, **SQLdiag** forcibly changes the start date so the start date and time are in the future. For example, if you specify `/B 01:00:00` and the current time is 08:00:00, **SQLdiag** forcibly changes the start date so that the start date is the next day.

**SQLdiag** uses the local time on the computer where the utility is running.

#### /E [+]*stop_time*

Specifies the date and time to stop collecting diagnostic data in the following format: `yyyyMMdd_HH:mm:ss`

The time is specified using 24-hour notation. For example, 2:00 P.M. should be specified as `14:00:00`.

Use `+` without the date (HH:mm:ss only) to specify a time that is relative to the *start* date and time. For example, if you specify a start time and end time by using `/B +02:00:00 /E +03:00:00`, **SQLdiag** waits 2 hours before it starts collecting information, then collects information for 3 hours before it stops and exits. If `/B` isn't specified, **SQLdiag** starts collecting diagnostics immediately and ends at the date and time specified by `/E`.

Don't insert a space between `+` and the specified *start_time* or *end_time*.

**SQLdiag** uses the local time on the computer where the utility is running.

#### /A *SQLdiag_application_name*

Enables running multiple instances of the **SQLdiag** utility against the same [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance.

Each *SQLdiag_application_name* identifies a different instance of **SQLdiag**. No relationship exists between a *SQLdiag_application_name* instance and a [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance name.

*SQLdiag_application_name* can be used to start or stop a specific instance of the **SQLdiag** service.

In this example, replace `<SQLdiag_application_name>` with the appropriate value for *SQLdiag_application_name*:

```console
sqldiag START /A <SQLdiag_application_name>
```

It can also be used with the `/R` option to register a specific instance of **SQLdiag** as a service. In this example, replace `<SQLdiag_application_name>` with the appropriate value for *SQLdiag_application_name*:

```console
sqldiag /R /A <SQLdiag_application_name>
```

> [!NOTE]  
> **SQLdiag** automatically prefixes `DIAG$` to the instance name specified for *SQLdiag_application_name*. This provides a sensible service name if you register **SQLdiag** as a service.

#### /T *protocol*

Connects to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] using one of the following protocol values.

| Protocol&nbsp;(and&nbsp;port) | Description |
| --- | --- |
| **tcp [ ,*port* ]** | Transmission Control Protocol/Internet Protocol (TCP/IP). You can optionally specify a port number for the connection. |
| **np** | Named pipes. By default, the default instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] listens on named pipe `\\.\pipe\sql\query` and `\\.\pipe\MSSQL$<instancename>\sql\query` for a named instance. You can't connect to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] by using an alternate pipe name. |
| **lpc** | Local procedure call. This shared memory protocol is available if the client is connecting to an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on the same computer. |

#### /Q

Runs **SQLdiag** in quiet mode. `/Q` suppresses all prompts, such as password prompts.

#### /G

Runs **SQLdiag** in generic mode. When `/G` is specified, on startup **SQLdiag** doesn't enforce [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] connectivity checks or verify that the user is a member of the **sysadmin** fixed server role. Instead, **SQLdiag** defers to Windows to determine whether a user has the appropriate rights to gather each requested diagnostic.

If `/G` isn't specified, **SQLdiag** checks to determine whether the user is a member of the Windows **Administrators** group, and doesn't collect [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] diagnostics if the user isn't an **Administrators** group member.

#### /R

Registers **SQLdiag** as a service. Any command line arguments that are specified when you register **SQLdiag** as a service are preserved for future runs of the service.

When **SQLdiag** is registered as a service, the default service name is `SQLDIAG`. You can change the service name by using the `/A` argument.

Use the `START` command line argument to start the service:

```console
sqldiag START
```

You can also use the `net start` command to start the service:

```console
net start SQLDIAG
```

#### /U

Unregisters **SQLdiag** as a service.

Use the `/A` argument also if unregistering a named **SQLdiag** instance.

#### /L

Runs **SQLdiag** in continuous mode when a start time or end time is also specified with the `/B` or `/E` arguments, respectively. **SQLdiag** automatically restarts after diagnostics collection stops due to a scheduled shutdown. For example, by using the `/E` or the `/X` arguments.

> [!NOTE]  
> **SQLdiag** ignores the `/L` argument if a start time or end time isn't specified by using the `/B` and `/E` command line arguments.

Using `/L` doesn't imply the service mode. To use `/L` when running **SQLdiag** as a service, specify it on the command line when you register the service.

#### /X

Runs **SQLdiag** in snapshot mode. **SQLdiag** takes a snapshot of all configured diagnostics and then shuts down automatically.

#### START | STOP | STOP_ABORT

Starts or stops the **SQLdiag** service. `STOP_ABORT` forces the service to shut down as quickly as possible without finishing collection of diagnostics it's currently collecting.

When these service control arguments are used, they must be the first argument used on the command line. For example:

```console
sqldiag START
```

Only the `/A` argument, which specifies a named instance of **SQLdiag**, can be used with `START`, `STOP`, or `STOP_ABORT` to control a specific instance of the **SQLdiag** service. In this example, replace `<SQLdiag_application_name>` with the appropriate value for *SQLdiag_application_name*:

```console
sqldiag START /A <SQLdiag_application_name>
```

## Security requirements

Unless **SQLdiag** is run in generic mode (by specifying the `/G` command line argument), the user who runs **SQLdiag** must be a member of the Windows **Administrators** group and a member of the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] **sysadmin** fixed server role. By default, **SQLdiag** connects to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] by using Windows Authentication, but it also supports [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Authentication.

## Performance considerations

The performance effects of running **SQLdiag** depend on the type of diagnostic data you have configured it to collect. For example, if you have configured **SQLdiag** to collect [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)] tracing information, the more event classes you choose to trace, the more your server performance is affected.

The performance impact of running **SQLdiag** is approximately equivalent to the sum of the costs of collecting the configured diagnostics separately. For example, collecting a trace with **SQLdiag** incurs the same performance cost as collecting it with [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)]. The performance impact of using **SQLdiag** is negligible.

## Required disk space

Because **SQLdiag** can collect different types of diagnostic information, the free disk space that is required to run **SQLdiag** varies. The amount of diagnostic information collected depends on the nature and volume of the workload that the server is processing and might range from a few megabytes to several gigabytes.

## Configuration files

On startup, **SQLdiag** reads the configuration file and the command line arguments that have been specified. You specify the types of diagnostic information that **SQLdiag** collects in the configuration file. By default, **SQLdiag** uses the `SQLdiag.xml` configuration file, which is extracted each time the tool runs and is located in the **SQLdiag** utility startup folder. The configuration file uses the XML schema, SQLDiag_schema.xsd, which is also extracted into the utility startup directory from the executable file each time **SQLdiag** runs.

### Edit the configuration files

You can copy and edit `SQLdiag.xml` to change the types of diagnostic data that **SQLdiag** collects. When editing the configuration file always use an XML editor that can validate the configuration file against its XML schema, such as [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)]. You shouldn't edit `SQLdiag.xml` directly. Instead, make a copy of `SQLdiag.xml` and rename it to a new file name in the same folder. Then edit the new file, and use the `/I` argument to pass it to **SQLdiag**.

### Edit the configuration file when SQLdiag runs as a service

If you have already run **SQLdiag** as a service and need to edit the configuration file, unregister the `SQLDIAG` service by specifying the `/U` command line argument and then re-register the service by using the `/R` command line argument. Unregistering and re-registering the service removes old configuration information that was cached in the Windows registry.

## Output folder

If you don't specify an output folder with the `/O` argument, **SQLdiag** creates a subfolder named `SQLDIAG` under the **SQLdiag** startup folder. For diagnostic information collection that involves high volume tracing, such as [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)], make sure that the output folder is on a local drive with enough space to store the requested diagnostic output.

When **SQLdiag** is restarted, it overwrites the contents of the output folder. To avoid this, specify `/N 2` on the command line.

## Data collection process

When **SQLdiag** starts, it performs the initialization checks necessary to collect the diagnostic data that have been specified in `SQLdiag.xml`. This process might take several seconds. After **SQLdiag** has started collecting diagnostic data when it runs as a console application, a message displays informing you that **SQLdiag** collection has started and that you can press CTRL+C to stop it. When **SQLdiag** is run as a service, a similar message is written to the Windows event log.

If you're using **SQLdiag** to diagnose a problem that you can reproduce, wait until you receive this message before you reproduce the problem on your server.

**SQLdiag** collects most diagnostic data in parallel. All diagnostic information is collected by connecting to tools, such as the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] **sqlcmd** utility or the Windows command processor, except when information is collected from Windows performance logs and event logs. **SQLdiag** uses one worker thread per computer to monitor the diagnostic data collection of these other tools, often simultaneously waiting for several tools to complete. During the collection process, **SQLdiag** routes the output from each diagnostic to the output folder.

## Stop data collection

After **SQLdiag** starts collecting diagnostic data, it continues to do so unless you stop it manually via `Ctrl+C`, or you create a `sqldiag.stop` file, or you configure it to stop at a specified time. You can configure **SQLdiag** to stop at a specific time by using the `/E` argument, or by using the `/X` argument, which causes **SQLdiag** to run in snapshot mode.

When **SQLdiag** stops, it stops all diagnostics it has started. For example, it stops [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)] traces it was collecting, it stops executing [!INCLUDE [tsql](../includes/tsql-md.md)] scripts it was running, and it stops any sub processes it has spawned during data collection. After diagnostic data collection has completed, **SQLdiag** exits.

### Stop SQLdiag when running as a console application

If you're running **SQLdiag** as a console application, press CTRL+C in the console window where **SQLdiag** is running to stop it. After you press CTRL+C, a message displays in the console window informing you that **SQLdiag** data collection is ending, and that you should wait until the process shuts down, which might take several minutes.

Press Ctrl+C twice to terminate all child diagnostic processes and immediately terminate the application.

### Stop SQLdiag when running as a service

If you're running **SQLdiag** as a service, run `sqldiag STOP` in the **SQLdiag** startup folder to stop it. Or, you can stop the **SQLdiag** services in the **Services.msc** applet.

> [!NOTE]  
> Pausing the **SQLdiag** service isn't supported. If you attempt to pause the **SQLdiag** service, it stops after it finishes collecting the diagnostics that it was collecting when you paused it. If you restart **SQLdiag** after stopping it, the application restarts and overwrites the output folder. To avoid overwriting the output folder, specify `/N 2` on the command line.

If you're running multiple instances of **SQLdiag** on the same computer, you can also pass the **SQLdiag** instance name to on the command line when you stop the service. For example, to stop a **SQLdiag** instance named Instance1, use the following syntax:

```console
sqldiag STOP /A Instance1
```

`/A` is the only command-line argument that can be used with `START`, `STOP`, or `STOP_ABORT`. If you need to specify a named instance of **SQLdiag** with one of the service control verbs, specify `/A` after the control verb on the command line as shown in the previous syntax example. When control verbs are used, they must be the first argument on the command line.

To stop the service as quickly as possible, run `sqldiag STOP_ABORT` in the utility startup folder. This command aborts any diagnostics collecting currently being performed without waiting for them to finish.

> [!NOTE]  
> Use `sqldiag STOP` or `sqldiag STOP_ABORT` to stop the **SQLdiag** service. Don't use the Windows Services Console to stop **SQLdiag** or other [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] services.

## Stop SQLdiag using sqldiag.stop file

**SQLdiag** also shuts down automatically when it finds a file named `sqldiag.stop` in the utility's `\Output` folder. This option applies regardless if **SQLdiag** runs as a console app or as a service. Creating a `.stop` file can be useful when you want to programmatically shut down **SQLdiag** after some event occurs, but you don't know in advance the time that this event occurs. The contents of the `sqldiag.stop` file are irrelevant. One option, besides manually creating the file, is to use a command like the following in a batch file to create `sqldiag.stop`:

```console
ECHO stop > F:\PSSDIAG\Output\sqldiag.stop
```

Another option is to use PowerShell:

```powershell
Set-Content -Value "stop" -Path "F:\PSSDIAG\Output\sqldiag.stop"
```

## Automatically start and stop SQLdiag

To automatically start and stop diagnostic data collection at a specified time, use the `/B <start_time>` and `/E <stop_time>` arguments, using 24-hour notation. For example, if you're troubleshooting a problem that consistently appears at approximately 02:00:00, you can configure **SQLdiag** to automatically start collecting diagnostic data at 01:45 and automatically stop at 03:00:00.

Use the `/B` and `/E` arguments to specify the start and stop time. Use 24-hour notation to specify an exact start and stop date and time with the general format yyyyMMdd_HH:mm:ss. The following example starts data collection at 01:45 and stops it at 3:00.

```console
sqldiag /B 01:45:00 /E 03:00:00
```

To specify a relative start or stop time, prefix the start and stop time with `+` and omit the date portion (yyyyMMdd_) as shown in the following example. This causes **SQLdiag** to wait one hour before it starts collecting information, then it collects information for two and a half hours before it stops and exits:

```console
sqldiag /B +01:00:00 /E +02:30:00
```

When a relative *start_time* is specified, **SQLdiag** starts at a time that is relative to the current date and time. When a relative *end_time* is specified, **SQLdiag** ends at a time that is relative to the specified *start_time*. If the start or end date and time that you have specified is in the past, **SQLdiag** forcibly changes the start date so that the start date and time are in the future.

This has important implications on the start and end dates you choose. Consider the following example:

```console
sqldiag /B +01:00:00 /E 08:30:00
```

If the current time is 08:00, the end time passes before diagnostic collection actually begins. Because **SQLdiag** automatically adjusts start and end dates to the next day when they occur in the past, in this example diagnostic collection starts at 09:00 today (a relative start time has been specified at 1 hour from now using `+`) and continues collecting until 08:30 the following morning.

### Stop and restart SQLdiag to collect daily diagnostics

To collect a specified set of diagnostics on a daily basis without having to manually start and stop **SQLdiag**, use the `/L` argument. The `/L` argument causes **SQLdiag** to run continuously by automatically restarting itself after a scheduled shutdown. When `/L` is specified, and **SQLdiag** stops because it has reached the end time specified with the `/E` argument, or it stops because it's being run in snapshot mode by using the `/X` argument, **SQLdiag** restarts instead of exiting.

The following example specifies that **SQLdiag** run in continuous mode to automatically restart after diagnostic data collecting occurs between 03:00:00 and 05:00:00.

```console
sqldiag /B 03:00:00 /E 05:00:00 /L
```

The following example specifies that **SQLdiag** run in continuous mode to automatically restart after taking a diagnostic data snapshot at 03:00:00.

```console
sqldiag /B 03:00:00 /X /L
```

## Run SQLdiag as a service

When you want to use **SQLdiag** to collect diagnostic data for long periods of time during which you might need to sign out of the computer on which **SQLdiag** is running, you can run it as a service.

### Register SQLdiag to run as a service

You can register **SQLdiag** to run as a service by specifying the `/R` argument at the command line. This registers **SQLdiag** to run as a service. The **SQLdiag** service name is `SQLDIAG`. Any other arguments you specify on the command line when you register **SQLdiag** as a service are preserved and reused when the service is started.

To change the default `SQLDIAG` service name, use the `/A` command-line argument to specify another name. **SQLdiag** automatically prefixes DIAG$ to any **SQLdiag** instance name specified with `/A` to create sensible service names.

### Unregister the SQLDIAG service

To unregister the service, specify the `/U` argument. Unregistering **SQLdiag** as a service also deletes the Windows registry keys of the service.

### Start or restart the SQLDIAG service

To start or restart the `SQLDIAG` service, run `sqldiag START` from the command line.

If you're running multiple instances of **SQLdiag** by using the `/A` argument, you can also pass the **SQLdiag** instance name on the command line when you start the service. For example, to start a **SQLdiag** instance named Instance1, use the following syntax:

```console
sqldiag START /A Instance1
```

You can also use the `net start` command to start the `SQLDIAG` service.

When you restart **SQLdiag**, it overwrites the contents in the current output folder. To avoid this, specify `/N 2` on the command line to rename the output folder when the utility starts.

Pausing the **SQLdiag** service isn't supported.

## Run multiple instances of SQLdiag

Run multiple instances of **SQLdiag** on the same computer by specifying `/A <SQLdiag_application_name>` on the command line. This is useful for collecting different sets of diagnostics simultaneously from the same [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance. For example, you can configure a named instance of **SQLdiag** to continuously perform lightweight data collection. Then, if a specific problem occurs on [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], you can run the default **SQLdiag** instance to collect diagnostics for that problem, or to gather a set of diagnostics that [!INCLUDE [msCoName](../includes/msconame-md.md)] Customer Support Services asks you to gather to diagnose a problem.

## Collect diagnostic data from clustered SQL Server instances

**SQLdiag** supports collecting diagnostic data from clustered [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instances. To gather diagnostics from clustered [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instances, make sure that `"."` is specified for the `name` attribute of the `<Machine>` element in the configuration file `SQLdiag.xml` and don't specify the `/G` argument on the command line. By default, `"."` is specified for the `name` attribute in the configuration file and the `/G` argument is turned off. Typically, you don't need to edit the configuration file or change the command line arguments when collecting from a clustered [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance.

When `"."` is specified as the machine name, **SQLdiag** detects that it's running on a cluster, and simultaneously retrieves diagnostic information from all virtual instances of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] that are installed on the cluster. If you want to collect diagnostic information from only one virtual instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] that is running on a computer, specify that virtual [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] for the `name` attribute of the `<Machine>` element in `SQLdiag.xml`.

> [!NOTE]  
> To collect [!INCLUDE [ssSqlProfiler](../includes/sssqlprofiler-md.md)] trace information from clustered [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instances, administrative shares (ADMIN$) must be enabled on the cluster.

## Related content

- [SQL command-line utilities (Database Engine)](command-prompt-utility-reference-database-engine.md)
