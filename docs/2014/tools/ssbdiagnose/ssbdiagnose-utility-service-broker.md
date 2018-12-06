---
title: "ssbdiagnose Utility (Service Broker) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "Service Broker, runtime reports"
  - "Service Broker, command prompt utilities"
  - "troubleshooting [Service Broker], conversations"
  - "troubleshooting [Service Broker], configurations"
  - "command prompt utilities [Service Broker]"
  - "Service Broker, troubleshooting"
  - "Service Broker, configuration reports"
  - "Service Broker, tools"
  - "troubleshooting [Service Broker], runtime"
  - "conversations [Service Broker], troubleshooting"
  - "troubleshooting [Service Broker], ssbdiagnose utility"
  - "tools [Service Broker], ssbdiagnose"
  - "Service Broker, ssbdiagnose utility"
  - "ssbdiagnose"
ms.assetid: 0c1636e8-a3db-438e-be4c-1ea40d1f4877
author: stevestein
ms.author: sstein
manager: craigg
---
# ssbdiagnose Utility (Service Broker)
  The **ssbdiagnose** utility reports issues in [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversations or the configuration of [!INCLUDE[ssSB](../../includes/sssb-md.md)] services. Configuration checks can be made for either two services or a single service. Issues are reported either in the command prompt window as human-readable text, or as formatted XML that can be redirected to a file or another program.  
  
## Syntax  
  
```  
  
      ssbdiagnose   
[ [ -XML ]  
    [ -LEVEL { ERROR | WARNING | INFO } ]  
  [-IGNOREerror_id ] [ ...n]  
    [ <baseconnectionoptions> ]  
  { <configurationreport> | <runtimereport> }  
]  
| -?  
  
<configurationreport> ::=  
    CONFIGURATION  
  { [ FROM SERVICEservice_name  
      [ <fromconnectionoptions> ]  
      [ MIRROR <mirrorconnectionoptions> ]  
    ]  
    [ TO SERVICEservice_name[, broker_id ]  
      [ <toconnectionoptions> ]  
      [ MIRROR <mirrorconnectionoptions> ]  
    ]  
  }  
    ON CONTRACTcontract_name  
  [ ENCRYPTION { ON | OFF | ANONYMOUS } ]  
  
<runtime_report> ::=  
    RUNTIME  
    [-SHOWEVENTS ]  
        [ -NEW  
         [ -ID { conversation_handle  
                | conversation_group_id  
                 | conversation_id  
                  }  
        ] [ ...n]  
        ]  
    [ -TIMEOUTtimeout_interval ]  
    [ <runtimeconnectionoptions> ]  
  
<baseconnectionoptions> ::=  
  <connectionoptions>  
  
<fromconnectionoptions> ::=  
  <connectionoptions>  
  
<toconnectionoptions> ::=  
  <connectionoptions>  
  
<mirrorconnectionoptions> ::=  
  <connectionoptions>  
  
<runtimeconnectionoptions> ::=  
  [ CONNECT TO <connectionoptions> ] [ ...n]  
  
<connectionoptions> ::=  
    [ -E | { -Ulogin_id [ -Ppassword ] } ]  
  [ -Sserver_name[\instance_name] ]  
  [ -ddatabase_name ]  
  [ -llogin_timeout ]  
  
```  
  
## Command Line Options  
 **-XML**  
 Specifies that the **ssbdiagnose** output be generated as formatted XML. This can be redirected to a file or to another application. If **-XML** is not specified, the **ssbdiagnose** output is formatted as human-readable text.  
  
 **-LEVEL** { **ERROR** | **WARNING** | **INFO**}  
 Specifies the level of messages to report.  
  
 **ERROR**: report only error messages.  
  
 **WARNING**: report error and warning messages.  
  
 **INFO**: report error, warning, and informational messages.  
  
 The default setting is **WARNING**.  
  
 **-IGNORE** *error_id*  
 Specifies that errors or messages that have the specified *error_id* not be included in reports. You can specify **-IGNORE** multiple times to suppress multiple message IDs.  
  
 **\<baseconnectionoptions>**  
 Specifies the base connection information that is used by **ssbdiagnose** when connection options are not included in a specific clause. The connection information that is given in a specific clause overrides the **baseconnectionoption** information. This is performed separately for each parameter. For example, if both **-S** and **-d** are specified in **baseconnetionoptions**, and only **-d** is specified in **toconnetionoptions**, **ssbdiagnose** uses -S from **baseconnetionoptions** and -d from **toconnetionoptions**.  
  
 **CONFIGURATION**  
 Requests a report of configuration errors between a pair of [!INCLUDE[ssSB](../../includes/sssb-md.md)] services, or for a single service.  
  
 **FROM SERVICE** *service_name*  
 Specifies the service that initiates conversations.  
  
 **\<fromconnectionoptions>**  
 Specifies the information that is required to connect to the database that holds the initiator service. If **fromconnectionoptions** is not specified, **ssbdiagnose** uses the connection information from **baseconnectionoptions** to connect to the initiator database. If **fromconnectionoptions** is specified it must include the database that contains the initiator service. If **fromconnectionoptions** is not specified, the **baseconnectionoptions** must specify the initiator database.  
  
 **TO SERVICE** *service_name*[, *broker_id* ]  
 Specifies the service that is the target for the conversations.  
  
 *service_name*: specifies the name of the target service.  
  
 *broker_id*: specifies the [!INCLUDE[ssSB](../../includes/sssb-md.md)] ID that identifies the target database. *broker_id* is a GUID. You can run the following query in the target database to find it:  
  
```  
SELECT service_broker_guid  
FROM sys.databases  
WHERE database_id = DB_ID();  
```  
  
 **\<toconnectionoptions>**  
 Specifies the information that is required to connect the database that holds the target service. If **toconnectionoptions** is not specified, **ssbdiagnose** uses the connection information from **baseconnectionoptions** to connect to the target database.  
  
 **MIRROR**  
 Specifies that the associated [!INCLUDE[ssSB](../../includes/sssb-md.md)] service is hosted in a mirrored database. **ssbdiagnose** verifies that the route to the service is a mirrored route, where MIRROR_ADDRESS was specified on CREATE ROUTE.  
  
 **\<mirrorconnectionoptions>**  
 Specifies the information that is required to connect to the mirror database. If **mirrorconnectionoptions** is not specified, **ssbdiagnose** uses the connection information from **baseconnectionoptions** to connect to the mirror database.  
  
 **ON CONTRACT** *contract_name*  
 Requests that **ssbdiagnose** only check configurations that use the specified contract. If ON CONTRACT is not specified, **ssbdiagnose** reports on the contract named DEFAULT.  
  
 **ENCRYPTION** { **ON** | **OFF** | **ANONYMOUS** }  
 Requests verification that the dialog is correctly configured for the specified level of encryption:  
  
 **ON**: Default setting. Full dialog security is configured. Certificates have been deployed on both sides of the dialog, a remote service binding is present, and the GRANT SEND statement for the target service specified the initiator user.  
  
 **OFF**: No dialog security is configured. No certificates have been deployed, no remote service binding was created, and the GRANT SEND for the initiator service specified the **public** role.  
  
 **ANONYMOUS**: Anonymous dialog security is configured. One certificate has been deployed, the remote service binding specified the anonymous clause, and the GRANT SEND for the target service specified the **public** role.  
  
 **RUNTIME**  
 Requests a report of issues that cause runtime errors for a [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation. If neither **-NEW** or **-ID** are specified, **ssbdiagnose** monitors all conversations in all databases specified in the connection options. If **-NEW** or **-ID** are specified, **ssbdiagnose** builds a list of the IDs specified in the parameters.  
  
 While **ssbdiagnose** is running, it records all [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] events that indicate runtime errors. It records the events that occur for the specified IDs, plus system-level events. If runtime errors are encountered, **ssbdiagnose** runs a configuration report on the associated configuration.  
  
 By default, runtime errors are not included in the output report, only the results of the configuration analysis. Use **-SHOWEVENTS** to have the runtime errors included in the report.  
  
 **-SHOWEVENTS**  
 Specifies that **ssbdiagnose** report [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] events during a RUNTIME report. Only events that are considered error conditions are reported. By default, **ssbdiagnose** only monitors error events; it does not report them in the output.  
  
 **-NEW**  
 Requests runtime monitoring of the first conversation that begins after **ssbdiagnose** starts running.  
  
 **-ID**  
 Requests runtime monitoring of the specified conversation elements. You can specify **-ID** multiple times.  
  
 If you specify a conversation handle, only events associated with the associated conversation endpoint are reported. If you specify a conversation ID, all events for that conversation and its initiator and target endpoints are reported. If a conversation group ID is specified, all events for all conversations and endpoints in the conversation group are reported.  
  
 *conversation_handle*  
 A unique identifier that identifies a conversation endpoint in an application. Conversation handles are unique to one endpoint of a conversation, the initiator and target endpoints have separate conversation handles.  
  
 Conversation handles are returned to applications by the *@dialog_handle* parameter of the **BEGIN DIALOG** statement, and the `conversation_handle` column in the result set of a **RECEIVE** statement.  
  
 Conversation handles are reported in the `conversation_handle` column of the **sys.transmission_queue** and **sys.conversation_endpoints** catalog views.  
  
 *conversation_group_id*  
 The unique identifier that identifies a conversation group.  
  
 Conversation group IDs are returned to applications by the *@conversation_group_id* parameter of the **GET CONVERSATION GROUP** statement and the `conversation_group_id` column in the result set of a **RECEIVE** statement.  
  
 Conversation group IDs are reported in the `conversation_group_id` columns of the **sys.conversation_groups** and **sys.conversation_endpoints** catalog views.  
  
 *conversation_id*  
 The unique identifier that identifies a conversation. Conversation IDs are the same for both the initiator and target endpoints of a conversation.  
  
 Conversation IDs are reported in the `conversation_id` column of the **sys.conversation_endpoints** catalog view.  
  
 **-TIMEOUT** *timeout_interval*  
 Specifies the number of seconds for a **RUNTIME** report to run. If **-TIMEOUT** is not specified the runtime report runs indefinitely. **-TIMEOUT** is used only on **RUNTIME** reports, not **CONFIGURATION** reports. Use ctrl + C to quit **ssbdiagnose** if **-TIMEOUT** was not specified or to end a runtime report before the time**-**out interval expires. *timeout_interval* must be a number between 1 and 2,147,483,647.  
  
 **\<runtimeconnectionoptions>**  
 Specifies the connection information for the databases that contain the services associated with conversation elements being monitored. If all the services are in the same database, you only have to specify one **CONNECT TO** clause. If the services are in separate databases you must supply a **CONNECT TO** clause for each database. If **runtimeconnectionoptions** is not specified, **ssbdiagnose** uses the connection information from **baseconnectionoptions**.  
  
 **-E**  
 Open a Windows Authentication connection to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by using your current Windows account as the login ID. The login must be a member of the **sysadmin** fixed-server role.  
  
 The -E option ignores the user and password settings of the SQLCMDUSER and SQLCMDPASSWORD environment variables.  
  
 If neither **-E** nor **-U** is specified, **ssbdiagnose** uses the value from the SQLCMDUSER environment variable. If SQLCMDUSER is not set either, **ssbdiagnose** uses Windows Authentication.  
  
 If the **-E** option is used together with the **-U** option or the **-P** option, an error message is generated.  
  
 **-U** *login_id*  
 Open a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication connection by using the specified login ID. The login must be a member of the **sysadmin** fixed-server role.  
  
 If neither **-E** nor **-U** is specified, **ssbdiagnose** uses the value from the SQLCMDUSER environment variable. If SQLCMDUSER is not set either, **ssbdiagnose** tries to connect by using Windows Authentication mode based on the Windows account of the user who is running **ssbdiagnose**.  
  
 If the **-U** option is used together with the **-E** option, an error message is generated. If the **-U** option is followed by more than one argument, an error message is generated and the program exits.  
  
 **-P** *password*  
 Specifies the password for the **-U** login ID. Passwords are case sensitive. If the **-U** option is used and the **-P** option is not used, **ssbdiagnose** uses the value from the SQLCMDPASSWORD environment variable. If SQLCMDPASSWORD is not set either, **ssbdiagnose** prompts the user for a password.  
  
> [!IMPORTANT]  
>  When you type a SET SQLCMDPASSWORD command, your password will be visible to anyone who can see your monitor.  
  
 If the **-P** option is specified without a password **ssbdiagnose** uses the default password (NULL).  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md).  
  
 The password prompt is displayed by printing the password prompt to the console, as follows: `Password:`  
  
 User input is hidden. This means that nothing is displayed and the cursor stays in position.  
  
 If the **-P** option is used with the **-E** option, an error message is generated.  
  
 If the **-P** option is followed by more than one argument, an error message is generated.  
  
 **-S** *server_name*[\\*instance_name*]  
 Specifies the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that holds the [!INCLUDE[ssSB](../../includes/sssb-md.md)] services to be analyzed.  
  
 Specify *server_name* to connect to the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on that server. Specify *server_name***\\***instance_name* to connect to a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on that server. If **-S** is not specified, **ssbdiagnose** uses the value of the SQLCMDSERVER environment variable. If SQLCMDSERVER is not set either, **ssbdiagnose** connects to the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on the local computer.  
  
 **-d** *database_name*  
 Specifies the database that holds the [!INCLUDE[ssSB](../../includes/sssb-md.md)] services to be analyzed. If the database does not exist, an error message is generated. If **-d** is not specified, the default is the database specified in the default-database property for your login.  
  
 **-l** *login_timeout*  
 Specifies the number of seconds before an attempt to connect to a server times out. If **-l** is not specified, **ssbdiagnose** uses the value set for the SQLCMDLOGINTIMEOUT environment variable. If SQLCMDLOGINTIMEOUT is not set either, the default time-out is thirty seconds. The login time-out must be a number between 0 and 65534. If the value that is supplied is not numeric or does not fall into that range, **ssbdiagnose** generates an error message. A value of 0 specifies time-out to be infinite.  
  
 **-?**  
 Displays command line help.  
  
## Remarks  
 Use **ssbdiagnose** to do the following:  
  
-   Confirm that there are no configuration errors in a newly configured [!INCLUDE[ssSB](../../includes/sssb-md.md)] application.  
  
-   Confirm that there are no configuration errors after changing the configuration of an existing [!INCLUDE[ssSB](../../includes/sssb-md.md)] application.  
  
-   Confirm that there are no configuration errors after a [!INCLUDE[ssSB](../../includes/sssb-md.md)] database is detached and then reattached to a new instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   Research whether there are configuration errors when messages are not successfully transmitted between services.  
  
-   Get a report of any errors that occur in a set of [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation elements.  
  
## Configuration Reporting  
 To correctly analyze the configuration used by a conversation, run a **ssbdiagnose** configuration report that uses the same options that are used by the conversation. If you specify a lower level of options for **ssbdiagnose** than are used by the conversation, **ssbdiagnose** might not report conditions that are required by the conversation. If you specify a higher level of options for **ssbdiagnose**, it might report items that are not required by the conversation. For example, a conversation between two services in the same database can be run with ENCPRYPTION OFF. If you run **ssbdiagnose** to validate the configuration between the two services, but use the default ENCRYPTION ON setting, **ssbdiagnose** reports that the database is missing a master key. A master key is not required for the conversation.  
  
 The **ssbdiagnose** configuration report analyzes only one [!INCLUDE[ssSB](../../includes/sssb-md.md)] service or a single pair of services every time it is run. To report on multiple pairs of [!INCLUDE[ssSB](../../includes/sssb-md.md)] services, build a .cmd command file that calls **ssbdiagnose** multiple times.  
  
## Runtime Reporting  
 When -RUNTIME is specified, **ssbdiagnose** searches all databases specified in **runtimeconnectionoptions** and **baseconnectionoptions** to build a list of [!INCLUDE[ssSB](../../includes/sssb-md.md)] IDs. The full list of IDs built depends on what is specified for -NEW and -ID:  
  
-   If neither **-NEW** or **-ID** are specified, the list includes all conversations for all databases specified in the connection options.  
  
-   If **-NEW** is specified, **ssbdiagnose** includes the elements for the first conversation that starts after **ssbdiagnose** is run. This includes the conversation ID and the conversation handles for both the target and initiator conversation endpoints.  
  
-   If **-ID** is specified with a conversation handle, only that handle is included in the list.  
  
-   If **-ID** is specified with a conversation ID, the conversation ID and the handles for both of its conversation endpoints are added to the list.  
  
-   If **-ID** is specified with a conversation group ID, all the conversation IDs and conversation handles in that group are added to the list.  
  
 The list does not include elements from databases that are not covered by the connection options. For example, assume that you use **-ID** to specify a conversation ID, but only provide a **runtimeconnectionoptions** clause for the initiator database and not the target database. **ssbdiagnose** will not include the target conversation handle in its list of IDs, only the conversation ID and the initiator conversation handle.  
  
 **ssbdiagnose** monitors the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] events from the databases covered by **runtimeconnectionoptions** and **baseconnectionoptions**. It searches for [!INCLUDE[ssSB](../../includes/sssb-md.md)] events that indicate an error was encountered by one or more of the [!INCLUDE[ssSB](../../includes/sssb-md.md)] IDs in the runtime list. **ssbdiagnose** also searches for system-level [!INCLUDE[ssSB](../../includes/sssb-md.md)] error events not specifically associated with any conversation group.  
  
 If **ssbdiagnose** finds conversation errors, the utility will attempt to report on the root cause of the events by also running a configuration report. **ssbdiagnose** uses the metadata in the databases to try to determine the instances, [!INCLUDE[ssSB](../../includes/sssb-md.md)] IDs, databases, services, and contracts used by the conversation. It then runs a configuration report using all available information.  
  
 By default, **ssbdiagnose** does not report error events. It only reports the underlying issues found during the configuration check. This minimizes the amount of information reported and helps you focus on the underlying configuration issues. You can specify **-SHOWEVENTS** to see the error events encountered by **ssbdiagnose**.  
  
## Issues Reported by ssbdiagnose  
 **ssbdiagnose** reports three classes of issues. In the XML output file, each class of issue is reported as a separate type of the Issue element. The three types of issues reported by **ssbdiagnose** are as follows:  
  
 **Diagnosis**  
 Reports a configuration issue. This includes issues found either a **CONFIGURATION** report is running, or during the configuration phase of a **RUNTIME** report. **ssbdiagnose** reports each configuration issue one time.  
  
 **Event**  
 Reports a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] event that indicates a problem was encountered by a conversation being monitored during a **RUNTIME** report. **ssbdiagnose** reports events every time they are generated. Events can be reported multiple times if several conversations encounter the problem.  
  
 **Problem**  
 Reports an issue that is preventing **ssbdiagnose** from completing a configuration analysis or from monitoring conversations.  
  
