---
title: "Scheduling Automatic Administrative Tasks in SQL Server Agent | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "scheduling administrative tasks [SMO]"
  - "SQL Server Agent [SMO]"
  - "automatic administrative SMO tasks"
ms.assetid: 900242ad-d6a2-48e9-8a1b-f0eea4413c16
author: stevestein
ms.author: sstein
manager: craigg
---
# Scheduling Automatic Administrative Tasks in SQL Server Agent
  In SMO, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent is represented by the following objects:  
  
-   The <xref:Microsoft.SqlServer.Management.Smo.Agent.JobServer> object has three collections of jobs, alerts and operators.  
  
-   The <xref:Microsoft.SqlServer.Management.Smo.Agent.OperatorCollection> object represents a list of pager, e-mail addresses and net send operators that can be notified of events automatically by the Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent.  
  
-   The <xref:Microsoft.SqlServer.Management.Smo.Agent.AlertCollection> object represents a list of circumstances such as system events or performance conditions that are monitored by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   The <xref:Microsoft.SqlServer.Management.Smo.Agent.JobCollection> object is slightly more complex. It represents a list of multi-step tasks that run at specified schedules. The steps and schedule information are stored in the <xref:Microsoft.SqlServer.Management.Smo.Agent.JobStep> and <xref:Microsoft.SqlServer.Management.Smo.Agent.JobSchedule> objects.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent objects are in the <xref:Microsoft.SqlServer.Management.Smo.Agent> namespace.  
  
## Examples  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual Basic SMO Project in Visual Studio .NET](../../../database-engine/dev-guide/create-a-visual-basic-smo-project-in-visual-studio-net.md) or [Create a Visual C&#35; SMO Project in Visual Studio .NET](../how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
1.  For programs that use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent, you must include the `Imports` statement to qualify the Agent namespace. Insert the statement after the other `Imports` statements, before any declarations in the application, such as:  
  
 `Imports Microsoft.SqlServer.Management.Smo`  
  
 `Imports Microsoft.SqlServer.Management.Common`  
  
 `Imports Microsoft.SqlServer.Management.Smo.Agent`  
  
## Creating a Job with Steps and a Schedule in Visual Basic  
 This code example creates a job with steps and a schedule, and then informs an operator.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBAgent1](SMO How to#SMO_VBAgent1)]  -->  
  
## Creating a Job with Steps and a Schedule in Visual C#  
 This code example creates a job with steps and a schedule, and then informs an operator.  
  
```  
{  
            //Connect to the local, default instance of SQL Server.  
            Server srv = new Server();  
  
            //Define an Operator object variable by supplying the Agent (parent JobServer object) and the name in the constructor.   
            Operator op = new Operator(srv.JobServer, "Test_Operator");  
  
            //Set the Net send address.   
            op.NetSendAddress = "Network1_PC";  
  
            //Create the operator on the instance of SQL Server Agent.   
            op.Create();  
  
            //Define a Job object variable by supplying the Agent and the name arguments in the constructor and setting properties.   
            Job jb = new Job(srv.JobServer, "Test_Job");  
  
            //Specify which operator to inform and the completion action.   
            jb.OperatorToNetSend = "Test_Operator";  
            jb.NetSendLevel = CompletionAction.Always;  
  
            //Create the job on the instance of SQL Server Agent.   
            jb.Create();  
  
            //Define a JobStep object variable by supplying the parent job and name arguments in the constructor.   
            JobStep jbstp = new JobStep(jb, "Test_Job_Step");  
            jbstp.Command = "Test_StoredProc";  
            jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess;  
            jbstp.OnFailAction = StepCompletionAction.QuitWithFailure;  
  
            //Create the job step on the instance of SQL Agent.   
            jbstp.Create();  
  
            //Define a JobSchedule object variable by supplying the parent job and name arguments in the constructor.   
  
            JobSchedule jbsch = new JobSchedule(jb, "Test_Job_Schedule");  
  
            //Set properties to define the schedule frequency, and duration.   
            jbsch.FrequencyTypes = FrequencyTypes.Daily;  
            jbsch.FrequencySubDayTypes = FrequencySubDayTypes.Minute;  
            jbsch.FrequencySubDayInterval = 30;  
            TimeSpan ts1 = new TimeSpan(9, 0, 0);  
            jbsch.ActiveStartTimeOfDay = ts1;  
  
            TimeSpan ts2 = new TimeSpan(17, 0, 0);  
            jbsch.ActiveEndTimeOfDay = ts2;  
            jbsch.FrequencyInterval = 1;  
  
            System.DateTime d = new System.DateTime(2003, 1, 1);  
            jbsch.ActiveStartDate = d;  
  
            //Create the job schedule on the instance of SQL Agent.   
            jbsch.Create();  
        }  
```  
  
## Creating a Job with Steps and a Schedule in PowerShell  
 This code example creates a job with steps and a schedule, and then informs an operator.  
  
```  
#Get a server object which corresponds to the default instance  
$srv = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Server  
  
#Define an Operator object variable by supplying the Agent (parent JobServer object) and the name in the constructor.  
$op = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.Operator -argumentlist $srv.JobServer, "Test_Operator"  
  
#Set the Net send address.  
$op.NetSendAddress = "Network1_PC"  
  
#Create the operator on the instance of SQL Agent.  
$op.Create()  
  
#Define a Job object variable by supplying the Agent and the name arguments in the constructor and setting properties.   
$jb = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.Job -argumentlist $srv.JobServer, "Test_Job"   
  
#Specify which operator to inform and the completion action.   
$jb.OperatorToNetSend = "Test_Operator";   
$jb.NetSendLevel = [Microsoft.SqlServer.Management.SMO.Agent.CompletionAction]::Always  
  
#Create the job on the instance of SQL Server Agent.   
$jb.Create()  
  
#Define a JobStep object variable by supplying the parent job and name arguments in the constructor.   
$jbstp = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.JobStep -argumentlist $jb, "Test_Job_Step"   
$jbstp.Command = "Test_StoredProc";   
$jbstp.OnSuccessAction = [Microsoft.SqlServer.Management.SMO.Agent.StepCompletionAction]::QuitWithSuccess;   
$jbstp.OnFailAction =[Microsoft.SqlServer.Management.SMO.Agent.StepCompletionAction]::QuitWithFailure;   
  
#Create the job step on the instance of SQL Agent.   
$jbstp.Create();   
  
#Define a JobSchedule object variable by supplying the parent job and name arguments in the constructor.   
$jbsch =  New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.JobSchedule -argumentlist $jb, "Test_Job_Schedule"   
  
#Set properties to define the schedule frequency, and duration.   
$jbsch.FrequencyTypes =  [Microsoft.SqlServer.Management.SMO.Agent.FrequencyTypes]::Daily  
  
$jbsch.FrequencySubDayTypes = [Microsoft.SqlServer.Management.SMO.Agent.FrequencySubDayTypes]::Minute  
$jbsch.FrequencySubDayInterval = 30  
$ts1 =  New-Object -TypeName TimeSpan -argumentlist 9, 0, 0  
$jbsch.ActiveStartTimeOfDay = $ts1  
$ts2 = New-Object -TypeName TimeSpan -argumentlist 17, 0, 0  
$jbsch.ActiveEndTimeOfDay = $ts2  
$jbsch.FrequencyInterval = 1  
$jbsch.ActiveStartDate = "01/01/2003"  
  
#Create the job schedule on the instance of SQL Agent.   
$jbsch.Create();  
```  
  
## Creating an Alert in Visual Basic  
 This code example creates an alert that is triggered by a performance condition. The condition must be provided in a specific format:  
  
 **ObjectName|CounterName|Instance|ComparisionOp|CompValue**  
  
 An operator is required for the alert notification. The <xref:Microsoft.SqlServer.Management.Smo.Agent.Operator> type requires square parentheses because `operator` is a Visual Basic keyword.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBAgent3](SMO How to#SMO_VBAgent3)]  -->  
  
## Creating an Alert in Visual C#  
 This code example creates an alert that is triggered by a performance condition. The condition must be provided in a specific format:  
  
 **ObjectName|CounterName|Instance|ComparisionOp|CompValue**  
  
 An operator is required for the alert notification. The <xref:Microsoft.SqlServer.Management.Smo.Agent.Operator> type requires square parentheses because `operator` is a [!INCLUDE[csprcs](../../../includes/csprcs-md.md)] keyword.  
  
```  
{  
             //Connect to the local, default instance of SQL Server.   
            Server srv = new Server();  
  
            //Define an Alert object variable by supplying the SQL Server Agent and the name arguments in the constructor.   
            Alert al = new Alert(srv.JobServer, "Test_Alert");  
  
            //Specify the performance condition string to define the alert.   
            al.PerformanceCondition = "SQLServer:General Statistics|User Connections||>|3";  
  
            //Create the alert on the SQL Agent.   
            al.Create();  
  
            //Define an Operator object variable by supplying the SQL Server Agent and the name arguments in the constructor.   
  
            Operator op = new Operator(srv.JobServer, "Test_Operator");  
            //Set the net send address.   
            op.NetSendAddress = "NetworkPC";  
            //Create the operator on the SQL Agent.   
            op.Create();  
            //Run the AddNotification method to specify the operator is notified when the alert is raised.   
            al.AddNotification("Test_Operator", NotifyMethods.NetSend);  
        }  
```  
  
## Creating an Alert in PowerShell  
 This code example creates an alert that is triggered by a performance condition. The condition must be provided in a specific format:  
  
 **ObjectName|CounterName|Instance|ComparisionOp|CompValue**  
  
 An operator is required for the alert notification. The <xref:Microsoft.SqlServer.Management.Smo.Agent.Operator> type requires square parentheses because `operator` is a [!INCLUDE[csprcs](../../../includes/csprcs-md.md)] keyword.  
  
```  
#Get a server object which corresponds to the default instance  
$srv = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Server  
  
#Define an Alert object variable by supplying the SQL Agent and the name arguments in the constructor.  
$al = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.Alert -argumentlist $srv.JobServer, "Test_Alert"  
  
#Specify the performance condition string to define the alert.  
$al.PerformanceCondition = "SQLServer:General Statistics|User Connections||>|3"  
  
#Create the alert on the SQL Agent.  
$al.Create()  
  
#Define an Operator object variable by supplying the Agent (parent JobServer object) and the name in the constructor.  
$op = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Agent.Operator -argumentlist $srv.JobServer, "Test_Operator"  
  
#Set the Net send address.  
$op.NetSendAddress = "Network1_PC"  
  
#Create the operator on the instance of SQL Agent.  
$op.Create()  
  
#Run the AddNotification method to specify the operator is notified when the alert is raised.  
$ns = [Microsoft.SqlServer.Management.SMO.Agent.NotifyMethods]::NetSend  
$al.AddNotification("Test_Operator", $ns)  
  
#Drop the alert and the operator  
$al.Drop()  
$op.Drop()  
```  
  
## Allowing User Access to Subsystem by Using a Proxy Account in Visual Basic  
 This code example shows how to allow a user access to a specified subsystem by using the <xref:Microsoft.SqlServer.Management.Smo.Agent.ProxyAccount.AddSubSystem%2A> method of the <xref:Microsoft.SqlServer.Management.Smo.Agent.ProxyAccount> object.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VBAgent2](SMO How to#SMO_VBAgent2)]  -->  
  
## Allowing User Access to Subsystem by Using a Proxy Account in Visual C#  
 This code example shows how to allow a user access to a specified subsystem by using the <xref:Microsoft.SqlServer.Management.Smo.Agent.ProxyAccount.AddSubSystem%2A> method of the <xref:Microsoft.SqlServer.Management.Smo.Agent.ProxyAccount> object.  
  
```  
//Connect to the local, default instance of SQL Server.   
{   
Server srv = default(Server);   
srv = new Server();   
//Declare a JobServer object variable and reference the SQL Server Agent.   
JobServer js = default(JobServer);   
js = srv.JobServer;   
//Define a Credential object variable by supplying the parent server and name arguments in the constructor.   
Credential c = default(Credential);   
c = new Credential(srv, "Proxy_accnt");   
//Set the identity to a valid login represented by the vIdentity string variable.   
//The sub system will run under this login.   
c.Identity = vIdentity;   
//Create the credential on the instance of SQL Server.   
c.Create();   
//Define a ProxyAccount object variable by supplying the SQL Server Agent, the name, the credential, the description arguments in the constructor.   
ProxyAccount pa = default(ProxyAccount);   
pa = new ProxyAccount(js, "Test_proxy", "Proxy_accnt", true, "Proxy account for users to run job steps in command shell.");   
//Create the proxy account on the SQL Agent.   
pa.Create();   
//Add the login, represented by the vLogin string variable, to the proxy account.   
pa.AddLogin(vLogin);   
//Add the CmdExec subsytem to the proxy account.   
pa.AddSubSystem(AgentSubSystem.CmdExec);   
}   
//Now users logged on as vLogin can run CmdExec job steps with the specified credentials.   
```  
  
## See Also  
 [SQL Server Agent](../../../ssms/agent/sql-server-agent.md)   
 [Implement Jobs](../../../ssms/agent/implement-jobs.md)  
  
  