## sqlcmd Environment Variables  
 The **ssbdiagnose** utility supports the SQLCMDSERVER, SQLCMDUSER, SQLCMDPASSWORD, and SQLCMDLOGINTIMOUT environment variables that are also used by the **sqlcmd** utility. You can set the environment variables either by using the command prompt SET command, or by using the **setvar** command in [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that you run by using **sqlcmd**. For more information about how to use **setvar** in **sqlcmd**, see [Use sqlcmd with Scripting Variables](../../relational-databases/scripting/sqlcmd-use-with-scripting-variables.md).  
  
## Permissions  
 In each **connectionoptions** clause, the login specified with either **-E** or **-U** must be a member of the **sysadmin** fixed-server role in the instance specified in **-S**.  
  
## Examples  
 This section contains examples of using **ssbdiagnose** at a command prompt.  
  
### A. Checking the Configuration of Two Services in the Same Database  
 The following example shows how to request a configuration report when the following are true;  
  
-   The initiator and target service are in the same database.  
  
-   The database is in the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   The instances is on the same computer on which **ssbdiagnose** is run.  
  
 The **ssbdiagnose** utility reports the configuration that uses the DEFAULT contract because ON CONTRACT is not specified.  
  
```  
ssbdiagnose -E -d MyDatabase CONFIGURATION FROM SERVICE /test/initiator TO SERVICE /test/target  
```  
  
### B. Checking the Configuration of Two Services on Separate Computers That Use One Login  
 The following example shows how to request a configuration report when the initiator and target service are on separate computers, but can be accessed by using the same Windows Authentication login.  
  
```  
ssbdiagnose -E CONFIGURATION FROM SERVICE /text/initiator -S InitiatorComputer -d InitiatorDatabase TO SERVICE /test/target -S TargetComputer -d TargetDatabase ON CONTRACT TestContract  
```  
  
### C. Checking the Configuration of Two Services on Separate Computers That Use Separate Logins  
 The following example shows how to request a configuration report when the initiator and target service are on separate computers, and separate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication logins are required for each instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```  
ssbdiagnose CONFIGURATION FROM SERVICE /text/initiator   
-S InitiatorComputer -U InitiatorLogin -p !wEx23Dvb   
-d InitiatorDatabase TO SERVICE /test/target -S TargetComputer   
-U TargetLogin -p ER!49jiy -d TargetDatabase ON CONTRACT TestContract  
```  
  
### D. Checking Mirrored Service Configurations on Separate Computers With Anonymous Encryption  
 The following example shows how to request a configuration report when the initiator and target service are on separate computers and the initiator is mirrored to a named instance. The report also verifies that the services are configured to use anonymous encryption.  
  
```  
ssbdiagnose -E CONFIGURATION FROM SERVICE /text/initiator   
-S InitiatorComputer -d InitiatorDatabase MIRROR   
-S MirrorComputer/MirrorInstance TO SERVICE /test/target   
-S TargetComputer -d TargetDatabase ON CONTRACT TestContract ENCRYPTION ANONYMOUS  
```  
  
### E. Checking the Configuration of Two Contracts  
 The following example shows how to build a command file that requests configuration reports when the following are true:  
  
-   The initiator and target service are in the same database.  
  
-   The database is in the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   The instance is on the same computer on which **ssbdiagnose** is run.  
  
 Each time **ssbdiagnose** is run it reports the configuration for a different contract between the same services.  
  
```  
ssbdiagnose -E -d MyDatabase CONFIGURATION FROM SERVICE   
/test/initiator TO SERVICE /test/target ON CONTRACT PayRaiseContract  
ssbdiagnose -E -d MyDatabase CONFIGURATION FROM SERVICE /test/initiator   
TO SERVICE /test/target ON CONTRACT PromotionContract  
```  
  
### F. Monitor the status of a specific conversation on the local computer with a time out  
 The following example shows how to monitor a specific conversation where the initiator and target services are in the same database in the default instance of the same computer that is running **ssbdiagnose**. The time-out interval is set to 20 seconds.  
  
```  
ssbdiagnose -E -d TestDatabase RUNTIME -ID D68D77A9-B1CF-41BF-A5CE-279ABCAB140D -TIMEOUT 20  
```  
  
### G. Monitor the status of a conversation that spans two computers  
 The following example shows how to monitor a specific conversation where the initiator and target services are on separate computers.  
  
```  
ssbdiagnose RUNTIME -ID D68D77A9-B1CF-41BF-A5CE-279ABCAB140D   
-TIMEOUT 10 CONNECT TO -E -S InitiatorComputer/InitiatorInstance   
-d InitiatorDatabase CONNECT TO -E -S TargetComputer/TargetInstance   
-d TargetDatabase  
```  
  
### H. Monitor the status of a conversation in two databases in the same instance  
 The following example shows how to monitor a specific conversation where the initiator and target services are in separate databases in the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The example uses the **baseconnectionoptions** to specify the instance and login information, and two CONNECT TO clauses to specify the databases. -SHOWEVENTS is specified so that all runtime events are included in the report output.  
  
```  
ssbdiagnose -E -S TestComputer/DevTestInstance RUNTIME -SHOWEVENTS   
-ID 5094d4a7-e38c-4c37-da37-1d58b1cb8455 -TIMEOUT 10 CONNECT TO   
-d InitiatorDatabase CONNECT TO -d TargetDatabase  
```  
  
### I. Monitor the status of two conversations between two databases  
 The following example shows how to monitor two conversations where the initiator and target services are in separate databases in the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The example uses the **baseconnectionoptions** to specify the instance and login information, and two CONNECT TO clauses to specify the databases.  
  
```  
ssbdiagnose -E -S TestComputer/DevTestInstance RUNTIME   
-ID 5094d4a7-e38c-4c37-da37-1d58b1cb8455   
-ID 9b293be9-226b-4e22-e169-1d2c2c15be86 -TIMEOUT 10 CONNECT TO   
-d InitiatorDatabase CONNECT TO -d TargetDatabase  
```  
  
### J. Monitor the status of all conversations between two databases  
 The following example shows how to monitor all the conversation between two databases in the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The example uses the **baseconnectionoptions** to specify the instance and login information, and two CONNECT TO clauses to specify the databases.  
  
```  
ssbdiagnose -E -S TestComputer/DevTestInstance RUNTIME   
-TIMEOUT 10 CONNECT TO -d InitiatorDatabase CONNECT TO   
-d TargetDatabase  
```  
  
### K. Ignore Specific Errors  
 The following example shows how to ignore known errors (303 and 304) in how activation is currently configured in a test system.  
  
```  
ssbdiagnose -IGNORE 303 -IGNORE 304 -E -d TestDatabase   
CONFIGURATION FROM SERVICE /test/initiator TO SERVICE /test/target   
ON CONTRACT TextContract  
```  
  
### L. Redirecting ssbdiagnose XML Output  
 The following example shows how to request that **ssbdiagnose** generate its output as an XML file that is redirected to a file. The TestDiag.xml file can then be opened by an application to analyze or report **ssbdiagnose** XML files. Or, you can view it from a general XML editor such as XML Notepad.  
  
```  
ssbdiagnose -XML -E -d MyDatabase CONFIGURATION FROM SERVICE   
/test/initiator TO SERVICE /test/target > c:\MyDiagnostics\TestDiag.xml  
```  
  
### M. Using an Environment Variable  
 The following example first sets the SQLCMDSERVER environment variable to hold the server name, and then runs **ssbdiagnose** without specifying **-S**.  
  
```  
SET SQLCMDSERVER=MyComputer  
ssbdiagnose -XML -E -d MyDatabase CONFIGURATION FROM SERVICE   
/test/initiator TO SERVICE /test/target  
```  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/begin-dialog-conversation-transact-sql)   
 [CREATE BROKER PRIORITY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-broker-priority-transact-sql)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-certificate-transact-sql)   
 [CREATE CONTRACT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-contract-transact-sql)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-endpoint-transact-sql)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-master-key-transact-sql)   
 [CREATE MESSAGE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-message-type-transact-sql)   
 [CREATE QUEUE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-queue-transact-sql)   
 [CREATE REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-remote-service-binding-transact-sql)   
 [CREATE ROUTE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-route-transact-sql)   
 [CREATE SERVICE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-service-transact-sql)   
 [RECEIVE &#40;Transact-SQL&#41;](/sql/t-sql/statements/receive-transact-sql)   
 [sys.transmission_queue &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-transmission-queue-transact-sql)   
 [sys.conversation_endpoints &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql)   
 [sys.conversation_groups &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-conversation-groups-transact-sql)  
  
  
